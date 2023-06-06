
using Microsoft.AspNetCore.Identity;

namespace File_Sharing.Data
{
    public class AppUserExtender : IdentityUser
    {
    [PersonalData]
    public string ShortName { get; set; }
    
    }
}