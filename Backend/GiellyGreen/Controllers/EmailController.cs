using AutoMapper;
using DataAccessLayer.Services;
using GiellyGreen.Helpers;
using GiellyGreen.Models;
using Rotativa;
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
    //[Authorize]
    public class EmailController : ApiController
    {
        
        public JsonResponse Post(DateTime invoiceDate, string InvoiceRef, List<int> selectedSupplierIds)
        {
            JsonResponse objResponse = new JsonResponse();
            try
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
                    if (invoice.Logo!=null)
                    {
                        String path = HttpContext.Current.Server.MapPath("~/ImageStorage");
                        invoice.Logo = Path.Combine(path, invoice.Logo);
                    }
                    Attachment attachment = new Attachment(new MemoryStream(pdfContoller.GetPDFBytes(invoice)), "Invoice.pdf");
                    EmailHelper.SendEmail(invoice.EmailAddress, invoice.InvoiceDate, invoice.SupplierName, attachment);
                }
                objResponse = JsonResponseHelper.GetJsonResponse(1, "Email Send Successfully ", null);
            }
            catch(Exception ex)
            {
                if(ex.InnerException!=null)
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(0, "Exception", ex.InnerException.Message);
                }
                else
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(0, "Exception", ex.Message);
                }
            }

            return objResponse;
        }

      [Route("ConvertToPdf")]
        public JsonResponse ConvertToPdf(DateTime invoiceDate, string InvoiceRef, List<int> selectedSupplierIds)
        {
            JsonResponse objResponse = new JsonResponse();
            try
            {
                MonthlyInvoiceRepository monthlyInvoiceRepository = new MonthlyInvoiceRepository();
                var invoiceDetails = monthlyInvoiceRepository.GetInvoicesForPDF(invoiceDate, selectedSupplierIds);

                PDFController pdfContoller = new PDFController();
                RouteData route = new RouteData();
                route.Values.Add("action", "GetPDFBytesForCombine");
                route.Values.Add("controller", "PDF");
                System.Web.Mvc.ControllerContext newContext = new System.Web.Mvc.ControllerContext(new HttpContextWrapper(System.Web.HttpContext.Current), route, pdfContoller);
                pdfContoller.ControllerContext = newContext;

                //foreach (var invoice in invoiceDetails)
                //{
                //    if (invoice.Logo != null)
                //    {
                //        String path = HttpContext.Current.Server.MapPath("~/ImageStorage");
                //        invoice.Logo = Path.Combine(path, invoice.Logo);
                //    }
                //    Attachment attachment = new Attachment(new MemoryStream(pdfContoller.GetPDFBytes(invoice)), "Invoice.pdf");
                //    EmailHelper.SendEmail(invoice.EmailAddress, invoice.InvoiceDate, invoice.SupplierName, attachment);
                //}
                var pdfBytesList = pdfContoller.GetPDFBytesForCombine(invoiceDetails);
                string pdfBase64String = Convert.ToBase64String(pdfBytesList);
               // Attachment attachment = new Attachment(new MemoryStream(pdfContoller.GetPDFBytesForCombine(invoiceDetails)), "Invoice.pdf");
              //  EmailHelper.SendEmail("rushijobanputra1712001@gmail.com", invoiceDate, "rUSGHI", attachment);
                objResponse = JsonResponseHelper.GetJsonResponse(1, "Combined pdf generated ", pdfBase64String);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(0, "Exception", ex.InnerException.Message);
                }
                else
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(0, "Exception", ex.Message);
                }
            }

            return objResponse;
        }
    }
}
