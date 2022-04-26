using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _SupplierName = value.Trim();
                    _SupplierName = Regex.Replace(value.Trim(), @"\s+", " ");
                }
            }
        }
        private string _SupplierReferenceNumber;
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Please enter valid supplier reference number")]
        [MaxLength(15)]
        public string SupplierReferenceNumber
        {
            get { return _SupplierReferenceNumber; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _SupplierReferenceNumber = value.Trim();
                    _SupplierReferenceNumber = Regex.Replace(value.Trim(), @"\s+", " ");
                }
            }
        }

        private string _BusinessAddress;
        [MaxLength(150)]
        public string BusinessAddress
        {
            get { return _BusinessAddress; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _BusinessAddress = value.Trim();
                    _BusinessAddress = Regex.Replace(value.Trim(), @"\s+", " ");
                }
            }
        }

        private string _EmailAddress;
        [Required]
        [RegularExpression(@"^([A-Za-z0-9][^'!&\\#*$%^?<>()+=:;`~\[\]{}|/,₹€@ ][a-zA-z0-9-._][^!&\\#*$%^?<>()+=:;`~\[\]{}|/,₹€@ ]*\@[a-zA-Z0-9][^!&@\\#*$%^?<>()+=':;~`.\[\]{}|/,₹€ ]*\.[a-zA-Z]{2,6})$", ErrorMessage = "Invalid Email address")]
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _EmailAddress = value.Trim();
                    _EmailAddress = Regex.Replace(value.Trim(), @"\s+", " ");
                }
            }
        }


        private string _PhoneNumber;
        [MaxLength(15)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _PhoneNumber = value.Trim();
                    _PhoneNumber = Regex.Replace(value.Trim(), @"\s+", " ");
                }
            }
        }

        private string _CompanyRegisteredNumber;
        [MaxLength(15)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid Register number.")]
        public string CompanyRegisteredNumber
        {
            get { return _CompanyRegisteredNumber; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _CompanyRegisteredNumber = value.Trim();
                    _CompanyRegisteredNumber = Regex.Replace(value.Trim(), @"\s+", " ");
                }
            }
        }

        private string _VATNumber;
        [MaxLength(15)]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Invalid VAT Number.")]
        public string VATNumber
        {
            get { return _VATNumber; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _VATNumber = value.Trim();
                    _VATNumber = Regex.Replace(value.Trim(), @"\s+", " ");
                }
            }
        }

        private string _TAXReference;
        [MaxLength(15)]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Invalid TAX Reference .")]
        public string TAXReference
        {
            get { return _TAXReference; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _TAXReference = value.Trim();
                    _TAXReference = Regex.Replace(value.Trim(), @"\s+", " ");
                }
            }
        }

        private string _CompanyRegisteredAddress;
        [MaxLength(150)]
        public string CompanyRegisteredAddress
        {
            get { return _CompanyRegisteredAddress; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _CompanyRegisteredAddress = value.Trim();
                    _CompanyRegisteredAddress = Regex.Replace(value.Trim(), @"\s+", " ");
                }
            }
        }
        public bool IsActive { get; set; } = false;
        public string Logo { get; set; } = null;
        public Nullable<bool> IsInvoicePresent { get; set; } = false;
    }
}