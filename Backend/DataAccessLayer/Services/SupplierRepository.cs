using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }

        public int DeleteSupplier(int supplierId)
        {
            return objDataAccess.DeleteSupplier(supplierId);
        }

        public List<Supplier> GetSuppliers(bool isActive)
        {
            if(!isActive)
            {
                return objDataAccess.Suppliers.ToList();
            }
            return objDataAccess.Suppliers.Where(x => x.IsActive == isActive).ToList();
        }
    }
}
