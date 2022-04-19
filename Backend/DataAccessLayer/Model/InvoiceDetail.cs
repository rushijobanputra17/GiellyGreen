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
    
    public partial class InvoiceDetail
    {
        public int InvoiceDetailId { get; set; }
        public int SupplierId { get; set; }
        public Nullable<decimal> HairServices { get; set; }
        public Nullable<decimal> BeautyServices { get; set; }
        public Nullable<decimal> CustomService1 { get; set; }
        public Nullable<decimal> CustomService2 { get; set; }
        public Nullable<decimal> CustomService3 { get; set; }
        public Nullable<decimal> CustomService4 { get; set; }
        public Nullable<decimal> CustomService5 { get; set; }
        public Nullable<decimal> Net { get; set; }
        public Nullable<decimal> VAT { get; set; }
        public Nullable<decimal> Gross { get; set; }
        public Nullable<decimal> AdvancePaid { get; set; }
        public Nullable<decimal> BalanceDue { get; set; }
        public Nullable<bool> Approved { get; set; }
        public int InvoiceId { get; set; }
    
        public virtual Supplier Supplier { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
