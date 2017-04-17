using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BulkBuyd.Domain.Entities;
using Markdig;

namespace BulkBuyd.Domain.DTOs
{
    public class BulkBuyDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        public string OwnerId { get; set; }
        public string OwnerName { get; set; }

        public static Expression<Func<BulkBuy, BulkBuyDto>> Projection
        {
            get
            {
                return x => new BulkBuyDto()
                {
                    Id = x.DisplayId,
                    Name = x.Name,
                    Description = x.Description,
                    DueDate = x.DueDate,
                    OwnerId = x.OwnerId,
                    OwnerName = x.Owner.DisplayName
                };
            }
        }
    }
}
