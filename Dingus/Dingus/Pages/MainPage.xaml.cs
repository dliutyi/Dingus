using Dingus.Models;
using Dingus.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Dingus.Pages
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            //MasterPage.ListView.ItemSelected += ListView_ItemSelected;
            
            MasterPage.ViewModel.MenuCommandEvent += MenuCommandEvent;
        }

        private void MenuCommandEvent(string command)
        {
            if(command == "SignOut")
            {
                App.MainNavigationService.PresentAsMainPage("SignIn");
                return;
            }

            App.MainNavigationService.NavigateToDetail(command);
            IsPresented = false;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            DashboardItem item = e.SelectedItem as DashboardItem;
            if (item == null)
            {
                return;
            }

            DisplayAlert("Item selected", item.Alias, "OK");

            //var page = (Page)Activator.CreateInstance(item.TargetType);
            //page.Title = item.Title;

            //Detail = new NavigationPage(page);
            //IsPresented = false;

            //MasterPage.ListView.SelectedItem = null;
        }
    }
}