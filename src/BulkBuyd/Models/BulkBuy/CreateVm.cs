using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkBuyd.Models.BulkBuy
{
    public class CreateVm
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ClosingDate { get; set; }

    }
}
