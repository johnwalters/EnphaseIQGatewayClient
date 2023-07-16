using IQClientLib.Database;
using IQClientLib.Database.Models;
using IQClientLib.Responses;
using IQClientLib.Responses.Consumption;

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

        public IQResponse GetResponse(int id)
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
            List<Consumption> consumptionsForId = (List<Consumption>)dbResponse.ToRawResponse(ResponseType.Consumption);
            foreach (var iq in consumptionsForId)
            {
                iq.Id = id;
            }
            return consumptionsForId;

        }

        public List<Inverter> GetInverterDb(int id)
        {
            var dbResponse = this.GetResponse(id);
            List<Inverter> invertersForId = (List<Inverter>)dbResponse.ToRawResponse(ResponseType.Inverters);
            foreach (var iq in invertersForId)
            {
                iq.Id = id;
            }
            return invertersForId;

        }
    }
}
