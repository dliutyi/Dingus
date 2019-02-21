using System;
using System.Collections.Generic;
using System.Text;

namespace Dingus.Models
{
    public class CompanyChart
    {
        /*
        "date": "2017-04-03",
        "open": 143.1192,
        "high": 143.5275,
        "low": 142.4619,
        "close": 143.1092,
        "volume": 19985714,
        "unadjustedClose": 143.7,
        "unadjustedVolume": 19985714,
        "change": 0.039835,
        "changePercent": 0.028,
        "vwap": 143.0507,
        "label": "Apr 03, 17",
        "changeOverTime": -0.0039
        */
        public float Close { get; set; }
        public DateTime Date { get; set; }
    }
}
