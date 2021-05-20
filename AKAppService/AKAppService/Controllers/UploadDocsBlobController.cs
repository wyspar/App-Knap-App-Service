using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Serilog;

namespace AKAppService.Controllers
{
    /// <summary>
    /// Rest API for sending docs files to blob storage
    /// </summary>

    [Route("[controller]")]
    [ApiController]
    public class UploadDocsBlobController : ControllerBase
    {
        private BlobServiceClient _blobSC;
        public UploadDocsBlobController(BlobServiceClient blobSC)
        {
            _blobSC = blobSC;
        }
        // POST api/UploadMusicBlobConroller/5
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadBlobToStorageAsync()
        {
            //additional information is being sent in via the submitted form now - songName and isPrivate are both key/value pairs included
            var file = Request.Form.Files[0];
            //we set the filename to store in our blob storage as a unique GUID plus the original file name
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            //we create a temporary localpath for our app service/container
            //note: this path might need to be changed depending on how the kubernetes container works with local temporary files
            //string localPath = @"C:\testpath\" + Guid.NewGuid().ToString();
            //@"C:\local\Temp\" +
            //we grab the file content type to set content type on blob retrieval/metadata updating
            string fileType = file.ContentType;
            //this is our storage container
            //string containerEndpoint = "https://revmixerstorage2.blob.core.windows.net/uploadmusic";

            //Name of the container we are uploading to
            BlobContainerClient containerClient = _blobSC.GetBlobContainerClient("appknapuploads");

            //here, we create a new blob container client giving the container URL, and we generate the "current users" credentials depending on if we're in a development
            //environment vs production.
            //the "DefaultAzureCredential" class instantiation pulls credentials from either the current logged in microsoft account if on a local environment
            //or from the deployed app's IAM role if in production
            //BlobContainerClient containerClient = new BlobContainerClient(new Uri(containerEndpoint), new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeSharedTokenCacheCredential = true }));
            try
            {
                //create container if it does not exists
                if (file.Length > 0)
                {
                    // use a filestream to write the consumed file to a local directory

                    //file.CopyTo(stream);
                    //reset the streams position to the start of the filestream
                    //stream.Position = 0;

                    //call our blob client and upload the file, giving the filename that we created earlier, along with the file stream
                    //containerClient.UploadBlob(fileName, stream);

                    //then we retrieve the newly uploaded blob to process metadata about the blob content type, and reupload
                    var blob = containerClient.GetBlobClient(fileName);
                    //reset the stream position to push the duplicate file again
                    //stream.Position = 0;
                    blob.Upload(
                        file.OpenReadStream(),
                        //set header information about the reuploaded blob
                        new BlobHttpHeaders
                        {
                            ContentType = fileType
                        },
                        conditions: null);
                    Log.Logger.Information($"File {fileName} uploaded to azure blob storage");
                    return CreatedAtAction("UploadBlobToStorage", blob);
                }
                //return Ok(new { name = fileName, songname = file.FileName });
                //return CreatedAtAction("UploadBlobToStorage", file);
                return StatusCode(500);
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.Message);
                return StatusCode(400, e.Message);
            }
        }
    }
}
