using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class Invoices
    {
        public int InvoiceId { get; set; }
        public string CustomHeader1 { get; set; }
        public string CustomHeader2 { get; set; }
        public string CustomHeader3 { get; set; }
        public string CustomHeader4 { get; set; }
        public string CustomHeader5 { get; set; }
        public string InvoiceRef { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<decimal> VATPercent { get; set; }

        public List<GetInvoiceDetails_Result> InvoiceDetails;
    }
}
