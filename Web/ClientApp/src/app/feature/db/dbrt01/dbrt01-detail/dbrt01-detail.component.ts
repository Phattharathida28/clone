import { Component } from '@angular/core';
import { Language } from '@app/models/db/language';
import { Dbrt01Service, Master } from '../dbrt01.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LanguageLang } from '@app/models/db/languageLang';
import { MenuItem } from 'primeng/api';
import { ActivatedRoute } from '@angular/router';
import { NotifyService } from '@app/core/services/notify.service';
import { ModalService } from '@app/shared/components/modal/modal.service';
import { Lang, RowState } from '@app/shared/types/data.types';
import { Observable, mergeMap, of, zip } from 'rxjs';

@Component({
  selector: 'x-dbrt01-detail',
  templateUrl: './dbrt01-detail.component.html'
})
export class Dbrt01DetailComponent {
  language: Language = new Language()
  languageLang: { [langCode: string]: LanguageLang } = {}
  master: Master
  form: FormGroup;
  breadcrumbItems: MenuItem[] = [
    { label: 'label.DBRT01.ProgramName', routerLink: '/db/dbrt01' },
    { label: 'label.DBRT01.Detail', routerLink: '/db/dbrt01/detail' },
  ]

  constructor(
    private sv: Dbrt01Service,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private ms: NotifyService,
    private md: ModalService) {

    this.activatedRoute.data.subscribe(({ language, master }) => {
      this.language = language
      this.master = master

      this.createForm()
      this.rebuildForm()
    })
  }

  createForm(): void {
    this.form = this.fb.group({
      languageCode: [null, [Validators.required, Validators.maxLength(20)]],
      description: null,
      pattern: null,
      active: true,
      rowState: null,
      rowVersion: null
    });

    this.form.valueChanges.subscribe(() => {
      if (this.form.controls["rowState"].value == RowState.Normal) this.form.controls["rowState"].setValue(RowState.Edit, { emitEvent: false });
    })
  }

  createFormDetail(languageLang: LanguageLang, pattern: string): FormGroup {
    const fg: FormGroup = this.fb.group({
      languageCode: null,
      languageCodeForname: null,
      languageName: [null, [Validators.maxLength(200), Validators.pattern(pattern)]],
      rowState: null,
      rowVersion: null
    })

    fg.patchValue(languageLang);

    fg.valueChanges.subscribe((res) => {
      if (fg.controls["rowState"].value == RowState.Normal) fg.controls["rowState"].setValue(RowState.Edit, { emitEvent: false });
    })

    return fg;
  }

  rebuildForm(): void {
    this.form.patchValue(this.language)
    this.master.langs.forEach((lang: Lang) => {
      const languageLangByLang: LanguageLang = this.language.languageLangs.find((languageLang: LanguageLang) => languageLang.languageCodeForname == lang.value) ?? new LanguageLang(this.language.languageCode, lang.value)
      this.languageLang[lang.value] = languageLangByLang
      this.languageLang[lang.value].form = this.createFormDetail(languageLangByLang, lang.pattern)
    })

    if (this.language.languageCode) {
      this.form.controls["languageCode"].disable()
      this.form.controls["rowState"].setValue(RowState.Normal)
    }
    else {
      this.form.controls["rowState"].setValue(RowState.Add)
      this.master.langs.forEach((lang: Lang) => this.languageLang[lang.value].form.controls["rowState"].setValue(RowState.Add))
    }

    this.form.markAsPristine();
    this.master.langs.forEach((lang: Lang) => this.languageLang[lang.value].form.markAsPristine())
  }

  save(): void {
    if (this.form.invalid || this.master.langs.some((lang: Lang) => this.languageLang[lang.value].form.invalid)) {
      this.ms.warning("message.STD00013");
      this.form.markAllAsTouched();
      this.master.langs.forEach((lang: Lang) => this.languageLang[lang.value].form.markAllAsTouched())
    }
    else {
      const data: Language = this.form.getRawValue();
      data.languageLangs = this.master.langs.map((lang: Lang) => this.languageLang[lang.value].form.getRawValue())
      this.sv.save(data).pipe(
        mergeMap(() => zip([this.sv.detail(this.form.controls["languageCode"].getRawValue()), this.sv.master()]))
      ).subscribe((res: [Language, Master]) => {
        this.language = res[0]
        this.master = res[1]

        this.rebuildForm()
        this.ms.success("message.STD00014")
      })
    }
  }

  canDeactivate(): Observable<boolean> {
    if (this.form.dirty || this.master.langs.some((lang: Lang) => this.languageLang[lang.value].form.dirty)) return this.md.confirm("message.STD00010");
    return of(true);
  }
}
