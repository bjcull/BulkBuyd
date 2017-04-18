using System.Text.RegularExpressions;

namespace BulkBuyd.Domain.Helpers
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
