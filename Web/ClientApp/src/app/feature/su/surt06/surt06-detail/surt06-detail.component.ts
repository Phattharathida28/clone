import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NotifyService } from '@app/core/services/notify.service';
import { Parameter } from '@app/models/su/parameter';
import { ModalService } from '@app/shared/components/modal/modal.service';
import { RowState } from '@app/shared/types/data.types';
import { MenuItem } from 'primeng/api';
import { Surt06Service } from '../surt06.service';
import { Observable, of, switchMap } from 'rxjs';

@Component({
  selector: 'x-surt06-detail',
  templateUrl: './surt06-detail.component.html'
})
export class Surt06DetailComponent {

  form: FormGroup;
  data: Parameter;
  breadcrumbItems: MenuItem[] = [
    { label: 'label.SURT06.ParameterManagement', routerLink: '/su/surt06' },
    { label: 'label.SURT06.Detail', routerLink: '/su/surt06/detail' },
  ]

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private ms: NotifyService,
    private md: ModalService,
    private sv: Surt06Service) {

    this.createForm();
    this.route.data.subscribe(({ detail }) => {
      if (detail) {
        this.form.patchValue(detail)
        this.data = detail
      }
      this.rebuildForm()
    })

  }

  createForm() {
    this.form = this.fb.group({
      parameterGroupCode: [null, Validators.required],
      parameterCode: [null, Validators.required],
      parameterValue: null,
      remark: null,
      rowState: null,
      rowVersion: null
    })
  }

  rebuildForm() {
    if (this.form.get('rowVersion').value) {
      this.form.controls["parameterGroupCode"].disable()
      this.form.controls["parameterCode"].disable()
      this.form.controls["rowState"].setValue(RowState.Edit)
    }
    else this.form.controls["rowState"].setValue(RowState.Add)
    this.form.markAsPristine();
  }

  validate = (): boolean => (this.form.invalid)

  save() {
    if (this.validate()) {
      this.ms.warning("message.STD00013");
      this.form.markAllAsTouched();
    }
    else {
      const data = this.form.getRawValue();
      this.sv.save(data).pipe(
        switchMap((parameter: Parameter) => this.sv.detail(parameter.parameterGroupCode, parameter.parameterCode))
      ).subscribe((res: Parameter) => {
        this.form.patchValue(res)
        this.rebuildForm()
        this.ms.success("message.STD00014")
      })
    }
  }

  canDeactivate(): Observable<boolean> {
    if (this.form.dirty) return this.md.confirm("message.STD00010");
    return of(true);
  }
}
