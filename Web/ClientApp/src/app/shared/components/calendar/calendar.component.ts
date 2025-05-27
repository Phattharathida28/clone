import { Component, Input } from '@angular/core';
import { BaseFormField } from '../base-form';

@Component({
  selector: 'calendar',
  templateUrl: './calendar.component.html'
})
export class CalendarComponent extends BaseFormField {
  @Input() floatLabel: boolean = false;
  @Input() helpText: string = "";
  @Input() axis: "horizontal" | "vertical" = "vertical"
  @Input() showIcon: boolean = true;
  @Input() maxDate: Date = null
  @Input() minDate: Date = null
  @Input() readonlyInput: boolean = false
  @Input() helpTextClass: string = ''
  @Input() showClear: boolean = false;

  override writeValue(obj: any): void {
    this.value = obj as Date;
  }

  onCalendaChange(data: Date) {
    this.onChange(data)
    this.value = data
  }
}
