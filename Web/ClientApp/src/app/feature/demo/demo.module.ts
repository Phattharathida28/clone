import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DemoRoutingModule } from './demo-routing.module';
import { FormlayoutComponent } from './formlayout/formlayout.component';
import { SharedModule } from '@app/shared/shared.module';
import { InputComponent } from './input/input.component';
import { ButtonComponent } from './button/button.component';
import { LazyTranslationService } from '@app/core/services/lazy-translation.service';
import { FloatlabelComponent } from './floatlabel/floatlabel.component';
import { InvalidstateComponent } from './invalidstate/invalidstate.component';
import { TableComponent } from './table/table.component';
import { PanelComponent } from './panel/panel.component';
import { OverlayComponent } from './overlay/overlay.component';
import { MessageComponent } from './message/message.component';
import { ChartComponent } from './chart/chart.component';
import { IconsComponent } from './icons/icons.component';
import { ModalComponent, ModalResolveComponent } from './overlay/modal/modal.component';
import { AttachmentComponent } from './attachment/attachment.component';
import { LifecycleComponent } from './lifecycle/lifecycle.component';
import { TestChangeComponent } from './lifecycle/test-change/test-change.component';
import { BindingComponent } from './binding/binding.component';


@NgModule({
  declarations: [
    FormlayoutComponent,
    InputComponent,
    ButtonComponent,
    FloatlabelComponent,
    InvalidstateComponent,
    TableComponent,
    PanelComponent,
    OverlayComponent,
    MessageComponent,
    ChartComponent,
    IconsComponent,
    ModalComponent,
    ModalResolveComponent,
    AttachmentComponent,
    LifecycleComponent,
    TestChangeComponent,
    BindingComponent
  ],
  imports: [
    CommonModule,
    DemoRoutingModule,
    SharedModule
  ]
})
export class DemoModule {
  constructor(private lazy: LazyTranslationService) {
    lazy.add('demo');
  }
}
