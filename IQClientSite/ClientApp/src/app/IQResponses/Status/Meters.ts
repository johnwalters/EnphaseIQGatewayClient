import { Generator } from "./Generator";
import { Grid } from "./Grid";
import { Load } from "./Load";
import { Storage } from "./Storage";
import { Pv } from "./Pv";



export class Meters {
  last_update: number;
  soc: number;
  main_relay_state: number;
  gen_relay_state: number;
  backup_bat_mode: number;
  backup_soc: number;
  is_split_phase: number;
  phase_count: number;
  enc_agg_soc: number;
  enc_agg_energy: number;
  acb_agg_soc: number;
  acb_agg_energy: number;
  pv: Pv;
  storage: Storage;
  grid: Grid;
  load: Load;
  generator: Generator;
}
