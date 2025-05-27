import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DetailDTO } from '@app/models/qo/standardPrice';
import { environment } from '@env/environment';

export interface ReportParam {
  paramsJson: any;
  module: string;
  reportName: string;
  exportType: string;
  fileName: string;
  autoLoadLabel?: string;
}

@Injectable({
  providedIn: 'root'
})
export class Qort01Service {
    private reportUrl = `${environment.reportUrl}`;
    constructor(private http:HttpClient) { }

    getSearch(page: any,query: any){
    const filter = Object.assign(query, page);
    console.log(filter)
    return this.http.disableLoading().post<any>('qort01/List', filter);
  
  }
  getMaster() {
    return this.http.get<any>('qort01/master');
  }

  getDetail(code:string){
    return this.http.get<DetailDTO>('qort01/detail', {params:{plId:code}});
  }
  
  save(qoPriceMaster:DetailDTO){
    return this.http.post<any>('qort01/save-detail', qoPriceMaster);
  }

  update(qoPriceMaster:DetailDTO){
    return this.http.put<any>('qort01/update', qoPriceMaster);
  }
  
  deleteProductItem(plDetId:number){
    return this.http.delete<any>('qort01/delete', {params: {plDetId:plDetId}});
  }
  printReport(model: ReportParam) {
    console.log("ยิง");
    console.log(this.reportUrl + "/exportReport" + model);
    return this.http.post('http://localhost:8080/report/exportReport', model, { responseType: 'blob' });
  }
}
