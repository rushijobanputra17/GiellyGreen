using DataAccessLayer.Model;
using System.Collections.Generic;

namespace GiellyGreen.Models
{
    public class CombinePdfViewModel
    {
        public List<GetInvoicesForPdf_Result>  InvoiceDetails;

        public GetProfile_Result CompanyProfile;
    }
}