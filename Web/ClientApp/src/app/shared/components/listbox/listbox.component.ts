import { Component, Input } from '@angular/core';
import { BaseFormField } from '../base-form';
import { SelectItem } from 'primeng/api';
import { ListboxChangeEvent } from 'primeng/listbox';

@Component({
  selector: 'listbox',
  template: `
    <p-listbox 
    [options]="items" 
    [(ngModel)]="value"
    [filter]="filter"
    optionLabel="label"
    [multiple]="multiple"
    [metaKeySelection]="metaKeySelection"
    [class]="class" 
    [ngClass]="hasErrors ? 'ng-invalid ng-dirty': ''" 
    [disabled]="disabled"
    (onChange)="onListboxChange($event)">
  </p-listbox>
  @if(hasErrors){
    <small class="text-red-600" *ngIf="control.hasError('required') || control.hasError('empty')">
        {{ 'message.STD00007' | translate }}
    </small>
  }
  `
})
export class ListboxComponent extends BaseFormField {
  @Input() items: SelectItem[] = [];
  @Input() filter: boolean = true;
  @Input() multiple: boolean = false;
  @Input() metaKeySelection: boolean = false;

  override writeValue(obj: any): void {
    this.value = Array.isArray(obj) ? this.items.filter(f => obj.includes(f.value)) : this.items.find(f => f.value == obj)
  }

  onListboxChange(event: ListboxChangeEvent) {
    const value = Array.isArray(event.value) ? event.value.map(m => m["value"]) : event.value["value"]
    this.onChange(value)
    this.value = event.value
  }
}

