import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { QoRoutingModule } from './qo-routing.module';
import { Qort01Component } from './qort01/qort01.component';
import { SharedModule } from "../../shared/shared.module";
import { LazyTranslationService } from '@app/core/services/lazy-translation.service';
import { Qort01DetailComponent } from './qort01/qort01-detail/qort01-detail.component';




@NgModule({
  declarations: [
    Qort01Component,
    Qort01DetailComponent,
    
  ],
  imports: [
    CommonModule,
    QoRoutingModule,
    SharedModule,
]
})
export class QoModule {
    constructor(private lazy: LazyTranslationService) {
      lazy.add('qo');
    }
}