import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DbRoutingModule } from './db-routing.module';
import { LazyTranslationService } from '@app/core/services/lazy-translation.service';
import { SharedModule } from '@app/shared/shared.module';
import { Dbrt01Component } from './dbrt01/dbrt01.component';
import { Dbrt01DetailComponent } from './dbrt01/dbrt01-detail/dbrt01-detail.component';
import { Dbrt02Component } from './dbrt02/dbrt02.component';
import { Dbrt02DetailComponent } from './dbrt02/dbrt02-detail/dbrt02-detail.component';


@NgModule({
  declarations: [
  
    Dbrt01Component,
       Dbrt01DetailComponent,
       Dbrt02Component,
       Dbrt02DetailComponent
  ],
  imports: [
    CommonModule,
    DbRoutingModule,
    SharedModule
  ]
})
export class DbModule {
  constructor(private lazy: LazyTranslationService) {
    lazy.add('db');
  }
}
