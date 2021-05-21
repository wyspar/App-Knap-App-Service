using AKAppModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AKAppDL
{
    public  interface ILocationRepoDB
    {
        public Task<Location> AddAnLocationAsync(Location location);
        public Task<Location> GetAnLocationByIDAsync(int id);
        public Task<Location> UpdateAnLocationAsync(Location location);
        public Task<Location> DeleteAnLocationAsync(Location location);
        public Task<List<Location>> GetAllLocationsAsync();
    }
}