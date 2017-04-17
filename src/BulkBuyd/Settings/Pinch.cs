using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkBuyd.Settings
{
    public class Pinch
    {
        public string MerchantId { get; set; }
        public string SecretKey { get; set; }
        public string PublishableKeyTest { get; set; }
        public string PublishableKeyLive { get; set; }
        public bool IsLive { get; set; }
    }
}
