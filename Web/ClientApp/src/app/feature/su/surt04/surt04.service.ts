import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '@app/models/su/user';
import { Observable } from 'rxjs';

export class Master {
  profiles: []
}

@Injectable({
  providedIn: 'root'
})
export class Surt04Service {

  constructor(private http: HttpClient) { }

  list = (keywords: string = ""): Observable<User[]> => this.http.disableLoading().get<User[]>("surt04/list", { params: { keywords } })

  detail = (userId: number) => this.http.get<User>('surt04/detail', { params: { userId } })

  master = () => this.http.get<any>('surt04/master')

  save = (data: User) => this.http.put("surt04/update", data);

  delete = (userId: number) => this.http.delete("surt04/delete", { params: { userId } })
}
