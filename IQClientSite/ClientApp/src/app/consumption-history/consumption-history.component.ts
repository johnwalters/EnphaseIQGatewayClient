import { Component, OnInit } from '@angular/core';
import { IqService } from '../iq.service';
import { Consumption, ConsumptionDb } from '../IQResponses/Consumption/Consumption';
import { ActivatedRoute } from '@angular/router';
import * as moment from 'moment-timezone';
import { ResponseType } from '../ResponseType';

export class ConsumptionModel {
  iqId: number;
  createdAt: number;
  reportType: string;
  whDlvdCum: number;
  wattHoursDeliveredSincePrevious: number;
  wattHoursDeliveredSinceFirst: number;
}

@Component({
  selector: 'app-consumption-history',
  templateUrl: './consumption-history.component.html',
  styleUrls: ['./consumption-history.component.css']
})
export class ConsumptionHistoryComponent implements OnInit {
  rawData = '';
  spinnerMessage: string = '';



  consumptionHistoryModelList: ConsumptionModel[];

  errorMessage = '';

  constructor(
    private service: IqService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getConsumptionHistory();
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
      consumptionModel.iqId = item.id;
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

  formatDate(ephochDate: number): string {
    return moment(new Date(ephochDate * 1000)).format('MM-DD-YY h:mm:ss A');
  }
}
