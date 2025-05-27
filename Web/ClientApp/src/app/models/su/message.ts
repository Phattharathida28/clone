import { EntityBase } from "../entityBase";

export class Message extends EntityBase {
    messageCode: string;
    messageDesc: string;
    remark: string;
    languageCode : string
}
