namespace IQClientLib.Responses
{
    public class Meter
    {
        public int Id { get; set; }
        public int eid { get; set; }
        public string state { get; set; }
        public string measurementType { get; set; }
        public string phaseMode { get; set; }
        public int phaseCount { get; set; }
        public string meteringStatus { get; set; }
        public List<string> statusFlags { get; set; }
    }
}
