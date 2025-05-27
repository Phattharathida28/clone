import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { ActivityLog } from '@app/models/su/activityLog';
import { inject } from '@angular/core';
import { Search, Surt08Service } from './surt08.service';
import { PaginatedDataSource } from '@app/shared/components/table-server/server-datasource';
import { PageRequest, PageCriteria } from '@app/shared/components/table-server/page';

export const activityLogs: ResolveFn<PaginatedDataSource<ActivityLog, Search>> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const service: Surt08Service = inject(Surt08Service)
  const datasource: PaginatedDataSource<ActivityLog, Search> = new PaginatedDataSource<ActivityLog, Search>(
    (activityLog: PageRequest<ActivityLog>, search: Search) => service.list(activityLog, search),
    new PageCriteria('created_date desc'))
  datasource.queryBy({ keywords: "" })

  return datasource
};