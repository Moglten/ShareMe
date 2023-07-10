
using File_Sharing.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace File_Sharing.Services.EmailService.Mail
{
    public class SendContactEmail : IEmailService
    {
        public void SendEmail(EmailServiceModel EmailSM)
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 25))
            {
                client.Credentials = new NetworkCredential("xgameplayer.com@gmail.com", "xgameplayer.com@gmail.com123");

                var msg = new MailMessage();
                msg.To.Add(EmailSM.Email);
                msg.Subject = EmailSM.Subject;
                msg.Body = "We have recevied this message from you," + EmailSM.Message + " somebody will contact you soon.";
                msg.From = new MailAddress("xgameplayer.com@gmail.com", "File Sharing Mail System", System.Text.Encoding.UTF8);

                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Send(msg);
            }
        }
    }
}
