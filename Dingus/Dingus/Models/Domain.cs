using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Dingus.Models
{
    public enum DomainType { HTTP, HTTPS };
    public class Domain : INotifyPropertyChanged
    {
        private bool _isActive;

        public string Address { get; set; }
        public string Port { get; set; }
        public DomainType Protocol { get; set; }
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                this._isActive = value;
                OnPropertyChanged("IsActive");
            }
        }

        public string Value { get { return ToString(); } }

        public override string ToString()
        {
            string enumString = Enum.GetName(typeof(DomainType), Protocol);
            return string.Format("{0}://{1}:{2}", enumString, Address, Port).ToLower();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
