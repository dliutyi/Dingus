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
    public partial class CompaniesPage : ContentPage
    {
        public CompaniesPage()
        {
            InitializeComponent();

            BindingContext = new CompaniesViewModel();
        }
    }
}