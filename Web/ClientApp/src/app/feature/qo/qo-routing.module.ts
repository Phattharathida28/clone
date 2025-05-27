import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Qort01Component } from './qort01/qort01.component';
import { Qort01Resolver } from './qort01/resolvers/qort01.resolver';
import { Qort01DetailComponent } from './qort01/qort01-detail/qort01-detail.component';
import { Qort01DetailResolver } from './qort01/resolvers/qort01-detail.resolver';
import { CanDeactivate } from '@app/core/guard/core.guard';

const routes: Routes = [
  {
    path: 'qort01',
    component:Qort01Component,
    resolve:{qort01:Qort01Resolver},
    data: {code: 'qort01'}
  },
  {
    path: 'qort01/detail',
    component:Qort01DetailComponent,
    resolve:{qort01:Qort01DetailResolver},
    canDeactivate:[CanDeactivate],
    data: {code: 'qort01' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class QoRoutingModule { }






