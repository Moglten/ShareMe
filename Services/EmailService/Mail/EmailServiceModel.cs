using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace File_Sharing.Services.EmailService.Mail
{
    public class EmailServiceModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}