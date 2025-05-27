import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '@app/core/authentication/auth.service';

@Component({
  selector: 'x-signin-callback-component',
  template: `<p>Processing signin callback</p>`
})
export class SigninCallbackComponent implements OnInit {

  constructor(private readonly _router: Router, private readonly _authService: AuthService) { }

  async ngOnInit() {
    await this._authService.completeSignIn();
    this._router.navigate([this._authService.redirectUrl], { replaceUrl: true });
  }
}
