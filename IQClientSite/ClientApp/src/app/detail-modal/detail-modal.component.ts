import { Component, OnInit } from '@angular/core';
import { IqService } from '../iq.service';
import { Inverter } from '../IQResponses/Inverter';
import { Meter } from '../IQResponses/Meter';
import { MeterReading } from '../IQResponses/MeterReading/MeterReading';
import { Status } from '../IQResponses/Status/Status';
import { Consumption } from '../IQResponses/Consumption/Consumption';
import { ActivatedRoute } from '@angular/router';
import * as moment from 'moment-timezone';
import { RequestType } from '../detail/RequestType';


@Component({
  selector: 'app-detail-modal',
  templateUrl: './detail-modal.component.html',
  styleUrls: ['./detail-modal.component.css']
})
export class DetailModalComponent implements OnInit {
  display: string;
  isOpen = false;

    // @Input() marketName: string;
  // @Input() notes: string;
  // @Output() notesChanged = new EventEmitter<any>();
  selectedRequestType: RequestType;
  RequestType = RequestType;

  inverters: Inverter[];
  meters: Meter[];
  meterReadings: MeterReading[];
  status: Status;
  consumptions: Consumption[];

  constructor() { }


  ngOnInit() {
    this.display = 'none';
  }

  clearAll():void{
    this.inverters = new Array<Inverter>();
    this.meters = new Array<Meter>();
    this.meterReadings = new Array<MeterReading>();
    this.status = new Status();
    this.consumptions = new Array<Consumption>();
    this.selectedRequestType = RequestType.status;
  }

  openModal() {
    this.isOpen = true;
    this.display = 'block';
  }

  onCloseHandled() {
    this.isOpen = false;
    this.display = 'none';
  }

  popupInverters(inverters:Inverter[] ){
    this.clearAll();
    this.inverters = inverters;
    this.selectedRequestType = RequestType.inverters;
    this.openModal();
  }

  popupConsumption(consumptions:Consumption[] ){
    this.clearAll();
    this.selectedRequestType = RequestType.consumption;
    this.consumptions = consumptions;
    this.openModal();
  }

  // onNotesChanged() {
  //   const notesChangedMessage = {
  //     marketName: this.marketName,
  //     notes: this.notes
  //   };
  //   this.notesChanged.emit(notesChangedMessage);
  //   this.onCloseHandled();
  // }

}
