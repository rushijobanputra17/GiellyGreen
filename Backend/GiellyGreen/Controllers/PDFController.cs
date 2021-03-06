using GiellyGreen.Models;
using System.Web.Mvc;

namespace GiellyGreen.Controllers
{
    public class PDFController : Controller
    {
        // GET: PDF
        public byte[] GetPDFBytes(PdfViewModel model)
        {
            return new Rotativa.ViewAsPdf("~/Views/PDF/Invoice.cshtml", model).BuildFile(ControllerContext);
        }

        public byte[] GetPDFBytesForCombine(CombinePdfViewModel model)
        {
            return new Rotativa.ViewAsPdf("~/Views/PDF/CombilePDF.cshtml", model).BuildFile(ControllerContext);
        }
    }
}