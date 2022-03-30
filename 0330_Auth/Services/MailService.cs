using _0330_Auth.Services.Interface;
using System.Net;
using System.Net.Mail;

namespace _0330_Auth.Services
{
    public class MailService : IMailService
    {
        public void SendVerifyMail(string mailTo, int userId)
        {
            // SMTP
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new NetworkCredential("bsdevteam01@gmail.com", "bs53741405P@ssw0rd");
            client.EnableSsl = true;

            //Mail
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("bsdevteam01@gmail.com", "BccupassGua");
            mail.To.Add(mailTo);
            mail.Priority = MailPriority.Normal;
            mail.Subject = "GuaGuaBuldGua School";
            mail.IsBodyHtml = true;
            mail.Body = @$"
                <h1>點及以下連結以啟用(驗證)帳戶</h1>
                <a href='https://localhost:44323/Account/Verify?user={userId}' target='_blank'>連結</a>
            ";

            client.Send(mail);
        }
    }
}
