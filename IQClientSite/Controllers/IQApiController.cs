using IQGatewayClientLib.Responses;
using IQGatewayClientLib.Responses.Consumption;
using IQGatewayClientLib.Responses.MeterReading;
using IQGatewayClientLib.Responses.Status;
using Microsoft.AspNetCore.Mvc;

namespace IQGatewayClientSite.Controllers
{
    public class IQApiController : Controller
    {
        private IQGatewayClientLib.Client _iqClient;
        private IConfiguration _configuration;

        public IQApiController(IConfiguration configuration)
        {
            var token = configuration["Token"];
            
            _iqClient = new IQGatewayClientLib.Client(token);
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


}
