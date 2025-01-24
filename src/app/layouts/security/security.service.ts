import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { ReturnDTO, User } from 'src/app/core/util/model';
import { NavbarService } from 'src/app/shared/components/navbar/navbar.service';
import { AuthService } from 'src/app/core/auth/auth.service';
import { CoreService } from 'src/app/core/core.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SecurityService {
  
  private baseUrl: string;

  constructor(     
    private router: Router,
    private http: HttpClient,
    private authService: AuthService,
    private navbarService: NavbarService,
    private coreService: CoreService,
  ) {    
    this.baseUrl =`${ this.coreService.getBaseUrl() }/ws/Token`; 
  }

  public TokenAuthenticate(entidade: User) : Observable<any>  {
    return this.http.post<ReturnDTO>(`${this.baseUrl}/Authenticate`, entidade);
  }

  public Loggout(){
    this.authService.hasUserAuth = false;
    this.authService.setToken(null);    
    this.navbarService.closeSideBar();
    this.router.navigate(['']);    
  }
}
