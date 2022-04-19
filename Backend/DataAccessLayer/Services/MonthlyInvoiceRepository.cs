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

        public List<object> GetAllInvoice(DateTime InvoiceMonth)
        {
            AutoMapper.MapperConfiguration configList = new AutoMapper.MapperConfiguration(cgf => cgf.CreateMap<GetAllInvoice_Result, object>());
            Mapper mapper = new Mapper(configList);
            return mapper.Map<List<object>>(objDataAccess.GetAllInvoice(InvoiceMonth).ToList());
        }

    }
}
