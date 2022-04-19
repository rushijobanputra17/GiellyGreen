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
        public int SupplierId { get; set; } = 0;

        private string _SupplierName;
        [Required(ErrorMessage = "Supplier name is required")]
        public string SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = Regex.Replace(value.Trim(), @"\s+", " "); }
        }
        private string _SupplierReferenceNumber;
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Please enter valid supplier reference number")]
        [MaxLength(15)]
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
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Invalid VAT Number.")]
        public string VATNumber { get; set; }

        [MaxLength(15)]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Invalid TAX Reference .")]
        public string TAXReference { get; set; }

        [MaxLength(150)]
        public string CompanyRegisteredAddress { get; set; }
        public bool IsActive { get; set; } = false;
        public string Logo { get; set; } = null;
        public Nullable<bool> IsInvoicePresent { get; set; } = false;
    } 
}