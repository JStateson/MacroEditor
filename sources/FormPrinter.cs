using MacroEditor.Properties;
using Microsoft.Office.Interop.Word;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MacroEditor.sources
{
    //<!-- @Reset Printer@ TimeStamp=DDDDDDDD_TTTTTT -->
    internal class FormPrinter
    {
        private string sFormIn = "";
        private string TimeStamp;
        private string sTS = "DDDDDDDD_TTTTTT";
        private class csIDs
        {
            public string sID;
            public List<string> sPhrase;
        }
        private List<csIDs> MyPrinterInfo;
        private int iLookup(string sID)
        {
            int i = 0;
            foreach (csIDs cid in MyPrinterInfo)
            {
                if(sID == cid.sID)
                {
                    return i;
                }
                i++;
            }
            Debug.Assert(false);
            return 0;
        }

        public int TagNameToPhrase(string sName)
        {
            for(int i = 0; i < SourceDestination.ListOfArgs.Count; i++)
            {
                if (SourceDestination.ListOfArgs[i].Contains(sName)) return i;
            }
            Debug.Assert(false);
            return -1;
        }

        public bool LookForDuplicates(string sBtn, string sUrl)
        {
            int i = 0;
            int j = 0;
            string sTagName = "";
            // this is wrong cannot have sBtn all 
            for(i = 0; i < PrinterListH.Count; i++)
            {
                sTagName = lbButtons[i];
                //SourceDestination.ListOfArgs[i] = sBtn;
                for(j = 0; j < PrinterListH[i].Count; j++)
                {
                    if (PrinterListH[i][j] == sUrl)
                    {
                        bool NotPageNorDocument = SourceDestination.AllowAdditionalItems(sBtn);
                        if(NotPageNorDocument)
                        {
                            string sMsg = "Item " + sTagName + " was assigned the same url as " + sBtn;
                            MessageBox.Show(sMsg, "ERROR");
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void Init()
        {
            MakeLBlists(Properties.Resources.PrinterList);
            sFormIn = Resources.PrinterTemplett;
            DateTime now = DateTime.Now;
            TimeStamp = now.ToString("yyyyMMdd_HHmmss");
            MyPrinterInfo = new List<csIDs>();
            foreach (string s in lbPrinterLayout)
            {
                csIDs cid = new csIDs();
                cid.sID = s;
                cid.sPhrase = new List<string>();
                MyPrinterInfo.Add(cid);
            }
        }

        public void ClearPrinterPhrases()
        {
            foreach(csIDs cid in MyPrinterInfo)
            {
                cid.sPhrase.Clear();
            }
        }

        private string FormatField(string sID, string sText)
        {
            string sOut = "";
            switch(sID)
            {
                case "@Reset Printer@":
                    if(sText.Contains(" steps "))
                    {
                        sOut = "<br>" + sText;
                    }
                    else
                    {
                        sOut = sText + " ";
                    }
                    break;
                case "@Network Connect@":

                case "@Software@":

                case "@Documents@":

                case "@YouTube@":

                case "@Reference@":
                    sOut = " " + sText;
                    break;
            }
            return sOut;
        }


        //sID might be @Reset Printer@
        public void AddPhrase(string sID, string sText)
        {
            int i = iLookup(sID);
            csIDs cid = MyPrinterInfo[i];
            //could check for dups here but would have to extract the url from the hyperlink
            cid.sPhrase.Add(sText);
        }


        private string GetParagraph(string sID, ref string sComment)
        {
            int i = sFormIn.IndexOf(sID);
            int j = sFormIn.LastIndexOf("<!--",i);
            int a = sFormIn.IndexOf("-->",j+4);
            Debug.Assert(a >= 0);
            a += 3;
            sComment = sFormIn.Substring(j, a - j);
            int k = sFormIn.IndexOf("<!--", a);
            if(k == -1)
            {
                return sFormIn.Substring(a).Trim();
            }
            return sFormIn.Substring(a, k - a).Trim();
        }

        public string GetMacro()
        {
            string sOut = Utils.mTableStyle;
            string sComment = "";
            foreach (csIDs cid in MyPrinterInfo)
            {
                if(cid.sPhrase.Count > 0)
                {
                    string sTemp = GetParagraph(cid.sID, ref sComment);
                    sComment = sComment.Replace(sTS, TimeStamp);
                    string t = "";
                    foreach(string s in cid.sPhrase)
                    {
                        t+= s.Trim() + " ";
                    }
                    sTemp = sTemp.Replace(cid.sID,t);
                    sTemp = sComment + sTemp;
                    sOut += sTemp;
                    sOut += "<br><br>";
                }
            }
            return sOut;
        }

        public string AnyMissing(string s)
        {
            string sOut = "";
            foreach (string t in SourceDestination.ListOfArgs)
            {
                if (s.Contains(t))
                {
                    sOut += t + " ";
                }
            }
            return sOut;
        }

        public List<List<string>> ResList = new List<List<string>>();
        public List<string> lbPrinterLayout;
        public List<string> lbUrls;
        public List<string> lbButtons;
        public List<string> lbTips;
        public List<string> lbPhrases;
        public List<List<string>> PrinterHttp = new List<List<string>>();
        public List<List<string>> PrinterListH = new List<List<string>>(); // the clip which is HTML or a bunch of steps or a page number
        public List<List<string>> PrinterListT = new List<List<string>>(); // the text to be clicked or the word Page or Document
        public cSourceDestination SourceDestination = new cSourceDestination();

        public void ClearHTTP()
        {
            for (int i = 0; i < PrinterHttp.Count; i++)
            {
                PrinterHttp[i].Clear();
            }
        }

        public void ClearPrinterLists()
        {
            for (int i = 0; i < PrinterListH.Count; i++)
                PrinterListH[i].Clear();
            for (int i = 0; i < PrinterListT.Count; i++)
                PrinterListT[i].Clear();
            for (int i = 0; i < PrinterHttp.Count; i++)
                PrinterHttp[i].Clear();
        }

        public void MakeLBlists(string prPrinterList)
        {
            string input = prPrinterList;

            int j;
            for (j = 0; j < 5; j++)
            {
                List<string> more = new List<string>();
                ResList.Add(more);
            }
            lbPrinterLayout = ResList[Utils.elbPrinterLayout];
            lbUrls = ResList[Utils.elbUrls];
            lbButtons = ResList[Utils.elbButtons];
            lbTips = ResList[Utils.elbTips];
            lbPhrases = ResList[Utils.elbPhrases];


            j = 0;
            int i = input.IndexOf("{");
            i++;
            while (i >= 0)
            {
                int k = input.IndexOf("}", i);
                string[] sS = input.Substring(i + 1, k - i - 1).Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in sS)
                {
                    ResList[j].Add(s);
                }
                j++;
                if (j == 5) break;
                i = k + 1;
                i = input.IndexOf("{", i);
            }

            foreach (string s in lbUrls)
            {
                SourceDestination.AddSrc(s);
            }

            for (i = 0; i < lbPhrases.Count; i++)
            {
                List<string> nMore = new List<string>();
                PrinterHttp.Add(nMore);
            }

            foreach (string s in lbButtons)
            {
                List<string> nMore = new List<string>();
                PrinterListH.Add(nMore);
                nMore = new List<string>();
                PrinterListT.Add(nMore);
            }

        }


        public int AddH_list(int n, string s)
        {
            PrinterListH[n].Add(s);
            return PrinterListH[n].Count;
        }
        public int AddT_list(int n, string s)
        {
            PrinterListT[n].Add(s);
            return PrinterListT[n].Count;
        }


        public void Reduce(string u, int iW, string sT)
        {
            string s = GetPhrase(u);
            switch (u)
            {
                case "Reset Video":
                    ResetVideo(s, iW, sT);
                    break;
                case "Reset Steps":
                    ResetSteps(s, iW, sT);
                    break;
                case "Router Video":
                    RouterVideo(s, iW, sT);
                    break;
                case "Direct Video":
                    DirectVideo(s, iW, sT);
                    break;
                case "Direct Page":  // cannot use s as there are more than 1 item
                    DirectPage(u, iW, sT);
                    break;
                case "Direct Doc":
                    DirectDoc(u, iW, sT);
                    break;
                case "WPS Page":
                    WpsPage(u, iW, sT);
                    break;
                case "WPS Doc":
                    WpsDoc(u, iW, sT);
                    break;
                case "Driver":
                    Driver(s, iW, sT);
                    break;
                case "Scanner":
                    Scanner(s, iW, sT);
                    break;
                case "Software":
                    Software(s, iW, sT);
                    break;
                case "All Docs":
                    AllDocs(s, iW, sT);
                    break;
                case "Reference":
                    Reference(s, iW, sT);
                    break;
                case "Parts":
                    Parts(s, iW, sT);
                    break;
                case "Assembly":
                    Assembly(s, iW, sT);
                    break;
                case "YouTube":
                    YouTube(s, iW, sT);
                    break;
            }
        }

        private string GetPhrase(string s)
        {
            s = "@" + s + "@";
            int n = s.Length;
            foreach (string t in lbPhrases)
            {
                int i = t.IndexOf(s);
                if (i >= 0)
                {
                    return t.Substring(0, i) + "@arg@" + t.Substring(i + n);
                }
            }
            Debug.Assert(false);
            return "";
        }

        private string GetRawPhrase(string s)
        {
            s = "@" + s + "@";
            int n = s.Length;
            foreach (string t in lbPhrases)
            {
                int i = t.IndexOf(s);
                if (i >= 0)
                {
                    return t;
                }
            }
            Debug.Assert(false);
            return "";
        }

        private int TagToPhrase(int iWorkingTab)
        {
            int n = 0;
            string s = lbButtons[iWorkingTab].ToString();
            foreach (string t in lbPhrases)
            {
                int i = t.IndexOf(s);
                if (i != -1)
                {
                    return n;
                }
                n++;
            }
            Debug.Assert(false);
            return -1;
        }

        private void ResetVideo(string s, int iWorkingTab, string sT)
        {
            int i = TagToPhrase(iWorkingTab);
            if (PrinterHttp[i].Count > 0) s = " @arg@";
            PrinterHttp[i].Add(s.Replace("@arg@", Utils.FormUrl(PrinterListH[iWorkingTab][0], sT)));
        }

        private void YouTube(string s, int iWorkingTab, string sT)
        {
            int i = TagToPhrase(iWorkingTab);
            if (PrinterHttp[i].Count > 0) s = " @arg@";
            PrinterHttp[i].Add(s.Replace("@arg@", Utils.FormUrl(PrinterListH[iWorkingTab][0], sT)));
        }

        private void Assembly(string s, int iWorkingTab, string sT)
        {
            int i = TagToPhrase(iWorkingTab);
            if (PrinterHttp[i].Count > 0) s = " @arg@";
            PrinterHttp[i].Add(s.Replace("@arg@", Utils.FormUrl(PrinterListH[iWorkingTab][0], sT)));
        }

        private void ResetSteps(string s, int iWorkingTab, string sT)
        {
            int i = TagToPhrase(iWorkingTab);
            string sH = PrinterListH[iWorkingTab][0];
            string sU = Utils.Form1CellTable(sH, "");
            string sO = s.Replace("<br>@arg@", sU);
            PrinterHttp[i].Add(sO);
        }
        private void RouterVideo(string s, int iWorkingTab, string sT)
        {
            int i = TagToPhrase(iWorkingTab);
            if (PrinterHttp[i].Count > 0) s = " @arg@";
            PrinterHttp[i].Add(s.Replace("@arg@", Utils.FormUrl(PrinterListH[iWorkingTab][0], sT)));
        }
        private void DirectVideo(string s, int iWorkingTab, string sT)
        {
            int i = TagToPhrase(iWorkingTab);
            if (PrinterHttp[i].Count > 0) s = " @arg@";
            PrinterHttp[i].Add(s.Replace("@arg@", Utils.FormUrl(PrinterListH[iWorkingTab][0], sT)));
        }
        private void DirectPage(string s, int iWorkingTab, string sT)
        {
            int i = TagToPhrase(iWorkingTab);
            string t = GetRawPhrase(s);
            string sH = PrinterListH[iWorkingTab][0];
            string sO = t.Replace("@Direct Page@", sH);
            if (PrinterHttp[i].Count == 0)
                PrinterHttp[i].Add(sO);
            else
            {
                PrinterHttp[i][0] = PrinterHttp[i][0].Replace("@Direct Page@", sH);
            }
        }
        private void DirectDoc(string s, int iWorkingTab, string sTt)
        {
            int i = TagToPhrase(iWorkingTab);
            string t = GetRawPhrase(s);
            string sH = PrinterListH[iWorkingTab][0];
            string sT = "User Manual";
            string sU = Utils.FormUrl(PrinterListH[iWorkingTab][0], sT);
            string sO = t.Replace("@Direct Doc@", sU);
            if (PrinterHttp[i].Count == 0)
                PrinterHttp[i].Add(sO);
            else
            {
                PrinterHttp[i][0] = PrinterHttp[i][0].Replace("@Direct Doc@", sU);
            }
        }
        private void WpsPage(string s, int iWorkingTab, string sT)
        {
            int i = TagToPhrase(iWorkingTab);
            string t = GetRawPhrase(s);
            string sH = PrinterListH[iWorkingTab][0];
            string sO = t.Replace("@WPS Page@", sH);
            if (PrinterHttp[i].Count == 0)
                PrinterHttp[i].Add(sO);
            else
            {
                PrinterHttp[i][0] = PrinterHttp[i][0].Replace("@WPS Page@", sH);
            }
        }
        private void WpsDoc(string s, int iWorkingTab, string sTt)
        {
            int i = TagToPhrase(iWorkingTab);
            string t = GetRawPhrase(s);
            string sH = PrinterListH[iWorkingTab][0];
            string sT = "User Manual";
            string sU = Utils.FormUrl(PrinterListH[iWorkingTab][0], sT);
            string sO = t.Replace("@WPS Doc@", sU);
            if (PrinterHttp[i].Count == 0)
                PrinterHttp[i].Add(sO);
            else
            {
                PrinterHttp[i][0] = PrinterHttp[i][0].Replace("@WPS Doc@", sU);
            }
        }
        private void Driver(string s, int iWorkingTab, string sT)
        {
            int i = TagToPhrase(iWorkingTab);
            if (PrinterHttp[i].Count > 0) s = " @arg@";
            PrinterHttp[i].Add(s.Replace("@arg@", Utils.FormUrl(PrinterListH[iWorkingTab][0], sT)));
        }
        private void Scanner(string s, int iWorkingTab, string sT)
        {
            int i = TagToPhrase(iWorkingTab);
            if (PrinterHttp[i].Count > 0) s = " @arg@";
            PrinterHttp[i].Add(s.Replace("@arg@", Utils.FormUrl(PrinterListH[iWorkingTab][0], sT)));
        }
        private void Software(string s, int iWorkingTab, string sT)
        {
            int i = TagToPhrase(iWorkingTab);
            if (PrinterHttp[i].Count > 0) s = "  @arg@";
            PrinterHttp[i].Add(s.Replace("@arg@", Utils.FormUrl(PrinterListH[iWorkingTab][0], sT)));
        }
        private void AllDocs(string s, int iWorkingTab, string sT)
        {
            int i = TagToPhrase(iWorkingTab);
            if (PrinterHttp[i].Count > 0) s = "  @arg@";
            PrinterHttp[i].Add(s.Replace("@arg@", Utils.FormUrl(PrinterListH[iWorkingTab][0], sT)));
        }
        private void Reference(string s, int iWorkingTab, string sT)
        {
            int i = TagToPhrase(iWorkingTab);
            if (PrinterHttp[i].Count > 0) s = "  @arg@";
            PrinterHttp[i].Add(s.Replace("@arg@", Utils.FormUrl(PrinterListH[iWorkingTab][0], sT)));
        }
        private void Parts(string s, int iWorkingTab, string sT)
        {
            int i = TagToPhrase(iWorkingTab);
            if (PrinterHttp[i].Count > 0) s = "  @arg@";
            PrinterHttp[i].Add(s.Replace("@arg@", Utils.FormUrl(PrinterListH[iWorkingTab][0], sT)));
        }

        public bool ApplyFormat(ref string FmtOut)
        {
            List<int> DataLoc = new List<int>();
            ClearPrinterPhrases();
            foreach (string s in lbPrinterLayout)
            {
                DataLoc = SourceDestination.PullFromSrc(s);
                foreach (int i in DataLoc)
                {
                    if (PrinterHttp[i].Count == 0)
                    {
                        continue;
                    }
                    foreach (string t in PrinterHttp[i])
                    {
                         AddPhrase(s, t);
                    }
                }
            }
            FmtOut= GetMacro();
            string m = AnyMissing(FmtOut);
            if (m != "")
            {
                MessageBox.Show("Missing items: " + m);
                return false;
            }
            return true;
        }
    
        public bool CreateFormData(ref string FmtOut)
        {
            return ApplyFormat(ref FmtOut);
        }

    }
}

