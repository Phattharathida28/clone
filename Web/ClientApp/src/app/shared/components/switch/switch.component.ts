import { Component } from '@angular/core';
import { BaseFormField } from '../base-form';
import { InputSwitchChangeEvent } from 'primeng/inputswitch';

@Component({
  selector: 'switch',
  template: `
  <p-inputSwitch 
    [class]="class" 
    [ngClass]="hasErrors ? 'ng-invalid ng-dirty': ''" 
    [(ngModel)]="value" 
    [disabled]="disabled" 
    (onChange)="onSwitchChange($event)">
  </p-inputSwitch>
  @if(hasErrors){
    <small class="text-red-600" *ngIf="control.hasError('required') || control.hasError('empty')">
        {{ 'message.STD00007' | translate }}
    </small>
  }`
})
export class SwitchComponent extends BaseFormField {

  override writeValue(obj: any): void {
    this.value = obj;
  }

  onSwitchChange(event: InputSwitchChangeEvent) {
    this.onChange(event.checked)
    this.value = event.checked
  }
}