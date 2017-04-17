using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkBuyd.Models.Pledges
{
    public class AddPledgeVm
    {
        public string Id { get; set; }
        public decimal PledgeAmount { get; set; }
        public string OrderDetails { get; set; }
    }
}
