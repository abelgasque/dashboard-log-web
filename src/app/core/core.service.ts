import { Injectable } from '@angular/core';

import * as moment from 'moment';

import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CoreService {

  constructor() { }

  public formatDatePtBr(data: string) {    
    return moment(data).format("DD/MM/YYYY HH:mm:ss");
  }

  public getBaseUrl() {
    return (environment.useUrlProd) ? environment.baseUrlProd : environment.baseUrlDev;
  }
}
