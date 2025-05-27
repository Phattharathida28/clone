import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Parameter } from '@app/models/su/parameter';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Surt06Service {

  constructor(private http: HttpClient) { }

  list = (keywords: string = ''): Observable<Parameter[]> => this.http.disableLoading().get<Parameter[]>("surt06/list", { params: { keywords } });

  detail = (parameterGroupCode: string, parameterCode: string): Observable<Parameter> => this.http.get<Parameter>("surt06/detail", { params: { parameterGroupCode, parameterCode } })

  delete = (parameterGroupCode: string, parameterCode: string): Observable<Parameter[]> => this.http.delete<any>("surt06/delete", { params: { parameterGroupCode, parameterCode } });

  save = (data) => this.http.post('surt06/update', data);
}
