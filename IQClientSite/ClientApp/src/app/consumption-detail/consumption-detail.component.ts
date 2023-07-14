import { Component, Input, OnInit } from '@angular/core';
import { Consumption } from '../IQResponses/Consumption/Consumption';
import { Helpers } from '../Helpers';

@Component({
  selector: 'app-consumption-detail',
  templateUrl: './consumption-detail.component.html',
  styleUrls: ['./consumption-detail.component.css'],
})
export class ConsumptionDetailComponent implements OnInit {
  @Input() consumptions: Consumption[];
  constructor() {}

  ngOnInit(): void {}

  formatDate(ephochDate: number): string {
    return Helpers.formatDate(ephochDate);
  }
}
