using IQClientLib.Responses;

namespace IQClientSite.Models
{
    public class GetMetersResponse : IQApiResponse
    {
        public List<Meter>? Payload { get; set; }
    }
}
