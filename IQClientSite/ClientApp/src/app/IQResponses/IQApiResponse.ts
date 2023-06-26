import { Consumption } from "./Consumption/Consumption";
import { Inverter } from "./Inverter";
import { Meter } from "./Meter";
import { MeterReading } from "./MeterReading/MeterReading";
import { Status } from "./Status/Status";

export class IQApiResponse {
    isSuccessful: boolean;
}

export class GetInvertersResponse extends IQApiResponse {
  payload:Inverter[];
}

export class GetMetersResponse extends IQApiResponse {
  payload:Meter[];
}

export class GetMeterReadingsResponse extends IQApiResponse {
  payload:MeterReading[];
}

export class GetStatusResponse extends IQApiResponse {
  payload:Status;
}

export class GetConsumptionResponse extends IQApiResponse {
  payload:Consumption[];
}
