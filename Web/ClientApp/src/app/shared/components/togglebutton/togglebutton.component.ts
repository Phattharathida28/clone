import { Component, Input } from '@angular/core';
import { BaseFormField } from '../base-form';
import { ToggleButtonChangeEvent } from 'primeng/togglebutton';

@Component({
  selector: 'togglebutton',
  template: `
  <p-toggleButton 
  [(ngModel)]="value" 
  [onLabel]="onLabel" 
  [offLabel]="offLabel"
  [onIcon]="onIcon"
  [offIcon]="offIcon"
  [iconPos]="iconPos"
  [disabled]="disabled"
  (onChange)="onToggleButtonChange($event)">
  </p-toggleButton>
  @if(hasErrors){
    <small class="text-red-600" *ngIf="control.hasError('required') || control.hasError('empty')">
        {{ 'message.STD00007' | translate }}
    </small>
  }`
})
export class TogglebuttonComponent extends BaseFormField {

  @Input() onLabel: string = ''
  @Input() offLabel: string = ''
  @Input() onIcon: string = ''
  @Input() offIcon: string = ''
  @Input() iconPos: "left" | "right" = 'left'

  override writeValue(value: boolean): void {
    this.value = value
  }

  onToggleButtonChange(event: ToggleButtonChangeEvent) {
    this.onChange(event.checked)
  }
}
