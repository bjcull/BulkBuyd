using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkBuyd.Domain.Entities
{
    public class Pledge : BaseEntity
    {
        public string OrderDetails { get; set; }
        public decimal PledgeAmount { get; set; }

        public string PinchPayerId { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int BulkBuyId { get; set; }
        public BulkBuy BulkBuy { get; set; }
    }
}
