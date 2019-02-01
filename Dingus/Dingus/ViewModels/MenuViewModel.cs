using Dingus.Pages;
using Dingus.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Dingus.Services;
using System.Threading.Tasks;
using Dingus.Helpers;

namespace Dingus.ViewModels
{
    public delegate void MenuCommandHandler(string command);

    public class MenuViewModel : ViewModelBase
    {
        public List<DashboardItem> _items;

        public ICommand MenuItemCommand { protected set; get; }
        public event MenuCommandHandler MenuCommandEvent;

        public MenuViewModel()
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
            MenuItemCommand = new Command<string>(MenuItemCommandHandler);
        }

        public void MenuItemCommandHandler(string commandParameter)
        {
            Type type = this.GetType();
            type.GetMethod(string.Format("{0}CommandHandler", commandParameter))?.Invoke(this, null);
            MenuCommandEvent?.Invoke(commandParameter);
        }

        public void SignOutCommandHandler()
        {
            AppSettings.CurrentUser = null;
        }

        public List<DashboardItem> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }
    }
}
