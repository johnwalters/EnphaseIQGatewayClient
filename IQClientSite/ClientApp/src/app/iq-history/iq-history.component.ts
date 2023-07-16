import { Component, OnInit, ViewChild } from '@angular/core';
import { DetailModalComponent } from '../detail-modal/detail-modal.component';
import { IqService } from '../iq.service';
import * as moment from 'moment';
import { IQResponse } from '../IQResponses/IQResponse';
import { ResponseType } from '../ResponseType';

export class iqModel extends IQResponse {

}

@Component({
  selector: 'app-iq-history',
  templateUrl: './iq-history.component.html',
  styleUrls: ['./iq-history.component.css']
})
export class IqHistoryComponent implements OnInit {

  @ViewChild(DetailModalComponent)
  private detailModal: DetailModalComponent;
  rawData = '';
  spinnerMessage: string = '';



  iqHistoryModelList: IQResponse[];

  errorMessage = '';

  constructor(
    private service: IqService,
  ) {}

  ngOnInit(): void {
    this.getHistory();
  }

  getHistory(): void {
    this.spinnerMessage = 'submitting getHistory call';
    this.errorMessage = '';
    let fromDate = moment("20230101").toDate();
    let toDate = moment("20231231").toDate();
    this.service.getHistory(fromDate, toDate).subscribe((resp) => {
      this.rawData = JSON.stringify(resp);
      if (resp.isSuccessful) {
        this.iqHistoryModelList = resp.payload;
      } else {
        this.errorMessage = 'Request failed. Check Logs.'
      }
      this.spinnerMessage = '';
    });
  }

  // buildIqModelList(consumptionList:Array<IQResponse>):void{
  //   this.consumptionHistoryModelList = new Array<ConsumptionModel>;
  //   let firstWattHoursDelivered = 0;
  //   let previousWattHoursDelivered = 0;
  //   for(let item of consumptionList){
  //     let consumptionModel = new ConsumptionModel();
  //     consumptionModel.iqId = item.id;
  //     consumptionModel.createdAt = item.createdAt;
  //     consumptionModel.reportType = item.reportType;
  //     consumptionModel.whDlvdCum = item.cumulative.whDlvdCum;
  //     if(firstWattHoursDelivered){
  //       consumptionModel.wattHoursDeliveredSinceFirst = consumptionModel.whDlvdCum - firstWattHoursDelivered;
  //     } else {
  //       consumptionModel.wattHoursDeliveredSinceFirst = 0;
  //       firstWattHoursDelivered = consumptionModel.whDlvdCum;
  //     }

  //     if(previousWattHoursDelivered){
  //       consumptionModel.wattHoursDeliveredSincePrevious = consumptionModel.whDlvdCum - previousWattHoursDelivered;
  //     } else {
  //       consumptionModel.wattHoursDeliveredSincePrevious = 0;

  //     }
  //     previousWattHoursDelivered = consumptionModel.whDlvdCum;

  //     this.consumptionHistoryModelList.push(consumptionModel);
  //   }

  popupIqDb(id:number, responseType:ResponseType): void {
    // TODO: based on response type, get the response from db, and popup response
    this.spinnerMessage = 'submitting getConsumption call';
    this.errorMessage = '';
    this.service.getConsumptionDb(id).subscribe((resp) => {
      if (resp.isSuccessful) {
        this.detailModal.popupConsumption(resp.payload);
      } else {
        this.errorMessage = 'Request failed. Check Logs.'
      }
      this.spinnerMessage = '';
    });
  }

  // formatDate(ephochDate: number): string {
  //   return moment(new Date(ephochDate * 1000)).format('MM-DD-YY h:mm:ss A');
  // }

}
