using AKAppModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKAppDL
{
    public class UploadRepoDB : IUploadRepoDB
    {
        private AKAppDBContext aKAppDBContext;
        public UploadRepoDB(AKAppDBContext aKAppDBContext)
        {
            this.aKAppDBContext = aKAppDBContext;
        }

        //Add an Upload to the database
        async Task<Upload> IUploadRepoDB.AddAnUploadAsync(Upload upload)
        {
            await aKAppDBContext.Upload.AddAsync(upload);
            await aKAppDBContext.SaveChangesAsync();
            return upload;
        }

        //Deletes an Upload from the db
        async Task<Upload> IUploadRepoDB.DeleteAnUploadAsync(Upload upload)
        {
            aKAppDBContext.Upload.Remove(upload);
            await aKAppDBContext.SaveChangesAsync();
            return upload;
        }
        async Task<Upload> IUploadRepoDB.UpdateAnUploadAsync(Upload upload)
        {
            Upload oldApp = await aKAppDBContext.Upload.Where(app => app.ID == upload.ID).FirstOrDefaultAsync();
            
            aKAppDBContext.Entry(oldApp).CurrentValues.SetValues(upload);
            await aKAppDBContext.SaveChangesAsync();
            aKAppDBContext.ChangeTracker.Clear();

            return oldApp;
        }

        //Get an Upload from the db by the id
        async Task<Upload> IUploadRepoDB.GetAnUploadByIDAsync(int uploadID)
        {
            return await aKAppDBContext.Upload
                .AsNoTracking()
                .FirstOrDefaultAsync(app => app.ID == uploadID);
        }

        //Get all Uploads in the db
        public async Task<List<Upload>> GetAllUploadsAsync()
        {
            return await aKAppDBContext.Upload
                .AsNoTracking()
                .Select(app => app)
                .ToListAsync();
        }
    }
}
