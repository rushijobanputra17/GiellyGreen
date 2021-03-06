using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GiellyGreen.Models
{
    public class InvoiceModels
    {
        public string CustomHeader1 { get; set; }
        public string CustomHeader2 { get; set; }
        public string CustomHeader3 { get; set; }
        public string CustomHeader4 { get; set; }
        public string CustomHeader5 { get; set; }

        [Required]
        public string InvoiceReference { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public int InvoiceId { get; set; }

        public List<InvoiceDetailsViewModels> invoiceDetails;
    }
}