using AKAppModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKAppDL
{
    public class AppRepoDB : IAppRepoDB
    {
        private AKAppDBContext aKAppDBContext;
        public AppRepoDB(AKAppDBContext aKAppDBContext)
        {
            this.aKAppDBContext = aKAppDBContext;
        }
        async Task<Application> IAppRepoDB.AddAnAppAsync(Application applicaton)
        {
            await aKAppDBContext.Application.AddAsync(applicaton);
            await aKAppDBContext.SaveChangesAsync();
            return applicaton;
        }
        async Task<Application> IAppRepoDB.DeleteAnAppAsync(Application applicaton)
        {
            aKAppDBContext.Application.Remove(applicaton);
            await aKAppDBContext.SaveChangesAsync();
            return applicaton;
        }
        async Task<Application> IAppRepoDB.UpdateAnAppAsync(Application applicaton)
        {
            Application oldApp = await aKAppDBContext.Application.Where(app => app.ID == applicaton.ID).FirstOrDefaultAsync();
            
            aKAppDBContext.Entry(oldApp).CurrentValues.SetValues(applicaton);
            await aKAppDBContext.SaveChangesAsync();
            aKAppDBContext.ChangeTracker.Clear();

            return oldApp;
        }
        async Task<Application> IAppRepoDB.GetAnAppAsync(Application applicaton)
        {
            return await aKAppDBContext.Application
                .Include(app => app.Location)
                .Include(app => app.Uploads)
                .AsNoTracking()
                .FirstOrDefaultAsync(app => app.ID == applicaton.ID);
        }
        public async Task<List<Application>> GetAllAppsAsync()
        {
            return await aKAppDBContext.Application
                .AsNoTracking()
                .Select(app => app)
                .ToListAsync();
        }
    }
}
