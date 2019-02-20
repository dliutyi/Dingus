using Dingus.Models;
using Dingus.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dingus.ViewModels
{
    class CompaniesViewModel : ViewModelBase
    {
        private string _search;
        private string _symbol;
        private ObservableCollection<Company> _companies;

        public ICommand TextChangeCommand { get; protected set; }
        public ICommand SelectedCompanyCommand { get; protected set; }

        private CompanyServices CompanyService { get; set; }

        public CompaniesViewModel()
        {
            CompanyService = new CompanyServices();
            TextChangeCommand = new Command(TextChangeCommandHandler);
            SelectedCompanyCommand = new Command<string>(SelectedCompanyCommandHandler);
        }

        public void TextChangeCommandHandler()
        {
            if(string.IsNullOrWhiteSpace(Search) == true)
            {
                return;
            }

            try
            {
                Symbol = null;
                Companies = new ObservableCollection<Company>(CompanyService.GetCompanies(Search));
            }
            catch
            {
                //await App.Current.MainPage.DisplayAlert("Exception", ex.Message, "OK");
            }
        }

        public void SelectedCompanyCommandHandler(string companySymbol)
        {
            Symbol = companySymbol;
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

        public string Symbol
        {
            get { return _symbol; }
            set { SetProperty(ref _symbol, value); }
        }
    }
}
