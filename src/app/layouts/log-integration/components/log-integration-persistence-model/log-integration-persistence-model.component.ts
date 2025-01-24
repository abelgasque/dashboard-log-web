import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-log-integration-persistence-model',
  templateUrl: './log-integration-persistence-model.component.html',
  styleUrls: ['./log-integration-persistence-model.component.css']
})
export class LogIntegrationPersistenceModelComponent implements OnInit {

  @Input() title: string = null;
  @Input() isOpen: boolean = false;
  @Input() data: any = null;

  constructor() { }

  ngOnInit(): void {
  }

}
