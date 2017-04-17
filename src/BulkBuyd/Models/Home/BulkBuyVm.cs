using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BulkBuyd.Domain.DTOs;
using BulkBuyd.Services.Helpers;
using Markdig;

namespace BulkBuyd.Models.Home
{
    public class BulkBuyVm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        public string OwnerId { get; set; }
        public string OwnerName { get; set; }

        public string DescriptionSummary => StripHtmlHelper.StripHtml(Markdown.ToHtml(Description));

        public static Expression<Func<BulkBuyDto, BulkBuyVm>> Projection
        {
            get
            {
                return x => new BulkBuyVm()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    DueDate = x.DueDate,
                    OwnerId = x.OwnerId,
                    OwnerName = x.OwnerName
                };
            }
        }
    }
}
