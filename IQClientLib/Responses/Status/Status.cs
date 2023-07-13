namespace IQClientLib.Responses.Status
{

    public class Status 
    {
        public int Id { get; set; }
        public Connection connection { get; set; }
        public Meters meters { get; set; }
        public Tasks tasks { get; set; }
        public Counters counters { get; set; }
    }

}
