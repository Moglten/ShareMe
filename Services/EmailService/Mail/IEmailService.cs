using File_Sharing;
using File_Sharing.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace File_Sharing.Services.EmailService.Mail
{
    public interface IEmailService
    {
        void SendEmail(EmailServiceModel EmailSM);
    }
}
