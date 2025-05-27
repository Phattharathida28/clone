import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Message } from '@app/models/su/message';
import { Surt05Service } from '../surt05.service';
import { NotifyService } from '@app/core/services/notify.service';
import { ModalService } from '@app/shared/components/modal/modal.service';
import { MenuItem } from 'primeng/api';
import { RowState } from '@app/shared/types/data.types';
import { Observable, of, switchMap } from 'rxjs';

@Component({
  selector: 'x-surt05-detail',
  templateUrl: './surt05-detail.component.html',
  styles: ``
})
export class Surt05DetailComponent {
  form: FormGroup;
  messages: Message;
  parameter: { messageCode: string }
  breadcrumbItems: MenuItem[] = [
    { label: 'label.SURT05.ProgramName', routerLink: '/su/surt05' },
    { label: 'label.SURT05.Detail', routerLink: '/su/surt05/detail' },
  ]

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private ms: NotifyService,
    private sv: Surt05Service,
    private md: ModalService) {
    this.createForm()
    this.route.data.subscribe(({ message }) => {
      this.messages = message ?? new Message()
      this.form.patchValue(message)
      this.rebuildData()
    })
  }

  rebuildData() {
    this.form.patchValue(this.messages)
    if (this.messages.messageCode) {
      this.form.controls["messageCode"].disable();
      this.form.controls["rowState"].setValue(RowState.Normal);
    }
    else {
      this.form.controls["rowState"].setValue(RowState.Add);
      this.form.controls["active"].setValue(true);
    }

    this.form.valueChanges.subscribe(() => {
      if (this.form.controls['rowState'].value == RowState.Normal) this.form.controls['rowState'].setValue(RowState.Edit);
    })

    this.form.markAsPristine()
  }

  createForm() {
    this.form = this.fb.group({
      messageCode: [null, [Validators.required, Validators.maxLength(20)]],
      messageDesc: null,
      remark: null,
      messageCodeTh: [null, [Validators.required, Validators.pattern(/^[ก-๙0-9#$^+=!*(){}\[\]@%& ]+$/), Validators.maxLength(200)]],
      messageCodeEn: [null, [Validators.required, Validators.pattern(/^[a-zA-Z0-9#$^+=!*(){}\[\]@%& ]+$/), Validators.maxLength(200)]],
      rowState: null,
      rowVersion: null,
      active: true
    })
  }

  validate = () => this.form.invalid;

  save() {
    if (this.validate()) {
      this.ms.warning('message.STD00013');
      this.form.markAllAsTouched();
    }
    else {
      this.sv.save(this.form.getRawValue()).pipe(
        switchMap((res: any) => this.sv.detail(res.messageCode))
      ).subscribe((res) => {
        this.messages = res;
        this.rebuildData();
        this.ms.success("message.STD00014");
      })
    }
  }

  canDeactivate(): Observable<boolean> {
    if (this.form.dirty) return this.md.confirm("message.STD00010");
    return of(true);
  }
}
