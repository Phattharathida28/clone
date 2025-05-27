import { Component, Input } from '@angular/core';
import { BaseFormField } from '../base-form';

@Component({
  selector: 'rating',
  template: `
    <p-rating 
    [(ngModel)]="value" 
    [stars]="stars"
    [cancel]="cancel"
    [disabled]="disabled"
    [readonly]="readonly"
    (onRate)="onRate(this.value)"
    (onCancel)="onRate(this.value)">
    </p-rating>
    @if (hasErrors) {
      <small class="text-red-600" *ngIf="control.hasError('required') || control.hasError('empty')">
          {{ 'message.STD00007' | translate }}
      </small>
    }`
})
export class RatingComponent extends BaseFormField {

  @Input() stars: number = 5
  @Input() cancel: boolean = true

  override writeValue(value: number): void {
    this.value = value
  }

  onRate(value: number) {
    this.onChange(value)
  }
}
