using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace GiellyGreen.Controllers
{
    public class EmailController : ApiController
    {
    
        //public void Post()
        //{
        //  string  toEmails = "harshmungra21@gmail.com,rushi1701@gmail.com";
        //    SendEmail(toEmails);
        //}

        public void SendEmail(string toEmails)
        {
            //toEmails = "harshmungra21@gmail.com,rushi1701@gmail.com";
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("mpte410@gmail.com") ;
            //mailMessage.CC = "invoices@giellygreen.co.uk";
            mailMessage.Subject = "Your invoice for the 2022-03";
            mailMessage.Body = "Please find attached a self-billed invoice to <receiving company>, prepared on your behalf, as per the agreement.Regard Gielly Green Limited";
            mailMessage.IsBodyHtml = true;
            mailMessage.Attachments.Add(new Attachment("D:/pdf.pdf"));
            string[] ToMuliId = toEmails.Split(',');
            mailMessage.CC.Add(new MailAddress("invoices@giellygreen.co.uk"));
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
