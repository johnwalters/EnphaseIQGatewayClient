using IQClientLib.Responses.Status;

namespace IQClientSite.Models
{
    public class GetStatusResponse : IQApiResponse
    {
        public Status? Payload { get; set; }
    }
}
