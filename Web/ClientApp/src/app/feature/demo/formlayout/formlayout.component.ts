import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SelectItem } from 'primeng/api';

@Component({
  selector: 'x-formlayout',
  templateUrl: './formlayout.component.html'
})
export class FormlayoutComponent {
  selectedState: any = null;

  states: any[] = [
    { name: 'Arizona', code: 'Arizona' },
    { name: 'California', value: 'California' },
    { name: 'Florida', code: 'Florida' },
    { name: 'Ohio', code: 'Ohio' },
    { name: 'Washington', code: 'Washington' }
  ];

  dropdownItems: SelectItem[] = [
    { label: 'Option 1', value: 'Option 1' },
    { label: 'Option 2', value: 'Option 2' },
    { label: 'Option 3', value: 'Option 3' }
  ];

  cities1: any[] = [];

  cities2: any[] = [];

  city1: any = null;

  city2: any = null;

  form: FormGroup = new FormGroup<{
    f1: FormControl<string | null>
  }>({
    f1: new FormControl('Option 3', Validators.maxLength(1))
  })

  constructor() {
    this.form.valueChanges.subscribe((res) => console.log(this.form.getRawValue()))
  }
}
