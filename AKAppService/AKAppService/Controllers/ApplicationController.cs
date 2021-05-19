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
                Application newApp = await this.appBL.AddAnAppAsync(application);
                Log.Logger.Information("New application created: " + $"{application.ID}");
                return CreatedAtAction("AddAnApp", newApp);
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.Message);
                return StatusCode(400);
            }
             
        }
    }
}
