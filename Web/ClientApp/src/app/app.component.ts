import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, GuardsCheckEnd, NavigationCancel, NavigationEnd, RouteConfigLoadEnd, RouteConfigLoadStart, Router } from '@angular/router';
import { Subject, filter, map, switchMap, takeUntil } from 'rxjs';
import { AuditService } from './core/authentication/audit.service';
import { I18nService } from './core/services/i18n.service';
import { LoadingService } from './pages/loading/loading.service';
import { PrimeNGConfig } from 'primeng/api';

@Component({
  selector: 'x-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit, OnDestroy {
  protected ngUnsubscribe: Subject<void> = new Subject<void>();
  constructor(
    private activatedRoute: ActivatedRoute,
    private i18n: I18nService,
    private router: Router,
    private ls: LoadingService,
    private audit: AuditService,
    private primengConfig: PrimeNGConfig
  ) { }

  ngOnInit() {
    this.primengConfig.ripple = true;
    this.i18n.init();
    this.router.events.pipe(takeUntil(this.ngUnsubscribe)).subscribe((evt) => {
      if (evt instanceof RouteConfigLoadStart || evt instanceof GuardsCheckEnd) this.ls.show();
      else if (evt instanceof RouteConfigLoadEnd || evt instanceof NavigationEnd || evt instanceof NavigationCancel) this.ls.hide();
    });

    const onNavigationEnd = this.router.events.pipe(filter(event => event instanceof NavigationEnd));

    onNavigationEnd.pipe(
      map(() => {
        let route = this.activatedRoute;
        while (route.firstChild) {
          route = route.firstChild;
        }
        return route;
      }),
      filter(route => route.outlet === 'primary'),
      switchMap(route => route.data),
      takeUntil(this.ngUnsubscribe)
    ).subscribe(event => {
      const code = event["code"];
      this.audit.code = code;
    });
  }

  ngOnDestroy() {
    this.i18n.destroy();
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
