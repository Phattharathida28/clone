import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoadingComponent } from './pages/loading/loading.component';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { ConfigurationService } from './core/services/configuration.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LayoutModule } from './layout/layout.module';
import { NotfoundComponent } from './pages/notfound/notfound.component';
import { EmptyComponent } from './pages/empty/empty.component';
import { AccessdeniedComponent } from './pages/accessdenied/accessdenied.component';
import { ConfirmationService, MessageService } from 'primeng/api';


const initializeApp = (appConfig: ConfigurationService) => () => appConfig.loadConfig()
@NgModule({
  declarations: [
    AppComponent,
    LoadingComponent,
    NotfoundComponent,
    EmptyComponent,
    AccessdeniedComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CoreModule,
    SharedModule,
    LayoutModule
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      multi: true,
      deps: [ConfigurationService]
    },
    MessageService, ConfirmationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }