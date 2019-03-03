using System.Net.Http;
using System.Threading.Tasks;
using Dingus.Helpers;

namespace Dingus.Services
{
    class BaseServices
    {
        public async Task<bool> CheckConnection(string domain)
        {
            NetServices service = new NetServices(AppSettings.BaseServerTimeout);
            try
            {
                HttpResponseMessage response = await service.GetResponse($"{domain}/api/home");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
