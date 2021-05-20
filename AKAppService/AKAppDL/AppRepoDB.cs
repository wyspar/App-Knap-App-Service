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

        //Add an application to the database
        async Task<Application> IAppRepoDB.AddAnAppAsync(Application applicaton)
        {
            //Find a location or address first
/*            Address foundAdd = await aKAppDBContext.Address
                .AsNoTracking()
                .FirstOrDefaultAsync(add => add.Street.Equals(applicaton.Location.Address.Street));*/
            Location foundLoc = await aKAppDBContext.Location
                .Include(app => app.Address)
                .AsNoTracking()
                .FirstOrDefaultAsync(loc => loc.ID == applicaton.Location.ID);

            if (foundLoc != null)
            {
/*                System.Diagnostics.Debug.WriteLine(foundLoc.Name);
                System.Diagnostics.Debug.WriteLine(foundLoc.ID);*/
                aKAppDBContext.Entry(applicaton.Location).State = EntityState.Unchanged;
                aKAppDBContext.Entry(applicaton.Location.Address).State = EntityState.Unchanged;
            }
            await aKAppDBContext.Application.AddAsync(applicaton);
            await aKAppDBContext.SaveChangesAsync();
            return applicaton;
        }

        //Deletes an application from the db
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

        //Get an application from the db by the id
        async Task<Application> IAppRepoDB.GetAnAppByIDAsync(int appID)
        {
            return await aKAppDBContext.Application
                .Include(app => app.Location)
                .Include(app => app.Uploads)
                .AsNoTracking()
                .FirstOrDefaultAsync(app => app.ID == appID);
        }

        //Get all applications in the db
        public async Task<List<Application>> GetAllAppsAsync()
        {
            return await aKAppDBContext.Application
                .AsNoTracking()
                .Select(app => app)
                .ToListAsync();
        }
    }
}
