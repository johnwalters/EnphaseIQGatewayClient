import { Cumulative } from "./Cumulative";
import { Line } from "./Line";



export class Consumption {
  id:number;
  createdAt: number;
  reportType: string;
  cumulative: Cumulative;
  lines: Line[];
}

export class ConsumptionDb extends Consumption {
  iqId: number;
}


