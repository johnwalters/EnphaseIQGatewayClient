using IQClientLib.Database.Models;


namespace IQClientLib.Database
{
    public class IQResponseRepo
    {
        //private IQResponse? _response;
        private string ConnectionString { get; set; }
        private Database _data { get; set; }

        public IQResponseRepo(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private Database Database
        {
            get
            {
                if (_data == null)
                {
                    _data = new Database(ConnectionString);
                }
                return _data;
            }
        }

        public void Insert(IQResponse response)
        {
            this.Database.AddResponse(response);
        }

        public void Delete(int id)
        {
            this.Database.DeleteResponse(id);
        }

        public IQResponse GetResponse(int id)
        {
            return this.Database.GetResponse(id);
        }

        public IEnumerable<IQResponse> GetAllResponses(ResponseType? responseType, DateTime fromDate, DateTime toDate)
        {
            return this.Database.GetAllResponses(responseType, fromDate, toDate);
        }
    }
}
