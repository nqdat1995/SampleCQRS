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
        public MailUtils(IConfiguration configuartion)
        {
            mailServer = configuartion["Mail:Server"];
            mailPort = int.Parse(configuartion["Mail:Port"] ?? "25");
        }

        public bool Send(string toEmail, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(toEmail);
            mail.From = new MailAddress("registerwc@sacombank.com", "Register WC", System.Text.Encoding.UTF8);
            mail.Subject = subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Port = mailPort;
            client.Host = mailServer;
            client.Send(mail);
            return true;
        }
    }
}
