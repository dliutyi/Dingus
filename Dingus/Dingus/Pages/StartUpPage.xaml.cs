using Dingus.ViewModels;
using Xamarin.Forms;

namespace Dingus.Pages
{
    public partial class StartUpPage : ContentPage
    {
        StartUpViewModel _startUpViewModel;
        public StartUpPage()
        {
            InitializeComponent();

            _startUpViewModel = new StartUpViewModel();
            BindingContext = _startUpViewModel;
        }

        private void HostItemTapped(object sender, ClickedEventArgs e)
        {
            _startUpViewModel.SetHost((sender as Button).Text);
            App.MainNavigationService.PresentAsMainPage("SignIn");
        }
    }
}