import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SpinnerService {

  public isActive: boolean = false;

  constructor() { }

  public openSpinner(){
    this.isActive = true;
  }

  public closeSpinner(){
    this.isActive = false;
  }
}
