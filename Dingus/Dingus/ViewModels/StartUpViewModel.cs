using Dingus.Helpers;
using Dingus.Models;
using Dingus.Services;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Dingus.ViewModels
{
    class StartUpViewModel : ViewModelBase
    {
        public bool _isConnecting;
        public ObservableCollection<Domain> _domains;
        
        public StartUpViewModel()
        {
            IsConnecting = true;
            Domains = new ObservableCollection<Domain>(AppSettings.Domains);

            Task.Run(async () => 
            {
                BaseServices baseServices = new BaseServices();
                foreach(Domain domain in Domains)
                {
                    domain.IsActive = await baseServices.Connection(domain.ToString());
                }
                IsConnecting = false;
            });
        }

        public void SetHost(string domain)
        {
            AppSettings.CurrentDomain = domain;
        }

        public ObservableCollection<Domain> Domains
        {
            get { return _domains; }
            set { SetProperty(ref _domains, value); }
        }

        public bool IsConnecting
        {
            get { return _isConnecting; }
            set { SetProperty(ref _isConnecting, value); }
        }
    }
}
