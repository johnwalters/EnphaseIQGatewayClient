using IQClientLib.Database.Models;

namespace IQClientLib.Responses.Status
{
    public class Meters 
    {
        public int Id { get; set; }
        public int last_update { get; set; }
        public int soc { get; set; }
        public int main_relay_state { get; set; }
        public int gen_relay_state { get; set; }
        public int backup_bat_mode { get; set; }
        public int backup_soc { get; set; }
        public int is_split_phase { get; set; }
        public int phase_count { get; set; }
        public int enc_agg_soc { get; set; }
        public int enc_agg_energy { get; set; }
        public int acb_agg_soc { get; set; }
        public int acb_agg_energy { get; set; }
        public Pv pv { get; set; }
        public Storage storage { get; set; }
        public Grid grid { get; set; }
        public Load load { get; set; }
        public Generator generator { get; set; }
    }




}
