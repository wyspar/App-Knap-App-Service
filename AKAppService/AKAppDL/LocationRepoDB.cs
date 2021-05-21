using AKAppModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKAppDL
{
    public class LocationRepoDB : ILocationRepoDB
    {
        private AKAppDBContext aKAppDBContext;
        public LocationRepoDB(AKAppDBContext aKAppDBContext)
        {
            this.aKAppDBContext = aKAppDBContext;
        }

        //Add an Location to the database
        async Task<Location> ILocationRepoDB.AddAnLocationAsync(Location location)
        {
            await aKAppDBContext.Location.AddAsync(location);
            await aKAppDBContext.SaveChangesAsync();
            return location;
        }

        //Deletes an Location from the db
        async Task<Location> ILocationRepoDB.DeleteAnLocationAsync(Location location)
        {
            aKAppDBContext.Location.Remove(location);
            await aKAppDBContext.SaveChangesAsync();
            return location;
        }
        async Task<Location> ILocationRepoDB.UpdateAnLocationAsync(Location location)
        {
            Location oldApp = await aKAppDBContext.Location.Where(app => app.ID == location.ID).FirstOrDefaultAsync();
            
            aKAppDBContext.Entry(oldApp).CurrentValues.SetValues(location);
            await aKAppDBContext.SaveChangesAsync();
            aKAppDBContext.ChangeTracker.Clear();

            return oldApp;
        }

        //Get an Location from the db by the id
        async Task<Location> ILocationRepoDB.GetAnLocationByIDAsync(int locationID)
        {
            return await aKAppDBContext.Location
                .AsNoTracking()
                .FirstOrDefaultAsync(app => app.ID == locationID);
        }

        //Get all Locations in the db
        public async Task<List<Location>> GetAllLocationsAsync()
        {
            return await aKAppDBContext.Location
                .AsNoTracking()
                .Select(app => app)
                .ToListAsync();
        }
    }
}
