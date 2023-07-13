using IQClientLib.Database;
using IQClientLib.Database.Models;
using IQClientLib.Responses;
using IQClientLib.Responses.Consumption;
using IQClientLib.Responses.MeterReading;
using IQClientLib.Responses.Status;
using Newtonsoft.Json;

namespace IQClientLib
{
    public class Client
    {

        private string _localEnvoyToken = @"GET_A_TOKEN";
        private static readonly string _localEnvoyIpAddress = "envoy.local"; // or maybe the ip address. ex: 192.168.68.101
        private static readonly int _maxGetAttempts = 3;
        private static readonly int _attemptDelayTicks = 2000;
        private IQResponseRepo _repo;

        public Client(string token, string connectionString)
        {
            _localEnvoyToken = token;
            if (!String.IsNullOrEmpty(connectionString))
            {
                _repo = new IQResponseRepo(connectionString);
            }

        }
        private async Task<string> GetLocalEnvoyJson(string urlSuffix)
        {

            try
            {
                // cert issue on envoy. This api call will only work in a local environment, so we just overlook any cert validation issues here.
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

                var client = new HttpClient(handler);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_localEnvoyToken}");

                try
                {
                    string apiUrl = $"https://{_localEnvoyIpAddress}/{urlSuffix}";
                    HttpResponseMessage response = await this.GetResponseFromServer(client, apiUrl);
                    response.EnsureSuccessStatusCode(); // Throws an exception if the response is not successful
                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    throw ex;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                return null;
            }
            return null;
        }

        private async Task<HttpResponseMessage> GetResponseFromServer(HttpClient client, string apiUrl)
        {
            var attempts = 0;
            var isAttempting = true;
            while (isAttempting)
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    return response;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    var isNoSuchHostException = ex.Message.ToLower().Contains("no such host is known");
                    if (isNoSuchHostException)
                    {
                        // happens when we hit it too hard. Delay and try again
                        attempts++;
                        isAttempting = attempts <= _maxGetAttempts;
                        if (isAttempting)
                        {
                            Console.WriteLine($"Received 'No such host is known' error from server. Delaying and trying again.");
                            Thread.SpinWait(_attemptDelayTicks);
                        }
                        Console.WriteLine($"Received 'No such host is known' error from server and tried {_maxGetAttempts} times. Failed.");

                    }
                    if (!isNoSuchHostException)
                    {
                        throw ex;
                    }

                }
            }
            return null;
        }

        private async Task<T> GetResponse<T>(string urlSuffix)
        {
            //var urlSuffix = "api/v1/production/inverters";
            string jsonResponse = await this.GetLocalEnvoyJson(urlSuffix);

            if (jsonResponse != null)
            {
                Console.WriteLine(jsonResponse);
                var inverterList = JsonConvert.DeserializeObject<T>(jsonResponse);

                return inverterList;
            }
            return default(T);

        }

        public async Task<List<Inverter>> GetInverters()
        {
            var apiResponse = await this.GetResponse<List<Inverter>>("api/v1/production/inverters");
            if (_repo != null)
            {
                _repo.Insert(new IQResponse(apiResponse));
            }
            return apiResponse;
        }

        public async Task<List<Meter>> GetMeters()
        {
            var apiResponse = await this.GetResponse<List<Meter>>("ivp/meters");
            if (_repo != null)
            {
                _repo.Insert(new IQResponse(apiResponse));
            }
            return apiResponse;
        }

        public async Task<List<MeterReading>> GetMeterReadings()
        {
            var apiResponse = await this.GetResponse<List<MeterReading>>("ivp/meters/readings");
            if (_repo != null)
            {
                _repo.Insert(new IQResponse(apiResponse));
            }
            return apiResponse;
        }

        public async Task<Status> GetStatus()
        {
            var apiResponse = await this.GetResponse<Status>("ivp/livedata/status");
            if (_repo != null)
            {
                _repo.Insert(new IQResponse(apiResponse));
            }
            return apiResponse;
        }

        public async Task<List<Consumption>> GetConsumption()
        {
            var apiResponse = await this.GetResponse<List<Consumption>>("ivp/meters/reports/consumption");
            if (_repo != null)
            {
                _repo.Insert(new IQResponse(apiResponse));
            }
            return apiResponse;
        }

        



    }

}
