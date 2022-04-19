using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{
    public interface IMonthlyInvoiceRepository
    {
        dynamic GetAllInvoice(DateTime InvoiceMonth);

        int AddInvoice(dynamic invoiceModel);
    }
}
