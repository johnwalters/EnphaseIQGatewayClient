namespace IQClientLib.Responses.Consumption
{
    public class ConsumptionDb: Consumption
    {
        public int iqId { get; set; }

        public ConsumptionDb(int id, Consumption consumption)
        {
            this.iqId = id;
            this.createdAt = consumption.createdAt;
            this.reportType = consumption.reportType;
            this.cumulative = consumption.cumulative;
            this.lines = consumption.lines;
        }

    }





    }
