using IQGatewayClientLib;
using NUnit.Framework;

using Microsoft.Extensions.Configuration;

namespace IQGatewayClientTests
{
    public class ClientTests
    {
        private IConfiguration _config;
        private string _token = "";
        private Client _client;

        [SetUp]
        public void Setup()
        {
            _config = InitConfiguration();
            _token = _config["Token"];
            _client = new Client(_token);
        }

        [Test]
        public void GetLocalEnvoyResponse()
        {
            var response = _client.GetInverters().Result;
            Assert.IsNotNull((response));

        }

        [Test]
        public void GetMetersResponse()
        {
            var response = _client.GetMeters().Result;
            Assert.IsNotNull((response));

        }

        [Test]
        public void GetMeterReadingsResponse()
        {
            var response = _client.GetMeterReadings().Result;
            Assert.IsNotNull((response));

        }

        [Test]
        public void GetStatusResponse()
        {
            var response = _client.GetStatus().Result;
            Assert.IsNotNull((response));

        }

        [Test]
        public void GetConsumptionResponse()
        {
            var response = _client.GetConsumption().Result;
            Assert.IsNotNull((response));

        }

        private static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            return config;
        }
    }
}
