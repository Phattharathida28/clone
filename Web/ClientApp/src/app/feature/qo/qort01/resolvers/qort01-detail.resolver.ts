import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, ResolveFn, Router, RouterStateSnapshot } from '@angular/router';
import { Qort01Component } from '../qort01.component';
import { Qort01Service } from '../qort01.service';
import { forkJoin, map, Observable, of } from 'rxjs';
import { master } from '@app/feature/su/surt01/surt01.resolver';
import { DetailDTO } from '@app/models/qo/standardPrice';

@Injectable({
  providedIn: 'root'
})
export class Qort01DetailResolver implements Resolve<any> {
  
  constructor(private router: Router, private sv: Qort01Service) { }
  
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> {
    const code = this.router.getCurrentNavigation()?.extras.state as any;
    const detail = code && code.plId ? this.sv.getDetail(code.plId) : of({detailItem:[]} as Partial<DetailDTO>);
    const master = this.sv.getMaster();
    return forkJoin([master,detail]).pipe(map((result) => {
      let master = result[0];
      let detail = result[1];
      return { master, detail}
    }))
  }
}