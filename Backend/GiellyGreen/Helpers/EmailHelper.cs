using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace GiellyGreen.Helpers
{
    public class EmailHelper
    {
        public static void SendEmail(string toEmails)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("mpte410@gmail.com");
            mailMessage.Subject = "Your invoice for the 2022-03";
            mailMessage.Body = "Please find attached a self-billed invoice to <receiving company>, prepared on your behalf, as per the agreement.Regard Gielly Green Limited";
            mailMessage.IsBodyHtml = true;
            mailMessage.Attachments.Add(new Attachment("D:/pdf.pdf"));
            string[] ToMuliId = toEmails.Split(',');
            mailMessage.CC.Add(new MailAddress("harshmungra21@gmail.com"));
            foreach (string ToEMailId in ToMuliId)
            {
                mailMessage.To.Add(new MailAddress(ToEMailId)); //adding multiple TO Email Id
            }
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential();
            NetworkCred.UserName = mailMessage.From.Address;
            NetworkCred.Password = "temp192837465";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mailMessage);
        }
    }
}