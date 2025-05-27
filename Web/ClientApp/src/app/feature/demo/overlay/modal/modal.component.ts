import { Component } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'x-modal',
  template: `
    <p>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore
        et dolore magna aliqua. Ut enim ad minim veniam,
        quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure
        dolor in reprehenderit
        in voluptate velit esse cillum dolore eu fugiat nulla pariatur.
        Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id
        est laborum.
        <br>
        <b>Initial data : </b>
        <br>
        {{ cf.data | json }}
    </p>

    <button (click)="close()" label="Close"></button>
  `
})
export class ModalComponent {

  constructor(public cf: DynamicDialogConfig, private ref: DynamicDialogRef) { }

  close() {
    this.ref.close("Data on Close")
  }
}


@Component({
  selector: 'x-modal-resolve',
  template: `
    <p>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore
        et dolore magna aliqua. Ut enim ad minim veniam,
        quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure
        dolor in reprehenderit
        in voluptate velit esse cillum dolore eu fugiat nulla pariatur.
        Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id
        est laborum.
        <br>
        <b>Initial data : </b>
        <br>
        {{ data.param | json }}
        <br>
        <b>Resolve Data : </b>
        <br>
        {{ data.resolved | json }}
        <br>
    </p>

    <button (click)="close()" label="Close"></button>
  `
})
export class ModalResolveComponent {
  data = this.cf.data
  constructor(public cf: DynamicDialogConfig, private ref: DynamicDialogRef) { }

  close() {
    this.ref.close("Data on Close")
  }
}
