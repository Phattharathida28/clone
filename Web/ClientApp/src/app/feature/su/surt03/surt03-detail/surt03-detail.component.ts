import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { Master, Surt03Service } from '../surt03.service';
import { Profile } from '@app/models/su/profile';
import { RowState } from '@app/shared/types/data.types';
import { NotifyService } from '@app/core/services/notify.service';
import { Observable, of, switchMap } from 'rxjs';
import { ModalService } from '@app/shared/components/modal/modal.service';

@Component({
  selector: 'x-surt03-detail',
  templateUrl: './surt03-detail.component.html'
})
export class Surt03DetailComponent {

  breadcrumbItems: MenuItem[] = [
    { label: 'label.SURT03.ProgramName', routerLink: '/su/surt03' },
    { label: 'label.SURT03.Detail', routerLink: '/su/surt03/detail' },
  ]

  form: FormGroup
  profile: Profile = new Profile()
  master: Master

  constructor(
    private route: ActivatedRoute,
    private sv: Surt03Service,
    private fb: FormBuilder,
    private ms: NotifyService,
    private md: ModalService
  ) {
    this.createForm()
    this.route.data.subscribe(({ profile, master }) => {
      this.profile = profile ?? new Profile()
      this.master = master ?? []
      this.profile.profileMenus.forEach(profileMenus => {
        this.master.menus = this.master.menus.filter(f => f.menuCode != profileMenus.menuCode)
        this.master.menus = [...this.master.menus]
      })

      this.rebuildForm()
    })
  }

  createForm() {
    this.form = this.fb.group({
      profileCode: [null, [Validators.required, Validators.maxLength(20)]],
      description: [null, [Validators.required, Validators.maxLength(200)]],
      active: true,
      rowState: null,
      rowVersion: null
    });


    this.form.valueChanges.subscribe((res) => {
      if (res) {
        if (this.form.controls['rowState'].value == RowState.Normal) this.form.controls['rowState'].setValue(RowState.Edit);
      }
    })
  }

  rebuildForm() {
    this.form.patchValue(this.profile)

    if (this.profile.profileCode) {
      this.form.controls["profileCode"].disable();
      this.form.controls["rowState"].setValue(RowState.Normal);
    }
    else this.form.controls["rowState"].setValue(RowState.Add);

    this.form.markAsPristine()
  }

  save() {
    if (this.form.invalid) {
      this.ms.warning('message.STD00013');
      this.form.markAllAsTouched();
    }
    else {
      const data = this.form.getRawValue();
      data["profileMenus"] = this.profile.profileMenus.map(m => {
        return {
          ...m,
          profileCode: data.profileCode,
        }
      })

      this.sv.save(data).pipe(
        switchMap((res: any) => this.sv.detail(res.profileCode))
      ).subscribe((res) => {
        this.profile = res;
        this.rebuildForm();
        this.ms.success("message.STD00014");
      })
    }
  }

  canDeactivate(): Observable<boolean> {
    if (this.form.dirty) return this.md.confirm("message.STD00010");
    return of(true);
  }
}
