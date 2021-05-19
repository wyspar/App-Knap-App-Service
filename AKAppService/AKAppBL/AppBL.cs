using AKAppModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AKAppDL;

namespace AKAppBL 
{
    public class AppBL : IAppBL
    {
        private IAppRepoDB appDL;
        public AppBL(IAppRepoDB appDL)
        {
            this.appDL = appDL;
        }
        Task<Application> IAppBL.AddAnAppAsync(Application application)
        {
            return appDL.AddAnAppAsync(application);
        }

        Task<Application> IAppBL.DeleteAnAppAsync(Application applicaton)
        {
            return appDL.DeleteAnAppAsync(applicaton);
        }

        Task<List<Application>> IAppBL.GetAllAppsAsync()
        {
            return appDL.GetAllAppsAsync();
        }

        Task<Application> IAppBL.GetAnAppAsync(Application applicaton)
        {
            return appDL.GetAnAppAsync(applicaton);
        }

        Task<Application> IAppBL.UpdateAnAppAsync(Application applicaton)
        {
            return appDL.UpdateAnAppAsync(applicaton);
        }
    }
}
