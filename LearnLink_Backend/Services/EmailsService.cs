using System.Net.Mail;
using System.Net;
using LearnLink_Backend.Models;
using System.Runtime.CompilerServices;

namespace LearnLink_Backend.Services;

// to fire and forget add proper logging service
public static class EmailsService
{
    public static async Task SendEmail(EmailModel emailModel)
    {
        var email = Environment.GetEnvironmentVariable("BusniessAccountEmail") ?? throw new Exception("email field not found in enviroment");
        using (MailMessage mail = new())
        {
            mail.From = new MailAddress(email);
            mail.To.Add(emailModel.ReicieverEmail);
            mail.Subject = emailModel.Header;
            mail.Body = emailModel.Body;
            mail.IsBodyHtml = true;

            using (SmtpClient smtp = new("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential(email, Environment.GetEnvironmentVariable("BusinessAccountPassword") ?? throw new Exception("password field not found in enviroment"));
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }
        }
    }
}
