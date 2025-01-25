import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { MatCardModule } from '@angular/material/card';
import {MatMenuModule} from '@angular/material/menu';
import {MatIconModule} from '@angular/material/icon'
import {MatButtonModule} from '@angular/material/button';

import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { MenuModule } from 'primeng/menu';

import { CoreService } from 'src/app/core/core.service';
import { SharedModule } from 'src/app/shared/shared.module';
import { LogIntegrationModule } from 'src/app/layouts/log-integration/log-integration.module';
import { DashboardLogIntegrationComponent } from 'src/app/layouts/dashboard/modules/dashboard-log-integration/dashboard-log-integration.component';
import { DashboardLogIntegrationService } from 'src/app/layouts/dashboard/modules/dashboard-log-integration/dashboard-log-integration.service';

@NgModule({
  declarations: [
    DashboardLogIntegrationComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule,
    FormsModule,
  
    MatCardModule,
    MatMenuModule,
    MatIconModule,
    MatButtonModule,

    ButtonModule,
    TableModule,
    MenuModule,

    SharedModule,
    LogIntegrationModule,
  ],
  providers:[
    DashboardLogIntegrationService,
    CoreService,
  ]
})
export class DashboardLogIntegrationModule { }
