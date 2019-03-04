using System;

namespace Dingus.Models
{
    public enum DomainType { Http, Https };
    public class Domain : ObservableModel
    {
        public bool IsActive
        {
            get => Get<bool>();
            set => Set(value);
        }

        public string Port { get; set; }
        public string Address { get; set; }
        public DomainType Protocol { get; set; }
        
        public override string ToString()
        {
            string enumString = Enum.GetName(typeof(DomainType), Protocol);
            return $"{enumString}://{Address}:{Port}".ToLower();
        }
    }
}
