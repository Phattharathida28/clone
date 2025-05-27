import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { FormDatasource } from '@app/core/services/base.service';
import { FormUtilService } from '@app/core/services/form-util.service';
import { NotifyService } from '@app/core/services/notify.service';
import { DetailDTO, DetailGoodsServiceDTO, QoPriceMaster } from '@app/models/qo/standardPrice';
import { ModalService } from '@app/shared/components/modal/modal.service';
import { RowState } from '@app/shared/types/data.types';
import { Guid } from 'guid-typescript';
import { MenuItem, SelectItem } from 'primeng/api';
import { Qort01Service, ReportParam } from '../qort01.service';
import { finalize, Observable, of, switchMap } from 'rxjs';

@Component({
  selector: 'x-qort01-detail',
  templateUrl: './qort01-detail.component.html',
  styleUrl: './qort01-detail.component.scss'
})
export class Qort01DetailComponent implements OnInit{

  breadcrumbItems: MenuItem[] = [
    { label: 'label.QORT01.ProgramName', routerLink: '/qo/qort01' },
    { label: 'label.QORT01.Detail', routerLink: '/qo/qort01/detail' },
  ]

  //master data
  tabLists:SelectItem[]=[{value:'1', label:'รายการสินค้า/บริการ'}];
  autoCompletePlType: SelectItem[] = []
  autoCompleteCurrency: SelectItem[] = []
  autoCompleteProduct: SelectItem[] = []
  autoCompleteUnit: SelectItem[] = []
  autoCompleteMethodData: SelectItem[] = [];
  autoCompleteVatTypes:SelectItem[]= [];


  //qoPrice master data
  qoPriceMaster:DetailDTO = {} as DetailDTO;
  qoPriceMasterDataSource!: FormDatasource<QoPriceMaster>;

  //product data
  ingoodDataSource: MatTableDataSource<FormDatasource<any>> =  new MatTableDataSource<FormDatasource<any>>([]);
  ingoodsFormData: FormDatasource<any>[] = [];
  inGoodDataList:any= [{product:'1'}];


  displayedColumns: string[] = ['editType','product','unit', 'standardPrice', 'price', 'action'];

  constructor(
    private fb:FormBuilder,
    private router : ActivatedRoute,
    private cd: ChangeDetectorRef,
    private modalService: ModalService,
    public util: FormUtilService,
    private notify: NotifyService,
    private sv : Qort01Service,
  ){}

  productsFormGroup:FormGroup;
  ngOnInit(): void {

    this.router.data.subscribe((res:any)=>{
      this.autoCompletePlType = res.qort01.master.ddlplType;
      this.autoCompleteCurrency = res.qort01.master.ddlCurrency;
      this.autoCompleteProduct = res.qort01.master.ddlGoodsService;
      this.autoCompleteUnit = res.qort01.master.ddlUnit;
      this.autoCompleteMethodData = res.qort01.master.ddlEditType;
      this.autoCompleteVatTypes = res.qort01.master.ddlTaxType;
      this.qoPriceMaster = res.qort01.detail;
      this.rebuildForm();
    })

  }

  rebuildForm() {
    this.qoPriceMasterDataSource = new FormDatasource<DetailDTO>(this.qoPriceMaster, this.createQoPriceMasterForm());
    if(this.qoPriceMaster && this.qoPriceMaster.plId)
    {
      this.qoPriceMasterDataSource.form.get('isAuto').disable();
    }
    // this.productsFormGroup = this.createProductForm();
    this.ingoodsFormData = []
    this.qoPriceMaster.detailItems.map(good => {
      const goodDataSource = new FormDatasource<DetailGoodsServiceDTO>(good, this.createGoodsServiceForm(good));
      if(this.qoPriceMaster && this.qoPriceMaster.plId)
      {
        goodDataSource.form.get('itemId').disable();
        goodDataSource.form.get('unitId').disable();
        goodDataSource.form.get('standardPrice').disable();
      }
      this.ingoodsFormData.push(goodDataSource);
    })
    this.reload();
  }
  createQoPriceMasterForm(){
    const fg = this.fb.group({
      plTypeCode:[null,[Validators.required]],
      plCode:null,
      isAuto:[true],
      plNameTH:[null,[Validators.required]],
      plNameEN:[null,[Validators.required]],
      currencyCode:[null,[Validators.required]],
      taxTypeCode:[null,[Validators.required]],
      remark:null
    })
    return fg;
  }

  reload() {
    this.ingoodDataSource.data = this.ingoodsFormData.filter(o => !o.isDelete);
  }
  createGoodsServiceForm(item){
      const fg =   this.fb.group({
          id: [Guid.create().toString()],
          plDetId:[item.plDetId],
          editType: ['Unchanged'],
          itemId: [item.itemId, Validators.required],
          unitId: [item.unitId, Validators.required],
          standardPrice: [item.standardPrice ?? 0],
          price: [item.price ?? 0, Validators.required],
          itemName: [item.itemName],
        });

    fg.get('itemId').valueChanges.subscribe(res => {
      if(res){
        const master = this.autoCompleteProduct.filter(f => f.value == res);
        fg.get('itemName')?.setValue(master[0].label);
      }
    });

    fg.get('editType').valueChanges.subscribe(res=>{
      if(res.toLowerCase() == ("PriceChange").toLowerCase() || res.toLowerCase() == ("Added").toLowerCase() )
      {
        fg.get('price').enable();
      }
      else if(res.toLowerCase() == ("Unchanged").toLowerCase()){
        fg.get('price').disable();
      }
    });

    return fg;
  }

  changeEditType(row){
    console.log(row);

  }
  addItem(){
    let newItem = new DetailGoodsServiceDTO();
    newItem.editType = "Added";
    const goodDataSource = new FormDatasource<any>(newItem, this.createGoodsServiceForm(newItem));
    goodDataSource.form.get('editType').disable();
    goodDataSource.form.get('standardPrice').disable();
    this.ingoodsFormData.push(goodDataSource);
    this.reload();
  }

  searchProduct(keyword:string){
    this.ingoodDataSource.filter= keyword;
    this.ingoodDataSource.filterPredicate = function (data,filter) {
      let isFiltered = false;
      // if(data.form.get('itemId')?.value)
      // {
      //     isFiltered = String(data.form.get('itemId')?.value).toLowerCase()?.includes(filter);
      
      //   }
      console.log(data.form.get('itemName')?.value);
      console.log(filter);
      console.log(data.form.get('itemName')?.value.toString().includes(filter));
      if(data.form.get('itemName')?.value)
      {
         isFiltered = String(data.form.get('itemName')?.value).toLowerCase()?.includes(filter.toLowerCase());

      }
      return isFiltered;
    }
  }

  deleteItem(row){
    this.modalService.confirm("คุณต้องการลบข้อมุล หรือ ไม่ ?").subscribe((res) => {
      if(res)
      {
        // this.ingoodsFormData = this.ingoodsFormData.map(item=>{
         if(row.form.get('id').value == row.form.get('id').value && !row.form.get('plDetId').value)
         {
           row.markForDelete();
           console.log(row);
         }
         else{
          this.sv.deleteProductItem(row.form.get('plDetId').value).pipe(
            switchMap((res:any) =>{
              return this.sv.getDetail(res);
            })
          ).subscribe(res=>{
            this.notify.success('ลบสำเร็จ');
            this.qoPriceMaster = res;
            this.rebuildForm();
          })
         }
        //  return item;
        // });
        this.reload();
      }
    });
  }


  save(){
    let invalid = false;
    this.util.markFormGroupTouched(this.qoPriceMasterDataSource.form);
    if(this.qoPriceMasterDataSource.form.invalid)
    {
      invalid = true;
    }
    if(this.ingoodsFormData.some(item=> item.form.invalid))
    {
      this.ingoodsFormData.map(source => this.util.markFormGroupTouched(source.form));
      invalid = true;
    }
    if(invalid){
      this.notify.warning("กรุณากรอกข้อมูลให้ครบถ้วน");
      return;
    }

    this.qoPriceMasterDataSource.updateValue();
    this.ingoodsFormData.forEach((dataSource)=>{
      dataSource.updateValue();
    });

    const detailItems = this.ingoodsFormData.filter(item=> !item.isNormal).map(source=> source.model);
    this.qoPriceMaster.detailItems = detailItems;


    if(this.qoPriceMaster.plId){
        this.sv.update(this.qoPriceMaster).pipe(
          switchMap((res:any)=>{
            this.notify.success("บันทึกข้อมูลเรียบร้อย");
            return this.sv.getDetail(res);
          })
        ).subscribe((res:any)=>{
          this.qoPriceMaster = res;
          this.rebuildForm();
        });
    }
    else{
      this.sv.save(this.qoPriceMaster).pipe(
          switchMap((res:any)=>{
            this.notify.success("บันทึกข้อมูลเรียบร้อย");
            return this.sv.getDetail(res);
          })
        ).subscribe((res:any)=>{
          this.qoPriceMaster = res;
          this.rebuildForm();
        });
    }

  }

  canDeactivate(): Observable<boolean> {
    if (this.qoPriceMasterDataSource.form.dirty || this.ingoodsFormData.some(source=>source.form.dirty)) {
      return this.modalService.confirm("คุณต้องการออกจากหน้านี้หรือไม่?");
    }
    return of(true);
  }


  printReport(exportType){
    console.log("เข้าละ pim");
    console.log(exportType);
    console.log(this.qoPriceMasterDataSource);

    let reportParam = {} as ReportParam;
    let objParam = {
      pi_ou_code:"1001",
      pi_lin_id:'TH',
      pi_program_id:"QORP03",
      //pi_pl_code:"PL432432",
      pi_pl_code:this.qoPriceMasterDataSource.form.value.plCode,
    };

    reportParam.paramsJson = objParam;
    reportParam.fileName = "QORP03";
    reportParam.reportName = "QORP03";
    reportParam.exportType = exportType;

    this.sv.printReport(reportParam).pipe(
      finalize(() => {
        console.log("โหลดเสร็จ");
      })
    ).subscribe(
      (res: Blob) => {
        const blob = new Blob([res], { type: 'application/pdf' }); // เปลี่ยน type ตามจริง
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        if(reportParam.exportType == "PDF"){
          a.download = reportParam.reportName + '.pdf'; // เปลี่ยนเป็น .xlsx ถ้าต้องการ
        }else {
          a.download = reportParam.reportName + '.xlsx'; // เปลี่ยนเป็น .xlsx ถ้าต้องการ
        }
        a.click();
        URL.revokeObjectURL(url); // cleanup
      },
      (err) => {
        console.error("เกิด error", err);
      }
    );

  }


  downloadExcel(){

  }

}

