import { Component, OnInit, ViewChild } from '@angular/core';
import { DetailModalComponent } from '../detail-modal/detail-modal.component';
import { IqService } from '../iq.service';
import * as moment from 'moment';
import { IQResponse } from '../IQResponses/IQResponse';
import { ResponseType } from '../ResponseType';
import { DatabaseService } from '../database.service';


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
    private service: DatabaseService,
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


  popupIqDb(id:number, responseType:any): void {
    // TODO: based on response type, get the response from db, and popup response
    switch(responseType){
      case ResponseType.inverters:
        this.popupInverterDb(id);
        break;
      case ResponseType.meters:
        this.popupMeterDb(id);
        break;
      case ResponseType.meterReadings:
        this.popupMeterReadingDb(id);
        break;
      case ResponseType.status:
        this.popupStatusDb(id);
        break;
      case ResponseType.consumption:
        this.popupConsumptionDb(id);
        break;
    }
    // this.spinnerMessage = 'getting history detail';
    // this.errorMessage = '';
    // this.service.getConsumptionDb(id).subscribe((resp) => {
    //   if (resp.isSuccessful) {
    //     this.detailModal.popupConsumption(resp.payload);
    //   } else {
    //     this.errorMessage = 'Request failed. Check Logs.'
    //   }
    //   this.spinnerMessage = '';
    // });
  }

  popupConsumptionDb(id:number): void {
    this.spinnerMessage = 'getting history detail';
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

  popupInverterDb(id:number): void {
    this.spinnerMessage = 'submitting Inverter call';
    this.errorMessage = '';
    this.service.getInverterDb(id).subscribe((resp) => {
      if (resp.isSuccessful) {
        this.detailModal.popupInverters(resp.payload);
      } else {
        this.errorMessage = 'Request failed. Check Logs.'
      }
      this.spinnerMessage = '';
    });
  }

  popupMeterDb(id:number): void {
    this.spinnerMessage = 'submitting Meter call';
    this.errorMessage = '';
    this.service.getMeterDb(id).subscribe((resp) => {
      if (resp.isSuccessful) {
        this.detailModal.popupMeters(resp.payload);
      } else {
        this.errorMessage = 'Request failed. Check Logs.'
      }
      this.spinnerMessage = '';
    });
  }

  popupMeterReadingDb(id:number): void {
    this.spinnerMessage = 'submitting MeterReading call';
    this.errorMessage = '';
    this.service.getMeterReadingsDb(id).subscribe((resp) => {
      if (resp.isSuccessful) {
        this.detailModal.popupMeterReadings(resp.payload);
      } else {
        this.errorMessage = 'Request failed. Check Logs.'
      }
      this.spinnerMessage = '';
    });
  }

  popupStatusDb(id:number): void {
    this.spinnerMessage = 'submitting Status call';
    this.errorMessage = '';
    this.service.getStatusDb(id).subscribe((resp) => {
      if (resp.isSuccessful) {
        this.detailModal.popupStatus(resp.payload);
      } else {
        this.errorMessage = 'Request failed. Check Logs.'
      }
      this.spinnerMessage = '';
    });
  }

  getResponseTypeDescription(responseType:any):string{
    switch(responseType){
      case ResponseType.inverters:
        return "Inverter";
      case ResponseType.meters:
        return "Meter";
      case ResponseType.meterReadings:
        return "Meter Readings";
      case ResponseType.status:
        return "Status";
      case ResponseType.consumption:
        return "Consumption";
    }
    return '';
  }


}
