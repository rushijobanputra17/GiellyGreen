using AutoMapper;
using DataAccessLayer.Model;
using DataAccessLayer.Services;
using GiellyGreen.Helpers;
using GiellyGreen.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GiellyGreen.Controllers
{
    //[Authorize]
    //[RoutePrefix("suppliers")]
    public class SuppliersController : ApiController
    {
        public static SupplierRepository supplierRepository = new SupplierRepository();
        public static AutoMapper.MapperConfiguration config = new AutoMapper.MapperConfiguration(cgf => cgf.CreateMap<SupplierViewModel, Supplier>());
        public static Mapper mapper = new Mapper(config);
        public static Team2_GiellyGreenEntities objDataAccess = new Team2_GiellyGreenEntities();

        public JsonResponse objResponse;

        // GET suppliers
        public JsonResponse Get(bool isActive)
        {
            try
            {
                var suppliers = supplierRepository.GetSuppliers(isActive); /*objDataAccess.GetSupplier(isActive).ToList();*////* *///
                String path = HttpContext.Current.Server.MapPath("~/ImageStorage");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                suppliers.ForEach(supplier =>
                {
                    if (!string.IsNullOrEmpty(supplier.Logo) && supplier.Logo != "null")
                    {
                        string imgPath = Path.Combine(path, supplier.Logo);
                        byte[] imgBytes = File.ReadAllBytes(imgPath);
                        supplier.Logo = Convert.ToBase64String(imgBytes);
                    }
                });

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
                model.IsInvoicePresent = false;
                if (ModelState.IsValid)
                {
                    if (model.Logo != null)
                    {
                        model.Logo = SupplierHelper.SetModelLogo(model.SupplierName, model.Logo);
                    }

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

        // PUT api/values/5
        public JsonResponse Put(int id, SupplierViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Logo != null)
                    {
                        model.Logo = SupplierHelper.SetModelLogo(model.SupplierName, model.Logo);
                    }

                    model.SupplierId = id;
                    if (supplierRepository.UpdateSupplier(mapper.Map<Supplier>(model)) == 1)
                    {
                        objResponse = JsonResponseHelper.GetJsonResponse(1, "Record updated successfully", model);
                    }
                    else
                    {
                        objResponse = JsonResponseHelper.GetJsonResponse(0, "There was an error while updating record", null);
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
                if (ex.InnerException.Message != null)
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
                if (ex.InnerException.Message != null)
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
