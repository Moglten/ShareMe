
using Microsoft.AspNetCore.Identity;

namespace File_Sharing.Data.DBModels
{
    public class AppUserExtender : IdentityUser
    {
    [PersonalData]
    public string ShortName { get; set; }

    }
}