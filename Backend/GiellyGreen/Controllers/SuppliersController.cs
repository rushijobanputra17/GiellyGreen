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
    [RoutePrefix("suppliers")]
    public class SuppliersController : ApiController
    {
        public static SupplierRepository supplierRepository = new SupplierRepository();

        public JsonResponse objResponse;

        // GET suppliers
        public JsonResponse Get(bool isActive)
        {
            try
            {
                var suppliers = supplierRepository.GetSuppliers(isActive);
                objResponse = JsonResponseHelper.GetJsonResponse(1, "Records found : " + suppliers.Count(), suppliers);
            }
            catch (Exception ex)
            {
                objResponse = JsonResponseHelper.GetJsonResponse(2, "Exception", ex.Message);
            }

            return objResponse;
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE suppliers/delete/5
        public JsonResponse Delete(int id)
        {
            try
            {
                if (supplierRepository.DeleteSupplier(id) == 1)
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(1, "Record deleted successfully", id);
                }
                else if(supplierRepository.DeleteSupplier(id) == 2)
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(3, "Delete failed because invoice is present for the supplier", null);
                }
                else
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(0, "Delete failed because no such supplier exist", null);
                }
            }
            catch (Exception ex)
            {
                objResponse = JsonResponseHelper.GetJsonResponse(2, "Exception", ex.Message);
            }

            return objResponse;
        }
    }
}
