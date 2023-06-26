import { Cumulative } from "./Cumulative";
import { Line } from "./Line";



export class Consumption {
  createdAt: number;
  reportType: string;
  cumulative: Cumulative;
  lines: Line[];
}
