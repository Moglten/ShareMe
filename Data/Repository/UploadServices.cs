using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using File_Sharing.Data;
using File_Sharing.Data.DBModels;
using File_Sharing.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace File_Sharing.Data
{
    public class UploadServices : IUploadServices
    {
        private readonly ApplicationDbContext _db;

        private readonly IMapper _mapper;
        public UploadServices(ApplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public IQueryable<UploadViewModel> GetAll()
        {
            return _db.Uploads
                    .OrderByDescending(u => u.UploadDate)
                    .ProjectTo<UploadViewModel>(_mapper.ConfigurationProvider);
        }

        public async Task<bool> CreateAsync(InputUpload UploadModel)
        {
            await _db.Uploads.AddAsync(_mapper.Map<Uploads>(UploadModel));
            await _db.SaveChangesAsync();

            return true;
        }
        
        public IQueryable<UploadViewModel> Search(string searchTerm)
        {
            return _db.Uploads.Where(u => u.OriginalFileName.Contains(searchTerm))
                              .OrderByDescending(u => u.UploadDate)
                              .ProjectTo<UploadViewModel>(_mapper.ConfigurationProvider);
        }

        public IQueryable<UploadViewModel> GetByUserId(string UserId)
        {
            return _db.Uploads.Where(u => u.UserId == UserId)
                              .OrderByDescending(u => u.UploadDate)
                              .ProjectTo<UploadViewModel>(_mapper.ConfigurationProvider);
        }

        public async Task<UploadViewModel> FindAsync(string Id, string UserId)
        {
           var selectedUpload = await _db.Uploads.FirstOrDefaultAsync(u => u.Id == Id && u.UserId == UserId);

            if (selectedUpload == null)
                return null;

            return _mapper.Map<UploadViewModel>(selectedUpload);
        }

        public async Task<bool> DeleteAsync(string Id, string UserId)
        {
            var upload = await _db.Uploads
                                .FirstOrDefaultAsync(u => u.Id == Id && u.UserId == UserId);

            if (upload == null)
                return false;

            _db.Uploads.Remove(upload);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<UploadViewModel> FindAsync(string Id)
        {
            var selectedUpload = await _db.Uploads.FindAsync(Id);

            if (selectedUpload == null)
                return null;

            return _mapper.Map<UploadViewModel>(selectedUpload);
        }

        public async Task<Uploads> FindDBAsync(string Id)
        {
            var selectedUpload = await _db.Uploads.FindAsync(Id);

            return selectedUpload;
        }

        public async Task<bool> UpdateAsync(Uploads UploadModel)
        {
            if(UploadModel == null)
                return false;
            _db.Update(UploadModel);

            await _db.SaveChangesAsync();

            return true;
        }

        public int GetTotalUploads()
        {
            return _db.Uploads.Count();
        }
    }
}