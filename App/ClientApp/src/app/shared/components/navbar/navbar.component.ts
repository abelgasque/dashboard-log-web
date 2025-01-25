import { Component, Input, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/auth/auth.service';
import { SecurityService } from 'src/app/layouts/security/security.service';
import { NavbarService } from './navbar.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  @Input() hasBtnMenu: boolean = false;
  @Input() hasBtnUserAuth: boolean = false;

  constructor(
    public navbarService: NavbarService,
    public authService: AuthService,
    public securityService: SecurityService,
  ) { }

  ngOnInit() { }

  public loggout(){
    this.securityService.Loggout();
    this.navbarService.closeSideBar();
  }
}
