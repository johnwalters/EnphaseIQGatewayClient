﻿using IQClientLib;
using IQClientLib.Database.Models;
using IQClientLib.Responses;
using IQClientLib.Responses.Consumption;
using IQClientLib.Responses.MeterReading;
using IQClientLib.Responses.Status;
using IQClientSite.Models;
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

       
    }

    


}
