using GiellyGreen.Controllers;
using Postal;
using System;
using System.Configuration;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.Routing;

namespace GiellyGreen.Helpers
{
    public class EmailHelper
    {
        public static void SendEmail(string emailAddress, DateTime invoiceDate, string supplierName, Attachment attachment)
        {
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            dynamic email = new Email("~/Views/Email/Email.cshtml");
            email.To = emailAddress;
            email.From = smtpSection.From;
            email.Month = invoiceDate.ToString("MMMM");
            email.Year = invoiceDate.ToString("yyyy");
            email.ReceivingCompany = supplierName;
            email.Attach(attachment);
            email.Send();
            //MailMessage mailMessage = new MailMessage
            //{
            //    From = new MailAddress(ConfigurationManager.AppSettings["FromMailAddress"].ToString(), "GiellyGreen"),
            //    Subject = "Your invoice for the " + invoiceDate.ToString("MMMM,yyyy"),
            //    Body = "Please find attached a self-billed invoice to " + supplierName + " , prepared on your behalf, as per the agreement.<br/>Regard<br/>Gielly Green Limited",
            //    IsBodyHtml = true
            //};
            //mailMessage.Attachments.Add(attachment);
            //mailMessage.CC.Add(new MailAddress("rushijobanputra1712001@gmail.com"));
            //mailMessage.To.Add(new MailAddress(emailAddress));
            //SmtpClient smtp = new SmtpClient
            //{
            //    Host = ConfigurationManager.AppSettings["Host"].ToString(),
            //    EnableSsl = true
            //};
            //NetworkCredential NetworkCred = new NetworkCredential
            //{
            //    UserName = mailMessage.From.Address,
            //    Password = ConfigurationManager.AppSettings["Password"].ToString()
            //};
            //smtp.UseDefaultCredentials = true;
            //smtp.Credentials = NetworkCred;
            //smtp.Port = 587;
            //smtp.Send(mailMessage);
        }

        public static System.Web.Mvc.ControllerContext GetPdfContext(string actionMethodName)
        {
            PDFController pdfContoller = new PDFController();
            RouteData route = new RouteData();
            route.Values.Add("action", actionMethodName);
            route.Values.Add("controller", "PDF");
            System.Web.Mvc.ControllerContext newContext = new System.Web.Mvc.ControllerContext(new HttpContextWrapper(HttpContext.Current), route, pdfContoller);
            return newContext;
        }
    }
}