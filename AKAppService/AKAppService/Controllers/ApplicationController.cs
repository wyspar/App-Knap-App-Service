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
    public class ApplicationController : ControllerBase
    {
        //Uses the business layer to make calls to the db
        private IAppBL appBL;
        public ApplicationController(IAppBL appBL)
        {
            this.appBL = appBL;
        }

        //This post method takes an application and submits it to the database
        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> AddAnAppAsync([FromBody] Application application)
        {
            try
            {
                Application newApp = await this.appBL.AddAnAppAsync(application);
                Log.Logger.Information("New application created: " + $"{application.ID}");
                return CreatedAtAction("AddAnApp", newApp);
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.Message);
                return StatusCode(400);
            }
        }//end of AddAnAppAsync

        //Get method for all applications
        [HttpGet]
        public async Task<IActionResult> GetAllAppsAsync()
        {
            try
            {
                return Ok(await this.appBL.GetAllAppsAsync());
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.Message);
                return StatusCode(500);
            }
        }

        //Get an application by the id
        [HttpGet("{appID}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetApplicationByIDAsync(int appID)
        {
            try
            {
                var foundApp = await appBL.GetAnAppByIDAsync(appID);
                if (foundApp != null) {
                    return Ok(foundApp);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.Message);
                return StatusCode(500);
            }
        }//End of GetApplicationAsync

        //Update an exisiting application in the database
       [HttpPut()]
        public async Task<IActionResult> UpdateAnAppAsync([FromBody] Application application)
        {
            try
            {
                await appBL.UpdateAnAppAsync(application);
                Log.Logger.Information("application at id: "+application.ID+" was updated.");
                return Ok("application was updated :)");
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.Message);
                return StatusCode(500);
            }
        }//End of UpdateAnAppAsync

        //Delete an exisiting application on the database
        [HttpDelete()]
        public async Task<IActionResult> DeleteAppAsync([FromBody] Application application)
        {
            try
            {
                await appBL.DeleteAnAppAsync(application);
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
