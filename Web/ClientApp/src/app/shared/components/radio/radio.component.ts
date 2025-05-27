import { Component, Input } from '@angular/core';
import { BaseFormField } from '../base-form';
import { SelectItem } from 'primeng/api';
import { Guid } from 'guid-typescript';
import { RadioButtonClickEvent } from 'primeng/radiobutton';

@Component({
  selector: 'radio',
  templateUrl: './radio.component.html'
})
export class RadioComponent extends BaseFormField {
  @Input() items: SelectItem[] = []
  @Input() vertical: boolean = false
  name: string = Guid.raw()

  override writeValue(obj: any): void {
    this.value = obj;
  }

  onClick(event: RadioButtonClickEvent) {
    this.onChange(event.value)
    this.value = event.value
  }
}