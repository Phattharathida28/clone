import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, ResolveFn, Router, RouterStateSnapshot } from '@angular/router';
import { Profile } from '@app/models/su/profile';
import { Master, Surt03Service } from './surt03.service';

export const profiles: ResolveFn<Profile[]> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(Surt03Service).list()

export const profile: ResolveFn<Profile> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const { profileCode } = inject(Router).getCurrentNavigation()?.extras.state as { profileCode: string } || { profileCode: null }

  return inject(Surt03Service).detail(profileCode)
}

export const master: ResolveFn<Master> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(Surt03Service).master()