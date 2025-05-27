import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Master, Surt02Service } from '../surt02.service';
import { NotifyService } from '@app/core/services/notify.service';
import { ModalService } from '@app/shared/components/modal/modal.service';
import { Lang, RowState } from '@app/shared/types/data.types';
import { Observable, mergeMap, of, switchMap, zip } from 'rxjs';
import { MenuItem } from 'primeng/api';
import { MenuLabel } from '@app/models/su/menuLabel';
import { Menu } from '@app/models/su/menu';

@Component({
  selector: 'x-surt02-detail',
  templateUrl: './surt02-detail.component.html',
  styles: ``
})
export class Surt02DetailComponent {

  form: FormGroup;
  parameter: { menuCode: string }
  master: Master;
  menu: Menu = new Menu;
  menuLabel: { [menuCode: string]: MenuLabel } = {}
  breadcrumbItems: MenuItem[] = [
    { label: 'label.SURT02.ProgramName', routerLink: '/su/surt02' },
    { label: 'label.SURT02.Detail', routerLink: '/su/surt02/detail' },
  ]

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private ms: NotifyService,
    private sv: Surt02Service,
    private md: ModalService) {
    this.route.data.subscribe(({ menu, master }) => {
      this.menu = menu
      this.master = master

      this.createForm()
      this.rebuildData()
    })
  }

  createForm(): void {
    this.form = this.fb.group({
      menuCode: [null, [Validators.required, Validators.maxLength(20)]],
      mainMenu: null,
      programCode: null,
      icon: [null, [Validators.maxLength(20)]],
      active: [null],
      rowState: null,
      rowVersion: null
    });

    this.form.valueChanges.subscribe(() => {
      if (this.form.controls["rowState"].value == RowState.Normal) this.form.controls["rowState"].setValue(RowState.Edit, { emitEvent: false });
    })
  }

  createFormDetail(menuLabel: MenuLabel, pattern: string): FormGroup {
    const fg: FormGroup = this.fb.group({
      languageCode: null,
      menuCode: null,
      menuName: [null, [Validators.pattern(pattern)]],
      rowState: null,
      rowVersion: null
    })

    fg.patchValue(menuLabel);

    fg.valueChanges.subscribe((res) => {
      if (fg.controls["rowState"].value == RowState.Normal) fg.controls["rowState"].setValue(RowState.Edit, { emitEvent: false });
    })

    return fg;
  }

  rebuildData(): void {
    this.form.patchValue(this.menu)
    this.master.langs.forEach((lang: Lang) => {
      const menuLabelByLang: MenuLabel = this.menu.menuLabels.find((menuLabel: MenuLabel) => menuLabel.languageCode == lang.value) ?? new MenuLabel()
      this.menuLabel[lang.value] = menuLabelByLang
      this.menuLabel[lang.value].form = this.createFormDetail(menuLabelByLang, lang.pattern)
    })

    if (this.menu.menuCode) {
      this.form.controls["menuCode"].disable();
      this.form.controls["rowState"].setValue(RowState.Normal);
    }
    else {
      this.form.controls["rowState"].setValue(RowState.Add);
      this.master.langs.forEach((lang: Lang) => this.menuLabel[lang.value].form.controls["rowState"].setValue(RowState.Add))
    }

    this.form.valueChanges.subscribe(() => {
      if (this.form.controls['rowState'].value == RowState.Normal) this.form.controls['rowState'].setValue(RowState.Edit);
    })

    this.form.markAsPristine()
    this.master.langs.forEach((lang: Lang) => this.menuLabel[lang.value].form.markAsPristine())
  }

  save(): void {
    if (this.form.invalid || this.master.langs.some((lang: Lang) => this.menuLabel[lang.value].form.invalid)) {
      this.ms.warning('message.STD00013');
      this.form.markAllAsTouched();
      this.master.langs.forEach((lang: Lang) => this.menuLabel[lang.value].form.markAllAsTouched())
    }
    else {
      const data: Menu = this.form.getRawValue();
      data.menuLabels = this.master.langs.map((lang: Lang) => {
        this.menuLabel[lang.value].form.get('languageCode').setValue(lang.value);
        return this.menuLabel[lang.value].form.getRawValue()
      })
      console.log(data.menuLabels)
      
      this.sv.save(data).pipe(
        mergeMap(() => zip([this.sv.detail(this.form.controls["menuCode"].getRawValue()), this.sv.master()]))
      ).subscribe((res: [Menu, Master]) => {
        this.menu = res[0]
        this.master = res[1]

        this.rebuildData()
        this.ms.success("message.STD00014")
      })
    }
  }

  canDeactivate(): Observable<boolean> {
    if (this.form.dirty) return this.md.confirm("message.STD00010");
    return of(true);
  }
}
