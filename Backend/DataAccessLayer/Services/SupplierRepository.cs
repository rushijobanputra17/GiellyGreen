using AutoMapper;
using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Services
{
    public class SupplierRepository : ISupplierRepository
    {
        private Team2_GiellyGreenEntities objDataAccess;

        public SupplierRepository()
        {
            objDataAccess = new Team2_GiellyGreenEntities();
        }

        public int AddSupplier(Supplier supplier)
        {
            return objDataAccess.InsertUpdateSupplier(null, supplier.SupplierName, supplier.SupplierReferenceNumber, supplier.BusinessAddress, supplier.EmailAddress, supplier.PhoneNumber, supplier.CompanyRegisteredNumber, supplier.VATNumber, supplier.TAXReference, supplier.CompanyRegisteredAddress, supplier.IsActive, supplier.Logo);
        }

        public int UpdateSupplier(Supplier supplier)
        {
            return objDataAccess.InsertUpdateSupplier(supplier.SupplierId, supplier.SupplierName, supplier.SupplierReferenceNumber, supplier.BusinessAddress, supplier.EmailAddress, supplier.PhoneNumber, supplier.CompanyRegisteredNumber, supplier.VATNumber, supplier.TAXReference, supplier.CompanyRegisteredAddress, supplier.IsActive, supplier.Logo);
        }

        public int? DeleteSupplier(int supplierId)
        {
            return objDataAccess.DeleteSupplier(supplierId).FirstOrDefault().ResponseStatus;

        }

        public List<Supplier> GetSuppliers(bool isActive)
        {
            AutoMapper.MapperConfiguration configList = new AutoMapper.MapperConfiguration(cgf => cgf.CreateMap<GetSupplier_Result, Supplier>());
            Mapper mapper = new Mapper(configList);
            return mapper.Map<List<Supplier>>(objDataAccess.GetSupplier(isActive).ToList());
        }

        public int UpdateStatus(bool status, int supplierId)
        {
            return objDataAccess.UpdateStatus(status, supplierId);
        }
    }
}
