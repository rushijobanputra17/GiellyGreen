using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GiellyGreen.Controllers
{
    public class PDFController : Controller
    {
        // GET: PDF
        public byte [] GetPDFBytes()
        {
            return new Rotativa.ViewAsPdf("~/Views/Home/Index.cshtml").BuildFile(ControllerContext);
        }
    }
}