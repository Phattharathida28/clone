import { Component } from '@angular/core';
import { Search } from './surt08.service';
import { ActivatedRoute } from '@angular/router';
import { ActivityLog } from '@app/models/su/activityLog';
import { PaginatedDataSource } from '@app/shared/components/table-server/server-datasource';

@Component({
  selector: 'x-surt08',
  templateUrl: './surt08.component.html'
})
export class Surt08Component {
  activityLogs: PaginatedDataSource<ActivityLog, Search>

  constructor(private activatedRoute: ActivatedRoute) {
    this.activatedRoute.data.subscribe(({ activityLogs }) => this.activityLogs = activityLogs)
  }

  search = (value: string = ''): void => this.activityLogs.queryBy({ keywords: value }, true)
}
