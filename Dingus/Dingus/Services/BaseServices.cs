using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dingus.Services
{
    class BaseServices
    {
        public async Task<bool> Connection(string domain)
        {
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(30);
            client.MaxResponseContentBufferSize = 256000;

            Uri uri = new Uri(string.Format("{0}/api/home", domain));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
