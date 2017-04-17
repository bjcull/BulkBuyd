using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkBuyd.Models.Payments
{
    public class MethodVm
    {
        public string PinchUrl { get; set; }
        public bool HasPayerAccount { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountBsb { get; set; }
    }
}
