import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CanDeactivate } from '@app/core/guard/core.guard';
import { Surt01Component } from './surt01/surt01.component';
import { Surt01DetailComponent } from './surt01/surt01-detail/surt01-detail.component';
import { program, programs, master as surt01Master } from './surt01/surt01.resolver';
import { Surt02Component } from './surt02/surt02.component';
import { Surt02DetailComponent } from './surt02/surt02-detail/surt02-detail.component';
import { menu, menus, master as surt02Master } from './surt02/surt02.resolver';
import { Surt03Component } from './surt03/surt03.component';
import { Surt03DetailComponent } from './surt03/surt03-detail/surt03-detail.component';
import { profile, profiles, master as surt03Master } from './surt03/surt03.resolver';
import { Surt04Component } from './surt04/surt04.component';
import { Surt04DetailComponent } from './surt04/surt04-detail/surt04-detail.component';
import { user, users, master as surt04Master } from './surt04/surt04.resolver';
import { Surt05Component } from './surt05/surt05.component';
import { messages, message } from './surt05/surt05.resolver';
import { Surt06Component } from './surt06/surt06.component';
import { detail, list } from './surt06/surt06.resolver';
import { Surt06DetailComponent } from './surt06/surt06-detail/surt06-detail.component';
import { Surt05DetailComponent } from './surt05/surt05-detail/surt05-detail.component';
import { Surt07Component } from './surt07/surt07.component';
import { contents } from './surt07/surt07.resolver';
import { Surt08Component } from './surt08/surt08.component';
import { activityLogs } from './surt08/surt08.resolver';

const routes: Routes = [
  { path: 'surt01', component: Surt01Component, title: 'Program', resolve: { programs }, data: { code: 'surt01' } },
  { path: 'surt01/detail', component: Surt01DetailComponent, title: 'Program Detail', resolve: { program, master: surt01Master }, canDeactivate: [CanDeactivate], data: { code: 'surt01' } },
  { path: 'surt02', component: Surt02Component, title: 'Menu', resolve: { menus }, data: { code: 'surt02' } },
  { path: 'surt02/detail', component: Surt02DetailComponent, title: 'Menu Detail', resolve: { menu, master: surt02Master }, canDeactivate: [CanDeactivate], data: { code: 'surt02' } },
  { path: 'surt03', component: Surt03Component, title: 'Permission', resolve: { profiles }, data: { code: 'surt03' } },
  { path: 'surt03/detail', component: Surt03DetailComponent, title: 'Permission Detail', resolve: { profile, master: surt03Master }, canDeactivate: [CanDeactivate], data: { code: 'surt03' } },
  { path: 'surt04', component: Surt04Component, title: 'User', resolve: { users }, data: { code: 'surt04' } },
  { path: 'surt04/detail', component: Surt04DetailComponent, title: 'User Detail', resolve: { user, master: surt04Master }, canDeactivate: [CanDeactivate], data: { code: 'surt04' } },
  { path: 'surt05', component: Surt05Component, title: 'Message', resolve: { messages }, data: { code: 'surt05' } },
  { path: 'surt05/detail', component: Surt05DetailComponent, title: 'Message Detail', resolve: { message }, canDeactivate: [CanDeactivate], data: { code: 'surt05' } },
  { path: 'surt06', component: Surt06Component, title: 'Parameter', resolve: { list }, data: { code: 'surt06' } },
  { path: 'surt06/detail', component: Surt06DetailComponent, title: 'Parameter Detail', resolve: { detail }, canDeactivate: [CanDeactivate], data: { code: 'surt06' } },
  { path: 'surt07', component: Surt07Component, title: 'File', resolve: { contents }, data: { code: 'surt07' } },
  { path: 'surt08', component: Surt08Component, title: 'Activity Logs', resolve: { activityLogs }, data: { code: 'surt08' } },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SuRoutingModule { }
