
using Dapper;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using IQClientLib.Database.Models;

namespace IQClientLib.Database
{
    public class Database
    {
        private SqlConnection _dapperSqlConnection;
        private string _connectionString;

        public Database(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqlConnection SqlConnection
        {
            get
            {
                if (_dapperSqlConnection == null)
                {
                    _dapperSqlConnection = new SqlConnection(_connectionString);
                }

                return _dapperSqlConnection;
            }
        }

        // ----------------------------------------------------------------------------------
        public void AddResponse(IQResponse response)
        {
            var jsonData = JsonConvert.SerializeObject(response);
            var parms = new DynamicParameters();
            if(response.ResponseType == ResponseType.Inverters)
            {
                parms.Add("@InverterLastReportDate", response.InverterLastReportDate, dbType: DbType.DateTime);
            }
            if (response.ResponseType == ResponseType.MeterReadings)
            {
                parms.Add("@MeterReadingTimestamp", response.MeterReadingTimestamp, dbType: DbType.DateTime);
            }
            if (response.ResponseType == ResponseType.Status)
            {
                parms.Add("@MetersLastUpdate", response.MetersLastUpdate, dbType: DbType.DateTime);
            }
            if (response.ResponseType == ResponseType.Consumption)
            {
                parms.Add("@ConsumptionReportCreatedAt", response.ConsumptionReportCreatedAt, dbType: DbType.DateTime);
            }
            parms.Add("@JsonData", jsonData, dbType: DbType.String);

            SqlConnection.Execute("KeywordAnalysis_AddOrUpdate", parms, commandType: CommandType.StoredProcedure);

        }
    }
}
