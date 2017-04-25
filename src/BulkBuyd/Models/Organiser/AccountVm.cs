using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BulkBuyd.Models.Organiser
{
    public class AccountVm
    {
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string Postcode { get; set; }
        [Required]
        public string Suburb { get; set; }
        [Required]
        public string MobileNumber { get; set; }
    }
}
