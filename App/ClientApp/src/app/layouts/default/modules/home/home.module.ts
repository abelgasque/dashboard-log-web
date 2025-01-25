import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CoreService } from 'src/app/core/core.service';
import { HomeComponent } from './home.component';

@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule
  ],
  providers:[
    CoreService
  ]
})
export class HomeModule { }
