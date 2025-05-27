import { Component } from '@angular/core';
import { ListItemGroup } from '@app/models/db/listItemGroup';
import { Dbrt02Service, Master } from '../dbrt02.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MenuItem } from 'primeng/api';
import { ActivatedRoute } from '@angular/router';
import { RowState } from '@app/shared/types/data.types';
import { ListItem } from '@app/models/db/listItem';

@Component({
  selector: 'x-dbrt02-detail',
  templateUrl: './dbrt02-detail.component.html'
})
export class Dbrt02DetailComponent {
  listItemGroup: ListItemGroup = new ListItemGroup()
  listItem:ListItem[] = [];
  listItemDelete:ListItem[] = [];
  master: Master
  form: FormGroup;
  breadcrumbItems: MenuItem[] = [
    { label: 'label.DBRT02.ProgramName', routerLink: '/db/dbrt02' },
    { label: 'label.DBRT02.Detail', routerLink: '/db/dbrt02/detail' },
  ]

  constructor(private activatedRoute: ActivatedRoute , private fb : FormBuilder , private sv : Dbrt02Service) {
    this.createForm();
    this.activatedRoute.data.subscribe(({ listItemGroup, master }) => {
      this.listItemGroup = listItemGroup
      this.master = master
      this.form.patchValue(this.listItemGroup);
      if(this.form.get('listItemGroupCode').value) this.form.get('listItemGroupCode').disable();
      this.rebuildForm();
    })
  }

  createForm(): void {
    this.form = this.fb.group({
      listItemGroupCode: [null, [Validators.required, Validators.maxLength(100)]],
      listItemGroupName: null,
      rowState: null,
      rowVersion: null
    });

    this.form.valueChanges.subscribe(() => {
      if (this.form.controls["rowState"].value == RowState.Normal) this.form.controls["rowState"].setValue(RowState.Edit, { emitEvent: false });
      else if(this.form.get('rowVersion').value == null) this.form.get('rowState').setValue(RowState.Add , { emitEvent: false });
    })
  }

  createFormDetail(listItem : ListItem){
    const fg:FormGroup = this.fb.group({
      listItemGroupCode : [null],
      listItemCode : [null , [Validators.required]],
      listItemNameTha : [null],
      listItemNameEng : [null],
      active : [true]
    })

    fg.patchValue(listItem);

    return fg;
  }

  addDetailForm(){
    let data = new ListItem();
    data['form'] = this.createFormDetail(data);
    this.listItem.unshift(data);
  }

  removeDetailForm(index:number){
    const check = this.listItem[index].rowVersion;
    if(typeof check != undefined) this.listItemDelete.push(this.listItem[index]);
    this.listItem.splice(index , 1);
  }

  rebuildForm(){
    this.listItem.map(m => m['form'] = this.createFormDetail(m))
  }

  save(): void {
    let form = this.form.getRawValue();
    form['listItems'] = this.listItem.concat(this.listItemDelete).map(m => m.form.getRawValue());
    console.log(form , 'Form');
    // this.sv.save(form).subscribe((res:any) => {
    //   console.log(res);
    // })
  }

  // canDeactivate(): Observable<boolean> {
  //   if (this.form.dirty || this.master.langs.some((lang: Lang) => this.listItemGroupLang[lang.value].form.dirty)) return this.md.confirm("message.STD00010");
  //   return of(true);
  // }
}
