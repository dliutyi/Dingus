using System;
using Newtonsoft.Json;

namespace Dingus.Models
{
    public class CompanyQuote
    {
        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        [JsonProperty("iexRealtimePrice")]
        public double Price { get; set; }
        [JsonProperty("latestTime")]
        public DateTime Date { get; set; }
    }
}
