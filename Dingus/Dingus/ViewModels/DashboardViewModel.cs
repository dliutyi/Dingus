using Dingus.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dingus.ViewModels
{
    class DashboardViewModel : ViewModelBase
    {
        public ICommand SignOutCommand { protected set; get; }

        public DashboardViewModel()
        {
            InitializeCommands();
        }

        public void InitializeCommands()
        {
            SignOutCommand = new Command(SignOutCommandHandler);
        }

        public async void SignOutCommandHandler()
        {
            App.Current.MainPage.Navigation.InsertPageBefore(new SignInPage(), ((NavigationPage)App.Current.MainPage).RootPage);
            await App.Current.MainPage.Navigation.PopToRootAsync();
        }
    }
}
