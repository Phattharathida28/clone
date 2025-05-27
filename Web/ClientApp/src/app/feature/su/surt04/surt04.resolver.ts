import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, ResolveFn, Router, RouterStateSnapshot } from '@angular/router';
import { User } from '@app/models/su/user';
import { Master, Surt04Service } from './surt04.service';

export const users: ResolveFn<User[]> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(Surt04Service).list()

export const user: ResolveFn<User> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
    const { userId } = inject(Router).getCurrentNavigation()?.extras.state as { userId: number } || { userId: 0 }
  
    return inject(Surt04Service).detail(userId)
  }
  
  export const master: ResolveFn<Master> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(Surt04Service).master()