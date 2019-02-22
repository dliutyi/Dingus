using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dingus.Models
{
    public class Company : INotifyPropertyChanged
    {
        private List<CompanyChart> _charts;
        private CompanyQuote _quote;

        public string Symbol { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsEnabled { get; set; }
        public string Type { get; set; }
        public string IexId { get; set; }

        public string Value { get { return ToString(); } }

        public List<CompanyChart> Charts
        {
            get
            {
                return _charts;
            }
            set
            {
                this._charts = value;
                OnPropertyChanged("Charts");
            }
        }

        public CompanyQuote Quote
        {
            get
            {
                return _quote;
            }
            set
            {
                this._quote = value;
                OnPropertyChanged("Quote");
            }
        }
        
        public override string ToString()
        {
            return string.Format("{0}({1})", Symbol, Name);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
