import { Component, Input } from '@angular/core';
import { BaseFormField } from '../base-form';

@Component({
  selector: 'number',
  templateUrl: './number.component.html'
})
export class NumberComponent extends BaseFormField {
  @Input() floatLabel: boolean = false;
  @Input() helpText: string = "";
  @Input() axis: "horizontal" | "vertical" = "vertical"
  @Input() min: number = 0;
  @Input() max: number = Infinity;
  @Input() minFractionDigits: number = 0;
  @Input() maxFractionDigits: number = 0;
  @Input() showButtons: boolean = false;
  @Input() helpTextClass: string = ''

  override writeValue(value: any): void {
    this.value = value
  }

  inputData(value: number) {
    if (value > this.max) value = this.max
    else if (value < this.min) value = this.min

    this.onChange(value);
    this.value = value;
  }
}
