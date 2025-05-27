import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Dbrt01Component } from './dbrt01/dbrt01.component';
import { language, languages, master as dbrt01Master } from './dbrt01/dbrt01.resolver';
import { Dbrt01DetailComponent } from './dbrt01/dbrt01-detail/dbrt01-detail.component';
import { CanDeactivate } from '@app/core/guard/core.guard';
import { Dbrt02DetailComponent } from './dbrt02/dbrt02-detail/dbrt02-detail.component';
import { Dbrt02Component } from './dbrt02/dbrt02.component';
import { listItemGroup, listItemGroups, master as dbrt02Master } from './dbrt02/dbrt02.resolver';

const routes: Routes = [
  { path: 'dbrt01', component: Dbrt01Component, title: 'Language', resolve: { languages }, data: { code: 'dbrt01' } },
  { path: 'dbrt01/detail', component: Dbrt01DetailComponent, title: 'Language', resolve: { language, master: dbrt01Master }, canDeactivate: [CanDeactivate], data: { code: 'dbrt01' } },
  { path: 'dbrt02', component: Dbrt02Component, title: 'List Item Group', resolve: { listItemGroups }, data: { code: 'dbrt02' } },
  { path: 'dbrt02/detail', component: Dbrt02DetailComponent, title: 'List Item Group', resolve: { listItemGroup, master: dbrt02Master }, canDeactivate: [CanDeactivate], data: { code: 'dbrt02' } },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DbRoutingModule { }
