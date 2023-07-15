import { Component, Input, OnInit } from '@angular/core';
import { MeterReading } from '../IQResponses/MeterReading/MeterReading';
import { Helpers } from '../Helpers';

@Component({
  selector: 'app-meter-readings-detail',
  templateUrl: './meter-readings-detail.component.html',
  styleUrls: ['./meter-readings-detail.component.css']
})
export class MeterReadingsDetailComponent implements OnInit {

  @Input() meterReadings: MeterReading[];

  constructor() { }

  ngOnInit(): void {
  }

  formatDate(ephochDate: number): string {
    return Helpers.formatDate(ephochDate);
  }

}
