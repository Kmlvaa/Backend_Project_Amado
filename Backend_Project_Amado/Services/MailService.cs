using System.Net.Mail;

namespace Backend_Project_Amado.Services
{
    public class MailService : IMailService
    {
        public Task SendEmailAsync(string email, string subject, string body)
        {
            var myMail = "emailAdress";
            var password = "password";

            SmtpClient smtp = new SmtpClient("host", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(myMail, password),
                EnableSsl = true
            };

            return smtp.SendMailAsync(new MailMessage(from: myMail,
                                                      to: email,
                                                      subject: subject,
                                                      body: body));
        }
    }
}
