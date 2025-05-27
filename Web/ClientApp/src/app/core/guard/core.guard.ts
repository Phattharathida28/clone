import { ActivatedRouteSnapshot, CanActivateChildFn, CanActivateFn, CanDeactivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { Observable, map, mergeMap, take } from 'rxjs';
import { AuthService } from '../authentication/auth.service';
import { User } from 'oidc-client-ts';
import { Injectable, inject } from '@angular/core';
import { environment } from '@env/environment';

export const CanActivate: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> => inject(GuardService).checkUser(state)


export const CanActivateChild: CanActivateChildFn = (childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> => {
  const sv = inject(GuardService)
  return sv.checkUser(state).pipe(mergeMap(() => sv.checkRole(childRoute)))
}


export const CanDeactivate: CanDeactivateFn<any> = (component: any, currentRoute: ActivatedRouteSnapshot, currentState: RouterStateSnapshot, nextState: RouterStateSnapshot) => {
  if (component.canDeactivate) {
    const result = component.canDeactivate();

    if (typeof result == "boolean") return result;

    return (result as Observable<boolean>)
  }

  return true;
}

@Injectable({ providedIn: 'root' })
export class GuardService {
  constructor(private authService: AuthService, private router: Router) { }
  checkUser = (state: RouterStateSnapshot): Observable<boolean> => {
    return this.authService.isAuthenticated().pipe(
      map(login => {
        if (login) return true;

        this.authService.redirectUrl = state.url;
        this.authService.login();

        return false;
      })
    )
  }

  checkRole = (childRoute: ActivatedRouteSnapshot): Observable<boolean> => {
    return this.authService.getUser().pipe(
      take(1),
      map((user: User | null) => {
        const code = (childRoute.data["code"] ?? '').toUpperCase();
        const roles = user?.profile?.["role"] as string[] ?? [];

        if (!environment.production) {
          const DEMO = [
            "lifecycle",
            "binding",
            "formlayout",
            "input",
            "floatlabel",
            "invalidstate",
            "button",
            "table",
            "panel",
            "overlay",
            "message",
            "charts",
            "icons",
            "attachment"
          ].map(m => m.toUpperCase())

          if (!roles.some(s => DEMO.includes(s))) {
            DEMO.forEach(f => roles.push(f))
          }
        }

        if ((roles.length && roles.includes(code)) || !childRoute.component) return true;

        this.router.navigate(['access-denied'], { replaceUrl: true })

        return false;
      })
    )
  }
}