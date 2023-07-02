using IQClientLib;
using IQClientLib.Database;
using IQClientLib.Database.Models;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace IQClientTests
{
    public class DbTests
    {
        private IConfiguration _config;
        private string _token = "";
        private string _connectionString = "";
        private Client _client;
        private IQResponseRepo _repo;

        [SetUp]
        public void Setup()
        {
            _config = InitConfiguration();
            _token = _config["Token"];
            _connectionString = _config["ConnectionStrings:DefaultConnection"];
            _client = new Client(_token);
            _repo = new IQResponseRepo(_connectionString);
        }

        [Test]
        public void AddInverterResponse()
        {
            var response = _client.GetInverters().Result;
            Assert.IsNotNull((response));
            var iqResponse = new IQResponse(response);
            _repo.Insert(iqResponse);

        }

        [Test]
        public void AddMetersResponse()
        {
            var response = _client.GetMeters().Result;
            Assert.IsNotNull((response));
            var iqResponse = new IQResponse(response);
            _repo.Insert(iqResponse);

        }

        [Test]
        public void AddMeterReadingsResponse()
        {
            var response = _client.GetMeterReadings().Result;
            Assert.IsNotNull((response));
            var iqResponse = new IQResponse(response);
            _repo.Insert(iqResponse);

        }

        [Test]
        public void AddStatusResponse()
        {
            var response = _client.GetStatus().Result;
            Assert.IsNotNull((response));
            var iqResponse = new IQResponse(response);
            _repo.Insert(iqResponse);

        }

        [Test]
        public void AddConsumptionResponse()
        {
            var response = _client.GetConsumption().Result;
            Assert.IsNotNull((response));
            var iqResponse = new IQResponse(response);
            _repo.Insert(iqResponse);

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
