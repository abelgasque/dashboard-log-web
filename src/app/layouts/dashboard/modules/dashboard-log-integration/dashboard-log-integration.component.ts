import { Component, OnInit } from '@angular/core';

import { MenuItem } from 'primeng/api';

import { CoreService } from 'src/app/core/core.service';
import { ErrorHandlerService } from 'src/app/core/util/error-handler.service';
import { DashboardLogIntegrationService } from './dashboard-log-integration.service';

@Component({
  selector: 'app-dashboard-log-integration',
  templateUrl: './dashboard-log-integration.component.html',
  styleUrls: ['./dashboard-log-integration.component.css']
})
export class DashboardLogIntegrationComponent implements OnInit {

  public listLogIntegration: any[] = [];
  public loadinglistLogIntegration: boolean = true;
  public dialogLogIntegration: boolean = false;
  public items: MenuItem[];

  public chartTypeBar: string = 'line';
  public chartDataBar: any;
  public titleBar: string = "Gráfico mensal de " + new Date().getFullYear().toString();    
  public menuItemsBar: MenuItem[]; 
  public loadingChartBar: boolean = true;

  public chartTypeLine: string = 'line';
  public chartDataLine: any;
  public titleLine: string = "Gráfico anual";    
  public menuItemsLine: MenuItem[]; 
  public loadingChartLine: boolean = true;
  public chartColorsLine: any[] = [];

  constructor(
    public coreService: CoreService,
    private dashboardLogIntegrationService: DashboardLogIntegrationService,
    private errorHandlerService: ErrorHandlerService,
  ) { 

    this.items = [
      {label: 'Update', icon: 'pi pi-refresh', command: () => {  }},
      {label: 'Delete', icon: 'pi pi-times', command: () => { }},
      {label: 'Angular.io', icon: 'pi pi-info', url: 'http://angular.io'},
      {separator: true},
      {label: 'Setup', icon: 'pi pi-cog', routerLink: ['/setup']}
    ];
  }

  ngOnInit(): void {
    this.configWidgetChartMonth();    
    this.configWidgetChartYear();
    this.configTableLogIntegration();
  }

  private configWidgetChartYear() : void {
    this.dashboardLogIntegrationService.GetLogIntegrationForChartDynamic(true)
    .subscribe({
      next: (resp) => {
        let labels: any[] = [];
        let listError: any[] = [];
        let listSuccess: any[] = [];

        if(resp.isSuccess && resp.resultObject.length > 0){
          for(let i = 0; i < resp.resultObject.length; i++){
            labels.push(resp.resultObject[i].nuYear);
            listError.push(resp.resultObject[i].nuError);
            listSuccess.push(resp.resultObject[i].nuSuccess);
          }
        }

        this.chartDataLine = {
          labels:  labels,
          datasets: [          
            { 
              label: 'Sucesso',
              data: listSuccess,
              backgroundColor: 'rgba(140, 212, 159,0.4)',
              borderColor: 'rgb(24, 240, 81)',
              pointBackgroundColor: 'rgba(140, 212, 159,1)',
              pointBorderColor: '#fff',
              pointHoverBackgroundColor: '#fff',
              pointHoverBorderColor: 'rgba(140, 212, 159,0.8)',
              fill: 'origin',  
            },    
            { 
              label: 'Erro',
              data: listError,    
              backgroundColor: 'rgba(255,0,0,0.3)',
              borderColor: 'red',
              pointBackgroundColor: 'rgba(148,159,177,1)',
              pointBorderColor: '#fff',
              pointHoverBackgroundColor: '#fff',
              pointHoverBorderColor: 'rgba(148,159,177,0.8)',
              fill: 'origin',
            },
          ]
        };
        this.loadingChartLine = false;
      },
      error: (error) => {
        console.log(error);
        this.errorHandlerService.handle(error);      
        this.loadingChartLine = false;
      }
    });    
  }

  private configWidgetChartMonth() {
    this.dashboardLogIntegrationService.GetLogIntegrationForChartDynamic(false)
    .subscribe({
      next: (resp) => {
        let labels: any[] = [];
        let listError: any[] = [];
        let listSuccess: any[] = [];

        if(resp.isSuccess && resp.resultObject.length > 0){
          for(let i = 0; i < resp.resultObject.length; i++){
            labels.push(resp.resultObject[i].nmMonth);
            listError.push(resp.resultObject[i].nuError);
            listSuccess.push(resp.resultObject[i].nuSuccess);
          }
        }

        this.chartDataBar = {
          labels:  labels,
          datasets: [
            { 
              label: 'Sucesso',
              data: listSuccess,
              backgroundColor: 'rgba(140, 212, 159,0.4)',
              borderColor: 'rgb(24, 240, 81)',
              pointBackgroundColor: 'rgba(140, 212, 159,1)',
              pointBorderColor: '#fff',
              pointHoverBackgroundColor: '#fff',
              pointHoverBorderColor: 'rgba(140, 212, 159,0.8)',
              fill: 'origin',  
            },    
            { 
              label: 'Erro',
              data: listError,    
              backgroundColor: 'rgba(255,0,0,0.3)',
              borderColor: 'red',
              pointBackgroundColor: 'rgba(148,159,177,1)',
              pointBorderColor: '#fff',
              pointHoverBackgroundColor: '#fff',
              pointHoverBorderColor: 'rgba(148,159,177,0.8)',
              fill: 'origin',
            },    
          ]
        };
        this.loadingChartBar = false;
      },
      error: (error) => {
        console.log(error);
        this.errorHandlerService.handle(error);      
        this.loadingChartBar = false;
      }
    });
  }

  private configTableLogIntegration() {
    this.dashboardLogIntegrationService.GetLogIntegration()
    .subscribe({
      next: (resp) => {
        if(resp.isSuccess && resp.resultObject.length > 0){
          this.listLogIntegration = resp.resultObject;
        }     
        this.loadinglistLogIntegration = false; 
      },
      error: (error) => {   
        console.log(error);
        this.errorHandlerService.handle(error);
        this.loadinglistLogIntegration = false; 
      }
    });
  }
}
