using IQClientLib.Responses.MeterReading;

namespace IQClientSite.Models
{
    public class GetMeterReadingsResponse : IQApiResponse
    {
        public List<MeterReading>? Payload { get; set; }
    }
}
