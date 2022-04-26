using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiellyGreen.Models
{
    public class CombinePdfViewModel
    {
        public List<GetInvoicesForPdf_Result>  InvoiceDetails;

        public GetProfile_Result CompanyProfile;
    }
}