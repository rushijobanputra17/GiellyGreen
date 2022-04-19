using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    internal class MonthlyInvoiceRepository : IMonthlyInvoiceRepository
    {
        private Team2_GiellyGreenEntities objDataAccess;

        public MonthlyInvoiceRepository()
        {
            objDataAccess = new Team2_GiellyGreenEntities();
        }

        public List<object> GetSuppliers(DateTime InvoiceMonth)
        {
            return objDataAccess.GetAllInvoice(InvoiceMonth);
        }

    }
}
