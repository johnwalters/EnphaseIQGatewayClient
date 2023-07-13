using IQClientLib.Responses;
using IQClientLib.Responses.Consumption;
using IQClientLib.Responses.MeterReading;
using IQClientLib.Responses.Status;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace IQClientLib.Database.Models
{
    public class IQResponse
    {
        public int Id { get; set; }
        public ResponseType ResponseType { get; set; }
        public DateTime? InverterLastReportDate { get; set; }
        public DateTime? MeterReadingTimestamp { get; set; }
        public DateTime? MetersLastUpdate { get; set; }
        public DateTime? ConsumptionReportCreatedAt { get; set; }
        public string JsonData { get; set; }

        public IQResponse()
        {
        }

        public IQResponse(List<Inverter> inverters)
        {
            this.ResponseType = ResponseType.Inverters;
            this.InverterLastReportDate = ConvertEpochDate(inverters[0].lastReportDate);
            this.JsonData = JsonConvert.SerializeObject(inverters);
        }

        public IQResponse(List<Meter> meters)
        {
            this.ResponseType = ResponseType.Meters;
            this.JsonData = JsonConvert.SerializeObject(meters);
        }

        public IQResponse(List<MeterReading> meterReadings)
        {
            this.ResponseType = ResponseType.MeterReadings;
            this.MeterReadingTimestamp = ConvertEpochDate(meterReadings[0].timestamp);
            this.JsonData = JsonConvert.SerializeObject(meterReadings);
        }

        public IQResponse(Status status)
        {
            this.ResponseType = ResponseType.Status;
            this.MetersLastUpdate = ConvertEpochDate(status.meters.last_update);
            this.JsonData = JsonConvert.SerializeObject(status);
        }

        public IQResponse(List<Consumption> consumption)
        {
            this.ResponseType = ResponseType.Consumption;
            this.ConsumptionReportCreatedAt = ConvertEpochDate(consumption[0].createdAt);
            this.JsonData = JsonConvert.SerializeObject(consumption);
        }

        public static DateTime ConvertEpochDate(long epochDate)
        {
            return DateTimeOffset.FromUnixTimeSeconds(epochDate).DateTime;
        }

        //public List<Consumption> ToConsumptions()
        //{
        //    var c = JsonConvert.DeserializeObject<List<Consumption>>(this.JsonData);
        //    return c;
        //}

        public object ToRawResponse(ResponseType responseType)
        {
            if (String.IsNullOrEmpty(this.JsonData)) return null;
            object rawResponse;
            switch (responseType)
            {
                case ResponseType.Inverters:
                    {
                        rawResponse = JsonConvert.DeserializeObject<List<Inverter>>(this.JsonData);
                        
                        break;
                    }
                case ResponseType.Meters:
                    {
                        rawResponse = JsonConvert.DeserializeObject<List<Meters>>(this.JsonData);
                        break;
                    }
                case ResponseType.MeterReadings:
                    {
                        rawResponse = JsonConvert.DeserializeObject<List<MeterReading>>(this.JsonData);
                        break;
                    }
                case ResponseType.Status:
                    {
                        rawResponse = JsonConvert.DeserializeObject<Status>(this.JsonData);
                        break;
                    }
                case ResponseType.Consumption:
                    {
                        rawResponse = JsonConvert.DeserializeObject<List<Consumption>>(this.JsonData);
                        break;
                    }
                default:
                    {
                        rawResponse = JsonConvert.DeserializeObject<List<Inverter>>(this.JsonData);
                        break;
                    }


            }
            try { 

                
            }catch(Exception ex) { Console.WriteLine($"Exception occurred when setting id in ToRawResponse() {ex}"); }
            
            return rawResponse;
        }
        //public object ToType<T>()
        //{
        //    var rawResponse = JsonConvert.DeserializeObject<T>(this.JsonData);
        //    return rawResponse;
        //}

    }

   

    public enum ResponseType
    {
        Inverters = 0,
        Meters,
        MeterReadings,
        Status,
        Consumption,
    }
}
