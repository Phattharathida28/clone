import { Observable, forkJoin } from 'rxjs';
import { TranslateLoader } from '@ngx-translate/core';
import { LazyTranslationService } from './lazy-translation.service';
import { map } from 'rxjs/operators';

export const isObject = (item: any) => (item && typeof item === 'object' && !Array.isArray(item));

export const mergeDeep = (target: any, ...sources: any[]): any => {
  if (!sources.length) return target;
  const source = sources.shift();

  if (isObject(target) && isObject(source)) {
    for (const key in source) {
      if (isObject(source[key])) {
        if (!target[key]) Object.assign(target, {
          [key]: {}
        });
        mergeDeep(target[key], source[key]);
      } else {
        Object.assign(target, {
          [key]: source[key]
        });
      }
    }
  }

  return mergeDeep(target, ...sources);
}

export class TranslateHttpLoader implements TranslateLoader {
  constructor(private lazy: LazyTranslationService) { }

  public getTranslation(lang: string): Observable<Object> {
    this.lazy.translationLoaded = true;
    const requests = this.lazy.loadByLang(lang);
    let result = {};

    return forkJoin(requests).pipe(map(response => {
      response.forEach(res => {
        result = mergeDeep(result, res);
      })
      return mergeDeep(result, this.lazy.getLoadedTranslate(lang));
    }));
  }


}