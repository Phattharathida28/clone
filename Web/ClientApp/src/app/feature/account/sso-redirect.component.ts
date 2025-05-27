import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@app/core/authentication/auth.service';
import { ConfigurationService } from '@app/core/services/configuration.service';
import { environment } from '@env/environment';

@Component({
  selector: 'x-sso-redirect-component',
  template: `<p>Processing Redirect</p>`
})
export class SsoRedirectComponent implements OnInit {
  constructor(
    private readonly _route: ActivatedRoute,
    private readonly _router: Router,
    private readonly _config: ConfigurationService,
    private readonly _authService: AuthService
  ) { }

  async ngOnInit() {
    this._route.queryParams.subscribe(params => {
      const token = params["token"];
      const programPath = params["programPath"];

      if (!token) this._router.navigate(['/']);

      this._authService.isAuthenticated().subscribe(isAuthen => {
        if (isAuthen) this._authService.logout(true, token, programPath);
        else {
          const url = environment.production ? this._config.getConfig().IdentityUrl : 'https://localhost:5001'
          window.location.replace(`${url}/external/ssoredirect?token=` + token + (programPath ? '&programpath=' + programPath : ''));
        }
      })
    });
  }
}