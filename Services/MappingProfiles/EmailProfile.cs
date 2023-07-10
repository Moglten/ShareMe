using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using File_Sharing.Data.DBModels;
using File_Sharing.Services.EmailService.Mail;
using File_Sharing.ViewModels;

namespace File_Sharing.Services.MappingProfiles
{
    public class EmailProfile : Profile
    {
        public EmailProfile()
        {
            CreateMap<EmailViewModel, Contact>()
                    .ForMember(dest => dest.UserId, opt => opt.MapFrom<CustomResolverLoggedinUser>())
                    .ForMember(c => c.Id, opt => opt.Ignore());

            CreateMap<EmailViewModel, EmailServiceModel>();
        }
    }
}