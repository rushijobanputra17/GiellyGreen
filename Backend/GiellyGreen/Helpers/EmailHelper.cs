﻿using GiellyGreen.Controllers;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Routing;

namespace GiellyGreen.Helpers
{
    public class EmailHelper
    {
        public static void SendEmail(string emailAddress, DateTime invoiceDate, string supplierName, Attachment attachment)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["FromMailAddress"].ToString());
            mailMessage.Subject = "Your invoice for the " + invoiceDate.ToString("MMMM,yyyy");
            mailMessage.Body = "Please find attached a self-billed invoice to "+ supplierName + " , prepared on your behalf, as per the agreement.Regard Gielly Green Limited";
            mailMessage.IsBodyHtml = true;
            mailMessage.Attachments.Add(attachment);
            //string[] ToMuliId = toEmails.Split(',');
            mailMessage.CC.Add(new MailAddress("harshmungra21@gmail.com"));
            mailMessage.To.Add(new MailAddress(emailAddress));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["Host"].ToString();
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential();
            NetworkCred.UserName = mailMessage.From.Address;
            NetworkCred.Password = ConfigurationManager.AppSettings["Password"].ToString();
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mailMessage);
        }

        public static System.Web.Mvc.ControllerContext GetPdfContext(string actionMethodName)
        {
            PDFController pdfContoller = new PDFController();
            RouteData route = new RouteData();
            route.Values.Add("action", "GetPDFBytes");
            route.Values.Add("controller", "PDF");
            System.Web.Mvc.ControllerContext newContext = new System.Web.Mvc.ControllerContext(new HttpContextWrapper(System.Web.HttpContext.Current), route, pdfContoller);
            return newContext;
        }
    }
}