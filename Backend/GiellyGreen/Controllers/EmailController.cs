using DataAccessLayer.Services;
using GiellyGreen.Helpers;
using GiellyGreen.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace GiellyGreen.Controllers
{
    //[Authorize]
    public class EmailController : ApiController
    {
        public static MonthlyInvoiceRepository monthlyInvoiceRepository = new MonthlyInvoiceRepository();
        JsonResponse objResponse;
        PDFController pdfContoller = new PDFController();

        public JsonResponse Post(DateTime invoiceDate, string invoiceRef, List<int> selectedSupplierIds)
        {
            try
            {
                var invoiceDetails = monthlyInvoiceRepository.GetInvoicesForPDF(invoiceDate, selectedSupplierIds);

                //PDFController pdfContoller = new PDFController();
                //RouteData route = new RouteData();
                //route.Values.Add("action", "GetPDFBytes");
                //route.Values.Add("controller", "PDF");
                //System.Web.Mvc.ControllerContext newContext = new System.Web.Mvc.ControllerContext(new HttpContextWrapper(System.Web.HttpContext.Current), route, pdfContoller);
                //pdfContoller.ControllerContext = newContext;

                pdfContoller.ControllerContext = EmailHelper.GetPdfContext("GetPDFBytes");

                if (invoiceDetails.Count > 0)
                {
                    foreach (var invoice in invoiceDetails)
                    {
                        if (invoice.Net > 0)
                        {
                            if (invoice.Logo != null)
                            {
                                String path = HttpContext.Current.Server.MapPath("~/ImageStorage");
                                invoice.Logo = Path.Combine(path, invoice.Logo);
                            }
                            Attachment attachment = new Attachment(new MemoryStream(pdfContoller.GetPDFBytes(invoice)), "Invoice.pdf");
                            EmailHelper.SendEmail(invoice.EmailAddress, invoice.InvoiceDate, invoice.SupplierName, attachment);
                        }
                    }
                    objResponse = JsonResponseHelper.GetJsonResponse(1, "Email Send Successfully ", null);
                }
                else
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(0, "No Records found", null);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(2, "Exception", ex.InnerException.Message);
                }
                else
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(2, "Exception", ex.Message);
                }
            }

            return objResponse;
        }

        [Route("ConvertToPdf")]
        [HttpPost]
        public JsonResponse ConvertToPdf(DateTime invoiceDate, string InvoiceRef, List<int> selectedSupplierIds)
        {
            try
            {
                var invoiceDetails = monthlyInvoiceRepository.GetInvoicesForPDF(invoiceDate, selectedSupplierIds);
                if (invoiceDetails.Count > 0)
                {
                    //PDFController pdfContoller = new PDFController();
                    //RouteData route = new RouteData();
                    //route.Values.Add("action", "GetPDFBytesForCombine");
                    //route.Values.Add("controller", "PDF");
                    //System.Web.Mvc.ControllerContext newContext = new System.Web.Mvc.ControllerContext(new HttpContextWrapper(System.Web.HttpContext.Current), route, pdfContoller);
                    //pdfContoller.ControllerContext = newContext;

                    pdfContoller.ControllerContext = EmailHelper.GetPdfContext("GetPDFBytesForCombine");

                    var pdfBytesList = pdfContoller.GetPDFBytesForCombine(invoiceDetails);
                    string pdfBase64String = Convert.ToBase64String(pdfBytesList);
                    objResponse = JsonResponseHelper.GetJsonResponse(1, invoiceDate.ToString("MMMM") + "_Invoice", pdfBase64String);
                }
                else
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(0, "No Records found", null);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(2, "Exception", ex.InnerException.Message);
                }
                else
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(2, "Exception", ex.Message);
                }
            }

            return objResponse;
        }
    }
}
