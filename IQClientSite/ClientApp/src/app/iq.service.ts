import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpService } from './HttpService';
import { Observable } from 'rxjs';
import { GetAllResponsesResponse, GetConsumptionDbResponse, GetConsumptionResponse, GetInvertersResponse, GetMeterReadingsResponse, GetMetersResponse, GetStatusResponse } from './IQResponses/IQApiResponse';
import { ResponseType } from './ResponseType';
import * as moment from 'moment';



@Injectable({
  providedIn: 'root'
})
export class IqService {

  someData:string = '';
  productCalcData:string = '';
  baseUrlWithPath:string;
  httpService: HttpService;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    ) {
    this.baseUrlWithPath = baseUrl + 'iqapi/';
    this.httpService = new HttpService(http, this.baseUrlWithPath);
  }

  getInverters(): Observable<GetInvertersResponse> {
    return this.httpService.get<GetInvertersResponse>('getInverters');
  }

  getMeters(): Observable<GetMetersResponse> {
    return this.httpService.get<GetMetersResponse>('getMeters');
  }

  getMeterReadings(): Observable<GetMeterReadingsResponse> {
    return this.httpService.get<GetMeterReadingsResponse>('GetMeterReadings');
  }

  getStatus(): Observable<GetStatusResponse> {
    return this.httpService.get<GetStatusResponse>('GetStatus');
  }

  getConsumption(): Observable<GetConsumptionResponse> {
    return this.httpService.get<GetConsumptionResponse>('GetConsumption');
  }

  getConsumptionDb(id:number): Observable<GetConsumptionResponse> {
    return this.httpService.get<GetConsumptionDbResponse>('GetConsumptionDb?id=' + id);
  }
  getInverterDb(id:number): Observable<GetInvertersResponse> {
    return this.httpService.get<GetInvertersResponse>('getInverterDb?id=' + id);
  }


  getConsumptionHistory(responseType:ResponseType, fromDate:Date, toDate:Date): Observable<GetConsumptionResponse> {
    let fromDateFmatted = moment(fromDate).format('YYYY-MM-DD HH:mm:ss');
    let toDateFormatted = moment(toDate).format('YYYY-MM-DD HH:mm:ss');
    const uploadData = new FormData();
    uploadData.append('responseType', responseType.toString() )
    uploadData.append('fromDate', fromDateFmatted )
    uploadData.append('toDate', toDateFormatted )

    return this.httpService.post<any>('GetConsumptionHistory', uploadData);
  }

  getHistory(fromDate:Date, toDate:Date): Observable<GetAllResponsesResponse> {
    let fromDateFmatted = moment(fromDate).format('YYYY-MM-DD HH:mm:ss');
    let toDateFormatted = moment(toDate).format('YYYY-MM-DD HH:mm:ss');
    const uploadData = new FormData();
    uploadData.append('fromDate', fromDateFmatted )
    uploadData.append('toDate', toDateFormatted )

    return this.httpService.post<any>('GetHistory', uploadData);
  }

}
