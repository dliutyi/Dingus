using Dingus.Models;
using Dingus.Services;
using Microcharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace Dingus.ViewModels
{
    class CompaniesViewModel : ViewModelBase
    {
        private string _search;
        private Company _company;
        private ObservableCollection<Company> _companies;

        public ICommand TextChangeCommand { get; protected set; }
        public ICommand SelectedCompanyCommand { get; protected set; }

        private CompanyServices CompanyService { get; set; }

        public CompaniesViewModel()
        {
            CompanyService = new CompanyServices();
            TextChangeCommand = new Command(TextChangeCommandHandler);
            SelectedCompanyCommand = new Command<Company>(SelectedCompanyCommandHandler);            
        }

        public void TextChangeCommandHandler()
        {
            if(string.IsNullOrWhiteSpace(Search) == true)
            {
                return;
            }

            try
            {
                SelectedCompany = null;
                Companies = new ObservableCollection<Company>(CompanyService.GetCompanies(Search));
            }
            catch
            {
                //await App.Current.MainPage.DisplayAlert("Exception", ex.Message, "OK");
            }
        }

        public async void SelectedCompanyCommandHandler(Company companySymbol)
        {
            SelectedCompany = companySymbol;
            SelectedCompany.Quote = await CompanyService.GetCompanyQuote(SelectedCompany.Symbol);
            SelectedCompany.Charts = await CompanyService.GetCompanyChart(SelectedCompany.Symbol);
        }

        public ObservableCollection<Company> Companies
        {
            get { return _companies; }
            set { SetProperty(ref _companies, value); }
        }

        public string Search
        {
            get { return _search; }
            set { SetProperty(ref _search, value); }
        }

        public Company SelectedCompany
        {
            get { return _company; }
            set { SetProperty(ref _company, value); }
        }
    }
}
