using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dingus.Models
{
    public class Company : ObservableModel
    {
        [JsonProperty("iexId")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Symbol { get; set; }

        public bool IsEnabled { get; set; }
        public DateTime Date { get; set; }

        public CompanyQuote Quote
        {
            get => Get<CompanyQuote>();
            set => Set(value);
        }

        public List<CompanyChart> Charts
        {
            get => Get<List<CompanyChart>>();
            set => Set(value);
        }
        
        public override string ToString() => $"{Symbol}({Name})";
    }
}
