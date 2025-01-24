import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { Observable } from 'rxjs';

import { AuthService } from 'src/app/core/auth/auth.service';
import { SpinnerService } from 'src/app/shared/components/spinner/spinner.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private router: Router,
    private auth: AuthService,
    private spinnerService: SpinnerService
  ) { }

  public canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    this.spinnerService.openSpinner();
    if (this.auth.isInvalidToken()) {            
        this.router.navigate(['/security', 'auth']);      
        this.spinnerService.closeSpinner();
        return false;
    } 
    this.spinnerService.closeSpinner();
    return true;
  }
}