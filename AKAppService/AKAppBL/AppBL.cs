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
/*            Task<Application> foundApp = appDL.GetAnAppAsync(application);

            if (foundApp.Result != null)
            {
                System.Diagnostics.Debug.WriteLine(foundApp.Result.FirstName);
                System.Diagnostics.Debug.WriteLine(foundApp.Result.ID);
                return foundApp;
            }
            System.Diagnostics.Debug.WriteLine(application.FirstName);*/
            return appDL.AddAnAppAsync(application);
        }

        Task<Application> IAppBL.DeleteAnAppAsync(Application application)
        {
            Task<Application> foundApp = appDL.GetAnAppAsync(application);
            if (foundApp != null)
            {
                return appDL.DeleteAnAppAsync(application); ;
            }
            return null;
        }

        Task<List<Application>> IAppBL.GetAllAppsAsync()
        {
            return appDL.GetAllAppsAsync();
        }

        Task<Application> IAppBL.GetAnAppAsync(Application application)
        {
            return appDL.GetAnAppAsync(application);
        }

        Task<Application> IAppBL.UpdateAnAppAsync(Application application)
        {
            return appDL.UpdateAnAppAsync(application);
        }
    }
}
