import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';

import { CoreService } from 'src/app/core/core.service';
import { LogIntegrationService } from './log-integration.service';
import { LogIntegrationPersistenceModelComponent } from './components/log-integration-persistence-model/log-integration-persistence-model.component';
import { LogIntegrationGridComponent } from './components/log-integration-grid/log-integration-grid.component';

@NgModule({
  declarations: [
    LogIntegrationPersistenceModelComponent,
    LogIntegrationGridComponent,
  ],
  imports: [
    CommonModule,    

    TableModule,
    DialogModule,
  ],
  providers:[    
    CoreService,
    LogIntegrationService,
  ],
  exports:[
    LogIntegrationPersistenceModelComponent,
    LogIntegrationGridComponent,
  ]
})
export class LogIntegrationModule { }
