using IQClientLib;
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
        private IQService _service;
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
            _service = new IQService(connectionString);
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

        public async Task<IActionResult> GetConsumptionHistory(string fromDate, string toDate )
        {
            try
            {
                DateTime.TryParse(fromDate, out DateTime from);
                DateTime.TryParse(toDate, out DateTime to);
                var iqResponses = _service.GetAllResponses(ResponseType.Consumption, from, to);
                // filter to just total_consumption report types
                List<Consumption> consumptionList = new List<Consumption>();
                foreach (var iq in iqResponses)
                {
                    List<Consumption> iqConsumptions = (List<Consumption>) iq.ToRawResponse(ResponseType.Consumption);
                    iqConsumptions = iqConsumptions.Where(c => c.reportType == "total-consumption").ToList();
                    foreach (var iqItem in iqConsumptions)
                    {
                        iqItem.Id = iq.Id;
                    }
                    consumptionList.AddRange(iqConsumptions);
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

       public async Task<IActionResult> GetHistory(string fromDate, string toDate )
        {
            try
            {
                DateTime.TryParse(fromDate, out DateTime from);
                DateTime.TryParse(toDate, out DateTime to);
                var iqResponses = _service.GetAllResponses(from, to);
                foreach(var iqItem in iqResponses)
                {
                    iqItem.JsonData = "";
                }

                var response = new GetAllResponsesResponse() { IsSuccessful = true, Payload = iqResponses.ToList() };
                response.IsSuccessful = iqResponses != null;
                var jsonResponse = Json(response);
                return jsonResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"exception occurred in IQApiController.GetHistory() - {ex}");
                var response = Json(new GetAllResponsesResponse() { IsSuccessful = false });
                return response;
            }
        }

        public async Task<IActionResult> GetConsumptionDb(int id)
        {
            try
            {
                var consumptionDbList = _service.GetConsumptionDb(id);

                var response = new GetConsumptionResponse() { IsSuccessful = true, Payload = consumptionDbList };
                response.IsSuccessful = consumptionDbList != null;
                var jsonResponse = Json(response);
                return jsonResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"exception occurred in IQApiController.GetConsumptionDb() - {ex}");
                var response = Json(new GetConsumptionResponse() { IsSuccessful = false });
                return response;
            }
        }

        public async Task<IActionResult> GetInverterHistory(string fromDate, string toDate)
        {
            try
            {
                DateTime.TryParse(fromDate, out DateTime from);
                DateTime.TryParse(toDate, out DateTime to);
                var iqResponses = _service.GetAllResponses(ResponseType.Inverters, from, to);
                // filter to just total_consumption report types
                List<Inverter> inverterList = new List<Inverter>();
                foreach (var iq in iqResponses)
                {
                    List<Inverter> iqInverters = (List<Inverter>)iq.ToRawResponse(ResponseType.Inverters);
                    foreach (var iqItem in iqInverters)
                    {
                        iqItem.Id = iq.Id;
                    }
                    inverterList.AddRange(iqInverters);
                }

                var response = new GetInvertersResponse() { IsSuccessful = true, Payload = inverterList };
                response.IsSuccessful = iqResponses != null;
                var jsonResponse = Json(response);
                return jsonResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"exception occurred in IQApiController.GetInverterHistory() - {ex}");
                var response = Json(new GetConsumptionResponse() { IsSuccessful = false });
                return response;
            }
        }

        public async Task<IActionResult> GetInverterDb(int id)
        {
            try
            {
                var inverterDbList = _service.GetInverterDb(id);

                var response = new GetInvertersResponse() { IsSuccessful = true, Payload = inverterDbList };
                response.IsSuccessful = inverterDbList != null;
                var jsonResponse = Json(response);
                return jsonResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"exception occurred in IQApiController.GetConsumptionDb() - {ex}");
                var response = Json(new GetConsumptionResponse() { IsSuccessful = false });
                return response;
            }
        }

        public async Task<IActionResult> GetMeterDb(int id)
        {
            try
            {
                var inverterDbList = _service.GetMeterDb(id);

                var response = new GetMetersResponse() { IsSuccessful = true, Payload = inverterDbList };
                response.IsSuccessful = inverterDbList != null;
                var jsonResponse = Json(response);
                return jsonResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"exception occurred in IQApiController.GetMeterDb() - {ex}");
                var response = Json(new GetMetersResponse() { IsSuccessful = false });
                return response;
            }
        }

        public async Task<IActionResult> GetMeterReadingDb(int id)
        {
            try
            {
                var meterReadingList = _service.GetMeterReadingDb(id);

                var response = new GetMeterReadingsResponse() { IsSuccessful = true, Payload = meterReadingList };
                response.IsSuccessful = meterReadingList != null;
                var jsonResponse = Json(response);
                return jsonResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"exception occurred in IQApiController.GetMeterReadingDb() - {ex}");
                var response = Json(new GetMeterReadingsResponse() { IsSuccessful = false });
                return response;
            }
        }

        public async Task<IActionResult> GetStatusDb(int id)
        {
            try
            {
                var statusDb = _service.GetStatusDb(id);

                var response = new GetStatusResponse() { IsSuccessful = true, Payload = statusDb };
                response.IsSuccessful = statusDb != null;
                var jsonResponse = Json(response);
                return jsonResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"exception occurred in IQApiController.GetStatusDb() - {ex}");
                var response = Json(new GetStatusResponse() { IsSuccessful = false });
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

    public class GetConsumptionDbResponse : IQApiResponse
    {
        public List<ConsumptionDb>? Payload { get; set; }
    }

    public class GetAllResponsesResponse : IQApiResponse
    {
        public List<IQResponse>? Payload { get; set; }
    }


}
