import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from '@env/environment';
import { enableProdMode } from '@angular/core';


export function removeSlash(url: string) {
  const last = url.charAt(url.length - 1);
  return last === '/' ? url.slice(0, -1) : url;
}

export function getBaseUrl() {
  return environment.production ? removeSlash(document.getElementsByTagName('base')[0].href) : environment.apiUrl;
}

export function getClientUrl() {
  return removeSlash(document.getElementsByTagName('base')[0].href);
}

const providers = [
  { provide: 'CLIENT_URL', useFactory: getClientUrl, deps: [] },
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
];

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers).bootstrapModule(AppModule).catch(err => console.error(err));
