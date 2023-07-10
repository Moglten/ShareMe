using Microsoft.AspNetCore.Identity;
using System;
using claim = System.Security.Claims;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace File_Sharing.Data.DBModels
{
    public class Contact
    {
        public Contact()
        {
            Id = Guid.NewGuid().ToString();
            
        }

        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual AppUserExtender User { get; set; }
    }
}
