using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MacroEditor
{
    public static class ClipboardHtml
    {
        public static string GetHtml()
        {
            if (!Clipboard.ContainsText(TextDataFormat.Html))
                return null;

            string clipboardHtml =
                Clipboard.GetText(TextDataFormat.Html);

            if (string.IsNullOrEmpty(clipboardHtml))
                return null;

            // Get the fragment offsets.
            Match startMatch = Regex.Match(
                clipboardHtml,
                @"StartFragment:(\d+)",
                RegexOptions.IgnoreCase);

            Match endMatch = Regex.Match(
                clipboardHtml,
                @"EndFragment:(\d+)",
                RegexOptions.IgnoreCase);

            if (startMatch.Success && endMatch.Success)
            {
                int startByte =
                    int.Parse(startMatch.Groups[1].Value);

                int endByte =
                    int.Parse(endMatch.Groups[1].Value);

                byte[] bytes =
                    Encoding.UTF8.GetBytes(clipboardHtml);

                if (startByte >= 0 &&
                    endByte > startByte &&
                    endByte <= bytes.Length)
                {
                    return Encoding.UTF8.GetString(
                        bytes,
                        startByte,
                        endByte - startByte);
                }
            }

            // Fallback if the offsets aren't available.
            const string startMarker =
                "<!--StartFragment-->";

            const string endMarker =
                "<!--EndFragment-->";

            int start =
                clipboardHtml.IndexOf(
                    startMarker,
                    StringComparison.OrdinalIgnoreCase);

            int end =
                clipboardHtml.IndexOf(
                    endMarker,
                    StringComparison.OrdinalIgnoreCase);

            if (start >= 0 && end > start)
            {
                start += startMarker.Length;

                return clipboardHtml.Substring(
                    start,
                    end - start);
            }

            return clipboardHtml;
        }
    }
}
