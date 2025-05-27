import { Component, Input } from '@angular/core';
import { BaseFormField } from '../base-form';
import { SelectItem } from 'primeng/api';

@Component({
  selector: 'dropdown',
  templateUrl: './dropdown.component.html'
})
export class DropdownComponent extends BaseFormField {
  @Input() items: SelectItem[] = [];
  @Input() label: string;
  @Input() filter: boolean = true;
  @Input() showClear: boolean = true;
  @Input() floatLabel: boolean = false;
  @Input() helpText: string = "";
  @Input() axis: "horizontal" | "vertical" = "vertical"
  @Input() helpTextClass: string = ''

  override writeValue(value: any): void {
    this.value = this.items?.find(f => f.value == value);
  }

  onSelected(obj: any) {
    this.value = this.items.find(f => f.value == obj?.value);
    this.onChange(this.value?.["value"] ?? null)
  }
}
