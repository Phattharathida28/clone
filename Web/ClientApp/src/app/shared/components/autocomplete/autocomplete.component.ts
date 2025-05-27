import { Component, Input, OnInit } from '@angular/core';
import { BaseFormField } from '../base-form';
import { AutoCompleteCompleteEvent } from 'primeng/autocomplete';

@Component({
  selector: 'autocomplete',
  templateUrl: './autocomplete.component.html'
})
export class AutocompleteComponent extends BaseFormField implements OnInit {
  @Input() items: string[] = []
  @Input() floatLabel: boolean = false;
  @Input() helpText: string = "";
  @Input() axis: "horizontal" | "vertical" = "vertical"
  @Input() dropdown: boolean = false;
  @Input() helpTextClass: string = ''
  searchItem: string[] = []

  ngOnInit(): void {
    this.searchItem = this.items
  }

  override writeValue(obj: any): void {
    this.value = obj;
  }

  search(event: AutoCompleteCompleteEvent) {
    this.searchItem = this.items.filter(f => f.toLowerCase().includes(event.query.toLowerCase()))
    this.onChange(this.value)
  }

  onHide() {
    this.onChange(this.value)
  }
}
