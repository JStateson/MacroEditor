using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;



namespace MacroEditor.sources
{
    public class cMyUrls
    {

        private int nTemplet = 0;

        public List<cUrls> UrlInfo = new List<cUrls>();
        public string sText;


        public string GetUpdated( int k, int kEnd)
        {
            int n = 0;
            int i, j;
            cUrls cu;
            for(i = k; i < kEnd; i++)
            {
                cu = UrlInfo[i];
                j = cu.sOrigResult.Length;
                if (!cu.bIsMacIDrecord)
                    sText = sText.Replace(Utils.strFill(n, j), cu.sChangedResult);
                n++;
            }
            return sText;
        }
        public string ResidualName()
        {
            int n = 0;
            int i;
            if (sText == null) return "";
            string sOut = sText;             
            foreach (cUrls cu in UrlInfo)
            {
                i = cu.sOrigResult.Length;
                sOut = sOut.Replace(Utils.strFill(n, i), "Macro" + (n+1).ToString());
                n++;
            }
            return sOut;
        }

        //<a is done here
        // use 1111, 2222, etc to use as pattern to replace else might get duplicate replacments
        private void ProcessA(string sLC)
        {
            int i = 0, j, k;
            string sH = "";
            string sT = "";
            while (true)
            {
                j = sLC.IndexOf("<a", i);
                if (j < 0) break;
                i = j + 2;
                k = sLC.IndexOf("</a>", i);
                Debug.Assert(k > 0);
                k += 4;
                string t = sText.Substring(j, k - j);
                bool b = Utils.GetHT(t, ref sH, ref sT);
                if (b)
                {
                    cUrls cu = new cUrls();
                    cu.sOrigResult = t;
                    cu.sChangedResult = t;
                    i = sText.IndexOf(t);
                    sText = Utils.ReplaceStringAtLoc(sText, nTemplet, i, t.Length);
                    nTemplet++;
                    cu.sProposedT = sT;
                    cu.sOrigText = sT;
                    cu.sProposedH = sH;
                    cu.sOrigHref = sH;
                    cu.sOrigHref = sH;
                    cu.bIsUrl = true;
                    cu.bIsMacIDrecord = false;
                    UrlInfo.Add(cu);
                }
                sLC = sText.ToLower();
                i = 0;
            }
        }


        //<img src="https://h30434.www3.hp.com/t5/image/serverpage/image-id/362710iC75893BC32089485">
        private bool GetSRC(string s, ref string sSRC)
        {
            string sLC = s.ToLower();
            int i, j;
            i = s.IndexOf("src=");
            if (i < 0) return false;
            i += 4;
            string c = s.Substring(i, 1);
            i++;
            j = s.IndexOf(c, i);
            if (j < 0) return false;
            sSRC = s.Substring(i, j - i);
            return true;
        }
        private void ProcessI(string sLC)
        {
            int i = 0, j, k;
            string sH = "";
            string sT = "";
            while (true)
            {

                j = sLC.IndexOf("<img ", i);
                if (j < 0) break;
                i = j + 5;
                k = sLC.IndexOf(">", i);
                Debug.Assert(k > 0);
                k++;
                string t = sText.Substring(j, k - j);
                bool b = GetSRC(t, ref sH);
                if (b)
                {
                    cUrls cu = new cUrls();
                    cu.sOrigResult = t;
                    cu.sChangedResult = t;
                    i = sText.IndexOf(t);
                    sText = Utils.ReplaceStringAtLoc(sText, nTemplet, i, t.Length);
                    nTemplet++;
                    cu.sProposedT = sT;
                    cu.sOrigText = sT;
                    cu.sProposedH = sH;
                    cu.sOrigHref = sH;
                    cu.bIsImage = true;
                    cu.bIsMacIDrecord = false;
                    UrlInfo.Add(cu);
                }
                sLC = sText.ToLower();
                i = 0;
            }
        }
        public int Init(string s)
        {
            sText = s;
            nTemplet = 0;   // a..z for replacement of original url
            ProcessA(sText.ToLower());
            ProcessI(sText.ToLower());
            return UrlInfo.Count;
        }

        public int Add(string s)
        {
            sText = s;
            nTemplet = 0;   // a..z for replacement of original url
            int n = UrlInfo.Count;
            ProcessA(sText.ToLower());
            ProcessI(sText.ToLower());
            return n;
        }
    }
}
