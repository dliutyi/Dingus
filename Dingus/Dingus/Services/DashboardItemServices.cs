using System.Threading.Tasks;
using System.Collections.Generic;
using Dingus.Models;
using Dingus.Helpers;

namespace Dingus.Services
{
    class DashboardItemServices
    {
        public async Task<List<DashboardItem>> GetItems()
        {
            NetServices service = new NetServices();
            return await service.GetDeserializedObject<List<DashboardItem>>($"{AppSettings.CurrentDomain}/api/dashboarditems");
        }
    }
}
