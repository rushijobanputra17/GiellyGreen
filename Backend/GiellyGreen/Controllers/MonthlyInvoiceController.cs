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
    [RoutePrefix("invoice")]
    public class MonthlyInvoiceController : ApiController
    {
        public static MonthlyInvoiceRepository monthlyInvoiceRepository   = new MonthlyInvoiceRepository();
        public JsonResponse objResponse;

        public JsonResponse Get(DateTime InvoiceMonth)
        {
            try
            {
                var InvoicveDetails = monthlyInvoiceRepository.GetAllInvoice(InvoiceMonth);

                objResponse = JsonResponseHelper.GetJsonResponse(1, "Records found : ", InvoicveDetails);
            }
            catch (Exception ex)
            {
                objResponse = JsonResponseHelper.GetJsonResponse(2, "Exception", ex.Message);
            }

            return objResponse;
        }

        public JsonResponse post(InvoiceModels invoiceModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //  DateTime.Now.ToString("MMMM dd");
                    invoiceModel.InvoiceDate = Convert.ToDateTime(invoiceModel.InvoiceDate);
                    //invoiceModel.InvoiceDate =  Convert.ToDateTime((Convert.ToDateTime(invoiceModel.InvoiceDate).ToString("yyyy-mm-dd")));
                    monthlyInvoiceRepository.AddInvoice(invoiceModel);
                    //if (monthlyInvoiceRepository.AddSupplier(mapper.Map<Supplier>(model)) == 1)
                    //{
                    //    objResponse = JsonResponseHelper.GetJsonResponse(1, "Record added successfully", model);
                    //}
                    //else
                    //{
                    //    objResponse = JsonResponseHelper.GetJsonResponse(0, "There was an error while adding record", null);
                    //}
                }
                else
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(0, "There was an error while adding record", ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage).ToList());
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
