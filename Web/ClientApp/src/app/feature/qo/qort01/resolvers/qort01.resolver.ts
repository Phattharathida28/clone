import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot, Router} from '@angular/router';
import { forkJoin, map, Observable, of } from 'rxjs';
import { Qort01Service } from '../qort01.service';


@Injectable({
  providedIn: 'root'
})
export class Qort01Resolver implements Resolve<any> {
	
  constructor(private router: Router, private service: Qort01Service) { }
  
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> {
    const master = this.service.getMaster();
    return forkJoin([master]).pipe(map((result) => {
      let master = result[0];
      return{master}
    }))
  }
}
