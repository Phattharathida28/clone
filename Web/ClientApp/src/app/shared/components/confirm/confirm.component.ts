import { Component } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'x-confirm',
  template: `
    <p>{{ cf.data.message | translate }}</p>
    <br>
    <div class="flex flex-wrap justify-content-end gap-2">
        <button label="Cancel" class="p-button-outlined" (click)="close(false)"></button>
        <button label="Confirm" (click)="close(true)"></button>
    </div>`
})
export class ConfirmComponent {
  constructor(public cf: DynamicDialogConfig, private ref: DynamicDialogRef) { }

  close(res) {
    this.ref.close(res)
  }
}
