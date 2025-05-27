import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Language } from '@app/models/db/language';
import { Lang } from '@app/shared/types/data.types';
import { Observable } from 'rxjs';

export type Master = {
  langs: Lang[];
}

@Injectable({
  providedIn: 'root'
})
export class Dbrt01Service {

  constructor(private http: HttpClient) { }

  list = (keywords: string = ""): Observable<Language[]> => this.http.get<Language[]>("dbrt01/list", { params: { keywords } })

  detail = (languageCode: string): Observable<Language> => this.http.get<Language>("dbrt01/detail", { params: { languageCode } })

  master = (): Observable<Master> => this.http.get<Master>("dbrt01/master")

  save = (Language: Language): Observable<Language> => this.http.post<Language>('dbrt01/save', Language);

  delete = (languageCode: string): Observable<void> => this.http.delete<void>("dbrt01/delete", { params: { languageCode } })
}