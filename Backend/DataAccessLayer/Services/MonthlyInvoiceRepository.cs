using AutoMapper;
using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public class MonthlyInvoiceRepository : IMonthlyInvoiceRepository
    {
        private Team2_GiellyGreenEntities objDataAccess;

        public MonthlyInvoiceRepository()
        {
            objDataAccess = new Team2_GiellyGreenEntities();
        }

        public int AddInvoice(dynamic model)
        {
            var rtv2 = Enumerable.FirstOrDefault(objDataAccess.InsertInvoice(0, model.InvoiceRef, model.InvoiceDate, model.CustomHeader1, model.CustomHeader2, model.CustomHeader3, model.CustomHeader4, model.CustomHeader5)).InvoiceId;
            if(rtv2 == 0)
            {
                return 0;
            }
            var invoiceDetail = 0;
            foreach (dynamic invoieDetail in model.invoiceDetails)
            {
                invoiceDetail =  objDataAccess.InsertUpdateInvoiceDetails(0, invoieDetail.SupplierId, invoieDetail.HairServices, invoieDetail.BeautyServices, invoieDetail.CustomService1, invoieDetail.CustomService2, invoieDetail.CustomService3, invoieDetail.CustomService4, invoieDetail.CustomService5, invoieDetail.Net, invoieDetail.VAT, invoieDetail.Gross, invoieDetail.AdvancePaid, invoieDetail.BalanceDue, invoieDetail.Approved, rtv2);
            }
            return invoiceDetail;
        }

        public dynamic GetAllInvoice(DateTime InvoiceMonth)
        {
            return objDataAccess.GetAllInvoice(InvoiceMonth).ToList();
        }
        


    }
}
