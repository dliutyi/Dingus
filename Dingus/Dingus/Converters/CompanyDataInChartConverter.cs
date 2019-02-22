using Dingus.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace Dingus.Converters
{
    class CompanyDataInChartConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<CompanyChart> companyData = (List<CompanyChart>)value;

            List<Microcharts.Entry> entries = new List<Microcharts.Entry>();
            if(companyData == null)
            {
                return entries;
            }

            int currentMonth = 0;
            foreach (CompanyChart data in companyData)
            {
                Microcharts.Entry entry = new Microcharts.Entry(data.Close);
                if(currentMonth != data.Date.Month)
                {
                    currentMonth = data.Date.Month;
                    entry.ValueLabel = data.Date.ToString("MM.yy");
                }
                entries.Add(entry);
            }

            return entries;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
