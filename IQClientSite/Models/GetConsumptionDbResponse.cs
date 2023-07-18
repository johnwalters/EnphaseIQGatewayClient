using IQClientLib.Responses.Consumption;

namespace IQClientSite.Models
{
    public class GetConsumptionDbResponse : IQApiResponse
    {
        public List<ConsumptionDb>? Payload { get; set; }
    }
}
