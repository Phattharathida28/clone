import { Guid } from "guid-typescript";
import { EntityBase } from "../entityBase";

export class teamManagement extends EntityBase {
    userId: number;
    firstnameTh: string;
    lastnameTh: string;
    positionNameEn: string;
    headUser: number;
}