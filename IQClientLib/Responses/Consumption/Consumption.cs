namespace IQClientLib.Responses.Consumption
{
    public class Consumption
    {
        public int createdAt { get; set; }
        public string reportType { get; set; }
        public Cumulative cumulative { get; set; }
        public List<Line> lines { get; set; }
    }





}
