import { ActivatedRouteSnapshot, ResolveFn, Router, RouterStateSnapshot } from '@angular/router';
import { Master, Surt02Service } from './surt02.service';
import { inject } from '@angular/core';
import { Menu } from '@app/models/su/menu';

export const menus: ResolveFn<Menu[]> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(Surt02Service).list()

export const menu: ResolveFn<Menu> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const { menuCode } = inject(Router).getCurrentNavigation()?.extras.state as { menuCode: string } || { menuCode: null }

  return inject(Surt02Service).detail(menuCode)
}

export const master: ResolveFn<Master> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(Surt02Service).master()