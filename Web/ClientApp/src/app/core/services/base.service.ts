import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { RowState } from '@app/shared/components/datatable/state.enum';
import { Guid } from 'guid-typescript';


export class FormDatasource<M extends EntityBase>{
    id!: string;
    model: M;
    form:FormGroup;
    formName:{ [key: string]: FormGroup};
    private formGroup?: { [key: string]: FormGroup };
    constructor(model: M, form?: FormGroup) {
        this.id = Guid.raw();
        this.model = model;
        if (!model.guid) {
            this.model.guid = Guid.raw();
        }
        if (model.rowState == null || model.rowState == undefined)
            this.model.rowState = RowState.Add;
        this.formGroup = {};
        if (form) {
            this.createForm(form, this.id);
        }
    }

    createForm(form: FormGroup, name: string = this.id) {
        form.valueChanges.subscribe(() => {
            if (!form.pristine && this.model.rowState === RowState.Normal) {
                this.model.rowState = RowState.Edit;
            }
        })
        form.patchValue(this.model);
        this.formGroup[name] = form;
        if(name === this.id) this.form = this.formGroup[name];
        else {
            Object.assign(this.formName,{ name:this.formGroup[name]});
        }
    }

    patchValue(name:string=this.id){
        this.formGroup[name]?.patchValue(this.model);
    }

    updateValue() {
        Object.values(this.formGroup).forEach((form: FormGroup) => {
            Object.assign(this.model, form.getRawValue());
        })
    }

    get isAdd() {
        return this.model.rowState === RowState.Add;
    }

    get isDelete() {
        return this.model.rowState === RowState.Delete;
    }

    get isNormal() {
        return this.model.rowState === RowState.Normal;
    }

    get isEdit() {
        return this.model.rowState === RowState.Edit;
    }

    markToNormal() {
        this.model.rowState = RowState.Normal;
    }

    markForDelete() {
        this.model.rowState = RowState.Delete;
    }

    markToEdit(){
        this.model.rowState = RowState.Edit;
    }

}

export class EntityBase {
    guid: string;
    rowState: RowState;
    rowVersion: string;
    constructor() {
        this.guid = Guid.raw();
        this.rowState = RowState.Add;
    }
    get isEdit() {
        return this.rowState === RowState.Edit;
    }
    markToEdit(){
        this.rowState = RowState.Edit;
    }
}

export class BaseList {
    guid: string;
    rowState: RowState;
    constructor() {

        this.guid = Guid.raw();
        this.rowState = RowState.Add;
    }
}
