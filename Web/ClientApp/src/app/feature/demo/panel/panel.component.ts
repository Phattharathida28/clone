import { Component } from '@angular/core';
import { SelectItem } from 'primeng/api';

@Component({
  selector: 'x-panel',
  templateUrl: './panel.component.html'
})
export class PanelComponent {

  items: SelectItem[] = [];

  constructor() {
    for (let index = 0; index < 3; index++) this.items.push({ value: index + 1, label: `Header ${index + 1}` })
  }

}

