import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivityLog } from '@app/models/su/activityLog';
import { PageRequest, PaginatedEndpoint } from '@app/shared/components/table-server/page';
import { Observable } from 'rxjs';

export type Search = {
  keywords: string
}

@Injectable({
  providedIn: 'root'
})
export class Surt08Service {

  constructor(private http: HttpClient) { }

  list = (page: PageRequest<ActivityLog>, query: Search): Observable<PaginatedEndpoint<ActivityLog, Search>> => this.http.post<PaginatedEndpoint<ActivityLog, Search>>('surt08/list', { ...page, ...query });
}
