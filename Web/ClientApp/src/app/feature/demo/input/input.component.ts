import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LayoutService } from '@app/layout/service/layout.service';
import { ModalService } from '@app/shared/components/modal/modal.service';
import { SelectItem } from 'primeng/api';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'x-input',
  templateUrl: './input.component.html'
})
export class InputComponent {

  form: FormGroup = new FormGroup<{
    float: FormControl<string>,
    switch: FormControl<boolean>,
    slider: FormControl<number>,
    sliderRange: FormControl<number[]>,
    rating: FormControl<number>,
    colorpicker: FormControl<string>,
    knob: FormControl<number>,
    togglebutton: FormControl<boolean>,
    selectbutton: FormControl<number>,
    selectbuttonmulti: FormControl<number[]>,
    inputgroupCheckbox: FormControl<boolean>,
    radio: FormControl<number>,
    checkbox: FormControl<number[]>,
  }>({
    float: new FormControl(null, Validators.required),
    switch: new FormControl(true),
    slider: new FormControl(70),
    sliderRange: new FormControl([20, 60]),
    rating: new FormControl(3),
    colorpicker: new FormControl(this.layoutService.config().color),
    knob: new FormControl(25),
    togglebutton: new FormControl(true),
    selectbutton: new FormControl(2),
    selectbuttonmulti: new FormControl([2, 3]),
    inputgroupCheckbox: new FormControl(true),
    radio: new FormControl(3),
    checkbox: new FormControl([1, 3]),
  })

  autocompleteItem: string[] = []

  items: SelectItem[] = []

  constructor(private layoutService: LayoutService, private md: ModalService) {
    for (let index = 0; index < 3; index++) {
      this.autocompleteItem.push(`item ${index + 1}`)
      this.items.push({ value: index + 1, label: `item ${index + 1}` })
    }
  }

  canDeactivate():Observable<boolean> {
    if (this.form.dirty) return this.md.confirm("message.STD00010");
    return of(true);
  }
}
