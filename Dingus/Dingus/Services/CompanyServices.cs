using Dingus.Helpers;
using Dingus.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dingus.Services
{
    class CompanyServices
    {
        public CompanyServices()
        {
            Task.Run(async () => 
            {
                AppSettings.Companies = await GetCompanies();
            });
        }

        public async Task<List<Company>> GetCompanies()
        {
            HttpClient client = new HttpClient();

            Uri uri = new Uri(string.Format("{0}/{1}/ref-data/symbols", AppSettings.IexTradingHost, AppSettings.IexTradingVersion));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string readResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Company>>(readResponse);
                }
                else
                {
                    throw new HttpRequestException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CompanyChart>> GetCompanyChart(string symbol, int interval = 7)
        {
            HttpClient client = new HttpClient();
            
            Uri uri = new Uri(string.Format("{0}/{1}/stock/{2}/chart/1y?chartInterval={3}", AppSettings.IexTradingHost, AppSettings.IexTradingVersion, symbol, interval));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string readResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<CompanyChart>>(readResponse);
                }
                else
                {
                    throw new HttpRequestException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CompanyQuote> GetCompanyQuote(string symbol)
        {
            HttpClient client = new HttpClient();
            
            Uri uri = new Uri(string.Format("{0}/{1}/stock/{2}/quote", AppSettings.IexTradingHost, AppSettings.IexTradingVersion, symbol));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string readResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CompanyQuote>(readResponse);
                }
                else
                {
                    throw new HttpRequestException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Company> GetCompanies(string keyword)
        {
            return AppSettings.Companies.FindAll(delegate(Company company)
            {
                return company.Name.ToUpper().Contains(keyword.ToUpper()) || company.Symbol.ToUpper().Contains(keyword.ToUpper());
            });
        }
    }
}
