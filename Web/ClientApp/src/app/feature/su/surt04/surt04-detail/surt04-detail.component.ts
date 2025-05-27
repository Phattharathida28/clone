import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MenuItem } from 'primeng/api';
import { Master, Surt04Service } from '../surt04.service';
import { User } from '@app/models/su/user';
import { ActivatedRoute } from '@angular/router';
import { UserProfile } from '@app/models/su/userProfile';
import { ModalService } from '@app/shared/components/modal/modal.service';
import { RowState } from '@app/shared/types/data.types';
import { switchMap, Observable, of } from 'rxjs';
import { NotifyService } from '@app/core/services/notify.service';

@Component({
  selector: 'x-surt04-detail',
  templateUrl: './surt04-detail.component.html',
  styles: ``
})
export class Surt04DetailComponent {
  breadcrumbItems: MenuItem[] = [
    { label: 'label.SURT04.ProgramName', routerLink: '/su/surt04' },
    { label: 'label.SURT04.Detail', routerLink: '/su/surt04/detail' },
  ]

  form: FormGroup;
  master: Master = new Master();
  user: User;
  deletes: any = [];

  constructor(
    private fb: FormBuilder,
    private ms: NotifyService,
    private md: ModalService,
    private route: ActivatedRoute,
    private sv: Surt04Service
  ) {
    this.createForm();
    this.route.data.subscribe(({ user, master }) => {
      this.user = user ?? new User()
      this.master = master ?? []
      this.rebuildForm()
    })
  }

  createForm() {
    this.form = this.fb.group({
      id: null,
      userName: [null, [Validators.required, Validators.maxLength(50), Validators.minLength(8)]],
      password: [null, [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&])/g), Validators.minLength(8), Validators.maxLength(20)]],
      passwordConfirm: [null, [Validators.required, this.validatePasswordConfirm.bind(this)]],
      firstname: [null, [Validators.required, Validators.maxLength(200)]],
      lastname: [null, [Validators.required, Validators.maxLength(200)]],
      firstnameTh: [null, [Validators.required, Validators.maxLength(200)]],
      lastnameTh: [null, [Validators.required, Validators.maxLength(200)]],
      email: [null, [Validators.required, Validators.email, Validators.maxLength(200)]],
      phoneNumber: [null, [Validators.required, Validators.pattern(/^\d{10}$/), Validators.maxLength(20)]],
      active: true,
    })
  }

  rebuildForm() {
    this.user["password"] = this.user.passwordHash || null;
    this.user["passwordConfirm"] = this.user.passwordHash || null;
    this.form.patchValue(this.user)

    if (this.user["id"]) {
      this.form.controls["userName"].disable();
      this.form.controls["password"].addValidators([Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&])/g), Validators.minLength(8), Validators.maxLength(20)]);
      this.form.controls["password"].updateValueAndValidity();
    }

    this.form.markAsPristine();
    this.form.markAsUntouched();
    this.user.userProfiles.map(m => m.form = this.createFormDetail(m))
  }

  createFormDetail(userProfile: UserProfile) {
    const fg: FormGroup = this.fb.group({
      profileCode: [null, Validators.required],
      rowState: null,
      rowVersion: null
    })

    fg.patchValue(userProfile);

    fg.valueChanges.subscribe((res) => {
      if (res) {
        if (fg.controls["rowState"].value == RowState.Normal) fg.controls["rowState"].setValue(RowState.Edit)
      }
    })

    return fg;
  }

  validatePasswordConfirm(control: AbstractControl): any {
    if (!control.parent) {
      return;
    }
    const password = control.parent.get('password').value;
    const passwordConfirm = control.value;
    if (password !== passwordConfirm) {
      return { passwordNotMatch: true };
    }
    return;
  }

  add() {
    let userProfile = new UserProfile()
    userProfile.rowState = RowState.Add;
    userProfile.form = this.createFormDetail(userProfile);
    this.user.userProfiles.push(userProfile);
    this.user.userProfiles = [...this.user.userProfiles];
  }

  remove(userProfile: UserProfile) {
    if (userProfile.form?.controls['rowState'].value != RowState.Add) {
      userProfile.form?.controls['rowState'].setValue(RowState.Delete);
    }

    this.deletes.push(userProfile)
    this.user.userProfiles = this.user.userProfiles.filter(f => f.guid != userProfile.guid);
  }

  validate(): boolean {
    if (this.form.controls["password"].value == this.user.passwordHash && this.user["id"]) {
      this.form.controls["password"].clearValidators();
      this.form.controls["password"].updateValueAndValidity();
    }
    else {
      this.form.controls["password"].addValidators([Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&])/g), Validators.minLength(8), Validators.maxLength(20)]);
      this.form.controls["password"].updateValueAndValidity();
    }
    if (this.form.invalid || this.user.userProfiles.some(s => s.form.invalid)) return true;

    return false;
  }

  save() {
    if (this.validate()) {
      this.ms.warning("message.STD00013");
      this.form.markAllAsTouched();
      this.user.userProfiles.forEach(f => f.form.markAllAsTouched())
    }
    else {
      const data = this.form.getRawValue();
      data["id"] = data["id"] ?? 0;
      data["passwordHash"] = data.password;
      data["userProfileLists"] = [...this.user.userProfiles.map(m => m.form.getRawValue()), ...this.deletes.map(m => m.form.getRawValue())]
      this.sv.save(data).pipe(
        switchMap((res: any) => this.sv.detail(res.userId))
      ).subscribe(res => {
        this.user = res;
        this.deletes = []
        this.rebuildForm();
        this.ms.success("message.STD00014");
      })
    }
  }

  canDeactivate(): Observable<boolean> {
    if (this.form.dirty || this.deletes.length > 0) return this.md.confirm("message.STD00010");
    return of(true);
  }
}
