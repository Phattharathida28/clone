import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Program } from '@app/models/su/program';
import { SelectItem } from 'primeng/api';
import { Observable } from 'rxjs';

export type Master = {
  lang: SelectItem[];
  modules: SelectItem[];
}

@Injectable({
  providedIn: "root"
})
export class Surt01Service {

  constructor(private http: HttpClient) { }

  list = (keywords: string = ""): Observable<Program[]> => this.http.disableLoading().get<Program[]>("surt01/list", { params: { keywords } })

  detail = (programCode: string): Observable<Program> => this.http.get<Program>("surt01/detail", { params: { programCode } })

  master = () => this.http.get<Master>("surt01/master")

  copy = (data) => this.http.put("surt01/copy", data)

  save = (data) => this.http.post('surt01/update', data);

  delete = (programCode: string) => this.http.delete("surt01/delete", { params: { programCode } })
}
