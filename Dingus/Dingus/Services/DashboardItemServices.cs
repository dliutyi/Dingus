using Dingus.Helpers;
using Dingus.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dingus.Services
{
    class DashboardItemServices
    {
        public async Task<List<DashboardItem>> GetItems()
        {
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            
            Uri uri = new Uri(string.Format("{0}/api/dashboarditems", AppSettings.CurrentDomain));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string readResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<DashboardItem>>(readResponse);
                }
                else
                {
                    throw new HttpRequestException();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
