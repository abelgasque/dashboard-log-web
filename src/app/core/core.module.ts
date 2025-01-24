import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { MatSidenavModule } from '@angular/material/sidenav';

import { ConfirmDialogModule } from 'primeng/confirmdialog';

import { AuthGuard } from 'src/app/core/auth/auth.guard';
import { AuthService } from 'src/app/core/auth/auth.service';
import { SharedModule } from 'src/app/shared/shared.module';
import { DefaultModule } from 'src/app/layouts/default/default.module';
import { SecurityModule } from 'src/app/layouts/security/security.module';
import { DashboardModule } from 'src/app/layouts/dashboard/dashboard.module';
import { LogIntegrationModule } from 'src/app/layouts/log-integration/log-integration.module';
import { ErrorHandlerService } from 'src/app/core/util/error-handler.service';
import { NavbarService } from 'src/app/shared/components/navbar/navbar.service';
import { CoreComponent } from 'src/app/core/core.component';

@NgModule({
  declarations: [
    CoreComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),    
    BrowserAnimationsModule,
    FormsModule,
    RouterModule,
    
    MatSidenavModule,
    
    ConfirmDialogModule,

    SharedModule,    
    SecurityModule,
    DefaultModule,
    DashboardModule,
    LogIntegrationModule,
  ],
  providers:[
    AuthGuard,
    AuthService,
    ErrorHandlerService,
    NavbarService
  ]
})
export class CoreModule { }
