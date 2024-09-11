using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MacroEditor.sources
{
    internal class PasteHTML
    {
        public string GetParagraph(ref string strIn)
        {
            string p = "";
            int i = strIn.IndexOf("<p>");
            if (i < 0) return "";
            int j = strIn.IndexOf("</p>", i + 3);
            if (j < 0) return "";
            int n = j - i;
            p = strIn.Substring(i + 3, j - i - 3);
            strIn = strIn.Remove(i, n).Replace("<p> </p>", " ");
            return p + "<br>" + GetParagraph(ref strIn);
        }

        public string GetSpans(ref string strIn)
        {
            string p = "";
            int i = strIn.IndexOf("<span>");
            if (i < 0)
            {
                if (strIn.Length == 0) return "";
                string sLast = strIn;
                strIn = "";
                return sLast;
            }
            if (i > 0)
            {
                p += strIn.Substring(0, i);
                strIn = strIn.Substring(i);
                return p + GetSpans(ref strIn);
            }
            int j = strIn.IndexOf("</span>", i + 6);
            if (j < 0) return "";
            int n = j - i + 7;
            p = strIn.Substring(i + 6, j - i - 6);
            strIn = strIn.Remove(i, n);
            return p + GetSpans(ref strIn);
        }

        public int RemoveStylesClasses(string sKey, ref string sIn)
        {
            int i = sIn.IndexOf(sKey);
            if (i < 0) return 0;
            int j = sIn.IndexOf('"', i + sKey.Length);
            if (j < 0) return 0;
            int n = j - i + 1;
            string s = sIn.Substring(i, n);
            sIn = sIn.Remove(i, n);
            return 1 + RemoveStylesClasses(sKey, ref sIn);
        }

        //s = s.Replace(" rel=\"nofollow noopener noreferrer\"", ""); or any combination
        // the HP site will add its referrals as needed
        public string sRemoveREL(ref string s)
        {
            int i = s.IndexOf(" rel=\"");
            if (i < 0) return s;
            int j = s.IndexOf('"', i + 6);
            j++;
            string t = s.Substring(0, i) + s.Substring(j);
            return sRemoveREL(ref t);
        }

        public void CleanSB(ref string s)
        {
            s = sRemoveREL(ref s);
            s = s.Replace("<span> </span>", " ");
            s = s.Replace("<br >", "<br>");
            s = s.Replace("<strong>", "<b>");
            s = s.Replace("</strong>", "</b>");
            s = s.Replace("<!--EndFragment-->", "");
            s = s.Replace("<br />", "<br>");
            s = s.Replace("&nbsp;", " ");
            //s = s.Replace(" target=\"_self\"", "");
            //s = s.Replace(" target=\"_blank\"", "");
            s = s.Replace(" data-unlink=\"true\"", "");
            s = Regex.Replace(s, @"\s+", " ");  // replace 1 or more white space with space
        }

        public string ProcessClip(ref string s)
        {
            string sOut = "";
            CleanSB(ref s);
            int n = RemoveStylesClasses("style=\"", ref s);
            n += RemoveStylesClasses("class=\"", ref s);
            n += RemoveStylesClasses("image-alt=\"", ref s);
            n += RemoveStylesClasses("role=\"", ref s);
            n += RemoveStylesClasses("title=\"", ref s);
            n += RemoveStylesClasses("alt=\"", ref s);
            n += RemoveStylesClasses("li-bindable=\"", ref s);
            n += RemoveStylesClasses("li-message-uid=\"", ref s);
            n += RemoveStylesClasses("li-image-url=\"", ref s);
            n += RemoveStylesClasses("li-image-display-id=\"", ref s);
            n += RemoveStylesClasses("li-bypass-lightbox-when-linked=\"", ref s);
            n += RemoveStylesClasses("tabindex=\"", ref s);
            n += RemoveStylesClasses("li-use-hover-links=\"", ref s);
            n += RemoveStylesClasses("li-compiled=\"", ref s);

            // first check for paragraphs
            s = Regex.Replace(s, @"\s+", " ");
            s = s.Replace("<span >", "<span>");
            s = s.Replace("<p >", "<p>");
            s = s.Replace("<strong >", "<b>"); // unaccountably these are needed again ???
            s = s.Replace("</strong>", "</b>");
            s = s.Replace("<br >", "<br>"); // fixes <br span ----
            while (true)
            {
                string sPara = GetParagraph(ref s);
                if (sPara.Length == 0) break;
                sOut += sPara + "<br>"; // sExtractInfo(sPara);
            }
            // if no paragraphs the try spans
            if (sOut != "")
            {
                s = sOut;
                sOut = "";
            }

            while (true)
            {
                string sSpan = GetSpans(ref s);
                if (sSpan == "")
                {
                    if (sOut.Contains("<span>"))
                    {
                        s = sOut;
                        sOut = "";
                        continue;
                    }
                    break;
                }
                sOut += sSpan + "<br>";
            }
            return sOut;
        }
    }
}
