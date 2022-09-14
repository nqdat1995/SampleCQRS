using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Utils
{
    public interface IMailUtils
    {
        bool Send(string toEmail, string subject, string body);
    }

    public class MailUtils : IMailUtils
    {
        private readonly string mailServer;
        private readonly int mailPort;
        private readonly string mailContent = "<html lang=\"en\"><head><meta charset=\"UTF-8\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"><meta name=\"viewport\" content=\"width=device-width,initial-scale=1\"><title>Vui Cùng World Cup</title><link rel=\"preconnect\" href=\"https://fonts.googleapis.com\"><link rel=\"preconnect\" href=\"https://fonts.gstatic.com\" crossorigin><link href=\"https://fonts.googleapis.com/css2?family=Ovo&amp;display=swap\" rel=\"stylesheet\"><style>*{margin:0;padding:0}.main{display:flex;flex-direction:column;align-items:center;justify-content:center;gap:22px 0;min-width:100vw;min-height:100vh;background:url(https://chi01pap001files.storage.live.com/y4mGsV0_3Z1dZDJxJj-E9MaNFJdyAN7Fdxnq6MNb0Mnzikql1ONDgi1YuV5oARKIW1jc18R5gCwdCzrDs64MZEjRdXG0--f3oDoUd06DriLH_vjnEj4uk-tOV5IWmD7xQ16lmgb8PMsPj8ryJc1SdVpW3xqDCwcyZSxewxnmSyXU2H-GVqv08fauZWWH5hUjaOI?width=2000&height=1428&cropmode=none) #fff5e3 no-repeat center center;background-size:cover}.a,.b,.c{font-size:28px;font-family:Ovo,serif}.b{font-size:50px;color:#c79b52}.d{font-family: 'Ovo', serif;padding:20px 40px;background-color:#103252;border-radius:100px;color:#fff;font-weight:700;font-family:Arial,Helvetica,sans-serif}</style></head><body><div class=\"main\"><div class=\"a\">You Are Invited To</div><div class=\"b\">Live A Life With Worldcup 2022</div><div class=\"c\">Your Code Is: @Code</div><a class=\"d\" href=\"#!\">Join Us</a></div></body></html>";
        public MailUtils(IConfiguration configuartion)
        {
            mailServer = configuartion["Mail:Server"];
            mailPort = int.Parse(configuartion["Mail:Port"] ?? "25");
        }

        public bool Send(string toEmail, string subject, string code)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(toEmail);
            mail.From = new MailAddress("registerwc@sacombank.com", "Register WC", System.Text.Encoding.UTF8);
            mail.Subject = subject;

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(mailContent.Replace("@Code", code))))
            {
                mail.Attachments.Add(new Attachment(ms, "invitation_letter.html", "text/html"));
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = "Please open the attachment file to get your code!";
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Port = mailPort;
                client.Host = mailServer;
                client.Send(mail);
            }

            return true;
        }
    }
}
