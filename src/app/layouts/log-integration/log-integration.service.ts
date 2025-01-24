import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { CoreService } from 'src/app/core/core.service';
import { ReturnDTO } from 'src/app/core/util/model';

@Injectable({
  providedIn: 'root'
})
export class LogIntegrationService {

  private baseUrl: string;

  constructor(    
    private http: HttpClient,
    private coreService: CoreService,
  ) { 
    this.baseUrl =`${ this.coreService.getBaseUrl() }/ws/LogIntegration`; 
  }

  public Insert(pEntity: any) : Observable<any> {
    return this.http.post<Promise<ReturnDTO>>(`${this.baseUrl}`, pEntity);
  }

  public Update(pEntity: any) : Observable<any> {
    return this.http.put<Promise<ReturnDTO>>(`${this.baseUrl}`, pEntity);
  }

  public Delete(pId: number) : Observable<any> {    
    return this.http.delete<Promise<ReturnDTO>>(`${this.baseUrl}/${pId}`);
  }

  public FindById(pId: number) : Observable<any> {
    return this.http.get<Promise<ReturnDTO>>(`${this.baseUrl}/${pId}`);
  }
}
