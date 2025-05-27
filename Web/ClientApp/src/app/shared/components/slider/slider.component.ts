import { Component, Input } from '@angular/core';
import { BaseFormField } from '../base-form';
import { SliderChangeEvent } from 'primeng/slider';

@Component({
  selector: 'slider',
  template: `
    <p-slider 
    [class]="class" 
    [(ngModel)]="value" 
    [disabled]="disabled"
    [range]="range"
    [orientation]="orientation"
    (onChange)="onSliderChange($event)">
    </p-slider>
    @if(hasErrors){
      <small class="text-red-600" *ngIf="control.hasError('required') || control.hasError('empty')">
          {{ 'message.STD00007' | translate }}
      </small>
    }`
})
export class SliderComponent extends BaseFormField {

  @Input() range: boolean = false
  @Input() orientation: "vertical" | "horizontal" = "horizontal"

  override writeValue(value: number | number[]): void {
    this.value = value
  }

  onSliderChange(event: SliderChangeEvent) {
    const value = (event["value"] || event["values"]) || 0
    this.onChange(value)
    this.value = value
  }
}
