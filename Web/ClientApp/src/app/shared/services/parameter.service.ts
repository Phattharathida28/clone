import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CacheService } from './cache.service';
import { shareReplay } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ParameterService {

  constructor(private http: HttpClient, private cache: CacheService<any>) { }

  getParameter(group: string, code?: string) {
    let parameter$ = this.cache.getValue(`${group}|${code}`);
    if (!parameter$) {
      parameter$ = this.http.get<any>(`parameter/${group}/${code || ''}`).pipe(
        shareReplay(1)
      );
      this.cache.setValue(`${group}|${code}`, parameter$);
    }
    return parameter$;
  }
}
