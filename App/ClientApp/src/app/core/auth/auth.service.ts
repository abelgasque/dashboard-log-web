import { Injectable } from '@angular/core';

//import { JwtHelperService } from "@auth0/angular-jwt";

import { User } from 'src/app/core/util/model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public hasUserAuth: boolean = true;

  // constructor(private jwtHelperService: JwtHelperService) { 
  //   this.validateUserSession();
  // }

  private validateUserSession(){
    if((this.getToken() != null)){            
      this.hasUserAuth = true;
    }
  }

  public isInvalidToken(){
    let token: string = this.getToken();
    
    if((token != null)){            
      return this.isTokenExpired(token);
    }

    return true;
  }

  public setToken(token: string) {
    localStorage.setItem("access_token", token);
  }
    
  public getToken() {
    return localStorage.getItem("access_token");
  }

  public decodeToken(pToken: string){
    //return this.jwtHelperService.decodeToken(pToken);
  }

  public getTokenExpirationDate(pToken: string){ 
    //return this.jwtHelperService.getTokenExpirationDate(pToken);
  }

  public isTokenExpired(pToken: string){ 
    //return this.jwtHelperService.isTokenExpired(pToken);
  }
}
