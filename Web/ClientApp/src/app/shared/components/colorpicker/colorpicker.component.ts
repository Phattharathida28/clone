import { Component, Input } from '@angular/core';
import { BaseFormField } from '../base-form';

@Component({
  selector: 'colorpicker',
  template: `
    <p-colorPicker 
    [(ngModel)]="value"
    [class]="class"
    [disabled]="disabled"
    [format]="format"
    [inline]="inline"
    (onChange)="onChangeColor(this.value)">
    </p-colorPicker>
    @if(hasErrors){
      <small class="text-red-600" *ngIf="control.hasError('required') || control.hasError('empty')">
          {{ 'message.STD00007' | translate }}
      </small>
    }`
})
export class ColorpickerComponent extends BaseFormField {

  @Input() inline: boolean = false
  @Input() format: "rgb" | "hex" | "hsb" = "hex"

  override writeValue(value: string): void {
    this.value = value
  }

  onChangeColor(value: string) {
    this.onChange(value)
  }
}
