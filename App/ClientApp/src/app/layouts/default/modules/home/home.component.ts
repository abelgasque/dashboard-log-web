import { Component, OnInit } from '@angular/core';
import { CoreService } from 'src/app/core/core.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public linkSwagger: string;

  constructor(private coreService: CoreService) { 
    this.linkSwagger =`${ this.coreService.getBaseUrl() }/swagger/index.html`; 
  }

  ngOnInit() {
  }

}
