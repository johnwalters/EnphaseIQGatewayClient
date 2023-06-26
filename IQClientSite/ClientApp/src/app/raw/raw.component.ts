import { Component, OnInit } from '@angular/core';
import { IqService } from '../iq.service';
import { Inverter } from '../IQResponses/Inverter';
import { Meter } from '../IQResponses/Meter';
import { MeterReading } from '../IQResponses/MeterReading/MeterReading';
import { Status } from '../IQResponses/Status/Status';
import { Consumption } from '../IQResponses/Consumption/Consumption';

@Component({
  selector: 'app-raw',
  templateUrl: './raw.component.html',
  styleUrls: ['./raw.component.css']
})
export class RawComponent implements OnInit {

  rawData = "";
  spinnerMessage: string = "";

  inverter:Inverter[];
  meters:Meter[];
  meterReadings:MeterReading[];
  status:Status;
  consumptions:Consumption[];

  constructor(
    private service: IqService,
  ) {}

  ngOnInit(): void {

  }

  getInverters(): void {
    this.spinnerMessage = 'submitting getInverters call';
    this.service.getInverters().subscribe((resp) => {
      this.rawData = JSON.stringify(resp);
      if(resp.isSuccessful){
        this.inverter = resp.payload;
        console.log(this.inverter);
      }
      this.spinnerMessage = '';
    });
  }

  getMeters(): void {
    this.spinnerMessage = 'submitting getMeters call';
    this.service.getMeters().subscribe((resp) => {
      this.rawData = JSON.stringify(resp);
      if(resp.isSuccessful){
        this.meters = resp.payload;
        console.debug(this.meters);
      }
      this.spinnerMessage = '';
    });
  }

  getMeterReadings(): void {
    this.spinnerMessage = 'submitting getMeterReadings call';
    this.service.getMeterReadings().subscribe((resp) => {
      this.rawData = JSON.stringify(resp);
      if(resp.isSuccessful){
        this.meterReadings = resp.payload;
        console.debug(this.meterReadings);
      }
      this.spinnerMessage = '';
    });
  }

  getStatus(): void {
    this.spinnerMessage = 'submitting getStatus call';
    this.service.getStatus().subscribe((resp) => {
      this.rawData = JSON.stringify(resp);
      if(resp.isSuccessful){
        this.status = resp.payload;
        console.debug(this.status);
      }
      this.spinnerMessage = '';
    });
  }

  getConsumption(): void {
    this.spinnerMessage = 'submitting getConsumption call';
    this.service.getConsumption().subscribe((resp) => {
      this.rawData = JSON.stringify(resp);
      if(resp.isSuccessful){
        this.consumptions = resp.payload;
        console.debug(this.consumptions);
      }
      this.spinnerMessage = '';
    });
  }

}
