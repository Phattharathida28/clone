import { Component } from '@angular/core';
import { SelectItem } from 'primeng/api';

@Component({
  selector: 'x-floatlabel',
  templateUrl: './floatlabel.component.html'
})
export class FloatlabelComponent {
  autocompleteItems: string[] = []
  items: SelectItem[] = []

  constructor() {
    for (let index = 0; index < 3; index++) {
      this.autocompleteItems.push(`item ${index + 1}`)
      this.items.push({ value: index + 1, label: `item ${index + 1}` })
    }
  }
}
