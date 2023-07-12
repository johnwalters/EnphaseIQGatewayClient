
using Dapper;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using IQClientLib.Database.Models;
using System.Linq;

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
        public IEnumerable<IQResponse> GetAllResponses(ResponseType? responseType, DateTime fromDate, DateTime toDate)
        {
            var responseTypeClause = "";
            if (responseType.HasValue)
            {
                responseTypeClause = $"ResponseType = {(int) responseType} AND ";
            }
            var sqlTemplate = @"SELECT * FROM IQResponse 
            WHERE 
            {responseTypeClause}
            CreateDate >= {fromDate} AND CreateDate <= {toDate}";
            
            var sql = sqlTemplate.Replace("{responseTypeClause}", responseTypeClause);
            sql = sql.Replace("{fromDate}", FormatDateParameter(fromDate));
            sql = sql.Replace("{toDate}", FormatDateParameter(toDate));
            var dbEntity = SqlConnection.Query<IQResponse>(sql);
            return dbEntity.AsEnumerable();
        }

        public IQResponse GetResponse(int id)
        {

            var sqlTemplate = @"SELECT * FROM IQResponse 
            WHERE 
            Id = {id}";

            var sql = sqlTemplate.Replace("{id}", id.ToString());
            var dbEntity = SqlConnection.QueryFirst<IQResponse>(sql);
            return dbEntity;
        }

        public void DeleteResponse(int id)
        {
            var sqlTemplate = @"DELETE IQResponse WHERE ID = {id}";
            var sql = sqlTemplate.Replace("{id}", id.ToString());
            SqlConnection.Execute(sql);
        }

        public void AddResponse(IQResponse response)
        {
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
           ,'{JsonData}' )";
            var sql = sqlTemplate.Replace("{ResponseType}", ((int) response.ResponseType).ToString());
            sql = sql.Replace("{@now}", FormatDateParameter(DateTime.Now) );
            sql = sql.Replace("{InverterLastReportDate}", FormatDateParameter(response.InverterLastReportDate));
            sql = sql.Replace("{MeterReadingTimestamp}", FormatDateParameter(response.MeterReadingTimestamp));
            sql = sql.Replace("{MetersLastUpdate}", FormatDateParameter(response.MetersLastUpdate));
            sql = sql.Replace("{ConsumptionReportCreatedAt}", FormatDateParameter(response.ConsumptionReportCreatedAt));
            sql = sql.Replace("{JsonData}", response.JsonData);

            SqlConnection.Execute(sql);

        }

        
        private string FormatDateParameter(DateTime? date)
        {
            if (date.HasValue)
            {
                return WrapSingleQuotes(date.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            return "NULL";
        }
        private string WrapSingleQuotes(string value)
        {
            return "'" + value + "'";
        }
    }
}
