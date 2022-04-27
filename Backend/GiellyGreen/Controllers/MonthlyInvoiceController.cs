using DataAccessLayer.Services;
using GiellyGreen.Helpers;
using GiellyGreen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GiellyGreen.Controllers
{
    [Authorize]
    public class MonthlyInvoiceController : ApiController
    {
        public static MonthlyInvoiceRepository monthlyInvoiceRepository = new MonthlyInvoiceRepository();
        public JsonResponse objResponse;

        public JsonResponse Get(DateTime invoiceMonth)
        {
            try
            {
                var invoiceDetails = monthlyInvoiceRepository.GetAllInvoice(invoiceMonth);
                objResponse = JsonResponseHelper.GetJsonResponse(1, "Records found : ", invoiceDetails);
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

        public JsonResponse post(InvoiceModels invoiceModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    invoiceModel.InvoiceDate = Convert.ToDateTime(invoiceModel.InvoiceDate);
                    var monthlyInvoice = monthlyInvoiceRepository.AddInvoice(invoiceModel);
                    if (monthlyInvoice == 0)
                    {
                        objResponse = JsonResponseHelper.GetJsonResponse(0, "Something Went Wrong while Adding table header", monthlyInvoice);
                    }
                    else
                    {
                        objResponse = JsonResponseHelper.GetJsonResponse(1, "Data Saved Successfully", monthlyInvoice);
                    }
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

        public JsonResponse Patch(DateTime selectedDate, List<int> selectedIds)
        {
            try
            {
                if (monthlyInvoiceRepository.ApproveSelectedInvoices(selectedIds, selectedDate) > 0)
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(1, "Selected invoices approved successfully", null);
                }
                else
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(0, "Invoices have no data", null);
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
