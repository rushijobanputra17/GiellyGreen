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
    [Authorize]
    public class EmailController : ApiController
    {
        public static MonthlyInvoiceRepository monthlyInvoiceRepository = new MonthlyInvoiceRepository();
        public static ProfileRepository ProfileRepository = new ProfileRepository();
        JsonResponse objResponse;
        PDFController pdfContoller = new PDFController();

        public JsonResponse Post(DateTime invoiceDate, string invoiceRef, List<int> selectedSupplierIds)
        {
            try
            {
                PdfViewModel pdfViewModel = new PdfViewModel();
                pdfViewModel.CompanyProfile = ProfileRepository.GetProfileInfo();

                var invoiceDetails = monthlyInvoiceRepository.GetInvoicesForPDF(invoiceDate, selectedSupplierIds);
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
                            pdfViewModel.Invoice = invoice;
                            Attachment attachment = new Attachment(new MemoryStream(pdfContoller.GetPDFBytes(pdfViewModel)), "Invoice.pdf");
                            EmailHelper.SendEmail(invoice.EmailAddress, invoice.InvoiceDate, invoice.SupplierName, attachment);
                        }
                    }
                    objResponse = JsonResponseHelper.GetJsonResponse(1, "Email Send Successfully ", null);
                }
                else
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(0, "Email cannot be send because selected invoices have no data", null);
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
                CombinePdfViewModel combinePdfViewModel = new CombinePdfViewModel();

                var invoiceDetails = monthlyInvoiceRepository.GetInvoicesForPDF(invoiceDate, selectedSupplierIds);
                if (invoiceDetails.Count > 0)
                {
                    combinePdfViewModel.CompanyProfile = ProfileRepository.GetProfileInfo();
                    combinePdfViewModel.InvoiceDetails = invoiceDetails;
                    pdfContoller.ControllerContext = EmailHelper.GetPdfContext("GetPDFBytesForCombine");
                    var pdfBytesList = pdfContoller.GetPDFBytesForCombine(combinePdfViewModel);
                    string pdfBase64String = Convert.ToBase64String(pdfBytesList);
                    objResponse = JsonResponseHelper.GetJsonResponse(1, invoiceDate.ToString("MMMM") + "_Invoice", pdfBase64String);
                }
                else
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(0, "Selected invoices have no data so they cannot be combined and downloaded", null);
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
