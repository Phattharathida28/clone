import { FormGroup } from "@angular/forms";
import { RowState } from "@app/shared/types/data.types";
import { Guid } from "guid-typescript";

export class EntityBase {
    createdBy: string;
    createdDate?: Date
    createdProgram: string;
    updatedBy: string;
    updatedDate?: Date;
    updatedProgram: string;

    guid: string;
    rowState: RowState;
    rowVersion: string;
    form?: FormGroup;

    constructor() {
        this.guid = Guid.raw();
        this.rowState = RowState.Add;
    }
}