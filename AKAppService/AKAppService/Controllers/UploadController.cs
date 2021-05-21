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
    public class UploadController : ControllerBase
    {
        //Uses the business layer to make calls to the db
        private IUploadBL uploadBL;
        public UploadController(IUploadBL uploadBL)
        {
            this.uploadBL = uploadBL;
        }

        //This post method takes an upload and submits it to the database
        //This is not needed because the upload docs blob controller handles posting an upload to the db
/*        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> AddAnUploadAsync([FromBody] Upload upload)
        {
            try
            {
                Upload newUpload = await this.uploadBL.AddAnUploadAsync(upload);
                Log.Logger.Information("New upload created: " + $"{upload.ID}");
                return CreatedAtAction("AddAnUpload", newUpload);
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.Message);
                return StatusCode(400);
            }
        }//end of AddAnUploadAsync*/

        //Get method for all uploads
        [HttpGet]
        public async Task<IActionResult> GetAllUploadsAsync()
        {
            try
            {
                return Ok(await this.uploadBL.GetAllUploadsAsync());
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.Message);
                return StatusCode(500);
            }
        }

        //Get an upload by the id
        [HttpGet("{uploadID}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUploadByIDAsync(int uploadID)
        {
            try
            {
                var foundUpload = await uploadBL.GetAnUploadByIDAsync(uploadID);
                if (foundUpload != null) {
                    return Ok(foundUpload);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.Message);
                return StatusCode(500);
            }
        }//End of GetUploadAsync

        //Update an exisiting upload in the database
       [HttpPut()]
        public async Task<IActionResult> UpdateAnUploadAsync([FromBody] Upload upload)
        {
            try
            {
                await uploadBL.UpdateAnUploadAsync(upload);
                Log.Logger.Information("upload at id: "+upload.ID+" was updated.");
                return Ok("upload was updated :)");
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.Message);
                return StatusCode(500);
            }
        }//End of UpdateAnUploadAsync

        //Delete an exisiting upload on the database
        [HttpDelete()]
        public async Task<IActionResult> DeleteUploadAsync([FromBody] Upload upload)
        {
            try
            {
                await uploadBL.DeleteAnUploadAsync(upload);
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
