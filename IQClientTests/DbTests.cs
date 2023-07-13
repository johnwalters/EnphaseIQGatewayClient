using IQClientLib;
using IQClientLib.Database;
using IQClientLib.Database.Models;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Net;

namespace IQClientTests
{
    public class DbTests
    {
        private IConfiguration _config;
        private string _token = "";
        private string _connectionString = "";
        private Client _client;
        private IQResponseRepo _repo;
        private IQService _service;

        [SetUp]
        public void Setup()
        {
            _config = InitConfiguration();
            _token = _config["Token"];
            _connectionString = _config["ConnectionStrings:DefaultConnection"];
            _client = new Client(_token, _connectionString);
            _repo = new IQResponseRepo(_connectionString);
            _service = new IQService(_connectionString);
        }

        [Test]
        public void AddInverterResponse()
        {
            var response = _client.GetInverters().Result;
            Assert.IsNotNull((response));
            var iqResponse = new IQResponse(response);
            var beforeInsert = DateTime.Now;
            _repo.Insert(iqResponse);
            var afterInsert = DateTime.Now;
            var entries = _repo.GetAllResponses(ResponseType.Inverters, beforeInsert, afterInsert).Where(e => e.ResponseType == ResponseType.Inverters);
            Assert.IsTrue(entries.Count() == 1);
            _repo.Delete(entries.ToList()[0].Id);
            entries = _repo.GetAllResponses(ResponseType.Inverters, beforeInsert, afterInsert).Where(e => e.ResponseType == ResponseType.Inverters);
            Assert.IsTrue(entries.Count() == 0);

        }

        [Test]
        public void AddMetersResponse()
        {
            var response = _client.GetMeters().Result;
            Assert.IsNotNull((response));
            var iqResponse = new IQResponse(response);
            var beforeInsert = DateTime.Now;
            _repo.Insert(iqResponse);
            var afterInsert = DateTime.Now;
            var entries = _repo.GetAllResponses(ResponseType.Meters, beforeInsert, afterInsert).Where(e => e.ResponseType == ResponseType.Meters);
            Assert.IsTrue(entries.Count() == 1);
            _repo.Delete(entries.ToList()[0].Id);
            entries = _repo.GetAllResponses(ResponseType.Meters, beforeInsert, afterInsert).Where(e => e.ResponseType == ResponseType.Meters);
            Assert.IsTrue(entries.Count() == 0);


        }

        [Test]
        public void AddMeterReadingsResponse()
        {
            var response = _client.GetMeterReadings().Result;
            Assert.IsNotNull((response));
            var iqResponse = new IQResponse(response);
            var beforeInsert = DateTime.Now;
            _repo.Insert(iqResponse);
            var afterInsert = DateTime.Now;
            var entries = _repo.GetAllResponses(ResponseType.MeterReadings, beforeInsert, afterInsert).Where(e => e.ResponseType == ResponseType.MeterReadings);
            Assert.IsTrue(entries.Count() == 1);
            _repo.Delete(entries.ToList()[0].Id);
            entries = _repo.GetAllResponses(ResponseType.MeterReadings, beforeInsert, afterInsert).Where(e => e.ResponseType == ResponseType.MeterReadings);
            Assert.IsTrue(entries.Count() == 0);

        }

        [Test]
        public void AddStatusResponse()
        {
            var response = _client.GetStatus().Result;
            Assert.IsNotNull((response));
            var iqResponse = new IQResponse(response);
            var beforeInsert = DateTime.Now;
            _repo.Insert(iqResponse);
            var afterInsert = DateTime.Now;
            var entries = _repo.GetAllResponses(ResponseType.Status, beforeInsert, afterInsert).Where(e => e.ResponseType == ResponseType.Status);
            Assert.IsTrue(entries.Count() == 1);
            _repo.Delete(entries.ToList()[0].Id);
            entries = _repo.GetAllResponses(ResponseType.Status, beforeInsert, afterInsert).Where(e => e.ResponseType == ResponseType.Status);
            Assert.IsTrue(entries.Count() == 0);

        }

        [Test]
        public void AddConsumptionResponse()
        {
            var response = _client.GetConsumption().Result;
            Assert.IsNotNull((response));
            var iqResponse = new IQResponse(response);
            var beforeInsert = DateTime.Now;
            _repo.Insert(iqResponse);
            var afterInsert = DateTime.Now;
            var entries = _repo.GetAllResponses(ResponseType.Consumption, beforeInsert, afterInsert).Where(e => e.ResponseType == ResponseType.Consumption);
            Assert.IsTrue(entries.Count() == 1);
            _repo.Delete(entries.ToList()[0].Id);
            entries = _repo.GetAllResponses(ResponseType.Consumption, beforeInsert, afterInsert).Where(e => e.ResponseType == ResponseType.Consumption);
            Assert.IsTrue(entries.Count() == 0);

        }

        [Test]
        public void GetConsumptionDbResponse()
        {
            var response = _client.GetConsumption().Result;
            Assert.IsNotNull((response));
            var iqResponse = new IQResponse(response);
            var beforeInsert = DateTime.Now;
            _repo.Insert(iqResponse);
            var afterInsert = DateTime.Now;
            var iqEntries = _repo.GetAllResponses(ResponseType.Consumption, beforeInsert, afterInsert).Where(e => e.ResponseType == ResponseType.Consumption).ToList();
            Assert.IsTrue(iqEntries.Count() >= 1);
            var dbConsumptions = _service.GetConsumptionDb(iqEntries[0].Id);
            Assert.IsNotNull(dbConsumptions);
            Assert.That(dbConsumptions.Count() > 0);

            _repo.Delete(iqEntries.ToList()[0].Id);
            iqEntries = _repo.GetAllResponses(ResponseType.Consumption, beforeInsert, afterInsert).Where(e => e.ResponseType == ResponseType.Consumption).ToList();
            Assert.IsTrue(iqEntries.Count() == 0);


        }

        [Test]
        public void GetSerializedConsumptionResponseFromDb()
        {
            var response = _client.GetConsumption().Result;
            Assert.IsNotNull((response));
            var iqResponse = new IQResponse(response);
            var beforeInsert = DateTime.Now;
            _repo.Insert(iqResponse);
            var afterInsert = DateTime.Now;
            var entries = _repo.GetAllResponses(ResponseType.Consumption, beforeInsert, afterInsert).Where(e => e.ResponseType == ResponseType.Consumption).ToList();
            Assert.IsTrue(entries.Count() == 1);
            var consumptionResponseFromDb = entries[0];
            var cr2 = consumptionResponseFromDb.ToRawResponse(ResponseType.Consumption);
            Assert.IsNotNull(cr2);
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
