import { Component, Input, OnInit } from '@angular/core';
import { Status } from '../IQResponses/Status/Status';
import { Helpers } from '../Helpers';

@Component({
  selector: 'app-status-detail',
  templateUrl: './status-detail.component.html',
  styleUrls: ['./status-detail.component.css']
})
export class StatusDetailComponent implements OnInit {

  @Input() status: Status;
  constructor() { }

  ngOnInit(): void {
  }

  formatDate(ephochDate: number): string {
    return Helpers.formatDate(ephochDate);
  }

}
