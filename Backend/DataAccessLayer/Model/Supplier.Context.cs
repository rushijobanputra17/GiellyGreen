﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Team2_GiellyGreenEntities : DbContext
    {
        public Team2_GiellyGreenEntities()
            : base("name=Team2_GiellyGreenEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
    
        public virtual ObjectResult<DeleteSupplier_Result> DeleteSupplier(Nullable<int> supplierId)
        {
            var supplierIdParameter = supplierId.HasValue ?
                new ObjectParameter("SupplierId", supplierId) :
                new ObjectParameter("SupplierId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<DeleteSupplier_Result>("DeleteSupplier", supplierIdParameter);
        }
    
        public virtual int InsertUpdateSupplier(Nullable<int> id, string supplierName, string supplierReferenceNumber, string businessAddress, string emailAddress, string phoneNumber, string companyRegisteredNumber, string vATNumber, string tAXReference, string companyRegisteredAddress, Nullable<bool> isActive, string logo)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var supplierNameParameter = supplierName != null ?
                new ObjectParameter("SupplierName", supplierName) :
                new ObjectParameter("SupplierName", typeof(string));
    
            var supplierReferenceNumberParameter = supplierReferenceNumber != null ?
                new ObjectParameter("SupplierReferenceNumber", supplierReferenceNumber) :
                new ObjectParameter("SupplierReferenceNumber", typeof(string));
    
            var businessAddressParameter = businessAddress != null ?
                new ObjectParameter("BusinessAddress", businessAddress) :
                new ObjectParameter("BusinessAddress", typeof(string));
    
            var emailAddressParameter = emailAddress != null ?
                new ObjectParameter("EmailAddress", emailAddress) :
                new ObjectParameter("EmailAddress", typeof(string));
    
            var phoneNumberParameter = phoneNumber != null ?
                new ObjectParameter("PhoneNumber", phoneNumber) :
                new ObjectParameter("PhoneNumber", typeof(string));
    
            var companyRegisteredNumberParameter = companyRegisteredNumber != null ?
                new ObjectParameter("CompanyRegisteredNumber", companyRegisteredNumber) :
                new ObjectParameter("CompanyRegisteredNumber", typeof(string));
    
            var vATNumberParameter = vATNumber != null ?
                new ObjectParameter("VATNumber", vATNumber) :
                new ObjectParameter("VATNumber", typeof(string));
    
            var tAXReferenceParameter = tAXReference != null ?
                new ObjectParameter("TAXReference", tAXReference) :
                new ObjectParameter("TAXReference", typeof(string));
    
            var companyRegisteredAddressParameter = companyRegisteredAddress != null ?
                new ObjectParameter("CompanyRegisteredAddress", companyRegisteredAddress) :
                new ObjectParameter("CompanyRegisteredAddress", typeof(string));
    
            var isActiveParameter = isActive.HasValue ?
                new ObjectParameter("IsActive", isActive) :
                new ObjectParameter("IsActive", typeof(bool));
    
            var logoParameter = logo != null ?
                new ObjectParameter("Logo", logo) :
                new ObjectParameter("Logo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertUpdateSupplier", idParameter, supplierNameParameter, supplierReferenceNumberParameter, businessAddressParameter, emailAddressParameter, phoneNumberParameter, companyRegisteredNumberParameter, vATNumberParameter, tAXReferenceParameter, companyRegisteredAddressParameter, isActiveParameter, logoParameter);
        }
    
        public virtual int UpdateStatus(Nullable<bool> status, Nullable<int> id)
        {
            var statusParameter = status.HasValue ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(bool));
    
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateStatus", statusParameter, idParameter);
        }
    
        public virtual ObjectResult<GetAllInvoiceDetail_Result> GetAllInvoiceDetail(Nullable<System.DateTime> invoiceMonth)
        {
            var invoiceMonthParameter = invoiceMonth.HasValue ?
                new ObjectParameter("InvoiceMonth", invoiceMonth) :
                new ObjectParameter("InvoiceMonth", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllInvoiceDetail_Result>("GetAllInvoiceDetail", invoiceMonthParameter);
        }
    
        public virtual ObjectResult<GetSupplier_Result> GetSupplier(Nullable<bool> status)
        {
            var statusParameter = status.HasValue ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetSupplier_Result>("GetSupplier", statusParameter);
        }
    }
}
