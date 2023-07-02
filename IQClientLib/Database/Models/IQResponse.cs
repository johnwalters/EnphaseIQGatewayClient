﻿using IQClientLib.Responses;
using IQClientLib.Responses.Status;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQClientLib.Database.Models
{
    public class IQResponse
    {
        public ResponseType ResponseType { get; set; }
        public DateTime? InverterLastReportDate { get; set; }
        public DateTime? MeterReadingTimestamp { get; set; }
        public DateTime? MetersLastUpdate { get; set; }
        public DateTime? ConsumptionReportCreatedAt { get; set; }
        public string ResponseJson { get; set; }

        public IQResponse(List<Inverter> inverters)
        {
            this.ResponseType = ResponseType.Inverters;
            var date = new DateTime(inverters[0].lastReportDate);
            this.InverterLastReportDate = date;
            this.ResponseJson = JsonConvert.SerializeObject(inverters);

        }

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
