import { Component, OnInit } from '@angular/core';
import { IqService } from '../iq.service';
import { Inverter } from '../IQResponses/Inverter';
import { Meter } from '../IQResponses/Meter';
import { MeterReading } from '../IQResponses/MeterReading/MeterReading';
import { Status } from '../IQResponses/Status/Status';
import { Consumption } from '../IQResponses/Consumption/Consumption';
import { ActivatedRoute } from '@angular/router';
import * as moment from 'moment-timezone';
import { ResponseType } from '../ResponseType';

enum RequestType {
  inverters,
  meters,
  meterreadings,
  status,
  consumption,
}

export class ConsumptionModel {
  createdAt: number;
  reportType: string;
  whDlvdCum: number;
  wattHoursDeliveredSincePrevious: number;
  wattHoursDeliveredSinceFirst: number;
}
@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css'],
})
export class DetailComponent implements OnInit {
  rawData = '';
  spinnerMessage: string = '';
  selectedRequestType: RequestType = RequestType.inverters;
  RequestType = RequestType;

  inverters: Inverter[];
  meters: Meter[];
  meterReadings: MeterReading[];
  status: Status;
  consumptions: Consumption[];
  consumptionHistoryModelList: ConsumptionModel[];
  isRawDataShowing = false;

  private sub: any;
  actionString: string;
  errorMessage = '';

  constructor(
    private service: IqService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.sub = this.activatedRoute.params.subscribe((params) => {
      this.actionString = params['action'];
      if (this.actionString) {
        // this.requestType = this.actionString as unknown as RequestType;
        this.selectedRequestType = (<any>RequestType)[this.actionString];
        this.rawData = '';
      }
    });
  }

  getInverters(): void {
    this.spinnerMessage = 'submitting getInverters call';
    this.errorMessage = '';
    this.service.getInverters().subscribe((resp) => {
      this.rawData = JSON.stringify(resp);
      if (resp.isSuccessful) {
        this.inverters = resp.payload;
        console.log(this.inverters);
      } else {
        this.errorMessage = 'Request failed. Check Logs.'
      }
      this.spinnerMessage = '';
    });
  }

  getMeters(): void {
    this.spinnerMessage = 'submitting getMeters call';
    this.errorMessage = '';
    this.service.getMeters().subscribe((resp) => {
      this.rawData = JSON.stringify(resp);
      if (resp.isSuccessful) {
        this.meters = resp.payload;
        console.debug(this.meters);
      } else {
        this.errorMessage = 'Request failed. Check Logs.'
      }
      this.spinnerMessage = '';
    });
  }

  getMeterReadings(): void {
    this.spinnerMessage = 'submitting getMeterReadings call';
    this.errorMessage = '';
    this.service.getMeterReadings().subscribe((resp) => {
      this.rawData = JSON.stringify(resp);
      if (resp.isSuccessful) {
        this.meterReadings = resp.payload;
        console.debug(this.meterReadings);
      } else {
        this.errorMessage = 'Request failed. Check Logs.'
      }
      this.spinnerMessage = '';
    });
  }

  getStatus(): void {
    this.spinnerMessage = 'submitting getStatus call';
    this.errorMessage = '';
    this.service.getStatus().subscribe((resp) => {
      this.rawData = JSON.stringify(resp);
      if (resp.isSuccessful) {
        this.status = resp.payload;
        console.debug(this.status);
      } else {
        this.errorMessage = 'Request failed. Check Logs.'
      }
      this.spinnerMessage = '';
    });
  }

  getConsumption(): void {
    this.spinnerMessage = 'submitting getConsumption call';
    this.errorMessage = '';
    this.service.getConsumption().subscribe((resp) => {
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

  getConsumptionHistory(): void {
    this.spinnerMessage = 'submitting getHistory call';
    this.errorMessage = '';
    let fromDate = moment("20230101").toDate();
    let toDate = moment("20231231").toDate();
    this.service.getConsumptionHistory(ResponseType.consumption, fromDate, toDate).subscribe((resp) => {
      this.rawData = JSON.stringify(resp);
      if (resp.isSuccessful) {
        this.buildConsumptionHistoryModelList(resp.payload);
      } else {
        this.errorMessage = 'Request failed. Check Logs.'
      }
      this.spinnerMessage = '';
    });
  }

  buildConsumptionHistoryModelList(consumptionList:Array<Consumption>):void{
    this.consumptionHistoryModelList = new Array<ConsumptionModel>;
    let firstWattHoursDelivered = 0;
    let previousWattHoursDelivered = 0;
    for(let item of consumptionList){
      let consumptionModel = new ConsumptionModel();
      consumptionModel.createdAt = item.createdAt;
      consumptionModel.reportType = item.reportType;
      consumptionModel.whDlvdCum = item.cumulative.whDlvdCum;
      if(firstWattHoursDelivered){
        consumptionModel.wattHoursDeliveredSinceFirst = consumptionModel.whDlvdCum - firstWattHoursDelivered;
      } else {
        consumptionModel.wattHoursDeliveredSinceFirst = 0;
        firstWattHoursDelivered = consumptionModel.whDlvdCum;
      }

      if(previousWattHoursDelivered){
        consumptionModel.wattHoursDeliveredSincePrevious = consumptionModel.whDlvdCum - previousWattHoursDelivered;
      } else {
        consumptionModel.wattHoursDeliveredSincePrevious = 0;

      }
      previousWattHoursDelivered = consumptionModel.whDlvdCum;

      this.consumptionHistoryModelList.push(consumptionModel);
    }
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
