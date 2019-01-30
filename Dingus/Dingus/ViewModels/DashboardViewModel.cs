using Dingus.Pages;
using Dingus.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Dingus.Services;
using System.Threading.Tasks;

namespace Dingus.ViewModels
{
    class DashboardViewModel : ViewModelBase
    {
        public List<DashboardItem> _items;
        public ICommand SignOutCommand { protected set; get; }

        public DashboardViewModel()
        {
            InitializeCommands();
            
            Task.Run(new Action(async () => 
            {
                DashboardItemServices itemService = new DashboardItemServices();
                Items = await itemService.GetItems();
            }));
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

        public List<DashboardItem> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }
    }
}
