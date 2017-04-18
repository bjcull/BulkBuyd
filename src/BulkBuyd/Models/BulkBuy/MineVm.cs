using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkBuyd.Domain.DTOs;
using BulkBuyd.Models.Home;

namespace BulkBuyd.Models.BulkBuy
{
    public class MineVm
    {
        public List<BulkBuyDto> BulkBuys { get; set; }

        public MineVm()
        {
            BulkBuys = new List<BulkBuyDto>();
        }
    }
}
