import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { HomeModule } from 'src/app/layouts/default/modules/home/home.module';

import { NavbarService } from 'src/app/shared/components/navbar/navbar.service';

import { DefaultComponent } from 'src/app/layouts/default/default.component';

@NgModule({
  declarations: [
    DefaultComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
     
    HomeModule,
  ],
  providers:[
    NavbarService
  ]
})
export class DefaultModule { }
