import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { BaseFormField } from '../base-form';

@Component({
  selector: 'inputgroup',
  template: `
  <div class="flex gap-3">
    <p-inputGroup>
      <ng-content select=".before-input"></ng-content>
      @if(!floatLabel){
        <input 
        pInputText 
        [value]="value" 
        [class]="class" 
        [placeholder]="placeholder"
        [ngClass]="hasErrors ? 'ng-invalid ng-dirty': ''" 
        [readonly]="readonly"
        [disabled]="disabled" 
        (blur)="onTouched()"
        (input)="onTextChange($event)"/>
      }
      @else {
        <span class="p-float-label">
          <input 
          pInputText 
          [value]="value" 
          [class]="class" 
          [placeholder]="placeholder"
          [ngClass]="hasErrors ? 'ng-invalid ng-dirty': ''" 
          [readonly]="readonly"
          [disabled]="disabled" 
          (blur)="onTouched()"
          (input)="onTextChange($event)"/>
          <label><ng-content></ng-content></label>
        </span>
      }
      
      <ng-content select=".after-input"></ng-content>
    </p-inputGroup>
  </div>

  @if(hasErrors){
    <small class="text-red-600" *ngIf="control.hasError('required') || control.hasError('empty')">
        {{ 'message.STD00007' | translate }}
    </small>
  }
`
})
export class InputgroupComponent extends BaseFormField implements OnChanges {

  @Input() floatLabel: boolean = false

  ngOnChanges(changes: SimpleChanges) {
    if (changes["value"]) {
      this.value = this.value ?? '';
    }
  }

  override writeValue(value: any): void {
    this.value = value
  }

  onTextChange($event: any) {
    const value = $event?.target?.value;
    this.onChange(value);
    if (this.required) {
      const notEmpty = new RegExp(/\S+/);
      if (notEmpty.test(value)) {
        this.removeErrors(['empty'], this.control!);
        this.control?.updateValueAndValidity();
      }
      else {
        this.addErrors({ empty: true }, this.control!);
      }
    }

    this.value = value;
  }
}
