using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AKAppBL;
using AKAppModels;
using Serilog;

namespace AKAppService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        //Uses the business layer to make calls to the db
        private ILocationBL locationBL;
        public LocationController(ILocationBL locationBL)
        {
            this.locationBL = locationBL;
        }

        //This post method takes a location and submits it to the database
        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> AddAnLocationAsync([FromBody] Location location)
        {
            try
            {
                Location newLocation = await this.locationBL.AddAnLocationAsync(location);
                Log.Logger.Information("New upload created: " + $"{location.ID}");
                return CreatedAtAction("AddAnLocation", newLocation);
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.Message);
                return StatusCode(400);
            }
        }//end of AddAnLocationAsync

        //Get method for all uploads
        [HttpGet]
        public async Task<IActionResult> GetAllLocationsAsync()
        {
            try
            {
                return Ok(await this.locationBL.GetAllLocationsAsync());
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.Message);
                return StatusCode(500);
            }
        }

        //Get an upload by the id
        [HttpGet("{locationID}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetLocationByIDAsync(int locationID)
        {
            try
            {
                var foundLocation = await locationBL.GetAnLocationByIDAsync(locationID);
                if (foundLocation != null) {
                    return Ok(foundLocation);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.Message);
                return StatusCode(500);
            }
        }//End of GetLocationAsync

        //Update an exisiting upload in the database
       [HttpPut()]
        public async Task<IActionResult> UpdateAnLocationAsync([FromBody] Location location)
        {
            try
            {
                await locationBL.UpdateAnLocationAsync(location);
                Log.Logger.Information("upload at id: "+ location.ID+" was updated.");
                return Ok("upload was updated :)");
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.Message);
                return StatusCode(500);
            }
        }//End of UpdateAnLocationAsync

        //Delete an exisiting upload on the database
        [HttpDelete()]
        public async Task<IActionResult> DeleteLocationAsync([FromBody] Location location)
        {
            try
            {
                await locationBL.DeleteAnLocationAsync(location);
                return Ok();
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.Message);                
                return StatusCode(500);
            }
        }

    }
}
