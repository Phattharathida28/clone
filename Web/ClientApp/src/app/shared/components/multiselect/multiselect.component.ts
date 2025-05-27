import { Component, Input } from '@angular/core';
import { BaseFormField } from '../base-form';
import { SelectItem } from 'primeng/api';
import { MultiSelectSelectAllChangeEvent } from 'primeng/multiselect';

@Component({
  selector: 'multiselect',
  templateUrl: './multiselect.component.html'
})
export class MultiselectComponent extends BaseFormField {
  @Input() items: SelectItem[] = [];
  @Input() filter: boolean = true;
  @Input() showClear: boolean = true;
  @Input() floatLabel: boolean = false;
  @Input() helpText: string = "";
  @Input() axis: "horizontal" | "vertical" = "vertical"
  @Input() helpTextClass: string = ''

  get selectAll() {
    return this.items.length == (this.value ? this.value.length : 0) && this.filter
  }

  override writeValue(value: any): void {
    this.value = this.items?.filter(f => value?.includes(f.value));
  }

  onSelected(data: SelectItem[]) {
    data = data || []
    const value = data.map(m => m.value)
    this.onChange(value)
    this.value = this.items.filter(f => value.includes(f.value))
  }

  onSelectAllChange(event: MultiSelectSelectAllChangeEvent) {
    if (event.checked) {
      this.onChange(this.items.map(m => m.value))
      this.value = this.items
    }
    else {
      this.onChange([])
      this.value = []
    }
  }
}
