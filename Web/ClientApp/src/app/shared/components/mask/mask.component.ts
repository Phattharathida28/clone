import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { BaseFormField } from '../base-form';

@Component({
  selector: 'mask',
  templateUrl: './mask.component.html'
})
export class MaskComponent extends BaseFormField implements OnChanges {
  @Input() floatLabel: boolean = false;
  @Input() helpText: string = "";
  @Input() axis: "horizontal" | "vertical" = "vertical"
  @Input() icon: string = ''
  @Input() iconPosition: 'left' | 'right' = 'left'
  @Input() mask: string = ''
  @Input() helpTextClass: string = ''

  ngOnChanges(changes: SimpleChanges) {
    if (changes["value"]) {
      this.value = this.value ?? '';
    }
  }
  override writeValue(value: any): void {
    this.value = value
  }

  onTextChange(event: Event) {
    const value = event?.target["value"];
    this.onChange(value)
    this.value = value
  }
}
