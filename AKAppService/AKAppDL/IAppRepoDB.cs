using AKAppModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AKAppDL
{
    public  interface IAppRepoDB
    {
        public Task<Application> AddAnAppAsync(Application applicaton);
        public Task<Application> GetAnAppAsync(Application applicaton);
        public Task<Application> UpdateAnAppAsync(Application applicaton);
        public Task<Application> DeleteAnAppAsync(Application applicaton);
        public Task<List<Application>> GetAllAppsAsync();
    }
}