using File_Sharing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace File_Sharing.Helpers.Mail
{
    public interface IEmailService
    {
        void SendEmail(ContactViewModel contactVM);
    }
}
