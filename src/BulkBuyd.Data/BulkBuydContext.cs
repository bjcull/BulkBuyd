using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkBuyd.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BulkBuyd.Data
{
    public class BulkBuydContext : IdentityDbContext<User>
    {
        public BulkBuydContext(DbContextOptions options)
            : base(options)
        {            
        }

        public DbSet<BulkBuy> BulkBuys { get; set; }
        public DbSet<Pledge> Pledges { get; set; }
    }
}
