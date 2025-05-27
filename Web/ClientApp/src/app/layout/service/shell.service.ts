import { Routes, Route } from '@angular/router';
import { CanActivate, CanActivateChild } from '@app/core/guard/core.guard';
import { LayoutComponent } from '../layout.component';

export class LayoutRoute {

  static childRoutes(routes: Routes): Route {
    return {
      path: '',
      component: LayoutComponent,
      children: routes,
      canActivate: [CanActivate],
      canActivateChild: [CanActivateChild],
      data: { reuse: true }
    };
  }
}