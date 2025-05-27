import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SaveDataService } from '@app/core/services/save-data.service';
import { PageCriteria } from '@app/shared/components/datatable/page';
import { PaginatedDataSource } from '@app/shared/components/datatable/server-datasource';
import { SelectItem } from 'primeng/api/selectitem';
import { Qort01Service } from './qort01.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'x-qort01',
  templateUrl: './qort01.component.html',
  styleUrl: './qort01.component.scss'
})
export class Qort01Component implements OnInit, OnDestroy{

  searchForm:FormGroup;
  keyword='';
  standardPrices: any[] = []
  autocompleteItems: SelectItem[] = []
  autocompletePlCodeItems: SelectItem[] = []
  plTypes:SelectItem[];
  vatTypes:SelectItem[];
  data!:PaginatedDataSource<any,any>;
  displayedColumns: string[] = ['plTypeName','plCode','plName','currencyName','taxTypeName'];
  initialPageSort = new PageCriteria();

  constructor(
    private fb:FormBuilder,
    private save:SaveDataService,
    private qort01Sv: Qort01Service,
    private router: ActivatedRoute
  ){}

  ngOnInit(): void {
    this.router.data.subscribe((res:any)=>{
      console.log(res);
      this.plTypes = res.qort01.master.ddlplType;
      this.vatTypes = res.qort01.master.ddlTaxType;
      this.autocompleteItems = res.qort01.master.ddlCurrency;
      this.autocompletePlCodeItems = res.qort01.master.ddlplCode;
    });

    this.initialPageSort = this.save.retrive('qort01page') ?? this.initialPageSort;
    let data =  this.save.retrive('qort01') ?? '';
    this.searchForm = this.createSearchForm(data);
     this.data = new PaginatedDataSource<any,any>(
      (request, query) => this.qort01Sv.getSearch(request,query),
      this.initialPageSort)

    this.data.queryBy(this.getCurrentData());
    console.log(this.data);

  }

  createSearchForm(data){
    console.log(data);
    const fg = this.fb.group({
      keyword:null,
      plTypeCode:null,
      currencyCode:null,
      taxTypeCode:null,
      plId:null
    });
    fg.patchValue(data);
    return fg;
  }
  search() {
    console.log(this.data);

    this.data.queryBy(this.getCurrentData());
  }
  clear(){
    this.searchForm.reset({
       keyword:null,
      plType:null,
      currencyCode:null,
      vatType:null,
      plId:null
    });
    this.search();
  }
  ngOnDestroy(): void {
    this.save.save(this.getCurrentData(),'qort01');
    this.save.save(this.data.getPageInfo(),'qort01page');
  }


  getCurrentData(){
    return this.searchForm.value;
  }

}
