using Dingus.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dingus.Helpers
{
    public class AppSettings
    {
        public static List<Domain> Domains
        {
            get
            {
                List<Domain> domains = new List<Domain>();
                if (Device.RuntimePlatform == Device.Android)
                {
                    domains.Add(new Domain() { Protocol = DomainType.HTTP, Address = "10.0.2.2", Port = "2456" });
                }
                else if(Device.RuntimePlatform == Device.iOS)
                {
                    domains.Add(new Domain() { Protocol = DomainType.HTTP, Address = "localhost", Port = "2456" });
                }
                domains.Add(new Domain() { Protocol = DomainType.HTTP, Address = "192.168.0.104", Port = "2314" });
                return domains;
            }
        }

        public static string CurrentDomain { get; set; }
        public static User CurrentUser { get; set; }
    }
}
