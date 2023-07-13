namespace IQClientLib.Responses.Consumption
{
    public class Consumption 
    {
        public int Id { get; set; }
        public int createdAt { get; set; }
        public string reportType { get; set; }
        public Cumulative cumulative { get; set; }
        public List<Line> lines { get; set; }
    }

    }
