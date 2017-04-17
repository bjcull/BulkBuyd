using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkBuyd.Domain.Entities
{
    public class PublicEntity : BaseEntity, IPublicEntity
    {
        public string DisplayId { get; set; }
    }
}
