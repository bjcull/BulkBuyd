using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BulkBuyd.Data
{
    public class TemporaryDbContextFactory : IDbContextFactory<BulkBuydContext>
    {
        public BulkBuydContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<BulkBuydContext>();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=pinchdb;Trusted_Connection=True;MultipleActiveResultSets=true",
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(BulkBuydContext).GetTypeInfo().Assembly.GetName().Name));
            return new BulkBuydContext(builder.Options);
        }
    }
}
