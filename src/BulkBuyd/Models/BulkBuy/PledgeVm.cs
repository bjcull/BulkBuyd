using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BulkBuyd.Domain.Entities;

namespace BulkBuyd.Models.BulkBuy
{
    public class PledgeVm
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string DisplayName { get; set; }
        public string OrderDetails { get; set; }
        public decimal PledgeAmount { get; set; }

        public static Expression<Func<Pledge, PledgeVm>> Projection
        {
            get
            {
                return x => new PledgeVm()
                {
                    UserId = x.UserId,
                    OrderDetails = x.OrderDetails,
                    DisplayName = x.User.DisplayName,
                    UserEmail = x.User.Email,
                    PledgeAmount = x.PledgeAmount
                };
            }
        }
    }
}
