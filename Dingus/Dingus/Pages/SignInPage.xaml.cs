using System;
using Xamarin.Forms;
using Dingus.ViewModels;

namespace Dingus.Pages
{
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();

            UserViewModel userViewModel = new UserViewModel();
            userViewModel.Validated += UserViewModelValidated;
            userViewModel.Exception += UserViewModelException;
            BindingContext = userViewModel;
        }

        private void UserViewModelValidated(object sender, EventArgs e)
        {
            App.MainNavigationService.PresentAsMainPage("Main");
        }

        private void UserViewModelException(Exception ex)
        {
            DisplayAlert("Exception", ex.Source + " - " + ex.Message, "OK");
        }

        private async void SignUpClicked(object sender, EventArgs e)
        {
            await App.MainNavigationService.NavigateTo("SignUp", false);
        }
    }
}
