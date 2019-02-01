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

        private async void UserViewModelValidated(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new MainPage(), ((NavigationPage)App.Current.MainPage).RootPage);
            await Navigation.PopToRootAsync();
        }

        private void UserViewModelException(Exception ex)
        {
            DisplayAlert("Exception", ex.Source + " - " + ex.Message, "OK");
        }

        private void SignUpClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }
    }
}
