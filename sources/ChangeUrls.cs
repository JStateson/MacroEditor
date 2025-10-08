using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroEditor
{
    internal class ChangeUrls
    {

        public class cWhereUsed
        {
            public string sysType;
            public string macroName;
            public string macroNumber;  // this can change if macros deleted or added
        }

        public class cFromToUrls
        {
            public string FromUrl;
            public string ToUrl;
            public List<cWhereUsed> WhereUsed = new List<cWhereUsed>();
        }

        public List<cFromToUrls> FromToUrls = new List<cFromToUrls>();

        public void Clear()
        {
            FromToUrls.Clear();
        }
        public cFromToUrls GetUrl(string sUrl)
        {
            foreach (cFromToUrls cft in FromToUrls)
            {
                if (cft.FromUrl == sUrl)
                {
                    return cft;
                }
            }
            cFromToUrls cfx = new cFromToUrls();
            cfx.FromUrl = sUrl;
            cfx.ToUrl = "";
            cfx.WhereUsed.Clear();
            FromToUrls.Add(cfx);
            return cfx;
        }
        public void AddUrl(string from, string to, string type, string name, string number)
        {
            cFromToUrls cft = GetUrl(from);
            cWhereUsed wu = new cWhereUsed();
            wu.sysType = type;
            wu.macroName = name;
            wu.macroNumber = number;
            cft.WhereUsed.Add(wu);
        }

        public List<string> FindFromUrls(string type, string name)
        {
            List<string> urls = new List<string>();
            foreach (cFromToUrls cft in FromToUrls)
            {
                foreach (cWhereUsed wu in cft.WhereUsed)
                {
                    if (wu.sysType == type && wu.macroName == name)
                    {
                        urls.Add(cft.FromUrl);
                    }
                }
            }
            return urls;
        }

        public bool IsOldUrl(string type, string name)
        {
            foreach (cFromToUrls cft in FromToUrls)
            {
                foreach (cWhereUsed wu in cft.WhereUsed)
                {
                    if (wu.sysType == type && wu.macroName == name)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public string FormSavedList()
        {
            string sOut = "";
            foreach (cFromToUrls cft in FromToUrls)
            {
                string s1 = cft.FromUrl + ":";
                foreach (cWhereUsed wu in cft.WhereUsed)
                {
                    s1 += wu.sysType + "," + wu.macroName + "," + wu.macroNumber + ";";
                }
                sOut += s1.TrimEnd(';') + Environment.NewLine;
            }
            return sOut;
        }

        public void SaveBadUrls(string sFile)
        {
            string sOut = FormSavedList();
            File.WriteAllText(sFile, sOut);
        }

        public int ReadBadUrls(string sFile)
        {
            if(!File.Exists(sFile)) return 0;   
            string sOut = File.ReadAllText(sFile);
            if (sOut.Length == 0) return 0;
            FromToUrls.Clear();
            string[] lines = sOut.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in lines)
            {
                string[] sMac = s.Split(':');
                cFromToUrls cft = new cFromToUrls();
                cft.FromUrl = sMac[0];
                string[] rhs = sMac[1].Split(';');
                foreach (string t in rhs)
                {
                    string[] u = t.Split(',');
                    cWhereUsed wu = new cWhereUsed();
                    wu.sysType = u[0];
                    wu.macroName = u[1];
                    wu.macroNumber = u[2];
                    cft.WhereUsed.Add(wu);
                }
                FromToUrls.Add(cft);
            }
            return lines.Length;
        }
    }
}
