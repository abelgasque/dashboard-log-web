import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

import { User } from 'src/app/core/util/model';
import { ErrorHandlerService } from 'src/app/core/util/error-handler.service';
import { MessagesService } from 'src/app/shared/components/messages/messages.service';
import { SpinnerService } from 'src/app/shared/components/spinner/spinner.service';
import { SecurityService } from 'src/app/layouts/security/security.service';
import { AuthService } from 'src/app/core/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public entity = new User();

  constructor(
    private router: Router,
    private securityService: SecurityService,
    private errorHandlerService: ErrorHandlerService,
    private spinnerService: SpinnerService,
    private messagesService: MessagesService,
    private authService: AuthService,
  ) { }

  ngOnInit(): void {
  }

  public login(f: NgForm){
    if((this.entity.userName.length <= 0) || (this.entity.password.length <= 0)){
      this.messagesService.showWarn("Preencha o formulário corretamente!");
    }else{
      this.spinnerService.openSpinner();
      this.securityService.TokenAuthenticate(this.entity).subscribe({
        next: (resp) => {
          this.authService.hasUserAuth = true;
          this.authService.setToken(resp.resultObject.token);
          this.router.navigate(['/dashboard','log-integration']);
          this.spinnerService.closeSpinner();
        },
        error: (error) => { 
          console.log(error);
          this.errorHandlerService.handle(error);
          this.spinnerService.closeSpinner();
        }
      });
    }
  }
}
