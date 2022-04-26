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
            var invoiceidValue = Enumerable.FirstOrDefault(objDataAccess.InsertInvoice(0, model.InvoiceReference, model.InvoiceDate, model.CustomHeader1, model.CustomHeader2, model.CustomHeader3, model.CustomHeader4, model.CustomHeader5)).InvoiceId;
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

        public dynamic GetAllInvoice(DateTime invoiceMonth)
        {
            AutoMapper.MapperConfiguration config = new AutoMapper.MapperConfiguration(cgf => cgf.CreateMap<GetInvoices_Result, Invoices>());
            Mapper mapper = new Mapper(config);
            var activeSupplierList = objDataAccess.GetSupplier(true).ToList();
            AutoMapper.MapperConfiguration supplierConfig = new AutoMapper.MapperConfiguration(cgf => cgf.CreateMap<GetSupplier_Result, GetInvoiceDetails_Result>());
            Mapper supplierMapper = new Mapper(supplierConfig);
            Invoices invoices = mapper.Map<Invoices>(objDataAccess.GetInvoices(invoiceMonth).FirstOrDefault());
            if (invoices != null)
            {
                invoices.InvoiceDetails = objDataAccess.GetInvoiceDetails(invoices.InvoiceId).ToList();
            }
            else
            {
                invoices = new Invoices()
                {
                    InvoiceDetails = new List<GetInvoiceDetails_Result>(),
                    VATPercent = objDataAccess.GetVATPercent().FirstOrDefault().DefaultVAT
                };
            }

            foreach (var supplier in activeSupplierList)
            {
                if (invoices.InvoiceDetails.Where(i => i.SupplierId == supplier.SupplierId).FirstOrDefault() == null)
                {
                    invoices.InvoiceDetails.Add(supplierMapper.Map<GetInvoiceDetails_Result>(supplier));
                }
            }
            return invoices;
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
