using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiellyGreen.Models
{
    public class EmailViewModel
    {

        public int SupplierId { get; set; }
        public string Logo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceRef { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string TAXReference { get; set; }
        public decimal? VAT { get; set; }
        public string BusinessAddress { get; set; }
        public decimal? HairServices { get; set; }
        public decimal? BeautyServices { get; set; }
        public decimal? CustomService1 { get; set; }
        public decimal? CustomService2 { get; set; }
        public decimal? CustomService3 { get; set; }
        public decimal? CustomService4 { get; set; }
        public decimal? CustomService5 { get; set; }
        public decimal? Net { get; set; }
        public decimal? AdvancePaid { get; set; }
        public decimal? BalanceDue { get; set; }
    }
}