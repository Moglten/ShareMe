using File_Sharing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace File_Sharing.Helpers.Mail
{
    public class EmailService : IEmailService
    {
        public void SendEmail(ContactViewModel contactVM)
        {
            using(SmtpClient client = new SmtpClient("smtp.gmail.com",25))
            {
                client.Credentials = new NetworkCredential("xgameplayer.com@gmail.com", "xgameplayer.com@gmail.com123");

                var msg = new MailMessage();
                msg.To.Add(contactVM.Email);
                msg.Subject = contactVM.Subject;
                msg.Body = "We have recevied this message from you," + contactVM.Message + " somebody will contact you soon." ;
                msg.From = new MailAddress("xgameplayer.com@gmail.com", "File Sharing Mail System", System.Text.Encoding.UTF8);

                client.Send(msg);
            }
        }
    }
}
