import { Component, Input } from '@angular/core';
import { BaseFormField } from '../base-form';

@Component({
  selector: 'chips',
  templateUrl: './chips.component.html'
})
export class ChipsComponent extends BaseFormField {
  @Input() floatLabel: boolean = false;
  @Input() helpText: string = "";
  @Input() axis: "horizontal" | "vertical" = "vertical"
  @Input() showClear: boolean = false
  @Input() allowDuplicate: boolean = true
  @Input() helpTextClass: string = ''

  override writeValue(obj: any): void {
    this.value = obj;
  }

  onDataChange(value: any) {
    this.onChange(value)
  }
}