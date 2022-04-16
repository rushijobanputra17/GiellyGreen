using AutoMapper;
using DataAccessLayer.Model;
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
        public static MapperConfiguration config = new MapperConfiguration(cgf => cgf.CreateMap<SupplierViewModel, Supplier>());
        public static Mapper mapper = new Mapper(config);
       public static Team2_GiellyGreenEntities objDataAccess = new Team2_GiellyGreenEntities();

        public JsonResponse objResponse;

        // GET suppliers
        public JsonResponse Get(bool isActive)
        {
            try
            {
                var suppliers = supplierRepository.GetSuppliers(isActive);/*bjDataAccess.GetSupplier(isActive).ToList(); *///
                objResponse = JsonResponseHelper.GetJsonResponse(1, "Records found : " + suppliers.Count(), suppliers);
            }
            catch (Exception ex)
            {
                objResponse = JsonResponseHelper.GetJsonResponse(2, "Exception", ex.Message);
            }

            return objResponse;
        }

        // POST api/values
        public JsonResponse Post(SupplierViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if (supplierRepository.AddSupplier(mapper.Map<Supplier>(model)) == 1)
                    {
                        objResponse = JsonResponseHelper.GetJsonResponse(1, "Record added successfully", model);
                    }
                    else
                    {
                        objResponse = JsonResponseHelper.GetJsonResponse(0, "There was an error while adding record", null);
                    }
                }
                else
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(0, "There was an error while adding record", ModelState.Values.SelectMany(v => v.Errors));
                }
                
            }
            catch(Exception ex)
            {
                objResponse = JsonResponseHelper.GetJsonResponse(2, "Error",ex.Message);
            }
            return objResponse;


        }

        //public JsonResponse Post(SupplierViewModel model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var product = supplierRepository.AddSupplier(mapper.Map<Supplier>(model));
        //            objResponse = .GetResponse(1, "Record added successfully", product);
        //        }
        //        else
        //        {
        //            objResponse = Helpers.ResponseHelper.GetResponse(0, "Error", ModelState.Values.SelectMany(v => v.Errors));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        objResponse = Helpers.ResponseHelper.GetResponse(2, "Exception", ex.Message);
        //    }

        //    return objResponse;
        //}

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE suppliers/delete/5
        public JsonResponse Delete(int id)
        {
            try
            {
                var responseStatus = supplierRepository.DeleteSupplier(id);
                if (responseStatus == 1)
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(1, "Record deleted successfully", id);
                }
                else if (responseStatus == 2)
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

        //PATCH suppliers/update/5
        public JsonResponse Patch(bool status, int supplierId)
        {
            try
            {
                if (supplierRepository.UpdateStatus(status, supplierId) == 1)
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(1, "Status updated successfully", supplierId);
                }
                else
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(0, "Something went wrong", null);
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
