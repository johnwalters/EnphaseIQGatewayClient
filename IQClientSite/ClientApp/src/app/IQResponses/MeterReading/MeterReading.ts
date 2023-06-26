import { Channel } from "./Channel";



export class MeterReading {
  eid: number;
  timestamp: number;
  actEnergyDlvd: number;
  actEnergyRcvd: number;
  apparentEnergy: number;
  reactEnergyLagg: number;
  reactEnergyLead: number;
  instantaneousDemand: number;
  activePower: number;
  apparentPower: number;
  reactivePower: number;
  pwrFactor: number;
  voltage: number;
  current: number;
  freq: number;
  channels: Channel[];
}
