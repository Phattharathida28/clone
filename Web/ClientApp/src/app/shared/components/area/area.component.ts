import { Component, Input } from '@angular/core';
import { BaseFormField } from '../base-form';

@Component({
  selector: 'areabox',
  templateUrl: './area.component.html'
})
export class AreaComponent extends BaseFormField {
  @Input() rows = 4;
  @Input() floatLabel: boolean = false;
  @Input() helpText: string = "";
  @Input() axis: "horizontal" | "vertical" = "vertical"
  @Input() helpTextClass: string = ''

  override writeValue(obj: any): void {
    this.value = obj;
  }

  onTextChange($event: any) {
    const value = $event?.target?.value;
    this.onChange(value);
    this.value = value;
  }
}
