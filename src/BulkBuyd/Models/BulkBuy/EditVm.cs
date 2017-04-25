using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BulkBuyd.Models.BulkBuy
{
    public class EditVm
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime ClosingDate { get; set; }

        public static Expression<Func<Domain.Entities.BulkBuy, EditVm>> Projection
        {
            get
            {
                return x => new EditVm()
                {
                    Id = x.DisplayId,
                    Name = x.Name,
                    Description = x.Description,
                    ClosingDate = x.DueDate,
                };
            }
        }

        public static EditVm FromEntity(Domain.Entities.BulkBuy entity)
        {
            return Projection.Compile().Invoke(entity);
        }

    }
}
