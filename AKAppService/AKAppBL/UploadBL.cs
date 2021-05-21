using AKAppModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AKAppDL;

namespace AKAppBL 
{
    public class UploadBL : IUploadBL
    {
        private IUploadRepoDB uploadDL;
        public UploadBL(IUploadRepoDB uploadDL)
        {
            this.uploadDL = uploadDL;
        }
        Task<Upload> IUploadBL.AddAnUploadAsync(Upload upload)
        {
            return uploadDL.AddAnUploadAsync(upload);
        }

        Task<Upload> IUploadBL.DeleteAnUploadAsync(Upload upload)
        {
            Task<Upload> foundUpload = uploadDL.GetAnUploadByIDAsync(upload.ID);
            if (foundUpload != null)
            {
                return uploadDL.DeleteAnUploadAsync(upload);
            }
            return null;
        }

        Task<List<Upload>> IUploadBL.GetAllUploadsAsync()
        {
            return uploadDL.GetAllUploadsAsync();
        }

        Task<Upload> IUploadBL.GetAnUploadByIDAsync(int uploadID)
        {
            return uploadDL.GetAnUploadByIDAsync(uploadID);
        }

        Task<Upload> IUploadBL.UpdateAnUploadAsync(Upload upload)
        {
            return uploadDL.UpdateAnUploadAsync(upload);
        }
    }
}
