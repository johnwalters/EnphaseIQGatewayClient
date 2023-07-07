import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpService } from './HttpService';
import { Observable } from 'rxjs';
import { GetAllResponsesResponse, GetConsumptionResponse, GetInvertersResponse, GetMeterReadingsResponse, GetMetersResponse, GetStatusResponse } from './IQResponses/IQApiResponse';
import { ResponseType } from './ResponseType';



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

  getHistory(responseType:ResponseType, fromDate:Date, toDate:Date): Observable<GetAllResponsesResponse> {
    const uploadData = new FormData();
    uploadData.append('responseType', responseType.toString() )
    uploadData.append('fromDate', fromDate.toString() )
    uploadData.append('toDate', toDate.toString() )

    return this.httpService.post<any>('GetHistory', uploadData);
  }
}
