using HtmlAgilityPack;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MacroEditor
{
    public class ChangeUrls
    {
        /*
        public class cUrlChange
        {
            public string FromUrl;
            public string ToUrl;
            public bool bValidated;
            public int uLocation;   // location of bad url in structure when looked up
            public bool NeedsToBeSaved;
        }
        */

        private string MacroName = "";
        private string sFilename = "";
        private string FromUrl = "";
        private string ToUrl = "";
        private int BadUrlType = 0; // 0:not bad url, 1:url is OK, 2: need to replace existing url, 4: fixed but not saved
        private bool MacrosHasBadUrl = false;
        private int NumBadUrls = 0;
        private string LocationFile = "";
        private string sOriginalPage = "";  // the original page of the macro when in the browser

        public void SetMacroName(string macName, string rFilename, string rOriginalPage)
        {
            MacroName = macName;
            sFilename = rFilename;
            sOriginalPage = rOriginalPage;
            MacrosHasBadUrl = IsOldUrl(rFilename, macName, out FromUrl, out ToUrl);
            NumBadUrls = CountOldUrls(rFilename, macName);
        }   

        public HashSet<string> GetUniqueUrls(string sUrlText)
        {
            string pattern = @"ftp\.[^\s""']*?\.(exe|zip|htm|html)\b";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var matches = regex.Matches(sUrlText);
            HashSet<string> uniqueMatches = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (Match m in matches)
            {
                uniqueMatches.Add(m.Value);
            }
            return uniqueMatches;
        }

        public class cWhereUsed
        {
            public string sysType;
            public string macroName;
            public string macroNumber;  // this can change if macros deleted or added
            public bool bValidated;
            public bool NeedsToBeSaved;
        }

        public string GetFromUrl()
        {
            return FromUrl;
        }

        public string GetOriginalPage()
        {
            return sOriginalPage;
        }

        public string GetToUrl()
        {
            return ToUrl;
        }

        public void SetNewUrl(string sNewUrl)
        {
            ToUrl = sNewUrl;
        }

        public int GetBadUrlType()
        {
            return BadUrlType;
        }
        public class cFromToUrls
        {
            public string FromUrl;
            public string ToUrl;
            public bool bValidated;
            public List<cWhereUsed> WhereUsed = new List<cWhereUsed>();
        }

        private int CountBadUrls()
        {
            int n = 0;
            foreach(cFromToUrls ftu in FromToUrls)
            {
                n += ftu.WhereUsed.Count;
            }
            return n;
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
            cfx.bValidated = false;
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

        public int IsBadUrl(string sIn)
        {
            BadUrlType = 0;
            FromUrl = "";
            foreach (cFromToUrls cft in FromToUrls)
            {
                if (sIn.Contains(cft.FromUrl))
                {
                    foreach (cWhereUsed wu in cft.WhereUsed)
                    {
                        if (wu.sysType == sFilename && wu.macroName == MacroName)
                        {
                            ToUrl = cft.ToUrl;
                            FromUrl = cft.FromUrl;
                            if (ToUrl == FromUrl)
                            {
                                BadUrlType =  wu.bValidated ? 0 : 3;
                                return BadUrlType;
                            }
                            BadUrlType = wu.bValidated ? 2 : 1;
                            return BadUrlType;
                        }
                    }
                }
            }
            return BadUrlType;
        }

        // Change the accessibility of cFilesNeedingChanges from private to public
        public class cFilesNeedingChanges
        {
            public class cSetOfChanges
            {
                public List<string> OldMacroNames = new List<string>();
                public List<string> OldMacroNumber = new List<string>();
                public string Filecode;
            }
            public string FromUrl = "";
            public string ToUrl = "";
            public bool ChangeApproved = false;
            public List<cSetOfChanges> soc = new List<cSetOfChanges>();

            public void Init(string sFrom)
            {
                FromUrl = sFrom;
                soc.Clear();
            }
            public void AddEntry(string fnCode, string MacroName, string MacroNumber)
            {
                if (soc.Count == 0)
                {
                    cSetOfChanges SOC = new cSetOfChanges();
                    SOC.Filecode = fnCode;
                    SOC.OldMacroNumber.Add(MacroNumber);
                    SOC.OldMacroNames.Add(MacroName);
                    soc.Add(SOC);
                    return;
                }
                foreach (cSetOfChanges SOC in soc)
                {
                    if (SOC.Filecode == fnCode)
                    {
                        SOC.OldMacroNames.Add(MacroName);
                        SOC.OldMacroNumber.Add(MacroNumber);
                        return;
                    }
                }
                cSetOfChanges Soc = new cSetOfChanges();
                Soc.Filecode = fnCode;
                Soc.OldMacroNames.Add(MacroName);
                Soc.OldMacroNumber.Add(MacroNumber);
                soc.Add(Soc);
            }
        }

        public cFilesNeedingChanges fnc = new cFilesNeedingChanges();

        public int GetNumberChanges(out string FileNames)
        {
            int n = 0;
            FileNames = "";
            Debug.Assert(FromUrl != "");
            fnc.Init(FromUrl);
            foreach (cFromToUrls cft in FromToUrls)
            {
                if (cft.FromUrl != FromUrl) continue;
                foreach (cWhereUsed wu in cft.WhereUsed)
                {
                    if (!wu.bValidated)
                    {
                        n++;
                        fnc.AddEntry(wu.sysType, wu.macroName, wu.macroNumber);
                    }
                }
                break;
            }
            string sFileNames="";
            for (int i = 0; i < fnc.soc.Count; i++)
            {
                sFileNames = fnc.soc[i].Filecode + ":";
                for(int j = 0; j < fnc.soc[i].OldMacroNumber.Count; j++)
                {
                    sFileNames += fnc.soc[i].OldMacroNumber[j] + ",";
                }
                sFileNames = sFileNames.TrimEnd(',') + Environment.NewLine;
                FileNames += sFileNames;
            }
            FileNames = FileNames.TrimEnd();
            return n;
        }

        public bool ExtractFTPurl(string s, out string sFtp)
        {
            sFtp = "";
            HashSet<string> uniqueMatches = GetUniqueUrls(s);
            if (uniqueMatches.Count == 0) return false;
            Debug.Assert(uniqueMatches.Count == 1);
            sFtp = uniqueMatches.First();
            return true;
        }

        public bool UpdateToUrl(string sFtpUrl)
        {
            //bool bFound = ExtractFTPurl(sText, out string sFtpUrl);
            //if (!bFound) return false;
            foreach (cFromToUrls cft in FromToUrls)
            {
                if (cft.FromUrl != FromUrl) continue;
                foreach (cWhereUsed wu in cft.WhereUsed)
                {
                    if (wu.sysType == sFilename && wu.macroName == MacroName)
                    {
                        wu.NeedsToBeSaved = true;
                        wu.bValidated = true;
                        cft.ToUrl = sFtpUrl;
                    }
                    return true;
                }
            }
            Debug.Assert(false, " not found in url list ");
            return false;
        }

        public void SignalAllGoodUrls()
        {
            foreach (cFromToUrls cft in FromToUrls)
            {
                if (cft.FromUrl != FromUrl) continue;
                cft.FromUrl = ToUrl;
                cft.ToUrl = ToUrl;
                cft.bValidated = true;
                foreach(cWhereUsed wu in cft.WhereUsed)
                {
                    wu.bValidated = true;
                    wu.NeedsToBeSaved = false;  // we will save it automatically
                }
            }
        }
        public bool SignalGoodUrl(bool IsValidated)
        {

            foreach (cFromToUrls cft in FromToUrls)
            {
                if (cft.FromUrl != FromUrl) continue;
                foreach (cWhereUsed wu in cft.WhereUsed)
                {
                    if (wu.sysType == sFilename && wu.macroName == MacroName)
                    {
                        wu.NeedsToBeSaved = IsValidated;
                        wu.bValidated = IsValidated;

                        if(BadUrlType == 1)
                        {
                            if (IsValidated)
                                cft.ToUrl = cft.FromUrl;
                            else
                                cft.ToUrl = "";
                        }
                        else
                        {
                            if (IsValidated)
                                cft.ToUrl = ToUrl;
                            else
                                cft.ToUrl = "";
                        }   
                        return true;
                    }
                }
            }
            Debug.Assert(false, " not found in url list ");
            return false;
        }

        public bool IsOldUrl(string FileTypeCode, string MacName, out string sFromUrl, out string sToUrl)
        {
            sFromUrl = "";
            sToUrl = "";
            foreach (cFromToUrls cft in FromToUrls)
            {
                foreach (cWhereUsed wu in cft.WhereUsed)
                {
                    if (wu.sysType == FileTypeCode && wu.macroName == MacName && !wu.bValidated)
                    {
                        sFromUrl = cft.FromUrl;
                        sToUrl = cft.ToUrl;
                        return true;
                    }
                }
            }
            return false;
        }
        public int CountOldUrls(string FileTypeCode, string MacName)
        {
            int n = 0;
            foreach (cFromToUrls cft in FromToUrls)
            {
                foreach (cWhereUsed wu in cft.WhereUsed)
                {
                    if (wu.sysType == FileTypeCode && wu.macroName == MacName && !wu.bValidated)
                    {
                        n++;
                    }
                }
            }
            return n;
        }

        public string ReplaceAllOldUrls(string sFtp)
        {
            string sOut = "";
            fnc.ToUrl = sFtp;
            ToUrl = sFtp;
            sOut += "Change " + fnc.FromUrl + Environment.NewLine;
            sOut += "To " + sFtp + Environment.NewLine;
            sOut += "At the following locations: " + Environment.NewLine;
            for (int i = 0; i < fnc.soc.Count; i++)
            {
                for (int j = 0; j < fnc.soc[i].OldMacroNumber.Count; j++)
                {
                    sOut += fnc.soc[i].Filecode + "#";
                    sOut += fnc.soc[i].OldMacroNumber[j] + ":" + fnc.soc[i].OldMacroNames[j] + Environment.NewLine;
                }
            }
            return sOut;
        }

        public string FormSavedList(bool ShowAll = true)
        {
            string sOut = "";
            foreach (cFromToUrls cft in FromToUrls)
            {
                int n = 0;
                string s1 = cft.FromUrl + Environment.NewLine;
                foreach (cWhereUsed wu in cft.WhereUsed)
                {
                    if (!ShowAll && wu.bValidated) continue;
                    s1 += wu.sysType.PadRight(3) + "#" + (wu.macroNumber + ":").PadRight(4) +  "\'" + wu.macroName + "\'"  + Environment.NewLine;
                    n++;
                }
                if(n>0)
                    sOut += "(" + n.ToString() + ")" + s1.TrimEnd(';') + Environment.NewLine;
            }
            return sOut;
        }

        public void SaveBadUrls(string sFile)
        {
            SaveToXml(sFile, ref FromToUrls);
        }

        public static void SaveToXml(string filePath, ref List<cFromToUrls> data)
        {
            File.Delete(filePath);
            XmlSerializer serializer = new XmlSerializer(typeof(List<cFromToUrls>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, data);
            }
        }

        public  List<cFromToUrls> LoadFromXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<cFromToUrls>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                return (List<cFromToUrls>)serializer.Deserialize(reader);
            }
        }

        // this restore all settings to quickly cancel any changes.
        public void ReloadFiles()
        {
            if (LocationFile == "") return;
            FromToUrls = LoadFromXml(LocationFile);
        }

        public void SaveXmlChanges()
        {
            if (LocationFile == "") return;
            SaveToXml(LocationFile, ref FromToUrls);
        }

        public int ReadBadUrls(string sFile)
        {
            LocationFile = "";
            if (!File.Exists(sFile)) return 0;
            LocationFile = sFile;
            FromToUrls = LoadFromXml(sFile);
            return CountBadUrls();
        }

        //"http://h10032.www1.hp.com/ctg/Manual/c06520607.pdf"
        public string ExtractHREF(string s)
        {
            string c = "://";
            int i = s.IndexOf(c, StringComparison.OrdinalIgnoreCase);   
            string sUrl = s.Substring(i+c.Length);
            return sUrl;
        }

        public async Task<string> HttpFileExists_Async(string surl)
        {
            string url = surl;
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };

            try
            {
                if (!surl.Contains("http"))
                    url = "https://" + surl;

                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(10);
                    var request = new HttpRequestMessage(HttpMethod.Get, url);

                    // Only read headers — avoids body download
                    var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                    // If status code is success → file exists
                    if (response.IsSuccessStatusCode)
                        return "";

                    // Otherwise check status code and reason phrase
                    string reason = response.ReasonPhrase?.ToLower() ?? "";
                    if (reason.Contains("not found") || reason.Contains("404"))
                    {
                        return "Not found or 404";
                    }

                    return "Unknown";
                }
            }

            catch (Exception ex)
            {
                return "Not found or 404";
            }
        }


        public async Task <string> HttpFileExists_Async3(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "HEAD"; // Only headers, not full page
                request.Timeout = 10000; // 5 seconds
                request.ReadWriteTimeout = 10000;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36";
                request.AllowAutoRedirect = false;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                        return "OK";
                    else if ((int)response.StatusCode == 404)
                        return "Not Found";
                    else
                        return "Other: " + response.StatusCode;
                }
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse resp)
                {
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                        return "Not Found";
                    return "Other: " + resp.StatusCode;
                }
                return "Timeout or network error";
            }
        }

        public async Task<string> HttpFileExists_Async1(string surl)
        {
            string url = surl;
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = true
            };

            try
            {
                if (!surl.Contains("http"))
                    url = "https://" + surl;

                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);
                    client.DefaultRequestHeaders.UserAgent.ParseAdd(
    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36"
);
                    //var request = new HttpRequestMessage(HttpMethod.Get, url);
                    //var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                    var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

                    if (response.StatusCode == HttpStatusCode.Moved ||
                        response.StatusCode == HttpStatusCode.Redirect ||
                        response.StatusCode == HttpStatusCode.RedirectKeepVerb)
                    {
                        var target = response.Headers.Location?.ToString() ?? "";

                        if (target.Contains("/error/404"))
                        {
                            return "Not found (redirect to HP 404 page)";
                        }
                    }


                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return "Not found (404)";
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        return "OK";
                    }

                    return "Unknown: " + response.StatusCode;
                }
            }

            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        public async Task<string> HttpFileExistsAsync(string surl)
        {
            string url = surl;
            try
            {
                if(!surl.Contains("http"))
                    url = "https://" + surl;


                using (HttpClient client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, url);

                    // Only read headers — avoids body download
                    var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                    // If status code is success → file exists
                    if (response.IsSuccessStatusCode)
                        return "";

                    // Otherwise check status code and reason phrase
                    string reason = response.ReasonPhrase?.ToLower() ?? "";
                    if (reason.Contains("not found") || reason.Contains("404"))
                        return "Not found or 404";

                    return "Unknown";
                }
            }

            catch (Exception ex)
            {
               return ex.Message;
            }
        }
    }
}
