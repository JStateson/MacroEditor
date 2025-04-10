using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using System.Diagnostics.Contracts;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Security;
using Microsoft.Win32;
using static System.Net.WebRequestMethods;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.CompilerServices;
using System.Windows.Media.Media3D;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Diagnostics;
using System.Windows.Interop;
using System.IO.Ports;
using System.Reflection;
using System.Windows.Media.Animation;
using System.Xml.Linq;
using MacroEditor.sources;
using Microsoft.Office.Interop.Word;
using Font = System.Drawing.Font;


namespace MacroEditor
{
    public partial class WordSearch : Form
    {

        private int SelectedRow = 0;
        private List<cRefURLs> RefUrls = new List<cRefURLs>();
        private List<CFound> cFound = new List<CFound>();
        private List<CFound> cSorted = new List<CFound>();
        private List<CFound> aSorted = new List<CFound>();
        private List<CFound> dSorted = new List<CFound>();
        private List<CBody> cAll;
        List<string> keywords;
        private bool[] KeyPresent;
        private int[] KeyCount;
        private int TotalMatches = 0;
        private string SearchCode = "(x)"; // W - P - A for Word, Phrase, Any
        public int LastViewed { get; set; }
        public string NewItemName { get; set; }
        public string NewItemID { get; set; }
        int nUseLastViewed = -1;
        int mUseLastViewed = -1;
        private bool TriedFailed = false;
        private int[] cWidth = new int[3] { 48, 64, 64 }; // last was 316 but now using fill
        private int[] Unsorted;
        private int[] SortInx;
        private int[] aSort;
        private int CFcnt = 0;
        private Font Reg12;
        private Font Reg10;
        private Color fBlue;
        private Color fDark;

        private string SelectedFile = "";
        private string HasFiles = "";
        private string SaveHasFiles = "";
        private bool[] ColSortDirection = new bool[4] { true, true, true, true }; // true is descending false is ascending
        private string[] CountryCodes;
        private string CountryCodeResults;
        private List<int> WhereInx = new List<int>();
        private int MaxMatches; // most matches in a macro
        private int WhichMatch = 0;
        private string aFilter = "";
        private static string LastSearchWord = "";
        private static string LastSearchParam = "";
        private cMacroChanges MViews;
        private string lbDroppedText = "";
        private cTitleInfo cTI;
        private cCheckSpell SpellCheck;
        private PrinterDB printerDB;
        private string DataFileRecord;

        public void EditHelp(string s)
        {
            if (Utils.bSpellingEnabled)
                SpellCheck.EditHelpDocs(s);
            else Utils.WordpadEdit(s);
        }

        public class cRefURLs
        {

            public string PageOut = "";
            public string sFile = "", sMacN = "", nMac = "";
            public void init(string ssFile, string ssMacN, string sMac)
            {
                sFile = ssFile;
                sMacN = ssMacN;
                nMac = sMac;
                PageOut = "";
            }

            // only look after the _blank>
            public bool LookForUrl(int inx, ref string s)
            {
                // <a href ...blank..self..whatever.....</a>
                string sBlank = "=\"_blank\">";
                string sSelf = "=\"_self\">";
                int Left_a, Right_a;
                Right_a = s.IndexOf("</a>", inx);
                if (Right_a == -1) return true;
                Left_a = s.LastIndexOf("<a href", inx);
                if (Left_a == -1) return true;
                string t = s.Substring(Left_a, Right_a+4 - Left_a) + "<br><br>";
                int i = t.IndexOf(sBlank);
                if (i == -1) i = t.IndexOf(sSelf);
                if (i == -1) i = t.IndexOf(">");
                Debug.Assert(i >= 0);
                if (i == -1) return false;
                inx -= Left_a;
                if (inx < i) return false;
                if (PageOut.Contains(t)) return true;
                PageOut += t;
                return true;
            }
        }


        private class cTitleInfo
        {
            public class cTitleData
            {
                public int LocInCBody;
                public int NumKeys;
                public string sTitle;
            }
            public int[] nSorted;
            public List<cTitleData> ctd;
            public void Init()
            {
                ctd = new List<cTitleData>();
                ctd.Clear();
            }
            
            public int DoSort()
            {
                int n = ctd.Count;
                if (n > 0)
                {
                    nSorted = new int[n];
                    int[] nUnsorted = new int[n];
                    for (int i = 0; i < n; i++)
                    {
                        nSorted[i] = i;
                        nUnsorted[i] = ctd[i].NumKeys;
                    }
                    RunSort(n, true, ref nSorted, ref nUnsorted);
                }
                return n;
            }
            
            public string GetName(int i)
            {
                if(i < ctd.Count)
                {
                    return ctd[nSorted[i]].sTitle;
                }
                return "";
            }
            private void nInsert(int nLocMacro, string s)
            {
                cTitleData td;
                int n = ctd.Count;
                if (n == 0)
                {
                    td = new cTitleData();
                    td.LocInCBody = nLocMacro;
                    td.sTitle = s;
                    td.NumKeys = 1;
                    ctd.Add(td);
                    return;
                }
                for(int i = 0; i < ctd.Count; i++)
                {
                    if (ctd[i].LocInCBody == nLocMacro)
                    {
                        ctd[i].NumKeys++;
                        return;
                    }
                }
                td = new cTitleData();
                td.LocInCBody = nLocMacro;
                td.sTitle = s;
                td.NumKeys = 1;
                ctd.Add(td);
            }
            public void Search( int i, string sName, string sKey)
            {
                if(sName.ToLower().Contains(sKey.ToLower()))
                {
                    nInsert(i, sName);
                }
            }

        }



        public WordSearch(ref List<CBody> Rcb, bool bAllowChangeExit, ref cMacroChanges rMViews,
            ref cCheckSpell MySpellCheck, ref PrinterDB rprinterDB)
        {
            InitializeComponent();
            printerDB = rprinterDB;
            SpellCheck = MySpellCheck;
            cAll = Rcb;
            LastViewed = -1;
            cbHPKB.Checked = Properties.Settings.Default.IncludeHPKB;
            cbOfferAlt.Checked = Properties.Settings.Default.OfferAltSearch;
            btnExitToMac.Enabled = bAllowChangeExit;
            NewItemID = "";
            NewItemName = "";
            Reg12 = cbHPKB.Font;
            Reg10 = gbAlltSearch.Font;
            fBlue = btnSearch.ForeColor;
            MViews = rMViews;
            cTI = new cTitleInfo();
            CountryCodes = Properties.Resources.Sorted_Raw_List.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            InRepeatMode();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Check if F3 key is pressed
            if (keyData == Keys.F3)
            {
                // Prevent the default behavior for F3
                return true;
            }

            // Call the base class method for other keys
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void SetLastParm()
        {
            foreach(RadioButton rb in gbRB.Controls)
            {
                if(rb.Checked)
                {
                    LastSearchParam = rb.Name;
                    return;
                }
            }
        }

        private void SetParamsFromLast()
        {
            switch(LastSearchParam)
            {
                case "rbExactMatch":
                    rbExactMatch.Checked = true;
                    break;
                case "rbEPhrase":
                    rbEPhrase.Checked = true;
                    break;
                case "rbAnyMatch":
                    rbAnyMatch.Checked = true;
                    break;
            }
        }

        public static void RunSort(int n, bool Descending, ref int[] SI, ref int[] US)
        {
            int a, b;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    a = SI[j];
                    b = SI[j + 1];
                    if (Descending)
                    {
                        if (US[a] < US[b])
                        {
                            SI[j] = SI[j + 1];
                            SI[j + 1] = a;
                        }
                    }
                    else
                    {
                        if (US[a] > US[b])
                        {
                            SI[j] = SI[j + 1];
                            SI[j + 1] = a;
                        }
                    }
                }
            }
        }


        private void InRepeatMode()
        {
            if (Properties.Settings.Default.WSrepeat && LastSearchWord != "")
            {
                tbKeywords.Text = LastSearchWord;
                    SetParamsFromLast();
            }
        }
        private void AreWeRepeating()
        {
            if(Properties.Settings.Default.WSrepeat)
            {
                if(LastSearchWord == "")
                {
                    LastSearchWord = tbKeywords.Text;
                    SetLastParm();
                }
                else
                {
                    if(LastSearchWord != tbKeywords.Text)
                    {
                        Properties.Settings.Default.WSrepeat = false;
                        LastSearchWord = "";
                        Properties.Settings.Default.Save();
                    }
                }
            }
        }

        private void CountryLookup(string sLine)
        {
            CountryCodeResults = "";
            int i = 5; //'#abc "
            string[] sWords = sLine.Split(new char[] { ' ', '\t', ',' },StringSplitOptions.RemoveEmptyEntries);
            foreach(string sWord in sWords)
            {
                foreach (string s in CountryCodes)
                {
                    if (s.StartsWith("#"))
                    {
                        if (s.Substring(i).ToLower().Contains(sWord))
                        {
                            CountryCodeResults += s + Environment.NewLine;
                        }
                    }
                }
            }
        }

        private void AddSelButtons(ref GroupBox gb, int iDir)
        {
            int i = 0;
            int x = 2, y = 18;
            gb.Controls.Clear();
            foreach (string s in Utils.LocalMacroPrefix)
            {
                Button btn = new Button();
                btn.Text = s;
                btn.Width = 50;
                btn.Height = 30;
                if (iDir == 1)
                    btn.Location = new System.Drawing.Point(x + i * (btn.Width + 10), y);
                else
                    btn.Location = new System.Drawing.Point(x, y + i * (btn.Width + 10));
                btn.Enabled = FileFound(s);
                gb.Controls.Add(btn);
                btn.Click += Btn_Click;
                if (i == 0) fDark = btn.ForeColor;
                i++;
            }
        }

        private void FilesHaveMatch()
        {
            foreach (Button b in gbSelect.Controls)
            {
                string s = b.Text;
                b.Enabled = FileFound(s);
            }
        }

        private void SetLangVisable(bool b)
        {
            cbvAddLangRef.Visible = b;
            cbvAddLangRef.Checked = false;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                SetLangVisable(false);
                SelectedFile = button.Text;
                aFilter = "";
                SortTable(0);
                foreach(Button btn in gbSelect.Controls)
                {
                    btn.ForeColor = fDark;
                }
                button.ForeColor = fBlue;
            }
        }

        private void FormCandidateMacros(string sBtnName)
        {
            CMoveSpace cms = new CMoveSpace();
            cms.Init();
            if (lbNewItems.DataSource != null)
                lbNewItems.DataSource = null;
            string s = Utils.sFindUses(sBtnName).Trim();
            string[] sOut = s.Split(new char[] {' '});
            int i = 0;
            foreach(string sID in sOut)
            {
                int n = cms.GetMacCountAvailable(sID);
                if (n == 0) sOut[i] = "";
                i++;
            }
            lbNewItems.Items.Clear();
            lbNewItems.DataSource = sOut;
            gbMakeNew.Visible = true;       
        }


        private string GetRefUrl(string sMacName)
        {
            int i = -1;
            string strRtn = "";
            foreach(cRefURLs cr in RefUrls)
            {
                if(sMacName == cr.sMacN)
                {
                    i = Convert.ToInt32(cr.nMac) ;
                    strRtn = cbvAddLangRef.Checked ? Utils.AddLanguageOption(cr.PageOut) : cr.PageOut;
                     break;
                }
            }
            Debug.Assert(i > 0, "RF macro number not found!");
            return strRtn;
        }

        private void dgvSearched_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || SelectedRow < 0) return;
            List<CFound> bSorted = (List<CFound>)dgvSearched.DataSource as List<CFound>;
            int n = bSorted[SelectedRow].WhereFound;
            string strType = bSorted[SelectedRow].File;
            string strTemp = cAll[n].sBody;
            string sDRecord = cAll[n].rBody;
            if (strTemp == "") return;
            string MacName = dgvSearched.Rows[e.RowIndex].Cells[3].Value.ToString();
            nUseLastViewed = n;
            if (dgvSearched.Rows[e.RowIndex].Cells[0].Value.ToString() == "RF" && cbOnlyRefs.Checked)
            {

                strTemp = GetRefUrl(MacName);
                if (strTemp == "") return;
            }
            else
            {
                if(cbvAddLangRef.Checked) strTemp = Utils.AddLanguageOption(strTemp);
            }

            ShowMacro(ref strTemp,ref sDRecord, strType, MacName);
  
        }

        private void ShowMacro(ref string strTemp, ref string sRec, string strType, string MacName)
        {

            string sOut = "";
            string sModels = "";    // not used here
            bool bRet;
            if (sRec.Length > 4)
            {
                bRet = printerDB.FormatRecord(sRec.Replace("<nl>",Environment.NewLine), ref sOut, ref sModels);
                if (!bRet) return;
            }
            sOut += strTemp;
            Utils.CopyHTML(Utils.ShowRawBrowser(sOut, strType));
            MViews.AddView(strType, MacName);
        }


        private void SortTable(int column)
        {
            bool b;
            int n=0;
            if(column == 2)
            {
                b = !ColSortDirection[2];
                ColSortDirection[2] = b;
                RunMacSort(CFcnt, b);
            }
            if(column == 0) // sort by file
            {
                b = !ColSortDirection[2];
                ColSortDirection[2] = b;
                if(SelectedFile == "")
                {
                    AlphaExtractFile(b);
                    n = aSort.Length;
                }
                else
                {
                    n = JustExtract(SelectedFile);
                }
                if(aFilter != "key")
                {
                    dSorted.Clear();
                    for (int i = 0; i < n; i++)
                    {
                        int j = aSort[i];
                        if (!cSorted[j].bWanted) continue;
                        dSorted.Add(cSorted[j]);
                    }
                    dgvSearched.DataSource = null;
                    dgvSearched.Invalidate();
                    dgvSearched.DataSource = dSorted;
                }
                else
                {
                    aSorted.Clear();
                    for (int i = 0; i < n; i++)
                    {
                        int j = aSort[i];
                        aSorted.Add(cSorted[j]);
                    }
                    dgvSearched.DataSource = null;
                    dgvSearched.Invalidate();
                    dgvSearched.DataSource = aSorted;
                }
                SetDGVwidth();
                dgvSearched.Refresh();
            }
        }

        private int GetLastWhereUsed(int Row)
        {
            int n;
            List<CFound> cFound = dgvSearched.DataSource as List<CFound>;
            if(cFound.Count > 0)
            {
                n = cFound[Row].WhereFound;
            }
            else
            {
                n = cSorted[Row].WhereFound;
            }
            lbKeyFound.Items.Clear();
            string[] sEach = cAll[n].fKeys.Trim().Split(new[] { "\n" }, StringSplitOptions.None);
            foreach (string s in sEach)
            {
                lbKeyFound.Items.Add(s);
            }
            return n;
        }

        private void dgvSearched_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRow = e.RowIndex;
            btnExitToMac.Enabled = true;
            btnExitTitle.Enabled = false;
            if (SelectedRow < 0)
            {
                SortTable(e.ColumnIndex);
                dgvSearched.Refresh();
                return;
            }
            nUseLastViewed = GetLastWhereUsed(SelectedRow);
            List<CFound> bSorted = (List<CFound>)dgvSearched.DataSource as List<CFound>;
            cbvAddLangRef.Visible = bSorted[SelectedRow].MayHaveLanguage;
        }

        private bool InHTTP(string v, int inx, string s)
        {
            // minimum of 9 chars t the 'b' http://a.b.c
            int i = inx - 1;
            int j = inx + 1;
            int n = v.Length;
            if (i < 8) return false;
            int k = n + j - i;
            string t = "." + v + ".";
            if((k + i) >= s.Length) return false;
            if (t == s.Substring(i, k)) return true;
            return false;
        }

        private string PerformSearch(int iBody, string sName, string sBody, string sMacID)
        {
            string text = sName + sBody;
            string strRtn = "";
            string sTmp = "";
            string sPattern = "";
            int i, n = keywords.Count;
            Regex regex;
            MatchCollection allMatches = null ;
            for (i = 0; i < n; i++) KeyCount[i] = 0;
            i = 0;
            foreach (string keyword in keywords)
            {
                if (keyword.Length > 1)
                {

                 cTI.Search(iBody, sName, keyword);

                if(keyword.Contains(" ") || rbEPhrase.Checked)
                {
                    sPattern = $@"\b{Regex.Escape(keyword)}\b";
                }
                else
                {
                    if(rbExactMatch.Checked)
                    {
                        sPattern = "\\b" + Regex.Escape(keyword) + "\\b";
                    }
                    else
                    {
                        sPattern = "\\b\\w*" + keyword + "\\w*\\b";
                    }
                }
                regex = new Regex(sPattern, cbIgnCase.Checked ? RegexOptions.IgnoreCase : RegexOptions.None);
                // need to avoid metacharacters in search such as [] or [ 

                try
                {
                    allMatches = regex.Matches(text);
                }
                catch (Exception ex)
                {
                }
                TotalMatches += allMatches.Count;
                foreach (Match m in allMatches)
                {
                    // ignore any text that is in the http part such as .hp.
                    if (InHTTP(m.Value.ToLower(), m.Index, text.ToLower())) continue;
                    sTmp = m.Value.ToLower();
                    if(sMacID == "RF" ) // && false
                    {
                        cRefURLs cr = RefUrls.Last();
                        bool b = cr.LookForUrl(m.Index, ref text);
                        if(!b)
                        {
                            // was NOT a match as was part of the url
                            KeyCount[i]--;
                        }
                    }
                    KeyCount[i]++;
                    if (strRtn.Contains(sTmp) || KeyCount[i] == 0) continue;
                    strRtn += sTmp + "\n";
                }
                }

                i++;
            }
            return strRtn;
        }

        private void CountFile(string s)
        {
            if (!HasFiles.Contains(s))
                HasFiles += s + " ";
        }

        private bool FileFound(string s)
        {
            return HasFiles.Contains(s) ;
        }

 
        private string RemoveSurroundQuotes(string s)
        {
            int n = s.Length-1;
            string t = s.Substring(0,1);
            if(t == "\"" || t == "'")
            {
                if(t == s.Substring(n,1))
                {
                    return s.Substring(1, n - 1);
                }
            }
            return s;
        }


        private string FormBetter(string s)
        {
            string t = s.ToLower();
            if (t.Contains("wi-fi"))
            {
                return s + " wifi";
            }
            if (t.Contains("wifi"))
            {
                return s + " wi-fi";
            }
            return s;
        }

        private void SetFont(Font f)
        {
            cbHPKB.Font = f;
            cbOfferAlt.Font = f;
        }

        private void TestForCountryCode(string sLine)
        {
            int i = sLine.IndexOf("country ");
            bool b = ((i >= 0) && (sLine.Length > 10));
            btnShowCC.Visible = b;
        }



        private void SplitStringWithQuotedPhrases(string input)
        {
            // Regex pattern to match words and quoted phrases
            string pattern = @"(""(?:\\.|[^""])*""|\S+)";
            keywords = new List<string>();
            if (rbEPhrase.Checked)
            {
                keywords.Add(input);
                return;
            }
            List <string> CandidateItems = new List<string>();
            foreach (Match match in Regex.Matches(input, pattern))
            {
                CandidateItems.Add(RemoveSurroundQuotes(match.Value));
            }
            keywords = CandidateItems.Distinct().ToList();
            if (keywords.Count != CandidateItems.Count)
            {
                string sOut = "";
                foreach (string s in keywords)
                {
                    sOut += s + " ";
                }
                tbKeywords.Text = sOut;
            }
            return;
        }

        private void SetMatchType()
        {
            if (rbExactMatch.Checked)
                SearchCode = "(E)";
            else if (rbAnyMatch.Checked)
                SearchCode = "(A)";
            else if (rbEPhrase.Checked)
                SearchCode = "(P)";                
        }


        private void RunSearch()
        {
            int iBody = 0;
            string sBetter = "";
            cFound.Clear();
            cSorted.Clear();
            RefUrls.Clear();
            aSorted.Clear();
            TotalMatches = 0;
            TriedFailed = true;
            lbKeyFound.Items.Clear();
            gbFound.Text = "Words Found:";
            tbMissing.Text = "";
            HasFiles = "";
            SelectedFile = "";
            cbvAddLangRef.Visible = false;
            cbvAddLangRef.Checked = false;
            dgvSearched.DataSource = null;
            dgvSearched.Rows.Clear();
            cbSelKey.Visible = !rbEPhrase.Checked;
            cbSelKey.Items.Clear();
            WhichMatch = 0;
            TestForCountryCode(tbKeywords.Text.ToLower());

            sBetter = FormBetter(tbKeywords.Text.Trim());
            if (rbEPhrase.Checked)
                sBetter = tbKeywords.Text.Trim().Replace("\"", "");
            else sBetter = FormBetter(tbKeywords.Text.Trim());
            tbKeywords.Text = sBetter;

            SplitStringWithQuotedPhrases(sBetter);

            MaxMatches = 0;
            int n = keywords.Count;
            int i = 0;
            KeyPresent = new bool[n];
            KeyCount = new int[n];
            CFcnt = 0;
            n = cAll.Count;
            Unsorted = new int[n];
            SortInx = new int[n];
            if (cbHPKB.Checked)
                HP_KB_find();
            lbDroppedText = "";

            cTI.Init();
            lbTitleSearch.Items.Clear();

            foreach (string s in keywords)
            {
                if(s.Length == 1)
                {
                    if (!lbDroppedText.Contains(s))
                        lbDroppedText +=s + ",";
                }
            }
            SetMatchType();
            foreach (CBody cb in cAll)
            {
                string sPrN = cb.Name + " ";
                if (cb.File == "RF") // && false
                {
                    cRefURLs cr = new cRefURLs();
                    cr.init(cb.File, cb.Name, cb.Number);
                    RefUrls.Add(cr);
                    //sPrN = "";  // RF does not need to have the name searched unlike all other macro information.
                }

                string sKeys = PerformSearch(iBody, sPrN, cb.sBody, cb.File);  // eg: do not include "support" for RF
                iBody++;
                if (sKeys != "")
                {
                    n = 0;
                    CFound cf = new CFound();
                    cf.bWanted = true;
                    cf.WhichMatch = 0;
                    cf.MayHaveLanguage = cb.sBody.IndexOf(Utils.sPossibleLanguageOption[0]) > -1;
                    if(KeyCount.Length == 1)
                    {
                        foreach (int m in KeyCount)
                        {
                            n += m;
                        }
                    }
                    else
                    {
                        int iBit = 1;
                        foreach (int m in KeyCount)
                        {
                            n += (m > 0) ? 1 : 0;
                            if(m>0)
                            {
                                cf.WhichMatch |= iBit;
                            }
                            iBit = iBit << 1;
                        }
                    }
                    cAll[i].fKeys = sKeys;
                    SortInx[CFcnt] = CFcnt;
                    Unsorted[CFcnt] = n;
                    cAll[i].nWdsfKey = Unsorted[CFcnt];
                    CFcnt++;
                    cf.Name = cb.Name;
                    cf.Number = cb.Number;
                    cf.File = cb.File;
                    CountFile(cb.File);
                    cf.Found = n.ToString();
                    MaxMatches = Math.Max(n, MaxMatches);
                    cf.WhereFound = i;
                    WhichMatch |= cf.WhichMatch;
                    cFound.Add(cf);
                }
                else
                {
                    cAll[i].fKeys = "";
                }
                i++;
            }
            if(CFcnt > 0)
            {
                AreWeRepeating();
            }
            else
            {
                // want to keep track of the how many searches turned up empty
                MViews.AddView("MI", SearchCode + tbKeywords.Text);
            }
            NotifyFinding(CFcnt);
            RunMacSort(CFcnt, true);
            gbFound.Text ="Words Found: " + cFound.Count.ToString();
            ShowTitles();
            if(cFound.Count > 0)
            {
                cbSelKey.Items.Clear();
                WhereInx.Clear();
                cbSelKey.Items.Add("Any");
                if(MaxMatches == cFound.Count)
                    cbSelKey.Items.Add("All");
                else cbSelKey.Items.Add("All " + MaxMatches.ToString());
                int k = 0;
                int kbit = 1;
                foreach (string s in keywords)
                {
                    if((WhichMatch & kbit) > 0)
                    {
                        cbSelKey.Items.Add(s);
                        WhereInx.Add(k);
                    }
                    k++;
                    kbit = kbit << 1;
                }
                SaveHasFiles = HasFiles;
                cbSelKey.SelectedIndexChanged -= cbSelKey_SelectedIndexChanged;
                cbSelKey.SelectedIndex = 0;
                cbSelKey.SelectedIndexChanged += cbSelKey_SelectedIndexChanged;
            }
        }

        private void NotifyFinding(int cnt)
        {
            string sMissing = "";
            int i = 1;
            foreach(string s in keywords)
            {
                if((WhichMatch & i) == 0)
                {
                    if(lbDroppedText.Contains(s))
                        sMissing += "(i) " + s;
                    else sMissing+= s;
                    sMissing += Environment.NewLine;
                }
                i = i << 1;
            }
            tbMissing.Text = sMissing;
            gbAlltSearch.Visible = cnt==0 && cbOfferAlt.Checked;
            if (gbAlltSearch.Visible) SetFont(Reg10);
            else SetFont(Reg12);
            gbMakeNew.Visible = false;
        }

        private int JustExtract(string w)
        {
            aSort = new int[cFound.Count];
            int i = 0;
            foreach (string s in Utils.LocalMacroPrefix)
            {
                if (s != w) continue;
                for (int j = 0; j < cFound.Count; j++)
                {
                    if (s == cSorted[j].File)
                    {
                        aSort[i] = j;
                        i++;
                    }
                }
            }
            return i;
        }

        // get alphabet sort order for file "AIO" "LJ" etc.
        private void AlphaExtractFile(bool b)
        {
            aSort = new int[cFound.Count];
            int i = 0;
            if(b)
            {
                foreach (string s in Utils.LocalMacroPrefix)
                {
                    for (int j = 0; j < cFound.Count; j++)
                    {
                        if (s == cSorted[j].File)
                        {
                            aSort[i] = j;
                            i++;
                        }
                    }
                }
            }
            else
            {
                foreach (string s in Utils.LocalMacroPrefix.AsEnumerable().Reverse())
                {
                    for (int j = 0; j < cFound.Count; j++)
                    {
                        if (s == cSorted[j].File)
                        {
                            aSort[i] = j;
                            i++;
                        }
                    }
                }
            }
        }

        private void SetDGVwidth()
        {
            int j = 0;
            foreach (int k in cWidth)
                dgvSearched.Columns[j++].Width = k;
            dgvSearched.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void RunMacSort(int CFcnt, bool Descending)
        {
            int i, j, n;
            if (CFcnt == 0) return;
            //dgvSearched.RowEnter -= dgvSearched_RowEnter;
            cSorted.Clear();
            RunSort(CFcnt, Descending, ref SortInx, ref Unsorted);
            n = cFound.Count;
            SelectedFile = "";
            for (i = 0; i < n; i++)
            {
                j = SortInx[i];
                //if ((cFound[j].WhichMatch & ShowBits) == ShowBits  || bIgnoreSB)
                cSorted.Add(cFound[j]);
            }
            dgvSearched.DataSource = cSorted;
            dgvSearched.Columns[1].HeaderText = "Mac#";
            SetDGVwidth();
            if (TotalMatches > 0)
            {
                //tbNumMatches.Text = TotalMatches.ToString();
                TriedFailed = false;
            }
            else
            {
                //tbNumMatches.Text = "";
                TriedFailed = true;
            }
            gbSelect.Visible = TotalMatches > 0;
            if(TotalMatches > 0)
            {
                AddSelButtons(ref gbSelect, 1); // +1 is to the right
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RunSearch();
        }

        private void tbKeywords_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                RunSearch();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExitToMac_Click(object sender, EventArgs e)
        {
            LastViewed = nUseLastViewed;
            this.Close();
        }

        private void WordSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.IncludeHPKB = cbHPKB.Checked;
            Properties.Settings.Default.OfferAltSearch = cbOfferAlt.Checked;
            Properties.Settings.Default.Save();
        }

        //https://h30434.www3.hp.com/t5/forums/searchpage/tab/message?filter=includeTkbs&q=cm1415&include_tkbs=true&collapse_discussion=true
        private void HP_KB_find()
        {
            string s = tbKeywords.Text.Trim();
            if (s == "") return;
            //string strUrl = "https://h30434.www3.hp.com/t5/forums/searchpage/tab/message?advanced=false&allow_punctuation=false&q=";
            string strUrl = "https://h30434.www3.hp.com/t5/forums/searchpage/tab/message?filter=includeTkbs&include_tkbs=true&q=";            
            Utils.LocalBrowser(strUrl + s);
        }



        private void WordSearch_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            EditHelp("SEARCH");
        }

        private void cbOfferAlt_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbOfferAlt.Checked)
            {
                gbAlltSearch.Visible = TriedFailed;
            }
            else gbAlltSearch.Visible = false;
            if (gbAlltSearch.Visible) SetFont(Reg10);
            else SetFont(Reg12);
        }


        private string PlusConCat(string t, List<string> ss)
        {
            string sRtn = t;
            int i = t.IndexOf('&');
            string sAppend = "";
            if(i != -1)
            {
                sAppend = t.Substring(i);
                sRtn = t.Substring(0,i);
            }
            foreach(string s in ss)
            {
                sRtn += s + "+";
            }
            t = sRtn.Substring(0, sRtn.Length - 1) + sAppend;
            return t;
        }

        private void RunB(string s, string t)
        {
            Utils.LocalBrowser(s + PlusConCat(t, keywords));
        }

        //https://www.ebay.com/sch/i.html?_nkw=hp+cm1415&_sacat=58058
        private void AltSearch(string sKey)
        {
            FormCandidateMacros(sKey);
            switch(sKey)
            { 
                case "PC":
                    RunB("https://www.google.com/search?q=", "DRIVERS+HP+");
                    RunB("https://www.google.com/search?q=", "DISASSEMBLE+HP+");
                    break;
                case "PRN":
                    RunB("https://www.google.com/search?q=", "PRINTER+DRIVERS+HP+");
                    RunB("https://www.google.com/search?q=", "PRINTER+MANUAL+HP+");
                    RunB("https://www.google.com/search?q=", "FACTORY+RESET+HP+");
                    RunB("https://www.google.com/search?q=", "YOUTUBE+NETWORK+CONNECT+HP+");
                    RunB("https://www.youtube.com/@HPSupport/search?query=", "");
                    RunB("https://support.hp.com/us-en/deviceSearch?q=", "&origin=pdp");
                    break;
                case "EBA":
                    RunB("https://www.ebay.com/sch/i.html?_nkw=", "HP &_sacat=58058");
                    break;
                case "GOO":
                    RunB("https://www.google.com/search?q=", "HP+");
                    break;
                case "MAN":
                    RunB("https://www.google.com/search?q=", "HP+MANUAL+");
                    break;
                case "DRV":
                    RunB("https://www.google.com/search?q=", "HP+DRIVERS+");
                    break;
                case "HPYT":
                    RunB("https://www.youtube.com/@HPSupport/search?query=", "");
                    break;
                case "HPKB":
                    HP_KB_find();
                    break;
            }

        }

        private void btnPC_Click(object sender, EventArgs e)
        {
            AltSearch("PC");
        }

        private void btnPrn_Click(object sender, EventArgs e)
        {
            AltSearch("PRN");
        }

        private void btnEbay_Click(object sender, EventArgs e)
        {
            AltSearch("EBA");
        }

        private void btnGoogle_Click(object sender, EventArgs e)
        {
            AltSearch("GOO");
        }

        private void btnMan_Click(object sender, EventArgs e)
        {
            AltSearch("MAN");
        }

        private void btnDrv_Click(object sender, EventArgs e)
        {
            AltSearch("DRV");
        }

        private void btnKbSearch_Click(object sender, EventArgs e)
        {
            AltSearch("HPKB");
        }

        private void btnHpYTsup_Click(object sender, EventArgs e)
        {
            AltSearch("HPYT");
        }

        private void btnMakeNew_Click(object sender, EventArgs e)
        {
            int r = lbNewItems.SelectedIndex;
            if (r < 0) return;
            string s = lbNewItems.Items[r].ToString();
            if (s == "") return;    // no space left
            NewItemID = s;
            NewItemName = tbKeywords.Text.Trim();
            this.Close();
        }

        private void btnShowCC_Click(object sender, EventArgs e)
        {
            string sLine = tbKeywords.Text.ToLower();
            int i = sLine.IndexOf("country ");
            bool b = ((i >= 0) && (sLine.Length > 10));
            btnShowCC.Visible = b;
            if (btnShowCC.Visible)
            {
                string sTemp = sLine.Replace("country", "");
                CountryLookup(sTemp);
            }
            lbKeyFound.Items.Clear();
            lbKeyFound.Items.Add("Double click to capture code");
            lbKeyFound.Items.AddRange(CountryCodeResults.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries));
        }

        private void FormOnlyThisKey(int inx)
        {
            int i, j, n;
            string sWhichKey = cbSelKey.SelectedItem.ToString();
            int iFound = keywords.FindIndex(item => item == sWhichKey);
            int iMask = 1 << iFound;
            aSorted.Clear();
            n = GetLastWhereUsed(SelectedRow);
            string[] sEach = cAll[n].fKeys.Trim().Split(new[] { "\n" }, StringSplitOptions.None);
            n = cFound.Count;
            dgvSearched.DataSource = cSorted;
            if (inx == 0)
            {
                gbFound.Text = "Words Found: " + cSorted.Count.ToString();
                HasFiles = SaveHasFiles;
                for (i = 0; i < n; i++)
                {
                    j = SortInx[i];
                    cFound[j].bWanted = true;
                }
                return;
            }
            inx--;
            HasFiles = "";
            for (i = 0; i < n; i++)
            {
                j = SortInx[i];
                cFound[j].bWanted = false;
                if (inx > 0)
                {

                    if ((cFound[j].WhichMatch & iMask) > 0)
                    {
                        aSorted.Add(cFound[j]);
                        cFound[j].bWanted = true;
                        CountFile(cFound[j].File);
                    }
                }
                else
                {
                    int v = Utils.CountSetBits(cFound[j].WhichMatch);
                    if (v == MaxMatches)
                    {
                        aSorted.Add(cFound[j]);
                        cFound[j].bWanted = true;
                        CountFile(cFound[j].File);
                    }
                }
            }
            dgvSearched.RowEnter -= dgvSearched_RowEnter;
            dgvSearched.DataSource = aSorted;
            dgvSearched.RowEnter += dgvSearched_RowEnter;
            gbFound.Text = "Words Found: " + aSorted.Count.ToString();
            aFilter = "key";
        }

        // this is used by the country code lookup
        private void lbKeyFound_DoubleClick(object sender, EventArgs e)
        {
            int index = lbKeyFound.SelectedIndex;
            if (index < 0) return;
            string sCode = lbKeyFound.Items[index].ToString();
            if (btnShowCC.Visible)
            {
                string tCode = lbKeyFound.Items[0].ToString();
                if(tCode.Contains("Double click"))
                {
                    Clipboard.SetText(sCode.Substring(0, 4));
                    return;          
                }
            }

        }


        private void cbSelKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormOnlyThisKey(cbSelKey.SelectedIndex);
            FilesHaveMatch();
        }

        private void dgvSearched_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SortTable(e.ColumnIndex);
            dgvSearched.Refresh();
            return;
        }

        private void lbTitleSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnExitToMac.Enabled = false;
            btnExitTitle.Enabled = true;
            int i = lbTitleSearch.SelectedIndex;
            if (i < 0) return;
            if (cTI.nSorted.Length <= i) return;
            int n = cTI.nSorted[i];
            if(cTI.ctd.Count <= n)return;
            mUseLastViewed = cTI.ctd[n].LocInCBody;
        }

        private void lbTitleSearch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(mUseLastViewed < 0 || (mUseLastViewed >= cAll.Count-1))
            {
                string sOut = "bug trap: mUse" + mUseLastViewed.ToString() + " count " + cAll.Count.ToString();
                MessageBox.Show(sOut, "error in Word Search lookup");
                return;
            }
            string strTemp = cAll[mUseLastViewed].sBody;
            string strType = cAll[mUseLastViewed].File;
            string MacName = cAll[mUseLastViewed].Name;
            string sRecord = cAll[mUseLastViewed].rBody;
            ShowMacro(ref strTemp, ref sRecord, strType, MacName);
            
        }

        private void btnExitTitle_Click(object sender, EventArgs e)
        {
            LastViewed = mUseLastViewed;
            this.Close();
        }

        private void ShowTitles()
        {
            int n = cTI.DoSort();
            for (int i = 0; i <n; i++)
            {
                lbTitleSearch.Items.Add(cTI.GetName(i));
            }
        }

        private void ChangeSearch(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                //string name = rb.Name;
                RunSearch();
            }
        }
    }
}
