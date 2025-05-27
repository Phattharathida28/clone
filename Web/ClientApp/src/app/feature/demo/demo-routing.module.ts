import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FormlayoutComponent } from './formlayout/formlayout.component';
import { InputComponent } from './input/input.component';
import { ButtonComponent } from './button/button.component';
import { FloatlabelComponent } from './floatlabel/floatlabel.component';
import { InvalidstateComponent } from './invalidstate/invalidstate.component';
import { TableComponent } from './table/table.component';
import { PanelComponent } from './panel/panel.component';
import { OverlayComponent } from './overlay/overlay.component';
import { MessageComponent } from './message/message.component';
import { ChartComponent } from './chart/chart.component';
import { IconsComponent } from './icons/icons.component';
import { CanDeactivate } from '@app/core/guard/core.guard';
import { AttachmentComponent } from './attachment/attachment.component';
import { LifecycleComponent } from './lifecycle/lifecycle.component';
import { BindingComponent } from './binding/binding.component';

const routes: Routes = [
  { path: 'lifecycle', component: LifecycleComponent, title: "Life Cycle", data: { code: "lifecycle" } },
  { path: 'binding', component: BindingComponent, title: "Binding", data: { code: "binding" } },
  { path: 'formlayout', component: FormlayoutComponent, title: "Form layout", data: { code: "formlayout" } },
  { path: 'input', component: InputComponent, title: "Input", data: { code: "input" }, canDeactivate: [CanDeactivate] },
  { path: 'floatlabel', component: FloatlabelComponent, title: "Float label", data: { code: "floatlabel" } },
  { path: 'invalidstate', component: InvalidstateComponent, title: "Invalid state", data: { code: "invalidstate" } },
  { path: 'button', component: ButtonComponent, title: 'Button', data: { code: "button" } },
  { path: 'table', component: TableComponent, title: 'Table', data: { code: "table" } },
  { path: 'panel', component: PanelComponent, title: 'Panel', data: { code: "panel" } },
  { path: 'overlay', component: OverlayComponent, title: 'Overlay', data: { code: "overlay" } },
  { path: 'message', component: MessageComponent, title: 'Message', data: { code: "message" } },
  { path: 'charts', component: ChartComponent, title: 'Charts', data: { code: "charts" } },
  { path: 'icons', component: IconsComponent, title: 'Icons', data: { code: "icons" } },
  { path: 'attachment', component: AttachmentComponent, title: 'Attachment', data: { code: "attachment" } },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DemoRoutingModule { }
