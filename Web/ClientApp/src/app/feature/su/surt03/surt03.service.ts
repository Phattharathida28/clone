import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Profile } from '@app/models/su/profile';
import { Observable } from 'rxjs';

export type Master = {
  menus: { menuName: string, menuCode: string, icon?: string }[]
}

@Injectable({
  providedIn: 'root'
})
export class Surt03Service {

  constructor(private http: HttpClient) { }

  list = (keywords: string = ""): Observable<Profile[]> => this.http.disableLoading().get<Profile[]>("surt03/list", { params: { keywords } })

  detail = (profileCode: string) => this.http.get<Profile>('surt03/detail', { params: { profileCode } })

  master = () => this.http.get<any>('surt03/master')

  save = (data: Profile) => this.http.put("surt03/update", data);

  delete = (profileCode: string) => this.http.delete("surt03/delete", { params: { profileCode } })
}
