using System;
using System.Text.RegularExpressions;

namespace MsDevShow.ShownoteStyleStripper.Common
{
    public static class Shownotes
    {
        public static string RemoveCruft(this string shownotesText)
        {
            return shownotesText.RemoveSpans().RemoveStyles();
        }

        private static string RemoveSpans(this string inputText)
        {
            inputText = Regex.Replace(inputText, "<[\\s]*span[^>]*>", string.Empty, RegexOptions.IgnoreCase);
            return Regex.Replace(inputText, "</[\\s]*span>", string.Empty, RegexOptions.IgnoreCase);
        }

        private static string RemoveStyles(this string inputText)
        {
            inputText = Regex.Replace(inputText, "style=\"([^\"]*)\"", string.Empty, RegexOptions.IgnoreCase);
            return Regex.Replace(inputText, "style='([^']*)'", string.Empty, RegexOptions.IgnoreCase);
        }
    }
}