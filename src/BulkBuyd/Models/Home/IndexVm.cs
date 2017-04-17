using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkBuyd.Domain.DTOs;

namespace BulkBuyd.Models.Home
{
    public class IndexVm
    {
        public List<BulkBuyVm> BulkBuys { get; set; }

        public IndexVm()
        {
            BulkBuys = new List<BulkBuyVm>();
        }
    }
}
