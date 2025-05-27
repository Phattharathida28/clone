import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { LayoutComponent } from './layout.component';
import { FooterComponent } from './footer.component';
import { MenuComponent } from './menu.component';
import { SidebarComponent } from './sidebar.component';
import { TopBarComponent } from './topbar.component';
import { MenuitemComponent } from './menuitem.component';
import { ConfigModule } from './config/config.module';
import { SharedModule } from '@app/shared/shared.module';

@NgModule({
    declarations: [
        MenuitemComponent,
        TopBarComponent,
        FooterComponent,
        MenuComponent,
        SidebarComponent,
        LayoutComponent,
    ],
    exports: [
        LayoutComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        BrowserAnimationsModule,
        SharedModule,
        RouterModule,
        ConfigModule,
    ]
})
export class LayoutModule { }
