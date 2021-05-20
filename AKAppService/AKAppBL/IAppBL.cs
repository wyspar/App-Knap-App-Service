using AKAppModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AKAppBL
{
    public interface IAppBL
    {
        public Task<Application> AddAnAppAsync(Application application);
        public Task<Application> GetAnAppByIDAsync(int id);
        public Task<Application> UpdateAnAppAsync(Application applicaton);
        public Task<Application> DeleteAnAppAsync(Application applicaton);
        public Task<List<Application>> GetAllAppsAsync();
    }
}