export class IQResponse {
  id: number;
  responseType: ResponseType;
  inverterLastReportDate: Date | null;
  meterReadingTimestamp: Date | null;
  metersLastUpdate: Date | null;
  consumptionReportCreatedAt: Date | null;
  jsonData: string;
}
