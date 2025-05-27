import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, ResolveFn, Router, RouterStateSnapshot } from '@angular/router';
import { ListItemGroup } from '@app/models/db/listItemGroup';
import { Dbrt02Service, Master } from './dbrt02.service';

export const listItemGroups: ResolveFn<ListItemGroup[]> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(Dbrt02Service).list()

export const listItemGroup: ResolveFn<ListItemGroup> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const { listItemGroupCode } = inject(Router).getCurrentNavigation()?.extras.state as { listItemGroupCode: string } || { listItemGroupCode: '' }

  return inject(Dbrt02Service).detail(listItemGroupCode)
}

export const master: ResolveFn<Master> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(Dbrt02Service).master()