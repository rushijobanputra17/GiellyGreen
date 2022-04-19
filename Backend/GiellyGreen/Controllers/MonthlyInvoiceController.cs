using DataAccessLayer.Services;
using GiellyGreen.Helpers;
using GiellyGreen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GiellyGreen.Controllers
{
    public class MonthlyInvoiceController : ApiController
    {
        public static MonthlyInvoiceRepository monthlyInvoiceRepository   = new MonthlyInvoiceRepository();
        public JsonResponse objResponse;

        public JsonResponse Get(DateTime InvoiceMonth)
        {
            try
            {
                var InvoicveDetails = monthlyInvoiceRepository.GetAllInvoice(InvoiceMonth);

                objResponse = JsonResponseHelper.GetJsonResponse(1, "Records found : " + InvoicveDetails.Count(), InvoicveDetails);
            }
            catch (Exception ex)
            {
                objResponse = JsonResponseHelper.GetJsonResponse(2, "Exception", ex.Message);
            }

            return objResponse;
        }
    }
}
