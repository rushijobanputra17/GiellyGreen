using DataAccessLayer.Model;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GiellyGreen.Controllers
{
    public class PDFController : Controller
    {
        // GET: PDF
        public byte[] GetPDFBytes(GetInvoicesForPdf_Result model)
        {
            return new Rotativa.ViewAsPdf("~/Views/PDF/Invoice.cshtml", model).BuildFile(ControllerContext);
        }

        public byte[] GetPDFBytesForCombine(List<GetInvoicesForPdf_Result> list)
        {
            return new Rotativa.ViewAsPdf("~/Views/PDF/CombilePDF.cshtml", list).BuildFile(ControllerContext);   
        }

        public ActionResult ReflectView()
        {
            return View("~/Views/PDF/Invoice.cshtml", new GetInvoicesForPdf_Result());
        }
    }
}