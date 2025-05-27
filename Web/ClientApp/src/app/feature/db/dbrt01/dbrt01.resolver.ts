import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, ResolveFn, Router, RouterStateSnapshot } from '@angular/router';
import { Language } from '@app/models/db/language';
import { Dbrt01Service, Master } from './dbrt01.service';

export const languages: ResolveFn<Language[]> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(Dbrt01Service).list()

export const language: ResolveFn<Language> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const { languageCode } = inject(Router).getCurrentNavigation()?.extras.state as { languageCode: string } || { languageCode: '' }

  return inject(Dbrt01Service).detail(languageCode)
}

export const master: ResolveFn<Master> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(Dbrt01Service).master()