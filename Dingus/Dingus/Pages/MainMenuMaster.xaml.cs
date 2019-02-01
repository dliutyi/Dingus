using Dingus.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Dingus.Pages
{
    public partial class MainMenuMaster : ContentPage
    {
        public ListView ListView;
        public MenuViewModel ViewModel { get; private set; }
        public MainMenuMaster()
        {
            InitializeComponent();

            ViewModel = new MenuViewModel();
            BindingContext = ViewModel;
            ListView = MenuItemsListView;
        }
    }
}