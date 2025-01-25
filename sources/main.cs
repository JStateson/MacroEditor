//#define SPECIAL2
//#define SPECIAL2
//#define SPECIAL2
//#define SPECIAL2
//#define SPECIAL3
//#define SPECIAL4
/*
 * to change font size visual studio ctrl shift . or ,
 * zoom is ctrl shift dot  or comma to set font in visual studio
 * load file is a comment for when a macro is read to be displayed in the datagridview
 * show macro is a command for ShowUneditedRow where a macro is displayed from the datagridview
 * design bug 8/15/2024: some tests for number of items read do not work as the variable is not updated
 * until the button-click-function returns giving the app the ability to show the updated values
 * example: 40 items in a file the file is deleted and a load request is made to read but the
 * number of items is the datagridview is still 40
 * datagridview is loaded only after the "delete all checked" returns so the test for the
 * number of macros returns 40 instead of 0.
 * caused by side effect of tbody.text = "" when numinbody holds old value
 * possibly fixed this date but made this comment to remember the problem and try to clean it up
 * 
 * some key functions
 * ShowUneditedRow(n) n is the row to position the cursor on when a macro file is loaded
 *        for initial display or programmatically if the macro needs be displayed
 * ShowBodyFromSelected is the function called from ShowUneditedRow and this fills in tbody.text
 *        it uses a key to obtain raw DataFileRecord and DataFileFormatted);
 * LoadFromTXT(s) and SaveAsTXT(s) read and write the macrofile who's ID string is s
 *        if the ID string is "HP" then the macro file is named HPMacros.txt in the same folder as the app
 * LoadAllFiles reads and stores macros into memory database for a fast search for keywords.
 *        This is done once at program initialization and once before duplicate urls are searched.
 *        Changes made to macros are not updated in the search database so the keyword search will
 *        miss the new items.  You must exit the program and restart it to update the database.
*/

using System;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Linq;
using System.Windows.Ink;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using System.Windows.Media.Animation;
using System.Dynamic;
using System.Configuration;
using System.Security.Policy;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Globalization;
using System.Data;
using MacroEditor.sources;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Data;
using AxWMPLib;
using System.Windows.Controls;
using System.Web;
using System.IO.Compression;
using System.Diagnostics.Eventing.Reader;
using static MacroEditor.CSendCloud;
using MacroEditor.Properties;
using Button = System.Windows.Forms.Button;
using Microsoft.Office.Interop.Word;
using Font = System.Drawing.Font;
using System.Windows.Media;
using Color = System.Drawing.Color;
using System.Xml.Linq;
using System.Windows.Shapes;
using Path = System.IO.Path;
using static System.Windows.Forms.AxHost;
using static System.Net.WebRequestMethods;
using File = System.IO.File;


namespace MacroEditor
{
    public partial class main : Form
    {
        private bool bForceClose = false;
        private int NumInBody = 0;  // probably should have used a List<string> instead of [Utils.NumMacros]
        private bool bHaveHTMLasLOCAL = false;      // if true then we just read in html 
        private bool bShowingError = false; // if true then highlighting errors between HP and local
        private bool bHaveBothHP = false; // have read both the HTML and the HPmacros for convenience uploading
        private string aPage;
        private int[] StartMac = new int[Utils.HPmaxNumber];
        private int[] StopMac = new int[Utils.HPmaxNumber];
        private int[] MacBody = new int[Utils.HPmaxNumber];
        private string strType = "";    // either PRN or PC for printer or pc macros or HP 
        private string TXTName = "";
        private int CurrentRowSelected = -1;
        private OpenFileDialog ofd;
        private bool bMacroErrors;
        private bool bInitialLoad = false;  // this is used with the bad ending to look for
        private List<string> strBadEnding = new List<string>();
        private bool bHaveHTML = false; // html macro was read in. this cannot be edited
        private bool bHaveHP = false;   // have an HPMacro.txt file
        private int NumSupplementalSignatures = 0;
        private Color NormalEditColor;
        public CMoveSpace cms;
        public List<CBody> cBodies;  // this is only updated when the program starts
        private string sBadMacroName;
        private string TextFromClipboardMNUs = "";
        private bool bTextFromClipboardMNUs; // can text be used as an argument to a search
        private cMacroChanges xMacroChanges;
        private cMacroChanges xMacroViews;
        private int tbBodyChecksumN = 0;
        private bool tbBodyChecksumB = false;
        private int NumCheckedMacros = 0;
        private string LastViewedFN = "";   // last macro file prefix
        private string UnfinishedFN = "";   // this file has "Change Me" or empty body
        private string UnfinishedMN = "";   // this macro is the unfinished one
        private int UnfinishedIndex = -1;
        private string vWarning = "";
        public List<dgvStruct> DataTable;   // from reading supplemental files
        public List<dgvStruct> HPDataTable; // from reading the HP HTTP file
        public BindingSource MyBindingSource = new BindingSource();
        public cDupHTTP DupHTTP;
        private bool tbChangeNotifed = false;   // if true then we notified the user the edit needs to be saved
        public cCheckSpell MySpellCheck = new cCheckSpell();
        private string[] BadSpell;
        private int iBadSpellIndex = 0;
        private bool bFocusLost = false;
        private List<int> SpellCandidates = new List<int>();
        private List<int> SpellIndex = new List<int>();
        private int nSavedCount = 0;
        private PrinterDB printerDB = new PrinterDB();
        private List<cQCmacros> AssociateMacros = new List<cQCmacros>();
        private string DataFileRecord = "";   // the tbody.text equivalent for the macroid record that is written to datafile and
        private bool bDataFileUnsaved;        // this is read in (if it exists) each time tbody.text is filled in.  bool true if unsaved
        private string DataFileFormatted = ""; // the displayable html from the raw datarecord
        private string HasNewDataRecord = "";   // if value then may need to update the datarecord. applies to update urls only
        private string HasNewFormattedData = "";

        public main()
        {
            InitializeComponent();
            lbName.AutoGenerateColumns = false;
            Utils.WhereExe = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString() +
                "\\" + Utils.MacPrinterFolder;
            Utils.FormAllTxt();
            EnableMacEdits(false);
            gbManageImages.Enabled = true;// System.Diagnostics.Debugger.IsAttached;
            int iBrowser = Properties.Settings.Default.BrowserID;
            if (iBrowser < 0) Utils.BrowserWanted = Utils.eBrowserType.eEdge;
            else Utils.BrowserWanted = (Utils.eBrowserType)Properties.Settings.Default.BrowserID;
            Utils.VolunteerUserID = Properties.Settings.Default.UserID;
            Utils.nLongestExpectedURL = Properties.Settings.Default.LongestExpectedURL;
            string strFilename = Properties.Settings.Default.HTTP_HP;
            this.Text = " HP Macro Editor";
            settingsToolStripMenuItem.ForeColor = Color.Black;
            xMacroChanges = new cMacroChanges();
            xMacroChanges.Init("MacroChanges.txt");
            xMacroViews = new cMacroChanges();
            xMacroViews.Init("MacroViews.txt");
            DataTable = new List<dgvStruct>();
            HPDataTable = new List<dgvStruct>();
            cms = new CMoveSpace();
            Utils.bSpellingEnabled = MySpellCheck.Init();
            if (!Utils.bSpellingEnabled)
            {
                btnSpellChk.Enabled = false;
                btnNextChk.Enabled = false;
                cbLaunchPage.Enabled = false;
            }
            NormalEditColor = btnCancelEdits.ForeColor;
            SetFGcolor("#FF6600");
            if (bForceClose)
            {
                timer1.Interval = 500;
            }

            vWarning = lblVurl.Text;
            if (Properties.Settings.Default.Vdisable)
            {
                lblVurl.Text = "CTRL-V behavies as usual" + Environment.NewLine + "No hyperlink shortcut";
            }
            else
            {
                lblVurl.Text = vWarning;
            }
            ConfigureAssociation();
            this.Shown += LoadInitialFiles;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Check if F3 key is pressed
            if (keyData == Keys.F3)
            {
                // Prevent the default behavior for F3
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void CheckPWE()
        {
            btnSpecialWord.Enabled = (Properties.Settings.Default.SpecialWord != "");
            btnCopyEmail.Enabled = (Properties.Settings.Default.sEmail != "");
        }

        private void LoadInitialFiles(object sender, EventArgs e)
        {
            printerDB.InitDB();
            settingsToolStripMenuItem.ForeColor = (Utils.CountImages() > 20) ? Color.Red : Color.Black;
            Utils.TotalNumberMacros = LoadAllFiles();
            CheckPWE(); // see if password and email are enabled for using
            mnuCmpHTTP.Enabled = File.Exists(Properties.Settings.Default.HTTP_HP);
            GetRecentArchive(); // this includes all files so files must be closed
            if (Properties.Settings.Default.cSplash == Properties.Settings.Default.sSplash) return;
            splash MySplash = new splash();
            MySplash.Show();
        }


        private string IgnoreSupSig(string s)
        {
            string strRtn = s;
            int i = s.IndexOf(Utils.SupSigPrefix);
            if (i > 0)
            {
                strRtn = Utils.NoTrailingNL(s.Substring(0, i));
            }
            return strRtn.Trim();
        }

#if aint
// bMacroError might be used here
        private void LookForHTMLfix()
        {
            bool b = false;
            bool c = false;
            for (int i = 0; i < Utils.NumMacros; i++) // was NIB
            {
                OriginalColor[i] = 0;
                bHPcorrected[i] = false;
                if (HTMLerr[i]) // there is an HTML error
                {
                    OriginalColor[i] = 1;
                    if (!HPerr[i])  // it was fixed here
                    {
                        b = true;
                        bHPcorrected[i] = true;
                        SetColorBlue(i);
                        OriginalColor[i] = 2;
                    }
                }
                HTMLerr[i] |= HPerr[i];
                c |= HTMLerr[i];
                if (HTMLerr[i])
                {
                    if (OriginalColor[i] != 2)  //todo TODO to do need to clean this up
                        OriginalColor[i] = 1;
                }
            }
            bHaveBothHPerr = c;
            bShowingError = c;
            bShowingDiff = false;
            lbRCcopy.Visible = b;
            bHaveBothHP = true;
        }
        
        private void RestoreColors()
        {
            for (int i = 0; i < NumInBody; i++)
            {
                switch (OriginalColor[i])
                {
                    case 0: SetDefaultCellColor(i);
                        break;
                    case 1: SetErrorRed(i);
                        break;
                    case 2: SetColorBlue(i);
                        break;
                }

            }
        }


        private void ShowHighlights()
        {
            if (bHaveBothHP)
            {
                if (bShowingError && bHaveBothHPerr)
                {
                    mShowDiff.Text = "Show Errors";
                    bShowingError = false;
                    bShowingDiff = true;
                    HighlightDIF();
                    return;
                }
                if (bShowingDiff && bHaveBothDIFF)
                {
                    mShowDiff.Text = "Show Diff";
                    bShowingError = true;
                    bShowingDiff = false;
                    //LookForHTMLfix();   // highlights errors TODO to do todo
                    RestoreColors();
                    return;
                }
                // there are no errors or no differences
                bShowingError = false;
                bShowingDiff = true;
                HighlightDIF();
            }
        }



        private void HighlightDIF()
        {
            int i = 0;
            foreach (dgvStruct row in DataTable)
            {
                if (row.HP_HTML_NO_DIFF)
                    SetDefaultCellColor(i);
                else SetErrorRed(i);
                i++;
            }
        }
#endif
        private static string GetDownloadsPath()
        {
            if (Environment.OSVersion.Version.Major < 6) throw new NotSupportedException();
            IntPtr pathPtr = IntPtr.Zero;
            try
            {
                SHGetKnownFolderPath(ref FolderDownloads, 0, IntPtr.Zero, out pathPtr);
                return Marshal.PtrToStringUni(pathPtr);
            }
            finally
            {
                Marshal.FreeCoTaskMem(pathPtr);
            }
        }


        private static Guid FolderDownloads = new Guid("374DE290-123F-4565-9164-39C4925E467B");
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern int SHGetKnownFolderPath(ref Guid id, int flags, IntPtr token, out IntPtr path);

        private string GetLastFolder()
        {
            string LastFolder = Properties.Settings.Default.LastFolder;
            if (LastFolder == null || LastFolder == "")
            {
                LastFolder = GetDownloadsPath();
                if (!Directory.Exists(LastFolder))
                    LastFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            return LastFolder;
        }


        private void FormMain_Load(object sender, EventArgs e)
        {
            ofd = new OpenFileDialog();
            ofd.DefaultExt = "*.html";
            ofd.InitialDirectory = GetLastFolder();
        }

        private void SetErrorRed(int r)
        {
            lbName.Rows[r].Cells[0].Style.Font = new Font("Arial", 10, FontStyle.Bold);
            lbName.Rows[r].Cells[0].Style.ForeColor = Color.Red;
        }

        private void SetColorGreen(int r)
        {
            lbName.Rows[r].Cells[0].Style.Font = new Font("Arial", 10, FontStyle.Bold);
            lbName.Rows[r].Cells[0].Style.ForeColor = Color.DarkGreen;
        }

        private void SetColorBlue(int r)
        {
            lbName.Rows[r].Cells[0].Style.Font = new Font("Arial", 10, FontStyle.Bold);
            lbName.Rows[r].Cells[0].Style.ForeColor = Color.Blue;
        }

        private void SetDefaultCellColor(int r)
        {
            lbName.Rows[r].Cells[0].Style.Font = new Font("Arial", 10, FontStyle.Regular);
            lbName.Rows[r].Cells[0].Style.ForeColor = Color.Black;
        }


        private string CorrectSpell(string s)
        {
            if (s == null) return "";
            bool b = true;
            string s1, s2;
            string sLC = s.ToLower();
            string sFill = "zzzzzzzzzz";
            string[] sBad = new string[] { "wifi", "wi-fi", "Users Manual" };
            string[] sGood = new string[] { "Wi-Fi", "Wi-Fi", "User Manual" };
            while (b)
            {
                int n = 0;
                int m = 0;
                foreach (string t in sBad)
                {
                    int i = sLC.IndexOf(t);
                    if (i != -1)
                    {
                        n++;
                        s1 = s.Substring(0, i);
                        s2 = s.Substring(i + t.Length);
                        s = s1 + sGood[m] + s2;
                        s1 = sLC.Substring(0, i);
                        s2 = sLC.Substring(i + t.Length);
                        sLC = s1 + sFill.Substring(0, sGood[m].Length) + s2;
                    }
                    m++;
                }
                b = n > 0;
            }
            return s;
        }

        private bool FindBody()
        {

            string strFind; //<span class="html-attribute-value">profilemacro_2</span>"&gt;</span>
            string strEnd = "<span class=\"html-tag\">";
            int j, k, n;
            bMacroErrors = false;
            for (int i = 0; i < HPDataTable.Count; i++)
            {
                strFind = "<span class=\"html-attribute-value\">profilemacro_" + (i + 1).ToString() + "</span>\"&gt;</span>";
                j = aPage.Substring(MacBody[i]).IndexOf(strFind);
                if (j < 0) return false;
                if (MacBody[i] == 0) continue;  // empty body is ok 
                n = MacBody[i] + j + strFind.Length;
                k = aPage.Substring(n).IndexOf(strEnd);
                if (k < 0) return false;
                string strBody = aPage.Substring(n, k);
                while (strBody.Contains("&amp;"))
                {
                    strBody = strBody.Replace("&amp;", "&");
                }

                strBody = strBody.Replace("&lt;", "<").Replace("&gt;", ">");
                strBody = Utils.RemoveNL(strBody.Replace("&nbsp;", " "));
                strBody = Utils.RemoveNL(strBody.Replace("&quot;", "'"));


                HPDataTable[i].sBody = strBody;
                strFind = Utils.BBCparse(strBody);
                HPDataTable[i].sErr = strFind;
                HPDataTable[i].HPerr = (strFind != "");
                if (HPDataTable[i].HPerr)
                {
                    bMacroErrors = true;
                    // SetErrorRed(i);
                }
            }
            mShowErr.Visible = bMacroErrors;
            bShowingError = bMacroErrors;
            return true;
        }

        private bool FindNames()
        {
            int j, k, n;
            int NumUsed = 0;
            string strName = "";
            string strFind = "<span class=\"html-attribute-name\">value</span>=\"<span class=\"html-attribute-value\">";
            tbNumMac.Text = "0";
            for (int i = 0; i < HPDataTable.Count; i++)
            {
                j = aPage.Substring(StartMac[i]).IndexOf(strFind);
                if (j < 0) return false;
                n = StartMac[i] + j + strFind.Length;
                if (n > StopMac[i]) // must be empty
                {
                    strName = "";
                    MacBody[i] = 0;
                }
                else
                {
                    k = aPage.Substring(n).IndexOf("</span>");
                    if (k < 0) return false;
                    strName = aPage.Substring(n, k);
                    MacBody[i] = n + k + 1;
                    NumUsed++;
                }
                HPDataTable[i].MoveM = false;
                HPDataTable[i].MacName = strName;
            }
            tbNumMac.Text = NumUsed.ToString();
            return true;
        }



        private bool FindMacros()
        {
            int j, k;
            NumInBody = 0;
            for (int i = 0; i < Utils.HPmaxNumber; i++)
            {
                string strFind = "Macro " + (i + 1).ToString();
                j = aPage.IndexOf(strFind);
                if (j < 0)
                {
                    Utils.HPmaxNumber = i;
                    return false;
                }
                dgvStruct dgv = new dgvStruct();
                dgv.Inx = i + 1;
                HPDataTable.Add(dgv);
                StartMac[i] = j;
                j += strFind.Length;
                k = aPage.Substring(j).IndexOf(strFind);
                if (k < 0)
                {
                    Utils.HPmaxNumber = i;
                    return false;
                }
                StopMac[i] = k + j;
            }
            NumInBody = HPDataTable.Count;
            Utils.HPmaxNumber = NumInBody;
            return true;
        }

        private void ParsePage()
        {
            HPDataTable.Clear();
            FindMacros();
            FindNames();
            FindBody();
        }


        //openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
        private bool ReadMacroHTML()
        {
            int nLength;
            string LastFolder = "";
            ofd.Filter = "HTML Files|*.html;*.htm|All Files|*.*";
            ofd.FilterIndex = 1;
            ofd.ShowDialog();
            string strFileName = ofd.FileName;
            if (!File.Exists(strFileName)) return false;
            LastFolder = Path.GetDirectoryName(ofd.FileName);
            Properties.Settings.Default.LastFolder = LastFolder;
            Properties.Settings.Default.HTTP_HP = strFileName;
            Properties.Settings.Default.Save();
            aPage = File.ReadAllText(strFileName);
            this.Text = " HP Macro Editor: " + strFileName;
            nLength = aPage.Length;
            ParsePage();
            return true;
        }

        private bool ReadLastHTTP()
        {
            string strFilename = Properties.Settings.Default.HTTP_HP;
            if (strFilename == null) return false;
            if (strFilename == "") return false;
            if (!File.Exists(strFilename))
            {
                MessageBox.Show("file " + strFilename + " cannot be found");
                return false;
            }
            aPage = File.ReadAllText(strFilename);
            if (aPage == null) return false;
            if (aPage.Length == 0) return false;
            ParsePage();
            return true;
        }


        private void RunBrowser(bool bMustFetch)
        {
            string strTemp = tbBody.Text.Trim();
            if (strTemp == "" || !tbBody.Enabled) return;
            strTemp = strTemp.Replace(Environment.NewLine, "<br>");
            string sMacroName = tbMacName.Text;
            xMacroViews.AddView(strType, sMacroName);

            if (DataTable[CurrentRowSelected].rBody.Length <= 4)
            {
                if (cbShowLang.Checked)
                {
                    strTemp = Utils.AddLanguageOption(strTemp);
                }
                Utils.CopyHTML(Utils.ShowRawBrowser(strTemp, strType));
            }
            else
            {
                if (HasNewDataRecord != "")
                    Utils.CopyHTML(Utils.ShowRawBrowser(HasNewFormattedData + strTemp, strType));
                else
                    Utils.CopyHTML(Utils.ShowRawBrowser(DataFileFormatted + strTemp, strType));
            }
        }


        private void btnGo_Click(object sender, EventArgs e)
        {
            RunBrowser(false);
        }


        private void EnableLoadTSMitem(string sPrefix, bool b)
        {
            foreach (ToolStripItem item in fileToolStripMenuItem.DropDownItems)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    if (sPrefix == FindPrefixFromText(item.Text))
                    {
                        item.Enabled = b;
                        return;
                    }
                }
            }
        }

        private string FindPrefixFromText(string sText)
        {
            foreach (string s in Utils.LocalMacroPrefix)
            {
                string t = " " + s + " ";
                if (sText.Contains(t)) return s;
            }
            return "";
        }



        private void LoadTSMmenu(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                string s = FindPrefixFromText(item.Text);
                if (s == "")
                {
                    Debug.Assert(false);
                }
                SelectFileItem(s);
            }
        }

        /*
         * kb notebook forum (no search)
         * https://h30434.www3.hp.com/t5/Notebooks-Knowledge-Base/tkb-p/notebooks-knowledge-base
         */
        private void FormQueryKB(string sS)
        {
            if (!bTextFromClipboardMNUs)
            {
                FormGoToKB(sS);
                return;
            }
            string s = sS.Substring(0, 1).ToLower();
            string w = "https://h30434.www3.hp.com/t5/forums/searchpage/tab/message";
            string f = w + "?filter=location&q=" + TextFromClipboardMNUs;
            string p = f + "&location=tkb-board:";
            string[] KBl = { "printers-knowledge-base", "desktop-knowledge-base",
                    "notebooks-knowledge-base","gaming-knowledge-base" };
            string sQ = "";
            switch (s)
            {
                case "p": sQ = p + KBl[0]; break;
                case "d": sQ = p + KBl[1]; break;
                case "n": sQ = p + KBl[2]; break;
                case "g": sQ = p + KBl[3]; break;
                case "a":
                    sQ = w + "?filter=includeTkbs&include_tkbs=true&q=" + TextFromClipboardMNUs;
                    break;
            }
            if (sQ != "")
                Utils.LocalBrowser(sQ);
        }

        private void mnuKnow(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                FormQueryKB(menuItem.Text);
            }
        }
        
        // the - sign does not have to be url encoded
        //https://www.google.com/search?q=OMEN+by+HP+880-181nf+Desktop+PC+Product+Specifications
        private void mnuOmen_Click(object sender, EventArgs e)
        {
            string sModel = Utils.ClipboardGetText().Trim();
            string s = "https://www.google.com/search?q=OMEN+by+HP+" + sModel  + "+Desktop+PC+Product+Specifications";
            Utils.LocalBrowser(s);
        }

        private void mnuAIOSpec_Click(object sender, EventArgs e)
        {
            string sModel = Utils.ClipboardGetText().Trim();
            string s = "https://www.google.com/search?q=HP+All-in-One+-+" + sModel + "+Product+Specifications";
            Utils.LocalBrowser(s);
        }

        private void mbyDesktop_Click(object sender, EventArgs e)
        {
            string sModel = Utils.ClipboardGetText().Trim();
            string s = "https://www.google.com/search?q=HP+%22" + sModel + "%22+Desktop+PC";
            Utils.LocalBrowser(s);
        }

        private void hPYouTubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sObj = Utils.ClipboardGetText().Replace(" ", "%20");
            string s = "https://www.youtube.com/@HPSupport/search?query=" + sObj;
            Utils.LocalBrowser(s);
        }

        private void mnuLapKey_Click(object sender, EventArgs e)
        {
            string sObj = "https://laptopkey.com/search.php?search_query=" + Utils.ClipboardGetText().Trim();
            Utils.LocalBrowser(sObj);
        }

        // this was to look at the clipboard and see if there were keywords
        private string ClipExtractSearchID()
        {
            string str = TextFromClipboardMNUs;
            int n = str.Length;
            if (n > 20) return "";  // was not a keyword probably a bunch of junk or url
            string str0 = "";
            char res;

            //remove any parens anywhere
            str0 = str.Replace("(", "").Replace(")", "");
            while (str0 != str)
            {
                str = str0;
                str0 = str.Replace("(", "").Replace(")", "");
            }

            //remove trailing periods or commas
            n = str.Length - 1;
            res = str[n];
            if (res == '.' || res == ',') str0 = str.Substring(0, n);

            while (str0 != str)
            {
                str = str0;
                n = str.Length - 1;
                res = str[n];
                if (res == '.' || res == ',') str0 = str.Substring(0, n);
            }
            str = str.Replace("HP", "");
            str = str.Replace("hp", "");
            return str;
        }

        private string GetSearchKeyword(string s)
        {
            if (!bTextFromClipboardMNUs) return "";
            string sObj = ClipExtractSearchID();
            if (s == "" || sObj == "") return "";
            return s + " " + sObj;
        }

        private void mnuDrvGoog_Click(object sender, EventArgs e)
        {
            string sObj = GetSearchKeyword("support.hp.com us-en drivers model series");
            if (sObj == "") return;
            sObj = sObj.Replace(" ", "+");
            Utils.LocalBrowser("https://google.com/search?q=" + sObj);
        }

        private void mnuDevCol_Click(object sender, EventArgs e)
        {
            string sObj = GetSearchKeyword("HP");
            if (sObj == "") return;
            Utils.LocalBrowser("https://us.driverscollection.com/Search/" + sObj.Replace(" ", "%20"));
        }

        //https://h30434.www3.hp.com/t5/forums/searchpage/tab/message?advanced=false&allow_punctuation=false&q=ipmmb-fm
        private void mnuSearchComm_Click(object sender, EventArgs e)
        {
            string s = "https://h30434.www3.hp.com/t5/forums/searchpage/tab/message?advanced=false&allow_punctuation=false&q=";
            if (!bTextFromClipboardMNUs)
                Utils.LocalBrowser("https://h30434.www3.hp.com");
            else
            {
                s += TextFromClipboardMNUs.Replace(" ", "%20");
            }
            Utils.LocalBrowser(s);
        }

        //HID\VID_187C&PID_0526\7&167D68C8&1&0000
        private string sExtract(string s, string sPat)
        {
            int e = 4;  // length of hex string
            int n = s.IndexOf(sPat);
            int m = sPat.Length;
            if (n < 0) return "";
            n += m;
            return s.Substring(n, e);
        }

        // "USB\\VID_2EF4&PID_5842&MI_00\\8&1AD5FA5&0&0000";
        // "PCI\VEN_1B21&DEV_2142&SUBSYS_87561043&REV_00\4&299AAA38&0&00E4"

        // devicehunt.com/search/type/usb/vendor/2EF4/device/5842
        private void mnuHuntDev_Click(object sender, EventArgs e)
        {
            string s = RunDeviceHunt();
            if(s == "")            
                s = "https://devicehunt.com";            
            Utils.LocalBrowser(s);
        }

        private string RunDeviceHunt()
        {
            string s = Utils.ClipboardGetText().ToUpper();
            string sType = "";
            if (s.Contains("USB")) sType = "/USB";
            if (s.Contains("PCI")) sType = "/PCI";
            //if (s.Contains("HID")) sType = "/HID";
            if (sType == "") return "";
            string sVid = sExtract(s, "\\VID_");
            if (sVid == "") sVid = sExtract(s, "\\VEN_");
            if (sVid == "") return "";
            string sPid = sExtract(s, "&PID_");
            if (sPid == "") sPid = sExtract(s, "&DEV_");
            if (sPid == "") return "";
            s = "https://devicehunt.com/view/type/" + sType + "/vendor/" + sVid + "/device/" + sPid;
            return s;
        }

        private void mnRecDis_Click(object sender, EventArgs e)
        {
            Utils.LocalBrowser("https://h30434.www3.hp.com/t5/custom/page/page-id/RecentDiscussions");
        }
        private void HaveSelected(bool bVal)
        {
            btnSaveM.Enabled = bVal;
            btnDelM.Enabled = bVal;
        }

        private void MakeSticky(string s)
        {
            bool b = true;
            if (Properties.Settings.Default.AllowSTICKYedits == false)
            {
                switch (s)
                {
                    case "RF":
                        b = (CurrentRowSelected >= Utils.RequiredMacrosRF.Length);
                        break;
                }
            }
            btnDelM.Enabled = b;
            btnSaveM.Enabled = b;
            //NumCheckedMacros = CountChecks(); // cannot do this from inside loading the file
            btnDelChecked.Enabled = NumCheckedMacros > 0 && b;
        }

        private void CheckForLanguageOption(bool bRowChanged)
        {
            cbShowLang.Visible = tbBody.Text.Contains(Utils.sPossibleLanguageOption[0]);
            cbShowLang.Checked = !bRowChanged;
        }

        private void AllowTBbody(bool bAccess)
        {
            tbBody.Enabled = bAccess;
            tbBody.ReadOnly = !bAccess;
            if (!bAccess)
            {
                tbNumMac.Text = "";
                tbMNum.Text = "";
            }
        }

        private void AllowTBbody()
        {
            AllowTBbody(lbName.Rows.Count > 0);
        }


        private bool AnyHyper(string sLC)
        {
            if (sLC.Contains("href=")) return true;
            if (sLC.Contains("<img ")) return true;
            if (sLC.Contains(Utils.NewPrnComment.ToLower())) return true;
            return false;
        }

        // show macro here
        private void ShowUneditedRow(int e)
        {
            bool bChanged = (CurrentRowSelected != e);
            CurrentRowSelected = e;
            lbNoDirect.Visible = false;
            if (lbName.Rows.Count == 0 || e >= lbName.Rows.Count)
            {
                tbBody.Text = "Please create a new macro by clicking 'NEW'";
                tbMacName.Text = "";
                AllowTBbody(false);
                return;
            }
            if (strType != LastViewedFN)
            {
                NumCheckedMacros = 0;
                LastViewedFN = strType;
            }

            tbMNum.Text = (1 + e).ToString();
            if (strType == "RF")
            {
                MakeSticky(strType);
            }
            else HaveSelected(true);
            ShowBodyFromSelected();
            tbMacName.Text = lbName.Rows[CurrentRowSelected].Cells[3].Value.ToString();
            EnableNewTestPrinter();
            lbName.ClearSelection();
            lbName.Rows[CurrentRowSelected].Selected = true;
            CheckForLanguageOption(bChanged);
            btnEditNew.Enabled = (tbBody.Text.IndexOf("TimeStamp") >= 0);
            btnShowURLs.Enabled = AnyHyper(tbBody.Text.ToLower());
            MustFinishEdit(true);
        }

        private void FocusSpeller(int n, int l)
        {
            int i = BadSpell[l].Length;
            //tbBody.Select(n, i);
            bFocusLost = true;
            //tbBody.Focus();
            Utils.ScrollToCaretPosition(tbBody, n, i);
        }

        private string rBodyFromTable()
        {
            string s = DataTable[CurrentRowSelected].rBody;
            if (s == "<nl>") return "";
            return DataTable[CurrentRowSelected].rBody.Replace("<nl>", Environment.NewLine);
        }


        private void ShowBodyFromSelected()
        {
            if (DataTable.Count == 0 || CurrentRowSelected >= DataTable.Count) return;
            if (DataTable[CurrentRowSelected].sBody == null)
            {
                DataTable[CurrentRowSelected].sBody = ""; // have named macro but not body so create empty one
                tbBody.Text = "";
                DataFileRecord = "";
            }
            else
            {
                tbBody.Text = DataTable[CurrentRowSelected].sBody.Replace("<br>", Environment.NewLine);
                DataFileRecord = rBodyFromTable();
                DataFileFormatted = "";
                if (DataFileRecord != "" && strType != "" && Utils.sPrinterTypes.Contains(strType + " "))
                {
                    bool bRtn = printerDB.FormatRecord(DataFileRecord, ref DataFileFormatted);
                    if (!bRtn)
                    {
                        MessageBox.Show("ERROR: failed to parse record ", "Critical");
                        return;
                    }
                    bRtn = Utils.HasWiFiDirect(ref DataFileFormatted);
                    lbNoDirect.Visible = !bRtn;
                }
            }

            tbBodyChecksumN = xMacroChanges.CalculateChecksum(tbBody.Text);
            tbBodyChecksumB = true;
            lbName.Rows[CurrentRowSelected].Selected = true;
            btnChangeUrls.Enabled = !Utils.IsPostableImage(tbBody.Text);
        }

        private bool ShowSelectedRow(int e)
        {
            bool bIgnore = false;
            string sMacName = "";
            if(lbName.Rows.Count > CurrentRowSelected)
                sMacName = lbName.Rows[CurrentRowSelected].Cells[3].Value.ToString();
            if(sMacName == Utils.UnNamedMacro)
            {
                ShowUneditedRow(e);
                MustFinishEdit(false);
                return false;
            }
            if (bPageSaved(ref bIgnore))
            {
                ShowUneditedRow(e);
            }
            else lbName.Rows[CurrentRowSelected].Selected = true;
            return true;
        }

        private void lbName_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if(e.ColumnIndex == 2)
            {
                if (lbName.Rows[e.RowIndex].Cells[2].Value != null)
                {
                    string sTemp = tbBody.Text;
                    string sClip = Clipboard.GetText();
                    tbShowClip.Text = sClip;
                    if (sClip.Length < 32)
                    {
                        sTemp = sTemp.Replace("@clipboard@", sClip);
                        sTemp = sTemp.Replace(Environment.NewLine, "<br>");
                        Utils.ShowRawBrowser(sTemp, strType);
                    }
                    else tbShowClip.Text += Environment.NewLine + "String over 32 long!";
                    return;
                }
            }

            RunBrowser(false);
        }



        private void btnClearEM_Click(object sender, EventArgs e)
        {
            tbBody.Clear();
        }

        private void CopyBodyToClipboard()
        {
            if (tbBody.Text == "") return;
            Clipboard.SetText(tbBody.Text.Replace(Environment.NewLine, "<br>"));
        }

        private void btnCopyTo_Click(object sender, EventArgs e)
        {
            CopyBodyToClipboard();
        }



        private void btnCopyFrom_Click(object sender, EventArgs e)
        {
            string s = Utils.GetHPclipboard();
            string t = Properties.Settings.Default.DoJust ? Utils.JustifiedText(s) : s;
            TbodyInsert(t);
        }


        private void PutOnNotepad(string strIn)
        {
            CSendNotepad SendNotepad = new CSendNotepad();
            string npTitle = strIn;
            SendNotepad.PasteToNotepad(strIn);
        }

        private void btnToNotepad_Click(object sender, EventArgs e)
        {
            string s = tbMacName.Text + Environment.NewLine;
            PutOnNotepad(s + tbBody.Text);
        }

        // the below is never set true?: TODO to do todo
        private void AccessDiffBoth(bool b)
        {
            bHaveBothHP = b;
        }



        // can move macros from all files except the HTML one
        private void AllowMacroMove(bool b)
        {
            mMoveMacro.Visible = b;
            lbName.ReadOnly = !b; // not sure if needed as it is empty ??
        }

        private void AllowMacroMove()
        {
            AllowMacroMove(DataTable.Count > 0);
        }

        private void LoadHTMLfile()
        {
            AccessDiffBoth(false);
            AllowMacroMove(false);
            lbRCcopy.Visible = false;
            bHaveHTMLasLOCAL = ReadMacroHTML();
            if (bHaveHTMLasLOCAL)
            {
                EnableMacEdits(false);
                strType = "";
                bHaveHTML = true;
                saveToXMLToolStripMenuItem.Enabled = true;
                ShowUneditedRow(0);
                AllowChanges(false);
                lbName.Columns[2].HeaderText = "Name HTML";
            }
            else AllowChanges(true);
            if (bHaveHTML)
            {
                if (bHaveHP) return;
                SaveHTTPasTXT("HP");
                LoadFromTXT("HP");
                ShowUneditedRow(0);
                AllowMacroMove(true);
            }
        }

        private void AllowChanges(bool f)
        {
            btnNew.Enabled = f;
            tbMacName.Enabled = f;
            //gpMainEdit.Enabled = f;
            gbSupp.Enabled = f;
        }

        private void readHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadHTMLfile();
        }

        private void TryDif(ref string str1, ref string str2)
        {
            int k = 0;
            for (int i = 0; i < str1.Length; i++)
            {
                if (str1[i] != str2[i])
                {
                    k++;
                }
            }
        }


        // load files here
        // HP and HTML can have blank macro names and body but NOT any others
        private int LoadFromTXT(string strFN)
        {
            int i = 0;
            string rBody;
            bool bNoEmpty = !(strFN == "HP" || strFN == "" || strFN == "HTML");
            string sErr = "";
            bMacroErrors = false;
            bool bMacroDiff = false;
            mShowErr.Visible = false;
            TXTName = strFN;
            strType = strFN;     //TODO todo to do this needs to be cleaned up
            gpMainEdit.Enabled = true;
            gbSupp.Enabled = true;
            string TXTmacName = Utils.FNtoPath(strFN);
            this.Text = " HP Macro Editor: " + TXTmacName;
            NumInBody = 0;
            DataTable.Clear();
            lbName.DataSource = null;
            lbName.Invalidate();
            if (File.Exists(TXTmacName))
            {
                StreamReader sr = new StreamReader(TXTmacName);
                string line = sr.ReadLine();
                string sBody, s;
                bHaveHTML = false;
                bool bAnyHPdiff = (strFN == "HP") && bHaveHTMLasLOCAL;
                lbName.RowEnter -= lbName_RowEnter;
                while (line != null)
                {
                    dgvStruct dgv = new dgvStruct();
                    dgv.Inx = i + 1;
                    dgv.MacName = line;
                    dgv.MoveM = false;
                    sBody = sr.ReadLine();
                    if (sBody == null) sBody = "";
                    sBody = sBody.Replace("&quot;", "'"); // jys 8/20/2024
                    dgv.sBody = sBody;
                    rBody = sr.ReadLine();
                    if (rBody == null) rBody = "";
                    if (rBody == "") rBody = "<nl>";
                    dgv.rBody = rBody;
                    sErr = "";
                    int j = sBody.ToLower().IndexOf("http:");
                    if (j >= 0)
                    {
                        sErr += " http: found(" + (j + 1).ToString() + ") ";
                    }
                    sErr += Utils.BBCparse(sBody);
                    dgv.HPerr = (sErr != "");
                    dgv.HPimage = Utils.bHasImgMarkup(sBody) || Utils.bHasImgMarkup(rBody);
                    dgv.sErr = sErr;
                    if (bAnyHPdiff)
                    {
                        if (HPDataTable[i].sBody == null) s = "";
                        else s = HPDataTable[i].sBody;
                        dgv.HP_HTML_DIF_LOC = Utils.FirstDifferenceIndex(sBody, s);
                        dgv.HP_HTML_NO_DIFF = (dgv.HP_HTML_DIF_LOC == -1);
                        if (!dgv.HP_HTML_NO_DIFF)
                            bMacroDiff = true;
                        //dgv.HPerr |= dgv.HP_HTML_NO_DIFF;
                    }
                    else dgv.HP_HTML_NO_DIFF = true;
                    if (dgv.HPerr)
                        bMacroErrors = true;
                    DataTable.Add(dgv);
                    i++;
                    NumInBody++;
                    line = sr.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    if (line == "")
                    {
                        // file  HP can have an empty macro unlike any others empty
                        // using Macro x now
                        line = Utils.GetDefaultMacName(i);
                    }
                    if (line == "" & bNoEmpty)
                    {
                        if (bInitialLoad)
                        {
                            strBadEnding.Add(strFN);
                        }
                        break;  // if stop here then file has a trailing newline !!!
                    }
                }
                NumInBody = DataTable.Count;
                MyBindingSource.DataSource = DataTable;
                lbName.DataSource = MyBindingSource;
                MyBindingSource.ResetBindings(false);
                CreateLB(strFN);
                lbName.Columns[2].ReadOnly = true;
                lbName.RowEnter += lbName_RowEnter;
                sr.Close();
                if (DataTable.Count > 0 && strFN == "HP")
                    bHaveBothHP = bHaveHTMLasLOCAL;
            }
            else CreateLB(strFN);
            tbNumMac.Text = i.ToString();
            for (i = 0; i < lbName.Rows.Count; i++)
            {
                if (DataTable[i].HPerr)
                {
                    bMacroErrors = true;
                    SetErrorRed(i);
                }
                if (DataTable[i].rBody.Length > 4)
                {
                    lbName.Rows[i].Cells[3].Style.ForeColor = Color.Blue;
                }
                    
            }
            btnNew.Enabled = lbName.RowCount < Utils.NumMacros;
            if (strFN == "HP")
            {
                btnNew.Enabled = false;
                for (i = 0; i < lbName.Rows.Count; i++)
                {
                    if (!DataTable[i].HP_HTML_NO_DIFF)
                    {
                        SetErrorRed(i);
                    }
                }
            }
            mShowErr.Visible = bMacroErrors;
            lbRCcopy.Visible = bMacroDiff;
            AllowTBbody(i > 0);
#if SPECIAL4
            SaveAsTXT(TXTName);
#endif
            EnableClipProcessing();
            EnableLoadTSMitem(strFN, i > 0);
            return i;
        }

        // if a checkbox check then the clipboard is looked at to obtain argument for site to handle
        private void EnableClipProcessing()
        {
            bool b = (strType == "RF");
            lbName.Columns[2].Visible = b;
            if(b)
            {
               foreach(cQCmacros m in AssociateMacros)
                {
                    lbName.Rows[m.LocInRF].Cells[2].Value = m.sType;
                }
            }
        }

        private void CreateLB(string s)
        {
            lbName.Columns.Clear();

            lbName.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Inx",
                HeaderText = "N",
                ValueType = typeof(int),
                Width = 36
            });
            lbName.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "MoveM",
                HeaderText = "Move",
                Width = 50
            });

            lbName.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Clip",
                HeaderText = "Clip ",
                Width = 36,
                Visible = false
            });


            lbName.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MacName",
                HeaderText = "Name: " + Utils.FNtoHeader(s),
                Width = 280
            });

            lbName.Columns[2].DefaultCellStyle.Font = new Font("Arial", 14);
            lbName.Columns[2].DefaultCellStyle.ForeColor = Color.Red;
        }

        private void SaveAsTXT(string strFN)
        {
            int i = 0;
            string strOut = "";
            TXTName = strFN;
            strType = strFN;    // TODO need to clean this up todo to do
            string TXTmacName = Utils.FNtoPath(strFN);
            foreach (dgvStruct row in DataTable)
            {
                string strName = row.MacName;
                string strBody = row.sBody;
                string rBody = row.rBody;
                if (rBody == "")
                    rBody = "<nl>";
                strOut += strName + Environment.NewLine + strBody + Environment.NewLine + rBody + Environment.NewLine;
                i++;
            }
            if (i > 0) Utils.WriteAllText(TXTmacName, strOut);
            else File.Delete(TXTmacName);
            NumInBody = i;
        }

        private void SaveHTTPasTXT(string strFN)
        {
            int i = 0;
            string strOut = "";
            TXTName = strFN;
            strType = strFN;    // TODO need to clean this up todo to do
            string TXTmacName = Utils.FNtoPath(strFN);
            foreach (dgvStruct row in HPDataTable)
            {
                string strName = row.MacName;
                string strBody = CorrectSpell(row.sBody);
                string rBody =  "<nl>";
                strOut += strName + Environment.NewLine + strBody + Environment.NewLine + rBody + Environment.NewLine;
                i++;
            }
            if (i > 0) Utils.WriteAllText(TXTmacName, strOut);
            else File.Delete(TXTmacName);
            NumInBody = i;
        }

        private void saveToXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!bHaveHTML) return;
            DialogResult Res1 = MessageBox.Show("This will overwrite HPmacros",
                "Possible loss of macros", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (Res1 == DialogResult.Yes)
            {
                SaveHTTPasTXT("HP");
            }
        }

        private string SaveDeletedMacro(int j)
        {
            string DeletedName = "";
            string sBody = DataTable[j].sBody;
            string sMacName = DataTable[j].MacName;
            string sType = strType;
            DeletedName = printerDB.GetDeletedName(strType, sMacName);
            Utils.WriteAllText(DeletedName + ".txt", sBody);
            return DeletedName;
        }


        // this removes the macro that is being displayed
        private int RemoveMacro()
        {
            int j = CurrentRowSelected;
            //string sDelName = SaveDeletedMacro(j);
            lbName.RowEnter -= lbName_RowEnter;
            MyBindingSource.Remove(MyBindingSource.Current);
            MyBindingSource.ResetBindings(false);
            lbName.RowEnter += lbName_RowEnter;
            if (j == lbName.RowCount)
                j--;
            if (j < 0) j = 0;
            return j;
        }

        private void EnableMacEdits(bool enable)
        {
            btnDelM.Enabled = enable && CurrentRowSelected >= 0;
            btnSaveM.Enabled = enable && CurrentRowSelected >= 0;
        }


        private void btnDelM_Click(object sender, EventArgs e)
        {
            
            ClearCanceledDataFile();
            string strName = tbMacName.Text;
            int i = CurrentRowSelected + 1;
            string strN = "";
            if (strType != "HP")
            {
                strN = (strName == "") ? "You are deleting an unnamed macro" :
                    "You are deleting the macro named " + strName;
                DialogResult Res1 = MessageBox.Show(strN, "Deleting  macro " + i.ToString(),
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (Res1 != DialogResult.Yes)
                {
                    return;
                }
                CurrentRowSelected = RemoveMacro();
            }
            else
            {
                strN = (strName == "") ? "You are removing the contents of an unnamed macro" :
                    "You are removing contents of the macro named " + strName;
                DialogResult Res1 = MessageBox.Show(strN, "Deleting  macro " + i.ToString(),
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (Res1 != DialogResult.Yes)
                {
                    return;
                }
                DataTable[CurrentRowSelected].sBody = "";
                DataTable[CurrentRowSelected].MacName = "Macro " + i.ToString();
                tbBody.Text = "";
            }
            xMacroChanges.TryRemove(strType, tbMacName.Text);
            MustFinishEdit(true);
            ReSaveAsTXT(TXTName);
            ShowUneditedRow(CurrentRowSelected);
        }

        private void ReSaveAsTXT(string TXTName)
        {
            SaveAsTXT(TXTName);
            LoadFromTXT(TXTName);
        }

        private int FailsHTMLparse()
        {
            if (HasBadUrl()) return 1;
            if (strType == "NO" || strType == "RF") return 0;
            return Utils.SyntaxTest(tbBody.Text);
        }

        private bool NoEmptyMacros()
        {
            int i = 0;
            tbMacName.Text = tbMacName.Text.Trim();
            if (tbMacName.Text == "")
            {
                tbMacName.Text = Utils.UnNamedMacro;
                i++;
            }
            Debug.Assert(tbBody.Text != null, "Edit body should not be null");
            tbBody.Text = tbBody.Text.Trim();
            if (tbBody.Text == "")
            {
                tbBody.Text = Utils.UnNamedMacro;
                i++;
            }
            return i == 2;  // both items blank
        }


        // if UpdateSelected then the macro name and body changed
        // return code 0:ok to save; 1:cannot save;  3: cannot save as do not want to overwrite
        // code of 2: html error needs to be corrected but could be ignored
        // 
        private int SaveCurrentMacros(bool UpdateSelected)
        {
            bool bChanged = false;
            NoEmptyMacros();
            string strName = tbMacName.Text;
            string strOld = "";
            if (lbName.RowCount == 0) return 1; // must have wanted to add a row: sorry, cannot do this

            int r = FailsHTMLparse();
            if (r > 0)
            {
                switch (r)
                {
                    case 1:
                        return 2;
                    case 2:
                        break;
                }
            }
            if (UpdateSelected)
            {
                strOld = lbName.Rows[CurrentRowSelected].Cells[3].Value.ToString();
                if (strName != strOld && (Utils.UnNamedMacro != strOld) || HasNewDataRecord != "" || bDataFileUnsaved)                   
                {
                    DialogResult Res1 = MessageBox.Show("This will overwrite " + strOld + " with " + strName,
            "Replaceing a macro", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (Res1 != DialogResult.Yes)
                    {
                        return 3;
                    }
                }

                lbName.Rows[CurrentRowSelected].Cells[3].Value = strName;
                DataTable[CurrentRowSelected].sBody =
                              RemoveNewLine(ref bChanged, Utils.NoTrailingNL(tbBody.Text).Trim());
                if(bDataFileUnsaved)
                {
                    DataTable[CurrentRowSelected].rBody = DataFileRecord.Replace(Environment.NewLine,"<nl>");
                    DataFileRecord = "";
                    bDataFileUnsaved = false;
                }
                
                if (tbBodyChecksumB)
                    xMacroChanges.isMacroChanged(tbBodyChecksumN, TXTName, strName, DataTable[CurrentRowSelected].sBody);
            }

            if (TXTName == "HP")
            {
                SaveAsTXT(TXTName);
                ReloadHP(CurrentRowSelected);
            }
            else
            {
                ReSaveAsTXT(TXTName);
            }
            ShowUneditedRow(CurrentRowSelected);  // jys 8/20/2024  put back in 21
            return 0;
        }

        private void RemoveHTML(ref string s)
        {
            bool b = true;
            int i, j;
            while (b)
            {
                i = s.IndexOf("<a href");
                b = i >= 0;
                if (b)
                {
                    j = s.IndexOf("</a>", i);
                    j += 4;
                    s = s.Remove(i, j - i);
                }
            }
            b = true;
            while (b)
            {
                i = s.IndexOf("<img ");
                b = i >= 0;
                if (b)
                {
                    j = s.IndexOf(">", i);
                    j++;
                    s = s.Remove(i, j - i);
                }
            }
        }
        private bool RefsOnly()
        {
            if (strType != "RF") return false;
            if (CurrentRowSelected > Utils.RequiredMacrosRF.Length) return false;
            string s = tbBody.Text.ToLower().Replace(Environment.NewLine, "");
            RemoveHTML(ref s);
            s = s.Trim();
            return s != "";
        }

        private void btnSaveM_Click(object sender, EventArgs e)
        {
            
            if (RefsOnly())
            {
                MessageBox.Show("Macros must contain URLs only, no text");
                btnCancelEdits.ForeColor = Color.Red;
                return;
            }
            if (NoEmptyMacros())
            {
                MessageBox.Show("You cannot save an empty macro");
                return;
            }
            int RtnCode = SaveCurrentMacros(true);
            if(Properties.Settings.Default.SpellCheckSave && Utils.bSpellingEnabled)
                MySpellCheck.DoCheck(tbBody.Text);
            MustFinishEdit(true);
            nSavedCount++;
        }

        private string RemoveNewLine(ref bool bChanged, string strIn)
        {
            string strOut = Utils.RemoveNL(strIn);
            bChanged = (strOut.Length != strIn.Length);
            return strOut;
        }



        private bool AddNew(string strNewName, string strBody)
        {
            bool bChanged = false;
            if (lbName.Rows.Count == Utils.NumMacros)
            {
                MessageBox.Show("Can only hold " + Utils.NumMacros.ToString() + " macros");
                return false;
            }
            if (strNewName == "")
            {
                MessageBox.Show("Must have a name for the macro");
                return false;
            }
            for (int i = 0; i < lbName.Rows.Count; i++)
            {
                if (lbName.Rows[i].Cells[3].Value.ToString() == strNewName)
                {
                    MessageBox.Show("Macro " + (i + 1).ToString() + " name must be unique!");
                    return false;
                }
            }
            if (CurrentRowSelected >= 0 && CurrentRowSelected < lbName.Rows.Count)
                lbName.Rows[CurrentRowSelected].Selected = false;
            dgvStruct dgv = new dgvStruct();
            dgv.Inx = lbName.Rows.Count;
            dgv.MacName = strNewName;
            dgv.HPerr = false;
            dgv.sBody = "";
            dgv.rBody = "";
            dgv.MoveM = false;
            dgv.sErr = "";
            // printer used to be defaulted here
            dgv.sBody = RemoveNewLine(ref bChanged, strBody);
            DataTable.Add(dgv);
            MyBindingSource.ResetBindings(false);
            tbBody.Text = strBody.Replace("<br>", Environment.NewLine);
            ReSaveAsTXT(TXTName);
            HaveSelected(true);
            lbName.Invalidate();
            lbName.Refresh();
            CurrentRowSelected = lbName.Rows.Count - 1;
            lbName.Rows[CurrentRowSelected].Selected = true;
            tbMacName.Text = strNewName;
            tbNumMac.Text = lbName.Rows.Count.ToString();
            EnableMacEdits(true);
            return true;
        }



        private int ReloadHP(int r)
        {
            int iCnt = 0;
            lbRCcopy.Visible = false;
            if (!Properties.Settings.Default.IgnoreHTTP)
            {
                if (!bHaveHTMLasLOCAL)
                    bHaveHTMLasLOCAL = ReadLastHTTP();
            }
            ShowUneditedRow(r);
            strType = "HP";
            iCnt = LoadFromTXT(strType);
            ShowUneditedRow(r);
            EnableMacEdits(true);
            return iCnt;
        }


        private void ReplaceText(int iStart, int iLen, string strText)
        {
            string sPrefix = tbBody.Text.Substring(0, iStart);
            string sSuffix = tbBody.Text.Substring(iStart + iLen);
            tbBody.Text = sPrefix + strText + sSuffix;
            Utils.ScrollToCaretPosition(tbBody, iStart , strText.Length);
        }

        private void TbodyInsert(string sClip)
        {
            int i = tbBody.SelectionStart;
            int j = tbBody.SelectionLength;
            ReplaceText(i, j, sClip);
        }

        private void btnSetObj_Click(object sender, EventArgs e)
        {
            string strReturn = "";
            bool bHaveHTML = false;
            bool bHaveImg = false;
            int i = tbBody.SelectionStart;
            int j = tbBody.SelectionLength;
            string sTemp = tbBody.Text;
            string strRaw = Utils.AdjustNoTrim(ref i, ref j, ref sTemp);
            string strLC = strRaw.ToLower();
            bHaveHTML = strLC.Contains("https:") || strLC.Contains("http:");
            bHaveImg = Utils.IsUrlImage(strLC);

            if (bHaveHTML || bHaveImg)
            {
                if (j < 12) return; // http://a.com is smallest
                LinkObject MyLO = new LinkObject(strRaw);
                MyLO.ShowDialog();
                strReturn = MyLO.strResultOut;
                MyLO.Dispose();
                if (strReturn == null) return;
                if (strReturn == "") return;
                ReplaceText(i, j, strReturn);

            }
            else
            {
                SetText MySET = new SetText(strRaw);
                MySET.ShowDialog();
                strReturn = MySET.strResultOut;
                MySET.Dispose();
                if (strReturn == null) return;
                if (strReturn == "") return;
                if (j > 0)
                {
                    ReplaceText(i, j, strReturn);
                }
                else
                {
                    tbBody.Text = tbBody.Text.Insert(i, strReturn);
                }

            }
        }

        private string AppendDash(string s, int n)
        {
            int i = n - s.Length;
            return s + string.Concat(Enumerable.Repeat('-', i));
        }

        private string GetReference()
        {
            string sRtn = "";
            int i = 0;
            foreach (string s in Utils.LocalMacroPrefix)
            {
                if (s == strType)
                {
                    string t = Utils.LocalMacroRefs[i];
                    if (t == "") break;
                    return string.Concat(Enumerable.Repeat(Environment.NewLine, 8)) + t;
                }
                i++;
            }
            return sRtn;
        }

        private bool AddBody(ref string sOut, string sModels)
        {
            string t;
            if (sOut.Contains(sModels))return false;
            int i = sOut.IndexOf(Utils.NewPrnComment);
            if (i < 0) return false;
            int j = sOut.IndexOf("-->", i+Utils.NewPrnComment.Length);

            i = j + 3;
            j = sOut.IndexOf(Utils.ModelsID,i);
            if(j < 0)
            {
                t = sOut.Insert(i, sModels);
                sOut = t;
                return true;
            }
            i = j + Utils.ModelsID.Length;
            int k = sOut.IndexOf("-->", i);
            Debug.Assert(k >= 0);
            k += 3;
            t = sOut.Remove(j, k - j);
            sOut = t.Insert(j,sModels);
            return true;
        }


        // edit wizard for new style macros
        private void btnEditNew_Click(object sender, EventArgs e)
        {

            DataFileRecord = rBodyFromTable();
            if (DataFileRecord != "")
            {
                cPrinter MyPrinter = new cPrinter(ref printerDB, ref MySpellCheck, tbMacName.Text, strType);
                MyPrinter.EditNewRecord(DataFileRecord, tbBody.Text);
                MyPrinter.ShowDialog();
                DataFileRecord = MyPrinter.strRecord;
                DataFileFormatted = MyPrinter.strResults;
                if (DataFileRecord != null)
                {
                    if (DataFileRecord != "")
                    {
                        string sTB = tbBody.Text.Replace(Environment.NewLine,"<br>");
                        bool bMustAdd = AddBody(ref sTB, MyPrinter.strModels);
                        nSavedCount++;
                        tbBodyChecksumB = false;
                        bDataFileUnsaved = true;
                        if (bMustAdd)
                        {
                            tbBody.Text = sTB;
                            SaveCurrentMacros(true);
                            bDataFileUnsaved = false;
                            MustFinishEdit(true);
                        }
                        else
                        {
                            bDataFileUnsaved = true;
                            MustFinishEdit(false);
                        }
                    }
                }
                MyPrinter.Dispose();
                return;
            }
            return;
        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            bool bIgnore = false;
            if (bPageSaved(ref bIgnore))
            {
                if (Utils.IsNewPRN(TXTName))
                {
                    ParseDevice MyLookup = new ParseDevice();
                    MyLookup.Parse(Clipboard.GetText());
                    string sName = MyLookup.GetModel();
                    MyLookup = null;
                    if(sName == "")
                    {
                        sName = Utils.UnNamedMacro;
                    }
                    AddNew(sName, "");
                    cPrinter MyPrinter = new cPrinter(ref printerDB, ref MySpellCheck, sName, strType);
                    MyPrinter.AddNewRecord(sName, strType);
                    MyPrinter.ShowDialog();
                    DataFileRecord = MyPrinter.strRecord;
                    DataFileFormatted = MyPrinter.strResults;
                    if(DataFileRecord != null)
                    {
                        if (DataFileRecord != "")
                        {
                            bDataFileUnsaved = true;
                            tbBody.Text = Utils.FormHeader(sName, strType) + MyPrinter.strModels;
                        }
                    }
                    MyPrinter.Dispose();
                }
                else
                {
                    AddNew(Utils.UnNamedMacro, GetReference());
                }

                nSavedCount++;
                tbBodyChecksumB = false;
            }
            
        }

        private void EnableNewTestPrinter()
        {
            //btnNewPrinter.Enabled = (tbMacName.Text == Utils.UnNamedMacro);
        }

        private void MustFinishEdit(bool bFinished)
        {
            if (bFinished || NumInBody == 0)
            {
                btnCancelEdits.ForeColor = NormalEditColor;
                btnSaveM.ForeColor = NormalEditColor;
                btnDelM.ForeColor = NormalEditColor;
                tbChangeNotifed = false;
            }
            else
            {
                btnCancelEdits.ForeColor = Color.Red;
                btnSaveM.ForeColor = Color.Red;
                btnDelM.ForeColor = Color.Red;
            }
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
            aboutBox.Dispose();
        }

        //need a copy of the edit box contents in full markup
        private string tbBodyMarked()
        {
            //string sBody = tbBody.Text.Trim().Replace(Environment.NewLine, "<br>");
            string sBody = tbBody.Text.Replace(Environment.NewLine, "<br>");
            return sBody.Trim();
        }


        //?? if NumInBody is equal to CurrentRowSelected then the we must have deleted the row??
        // this side effect happened
        private bool bNothingToSave()
        {
            //if (tbChangeNotifed) return false; // this test my make the below obsolete
            // the above generated a must save warning when clicking on save !!!
            if (CurrentRowSelected < 0 || strType == "" || NumInBody == 0 || NumInBody == CurrentRowSelected)
            {
                // if row count is 0 then a new macro file and user should have saved: sorry
                return true; // nothing to save 
            }
            if (DataTable[CurrentRowSelected].sBody == null)
            {
                if (tbBody.Text.Trim().Length > 0) return false;
                return true; // a leftover "Change Me" has empty body
            }
            string s = tbBodyMarked();
            bool bEdited = (s != DataTable[CurrentRowSelected].sBody);
            return !bEdited;
        }

        private bool bPageSaved(ref bool bIgnore)
        {
            string sMsg = "Macro was not saved\r\nEither save, cancel edits or delete";
            bIgnore = false;
            if (tbMacName.Text.Trim() == "" && strType != "HP")
            {
                NoEmptyMacros();
                sMsg = "Un-named macro not saved, using " + Utils.UnNamedMacro;
            }
            if (bNothingToSave()) return true;
            MustFinishEdit(false);
            DialogResult Res1 = MessageBox.Show(sMsg, "Cannot be ignored", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // cannot ignore as this procedure was called by an attempted change to a different macro file
            // and I cannot handle saving during the procedure call. Wish I knew more about these calls as the
            // error message from the internal handler was strange and was always trap when I try to save.
            return false;
        }

        private void btnChangeUrls_Click(object sender, EventArgs e)
        {
            bool bIgnore = false;
            if (bPageSaved(ref bIgnore))
            {
                ManageMacros MyManageMac = new ManageMacros(strType, ref DataTable, ref MySpellCheck);
                MyManageMac.ShowDialog();
                if (MyManageMac.nAnyChanges > 0)
                {
                    SaveCurrentMacros(false);
                    btnChangeUrls.Enabled = !Utils.IsPostableImage(tbBody.Text);
                }
                MyManageMac.Dispose();
            }
        }

        private void btnAppendMac_Click(object sender, EventArgs e)
        {
            int i = tbBody.SelectionStart;
            CreateMacro MyCM = new CreateMacro(strType);
            MyCM.ShowDialog();
            string strReturn = MyCM.strResultOut;
            MyCM.Dispose();
            if (strReturn == null) return;
            if (strReturn == "") return;
            btnChangeUrls.Enabled = true;
            string s1 = tbBody.Text.Substring(0, i) + "<br>";
            string s2 = "<br>" + tbBody.Text.Substring(i);
            tbBody.Text = s1 + strReturn + s2;
        }

        private void downloadMacrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //https://h30434.www3.hp.com/t5/user/myprofilepage/tab/user-macros
            string UserMacs = "https://h30434.www3.hp.com/t5/user/myprofilepage/tab/user-macros";
            Utils.LocalBrowser(UserMacs);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }


        private void helpWithUtilsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp("UTILS");
        }

        private void helpWithWebSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp("WEB");
        }

        private void managingImagesHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp("MANAGE");
        }

        private void helpWithFILEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp("FILE");
        }

        private void helpWithSignaturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp("SIG");
        }

        private void helpWithSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp("SEARCH");
        }

        private void ShowHelp(string sHelp)
        {
            EditHelp(sHelp);
        }

        private void EditHelp(string s)
        {
            if (Utils.bSpellingEnabled)
                MySpellCheck.EditHelpDocs(s);
            else Utils.WordpadEdit(s);
        }

        private void helpWithEditingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp("EDIT");
        }

        private void EDITLINKHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp("EDITLINK");
        }

        private void helpWithErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp("XMLERRORS");
        }


        private void lbName_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (strType != "HP") return;
                int r = e.RowIndex;
                if (r == -1) return;
                if (HPDataTable.Count == 0)
                {
                    MessageBox.Show("Please open your original HP macro file.");
                    return;
                }
                if (!DataTable[r].HP_HTML_NO_DIFF) //(!HP_HTML_NO_DIFF[r])
                {
                    string sWhereErr = "";
                    if (DataTable[r].HP_HTML_DIF_LOC == 0)
                    {
                        sWhereErr = " Diff is at end (or empty)";
                    }
                    else if (DataTable[r].HP_HTML_DIF_LOC > 0)
                    {
                        sWhereErr = " Diff at char " + DataTable[r].HP_HTML_DIF_LOC.ToString();
                    }
                    string s = "Original Macro " + (r + 1).ToString() + ": '" + HPDataTable[r].MacName + "'" + sWhereErr + Environment.NewLine + HPDataTable[r].sBody;
                    PutOnNotepad(s);
                }
            }
        }

        //copy and pasting from a reply can sometimes get a ... instead of the full url
        private bool HasBadUrl()
        {
            if (tbBody.Text.Contains("..."))
            {
                DialogResult Res1 = MessageBox.Show("There is a '...' in the URL.  Click OK to ignore", "Possibly bad URL", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (Res1 == DialogResult.OK)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        private void FullSpellCheck()
        {
            if (!Utils.bSpellingEnabled) return;
            tbBody.Text = tbBody.Text.Replace("<br>", Environment.NewLine);
            BadSpell = MySpellCheck.RunSpellList(tbBody.Text);
            if (BadSpell.Length > 0)
            {
                CreateCandidateList();
                iBadSpellIndex = 0;
                SetNextMissed();
            }
            btnNextChk.Enabled = BadSpell.Length > 0;
        }


        private void lbName_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            ShowSelectedRow(e.RowIndex);
            if(bFocusLost)
            {
                bFocusLost = false;
                return;
            }

            if (cbLaunchPage.Checked)
            {
                FullSpellCheck();
            }
        }

        private List<int> FindWordIndices(string input, string wordToFind)
        {
            List<int> indices = new List<int>();
            int startIndex = 0;

            while ((startIndex = input.IndexOf(wordToFind, startIndex)) != -1)
            {
                indices.Add(startIndex);
                startIndex += wordToFind.Length;
            }

            return indices;
        }

        private void CreateCandidateList()
        {
            SpellCandidates.Clear();
            SpellIndex.Clear();
            string t="";
            int i = 0;
            string s = tbBody.Text;
            foreach(string w in BadSpell)
            {
                if (w == "i")
                    t = "i ";
                else t = w;
                List<int> inx = FindWordIndices(s, t);
                foreach(int j in inx)
                {
                    SpellCandidates.Add(j);
                    SpellIndex.Add(i);
                }
                i++;
            }
        }

        private void ClearCanceledDataFile()
        {
            HasNewDataRecord = "";
            if (bDataFileUnsaved)
            {
                bDataFileUnsaved = false;
                DataFileRecord = "";
                DataFileFormatted = "";
            }
        }

        private void btnCancelEdits_Click(object sender, EventArgs e)
        {
            ClearCanceledDataFile();
            ShowBodyFromSelected();
            SetFGcolor("#FF6600");
            tbColorCode.ForeColor = ColorTranslator.FromHtml(tbColorCode.Text.ToString());
            MustFinishEdit(true);
        }

        private void btnLinkAll_Click(object sender, EventArgs e)
        {
            string sBody = tbBody.Text;
            int i = tbBody.SelectionStart;
            int j = tbBody.SelectionLength;
            string strRaw = Utils.AdjustNoTrim(ref i, ref j, ref sBody);
            bool bHasHyper = sBody.Contains("<a ") || sBody.Contains("<img ");
            if (!bHasHyper) // do not want to unlink urls
            {
                if (j == 0 || strRaw.Trim().Length == 0)
                {
                    sBody = tbBody.Text;
                    Utils.ReplaceUrls(ref sBody, true);
                    tbBody.Text = sBody;
                    return;
                }
            }
            // see if there is a range to link

            if (j < 12) return; // http://a.com is smallest
            sBody = strRaw.ToLower();
            bHasHyper = sBody.Contains("<a ") || sBody.Contains("<img ");
            if (bHasHyper) return;  // do not want to link anything twice
            Utils.ReplaceUrls(ref strRaw, true);
            ReplaceText(i, j, strRaw);
        }

        // This code will run when Ctrl+V is pressed.  if url selected then option to make a hyperlink
        // there is either a url in the clipboard and the users highlighted text or vice-versa
        private void tbBody_KeyDown(object sender, KeyEventArgs e)
        {

            int i, j, k;
            string sBody, sFromCB, sL;
            string sText, sS, sE;
            string sOut = "";
            if (e.Control && e.KeyCode == Keys.V)
            {
                if (Properties.Settings.Default.Vdisable) return;
                string tBody = tbBody.Text;
                sBody = tBody.ToLower();
                i = tbBody.SelectionStart;
                j = tbBody.SelectionLength;
                if (j == 0) return;
                sFromCB = Clipboard.GetText();

                sL = sFromCB.ToLower();
                if (sL.Trim().Length == 0) return;
                k = sL.IndexOf("http");
                if(k != 0) return;

                sText = Utils.AdjustNoTrim(ref i, ref j, ref tBody);
                if (sText.Contains("http") && sFromCB.Contains("http")) return;
                sOut = Utils.FormUrl(sFromCB, sText);
                e.Handled = true;
                e.SuppressKeyPress = true;
                sS = tBody.Substring(0, i);
                sE = tBody.Substring(i + j);
                tbBody.Text = sS + sOut + sE;
                Utils.ScrollToCaretPosition(tbBody, i , sOut.Length);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            PositionNext(-1);
        }

        private void PositionNext(int iDir)
        {
            int n = Utils.LocalMacroPrefix.Length;
            int i = 0;
            if (strType == "")
            {
                SelectFileItem("PC");
                return;
            }
            i = Array.IndexOf(Utils.LocalMacroPrefix, strType);
            i += iDir;
            if (i < 0) i = n - 1;
            if (i == n) i = 0;
            n = SelectFileItem(Utils.LocalMacroPrefix[i]);
            if (n > 0) lbName.Focus();
        }

        // find next file PC->PRN->HP and repeat back to PC
        private void btnNextTable_Click(object sender, EventArgs e)
        {
            PositionNext(1);
        }

        private int WarnMissing(string sFN)
        {
            string sOut = "";
            string TXTmacName = Utils.FNtoPath(sFN);
            DialogResult dr = MessageBox.Show("Reference Macro RFmacros.txt is missing\r\nSelect YES to install Placeholders or NO to quit",
                "WARNING", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No)
            {
                bForceClose = true;
                return -1;
            }
            foreach (string s in Utils.RequiredMacrosRF)
            {
                sOut += s + Environment.NewLine + "Content is missing" + Environment.NewLine + "<nl>" + Environment.NewLine;
            }
            Utils.WriteAllText(TXTmacName, sOut);
            return Utils.RequiredMacrosRF.Length;
        }

        private void ShowEmpty(string sWanted)
        {

            NumInBody = 0;
            DataTable.Clear();
            lbName.DataSource = null;
            lbName.Invalidate();
            tbBody.Text = "";
            tbMacName.Text = "";
            strType = sWanted;
            this.Text = " HP Macro Editor";
            btnSaveM.Enabled = false;
            btnNew.Enabled = true; // allow to be created
            CreateLB(strType);
        }

        private int SelectFileItem(string sPrefix)
        {
            bool bIgnore = false;
            int iCnt = 0;
            if (strType != sPrefix)
            {
                if (!bPageSaved(ref bIgnore))
                {
                    return 0; // user failed to save edits
                }
            }
            strType = sPrefix;
            TXTName = sPrefix;
            if (Utils.NoFileThere(sPrefix))
            {
                ShowEmpty(sPrefix);
                AllowTBbody(false);
                AllowMacroMove(false);
                tbBody.Text = "Please create a new macro by clicking 'NEW'";
                return 0;
            }
            if (strType != LastViewedFN)
            {
                NumCheckedMacros = 0;
                LastViewedFN = strType;
            }
            btnSaveM.Enabled = true;
            lbRCcopy.Visible = false;
            mMoveMacro.Visible = true;
            lbName.ReadOnly = false;

            if (sPrefix == "HP")
            {
                iCnt = ReloadHP(0);
            }
            else
            {
                AccessDiffBoth(false);
                iCnt = LoadFromTXT(strType);
                EnableMacEdits(true);
                ShowUneditedRow(0);
            }
            AllowMacroMove(true);

            return iCnt;
        }

        private void mShowDiff_Click(object sender, EventArgs e)
        {
            if (bHaveBothHP)
            {
                //ShowHighlights();
            }
        }

        private void mShowErr_Click(object sender, EventArgs e)
        {
            ShowErrors MySE = new ShowErrors(ref DataTable);
            MySE.Show();
        }

        private int LoadAllFiles()
        {
            int nMacroCnt = 0;
            bool bNoEmpty;
            sBadMacroName = "";
            if (cBodies == null)
            {
                if(!Properties.Settings.Default.IgnoreHTTP)
                {
                    bHaveHTMLasLOCAL = ReadLastHTTP();
                }
                else bHaveHTMLasLOCAL = false;
                cBodies = new List<CBody>();
                if (!File.Exists(Utils.FNtoPath("RF")))
                {
                    int n = WarnMissing("RF");
                    if (n > 0) return LoadFromTXT("RF");
                    return 0;
                }
                foreach (string strFN in Utils.LocalMacroPrefix)
                {
                    int i = 0;
                    bNoEmpty = !(strFN == "HP" || strFN == "" || strFN == "HTML");
                    string FNpath = Utils.FNtoPath(strFN);

                    if (File.Exists(FNpath))
                    {
                        StreamReader sr = new StreamReader(FNpath);
                        string strMN = sr.ReadLine();
                        string sBody;
                        string rBody;
                        bHaveHTML = false;
                        while (strMN != null)
                        {
                            if (strMN.Contains(Utils.UnNamedMacro))
                            {
                                sBadMacroName +=
                                    "Macro " + (i + 1).ToString() + " in " + strFN + " is un-named\r\n";
                                UnfinishedFN = strFN;
                                UnfinishedMN = strMN;
                                UnfinishedIndex = i;
                            }
                            sBody = sr.ReadLine();
                            rBody = sr.ReadLine();
                            if (rBody == null)
                                rBody = "<nl>";
                            if (rBody == "")
                                rBody = "<nl>";
                            CBody cb = new CBody();
                            cb.File = strFN;
                            cb.Number = (i + 1).ToString();
                            cb.Name = strMN;
                            cb.bHasImages =
                                Utils.bHasImgMarkup(rBody) || Utils.bHasImgMarkup(sBody);                       cb.bHasImages = Utils.bHasImgMarkup(rBody) || Utils.IsUrlImage(sBody);
#if SPECIAL2
                        bDebug |= RunBorderFix(strFN, i+1, cb.Name, ref sBody);
#endif
#if SPECIAL3
                        bDebug |= RunLookMissingTR(strFN, i + 1, cb.Name, ref sBody);
#endif

                            cb.sBody = (sBody == null) ? "" : sBody;
                            cb.rBody = rBody;
                            cb.fKeys = "";
                            cBodies.Add(cb);                            
                            sBody = Utils.BBCparse(sBody);
                            sBody = sBody.Replace("&quot;", "'"); // jys 8/20/2024
                            i++;
                            nMacroCnt++;
                            strMN = sr.ReadLine();
                            if (strMN == null)
                            {
                                break;
                            }
                            if (strMN == "")
                            {
                                // file  HP can have an empty macro unlike any others empty
                            }
                            if (strMN == "" & bNoEmpty)
                            {
                                strBadEnding.Add(strFN);
                                break;  // if stop here then file has a trailing newline !!!
                            }
                        }
                        sr.Close();
                        if (strFN == "HP")
                        {
                            if(i > 0)
                            {
                                bHaveHP = true;
                            }
                            else bHaveHP = false;
                            mnuCmpHpTr.Enabled |= bHaveHP;
                        }
                        if(strFN == "TR")
                        {
                            if (i >= Utils.HPminNumber)
                            {
                                mnuCmpHpTr.Enabled = true;
                            }
                            else mnuCmpHpTr.Enabled = false;
                        }
                    }
                    EnableLoadTSMitem(strFN, i > 0);
                }
            }

            bInitialLoad = false;
            if (sBadMacroName != "")
            {
                MessageBox.Show(sBadMacroName, "Bad macro names");
            }
            if (strBadEnding.Count > 0)
            {
                string strNames = "";
                foreach (string s in strBadEnding)
                {
                    strNames += (s + " ");
                }
                MessageBox.Show("One or more files have trailing newline and will be re-written\r\nIf this happens repeatedly please create an issue",
                    strNames, MessageBoxButtons.OK);
                ReWriteBadFiles();
            }
            if (strType != "") lbName.ReadOnly = false;

            if (UnfinishedFN == "")
            {
                if (LoadStartupMacro())
                {
                    AllowTBbody(lbName.Rows.Count > 0);
                }
                else
                {
                    LoadFromTXT("HP");
                    ShowUneditedRow(0);
                }
            }
            else
            {
                LoadFromTXT(UnfinishedFN);
                ShowUneditedRow(UnfinishedIndex);
            }
            AllowTBbody();
            return nMacroCnt;
        }

        private bool LoadStartupMacro()
        {
            string s = Properties.Settings.Default.StartupReturn;
            bool b = Properties.Settings.Default.bStartupReturn;
            if (b)
            {
                int n = s.IndexOf(":");
                int m = 0;
                int.TryParse(s.Substring(n + 1), out m);
                LoadFromTXT(s.Substring(0, n));
                lbName.RowEnter -= lbName_RowEnter;
                if (m >= lbName.Rows.Count) m = 0;
                ShowUneditedRow(m);
                lbName.RowEnter += lbName_RowEnter;
            }
            return b;
        }

        private void ReWriteBadFiles()
        {
            foreach (string strFN in strBadEnding)
            {
                string strOut = "";
                bool bFound = false;
                foreach (CBody cb in cBodies)
                {
                    if (cb.File == strFN)
                    {
                        if (bFound) strOut += Environment.NewLine;
                        strOut += cb.Name + Environment.NewLine;
                        strOut += cb.sBody;
                        bFound = true;
                    }
                }
                if (strOut != "") // probably should assert this
                {
                    File.WriteAllText(Utils.FNtoPath(strFN), strOut);
                }
            }
        }

        private void RaiseSearch()
        {
            string sFN = strType;
            bool bFinishedEdits = bNothingToSave();
            WordSearch ws = new WordSearch(ref cBodies, bFinishedEdits, ref xMacroViews, ref MySpellCheck, ref printerDB);
            ws.ShowDialog();
            int i, n = ws.LastViewed;
            string NewID = ws.NewItemID;
            string NewName = ws.NewItemName;
            ws.Dispose();
            LastViewedFN = "";  // also sets checked macro count to 0 
            LastViewedFN = "";  // also sets checked macro count to 0 
            if (n >= 0 && bFinishedEdits)
            {
                CBody cb = cBodies[n];
                LoadFromTXT(cb.File);
                sFN = cb.File;
                i = Convert.ToInt32(cb.Number);
                ShowUneditedRow(i - 1);
            }
            if (NewID != "" && bFinishedEdits)
            {
                n = LoadFromTXT(NewID);
                sFN = NewID;
                if (n < Utils.NumMacros)
                {
                    AddNew(NewName, GetReference());
                }
            }
            if (strType != "HP")
            {
                AccessDiffBoth(false);
                EnableMacEdits(true);
            }
        }

        private int CountChecks()
        {
            int n = 0;
            int i = 0;
            foreach (DataGridViewRow row in lbName.Rows)
            {
                row.Cells[1].Value = row.Cells[1].EditedFormattedValue;
                if (strType == "RF")
                {
                    if (i < Utils.RequiredMacrosRF.Length)
                    {
                        row.Cells[1].Value = false;
                    }
                    else
                    {
                        if ((bool)row.Cells[1].Value)
                        {
                            n++;
                        }
                    }
                    i++;
                }
                else if ((bool)row.Cells[1].Value) n++;
            }
            return n;
        }


        private void AppendTheseRows(CMoveSpace cms)
        {
            string strAdded = "";
            string sB = "";
            string sM = "";
            string sR = "";
            int i = -1;
            foreach (DataGridViewRow row in lbName.Rows)
            {
                bool bWantSelect = ((bool)row.Cells[1].Value) || ((bool)row.Cells[1].EditedFormattedValue);
                i++;
                if (bWantSelect)
                {
                    sM = row.Cells[3].Value.ToString(); // macro name
                    sB = DataTable[i].sBody;
                    sR = DataTable[i].rBody;
                    if (sR == null) sR = "<nl>";
                    if (sR == "") sR = "<nl>";
                    if (sM != "")
                    {
                        strAdded += sM + Environment.NewLine;
                        strAdded += sB + Environment.NewLine;
                        strAdded += sR + Environment.NewLine;
                    }
                    else
                    {
                        if(cms.strType == "HP")
                        {
                            sM = Utils.GetDefaultMacName(i);
                            strAdded += sM + Environment.NewLine;
                            strAdded += sB + Environment.NewLine;
                            strAdded += "<nl>" + Environment.NewLine;
                        }
                    }
                    row.Cells[1].Value = true;
                }
            }
            Utils.FileAppendText(cms.strDes, strAdded);  // has a pair of newlines at end
            mnuCmpHpTr.Enabled = cms.strType == "HP" && cms.strDes == "TR";
        }

        private void InsertTheseRows(CMoveSpace cms)
        {
            List<CNewMac> cb = new List<CNewMac>();
            string[] HPsaved;
            string strOut = "";
            string strLoc = "";
            int i = -1;
            foreach (DataGridViewRow row in lbName.Rows)
            {
                bool bWantSelect = ((bool)row.Cells[1].Value) || ((bool)row.Cells[1].EditedFormattedValue);
                i++;
                if (bWantSelect)
                {
                    CNewMac newMac = new CNewMac();
                    newMac.AddNB(row.Cells[3].Value.ToString(), DataTable[i].sBody, DataTable[i].rBody);   // no newlines as added later
                    row.Cells[1].Value = true;
                    cb.Add(newMac);
                }
            }
            // look through the HP file for a place to put them
            strLoc = Utils.FNtoPath("HP");
            HPsaved = File.ReadAllLines(strLoc);
            i = -1;
            foreach (string s in HPsaved)
            {
                i++;
                if (s == "")
                {
                    if (cb.Count != 0)
                    {
                        HPsaved[i] = cb[0].sName;
                        string t = cb[0].sBody;
                        HPsaved[i + 1] = (t == "") ? "Body Missing" : t;
                        cb.RemoveAt(0);
                    }
                }
                strOut += HPsaved[i] + Environment.NewLine;
            }
            Utils.WriteAllText(strLoc, Utils.NoTrailingNL(strOut));
        }

        private void PerformMove(CMoveSpace cms)
        {
            int i = -1;
            int nResume = -1;   // row to resume at

            if (!cms.bDelete)        // need to move them, not just delete them
            {
                if (cms.strDes != "HP")
                {
                    AppendTheseRows(cms);
                }
                else
                {
                    InsertTheseRows(cms);
                }
            }
            NumCheckedMacros = 0;
            if (cms.bCopy) return;

            //handle the ones left over.  if HP then just blank out the body and save
            //else replace the disk file and reload

            if (cms.strType == "HP")
            {
                foreach (DataGridViewRow row in lbName.Rows)
                {
                    i++;
                    if ((bool)row.Cells[1].EditedFormattedValue)
                    {
                        DataTable[i].sBody = "";
                        DataTable[i].MacName = "Macro" + (i + 1).ToString();
                        row.Cells[3].Value = DataTable[i].MacName;
                        row.Cells[1].Value = false;
                        if (i == CurrentRowSelected) tbBody.Text = "";
                        if (nResume < 0)
                            nResume = i;
                    }
                }
                ReSaveAsTXT(cms.strType);
                ShowUneditedRow(nResume);
                return;
            }
            SaveWantedMacros(cms.strType);
        }

        // the ones unchecked are to be saved
        private void SaveWantedMacros(string strType)
        {
            string strAdded = "";
            string strPath = Utils.FNtoPath(strType);
            int i = -1;
            foreach (DataGridViewRow row in lbName.Rows)
            {
                i++;
                bool bWantDelete = (bool)row.Cells[1].Value; // || ((bool)row.Cells[1].EditedFormattedValue);
                if (!bWantDelete)
                {
                    strAdded += row.Cells[3].Value.ToString() + Environment.NewLine;
                    strAdded += DataTable[i].sBody + Environment.NewLine;
                    strAdded += DataTable[i].rBody + Environment.NewLine;
                }
                else
                {
                    row.Cells[1].Value = false;
                }
            }
            if (strAdded != "")
            {
                Utils.WriteAllText(strPath, strAdded);
                LoadFromTXT(strType);
                NumInBody = i + 1;
            }
            else
            {
                File.Delete(strPath);
                lbName.Rows.Clear();
                NumInBody = 0;
            }
            ShowUneditedRow(0);
        }


        private void mMoveMacro_Click(object sender, EventArgs e)
        {
            cms.Init();
            cms.strType = strType;
            cms.bRun = false;
            cms.bDelete = false;

            cms.nChecked = CountChecks();
            MoveMacro mm = new MoveMacro(ref cms);
            mm.ShowDialog();
            mm.Dispose();
            if (cms.bRun && cms.nChecked > 0)
            {
                PerformMove(cms);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RaiseSearch();
        }

        private void btnDelChecked_Click(object sender, EventArgs e)
        {
            int n = CountChecks();  // is not always the number actually checked as some reserved
            if (n == 0) return;
            if (n > 1 && n == NumCheckedMacros)
            {
                DialogResult dr = MessageBox.Show("You are deleting all the macros", "YES will confirm", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No) return;
            }
            CMoveSpace cms = new CMoveSpace();
            cms.strType = strType;
            cms.bDelete = true;
            PerformMove(cms);
            NumCheckedMacros = CountChecks();
            btnDelChecked.Enabled = NumCheckedMacros > 0;
        }

        private void mnuLCnT_Click(object sender, EventArgs e)
        {
            utils MyUtils = new utils(ref MySpellCheck);
            MyUtils.Show();
        }

        private void mnuRemoveLocalImgs_Click(object sender, EventArgs e)
        {
            RemoveImages ri = new RemoveImages(ref cBodies);
            ri.ShowDialog();
            ri.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bForceClose)
            {
                this.Close();
            }

        }

        private void btnCopyEmail_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.sEmail == "") return;
            Clipboard.SetText(Properties.Settings.Default.sEmail);
        }

        private void btnSpecialWord_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.SpecialWord == "") return;
            Clipboard.SetText(Properties.Settings.Default.SpecialWord);
        }

        private void main_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            EditHelp("FILE");
        }


        private void btnBold_Click(object sender, EventArgs e)
        {
            Utils.AddBold(ref tbBody);
        }

        private void SetClipUC(bool UCwanted)
        {
            string s;
            if (UCwanted)
            {
                s = Utils.ClipboardGetText().ToUpper();
                Clipboard.SetText(s);
            }
            else
            {
                s = Utils.ClipboardGetText().ToLower();
                Clipboard.SetText(s);
            }
        }


        private void btnCleanUrl_Click(object sender, EventArgs e)
        {
            int i, j;
            string sCleanedClip;
            string sOut = "";
            string s,sDirty = Utils.ClipboardGetText();
            if (sDirty == null) return;
            if (sDirty == "") return;
            s = sDirty.ToUpper();
            if (!(s.Contains("HTTPS:") || s.Contains("HTTP:"))) return;
            i = sDirty.Length;
            sOut = AppendDash("Before cleaning", 40) + Environment.NewLine + sDirty + Environment.NewLine;
            s = sDirty;
            Utils.ReplaceUrls(ref s, false);
            sCleanedClip = s;
            Clipboard.SetText(s);
            j = sDirty.Length;
            sOut += AppendDash(Environment.NewLine + "After cleaning", 40) + Environment.NewLine + s + Environment.NewLine;
            sOut += (i == j) ? "No difference" : "\r\nShortened " + (i - j).ToString() + " characters";
            tbCleanedURL.Text = s;
            if (cbShowCleaned.Checked)
            {
                PutOnNotepad(sOut);
            }
            Clipboard.SetText(sCleanedClip);
        }


        // some <tr> are missing the required <tr><td> and cannot be used in forum macro
        // this seems to be a requirement to HP forums and is not an HTML requirement 
        // the program fixes it so that any macros can be used at in the HP forum
        // this would not have to be done if I knew about the problem before I coded anything
#if SPECIAL3
        private bool RunLookMissingTR(string sType, int i, string mName, ref string sIn)
        {
            int n = sIn.IndexOf("<td></td>");
            int m = 0;
            if(n > 0)
            {
                m = 1;
                sIn = sIn.Replace("<td></td>", "<td>&nbsp;</td>");
            }
            string s = sIn;
            sIn = s;
            n = m + LookMissing(ref sIn, 0);
            if (n > 0)
            {
                using (StreamWriter writer = File.AppendText(Utils.WhereExe + "\\LookedMissing.txt"))
                {
                    writer.WriteLine(sType + " " + i.ToString() + " " + mName + " " + n.ToString());
                }
            }
            return n > 0;
        }

        private int LookMissing(ref string sIn, int n)
        {
            string s1, s2, s3;
            int k;
            if (n >= sIn.Length) return 0;
            int i = sIn.IndexOf("<table", n); //6
            if (i < 0) return 0;
            int j = sIn.IndexOf("</table>", i); //8
            if (j < 0) return 0;
            string s = sIn.Substring(i, 8 + j - i);
            n += (j + 8);
            if (s.Contains("<tr>"))return LookMissing(ref sIn, n);
            k = sIn.IndexOf("<td>", i);
            Debug.Assert((i < k) && (k < j));
            s1 = sIn.Substring(0, i);
            k = s.IndexOf("<td>");  // should be first one
            s3 = sIn.Substring(j + 8);
            s2 = s.Substring(0, k) + "<tr>" + s.Substring(k); // 4
            s2 = s2.Replace("</table>", "</tr></table>"); //5
            sIn = s1 + s2 + s3;
            n += 9;// (9 + s2.Length());
            return 1 + LookMissing(ref sIn, n);
        }
#endif

#if SPECIAL2
        private bool RunBorderFix(string sType, int i, string mName, ref string sIn)
        {
            int n = BorderFix(ref sIn);
            if(n > 0)
            {
                using (StreamWriter writer = File.AppendText(Utils.WhereExe + "\\FixedBorder.txt"))
                {
                    writer.WriteLine(sType + " " + i.ToString() + " " + mName + " " + n.ToString());
                }
            }
            return n > 0;
        }

        /*
         * replace
         * <img src= border="2">
         * with
         * <table border="1" width="50%"><td><img src=></td></table>
         * 
        */
        private int BorderFix(ref string s)
        {
            string t,u;
            int i,j;
            j = s.LastIndexOf("border=\"2\">");
            if (j < 0) return 0;
            i = s.LastIndexOf("<img", j);
            if (i < 0) return 0;
            t = s.Substring(i, j - i) + ">";
            u = Utils.Form1CellTable(t);
            t = s.Substring(i, 11 + j - i);
            s = s.Replace(t, u);
            return 1 + BorderFix(ref s);
        }
#endif
        //red is #FF6600
        private void btnRed_Click(object sender, EventArgs e)
        {
            if (SetFGcolor(tbColorCode.Text))
            {
                string s = tbColorCode.Text;    // may have changed
                Utils.AddColor(ref tbBody, s);
            }
        }

        private bool SetFGcolor(string sColor)
        {
            string s = sColor.Trim().ToUpper();
            bool bRtn = true;   // assume all is ok
            if (Utils.IsValidHtmlColor(s))
            {
                tbColorCode.Text = s;
            }
            else
            {
                s = "#FF6600";
                tbColorCode.Text = s;
                bRtn = false;
            }
            tbColorCode.ForeColor = ColorTranslator.FromHtml(s);
            return bRtn;
        }
        private void btnColors_Click(object sender, EventArgs e)
        {
            Utils.ShowPageInBrowser("", Utils.sHTMLcolors);
        }

        private void BeepError()
        {
            int frequency = 1000;
            int duration = 500;
            Console.Beep(frequency, duration);
        }

        private void FormGoToKB(string sS)
        {
            string s = sS.Substring(0, 1).ToLower();
            string w = "https://h30434.www3.hp.com/t5/";

            string[] KBl =
            {
                "Printers-Knowledge-Base/tkb-p/printers-knowledge-base",
                "Desktop-Knowledge-Base/tkb-p/desktop-knowledge-base",
                "Notebooks-Knowledge-Base/tkb-p/notebooks-knowledge-base",
                "Gaming-Knowledge-Base/tkb-p/gaming-knowledge-base"
            };
            string sQ = "";
            switch (s)
            {
                case "p": sQ = w + KBl[0]; break;
                case "d": sQ = w + KBl[1]; break;
                case "n": sQ = w + KBl[2]; break;
                case "g": sQ = w + KBl[3]; break;
                case "a":
                    sQ = "https://h30434.www3.hp.com/t5/custom/page/page-id/RecentDiscussions";
                    break;
            }
            if (sQ != "")
                Utils.LocalBrowser(sQ);
        }

        private void HPWS_click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                FormGoToKB(menuItem.Text);
            }
        }

        private void bltnHR_Click(object sender, EventArgs e)
        {
            Utils.InsertHR(ref tbBody);
        }

        private void mnuAskQ_Click(object sender, EventArgs e)
        {
            Utils.ShellHTML("SiteMap.html", true);
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            bool bIgnore = false;
            bool CanCloseFiles = true;
            if (!bForceClose)
            {
                if (!bPageSaved(ref bIgnore))
                {
                    e.Cancel = !bIgnore;
                    CanCloseFiles = false;
                }
            }
            if(e.Cancel == bIgnore)
            {

            }
            if(CanCloseFiles)
            {
                xMacroChanges.SaveChanges();
                xMacroViews.SaveChanges();
                MySpellCheck.DoExit();
                TryGetNextArchive();
                string s = TXTName + ":" + CurrentRowSelected.ToString();
                Properties.Settings.Default.StartupReturn = s;
                Properties.Settings.Default.Save();
            }

        }


        private void btnSwapBR_Click(object sender, EventArgs e)
        {
            tbBody.TextChanged -= tbBody_TextChanged;   
            if (btnSwapBR.Text == "Show <BR>")
            {
                btnSwapBR.Text = "Hide <BR>";
                tbBody.Text = tbBody.Text.Replace(Environment.NewLine, "<br>");
            }
            else
            {
                btnSwapBR.Text = "Show <BR>";
                tbBody.Text = tbBody.Text.Replace("<br />", "<br>");
                tbBody.Text = tbBody.Text.Replace("<br>", Environment.NewLine);
            }
            tbBody.TextChanged += tbBody_TextChanged;
        }

        private void btnHTest_Click(object sender, EventArgs e)
        {
            Utils.SyntaxTest(tbBody.Text);
        }


        private void SigImages()
        {
            CSignature MySigTest = new CSignature( ref MySpellCheck);
            MySigTest.ShowDialog();
            MySigTest.Dispose();
        }
        private void mnuEmoji_Click(object sender, EventArgs e)
        {
            Utils.ShellHTML(Properties.Resources.emoji, false);
        }

        private void mnuImgSig_Click(object sender, EventArgs e)
        {
            SigImages();
        }

        private void mnuCCodes_Click(object sender, EventArgs e)
        {
            Utils.ShellHTML(Properties.Resources.HP_CountryCodes, false);
        }

        private void EnableAllWeb(bool b)
        {
            mnuSearchComm.Enabled = b;
            mnuDrvGoog.Enabled = b;
            mnuHuntDev.Enabled = b;

        }

        private void mnRecDis_Click_1(object sender, EventArgs e)
        {
            TextFromClipboardMNUs = Utils.ClipboardGetText();
            string sL = TextFromClipboardMNUs.ToLower();
            int n = TextFromClipboardMNUs.Length;
            bTextFromClipboardMNUs = !(n <= 1 || n > 128);
            mnuDrvGoog.Enabled = bTextFromClipboardMNUs;
            mnuDevCol.Enabled = bTextFromClipboardMNUs;
            hPYouTubeToolStripMenuItem.Enabled = bTextFromClipboardMNUs;
            mnuHuntDev.Enabled = sL.Contains("dev") && sL.Contains("ven");
            mnuHuntDev.Enabled |= sL.Contains("vid") && sL.Contains("pid");
        }


        // not used jys
        private string sExtractInfo(string sP)
        {
            string sout = "";
            int i = sP.IndexOf("<a href=\"");
            int j = sP.IndexOf("</a>", i + 9);
            string s = sP.Substring(i + 9, j - i - 9);
            return sout;
        }


        private void btnFromHP_Click(object sender, EventArgs e)
        {
            string s = Utils.GetHPclipboard().Trim();
            PasteHTML ph = new PasteHTML();
            string sOut = ph.ProcessClip(ref s);           
            TbodyInsert(sOut.Trim());
        }

        private void lbName_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;

            // Check if the click is on a checkbox cell
            if (dataGridView.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
            {
                if (e.RowIndex != -1)
                {
                    bool isChecked = (bool)dataGridView[e.ColumnIndex, e.RowIndex].Value;
                    dataGridView[e.ColumnIndex, e.RowIndex].Value = !isChecked;
                    NumCheckedMacros += isChecked ? -1 : 1;
                }
                else
                {
                    if (e.ColumnIndex == 0 || e.ColumnIndex == 2) return;
                    NumCheckedMacros = 0;
                    for (int i = 0; i < lbName.Rows.Count; i++)
                    {
                        bool isChecked = (bool)lbName.Rows[i].Cells[1].Value;
                        lbName.Rows[i].Cells[1].Value = !isChecked;
                        NumCheckedMacros += isChecked ? 0 : 1; // not 1:0 
                    }
                }
                btnDelChecked.Enabled = NumCheckedMacros > 0;
            }
        }


        private void btnShowURLs_Click(object sender, EventArgs e)
        {          
            EditOldUrls UDurl;
            string strBody = "";
            bool bUnChanged = true;
            DataFileRecord = rBodyFromTable();
            if (DataFileRecord != "")
            {
                string strTemp = tbBody.Text.Trim();
                UDurl = new EditOldUrls(strTemp, DataFileRecord, ref printerDB);
                UDurl.ShowDialog();
                strBody = UDurl.sBodyOut;
                if(DataFileRecord != "")
                {
                    HasNewDataRecord = UDurl.DataRecordOut;
                    HasNewFormattedData = UDurl.FormattedDataOut;
                }
                UDurl.Dispose();
                if (strBody == null) return;
                if (strBody == "") return;
                int i = DataFileRecord.IndexOf(" -->") + 4;
                int j = HasNewDataRecord.IndexOf(" -->") + 4;
                bUnChanged = (DataFileRecord.Substring(i) == HasNewDataRecord.Substring(j));
                if (bUnChanged) HasNewDataRecord = DataFileRecord;  // use the old timestamp
                bDataFileUnsaved = !bUnChanged;
                DataFileRecord = HasNewDataRecord;
                DataFileFormatted = HasNewDataRecord;
                tbBody.Text = strBody;  

            }
            else
            {
                UDurl = new EditOldUrls(tbBody.Text, "", ref printerDB);
                UDurl.ShowDialog();
                strBody = UDurl.sBodyOut;
                UDurl.Dispose();
                if (strBody == null) return;
                if (strBody == "") return;
                bUnChanged = (tbBody.Text == strBody);
                tbBody.Text = strBody;
            }
            MustFinishEdit(bUnChanged);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //HighlightDIF();
            tbShowClip.Text = "";
            timer2.Enabled = false;
        }

        private void mnPhAlbum_Click(object sender, EventArgs e)
        {
            Utils.ShowMyPhotoAlbum();
        }

        private void mnuLShowDups_Click(object sender, EventArgs e)
        {
            cDupHTTP DupHTTP = new cDupHTTP();
            cBodies = null;
            Utils.TotalNumberMacros = LoadAllFiles();
            foreach (CBody cb in cBodies)
            {
                int n = DupHTTP.AddN(cb.File, cb.Number, cb.sBody);
                int i = Utils.LocalMacroIndexOf(cb.File);
                DupHTTP.nHyper[i] += n;
            }
            ShowDups sd = new ShowDups(ref DupHTTP, ref cBodies, ref MySpellCheck);
            sd.ShowDialog();
            if (sd.strFN != "")
            {
                LoadFromTXT(sd.strFN);
                ShowUneditedRow(sd.iMN);
            }
            sd.Dispose();
        }

        private void lbName_SelectionChanged(object sender, EventArgs e)
        {
            if (lbName.SelectedRows.Count > 0)
            {
                int selectedRowIndex = lbName.SelectedRows[0].Index;
                if (selectedRowIndex >= 0 && selectedRowIndex < MyBindingSource.Count)
                {
                    MyBindingSource.Position = selectedRowIndex;
                }
            }
        }

  
        private void mnuBIOSemu_Click(object sender, EventArgs e)
        {
            BiosEmuSim bes = new BiosEmuSim();
            bes.ShowDialog();
            bes.Dispose();
        }

        private void tbBody_TextChanged(object sender, EventArgs e)
        {
            if (tbChangeNotifed) return;
            tbChangeNotifed = true; 
            MustFinishEdit(false);
        }

        private void mnuToEnglish_Click(object sender, EventArgs e)
        {
            string sObj = Utils.ClipboardGetText().Replace(" ", "%20");
            if (sObj.Length < 20)
            {
                sObj = "translate " + sObj + " to English";

            }
            else sObj = "translate to English";
            sObj = sObj.Replace(" ", "+");
            Utils.LocalBrowser("https://google.com/search?q=" + sObj);
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string sItem = e.ClickedItem.ToString();
            switch(sItem)
            {
                case "Copy":
                    tbBody.Copy(); break;
                case "Cut":
                    tbBody.Cut(); break;
                case "Paste":
                    tbBody.Paste(); break;
                case "Delete":
                    tbBody.SelectedText = ""; break;
            }
        }

        private void PrinterItemClicked(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
            string sName = clickedItem.Name;
            int selectionStart = tbBody.SelectionStart;
            int rowNumber = tbBody.GetLineFromCharIndex(selectionStart);
            int i = tbBody.GetFirstCharIndexFromLine(rowNumber);
            int iLen, iLenSelect;
            string sPara;
            switch (sName)
            {
                case "tsm1s":
                case "tsm2s":
                case "tsm3s":
                case "tsm4s":
                case "tsm5s":
                case "tsm6s":
                case "tsm7s":
                case "tsm8s":
                case "tsm9s":
                case "tsm10s":
                    iLen = tbBody.SelectionLength;
                    if (iLen == 0) return;
                    string s = "<font size=\"" + clickedItem.Text + "\">";
                    sPara =tbBody.Text.Substring(selectionStart, iLen);
                    s += sPara + "</font>";
                    ReplaceText(selectionStart, iLen, s);
                    break;

                case "tsmJustify":
                    iLen = tbBody.SelectionLength;
                    if (iLen == 0) return;
                    sPara = Utils.JustifiedText(tbBody.Text.Substring(selectionStart, iLen));
                    ReplaceText(selectionStart, iLen, sPara);
                    break;

                case "tsmTable":
                    iLenSelect = tbBody.SelectionLength;
                    if (iLenSelect == 0)
                    {
                        string t = Clipboard.GetText();
                        iLen = t.Length;
                        if (iLen == 0) return;
                        sPara = Utils.Form1CellTable(t,"");
                    }
                    else 
                        sPara = Utils.Form1CellTable(tbBody.Text.Substring(selectionStart, iLenSelect),"");
                    ReplaceText(selectionStart, iLenSelect, sPara);
                    break;

                case "tsmNumList":
                    iLenSelect = tbBody.SelectionLength;
                    if (iLenSelect == 0)
                    {
                        string t = Clipboard.GetText();
                        iLen = t.Length;
                        if (iLen == 0) return;
                        sPara = Utils.FormNumList(t);
                    }
                    else
                        sPara = Utils.FormNumList(tbBody.Text.Substring(selectionStart, iLenSelect));
                    ReplaceText(selectionStart, iLenSelect, sPara);
                    break;


                case "tsmResetVideo":
                    tbBody.Text = tbBody.Text.Insert(i,Utils.sDefaultINK[0]);
                    break;
                case "tsmWiFiSetup":
                    tbBody.Text = tbBody.Text.Insert (i, Utils.sDefaultINK[1]);
                    break;
                case "tsmDirect":
                    tbBody.Text = tbBody.Text.Insert(i, Utils.sDefaultINK[2]);
                    break;
                case "tsmWPS":
                    tbBody.Text = tbBody.Text.Insert(i, Utils.sDefaultINK[3]);
                    break;
                case "tsmDriver":
                    tbBody.Text = tbBody.Text.Insert(i, Utils.sDefaultINK[4]);
                    break;
                case "tsmScan":
                    tbBody.Text = tbBody.Text.Insert(i, Utils.sDefaultINK[5]);
                    break;
                case "tsmID":
                    tbBody.Text = tbBody.Text.Insert(i, Utils.sDefaultINK[6]);
                    break;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            toolStripMenuItem1.Enabled = Utils.IsNewPRN(strType);
        }

        private void btnSpellChk_Click(object sender, EventArgs e)
        {
            FullSpellCheck();
        }

        private void SetNextMissed()
        {
            int Loc = SpellCandidates[iBadSpellIndex];
            int Inx = SpellIndex[iBadSpellIndex];
            FocusSpeller(Loc, Inx);
            iBadSpellIndex++;
            if (iBadSpellIndex >= SpellIndex.Count)
                iBadSpellIndex = 0;
        }

        private void btnNextChk_Click(object sender, EventArgs e)
        {
            SetNextMissed();
        }

        private void mnuCmpHpTr_Click(object sender, EventArgs e)
        {
            CompareHPTR cHPTR = new CompareHPTR(ref HPDataTable,"HP");
            cHPTR.ShowDialog();
            cHPTR.Dispose();
        }

        private void mnuCmpHTTP_Click(object sender, EventArgs e)
        {
            bHaveHTMLasLOCAL = ReadLastHTTP();
            if (bHaveHTMLasLOCAL)
            {
                CompareHPTR cHPTR = new CompareHPTR(ref HPDataTable,"HTTP");
                cHPTR.ShowDialog();
                cHPTR.Dispose();
            }
        }



        private void GetNextArchive(string sWhich)
        {
            string filename = Utils.GetDateTimeName(sWhich);
            string folderPath = Properties.Settings.Default.MacroArchive;
            if (Directory.Exists(folderPath))
            {
                string fullPath = System.IO.Path.Combine(folderPath, filename);
                CompressMacros(fullPath, sWhich);
            }
        }

        private void TryGetNextArchive()
        {
            int n = Properties.Settings.Default.AllowChgInx;
            if (nSavedCount < (1 << n)) return;
            GetNextArchive("MACROS");
        }



        // either ALL or    MACROS
        private string GetWildCardName(string sWhich)
        {
            string wc = sWhich + "*.zip";
#if DEBUG
            wc = "DEB_" + wc;
#endif
            return wc;
        }

        private bool GetRecentArchive()
        {
            string folderPath = Properties.Settings.Default.MacroArchive;
            if (!Directory.Exists(folderPath)) return false;
            int n = Properties.Settings.Default.AllowDaysInx;
            var lastWriteTime = FileUtilities.GetMostRecentFileLastWriteTime(folderPath,GetWildCardName("ALL"));
            string filename = Utils.GetDateTimeName("ALL");

            string fullPath = System.IO.Path.Combine(folderPath, filename);
            if (lastWriteTime == null)
            {
                CompressMacros(fullPath, "ALL");
                return true;
            }
            DateTime dt = (DateTime)lastWriteTime;
            TimeSpan ts = DateTime.Now - dt;
            if(ts.Days > (1 << n))
                CompressMacros(fullPath, "ALL");
            return true;
        }


        // button clicked from the new printer tool area so  not a real printer
/*
        private void btnNewPrinter_Click(object sender, EventArgs e)
        {
          
            cPrinter MyPrinter = new cPrinter(ref printerDB, ref MySpellCheck);
            //MyPrinter.AddTestRecord(Utils.DefaultMacName, Utils.DefaultMacSys);
            MyPrinter.ShowDialog();
            string strResults = MyPrinter.strResults;
            string strRecord = MyPrinter.strRecord;
            string sComment = MyPrinter.GetUnsavedHeader();
            tbBody.Text = sComment;
            tbMacName.Text = sNewKey;
            MyPrinter.Dispose();
           
        }
*/

        private void tsmTexting_Click(object sender, EventArgs e)
        {
            Texting MyTexting = new Texting();
            MyTexting.Show();
        }

        private string GetID(string id, string sUrl)
        {
            string s1 = "-->";
            string s2 = "</table>";
            string s3 = "@" + id.Substring(3); //@ID is 3 characters
            string sTemplett =  Resources.PrinterTemplett;
            int i = sTemplett.IndexOf(id);
            int j = sTemplett.IndexOf(s1, i) + s1.Length; 
            int k = sTemplett.IndexOf(s2, i) + s2.Length;
            string sTemp = sTemplett.Substring(j, k - j);
            string sOut = sTemp.Replace(s3, "Click here " + sUrl);
            return sOut;
        }

        private void tsmInteractive_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menuItem)
            {
                string sClip = Clipboard.GetText();
                ParseDevice MyLookup = new ParseDevice();
                string sWanted = MyLookup.Parse(sClip); // could be model or product id
                tbClipboard.Text = sWanted == "" ? "bad clipboard contents" : sWanted;
                if (sWanted != "")
                {
                    string sID = menuItem.Tag as string;
                    string sUrl = MyLookup.GetSearchUrl(sID, sWanted);
                    sUrl = Utils.FormUrl(sUrl, sWanted);
                    string sOut = GetID(sID, sUrl);
                    Utils.ShowRawBrowser(sOut, "");
                }
                MyLookup = null;
            }   
        }

                


        private void tsmRunArchive_Click(object sender, EventArgs e)
        {
            GetNextArchive("MACROS");
        }

        private void tsmRunArchiveAll_Click(object sender, EventArgs e)
        {
            GetNextArchive("ALL");
        }


        private void tsmConfig_Click(object sender, EventArgs e)
        {
            bool bMustExit = false;
            bool bIgnore = false;
            int i = 0;
            Settings MySettings = new Settings(Utils.BrowserWanted, Utils.VolunteerUserID, NumSupplementalSignatures,
                ref xMacroChanges, ref xMacroViews);
            MySettings.ShowDialog();
            Utils.BrowserWanted = MySettings.eBrowser;
            Utils.VolunteerUserID = MySettings.userid;
            bMustExit = MySettings.bWantsExit;
            MySettings.Dispose();
            if (bMustExit) this.Close();
            CheckPWE();
            if (xMacroChanges.sGoTo != "")
            {
                if (!bPageSaved(ref bIgnore)) return;
                string sFN = "";
                string sMN = "";
                xMacroChanges.GoToMacro(ref sFN, ref sMN);
                if (sFN != strType)
                    LoadFromTXT(sFN);
                foreach (dgvStruct dgv in DataTable)
                {
                    if (dgv.MacName == sMN) break;
                    i++;
                    if (i == DataTable.Count)
                    {
                        i = 0;
                        break;
                    }
                }
                ShowUneditedRow(i);
            }
        }

        private void tsmAssociate_Click(object sender, EventArgs e)
        {
            associate MyAssociate = new associate(ref AssociateMacros);
            MyAssociate.ShowDialog();
            bool bChanged = MyAssociate.bChanged;
            if (bChanged && strType == "RF")
            {
                LoadFromTXT("RF");
                ShowBodyFromSelected();
            }
            MyAssociate.Dispose();
            UpdateMyAssoc();
        }



        public static void CreateZipFromFolder(string sourceFolderPath, List<string>WantedZips, string destinationZipFilePath)
        {
            using (FileStream zipToOpen = new FileStream(destinationZipFilePath, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                {
                    foreach (string file in WantedZips)
                    {
                        string sPath = Utils.WhereExe + "/" + file;
                        if (!File.Exists(sPath)) continue;
                        archive.CreateEntryFromFile(sPath, file);
                    }
                    string folderName = Path.GetFileName(sourceFolderPath.TrimEnd(Path.DirectorySeparatorChar));
                    AddDirectoryToZip(archive, sourceFolderPath, folderName);
                }
            }
        }

        private static void AddDirectoryToZip(ZipArchive archive, string sourceDirectory, string entryName)
        {
            // Add the directory entry itself to ensure it's included in the archive
            archive.CreateEntry($"{entryName}/");

            // Add all files from the directory to the archive
            foreach (string file in Directory.GetFiles(sourceDirectory))
            {
                string fileName = Path.GetFileName(file);
                archive.CreateEntryFromFile(file, Path.Combine(entryName, fileName));
            }

            // Recursively add all subdirectories
            foreach (string directory in Directory.GetDirectories(sourceDirectory))
            {
                string directoryName = Path.GetFileName(directory);
                AddDirectoryToZip(archive, directory, Path.Combine(entryName, directoryName));
            }
        }



        // cannot do this if any files are open!
        private void CompressMacros(string sPathZip, string sPrefix)
        {
            string sourceFolder = Utils.WhereExe;
            string DBfolder = sourceFolder;
            string destinationZipFile = sPathZip;
            string[] Images = Directory.GetFiles(Utils.WhereExe, "LOCALIMAGEFILE-*.png");

            List<string> WantedZip = new List<string>();
            foreach (string s in Images)
                WantedZip.Add(Path.GetFileName(s));
            foreach (string s in Utils.ListAllTxt)
                WantedZip.Add(s);
            if (sPrefix == "ALL")
            {
                string[] RTFs = Directory.GetFiles(Utils.WhereExe, "*.docx");
                foreach (string s in RTFs)
                {
                    if (s == Utils.ScratchSpellFile) continue;
                    WantedZip.Add(Path.GetFileName(s));
                }

            }
            CreateZipFromFolder(DBfolder, WantedZip, sPathZip);
        }

        private void cbShowCleaned_CheckedChanged(object sender, EventArgs e)
        {
            tbCleanedURL.Text = "";
        }


        private string wasHtml = "";
        private void bWasHTML_Click(object sender, EventArgs e)
        {
            if(wasHtml.Length > 0)
            {
                Clipboard.SetText(wasHtml);
                wasHtml = "";
                bWasHTML.Visible = false;
            }
        }
        private void btnNew_MouseHover(object sender, EventArgs e)
        {
            string s = Clipboard.GetText();
            if (s.Length > 32)
            {
                int i = s.IndexOf("http");
                if(i == 0)
                {
                    wasHtml = s;                    
                    bWasHTML.Visible = true;
                }
                s = "";
            }
            if (s == "")
                s = Utils.UnNamedMacro;
            Clipboard.SetText(s);
            tbShowClip.Text = s;
            timer2.Enabled = true;
        }

       
  

        private void lbName_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            bool bAllowMenu = true;
            if (strType != "RF") return;
            if (e.RowIndex < 0 || e.ColumnIndex != 2) return;
            if (e.Button == MouseButtons.Right)
            {
                // Get the row index where the click occurred
                //var hitTestInfo = lbName.HitTest(e.X, e.Y);
                //if (hitTestInfo.RowIndex >= 0) // Check if a row was clicked
                //{}
                bool bHasCheck = lbName.Rows[e.RowIndex].Cells[2].Value != null;
                cMS2_Row = e.RowIndex;
                if(e.RowIndex != CurrentRowSelected)
                    bAllowMenu = ShowSelectedRow(e.RowIndex);
                if(bAllowMenu)
                    contextMenuStrip2.Show(Cursor.Position);            
            }
        }

        int cMS2_Row = -1;
        int cMS2_obj = -1;
        private void contextMenuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string sItem = e.ClickedItem.ToString();
            switch (sItem)
            {
                case "Add QuickWatch":
                    cMS2_obj = 0;
                    lbName.Rows[cMS2_Row].Cells[2].Value = "Q";
                    break;
                case "Add Clipboard macro":
                    cMS2_obj = 1;
                    lbName.Rows[cMS2_Row].Cells[2].Value = "C";
                    break;
                case "Remove":
                    cMS2_obj = 2;
                    lbName.Rows[cMS2_Row].Cells[2].Value = "";
                    break;
            }
            if(cMS2_obj != -1)
            {
                Debug.Assert(CurrentRowSelected == cMS2_Row);
                DataTable[CurrentRowSelected].rBody = (string)lbName.Rows[cMS2_Row].Cells[2].Value;
                SaveAsTXT(TXTName);
                ConfigureAssociation();                
            }
        }

        private void Qwclick(object sender, EventArgs e)
        {           
            ToolStripMenuItem  t = sender as ToolStripMenuItem;
            string s = AssociateMacros[(int)t.Tag].sBody;
            s = s.Replace(Environment.NewLine, "<br>");
            Utils.CopyHTML(s);
        }

        private void UpdateQWarnings()
        {
            ToolStripMenuItem tmQW = quickWarningsToolStripMenuItem;
            tmQW.DropDownItems.Clear();
            int i = 0;
            foreach (cQCmacros m in AssociateMacros)
            {
                if (m.sType == "Q")
                {
                    ToolStripMenuItem submenuItem = new ToolStripMenuItem(m.sName);
                    submenuItem.Tag = i;
                    submenuItem.Click += (s, e) => Qwclick(s, e);
                    tmQW.DropDownItems.Add(submenuItem);
                }
                i++;
            }
            ToolStripMenuItem parentMenu = new ToolStripMenuItem("Parent Menu");
        }

        private void ClipClick(object sender, EventArgs e)
        {
            ToolStripMenuItem t = sender as ToolStripMenuItem;
            string s = AssociateMacros[(int)t.Tag].sBody;
            string c = Utils.ClipboardGetText().Trim();
            string sObj = s.Replace("@clipboard@", c);
            if(sObj.Contains("@clip-board@"))
            {
                int i = c.IndexOf("-");
                if (i < 0)
                {
                    sObj = s.Replace("@clip-board@", c);
                }
                else
                {
                    i++;
                    string sPrefix = c.Substring(0, i);
                    string st = "";
                    char[] cC = c.Substring(i).ToCharArray();
                    for(i = 0; i < 2; i++)
                    {
                        if (cC[i] >= 'a' && cC[i] <= 'z')
                        {
                            st += cC[i];
                        }
                    }
                    if (st == "") st += cC[0] + "xxx";
                    sPrefix += st;
                    sObj = s.Replace("@clip-board@", sPrefix);
                }
            }
            sObj = sObj.Replace(Environment.NewLine, "<br>");
            Utils.ShowRawBrowser(sObj, "");
            //Utils.LocalBrowser(sObj);
        }

        private void UpdateClips()
        {
            int i,j;
            int sepStart = mnRecDis.DropDownItems.IndexOf(toolStripSepSTART);
            int sepEnd = mnRecDis.DropDownItems.IndexOf(toolStripSepEND);
            for (i = sepEnd -1; i > sepStart; i--)
            {
                mnRecDis.DropDownItems.RemoveAt(i);
            }
            i = 0;
            j = 1;
            foreach (cQCmacros m in AssociateMacros)
            {
                if (m.sType == "C")
                {
                    ToolStripMenuItem submenuItem = new ToolStripMenuItem(m.sName);
                    submenuItem.Tag = i;
                    submenuItem.Click += (s, e) => ClipClick(s, e);
                    mnRecDis.DropDownItems.Insert(sepStart + j, submenuItem);
                    j++;
                }
                i++;
            }
        }

        private void UpdateMyAssoc()
        {
            //menuStrip1.Items.Remove(quickWarningsToolStripMenuItem);
            UpdateQWarnings();
            UpdateClips();
        }
        private void ConfigureAssociation()
        {
            associate MyAssociate = new associate(ref AssociateMacros);
            MyAssociate.Dispose();
            UpdateMyAssoc();
        }

        private void btnShowImage_Click(object sender, EventArgs e)
        {
            int r = 0;
            foreach (dgvStruct row in DataTable)
            {
                if(row.HPimage)
                {
                    lbName.Rows[r].Cells[1].Style.BackColor = Color.Blue;
                }
                r++;
            }
        }
    }
}
