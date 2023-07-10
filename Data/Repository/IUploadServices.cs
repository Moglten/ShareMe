using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using File_Sharing.ViewModels;

namespace File_Sharing.Data
{
    public interface IUploadServices
    {
        IQueryable<UploadViewModel> GetAll();
        IQueryable<UploadViewModel> Search(String searchTerm);
        IQueryable<UploadViewModel> GetByUserId(string UserId);
        Task<bool> CreateAsync (InputUpload UploadModel);
        Task<UploadViewModel> FindAsync(string Id, string UserId);
        Task<UploadViewModel> FindAsync(string Id);
        Task<bool> UpdateAsync(UploadViewModel UploadModel);
        Task<bool> DeleteAsync(string Id, string UserId);
        int GetTotalUploads();
    }
}