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
            var invoiceidValue = Enumerable.FirstOrDefault(objDataAccess.InsertInvoice(0, model.InvoiceRef, model.InvoiceDate, model.CustomHeader1, model.CustomHeader2, model.CustomHeader3, model.CustomHeader4, model.CustomHeader5)).InvoiceId;
            if (invoiceidValue == 0)
            {
                return 0;
            }
            var invoiceDetail = 0;
            foreach (dynamic invoieDetail in model.invoiceDetails)
            {
                if (invoieDetail.Net > 0)
                {
                    invoiceDetail = objDataAccess.InsertUpdateInvoiceDetails(0, invoieDetail.SupplierId, invoieDetail.HairServices, invoieDetail.BeautyServices, invoieDetail.CustomService1, invoieDetail.CustomService2, invoieDetail.CustomService3, invoieDetail.CustomService4, invoieDetail.CustomService5, invoieDetail.Net, invoieDetail.VAT, invoieDetail.Gross, invoieDetail.AdvancePaid, invoieDetail.BalanceDue, invoieDetail.Approved, invoiceidValue);
                }

            }
            return invoiceDetail;
        }

        public dynamic GetAllInvoice(DateTime InvoiceMonth)
        {
            AutoMapper.MapperConfiguration configList = new AutoMapper.MapperConfiguration(cgf => cgf.CreateMap<GetSupplier_Result, GetAllInvoice_Result>());
            Mapper mapper = new Mapper(configList);
            var MonthlyInvoiceList = objDataAccess.GetAllInvoice(InvoiceMonth).ToList();
            var GetActiveSupplierList = objDataAccess.GetSupplier(true).ToList();
            foreach (var supplier in GetActiveSupplierList)
            {
                var objectMonth = MonthlyInvoiceList.Where(i => i.SupplierId == supplier.SupplierId).FirstOrDefault();
                if (MonthlyInvoiceList.Where(i => i.SupplierId == supplier.SupplierId).FirstOrDefault() == null)
                {
                    MonthlyInvoiceList.Add(mapper.Map<GetAllInvoice_Result>(supplier));
                }
            }

            return MonthlyInvoiceList;
        }

        public int? ApproveSelectedInvoices(List<int> selectedIds, DateTime selectedDate)
        {
            return objDataAccess.ApproveInvoices(String.Join(",", selectedIds), selectedDate).FirstOrDefault().response;
        }

        public dynamic GetInvoicesForPDF(DateTime invoiceDate, List<int> selectedSupplierIds)
        {
            return objDataAccess.GetInvoicesForPdf(String.Join(",", selectedSupplierIds), invoiceDate).ToList();
        }

    }
}
