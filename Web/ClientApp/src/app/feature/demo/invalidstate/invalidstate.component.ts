import { AfterViewInit, Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SelectItem } from 'primeng/api';

@Component({
  selector: 'x-invalidstate',
  templateUrl: './invalidstate.component.html'
})
export class InvalidstateComponent implements AfterViewInit {
  autocompleteItems: string[] = []
  items: SelectItem[] = []
  form: FormGroup = new FormGroup({
    InputText: new FormControl(null, Validators.required),
    AutoComplete: new FormControl(null, Validators.required),
    Calendar: new FormControl(null, Validators.required),
    Chips: new FormControl(null, Validators.required),
    Password: new FormControl(null, Validators.required),
    InputMask: new FormControl(null, Validators.required),
    InputNumber: new FormControl(null, Validators.required),
    Dropdown: new FormControl(null, Validators.required),
    MultiSelect: new FormControl(null, Validators.required),
    Textarea: new FormControl(null, Validators.required),
  })
  constructor() {
    for (let index = 0; index < 3; index++) {
      this.autocompleteItems.push(`item ${index + 1}`)
      this.items.push({ value: index + 1, label: `item ${index + 1}` })
    }
  }

  ngAfterViewInit(): void {
    setTimeout(() => {
      this.form.markAllAsTouched()
    }, 0);
  }
}
