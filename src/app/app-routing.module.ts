import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from 'src/app/core/auth/auth.guard';
import { CoreComponent } from './core/core.component';

//default module
import { DefaultComponent } from 'src/app/layouts/default/default.component';
import { HomeComponent } from 'src/app/layouts/default/modules/home/home.component';

//dashboard module
import { DashboardComponent } from 'src/app/layouts/dashboard/dashboard.component';
import { DashboardLogIntegrationComponent } from 'src/app/layouts/dashboard/modules/dashboard-log-integration/dashboard-log-integration.component';

//security module
import { SecurityComponent } from 'src/app/layouts/security/security.component';
import { LoginComponent } from 'src/app/layouts/security/modules/login/login.component';

//shared module
import { PageMaintenanceComponent } from 'src/app/shared/layouts/page-maintenance/page-maintenance.component';
import { PageNotAuthorizedComponent } from 'src/app/shared/layouts/page-not-authorized/page-not-authorized.component';
import { PageNotFoundComponent } from 'src/app/shared/layouts/page-not-found/page-not-found.component';

const routes: Routes = [
  {
    path: '',
    component: CoreComponent,
    children: [
      //default module
      {
        path: '', component: DefaultComponent,
        children: [
          { path: '', component: HomeComponent },          
        ]
      },

      //dashboard module
      {
        path: 'dashboard', component: DashboardComponent,
        children: [
          { path: 'log-integration', component: DashboardLogIntegrationComponent, canActivate: [AuthGuard] },
        ]
      },

      //security module
      {
        path: 'security', component: SecurityComponent,
        children: [
          { path: 'auth', component: LoginComponent },
        ]
      },

      //shared module
      { path: 'page-not-authorized', component: PageNotAuthorizedComponent },
      { path: 'page-not-found', component: PageNotFoundComponent },
      { path: 'page-maintenance', component: PageMaintenanceComponent },

      { path: '', redirectTo: '', pathMatch: 'full' },
      { path: '**', redirectTo: 'page-not-found' }
    ]
  }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }