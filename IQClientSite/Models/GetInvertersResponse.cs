using IQClientLib.Responses;

namespace IQClientSite.Models
{
    public class GetInvertersResponse : IQApiResponse
    {
        public List<Inverter>? Payload { get; set; }
    }
}
