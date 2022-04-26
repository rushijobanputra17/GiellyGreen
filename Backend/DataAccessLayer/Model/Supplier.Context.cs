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
    
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
    
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
    
        public virtual ObjectResult<GetSupplier_Result> GetSupplier(Nullable<bool> status)
        {
            var statusParameter = status.HasValue ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetSupplier_Result>("GetSupplier", statusParameter);
        }
    
        public virtual int InsertUpdateInvoiceDetails(Nullable<int> invoiceDetailId, Nullable<int> supplierId, Nullable<decimal> hairServices, Nullable<decimal> beautyServices, Nullable<decimal> customService1, Nullable<decimal> customService2, Nullable<decimal> customService3, Nullable<decimal> customService4, Nullable<decimal> customService5, Nullable<decimal> net, Nullable<decimal> vAT, Nullable<decimal> gross, Nullable<decimal> advancePaid, Nullable<decimal> balanceDue, Nullable<bool> approved, Nullable<int> invoiceId)
        {
            var invoiceDetailIdParameter = invoiceDetailId.HasValue ?
                new ObjectParameter("InvoiceDetailId", invoiceDetailId) :
                new ObjectParameter("InvoiceDetailId", typeof(int));
    
            var supplierIdParameter = supplierId.HasValue ?
                new ObjectParameter("SupplierId", supplierId) :
                new ObjectParameter("SupplierId", typeof(int));
    
            var hairServicesParameter = hairServices.HasValue ?
                new ObjectParameter("HairServices", hairServices) :
                new ObjectParameter("HairServices", typeof(decimal));
    
            var beautyServicesParameter = beautyServices.HasValue ?
                new ObjectParameter("BeautyServices", beautyServices) :
                new ObjectParameter("BeautyServices", typeof(decimal));
    
            var customService1Parameter = customService1.HasValue ?
                new ObjectParameter("CustomService1", customService1) :
                new ObjectParameter("CustomService1", typeof(decimal));
    
            var customService2Parameter = customService2.HasValue ?
                new ObjectParameter("CustomService2", customService2) :
                new ObjectParameter("CustomService2", typeof(decimal));
    
            var customService3Parameter = customService3.HasValue ?
                new ObjectParameter("CustomService3", customService3) :
                new ObjectParameter("CustomService3", typeof(decimal));
    
            var customService4Parameter = customService4.HasValue ?
                new ObjectParameter("CustomService4", customService4) :
                new ObjectParameter("CustomService4", typeof(decimal));
    
            var customService5Parameter = customService5.HasValue ?
                new ObjectParameter("CustomService5", customService5) :
                new ObjectParameter("CustomService5", typeof(decimal));
    
            var netParameter = net.HasValue ?
                new ObjectParameter("Net", net) :
                new ObjectParameter("Net", typeof(decimal));
    
            var vATParameter = vAT.HasValue ?
                new ObjectParameter("VAT", vAT) :
                new ObjectParameter("VAT", typeof(decimal));
    
            var grossParameter = gross.HasValue ?
                new ObjectParameter("Gross", gross) :
                new ObjectParameter("Gross", typeof(decimal));
    
            var advancePaidParameter = advancePaid.HasValue ?
                new ObjectParameter("AdvancePaid", advancePaid) :
                new ObjectParameter("AdvancePaid", typeof(decimal));
    
            var balanceDueParameter = balanceDue.HasValue ?
                new ObjectParameter("BalanceDue", balanceDue) :
                new ObjectParameter("BalanceDue", typeof(decimal));
    
            var approvedParameter = approved.HasValue ?
                new ObjectParameter("Approved", approved) :
                new ObjectParameter("Approved", typeof(bool));
    
            var invoiceIdParameter = invoiceId.HasValue ?
                new ObjectParameter("InvoiceId", invoiceId) :
                new ObjectParameter("InvoiceId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertUpdateInvoiceDetails", invoiceDetailIdParameter, supplierIdParameter, hairServicesParameter, beautyServicesParameter, customService1Parameter, customService2Parameter, customService3Parameter, customService4Parameter, customService5Parameter, netParameter, vATParameter, grossParameter, advancePaidParameter, balanceDueParameter, approvedParameter, invoiceIdParameter);
        }
    
        public virtual ObjectResult<ApproveInvoices_Result> ApproveInvoices(string selectedIds, Nullable<System.DateTime> selectedDate)
        {
            var selectedIdsParameter = selectedIds != null ?
                new ObjectParameter("SelectedIds", selectedIds) :
                new ObjectParameter("SelectedIds", typeof(string));
    
            var selectedDateParameter = selectedDate.HasValue ?
                new ObjectParameter("SelectedDate", selectedDate) :
                new ObjectParameter("SelectedDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ApproveInvoices_Result>("ApproveInvoices", selectedIdsParameter, selectedDateParameter);
        }
    
        public virtual ObjectResult<GetInvoicesForPdf_Result> GetInvoicesForPdf(string selectedIds, Nullable<System.DateTime> selectedDate)
        {
            var selectedIdsParameter = selectedIds != null ?
                new ObjectParameter("SelectedIds", selectedIds) :
                new ObjectParameter("SelectedIds", typeof(string));
    
            var selectedDateParameter = selectedDate.HasValue ?
                new ObjectParameter("SelectedDate", selectedDate) :
                new ObjectParameter("SelectedDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetInvoicesForPdf_Result>("GetInvoicesForPdf", selectedIdsParameter, selectedDateParameter);
        }
    
        public virtual ObjectResult<GetInvoiceDetails_Result> GetInvoiceDetails(Nullable<int> invoiceId)
        {
            var invoiceIdParameter = invoiceId.HasValue ?
                new ObjectParameter("InvoiceId", invoiceId) :
                new ObjectParameter("InvoiceId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetInvoiceDetails_Result>("GetInvoiceDetails", invoiceIdParameter);
        }
    
        public virtual int InsertUpdateProfile(Nullable<int> profileId, string companyName, string addressLine, string city, string zipCode, string country, Nullable<decimal> defaultVAT)
        {
            var profileIdParameter = profileId.HasValue ?
                new ObjectParameter("ProfileId", profileId) :
                new ObjectParameter("ProfileId", typeof(int));
    
            var companyNameParameter = companyName != null ?
                new ObjectParameter("CompanyName", companyName) :
                new ObjectParameter("CompanyName", typeof(string));
    
            var addressLineParameter = addressLine != null ?
                new ObjectParameter("AddressLine", addressLine) :
                new ObjectParameter("AddressLine", typeof(string));
    
            var cityParameter = city != null ?
                new ObjectParameter("City", city) :
                new ObjectParameter("City", typeof(string));
    
            var zipCodeParameter = zipCode != null ?
                new ObjectParameter("ZipCode", zipCode) :
                new ObjectParameter("ZipCode", typeof(string));
    
            var countryParameter = country != null ?
                new ObjectParameter("Country", country) :
                new ObjectParameter("Country", typeof(string));
    
            var defaultVATParameter = defaultVAT.HasValue ?
                new ObjectParameter("DefaultVAT", defaultVAT) :
                new ObjectParameter("DefaultVAT", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertUpdateProfile", profileIdParameter, companyNameParameter, addressLineParameter, cityParameter, zipCodeParameter, countryParameter, defaultVATParameter);
        }
    
        public virtual ObjectResult<GetProfile_Result> GetProfile()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetProfile_Result>("GetProfile");
        }
    
        public virtual ObjectResult<GetInvoices_Result> GetInvoices(Nullable<System.DateTime> invoiceMonth)
        {
            var invoiceMonthParameter = invoiceMonth.HasValue ?
                new ObjectParameter("InvoiceMonth", invoiceMonth) :
                new ObjectParameter("InvoiceMonth", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetInvoices_Result>("GetInvoices", invoiceMonthParameter);
        }
    
        public virtual ObjectResult<InsertInvoice_Result> InsertInvoice(Nullable<int> invoiceId, string invoiceRef, Nullable<System.DateTime> invoiceDate, string customCol1, string customCol2, string customCol3, string customCol4, string customCol5)
        {
            var invoiceIdParameter = invoiceId.HasValue ?
                new ObjectParameter("InvoiceId", invoiceId) :
                new ObjectParameter("InvoiceId", typeof(int));
    
            var invoiceRefParameter = invoiceRef != null ?
                new ObjectParameter("InvoiceRef", invoiceRef) :
                new ObjectParameter("InvoiceRef", typeof(string));
    
            var invoiceDateParameter = invoiceDate.HasValue ?
                new ObjectParameter("InvoiceDate", invoiceDate) :
                new ObjectParameter("InvoiceDate", typeof(System.DateTime));
    
            var customCol1Parameter = customCol1 != null ?
                new ObjectParameter("CustomCol1", customCol1) :
                new ObjectParameter("CustomCol1", typeof(string));
    
            var customCol2Parameter = customCol2 != null ?
                new ObjectParameter("CustomCol2", customCol2) :
                new ObjectParameter("CustomCol2", typeof(string));
    
            var customCol3Parameter = customCol3 != null ?
                new ObjectParameter("CustomCol3", customCol3) :
                new ObjectParameter("CustomCol3", typeof(string));
    
            var customCol4Parameter = customCol4 != null ?
                new ObjectParameter("CustomCol4", customCol4) :
                new ObjectParameter("CustomCol4", typeof(string));
    
            var customCol5Parameter = customCol5 != null ?
                new ObjectParameter("CustomCol5", customCol5) :
                new ObjectParameter("CustomCol5", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InsertInvoice_Result>("InsertInvoice", invoiceIdParameter, invoiceRefParameter, invoiceDateParameter, customCol1Parameter, customCol2Parameter, customCol3Parameter, customCol4Parameter, customCol5Parameter);
        }
    }
}
