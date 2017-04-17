using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkBuyd.Domain.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
