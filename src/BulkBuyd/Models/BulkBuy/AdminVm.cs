using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BulkBuyd.Models.BulkBuy
{
    public class AdminVm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ClosingDate { get; set; }

        public static Expression<Func<Domain.Entities.BulkBuy, AdminVm>> Projection
        {
            get
            {
                return x => new AdminVm()
                {
                    Id = x.DisplayId,
                    Name = x.Name,
                    Description = x.Description,
                    ClosingDate = x.DueDate,
                };
            }
        }

        public static AdminVm FromEntity(Domain.Entities.BulkBuy entity)
        {
            return Projection.Compile().Invoke(entity);
        }

    }
}
