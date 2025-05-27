import { Component, Input, OnInit } from '@angular/core';
import { BaseFormField } from '../base-form';

@Component({
  selector: 'knob',
  template: `
    <p-knob 
    [(ngModel)]="value" 
    [min]="min" 
    [max]="max"
    [size]="size"
    [strokeWidth]="strokeWidth"
    [disabled]="disabled"
    [showValue]="showValue"
    [readonly]="readonly"
    [valueTemplate]="valueTemplate"
    (onChange)="onKnobChange(this.value)">
    </p-knob>
    @if(hasErrors){
      <small class="text-red-600" *ngIf="control.hasError('required') || control.hasError('empty')">
          {{ 'message.STD00007' | translate }}
      </small>
    }
    `
})
export class KnobComponent extends BaseFormField implements OnInit {

  @Input() min: number = 0
  @Input() max: number = 100
  @Input() size: number = 100
  @Input() strokeWidth: number = 14
  @Input() showValue: boolean = true
  @Input() valueTemplate: string = ""
  oldValue: number

  ngOnInit(): void {
    if (!this.valueTemplate) this.valueTemplate = `${this.value}`
    else this.valueTemplate = this.valueTemplate.replaceAll("[value]", this.value.toString())
  }

  override writeValue(value: number): void {
    this.value = value
    this.oldValue = value
  }

  onKnobChange(value: number) {
    this.onChange(value)
    if (this.valueTemplate) this.valueTemplate = this.valueTemplate.replaceAll(this.oldValue?.toString(), value?.toString())
    this.oldValue = value
  }
}
