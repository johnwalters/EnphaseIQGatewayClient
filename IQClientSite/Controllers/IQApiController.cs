using IQClientLib.Database.Models;
using IQClientLib.Responses;
using IQClientLib.Responses.Consumption;
using IQClientLib.Responses.MeterReading;
using IQClientLib.Responses.Status;
using Microsoft.AspNetCore.Mvc;

namespace IQClientSite.Controllers
{
    public class IQApiController : Controller
    {
        private IQClientLib.Client _iqClient;
        private readonly string UNSET_TOKEN = "GETaTOKEN";

        public IQApiController(IConfiguration configuration)
        {
            var token = configuration["Token"];
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            if (string.IsNullOrEmpty(token) || token == UNSET_TOKEN)
            {
                throw new ApplicationException($"Authentication token not set in appsettings file(s)");
            }
            _iqClient = new IQClientLib.Client(token, connectionString);
        }
        public async Task<IActionResult> GetInverters()
        {
            try
            {
                var iqResponse = await _iqClient.GetInverters();
                var response = new GetInvertersResponse() { IsSuccessful = true, Payload = iqResponse };
                response.IsSuccessful = iqResponse != null;
                var jsonResponse = Json(response);
                return jsonResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"exception occurred in IQApiController.GetInverters() - {ex}");
                var response = Json(new IQApiResponse() { IsSuccessful = false });
                return response;
            }

        }

        public async Task<IActionResult> GetMeters()
        {
            try
            {
                var iqResponse = await _iqClient.GetMeters();
                var response = new GetMetersResponse() { IsSuccessful = true, Payload = iqResponse };
                response.IsSuccessful = iqResponse != null;
                var jsonResponse = Json(response);
                return jsonResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"exception occurred in IQApiController.GetMeters() - {ex}");
                var response = Json(new IQApiResponse() { IsSuccessful = false });
                return response;
            }
        }

        public async Task<IActionResult> GetMeterReadings()
        {
            try
            {
                var iqResponse = await _iqClient.GetMeterReadings();
                var response = new GetMeterReadingsResponse() { IsSuccessful = true, Payload = iqResponse };
                response.IsSuccessful = iqResponse != null;
                var jsonResponse = Json(response);
                return jsonResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"exception occurred in IQApiController.GetMeterReadings() - {ex}");
                var response = Json(new IQApiResponse() { IsSuccessful = false });
                return response;
            }
        }

        public async Task<IActionResult> GetStatus()
        {
            try
            {
                var iqResponse = await _iqClient.GetStatus();
                var response = new GetStatusResponse() { IsSuccessful = true, Payload = iqResponse };
                response.IsSuccessful = iqResponse != null;
                var jsonResponse = Json(response);
                return jsonResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"exception occurred in IQApiController.GetStatus() - {ex}");
                var response = Json(new IQApiResponse() { IsSuccessful = false });
                return response;
            }
        }

        public async Task<IActionResult> GetConsumption()
        {
            try
            {
                var iqResponse = await _iqClient.GetConsumption();
                var response = new GetConsumptionResponse() { IsSuccessful = true, Payload = iqResponse };
                response.IsSuccessful = iqResponse != null;
                var jsonResponse = Json(response);
                return jsonResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"exception occurred in IQApiController.GetConsumption() - {ex}");
                var response = Json(new IQApiResponse() { IsSuccessful = false });
                return response;
            }
        }

        public async Task<IActionResult> GetHistory(ResponseType? responseType, string fromDate, string toDate )
        {
            try
            {
                DateTime.TryParse(fromDate, out DateTime from);
                DateTime.TryParse(toDate, out DateTime to);
                var iqResponses = await _iqClient.GetAllResponses(responseType, from, to);
                // filter to just total_consumption report types
                var consumptionList = new List<Consumption>();
                foreach(var iq in iqResponses)
                {
                    var iqConsumptions = iq.ToConsumptions();
                    foreach(var iqC in iqConsumptions)
                    {
                        if(iqC.reportType == "total-consumption")
                        {
                            consumptionList.Add(iqC);
                        }
                    }
                }

                var response = new GetConsumptionResponse() { IsSuccessful = true, Payload = consumptionList };
                response.IsSuccessful = iqResponses != null;
                var jsonResponse = Json(response);
                return jsonResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"exception occurred in IQApiController.GetHistory() - {ex}");
                var response = Json(new GetConsumptionResponse() { IsSuccessful = false });
                return response;
            }
        }
    }

    public class IQApiResponse
    {
        public bool IsSuccessful { get; set; }
    }

    public class GetInvertersResponse : IQApiResponse
    {
        public List<Inverter>? Payload { get; set; }
    }
    public class GetMetersResponse : IQApiResponse
    {
        public List<Meter>? Payload { get; set; }
    }

    public class GetMeterReadingsResponse : IQApiResponse
    {
        public List<MeterReading>? Payload { get; set; }
    }

    public class GetStatusResponse : IQApiResponse
    {
        public Status? Payload { get; set; }
    }

    public class GetConsumptionResponse : IQApiResponse
    {
        public List<Consumption>? Payload { get; set; }
    }

    public class GetAllResponsesResponse : IQApiResponse
    {
        public List<IQResponse>? Payload { get; set; }
    }


}
