import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { DashboardLogIntegrationModule } from 'src/app/layouts/dashboard/modules/dashboard-log-integration/dashboard-log-integration.module';

import { DashboardComponent } from 'src/app/layouts/dashboard/dashboard.component';

@NgModule({
  declarations: [
    DashboardComponent,    
  ],
  imports: [
    CommonModule,
    RouterModule,

    DashboardLogIntegrationModule,
  ]
})
export class DashboardModule { }
