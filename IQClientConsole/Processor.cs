using IQClientLib;
using IQClientLib.Database.Models;
using Microsoft.Extensions.Configuration;


namespace IQClientConsole
{
    public class Processor
    {
        private IConfiguration _config;
        private Client _client;

        public Processor()
        {
            _config = InitConfiguration();
            var token = _config["Token"];
            var connectionString = _config["ConnectionStrings:DefaultConnection"];
            _client = new Client(token, connectionString);
        }

        public void GetIQResponses()
        {
            WriteMessage("Getting Inverters response.");
            var inverterResponse = _client.GetInverters().Result;
            Console.WriteLine(new IQResponse(inverterResponse).JsonData + "\n");

            WriteMessage("Getting Meters response.");
            var meterResponse = _client.GetMeters().Result;
            Console.WriteLine(new IQResponse(meterResponse).JsonData + "\n");

            WriteMessage("Getting Meter Readings response.");
            var meterReadingResponse = _client.GetMeterReadings().Result;
            Console.WriteLine(new IQResponse(meterReadingResponse).JsonData + "\n");

            WriteMessage("Getting Status response.");
            var statusResponse = _client.GetStatus().Result;
            Console.WriteLine(new IQResponse(statusResponse).JsonData + "\n");

            WriteMessage("Getting Consumption response.");
            var consumptionResponse = _client.GetConsumption().Result;
            Console.WriteLine(new IQResponse(consumptionResponse).JsonData + "\n");
        }

        public void GetInverters()
        {
            WriteMessage("Getting Inverters response.");
            try
            {
                var inverterResponse = _client.GetInverters().Result;
                Console.WriteLine(new IQResponse(inverterResponse).JsonData + "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred {ex}");
            }


        }

        public void GetMeters()
        {
            WriteMessage("Getting Meters response.");
            try
            {
                var meterResponse = _client.GetMeters().Result;
                Console.WriteLine(new IQResponse(meterResponse).JsonData + "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred {ex}");
            }

        }

        public void GetMeterReadings()
        {
            WriteMessage("Getting Meter Readings response.");
            try
            {
                var meterReadingResponse = _client.GetMeterReadings().Result;
                Console.WriteLine(new IQResponse(meterReadingResponse).JsonData + "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred {ex}");
            }

        }

        public void GetStatus()
        {

            WriteMessage("Getting Status response.");
            try
            {
                var statusResponse = _client.GetStatus().Result;
                Console.WriteLine(new IQResponse(statusResponse).JsonData + "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred {ex}");
            }
        }

        public void GetConsumption()
        {
            WriteMessage("Getting Consumption response.");
            try
            {
                var consumptionResponse = _client.GetConsumption().Result;
                Console.WriteLine(new IQResponse(consumptionResponse).JsonData + "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred {ex}");
            }
        }

        private void WriteMessage(string message, bool addPressCtrlCToCancelMessage = false)
        {
            Console.WriteLine(DateTime.Now.ToString("y-MM-dd hh:mm:ss") + " " + message);
            if (!addPressCtrlCToCancelMessage) return;
            Console.WriteLine("Press ctrl+C to cancel.");
        }

        private IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            //.AddJsonFile("appsettings.Production.json")
            .Build();
            return config;
        }
    }
}
