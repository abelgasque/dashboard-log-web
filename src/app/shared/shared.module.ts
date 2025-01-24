import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatMenuModule } from '@angular/material/menu';
import { MatListModule } from '@angular/material/list';
import { MatCardModule } from '@angular/material/card';

import { NgChartsModule } from 'ng2-charts';

import { ButtonModule } from 'primeng/button';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { MenuModule } from 'primeng/menu';

import { MessagesService } from './components/messages/messages.service';
import { NavbarService } from './components/navbar/navbar.service';
import { SecurityService } from '../layouts/security/security.service';
import { SpinnerService } from './components/spinner/spinner.service';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { MessagesComponent } from './components/messages/messages.component';
import { SidebarDefaultComponent } from './components/sidebar-default/sidebar-default.component';
import { PageNotFoundComponent } from './layouts/page-not-found/page-not-found.component';
import { PageNotAuthorizedComponent } from './layouts/page-not-authorized/page-not-authorized.component';
import { PageMaintenanceComponent } from './layouts/page-maintenance/page-maintenance.component';
import { SpinnerLoadingComponent } from './components/spinner-loading/spinner-loading.component';
import { WidgetChartDynamicComponent } from './widgets/widget-chart-dynamic/widget-chart-dynamic.component';
import { WidgetChartPieComponent } from './widgets/widget-chart-pie/widget-chart-pie.component';

@NgModule({
  declarations: [
    NavbarComponent,
    FooterComponent,
    SpinnerComponent,
    MessagesComponent,
    SidebarDefaultComponent,
    SpinnerLoadingComponent,
    
    PageNotFoundComponent,
    PageNotAuthorizedComponent,
    PageMaintenanceComponent,        
    
    WidgetChartDynamicComponent,
    WidgetChartPieComponent,
  ],
  imports: [
    CommonModule,    
    RouterModule,
    FormsModule,

    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatDividerModule,
    MatMenuModule,
    MatListModule,
    MatCardModule,
    
    NgChartsModule,

    ButtonModule,
    ProgressSpinnerModule,
    ToastModule,
    MenuModule
  ],
  exports:[
    NavbarComponent,
    FooterComponent,
    SpinnerComponent,
    SpinnerLoadingComponent,
    MessagesComponent,
    SidebarDefaultComponent,
    
    WidgetChartDynamicComponent,
    WidgetChartPieComponent,
  ],
  providers:[
    MessageService,
    MessagesService,
    SecurityService,
    SpinnerService,
    NavbarService,
  ]
})
export class SharedModule { }
