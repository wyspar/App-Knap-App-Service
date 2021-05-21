using AKAppModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AKAppDL
{
    public  interface IUploadRepoDB
    {
        public Task<Upload> AddAnUploadAsync(Upload upload);
        public Task<Upload> GetAnUploadByIDAsync(int id);
        public Task<Upload> UpdateAnUploadAsync(Upload upload);
        public Task<Upload> DeleteAnUploadAsync(Upload upload);
        public Task<List<Upload>> GetAllUploadsAsync();
    }
}