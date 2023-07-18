using IQClientLib.Database;
using IQClientLib.Database.Models;
using IQClientLib.Responses;
using IQClientLib.Responses.Consumption;
using IQClientLib.Responses.MeterReading;
using IQClientLib.Responses.Status;

namespace IQClientLib
{
    public class IQService
    {
        private IQResponseRepo _repo;

        public IQService(string connectionString)
        {
            _repo = new IQResponseRepo(connectionString);
        }
        public IEnumerable<IQResponse> GetAllResponses(ResponseType? responseType, DateTime fromDate, DateTime toDate)
        {

            if (_repo != null)
            {
                return _repo.GetAllResponses(responseType, fromDate, toDate);
            }
            return new List<IQResponse>().AsEnumerable();

        }

        public IEnumerable<IQResponse> GetAllResponses(DateTime fromDate, DateTime toDate)
        {

            if (_repo != null)
            {
                return _repo.GetAllResponses(null, fromDate, toDate);
            }
            return new List<IQResponse>().AsEnumerable();

        }

        private IQResponse GetResponse(int id)
        {

            if (_repo != null)
            {
                return _repo.GetResponse(id);
            }
            return null;

        }

        public List<Consumption> GetConsumptionDb(int id)
        {


            var dbResponse = this.GetResponse(id);
            var consumptionsForId = (List<Consumption>)dbResponse.ToRawResponse(ResponseType.Consumption);
            foreach (var iq in consumptionsForId)
            {
                iq.Id = id;
            }
            return consumptionsForId;

        }

        public List<Inverter> GetInverterDb(int id)
        {
            var dbResponse = this.GetResponse(id);
            var invertersForId = (List<Inverter>)dbResponse.ToRawResponse(ResponseType.Inverters);
            foreach (var iq in invertersForId)
            {
                iq.Id = id;
            }
            return invertersForId;

        }

        public List<Meter> GetMeterDb(int id)
        {
            var dbResponse = this.GetResponse(id);
            var metersForId = (List<Meter>)dbResponse.ToRawResponse(ResponseType.Meters);
            foreach (var iq in metersForId)
            {
                iq.Id = id;
            }
            return metersForId;
        }
        public List<MeterReading> GetMeterReadingDb(int id)
        {
            var dbResponse = this.GetResponse(id);
            var meterReadingsForId = (List<MeterReading>)dbResponse.ToRawResponse(ResponseType.MeterReadings);
            foreach (var iq in meterReadingsForId)
            {
                iq.Id = id;
            }
            return meterReadingsForId;
        }
        public Status GetStatusDb(int id)
        {
            var dbResponse = this.GetResponse(id);
            var statusForId = (Status)dbResponse.ToRawResponse(ResponseType.Status);
            statusForId.Id = id;
            return statusForId;
        }
    }
}
