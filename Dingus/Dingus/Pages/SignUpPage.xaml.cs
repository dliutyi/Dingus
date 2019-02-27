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

        private void UserViewModelValidated(object sender, EventArgs e)
        {
            App.MainNavigationService.PresentAsMainPage("Main");
        }

        private void UserViewModelException(Exception ex)
        {
            DisplayAlert("Exception", ex.Source + " - " + ex.Message, "OK");
        }
    }
}