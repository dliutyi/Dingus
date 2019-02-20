using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dingus.Models
{
    public class Company
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsEnabled { get; set; }
        public string Type { get; set; }
        public string IexId { get; set; }

        public string Value { get { return ToString(); } }

        public override string ToString()
        {
            return string.Format("{0}({1})", Symbol, Name);
        }
    }
}
