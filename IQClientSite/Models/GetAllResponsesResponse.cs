using IQClientLib.Database.Models;

namespace IQClientSite.Models
{
    public class GetAllResponsesResponse : IQApiResponse
    {
        public List<IQResponse>? Payload { get; set; }
    }
}
