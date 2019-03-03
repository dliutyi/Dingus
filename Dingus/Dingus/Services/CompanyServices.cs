using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Dingus.Models;
using Dingus.Helpers;

namespace Dingus.Services
{
    class CompanyServices
    {
        private NetServices _service;
        private JsonSerializerSettings _jsonSettings;

        public CompanyServices()
        {
            _service = new NetServices();
            _jsonSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            Task.Run(async () => AppSettings.Companies = await GetCompanies());
        }

        public async Task<List<Company>> GetCompanies()
        {
            return await _service.GetDeserializedObject<List<Company>>($"{AppSettings.IexTradingHost}/{AppSettings.IexTradingVersion}/ref-data/symbols", _jsonSettings);
        }

        public async Task<List<CompanyChart>> GetCompanyChart(string symbol, int interval = 7)
        {
            return await _service.GetDeserializedObject<List<CompanyChart>>($"{AppSettings.IexTradingHost}/{AppSettings.IexTradingVersion}/stock/{symbol}/chart/1y?chartInterval={interval}", _jsonSettings);
        }

        public async Task<CompanyQuote> GetCompanyQuote(string symbol)
        {
            return await _service.GetDeserializedObject<CompanyQuote>($"{AppSettings.IexTradingHost}/{AppSettings.IexTradingVersion}/stock/{symbol}/quote", _jsonSettings);
        }

        public List<Company> GetCompanies(string keyword)
        {
            return AppSettings.Companies.FindAll((Company company) => company.Name.ToUpper().Contains(keyword.ToUpper()) || company.Symbol.ToUpper().Contains(keyword.ToUpper()));
        }
    }
}
