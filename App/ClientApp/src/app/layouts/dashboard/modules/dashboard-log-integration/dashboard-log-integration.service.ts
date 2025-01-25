import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs';

import { CoreService } from 'src/app/core/core.service';
import { ReturnDTO } from 'src/app/core/util/model';

@Injectable({
  providedIn: 'root'
})
export class DashboardLogIntegrationService {

  private baseUrl: string;

  constructor(    
    private http: HttpClient,
    private coreService: CoreService,
  ) 
  { 
    this.baseUrl =`${ this.coreService.getBaseUrl() }/ws/LogIntegration`; 
  }

  public GetLogIntegrationForChartDynamic(pMustFilterYear: boolean) : Observable<any> {     
    let params = new HttpParams({
      fromObject:{
        pMustFilterYear: pMustFilterYear,
      }
    });

    return this.http.get<Promise<ReturnDTO>>(`${this.baseUrl}/GetLogIntegrationForChartDynamic`, {params});
  }
  
  public GetLogIntegration() : Observable<any> {
    return this.http.get<Promise<ReturnDTO>>(`${this.baseUrl}/GetLogIntegrationDay`);
  }
}
