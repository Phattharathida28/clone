import { Component, Input } from '@angular/core';
import { BaseFormField } from '../base-form';
import { SelectItem } from 'primeng/api';
import { SelectButtonChangeEvent } from 'primeng/selectbutton';

@Component({
  selector: 'selectbutton',
  template: `
    <p-selectButton 
    [class]="class"
    [ngClass]="hasErrors ? 'ng-invalid ng-dirty': ''"
    [options]="items" 
    [(ngModel)]="value" 
    optionLabel="label" 
    optionValue="value"
    [allowEmpty]="allowEmpty"
    [multiple]="multiple"
    [disabled]="disabled"
    optionDisabled="disabled"
    (onChange)="onOptionChange($event)">
  </p-selectButton>
  @if(hasErrors){
    <small class="text-red-600" *ngIf="control.hasError('required') || control.hasError('empty')">
        {{ 'message.STD00007' | translate }}
    </small>
  }
  `
})
export class SelectbuttonComponent extends BaseFormField {

  @Input() items: SelectItem[] = []
  @Input() allowEmpty: boolean = true;
  @Input() multiple: boolean = false;

  override writeValue(value: any): void {
    this.value = value
  }

  onOptionChange(event: SelectButtonChangeEvent) {
    this.onChange(event.value)
  }
}
