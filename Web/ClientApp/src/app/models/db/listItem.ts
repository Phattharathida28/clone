import { EntityBase } from "../entityBase";

export class ListItem extends EntityBase {
   listItemGroupCode : string;
   listItemCode : string;
   listItemNameTha : string;
   listItemNameEng : string;
   active : boolean;
   sequence : number;
}