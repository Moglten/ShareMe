using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace File_Sharing.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUserExtender>
    {
        public ApplicationDbContext(DbContextOptions Options):base(Options)
        {

        }


        public DbSet<Uploads> Uploads { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
