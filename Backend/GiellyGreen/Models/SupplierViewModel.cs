using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace GiellyGreen.Models
{
    public class SupplierViewModel
    {
        public int SupplierId { get; set; }

        private string _SupplierName;
        [Required(ErrorMessage = "Supplier name is required")]
        public string SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = Regex.Replace(value.Trim(), @"\s+", " "); }
        }
        private string _SupplierReferenceNumber;
        [Required(ErrorMessage = "Supplier reference number is required")]
        //[RegularExpression("@^[a-zA-Z0-9]*$", ErrorMessage = "Invalid Supplier reference number")]
        public string SupplierReferenceNumber
        {
            get { return _SupplierReferenceNumber; }
            set { _SupplierReferenceNumber = value.Trim(); }
        }

        private string _BusinessAddress;
        [MaxLength(150)]
        public string BusinessAddress
        {
            get { return _BusinessAddress; }
            set { _BusinessAddress = value.Trim(); }
        }

        [Required]
        [RegularExpression(@"^([A-Za-z0-9][^'!&\\#*$%^?<>()+=:;`~\[\]{}|/,₹€@ ][a-zA-z0-9-._][^!&\\#*$%^?<>()+=:;`~\[\]{}|/,₹€@ ]*\@[a-zA-Z0-9][^!&@\\#*$%^?<>()+=':;~`.\[\]{}|/,₹€ ]*\.[a-zA-Z]{2,6})$", ErrorMessage = "Invalid Email address")]
        public string EmailAddress { get; set; }

        [MaxLength(15)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        [MaxLength(15)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid Register number.")]
        public string CompanyRegisteredNumber { get; set; }

        [MaxLength(15)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid VAT Number.")]
        public string VATNumber { get; set; }

        [MaxLength(15)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid TAX Reference .")]
        public string TAXReference { get; set; }

        [MaxLength(150)]
        public string CompanyRegisteredAddress { get; set; }
        public bool IsActive { get; set; }
        public string Logo { get; set; }
        public Nullable<bool> IsInvoicePresent { get; set; }
    }
}