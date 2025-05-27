import { ActivatedRouteSnapshot, ResolveFn, Router, RouterStateSnapshot } from '@angular/router';
import { Surt06Service } from './surt06.service';
import { Parameter } from '@app/models/su/parameter';
import { inject } from '@angular/core';

export const list: ResolveFn<Parameter[]> = (route, state) => inject(Surt06Service).list();

export const detail: ResolveFn<Parameter> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const { parameterGroupCode, parameterCode } = inject(Router).getCurrentNavigation()?.extras.state as { parameterGroupCode: string, parameterCode: string } || { parameterGroupCode: null, parameterCode: null }

  if (!parameterCode && !parameterGroupCode) {
    return null;
  }
  return inject(Surt06Service).detail(parameterGroupCode, parameterCode)
}