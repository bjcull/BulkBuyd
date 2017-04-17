using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BulkBuyd.Services.Helpers
{
    public class StripHtmlHelper
    {
        const string HTML_TAG_PATTERN = "<.*?>";

        public static string StripHtml(string inputString)
        {
            return Regex.Replace
              (inputString, HTML_TAG_PATTERN, string.Empty);
        }
    }
}
