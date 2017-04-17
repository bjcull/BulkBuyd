using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkBuyd.Domain.Services
{
    public interface IShortcodeService
    {
        string GenerateId(string prefix, int length = 14);
        string GenerateShortcode(int length = 14);
    }
}
