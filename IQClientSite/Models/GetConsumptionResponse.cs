using IQClientLib.Responses.Consumption;

namespace IQClientSite.Models
{
    public class GetConsumptionResponse : IQApiResponse
    {
        public List<Consumption>? Payload { get; set; }
    }
}
