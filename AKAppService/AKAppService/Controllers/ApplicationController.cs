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
        private IAppBL appBL;
        public ApplicationController(IAppBL appBL)
        {
            this.appBL = appBL;
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> AddAnAppAsync([FromBody] Application application)
        {
            try
            {
                await IAppBL.AddAnAppAsync(application);
                Log.Logger.Information("New application created: " + $"{application.ID}");
                return CreatedAtAction("AddedApplication", application);
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.Message);
                return StatusCode(400);
            }
             
        }
    }
}
