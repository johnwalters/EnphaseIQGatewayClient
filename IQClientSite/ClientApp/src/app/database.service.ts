import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpService } from './HttpService';
import { Observable } from 'rxjs';
import {
  GetAllResponsesResponse,
  GetConsumptionDbResponse,
  GetConsumptionResponse,
  GetInvertersResponse,
  GetMeterReadingsResponse,
  GetMetersResponse,
  GetStatusResponse,
} from './IQResponses/IQApiResponse';
import { ResponseType } from './ResponseType';
import * as moment from 'moment';

@Injectable({
  providedIn: 'root'
})
export class DatabaseService {

  someData: string = '';
  productCalcData: string = '';
  baseUrlWithPath: string;
  httpService: HttpService;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrlWithPath = baseUrl + 'db/';
    this.httpService = new HttpService(http, this.baseUrlWithPath);
  }

  getConsumptionDb(id: number): Observable<GetConsumptionResponse> {
    return this.httpService.get<GetConsumptionDbResponse>(
      'GetConsumptionDb?id=' + id
    );
  }
  getInverterDb(id: number): Observable<GetInvertersResponse> {
    return this.httpService.get<GetInvertersResponse>('getInverterDb?id=' + id);
  }
  getMeterDb(id: number): Observable<GetMetersResponse> {
    return this.httpService.get<GetMetersResponse>('getMeterDb?id=' + id);
  }
  getMeterReadingsDb(id: number): Observable<GetMeterReadingsResponse> {
    return this.httpService.get<GetMeterReadingsResponse>(
      'getMeterReadingDb?id=' + id
    );
  }
  getStatusDb(id: number): Observable<GetStatusResponse> {
    return this.httpService.get<GetStatusResponse>('getStatusDb?id=' + id);
  }

  getConsumptionHistory(
    responseType: ResponseType,
    fromDate: Date,
    toDate: Date
  ): Observable<GetConsumptionResponse> {
    let fromDateFmatted = moment(fromDate).format('YYYY-MM-DD HH:mm:ss');
    let toDateFormatted = moment(toDate).format('YYYY-MM-DD HH:mm:ss');
    const uploadData = new FormData();
    uploadData.append('responseType', responseType.toString());
    uploadData.append('fromDate', fromDateFmatted);
    uploadData.append('toDate', toDateFormatted);

    return this.httpService.post<any>('GetConsumptionHistory', uploadData);
  }

  getHistory(
    fromDate: Date,
    toDate: Date
  ): Observable<GetAllResponsesResponse> {
    let fromDateFmatted = moment(fromDate).format('YYYY-MM-DD HH:mm:ss');
    let toDateFormatted = moment(toDate).format('YYYY-MM-DD HH:mm:ss');
    const uploadData = new FormData();
    uploadData.append('fromDate', fromDateFmatted);
    uploadData.append('toDate', toDateFormatted);

    return this.httpService.post<any>('GetHistory', uploadData);
  }
}
