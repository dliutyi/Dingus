using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Dingus.Helpers;
using Dingus.Models;
using Dingus.Services;

namespace Dingus.ViewModels
{
    class StartUpViewModel : ViewModelBase
    {
        private bool _isConnecting;
        private ObservableCollection<Domain> _domains;
        
        public ICommand NavigateCommand { get; private set; }
        public ICommand SelectedHostCommand { get; private set; }
        
        public StartUpViewModel()
        {
            IsConnecting = true;

            Domains = new ObservableCollection<Domain>(AppSettings.Domains);
            NavigateCommand = new Command<string>(NavigateCommandHandler);
            SelectedHostCommand = new Command<string>(SelectedHostCommandHandler);

            Task.Run(async () => 
            {
                BaseServices baseServices = new BaseServices();
                foreach(Domain domain in Domains)
                {
                    domain.IsActive = await baseServices.CheckConnection(domain.ToString());
                }
                IsConnecting = false;
            });
        }

        private void NavigateCommandHandler(string pageName) => App.MainNavigationService.PresentAsMainPage(pageName);
        private void SelectedHostCommandHandler(string domain) => AppSettings.CurrentDomain = domain;

        public ObservableCollection<Domain> Domains
        {
            get => _domains;
            set => SetProperty(ref _domains, value);
        }

        public bool IsConnecting
        {
            get => _isConnecting;
            set => SetProperty(ref _isConnecting, value);
        }
    }
}
