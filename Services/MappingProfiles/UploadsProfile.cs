using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using File_Sharing.Data.DBModels;
using File_Sharing.ViewModels;

namespace File_Sharing.Services.MappingProfiles
{
    public class UploadsProfile : Profile
    {
        public UploadsProfile()
        {
           CreateMap<InputUpload, Uploads>()
                        .ForMember(dest => dest.UploadDate, opt => opt.Ignore())
                        .ForMember(des => des.Id, opt => opt.Ignore());
        // from Source to Destination
           CreateMap<Uploads, UploadViewModel>();

           CreateMap<UploadViewModel, Uploads>()
                        .ForMember(dest => dest.UploadDate, opt => opt.Ignore())
                        .ForMember(des => des.Id, opt => opt.Ignore());
        }
    }
}