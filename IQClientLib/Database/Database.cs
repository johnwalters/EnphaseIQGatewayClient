
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
        public void AddResponseOld(IQResponse response)
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

        public void AddResponse(IQResponse response)
        {
            var jsonData = JsonConvert.SerializeObject(response);
            var sqlTemplate = @"INSERT INTO [dbo].[IQResponse]
           (ResponseType
           ,CreateDate
           ,InverterLastReportDate
           ,MeterReadingTimestamp
           ,MetersLastUpdate
           ,ConsumptionReportCreatedAt
           ,JsonData)
     VALUES
           ({ResponseType},
            {@now}
           ,{InverterLastReportDate}
           ,{MeterReadingTimestamp}
           ,{MetersLastUpdate}
           ,{ConsumptionReportCreatedAt}
           ,{JsonData} )";
            var sql = sqlTemplate.Replace("{ResponseType}", ((int) response.ResponseType).ToString());
            sql = sql.Replace("{@now}", FormatDateParameter(DateTime.Now) );
            sql = sql.Replace("{InverterLastReportDate}", FormatDateParameter(response.InverterLastReportDate));
            sql = sql.Replace("{MeterReadingTimestamp}", FormatDateParameter(response.MeterReadingTimestamp));
            sql = sql.Replace("{MetersLastUpdate}", FormatDateParameter(response.MetersLastUpdate));
            sql = sql.Replace("{ConsumptionReportCreatedAt}", FormatDateParameter(response.ConsumptionReportCreatedAt));
            sql = sql.Replace("{JsonData}", WrapSingleQuotes(jsonData));

            SqlConnection.Execute(sql);

        }
        private string FormatDateParameter(DateTime? date)
        {
            if (date.HasValue)
            {
                return WrapSingleQuotes(date.Value.ToString("yyyy-MM-dd hh:mm:ss"));
            }
            return "NULL";
        }
        private string WrapSingleQuotes(string value)
        {
            return "'" + value + "'";
        }
    }
}
