using AutoMapper;
using DataAccessLayer.Services;
using GiellyGreen.Helpers;
using GiellyGreen.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace GiellyGreen.Controllers
{
    [Authorize]
    public class EmailController : ApiController
    {
        public JsonResponse Post(DateTime invoiceDate, string InvoiceRef, List<int> selectedSupplierIds)
        {
           
            MonthlyInvoiceRepository monthlyInvoiceRepository = new MonthlyInvoiceRepository();
            var invoiceDetails = monthlyInvoiceRepository.GetInvoicesForPDF(invoiceDate, selectedSupplierIds);

            PDFController pdfContoller = new PDFController();
            RouteData route = new RouteData();
            route.Values.Add("action", "GetPDFBytes");
            route.Values.Add("controller", "PDF");
            System.Web.Mvc.ControllerContext newContext = new System.Web.Mvc.ControllerContext(new HttpContextWrapper(System.Web.HttpContext.Current), route, pdfContoller);
            pdfContoller.ControllerContext = newContext;

            foreach (var invoice in invoiceDetails)
            {
                Attachment attachment = new Attachment(new MemoryStream(pdfContoller.GetPDFBytes(invoice)), "Invoice.pdf");
               EmailHelper.SendEmail(invoice.EmailAddress,invoice.InvoiceDate,invoice.SupplierName, attachment);
            }
            return new JsonResponse();
        }

       
    }
}
