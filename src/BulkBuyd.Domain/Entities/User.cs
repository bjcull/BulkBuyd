using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BulkBuyd.Domain.Entities
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }

        public string PinchPayerId { get; set; }
    }
}
