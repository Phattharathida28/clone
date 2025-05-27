import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  template: ''
})
export class SubscriptionDisposer implements OnDestroy {
  protected ngUnsubscribe: Subject<void> = new Subject<void>();
  
  constructor() { }

  ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}