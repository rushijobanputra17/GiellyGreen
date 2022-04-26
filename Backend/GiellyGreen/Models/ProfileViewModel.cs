using System;
using System.ComponentModel.DataAnnotations;

namespace GiellyGreen.Models
{
    public class ProfileViewModel
    {
        public int ProfileId { get; set; }

        [Required]
        public string CompanyName { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public Nullable<decimal> DefaultVAT { get; set; }
    }
}