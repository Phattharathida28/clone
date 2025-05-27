import { ApplicationRef, Inject, Injectable, inject } from '@angular/core';
import { User, UserManager } from 'oidc-client-ts';
import { BehaviorSubject, concat, filter, first, firstValueFrom, from, map, mergeMap, Observable, take, tap } from 'rxjs';
import { ConfigurationService } from '../services/configuration.service';
import { Config, LayoutService } from '@app/layout/service/layout.service';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private redirectUrlKey = 'redirect-url';
  private userManager: UserManager = new UserManager({
    authority: this.configuration.getConfig().IdentityUrl,
    client_id: 'ccs.spa',
    redirect_uri: `${this.clientUrl}/signin-callback`,
    silent_redirect_uri: `${this.clientUrl}/silent-callback.html`,
    accessTokenExpiringNotificationTimeInSeconds: 8,
    post_logout_redirect_uri: `${this.clientUrl}`,
    response_type: 'code',
    filterProtocolClaims: false,
    loadUserInfo: true,
    automaticSilentRenew: true,
    monitorSession: true,
    scope: 'openid profile ccs.resource.api ccs.content.api ccs.report.api'
  });
  private userSubject: BehaviorSubject<User | null> = new BehaviorSubject(null as User | null);
  constructor(private configuration: ConfigurationService, private appRef: ApplicationRef, @Inject('CLIENT_URL') private clientUrl: string) {

  }

  private async ensureUserManagerInitialized() {

    if (this.userManager !== undefined) return;

    await firstValueFrom(this.appRef.isStable.pipe(first(isStable => isStable)));
    if (!this.userManager) {
      const settings = {
        authority: this.configuration.getConfig().IdentityUrl,
        client_id: 'ccs.spa',
        redirect_uri: `${this.clientUrl}/signin-callback`,
        silent_redirect_uri: `${this.clientUrl}/silent-callback.html`,
        accessTokenExpiringNotificationTimeInSeconds: 8,
        post_logout_redirect_uri: `${this.clientUrl}`,
        response_type: 'code',
        filterProtocolClaims: false,
        loadUserInfo: true,
        automaticSilentRenew: true,
        monitorSession: true,
        scope: 'openid profile ccs.resource.api ccs.content.api ccs.report.api'
      };
      this.userManager = new UserManager(settings);
      this.userManager.events.addUserLoaded(user => {

        this.userSubject.next(user);
      })
      this.userManager.events.addUserSignedOut(async () => {

        this.userSubject.next(null);
        await this.userManager.signoutRedirect();
      });
    }
  }

  public isAuthenticated = (): Observable<boolean> => this.getUser().pipe(map(u => !!u && !u.expired));

  public getUser(): Observable<User | null> {
    return concat(
      this.userSubject.pipe(take(1), filter(u => !!u)),
      this.getUserFromStorage().pipe(filter(u => !!u), tap(u => this.userSubject.next(u))),
      this.userSubject.asObservable());
  }

  public getAccessToken(): Observable<string> {
    return this.isAuthenticated().pipe(
      first(u => !!u),
      mergeMap(() => this.userManager.getUser()),
      map(u => u ? `${u?.["token_type"]} ${u?.["access_token"]}` : "")
    );
  }


  private getUserFromStorage(): Observable<User | null> {
    return from(this.ensureUserManagerInitialized())
      .pipe(
        mergeMap(() => this.userManager.getUser())
      );
  }

  public async login(): Promise<void> {
    await this.ensureUserManagerInitialized();
    return this.userManager.signinRedirect();
  }

  public async completeSignIn(): Promise<void | null> {
    await this.ensureUserManagerInitialized();
    return this.userManager.signinCallback().then(user => {
      if (user) {
        const setting = {
          ripple: user.profile["Ripple"] == "True",
          inputStyle: user.profile["Inputstyle"] || "outlined",
          menuMode: user.profile["Menumode"] || "static",
          scale: Number(user.profile["Scale"]) || 14,
          color: user.profile["Color"] || "#3B82F6"
        }
        let colorScheme: string = user.profile["Colorscheme"] as string || null

        if (colorScheme == "system" || !colorScheme) {
          if (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches) colorScheme = "dark";
          else colorScheme = "light"
        }

        setting["colorScheme"] = colorScheme

        localStorage.setItem("access_token", user.access_token)
        localStorage.setItem("setting", JSON.stringify(setting as Config))
        inject(LayoutService).changeTheme()
        this.userSubject.next(user);
      }
    }).catch(() => {
      return null;
    })

  }

  public async logout(reAuthen: boolean = false, token: string = "", programPath: string = ""): Promise<void> {
    await this.ensureUserManagerInitialized();
    localStorage.removeItem("access_token")
    if (reAuthen) return this.userManager.signoutRedirect({ extraQueryParams: { 'reAuthen': reAuthen.toString(), 'token': token, 'programPath': programPath } });
    else return this.userManager.signoutRedirect();
  }

  get redirectUrl() {
    return sessionStorage.getItem(this.redirectUrlKey) || '/';
  }
  set redirectUrl(value) {
    sessionStorage.setItem(this.redirectUrlKey, value);
  }

  get getPermission() {
    return this.userSubject.pipe(filter(f => !!f), take(1), map((m: any) => {
      if (typeof (m.profile["Permission"]) == "string") return [m.profile["Permission"]];
      else return m.profile["Permission"];
    }))
  }

}