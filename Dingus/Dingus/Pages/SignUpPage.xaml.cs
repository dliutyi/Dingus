using Dingus.ViewModels;
using System;
using Xamarin.Forms;

namespace Dingus.Pages
{
	public partial class SignUpPage : ContentPage
	{
		public SignUpPage ()
		{
			InitializeComponent ();

            UserViewModel userViewModel = new UserViewModel();
            userViewModel.Validated += UserViewModelValidated;
            userViewModel.Exception += UserViewModelException;
            BindingContext = userViewModel;
        }

        private async void UserViewModelValidated(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new DashboardPage(), ((NavigationPage)App.Current.MainPage).RootPage);
            await Navigation.PopToRootAsync();
        }

        private void UserViewModelException(Exception ex)
        {
            DisplayAlert("Exception", ex.Source + " - " + ex.Message, "OK");
        }
    }
}