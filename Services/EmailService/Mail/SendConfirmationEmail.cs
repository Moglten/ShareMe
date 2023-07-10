using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using File_Sharing.Services.EmailService.Mail;

namespace File_Sharing.Services.EmailService.Mail
{
    public class SendConfirmationEmail : IEmailService
    {
        public Task SendEmailAsync(EmailServiceModel EmailSM)
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.Credentials = new NetworkCredential("xgameplayer.com@gmail.com", "xgameplayer.com@gmail.com123");

                var msg = new MailMessage();
                msg.To.Add(EmailSM.Email);
                msg.Subject = EmailSM.Subject;
                msg.Body = "The Confirmation Email that you should click, \n" + EmailSM.Message + "\n Congratulation you new part of the appliaction.";
                msg.From = new MailAddress("xgameplayer.com@gmail.com", "File Sharing Mail System", System.Text.Encoding.UTF8);

                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Send(msg);
            }
            return Task.CompletedTask;
        }
    }
}