using AKAppModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AKAppDL;

namespace AKAppBL 
{
    public class LocationBL : ILocationBL
    {
        private ILocationRepoDB locationDL;
        public LocationBL(ILocationRepoDB locationDL)
        {
            this.locationDL = locationDL;
        }
        Task<Location> ILocationBL.AddAnLocationAsync(Location location)
        {
            return locationDL.AddAnLocationAsync(location);
        }

        Task<Location> ILocationBL.DeleteAnLocationAsync(Location location)
        {
            Task<Location> foundLocation = locationDL.GetAnLocationByIDAsync(location.ID);
            if (foundLocation != null)
            {
                return locationDL.DeleteAnLocationAsync(location);
            }
            return null;
        }

        Task<List<Location>> ILocationBL.GetAllLocationsAsync()
        {
            return locationDL.GetAllLocationsAsync();
        }

        Task<Location> ILocationBL.GetAnLocationByIDAsync(int locationID)
        {
            return locationDL.GetAnLocationByIDAsync(locationID);
        }

        Task<Location> ILocationBL.UpdateAnLocationAsync(Location location)
        {
            return locationDL.UpdateAnLocationAsync(location);
        }
    }
}
