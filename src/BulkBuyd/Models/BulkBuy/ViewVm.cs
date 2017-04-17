using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BulkBuyd.Domain.DTOs;
using Markdig;

namespace BulkBuyd.Models.BulkBuy
{
    public class ViewVm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ClosingDate { get; set; }
        public string OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string DescriptionHtml => Markdown.ToHtml(Description);

        public string RedirectUrl { get; set; }
        public bool IsLoggedIn { get; set; }

        public bool IsPledged { get; set; }
        public decimal PledgeAmount { get; set; }
        public string OrderDetails { get; set; }

        public IEnumerable<PledgeVm> Pledges { get; set; }

        public ViewVm()
        {
            Pledges = new List<PledgeVm>();
        }

        public static Expression<Func<Domain.Entities.BulkBuy, ViewVm>> Projection
        {
            get
            {
                return x => new ViewVm()
                {
                    Id = x.DisplayId,
                    Name = x.Name,
                    Description = x.Description,
                    ClosingDate = x.DueDate,
                    OwnerName = x.Owner.DisplayName,
                    OwnerId = x.OwnerId,
                    Pledges = x.Pledges.AsQueryable().Select(PledgeVm.Projection)
                };
            }
        }

        public static ViewVm FromEntity(Domain.Entities.BulkBuy entity)
        {
            return Projection.Compile().Invoke(entity);
        }
    }
}
