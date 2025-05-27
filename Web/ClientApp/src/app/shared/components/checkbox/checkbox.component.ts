import { Component, Input } from '@angular/core';
import { BaseFormField } from '../base-form';
import { SelectItem } from 'primeng/api';
import { Guid } from 'guid-typescript';
import { CheckboxChangeEvent } from 'primeng/checkbox';

@Component({
  selector: 'checkbox',
  templateUrl: './checkbox.component.html'
})
export class CheckboxComponent extends BaseFormField {
  @Input() items: SelectItem[] = []
  @Input() vertical: boolean = false
  @Input() label: string = ''
  name: string = `checkbox${Guid.raw()}`

  override writeValue(obj: any): void {
    this.value = obj;
  }

  onCheckboxChange(event: CheckboxChangeEvent) {
    this.onChange(event.checked)
  }
}
