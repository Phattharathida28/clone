import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, ResolveFn, Router, RouterStateSnapshot } from '@angular/router';
import { Program } from '@app/models/su/program';
import { Master, Surt01Service } from './surt01.service';

export const programs: ResolveFn<Program[]> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(Surt01Service).list()

export const program: ResolveFn<Program> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const { programCode } = inject(Router).getCurrentNavigation()?.extras.state as { programCode: string } || { programCode: null }

  return inject(Surt01Service).detail(programCode)
}

export const master: ResolveFn<Master> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(Surt01Service).master()