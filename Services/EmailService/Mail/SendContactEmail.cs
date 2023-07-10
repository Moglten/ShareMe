using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace File_Sharing.Services.EmailService.Mail
{
    public class SendContactEmail : IEmailService
    {
        public Task SendEmailAsync(EmailServiceModel EmailSM)
        {
            const string senderEmail = "xgameplayer.com@gmail.com";
            const string senderPassword = "xgameplayer.com@gmail.com123";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true
            };
            return client.SendMailAsync(new MailMessage(
                senderEmail,
                EmailSM.Email,
                EmailSM.Subject,
                EmailSM.Message
            ));
        }
    }
}
