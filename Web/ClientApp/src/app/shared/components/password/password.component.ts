import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { BaseFormField } from '../base-form';

@Component({
  selector: 'password',
  templateUrl: './password.component.html'
})
export class PasswordComponent extends BaseFormField implements OnChanges {
  @Input() floatLabel: boolean = false;
  @Input() helpText: string = "";
  @Input() axis: "horizontal" | "vertical" = "vertical"
  @Input() icon: string = ''
  @Input() iconPosition: 'left' | 'right' = 'left'
  @Input() feedback: boolean = false;
  @Input() toggleMask: boolean = true;
  @Input() mediumRegex: string = "^(((?=.*[a-z])(?=.*[A-Z]))|((?=.*[a-z])(?=.*[0-9]))|((?=.*[A-Z])(?=.*[0-9])))(?=.{6,})"
  @Input() strongRegex: string = "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{8,})"
  @Input() helpTextClass: string = ''

  ngOnChanges(changes: SimpleChanges) {
    if (changes["value"]) {
      this.value = this.value ?? '';
    }
  }
  override writeValue(value: any): void {
    this.value = value
  }

  onTextChange($event: any) {
    const value = $event?.target?.value;
    this.onChange(value)
  }
}