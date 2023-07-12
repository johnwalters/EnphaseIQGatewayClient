import { Component, OnInit } from '@angular/core';
import { IqService } from '../iq.service';
import { Inverter } from '../IQResponses/Inverter';
import { Meter } from '../IQResponses/Meter';
import { MeterReading } from '../IQResponses/MeterReading/MeterReading';
import { Status } from '../IQResponses/Status/Status';
import { Consumption } from '../IQResponses/Consumption/Consumption';
import { ActivatedRoute } from '@angular/router';
import * as moment from 'moment-timezone';


enum RequestType {
  inverters,
  meters,
  meterreadings,
  status,
  consumption,
}


@Component({
  selector: 'app-db-detail',
  templateUrl: './db-detail.component.html',
  styleUrls: ['./db-detail.component.css']
})
export class DbDetailComponent implements OnInit {
  rawData = '';
  spinnerMessage: string = '';
  selectedRequestType: RequestType = RequestType.consumption;
  RequestType = RequestType;

  inverters: Inverter[];
  meters: Meter[];
  meterReadings: MeterReading[];
  status: Status;
  consumptions: Consumption[];
  isRawDataShowing = false;

  private sub: any;
  actionString: string;
  errorMessage = '';
  id:number = 0;

  constructor(
    private service: IqService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.sub = this.activatedRoute.params.subscribe((params) => {
      this.id = params['id'];
      if(this.id){
        this.getConsumptionDb();
      }

    });
  }

  // getInverters(): void {
  //   this.spinnerMessage = 'submitting getInverters call';
  //   this.errorMessage = '';
  //   this.service.getInverters().subscribe((resp) => {
  //     this.rawData = JSON.stringify(resp);
  //     if (resp.isSuccessful) {
  //       this.inverters = resp.payload;
  //       console.log(this.inverters);
  //     } else {
  //       this.errorMessage = 'Request failed. Check Logs.'
  //     }
  //     this.spinnerMessage = '';
  //   });
  // }

  // getMeters(): void {
  //   this.spinnerMessage = 'submitting getMeters call';
  //   this.errorMessage = '';
  //   this.service.getMeters().subscribe((resp) => {
  //     this.rawData = JSON.stringify(resp);
  //     if (resp.isSuccessful) {
  //       this.meters = resp.payload;
  //       console.debug(this.meters);
  //     } else {
  //       this.errorMessage = 'Request failed. Check Logs.'
  //     }
  //     this.spinnerMessage = '';
  //   });
  // }

  // getMeterReadings(): void {
  //   this.spinnerMessage = 'submitting getMeterReadings call';
  //   this.errorMessage = '';
  //   this.service.getMeterReadings().subscribe((resp) => {
  //     this.rawData = JSON.stringify(resp);
  //     if (resp.isSuccessful) {
  //       this.meterReadings = resp.payload;
  //       console.debug(this.meterReadings);
  //     } else {
  //       this.errorMessage = 'Request failed. Check Logs.'
  //     }
  //     this.spinnerMessage = '';
  //   });
  // }

  // getStatus(): void {
  //   this.spinnerMessage = 'submitting getStatus call';
  //   this.errorMessage = '';
  //   this.service.getStatus().subscribe((resp) => {
  //     this.rawData = JSON.stringify(resp);
  //     if (resp.isSuccessful) {
  //       this.status = resp.payload;
  //       console.debug(this.status);
  //     } else {
  //       this.errorMessage = 'Request failed. Check Logs.'
  //     }
  //     this.spinnerMessage = '';
  //   });
  // }

  getConsumptionDb(): void {
    this.spinnerMessage = 'submitting getConsumption call';
    this.errorMessage = '';
    this.service.getConsumptionDb(this.id).subscribe((resp) => {
      this.rawData = JSON.stringify(resp);
      if (resp.isSuccessful) {
        this.consumptions = resp.payload;
        console.debug(this.consumptions);
      } else {
        this.errorMessage = 'Request failed. Check Logs.'
      }
      this.spinnerMessage = '';
    });
  }




  getStatusFlags(statusFlags: string[]): string {
    let concatedFlags = '';
    for (let flag of statusFlags) {
      concatedFlags += ' ' + flag;
    }
    return concatedFlags;
  }

  formatDate(ephochDate: number): string {
    return moment(new Date(ephochDate * 1000)).format('MM-DD-YY h:mm:ss A');
  }


}
