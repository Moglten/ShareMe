using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using File_Sharing.Data.DBModels;
using File_Sharing.ViewModels;

namespace File_Sharing.Services.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUserExtender, UserViewModel>();
        }
    }
}