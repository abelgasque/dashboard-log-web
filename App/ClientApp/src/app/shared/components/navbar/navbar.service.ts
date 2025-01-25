import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NavbarService {

  public openedSideBar: boolean = false;

  constructor() { }

  public toggleSideBar() {
    this.openedSideBar = !this.openedSideBar;
  }

  public openSideBar(){
    this.openedSideBar = true;
  }

  public closeSideBar(){
    this.openedSideBar = false;
  }
}
