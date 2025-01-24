import { Component, OnInit } from '@angular/core';

import { NavbarService } from 'src/app/shared/components/navbar/navbar.service';

@Component({
  selector: 'app-core',
  templateUrl: './core.component.html',
  styleUrls: ['./core.component.css']
})
export class CoreComponent implements OnInit {

  constructor(public navbarService: NavbarService) { }

  ngOnInit(): void {
  }

}
