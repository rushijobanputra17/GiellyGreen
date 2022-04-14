using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{
    internal interface ISupplierRepository
    {
        List<Supplier> GetSuppliers(bool isActive);

        bool AddSupplier(Supplier supplier);

        bool UpdateSupplier(Supplier supplierToUpdate, Supplier updatedSupplier, int supplierId);

        bool DeleteSupplier(int supplierId);
    }
}
