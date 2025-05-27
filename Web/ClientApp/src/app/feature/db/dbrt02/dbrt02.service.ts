import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ListItemGroup } from '@app/models/db/listItemGroup';
import { Lang } from '@app/shared/types/data.types';
import { Observable } from 'rxjs';

export type Master = {
  langs: Lang[];
}

@Injectable({
  providedIn: 'root'
})
export class Dbrt02Service {

  constructor(private http: HttpClient) { }

  list = (keywords: string = ""): Observable<ListItemGroup[]> => this.http.get<ListItemGroup[]>("dbrt02/list", { params: { keywords } })

  detail = (listItemGroupCode: string): Observable<ListItemGroup> => this.http.get<ListItemGroup>("dbrt02/detail", { params: { listItemGroupCode } })

  master = (): Observable<Master> => this.http.get<Master>("dbrt02/master")

  save = (ListItemGroup: ListItemGroup): Observable<ListItemGroup> => this.http.post<ListItemGroup>('dbrt02/save', ListItemGroup);

  delete = (listItemGroupCode: string): Observable<void> => this.http.delete<void>("dbrt02/delete", { params: { listItemGroupCode } })
}
