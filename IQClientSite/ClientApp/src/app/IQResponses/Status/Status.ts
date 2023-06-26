import { Connection } from "./Connection";
import { Counters } from "./Counters";
import { Meters } from "./Meters";
import { Tasks } from "./Tasks";



export class Status {
  connection: Connection;
  meters: Meters;
  tasks: Tasks;
  counters: Counters;
}
