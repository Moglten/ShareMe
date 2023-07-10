using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace File_Sharing.Services.MappingProfiles
{
    public class CustomResolverLoggedinUser : IValueResolver<object, object, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomResolverLoggedinUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Resolve(object source, object destination, string destMember, ResolutionContext context)
        {
             return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
