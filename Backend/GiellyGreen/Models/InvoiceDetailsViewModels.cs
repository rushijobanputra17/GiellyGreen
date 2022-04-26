using System;

namespace GiellyGreen.Models
{
    public class InvoiceDetailsViewModels
    {

        public int? InvoiceDetailId { get; set; }
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
    }
}