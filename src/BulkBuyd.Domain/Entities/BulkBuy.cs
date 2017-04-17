using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkBuyd.Domain.Entities
{
    public class BulkBuy : PublicEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        public string OwnerId { get; set; }
        public User Owner { get; set; }

        public ICollection<Pledge> Pledges { get; set; }

        public BulkBuy()
        {
            Pledges = new List<Pledge>();
        }
    }
}
