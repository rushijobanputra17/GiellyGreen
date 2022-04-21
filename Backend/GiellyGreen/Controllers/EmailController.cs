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
    public class EmailController : ApiController
    {
        public JsonResponse Post(DateTime invoiceDate, string InvoiceRef, List<int> selectedSupplierIds)
        {
            PDFController pdfContoller = new PDFController();
            RouteData route = new RouteData();
            route.Values.Add("action", "GetPDFBytes");
            route.Values.Add("controller", "PDF");
            System.Web.Mvc.ControllerContext newContext = new System.Web.Mvc.ControllerContext(new HttpContextWrapper(System.Web.HttpContext.Current), route, pdfContoller);
            pdfContoller.ControllerContext = newContext;
            Attachment attachment = new Attachment(new MemoryStream(pdfContoller.GetPDFBytes()), "Invoice.pdf");
            return new JsonResponse();
        }
    }
}
