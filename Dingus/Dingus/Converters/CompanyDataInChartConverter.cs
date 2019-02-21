using Dingus.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using System.Linq;

namespace Dingus.Converters
{
    class CompanyDataInChartConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<CompanyChart> companyData = (List<CompanyChart>)value;
            if(companyData != null)
            {
                return new List<Microcharts.Entry>(from data in companyData select new Microcharts.Entry(data.Close));
            }
            return new List<Microcharts.Entry>();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
