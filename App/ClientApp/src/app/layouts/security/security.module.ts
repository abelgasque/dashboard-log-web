import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { FlexLayoutModule } from "@angular/flex-layout";

import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';

// import { JwtModule } from "@auth0/angular-jwt";

import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';

import { SecurityService } from 'src/app/layouts/security/security.service';
import { ErrorHandlerService } from 'src/app/core/util/error-handler.service';
import { SpinnerService } from 'src/app/shared/components/spinner/spinner.service';
import { MessagesService } from 'src/app/shared/components/messages/messages.service';
import { CoreService } from 'src/app/core/core.service';

import { environment } from 'src/environments/environment';
import { SecurityComponent } from 'src/app/layouts/security/security.component';
import { LoginComponent } from 'src/app/layouts/security/modules/login/login.component';

export function tokenGetter() {
  return localStorage.getItem("access_token");
}

@NgModule({
  declarations: [
    SecurityComponent,
    LoginComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule,
    FormsModule,

    FlexLayoutModule,
    
    MatIconModule,
    MatButtonModule,
    MatCardModule,

    InputTextModule,
    PasswordModule,
    
    // JwtModule.forRoot({
    //   config: {
    //     tokenGetter: tokenGetter,
    //     allowedDomains: environment.tokenWhitelistedDomains,
    //     disallowedRoutes: environment.tokenBlacklistedRoutes
    //   }
    // }),
  ],
  providers:[
    SecurityService,
    ErrorHandlerService,
    SpinnerService,
    MessagesService,
    CoreService,
  ]
})
export class SecurityModule { }
