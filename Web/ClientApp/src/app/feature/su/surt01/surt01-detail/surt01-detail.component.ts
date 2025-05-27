import { Component } from '@angular/core';
import { Master, Surt01Service } from '../surt01.service';
import { ActivatedRoute } from '@angular/router';
import { Program } from '@app/models/su/program';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RowState } from '@app/shared/types/data.types';
import { switchMap, Observable, of } from 'rxjs';
import { NotifyService } from '@app/core/services/notify.service';
import { MenuItem, SelectItem } from 'primeng/api';
import { ProgramLabel } from '@app/models/su/programLabel';
import { ModalService } from '@app/shared/components/modal/modal.service';

@Component({
  selector: 'x-surt01-detail',
  templateUrl: './surt01-detail.component.html'
})
export class Surt01DetailComponent {

  program: Program = new Program()
  master: Master;
  form: FormGroup;
  deletes: ProgramLabel[] = []
  breadcrumbItems: MenuItem[] = [
    { label: 'label.SURT01.ProgramManagement', routerLink: '/su/surt01' },
    { label: 'label.SURT01.Detail', routerLink: '/su/surt01/detail' },
  ]

  constructor(
    private sv: Surt01Service,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private ms: NotifyService,
    private md: ModalService) {
    this.activatedRoute.data.subscribe(({ program, master }) => {
      this.program = program ?? new Program()
      this.master = master
      this.createForm()
      this.rebuildForm()
    })
  }

  createForm() {
    this.form = this.fb.group({
      programCode: [null, [Validators.required, Validators.maxLength(50)]],
      programName: [null, [Validators.maxLength(200)]],
      programPath: [null, [Validators.maxLength(200)]],
      systemCode: "ccs",
      moduleCode: null,
      rowState: null,
      rowVersion: null
    });


    this.form.valueChanges.subscribe(() => {
      if (this.form.controls["rowState"].value == RowState.Normal) this.form.controls["rowState"].setValue(RowState.Edit, { emitEvent: false });
    })
  }

  createFormDetail(programLabel: ProgramLabel) {
    const fg: FormGroup = this.fb.group({
      programCode: null,
      fieldName: [null, Validators.required],
      labelName: [null, Validators.required],
      languageCode: null,
      systemCode: null,
      moduleCode: null,
      rowState: null,
      rowVersion: null
    })

    fg.patchValue(programLabel);

    if (programLabel.programCode) fg.controls["fieldName"].disable();

    fg.valueChanges.subscribe(() => {
      if (fg.controls["rowState"].value == RowState.Normal) fg.controls["rowState"].setValue(RowState.Edit, { emitEvent: false });
    })

    return fg;
  }

  rebuildForm() {
    this.deletes = [];
    this.form.patchValue(this.program)

    if (this.program.programCode) {
      this.form.controls["programCode"].disable()
      this.form.controls["programPath"].disable()
      this.form.controls["moduleCode"].disable()
      this.form.controls["rowState"].setValue(RowState.Normal)
    }
    else this.form.controls["rowState"].setValue(RowState.Add)

    this.program.programLabels.map(m => m.form = this.createFormDetail(m))
    this.master.lang.forEach((lang: SelectItem) => this.program[lang.value] = this.program.programLabels.filter((programLabel: ProgramLabel) => programLabel.languageCode.toLowerCase() == lang.value.toLowerCase()))

    this.form.markAsPristine();
    this.program.programLabels.forEach(f => f.form.markAsPristine())
  }

  add(languageCode: string) {
    if (this.master.lang.some(s => s.value == languageCode)) {
      let programLabel = new ProgramLabel();
      programLabel.languageCode = languageCode;
      programLabel.rowState = RowState.Add;
      programLabel.form = this.createFormDetail(programLabel);
      this.program.programLabels.push(programLabel);
      this.program[languageCode].unshift(programLabel);
      this.program[languageCode] = [...this.program[languageCode]];
    }
  }

  remove(ProgramLabel: ProgramLabel, languageCode: string) {
    if (ProgramLabel.form?.controls['rowState'].value != RowState.Add) {
      ProgramLabel.form?.controls['rowState'].setValue(RowState.Delete);
    }

    this.deletes.push(ProgramLabel)
    this.program.programLabels = this.program.programLabels.filter((programLabel: ProgramLabel) => programLabel.guid != ProgramLabel.guid);
    this.program[languageCode] = this.program.programLabels.filter(f => f.languageCode == languageCode);
  }

  copy(langCode: string) {
    if (this.form.dirty || this.program.programLabels.some(s => s.form.dirty) || this.deletes.length > 0 || this.program.programLabels.some(s => s.form.controls["rowState"].value == RowState.Add)) {
      this.ms.warning('message.STD00011')
    }
    else {
      const data = { langCode, programCode: this.form.controls['programCode'].value }
      this.sv.copy(data).pipe(
        switchMap((program: Program) => this.sv.detail(program.programCode))
      ).subscribe((program: Program) => {
        this.program = program
        this.rebuildForm()
        this.ms.success("message.STD00012")
      })
    }
  }

  validate = (): boolean => (this.form.invalid || this.program.programLabels.some(s => s.form.invalid))

  save() {
    if (this.validate()) {
      this.ms.warning("message.STD00013");
      this.form.markAllAsTouched();
      this.program.programLabels.forEach(f => f.form.markAllAsTouched())
    }
    else {
      const data = this.form.getRawValue();
      data["programLabels"] = [...this.program.programLabels.map(m => m.form.getRawValue()), ...this.deletes.map(m => m.form.getRawValue()).filter(f => f.rowState != RowState.Add)];
      data["programLabels"].filter(f => f.programCode == null).forEach(f => {
        f.programCode = this.form.controls["programCode"].value;
        f.systemCode = "ccs";
        f.moduleCode = this.form.controls["moduleCode"].value;
      })
      this.sv.save(data).pipe(
        switchMap((program: Program) => this.sv.detail(program.programCode))
      ).subscribe((res: Program) => {
        this.program = res
        this.rebuildForm()
        this.ms.success("message.STD00014")
      })
    }
  }

  canDeactivate(): Observable<boolean> {
    if (this.form.dirty || this.program.programLabels.some(s => s.form.dirty) || this.deletes.length > 0 || this.program.programLabels.some(s => s.form.controls["rowState"].value == RowState.Add)) return this.md.confirm("message.STD00010");
    return of(true);
  }
}
