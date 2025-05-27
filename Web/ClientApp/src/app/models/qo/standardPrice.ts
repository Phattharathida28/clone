import { EntityBase } from "@app/core/services/base.service";

export class QoPriceMaster extends EntityBase{
  plId:number;
  plTypeCode:string;
  plTypeName:string;
  plCode:string;
  plNameTH:string;
  currencyCode:string;
  currencyName:string;
  taxTypeCode:string;
  taxTypeName:number;

}

export class DetailDTO extends EntityBase{
  plId:number;
  plTypeCode:string;
  plTypeName:string;
  plCode:string;
  plNameTH:string;
  plNameEN:string;
  currencyCode:string;
  currencyName:string;
  taxTypeCode:string;
  taxTypeName:number;
  remark:string;

  isAuto:boolean;
  detailItems:DetailGoodsServiceDTO[]
}


export class DetailGoodsServiceDTO extends EntityBase{
  plDetId:number;
  editType:string;
  editTypeName:string;
  itemId:number;
  itemName:string;
  unitId:number;
  unitCode:number;
  unitName:string;
  standardPrice:number;
  price:number;
}


// export class DropDownDTO{
//   value
// }
