import { Component } from '@angular/core';

@Component({
  selector: 'x-table',
  templateUrl: './table.component.html',
  styles: ``
})
export class TableComponent {
  data: any[] = []

  constructor() {
    for (let index = 0; index < 100; index++) {
      this.data.push({
        column1: index + 1,
        column2: Math.random(),
        column3: Math.random(),
        column4: Math.random(),
        column5: Math.random(),
        column6: Math.random(),
      })
    }
  }
}
