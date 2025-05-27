import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Content } from '@app/models/su/content';
import { Observable } from 'rxjs';

export interface ContentDTO extends Content {
  url: string
}

@Injectable({
  providedIn: 'root'
})
export class Surt07Service {

  constructor(private http: HttpClient) { }

  list = (keywords: string = ''): Observable<ContentDTO[]> => this.http.disableLoading().get<ContentDTO[]>("surt07/list", { params: { keywords } });

}
