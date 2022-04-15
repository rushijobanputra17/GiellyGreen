//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            this.Invoices = new HashSet<Invoice>();
        }
    
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierReferenceNumber { get; set; }
        public string BusinessAddress { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyRegisteredNumber { get; set; }
        public string VATNumber { get; set; }
        public string TAXReference { get; set; }
        public string CompanyRegisteredAddress { get; set; }
        public bool IsActive { get; set; }
        public string Logo { get; set; }
        public Nullable<bool> IsInvoicePresent { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
