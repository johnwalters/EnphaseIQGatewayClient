namespace IQClientLib.Responses
{
    public class Inverter
    {
        public double serialNumber { get; set; }
        public int lastReportDate { get; set; }
        public int devType { get; set; }
        public int lastReportWatts { get; set; }
        public int maxReportWatts { get; set; }
    }
}
