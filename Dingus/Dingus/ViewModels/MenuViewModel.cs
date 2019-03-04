using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Reflection;
using Xamarin.Forms;
using Dingus.Services;
using Dingus.Helpers;
using Dingus.Models;

namespace Dingus.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private List<DashboardItem> _items;

        public ICommand MenuItemCommand { get; private set; }

        public MenuViewModel()
        {
            MenuItemCommand = new Command<string>(MenuItemCommandHandler);
            
            Task.Run(new Action(async () => 
            {
                DashboardItemServices itemService = new DashboardItemServices();
                Items = await itemService.GetItems();
            }));
        }

        private void MenuItemCommandHandler(string itemName)
        {
            Type type = this.GetType();
            MethodInfo method = type.GetMethod(string.Format("{0}CommandHandler", itemName));
            if (method == null)
            {
                App.MainNavigationService.NavigateToDetail(itemName);
            }
            else
            {
                method.Invoke(this, null);
            }
        }

        public async void SignOutCommandHandler()
        {
            AppSettings.CurrentUser = null;
            await App.MainNavigationService.NavigateBack();
        }

        public List<DashboardItem> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }
    }
}
