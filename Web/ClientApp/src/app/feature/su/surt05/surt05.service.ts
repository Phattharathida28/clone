import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Message } from '@app/models/su/message';
import { RowState } from '@app/shared/types/data.types';
import { Observable } from 'rxjs';



export class MessageDTO extends Message {
  messageCodeTh: string
  messageCodeEn: string
}


@Injectable({
  providedIn: 'root'
})
export class Surt05Service {

  constructor(private http: HttpClient) { }

  list = (keywords: string = ""): Observable<Message[]> => this.http.disableLoading().get<Message[]>("surt05/list", { params: { keywords } })

  detail = (messageCode: string) => this.http.get<Message>('surt05/detail', { params: { messageCode } })

  save = (data: MessageDTO) => {
    if (data.rowState == RowState.Add) return this.http.post("surt05/create", data);
    else return this.http.put("surt05/update", data);
  }

  delete = (messageCode: string) => this.http.delete("surt05/delete", { params: { messageCode } })

}