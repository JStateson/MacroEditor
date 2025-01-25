using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.Security;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Text;
using System.Drawing;
using System.Windows.Ink;
using System.Collections.Generic;
using System.Security.Policy;
using System.Windows.Automation;
using System.Net.NetworkInformation;
using System.Linq.Expressions;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.AxHost;
using static MacroEditor.main;
using System.Windows.Media.Animation;
using System.Runtime.Remoting.Lifetime;
using System.Windows.Documents;
using System.Windows.Input;

namespace MacroEditor
{
    public class CMoveSpace
    {/// <summary>
     /// "PC", "AIO", "LJ", "DJ", "OJ", "IN", "OS", "NET", "HW", "RF", "NO", "TR", "HP"  DO NOT CHANGE ORDER OF BELOW ITEMS
     /// </summary>

        public string[] MacroIDs;
        public int[] nMacsInFile;
        public int[] nMacsAllowed;
        public void Init()
        {
            int i = 0;
            MacroIDs = Utils.LocalMacroPrefix;
            nMacsInFile = new int[MacroIDs.Length];
            nMacsAllowed = new int[MacroIDs.Length];
            for (i = 0; i < MacroIDs.Length - 1; i++)
                nMacsAllowed[i] = Utils.NumMacros;
            nMacsAllowed[MacroIDs.Length - 1] = Utils.HPmaxNumber; // only 30 macros in HP forum original list
            // this item has to be "HP" and stay at end of any string list of files
            CountEmpties();
        }
        public void SetMacCount(string ID, int n)
        {
            int i = 0;
            foreach(string s in MacroIDs)
            {
                if(s == ID)
                {
                    nMacsInFile[i] = n;
                    return;
                }
                i++;
            }
        }
        public int GetMacCount(string ID)
        {
            int i = 0;
            foreach (string s in MacroIDs)
            {
                if (s == ID)
                {
                    return nMacsInFile[i];
                }
                i++;
            }
            return -1;
        }
        public int GetMacCountAvailable(string ID)
        {
            int i = 0;
            int n = 0;
            foreach (string s in MacroIDs)
            {
                if (s == ID)
                {
                    return nMacsAllowed[i] - nMacsInFile[i];
                }
                i++;
            }
            return n;
        }
        public void UpdateCount()
        {
            int n = GetMacCount(strDes);
            n += nChecked;
            SetMacCount(strDes, n);
            n = GetMacCount(strType);   //removing checked from source file
            n -= nChecked;
            SetMacCount(strType, n);
        }

        private int CountItems(string s)
        {
            if (Utils.NoFileThere(s)) return 0;
            string[] sAll = File.ReadAllLines(Utils.FNtoPath(s));
            if (s == "HP")
            {
                int j = 0;
                for (int i = 0; i < sAll.Length; i += 2)
                {
                    if (sAll[i].Length != 0) j++;
                }
                return j;
            }
            return sAll.Length / 2;
        }

        public void CountEmpties()
        {
            foreach (string s in Utils.LocalMacroPrefix)
            {
                SetMacCount(s, CountItems(s));
            }
        }

        public int nChecked;    // this many checked
        public bool bRun;       // if true then perform move
        public bool bCopy;      // do not delete
        public string strType;    // name of the "from" file ie: source
        public string strDes;   // destination
        public bool bDelete;    // if true then just delete the item from the source, no move required
    }

    public class cMacroChanges
    {
        private List<string> mChanges;
        private string ChangeList = "";
        public string sGoTo = "";
        public int nSelectedRowIndex;
        private int iPadSize = 6;   // 5 integers including a space
        private int x16 = 17;   // 16 hex chars plus a space
        private int x4 = 5;     // 4 hex chars plus a space
        private int ReadLinesIntoList()
        {
            mChanges = new List<string>();

            if (File.Exists(ChangeList))
            {
                mChanges.AddRange(File.ReadAllLines(ChangeList));
            }
            else
            {
                return 0;
            }
            return mChanges.Count;
        }

        public int Init(string sFileName)
        {
            ChangeList = Utils.WhereExe + "\\" + sFileName;
            return ReadLinesIntoList();
        }

        public int GetZ000(string sHEX)
        {
            string s = sHEX.Substring(sHEX.Length - 3);
            return Convert.ToInt32(s,16);
        }
        private string SetZ000(string sHex, int n)
        {
            string sRtn = "";
            if(n == 0)
            {
                sRtn = sHex.Substring(0, 13) + "000";
            }
            else // add one
            {
                n = GetZ000(sHex);
                n++;
                sRtn = Convert.ToString(n,16);
                sRtn =sHex.Substring(0,13) + sRtn.PadLeft(3, '0');
            }
            return sRtn;
        }

        private void IncChange(int i)
        {
            string s = mChanges[i];
            int j = s.IndexOf(":");
            string t = SetZ000(s.Substring(0,j), 1);
            t += s.Substring(j);
            mChanges[i] = t;
        }

        public string TicksToHex(long ticks)
        {
            return ticks.ToString("X16");
        }



        // filename and macro name
        public void AddChange(string sFN, string sMN)
        {

            DateTime date = DateTime.Now;
            //string formattedDate = date.ToString("MMMM dd yyyy, hh:mm tt");
            //  July 04 2024, 01:45 PM
            string hexString = TicksToHex(date.Ticks);
            string sFnMn = sFN + ":" + sMN;
            //if (mChanges.Any(s => s.Contains(sFnMn))) return;
            Predicate<string> matchPredicate = s => s.Contains(sFnMn);
            int i = mChanges.FindIndex(matchPredicate);
            if(i == -1)
                mChanges.Add(SetZ000(hexString,0) + ":" + sFnMn);
            else
            {
                IncChange(i);
            }
        }
        public void AddView(string sFN, string sMN)
        {

            DateTime date = DateTime.Now;
            //string formattedDate = date.ToString("MMMM dd yyyy, hh:mm tt");
            //  July 04 2024, 01:45 PM
            long ticks = date.Ticks;
            string hexString = "0001";
            string sFnMn = sFN + ":" + sMN;
            Predicate<string> matchPredicate = s => s.Contains(sFnMn);
            int i = mChanges.FindIndex(matchPredicate);
            if(i >= 0)
            {
                string s = mChanges[i].Substring(0, 4);
                int n = Convert.ToInt32(s, 16);
                n++;
                hexString = n.ToString("X4");
                mChanges.RemoveAt(i);
            }
            mChanges.Add(hexString + ":" + sFnMn);
        }

        public bool bIsEmpty()
        {
            return mChanges.Count <= 0;
        }

        public void RemView(string sFN, string t)
        {
            string sFnMn = sFN + ":" + t.Substring(iPadSize);
            Predicate<string> matchPredicate = s => s.Contains(sFnMn);
            int i = mChanges.FindIndex(matchPredicate);
            if (i >= 0)
            {
                mChanges.RemoveAt(i);
            }
        }

        public bool GoToMacro(ref string sFN, ref string sMN)
        {
            if (sGoTo == "") return false;
            sFN = sGoTo.Substring(0, 2);
            sMN = sGoTo.Substring(3);
            return true;
        }

        public void ClearChanges()
        {
            File.Delete(ChangeList);
            mChanges.Clear();
        }

        public void SaveChanges()
        {
            if (mChanges.Count > 0)
            {
                File.WriteAllLines(ChangeList, mChanges);
            }
            else File.Delete(ChangeList);
        }

        // sMN is the 2 or 3 character Macro Name
        public void TryRemove(string sFN, string sMN)
        {
            string sFnMn = sFN + ":" + sMN;
            mChanges.RemoveAll(line => line == sFnMn);
        }

        public List<string> GetFN(int i)
        {
            List<string> st = new List<string>();
            foreach (string s in mChanges)
            {
                string t = sGetFN(s.Substring(i));
                if (!st.Contains(t))
                {
                    st.Add(t);
                }
            }
            return st;
        }

        public List<string> GetFNChanges()
        {
            return GetFN(x16);
        }
        public List<string> GetFNViews()
        {
            return GetFN(x4);
        }

        public string sGetFN(string s)
        {
            int i = s.IndexOf(":");
            return s.Substring(0, i);
        }

        public List<int> GetMNViews(string sFN, ref List<string> stEdit, ref List<int> nViewed)
        {
            stEdit.Clear();   // the edit data source
            nViewed.Clear();     // the edit date
            int n;
            foreach (string s in mChanges)
            {
                string iView = s.Substring(0, 4);    // times viewed in hex
                n = Convert.ToInt32(iView, 16);
                string sn = n.ToString().PadLeft(iPadSize-1) + " ";
                string t = sGetFN(s.Substring(x4));
                if (t == sFN)
                {
                    stEdit.Add(sn + s.Substring(iPadSize + t.Length)); // there is no :
                    nViewed.Add((int)Convert.ToInt32(iView, 16));
                }
            }
            // Create a list of tuples where each tuple contains an integer and its original index
            var SrtInx = nViewed.Select((number, index) => new { Number = number, Index = index }).ToList();

            // Sort the list of tuples based on the integer values
            var sortedSrtInx = SrtInx.OrderByDescending(x => x.Number).ToList(); //Descending
            List<int> SrtOut = new List<int>();
            foreach (var item in sortedSrtInx)
            {
                SrtOut.Add(item.Index);
            }
            return SrtOut;
        }
        //1234567890123456:FileID:Macroname
        //---hex----------:xx or xxx: 
        public List<int> GetMNChanges(string sFN, ref List<string> stEdit, ref List<long> lDate)
        {
            stEdit.Clear();   // the edit data source
            lDate.Clear();     // the edit date
            int n = 0;
            foreach (string s in mChanges)
            {
                string dST = s.Substring(0, 16);    // date string
                string t = sGetFN(s.Substring(x16));
                if (t == sFN)
                {
                    stEdit.Add(s.Substring(x16 + t.Length + 1));
                    lDate.Add((long) Convert.ToInt64(dST,16));
                    n++;
                }
            }
            // Create a list of tuples where each tuple contains an integer and its original index
            var SrtInx = lDate.Select((number, index) => new { Number = number, Index = index }).ToList();

            // Sort the list of tuples based on the integer values
            var sortedSrtInx = SrtInx.OrderByDescending(x => x.Number).ToList(); //Descending
            List<int> SrtOut = new List<int>();
            foreach(var item in sortedSrtInx)
            {
                SrtOut.Add(item.Index);
            }
            return SrtOut;
        }

        public int CalculateChecksum(string input)
        {
            int checksum = 0;
            foreach (char character in input)
            {
                checksum += character;
            }
            return checksum;
        }

        public void isMacroChanged(int chk, string sFN, string sMN, string sBody)
        {
            if (chk != CalculateChecksum(sBody))
            {
                AddChange(sFN, sMN);
            }
        }
    }



        // to add additional macro pages you need to mod the above cms to add an neXX and the below
        // and add a specific file opening if desired to have it in the menu dropdown
    public static class Utils
    {
        public const string mTableStyle = "<style>table  th, td { border: 1px solid black; padding: 10px; text-align: justify;}</style>";
        public const int iNMacros = 13;
        public const int NumMacros = 999;
        public static int HPmaxNumber = 40;  // some users may have more!
        public static int HPminNumber = 30; // eveyone gets 30
        public static int nLongestExpectedURL = 256;
        public static int TotalNumberMacros = 0;
        public static string[] nUse ={ // these must match the button names in WordSearch
            //"PC","Printer","Drivers","EBay","Google","Manuals","YouTube","HP KB"
              "PC","PRN",    "DRV",    "EBA", "GOO",   "MAN",    "HPYT",   "HPKB"
        };
        public static bool bSpellingEnabled = false;
        public static string[] sDefaultINK = new string[7] {
                    "You may need to reset the printer: video here",
                    "Wi-Fi setup to router:  video here",
                    "For Wi-Fi direct setup see page xx click here.<br>This is useful if there is no modem or you do not want to give someone access to your modem but you want to let them print something.",
                    "Simple push button or WPS setup is described on page xx of User Manual",
                    "Full feature DRIVER software DEVICE MONTH YEAR",
                    "Full feature SCANNER software DEVICE MONTH YEAR",
                    "Printer Reference ID" };

        public static string ModelsID = "<br><!-- @MODELS@ ";

        public static bool IsNewPRN (string sT)
        {
            if (sT == "") return false;
            return (sPrinterTypes.Contains(" " + sT + " "));
        }

        public static string nBR(int n)
        {
            string s = "";
            for (int i = 0; i < n; i ++)
            {
                s += "<br>";
            }
            return s;
        }


        public static string GetDateTimeName(string sPrefix)
        {
            DateTime now = DateTime.Now;
            string filename = sPrefix + now.ToString("yyyyMMdd_HHmmss") + ".zip";
#if DEBUG
            filename = "DEB_" + filename;
#endif
            return filename;
        }



        public static bool GetHT(string s, ref string sH, ref string sT)
        {
            string sLC = s.ToLower();
            int i = sLC.IndexOf("href=");
            if (i < 0) return false;
            i += 5;
            string c = s.Substring(i, 1);
            i++;
            int j = sLC.IndexOf(c, i);
            if (j < 0) return false;
            sH = s.Substring(i, j - i);
            i = s.IndexOf(">", j);
            if (i < 0) return false;
            i++;
            j = s.LastIndexOf("</a>");
            if (j < 0) return false;
            sT = s.Substring(i, j - i);
            return true;
        }

        public static int ReadFile(string strFN, ref List<cMacroEach> ThisList)
        {
            string TXTmacName = Utils.FNtoPath(strFN);
            int n = 0;
            if (File.Exists(TXTmacName))
            {
                StreamReader sr = new StreamReader(TXTmacName);
                string line = sr.ReadLine();
                while (line != null)
                {
                    cMacroEach cME = new cMacroEach();
                    cME.sName = line;
                    cME.sBody = sr.ReadLine();
                    cME.rBody = sr.ReadLine();
                    ThisList.Add(cME);
                    n++;
                    line = sr.ReadLine();
                    if (line == null) break;
                    if (line == "")
                        line = Utils.GetDefaultMacName(n);
                }
                sr.Close();
            }
            return n;
        }

        public static string nNL(int n)
        {
            string s = "";
            for (int i = 0; i < n; i++)
            {
                s += Environment.NewLine;
            }
            return s;
        }

        private static string[] sUse = // possible new macros
        {
            "PC AIO HW",            //PC
            "LJ DJ OJ IN HW",          //PRN
            "PC AIO LJ DJ OJ IN",      //DRV
            "NET OS HW",            //EBY
            "NET OS HW RF NO",      //GOO
            "PC AIO LJ DJ OJ IN",      //MAN
            "LJ DJ OJ IN NET",         //Youtube
            "PC AIO LJ DJ OJ IN OS"    //HP KB
        };
        public static string sFindUses(string s)
        {
            int i = 0;
            foreach(string t in nUse )
            {
                if (s == t) return sUse[i];
                i++;
            }
            return "";
        }
        // do not change the order of below items and HP must be last!
        public static string sPrinterTypes = " LJ DJ OJ IN ";    // must have a space and match below
        public static string[] LocalMacroPrefix = new string[iNMacros]  { "PC", "AIO", "LJ", "DJ", "OJ", "IN", "OS", "NET", "HW", "RF", "NO", "TR", "HP" };
        public static string[] LocalMacroFullname = new string[iNMacros] { "Desktop(PC)", "AIO or Laptop", "LaserJet(LJ)",
                "DeskJet(DJ)", "OfficeJet(OJ)", "Tank-Inkjet(IN)", "OS related", "Network related", "Hardware", "Reference", "Notes", "Transfer", "HP from HTML" };
        public static string[] LocalMacroRefs = new string[iNMacros] {"PC Reference","PC Reference","LaserJet Reference",
                "DeskJet Reference","OfficeJet Reference","Tank-Ink Reference", "", "", "", "","","",""};

        // there is an "SI" type which is used for SIgnature images and an Al for AllowedSpelling
        public static List<string>ListAllTxt = new List<string>();
        public static void FormAllTxt()
        {
            ListAllTxt.Clear();
            foreach(string s in LocalMacroPrefix )
                ListAllTxt.Add( FNtoName(s) );
            ListAllTxt.Add(FNtoName("SI"));
            ListAllTxt.Add(SpellList);
            ListAllTxt.Add(AssociationList);
        }

        public static int elbPrinterLayout = 0;
        public static int elbUrls = 1;
        public static int elbButtons = 2;
        public static int elbTips = 3;
        public static int elbPhrases = 4;
        public static string XMLprefix = "<!DOCTYPE html><html><head><meta http-equiv=\"Content-type\" content=\"text/html;charset=UTF-8\" /></head><body style=\"width: 800px; auto;\">";
        public static string XMLsuffix = "</body></html>";
        public static string sIsAlbum = "/image/serverpage/image-id";
        public static string sHasSize = "/image-size/";
        public static string sDifSiz = "tiny,thumb,small,medium,large";
        //public static string XMLdtd = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
        public static int HPforumWidth = 825;   // seems like any response "box" never exceeds 825 pixels
        //    <div style="width: 825px; background-color: lightblue; padding: 10px;">
        //<body style="width: 800px; margin: 0 auto;">
        public static string WhereExe = "";
        public static string UnNamedMacro = "Change Me";
        public static string SpellList = "AllowedSpelling.txt";
        public static string ScratchSpellFile = "ScratchSpellFile.docx";
        public static int MaxLinesInSteps = 12; // no more than 16 lines in any steps of instructions.
        public static string AssociationList = "Associate.txt";
        public static string NewPrnComment = "<!-- @MACRO@:(";
        public static string MacPrinterFolder = "DataFiles";
        public static string SupSigPrefix = "=+-=";   // these are used to identify the macro addition to make it eacy
        public static string SupSigSuffix = "=-+=";   // to delete or to change.
        public static bool bRecordUnscrubbedURLs = false;
        public static string YesButton = "<img src=\"https://h30467.www3.hp.com/t5/image/serverpage/image-id/71238i8585EF0CF97FB353/image-dimensions/50x27?v=v2\">";
        public static string SolButton = "<img src=\"https://h30467.www3.hp.com/t5/image/serverpage/image-id/71236i432711946C879F03/image-dimensions/129x32?v=v2\">";
        public static string AllAlphas = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxya";
        public static string[] sPossibleLanguageOption = { "-16\" target=", "-16?openCLC=true\" target=" };
        public static List<string> uButtons;
        public static int LocalMacroIndexOf(string s)
        {
            int n = 0;
            foreach(string t in LocalMacroPrefix)
            {
                if (t == s) break;
                n++;
            }
            return n;
        }

        public static string NewItemHasErr(string sItem, string TagName)
        {
            string sClip = sItem.ToLower();
            int i;
            int ErrCod = 0;
            string[] sMsg = {
                "Expected http or https but found none",
                "Newlines are not permitted",
                "expected integer value, found none",
                "the phrase ftp is missing",
                "the site YouTube is missing",
                "too many steps, only" + Utils.MaxLinesInSteps.ToString() + " allowd"
            };
            switch (TagName)
            {
                case "Reset Video":
                case "Router Video":
                case "Direct Video":
                case "Direct Doc":
                case "WPS Doc":
                case "Software":
                case "All Docs":
                case "Reference":
                case "Parts":
                case "Assembly":
                    if (sClip.IndexOf("http") == -1) ErrCod = 1;
                    if (sClip.IndexOf(Environment.NewLine) >= 0) ErrCod = 2;
                    break;
                case "Direct Page":
                case "WPS Page":
                    ErrCod = int.TryParse(sClip, out i) ? 0 : 3;
                    break;
                case "Driver":
                case "Scanner":
                    ErrCod = (sClip.IndexOf("ftp") == -1) ? 4 : 0;
                    break;
                case "YouTube":
                    if (sClip.IndexOf("youtube") == -1) ErrCod = 5;
                    if (sClip.IndexOf("http") == -1) ErrCod = 1;
                    if (sClip.IndexOf(Environment.NewLine) >= 0) ErrCod = 2;
                    break;
                case "Reset Steps":
                    i = sClip.Count(c => c == '\n');
                    if (i > Utils.MaxLinesInSteps) ErrCod = 6;
                    break;
            }
            if (ErrCod > 0)
            {
                return( "The item you entered is not in the correct format" + Environment.NewLine + sMsg[ErrCod - 1]);
            }
            return "";
        }
        public static string sTOe(string s)
        {
            return "@" + s + "@";
        }

        public static void ScrollToCaretPosition(System.Windows.Forms.TextBox textBox, int characterPosition, int iLen)
        {
            textBox.Focus();
            textBox.SelectionStart = characterPosition;
            textBox.SelectionLength = iLen;
            textBox.ScrollToCaret();
        }

        public static string GetDefaultMacName(int i)
        {
            return "Macro " + (i + 1).ToString();
        }

        private static string RemoveHeaders(string s)
        {
            string t = s.Replace("<!--StartFragment-->", "");
            int i = t.IndexOf("<body>");
            int j = t.IndexOf("</body>");
            if (i < 0 || j < 0) return "";
            i += 6;
            return t.Substring(i, j - i);
        }

        public static string ConvertToHTML(string sClip)
        {
            string strBody = sClip;
            while(strBody.Contains("%25"))
                strBody = strBody.Replace("%25", "%");

            string[] sHave = { "%26","&amp;", "&lt;", "&gt;", "&nbsp;", "%3A", "%2F", "%3F", "%3D", "<P>", "</P>" };
            string[] sWant = { "&"  ,"&"    , "<"   , ">"   , " "     , ":"  , "/"  , "?"  , "="  , "<p>", "</p>" };
            if (strBody == "") return "";
            string hCase, wCase;
            for (int i = 0; i < sHave.Length; i++)
            {
                hCase = sHave[i].ToLower();
                wCase = sWant[i];
                while (strBody.Contains(hCase))
                {
                    strBody = strBody.Replace(hCase, wCase);
                }
                hCase = sHave[i].ToUpper();
                while (strBody.Contains(hCase))
                {
                    strBody = strBody.Replace(hCase, wCase);
                }
            }
            return strBody;
        }

        // find number of rows
        public static int MakeDivisible(int nItems, int nColumnsWanted)
        {
            int remainder = nItems % nColumnsWanted;
            if (remainder == 0) return nItems;
            return nItems + (nColumnsWanted - remainder);
        }


        public static string FormHeader(string sName, string sType)
        {
            string sComment = "<!-- @MACRO@:(@key@) TimeStamp=DDDDDDDD_TTTTTT -->";
            string sTS = "DDDDDDDD_TTTTTT";
            DateTime now = DateTime.Now;
            string sKey = sName + ":" + sType;
            string TimeStamp = now.ToString("yyyyMMdd_HHmmss");
            sComment = sComment.Replace(sTS, TimeStamp);
            sComment = sComment.Replace("@key@", sKey);
            return sComment;
        }

        public static string GetHPclipboard()
        {
            string strTemp = "";
            if (Clipboard.ContainsText(TextDataFormat.Html))
            {
                strTemp = RemoveHeaders(Clipboard.GetText(TextDataFormat.Html));
            }
            else if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                strTemp = Clipboard.GetText(TextDataFormat.Text);
                if (!strTemp.Contains(Environment.NewLine))
                {
                    strTemp = strTemp.Replace("\n", Environment.NewLine);
                }
            }
            return strTemp;
        }

        public static string AddLanguageOption(string sIN)
        {
            if (sIN.IndexOf(sPossibleLanguageOption[0] ) != -1)
            {
                sIN = sIN.Replace(sPossibleLanguageOption[0], sPossibleLanguageOption[1]);
            }
            return sIN;
        }

        public static string ReplaceStringAtLoc(string original, int iFiller, int startIndex, int length)
        {
            if (startIndex < 0 || startIndex >= original.Length || startIndex + length > original.Length)
            {
                throw new ArgumentOutOfRangeException("startIndex or length is out of range.");
            }
            string replacement = strFill(iFiller, length); // was 8
            string before = original.Substring(0, startIndex);
            string after = original.Substring(startIndex + length);

            return before + replacement + after;
        }


        private static bool bJunk(char c)
        {
            string sJunk = " \r\n\t";
            if (sJunk.Contains(c)) return true;
            return false;
        }

        // adjust i and j so that they do not contains any leading or trailing spaces
        // i is start position, k is length of string
        // no newlines either
        public static string AdjustNoTrim(ref int i, ref int k, ref string s)
        {
            if (s == "" || k == 0) return "";
            if (i >= s.Length) return "";
            int j = i + k - 1;
            char c = s[i];
            while(bJunk(c))
            {
                i++;
                if(i >= s.Length) break;
                c = s[i];
            }
            c = s[j];
            while(bJunk(c))
            {
                j--;
                c = s[j];
            }
            k = j - i + 1;
            if (k > 0)
            {
                return s.Substring(i, k);
            }
            k = 0;
            return "";
        }
        public static void ShellHTML(string s, bool IsFilename)
        {
            string sTemp = WhereExe + "\\";
            if(IsFilename)
            {
                sTemp += s;
                if (!File.Exists(sTemp)) return;
            }
            else
            {
                sTemp += "MyHtml.html";
                File.WriteAllText(sTemp, s);
            }
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "explorer.exe", // The application to run
                    Arguments = sTemp,           // Any arguments to pass to the application
                    UseShellExecute = true,   // Whether to use the operating system shell to start the process
                    RedirectStandardOutput = false, // Whether to redirect the output (for console applications)
                    RedirectStandardError = false,  // Whether to redirect the error output (for console applications)
                    CreateNoWindow = false    // Whether to create a window for the process
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        public class cIDhttp
        {
            public string id;
            public string text;      // "Reset Video" - Page - Doc
            public string obj;     //  url             nn     url
        }


        internal static class ClipboardFormats
        {
            static readonly string HEADER =
                "Version:0.9\r\n" +
                "StartHTML:{0:0000000000}\r\n" +
                "EndHTML:{1:0000000000}\r\n" +
                "StartFragment:{2:0000000000}\r\n" +
                "EndFragment:{3:0000000000}\r\n";

            static readonly string HTML_START =
                "<html>\r\n" +
                "<body>\r\n" +
                "<!--StartFragment-->";

            static readonly string HTML_END =
                "<!--EndFragment-->\r\n" +
                "</body>\r\n" +
                "</html>";

            public static string ConvertHtmlToClipboardData(string html)
            {
                var encoding = new System.Text.UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
                var data = Array.Empty<byte>();

                var header = encoding.GetBytes(String.Format(HEADER, 0, 1, 2, 3));
                data = data.Concat(header).ToArray();

                var startHtml = data.Length;
                data = data.Concat(encoding.GetBytes(HTML_START)).ToArray();

                var startFragment = data.Length;
                data = data.Concat(encoding.GetBytes(html)).ToArray();

                var endFragment = data.Length;
                data = data.Concat(encoding.GetBytes(HTML_END)).ToArray();

                var endHtml = data.Length;

                var newHeader = encoding.GetBytes(
                    String.Format(HEADER, startHtml, endHtml, startFragment, endFragment));
                if (newHeader.Length != startHtml)
                {
                    throw new InvalidOperationException(nameof(ConvertHtmlToClipboardData));
                }

                Array.Copy(newHeader, data, length: startHtml);
                return encoding.GetString(data);
            }
        }

        public static bool HasWiFiDirect(ref string s)
        {
            int i = s.IndexOf("Wi-Fi Direct");
            return (i >= 0);
        }
        public static void CopyHTML(string html)
        {
            var dataObject = new DataObject();
            string s = ClipboardFormats.ConvertHtmlToClipboardData(html);
            Clipboard.SetData(DataFormats.Html, s);
        }

        public static string ClipboardGetText()
        {
            string t = Clipboard.GetText();
            if (t == null) t = "";
            return t.Trim();
        }
        public static string ShowPageInBrowser(string strType, string strTemp)
        {
            string sPP = Properties.Settings.Default.sPPrefix;
            strTemp = strTemp.Replace(Environment.NewLine, "<br>");
            if (sPP != "init" && strType != "" && Utils.sPrinterTypes.Contains(strType + " "))
            {
                strTemp = "<br>" + Properties.Settings.Default.sPPrefix + "<br><br>" + strTemp +
                    "<br><br>" + Properties.Settings.Default.sMSuffix;
            }
            else 
            if(strType != "")
                strTemp += "<br><br>" + Properties.Settings.Default.NotPrnSuffix;
            // <br> causes problems with justification need to keep br except in justificaton
            //strTemp = strTemp.Replace("<br>",Environment.NewLine);
            ShellHTML(strTemp, false);
            return strTemp;
        }

        public static string FormNumList(string sIn)
        {
            string[] sSin = sIn.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            string sOut = "<table border='2'><tr><td><ol><br>";
            foreach (string s in sSin)
            {
                sOut += "<li>" + s + "</li>";
            }
            sOut += "</ol></td></tr></table>";
            return sOut;
        }

        public static string JustifiedText(string t)
        {
            string s = t.Replace("<br>", " ");
            s = s.Replace(Environment.NewLine, " ");
            s = s.Replace("  ", " ");
            return "<div style=\"text-align: justify;\">" + s + "</div>";
        }

        public static string sHTMLcolors = "<table border='1'><tr><td>Black</td><td><font color='#000000'>ForeGround</font></td><td><font color='#000000'><b>ForeGroundBold</b></font></td><td><span style='background-color: #000000; color: black;'>Background</span></td><td><span style='background-color: #000000; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#000000'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>White</td><td><font color='#FFFFFF'>ForeGround</font></td><td><font color='#FFFFFF'><b>ForeGroundBold</b></font></td><td><span style='background-color: #FFFFFF; color: black;'>Background</span></td><td><span style='background-color: #FFFFFF; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#FFFFFF'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Red</td><td><font color='#FF0000'>ForeGround</font></td><td><font color='#FF0000'><b>ForeGroundBold</b></font></td><td><span style='background-color: #FF0000; color: black;'>Background</span></td><td><span style='background-color: #FF0000; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#FF0000'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Green</td><td><font color='#008000'>ForeGround</font></td><td><font color='#008000'><b>ForeGroundBold</b></font></td><td><span style='background-color: #008000; color: black;'>Background</span></td><td><span style='background-color: #008000; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#008000'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Blue</td><td><font color='#0000FF'>ForeGround</font></td><td><font color='#0000FF'><b>ForeGroundBold</b></font></td><td><span style='background-color: #0000FF; color: black;'>Background</span></td><td><span style='background-color: #0000FF; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#0000FF'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Yellow</td><td><font color='#FFFF00'>ForeGround</font></td><td><font color='#FFFF00'><b>ForeGroundBold</b></font></td><td><span style='background-color: #FFFF00; color: black;'>Background</span></td><td><span style='background-color: #FFFF00; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#FFFF00'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Cyan</td><td><font color='#00FFFF'>ForeGround</font></td><td><font color='#00FFFF'><b>ForeGroundBold</b></font></td><td><span style='background-color: #00FFFF; color: black;'>Background</span></td><td><span style='background-color: #00FFFF; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#00FFFF'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Magenta</td><td><font color='#FF00FF'>ForeGround</font></td><td><font color='#FF00FF'><b>ForeGroundBold</b></font></td><td><span style='background-color: #FF00FF; color: black;'>Background</span></td><td><span style='background-color: #FF00FF; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#FF00FF'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Silver</td><td><font color='#C0C0C0'>ForeGround</font></td><td><font color='#C0C0C0'><b>ForeGroundBold</b></font></td><td><span style='background-color: #C0C0C0; color: black;'>Background</span></td><td><span style='background-color: #C0C0C0; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#C0C0C0'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Gray</td><td><font color='#808080'>ForeGround</font></td><td><font color='#808080'><b>ForeGroundBold</b></font></td><td><span style='background-color: #808080; color: black;'>Background</span></td><td><span style='background-color: #808080; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#808080'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Maroon</td><td><font color='#800000'>ForeGround</font></td><td><font color='#800000'><b>ForeGroundBold</b></font></td><td><span style='background-color: #800000; color: black;'>Background</span></td><td><span style='background-color: #800000; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#800000'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Olive</td><td><font color='#808000'>ForeGround</font></td><td><font color='#808000'><b>ForeGroundBold</b></font></td><td><span style='background-color: #808000; color: black;'>Background</span></td><td><span style='background-color: #808000; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#808000'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Purple</td><td><font color='#800080'>ForeGround</font></td><td><font color='#800080'><b>ForeGroundBold</b></font></td><td><span style='background-color: #800080; color: black;'>Background</span></td><td><span style='background-color: #800080; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#800080'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Teal</td><td><font color='#008080'>ForeGround</font></td><td><font color='#008080'><b>ForeGroundBold</b></font></td><td><span style='background-color: #008080; color: black;'>Background</span></td><td><span style='background-color: #008080; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#008080'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Navy</td><td><font color='#000080'>ForeGround</font></td><td><font color='#000080'><b>ForeGroundBold</b></font></td><td><span style='background-color: #000080; color: black;'>Background</span></td><td><span style='background-color: #000080; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#000080'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Orange</td><td><font color='#FFA500'>ForeGround</font></td><td><font color='#FFA500'><b>ForeGroundBold</b></font></td><td><span style='background-color: #FFA500; color: black;'>Background</span></td><td><span style='background-color: #FFA500; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#FFA500'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Aquamarine</td><td><font color='#7FFFD4'>ForeGround</font></td><td><font color='#7FFFD4'><b>ForeGroundBold</b></font></td><td><span style='background-color: #7FFFD4; color: black;'>Background</span></td><td><span style='background-color: #7FFFD4; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#7FFFD4'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Turquoise</td><td><font color='#40E0D0'>ForeGround</font></td><td><font color='#40E0D0'><b>ForeGroundBold</b></font></td><td><span style='background-color: #40E0D0; color: black;'>Background</span></td><td><span style='background-color: #40E0D0; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#40E0D0'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Lime</td><td><font color='#00FF00'>ForeGround</font></td><td><font color='#00FF00'><b>ForeGroundBold</b></font></td><td><span style='background-color: #00FF00; color: black;'>Background</span></td><td><span style='background-color: #00FF00; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#00FF00'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Fuchsia</td><td><font color='#FF00FF'>ForeGround</font></td><td><font color='#FF00FF'><b>ForeGroundBold</b></font></td><td><span style='background-color: #FF00FF; color: black;'>Background</span></td><td><span style='background-color: #FF00FF; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#FF00FF'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Indigo</td><td><font color='#4B0082'>ForeGround</font></td><td><font color='#4B0082'><b>ForeGroundBold</b></font></td><td><span style='background-color: #4B0082; color: black;'>Background</span></td><td><span style='background-color: #4B0082; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#4B0082'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Violet</td><td><font color='#EE82EE'>ForeGround</font></td><td><font color='#EE82EE'><b>ForeGroundBold</b></font></td><td><span style='background-color: #EE82EE; color: black;'>Background</span></td><td><span style='background-color: #EE82EE; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#EE82EE'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Pink</td><td><font color='#FFC0CB'>ForeGround</font></td><td><font color='#FFC0CB'><b>ForeGroundBold</b></font></td><td><span style='background-color: #FFC0CB; color: black;'>Background</span></td><td><span style='background-color: #FFC0CB; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#FFC0CB'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Peach</td><td><font color='#FFDAB9'>ForeGround</font></td><td><font color='#FFDAB9'><b>ForeGroundBold</b></font></td><td><span style='background-color: #FFDAB9; color: black;'>Background</span></td><td><span style='background-color: #FFDAB9; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#FFDAB9'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Beige</td><td><font color='#F5F5DC'>ForeGround</font></td><td><font color='#F5F5DC'><b>ForeGroundBold</b></font></td><td><span style='background-color: #F5F5DC; color: black;'>Background</span></td><td><span style='background-color: #F5F5DC; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#F5F5DC'&gt;ForeGround&lt;/font&gt;</td></tr></table>";

        // make sure no newline or <br> in any style paragraph
        public static string JustSpan(string s)
        {
            string sSa = "<div style=";
            string sSb = "</div>";
            string sOut = "";

            int j;
            int i = s.IndexOf(sSa);
            while (i >= 0)
            {
                sOut += s.Substring(0, i).Replace(Environment.NewLine, "<br>");
                j = s.IndexOf(sSb, i + sSa.Length);
                string sSpan = s.Substring(i, sSb.Length + j - i);
                sSpan = sSpan.Replace("<br>", " ");
                sSpan = sSpan.Replace(Environment.NewLine, " ");
                sOut += sSpan;
                s = s.Substring(j + sSb.Length);
                i = s.IndexOf(sSa);
            }
            sOut += s.Replace(Environment.NewLine, "<br>");
            return sOut;
        }
        public static string ShowRawBrowser(string s, string strType)
        {
            //string s = JustSpan(t);
            if (s == "") return "";
            string sOut = s;
            string sPP = Properties.Settings.Default.sPPrefix.Replace(Environment.NewLine, " ");
            string sPS = Properties.Settings.Default.NotPrnSuffix.Replace(Environment.NewLine, " ");
            string sMS = Properties.Settings.Default.sMSuffix.Replace(Environment.NewLine, " ");
            if (sPP != "init" && strType != "" && Utils.sPrinterTypes.Contains(strType + " "))
            {
                if (Properties.Settings.Default.bUsePrefix && Properties.Settings.Default.bUsePrefix)
                {
                    sOut = sPP + "<br><br>" + s + "<br><br>" + sMS;
                }
                else if (Properties.Settings.Default.bUsePrefix)
                {
                    sOut = sPP + "<br><br>" + s;
                }
                else if (Properties.Settings.Default.bUseSuffix)
                {
                    sOut = s + "<br><br>" + sMS;
                }
                else sOut = s;
            }
            else
            {
                if (strType != "")
                    sOut = s + "<br><br>" + sPS;
                else sOut = s;
            }
            ShellHTML(sOut, false);
            return sOut;
        }

        public static int SyntaxTest(string s)
        {
            string strErr = Utils.BBCparse(s);
            if (strErr == "") return 0;
            DialogResult Res1 = MessageBox.Show(strErr, "Click YES to see errors or NO to ignore", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
            if (Res1 == DialogResult.Yes)
            {
                ShowParseLocationErrors(s);
                MessageBox.Show(strErr, "Errors are near locations listed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
            if (Res1 == DialogResult.No)
            {
                return 2;
            }
            return 0;
        }

        public static string[] RequiredMacrosRF = { "PC AIO LAPTOP support documents", "Printer support documents" };//, "HP-KB-WIKI"};

        public static string GetHelpFile(string sHelp)
        {
            string[] strForms = { "FILE", "SIG", "EDIT",  "EDITLINK",
                "MANAGE","XMLERRORS", "UTILS", "SEARCH", "WEB" ,"NEW-PRINTER"};
            string[] strFiles = { "mnu-file.docx","mnu-imag-sig.docx","mnu-main-edit.docx", "mnu-edit-link.docx",
                "mnu-manage-img.docx", "mnu-paste-sig.docx", "mnu-util.docx", "mnu-word-search.docx" ,
                "mnu-web-search.docx","mnu-new-printer.docx"};
            int n = 0;
            foreach (string s in strForms)
            {
                if (s == sHelp)
                {

                    return strFiles[n]; ;
                }
                n++;
            }
            return "";
        }

        public static void WordpadEdit(string sHelp)
        {
            string sFilePath = WhereExe + "/" + GetHelpFile(sHelp);
            //Process.Start("wordpad.exe",  sFilePath);
            Process.Start(sFilePath);
        }

        public static int HasSupSig(ref string s, ref int i, ref int j)
        {
            if (s == null) return 0;
            if (s == "") return 0;
            i = s.IndexOf(SupSigPrefix);
            j = s.IndexOf(SupSigSuffix);
            if (i > 0 && j > 0 && i < j) return 1;
            return 0;
        }

        public static string FNtoHeader(string strFN)
        {
            int i = 0;
            foreach (string s in LocalMacroPrefix)
            {
                if(s ==  strFN)
                {
                    return LocalMacroFullname[i];
                }
                i++;
            }
            return "HTML (uneditable)";
        }
        public static void ShowParseLocationErrors(string strText)
        {
            string strLoc = WhereExe +  "\\MyHtmlErr.txt";
            File.WriteAllText(strLoc, strText);
            Utils.NotepadViewer(strLoc);
        }


        public static string FNtoPath(string strFN)
        {
            return WhereExe + "/" + FNtoName(strFN);
        }

        public static string FNtoName(string strFN)
        {
            string l = strFN.ToLower();
            if (l == "si") return "Signatures.txt";
            if (l == "ass") return "Associate.txt";
            if (l == "all") return "AllowedSpelling.txt";
            return strFN + "macros.txt";
        }

        public static bool NoFileThere(string strFN)
        {
            string strPath = FNtoPath(strFN);
            if (File.Exists(strPath)) return false;
            return true;
        }

        public static string RemoveNL(string text)
        {
            string strRtn = text.Replace(Environment.NewLine, "<br>");
            return strRtn.Replace("\n", "<br>").Trim();
        }

        public static void WriteAllText(string strLoc, string strData)
        {
            File.WriteAllText(strLoc, NoTrailingNL(strData));
        }

        public static void NotepadViewer(string strFile)
        {
            if (strFile == "") return;
            Process.Start("C:\\Windows\\Notepad.exe", strFile);
        }
        // BBCODE parse for bad tags
        public static string BBCparse(string strIn)
        {
            string strRtn = "";
            HtmlDocument htmlDoc = new HtmlDocument();
            if (strIn == null) return "";
            htmlDoc.LoadHtml(strIn + " ");  // seems needed to catch trailing open tag
            foreach(var strErr in htmlDoc.ParseErrors)
            {
                string strLine = " line:" + strErr.Line.ToString() + ", char:" + strErr.LinePosition.ToString();
                strRtn += strErr.Reason + strLine + Environment.NewLine;
            }
            int i = strIn.IndexOf("...");
            if(i >= 0)
            {
                strRtn += "possible '...' problem at " + i.ToString() + Environment.NewLine;
            }
            return strRtn;
        }

        public static string FormUrl(string strUrl, string strIn)
        {
            if (strIn == "") strIn = strUrl;
            return "<a href=\"" + strUrl + "\" target=\"_blank\">" + strIn + "</a>";
        }


        public static int FirstDifferenceIndex(string str1, string str2)
        {
            if (str1 == null) return 0;
            if (str2 == null) return 0;
            int minLength = Math.Min(str1.Length, str2.Length);
            for (int i = 0; i < minLength; i++)
            {
                if (str1[i] != str2[i])
                {
                    return i + 1;
                }
            }

            // If all characters up to the length of the shorter string are the same
            if (str1.Length != str2.Length)
            {
                return 0;
            }

            // If the strings are identical, return -1
            return -1;
        }


        public const string AssignedImageName = "LOCALIMAGEFILE"; // PRN and PC suffix 
        public enum eBrowserType
        {
            eEdge = 0,
            eChrome = 1,
            eFirefox = 2
        }

        public static bool GetPixSize(string strName, ref int iHeight, ref int iWidth)
        {
            string strPath = WhereExe + "\\" + strName;
            try
            {
                using (var image = new Bitmap(strPath))
                {
                    iWidth = image.Width;
                    iHeight = image.Height;
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string AssembleImage(string strUrl, int Height, int Width, bool bBox)
        {
            string sBox = bBox ? "border=\"1\"" : "";


            if (strUrl.Contains("image-id"))
            {
                return "<img " + sBox + "src=\"" + strUrl.Trim() + "\">";
            }


            return "<img " + sBox + "src=\"" + strUrl + "\"  height=\"" + Height.ToString() + "\" width=\"" + Width.ToString() + "\">";
        }

        public static string AssembleImage(string strUrl, string sSizeID, bool bBox)
        {
            if (sSizeID == "default") return AssembleIMG(strUrl, bBox);
            return "<img src=\"" + strUrl.Trim() +"/image-size/" + sSizeID + "\">";
        }

        public static string AssembleIMG(string strURL, bool bBox)
        {
            string sBox = bBox ? "border=\"1\"" : "";
            return "<img " + sBox + "src=\"" + strURL.Trim() + "\">";
        }
        public static eBrowserType BrowserWanted = eBrowserType.eEdge;
        public static string VolunteerUserID = "";
        public static void LocalBrowser(string strUrl)
        {
            switch (BrowserWanted)
            {
                case Utils.eBrowserType.eFirefox:
                    Process.Start("firefox.exe", "-new-window " + strUrl);
                    break;
                case Utils.eBrowserType.eEdge:
                    Process.Start("microsoft-edge:" + strUrl);
                    break;
                case Utils.eBrowserType.eChrome:
                    //Process.Start("chrome.exe", "--allow-running-insecure-content  " + strUrl);
                    Process.Start("chrome.exe", strUrl);
                    break;
            }
        }
        public static void RunFirefox(string strUrl)
        {
                    Process.Start("firefox.exe", "-new-window " + strUrl);
        }
        public static void ShowMyPhotoAlbum()
        {
            string UserID = Utils.VolunteerUserID;
            string PhotoGallery;
            if (UserID == "") PhotoGallery = "https://h30434.www3.hp.com/t5/user/myprofilepage/tab/personal-profile:personal-info";
            else PhotoGallery = "https://h30434.www3.hp.com/t5/media/gallerypage/user-id/" + UserID + "/tab/albums";
            LocalBrowser(PhotoGallery);
        }

        public static int CountSetBits(int n)
        {
            int count = 0;
            while (n != 0)
            {
                count += n & 1;
                n >>= 1;
            }
            return count;
        }
        private static string ColorToHtml(Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }
        //<span style='background-color: #000000; color: white;'>ForeGroundBold</span>
        public static string ApplyColors1(ref TextBox tb, bool bPreserveSpaces, bool bBox)
        {
            string tbText = bPreserveSpaces ? tb.Text.Replace(" ", "&nbs;") :  tb.Text;
            if (bBox)
            {
                tbText = "<table border='1'><tr><td>" + tbText + "</td></tr></table>";
            }
            Graphics graphics = tb.CreateGraphics();
            int px = (int)Math.Round(tb.Font.SizeInPoints * graphics.DpiY / 72);
            string s = "<span style=\"color: " + tb.ForeColor.Name + "; background-color: " + tb.BackColor.Name + "; ";

            int i = (int)tb.Font.Style;   // enums 1 and 2 are now 3 for bold italix
            if(i == 3)
            {
                s += "font-style: italic; ";
                s += "font-weight: bold; ";
            }
            if (tb.Font.Style == FontStyle.Italic) s += "font-style: italic; ";
            if (tb.Font.Style == FontStyle.Bold) s += "font-weight: bold; ";
            s += "font-size: " + px.ToString() + "px; font-family: '" + tb.Font.Name + "'\"; >";
            s += tbText + "</span>";
            return s;
        }
        public static string ApplyColors(ref TextBox tb)
        {
            string sNBI = "";
            string s = tb.Text;
            string sCol = "";
            string sFs = "";
            string sFe = "";
            string sS, sE;
            string sFONTs = "";
            float fontSize = tb.Font.Size - 5;  // html seems to need -5 to scale good
            int iSize = (int)Math.Ceiling(fontSize);
            sFONTs = fontSize.ToString("0.00");
            if (tb.Font.Style == FontStyle.Bold) sNBI = "b";
            if (tb.Font.Style == FontStyle.Italic) sNBI = "i";

            int i = (int)tb.Font.Style;   // enums 1 and 2 are now 3 for bold italix
            Color color = Color.FromArgb(tb.ForeColor.ToArgb());
            sCol = ColorToHtml(color);
            if (i == 3)
            {
                sS = "<b><i>";
                sE = "</b></i>";
            }
            else
            {
                sS = (sNBI == "") ? "" : "<" + sNBI + ">";
                sE = (sNBI == "") ? "" : "</" + sNBI + ">";
            }
            if (sCol != "#000000")
            {
                sFs = "<font color=\"" + sCol + "\" size=\"" + sFONTs + "\">";
                sFe = "</font>";
            }
            else
            {
                sFs = "<font size=\"" + sFONTs + "\">";
                sFe = "</font>";
            }
            return sS + sFs + s + sFe + sE;
        }

        public static string Form1CellTable(string strIn, string sWidth)
        {
            if (sWidth == "")
            {
                return "<table border=\"1\"><tr><td>" + strIn + "</td></tr></table>";
            }
            return "<table border=\"1\" width=\"" + sWidth + "%\"><tr><td>" + strIn + "</td></tr></table>";
        }

        // this puts a newline in the table to make it easier to read the text and copy it
        // the <p> does not work at the HP forum and a double newline is needed
        public static string Form1CellTableP(string strIn, string sWidth)
        {
            if (sWidth == "")
            {
                return "<table border=\"1\"><tr><td><br>&nbsp;" + strIn + "&nbsp;<br><br></td></tr></table>";
            }
            return "<table border=\"1\" width=\"" +sWidth+ "%\"><tr><td><br>&nbsp;" + strIn + "&nbsp;<br><br></td></tr></table>";
        }
        public static void PurgeLocalImages(string strType,  string WhereExe)
        {
            var dir = new DirectoryInfo(WhereExe);
            foreach (var file in dir.EnumerateFiles(AssignedImageName + "-" + strType +"-*.png"))
            {
                file.Delete();
            }
        }

        public static int CountImages()
        {
            string[] files = Directory.GetFiles(WhereExe, "*.png");
            return files.Length;
        }
        public class CLocalFiles
        {
            public bool NotUsed { get; set; }
            public string Name { get; set; }
        }

        public static string GetNextImageFile(string strType, string strExe)
        {
            int i = 0;
            string strName, strPath;
            while(true)
            {
                strName = AssignedImageName + "-" + strType + "-" + i.ToString() + ".png";
                strPath = strExe + "/" + strName;
                if(File.Exists(strPath))
                {
                    i++;
                    if(i > 90)
                    {
                        MessageBox.Show("ERROR: over 90 images in " + strExe + "\r\nSave what you can as I am purging");
                        Process.Start(strExe);
                        PurgeLocalImages(strType, strExe);
                    }
                    continue;
                }
                return strName;
            }
        }

        // paths returned from file explorer have quotes
        public static string RemoveOuterQuotes(string strIn)
        {
            string strTmp = strIn.Trim();
            if (strTmp.Length == 0) return "";
            if (strTmp.Substring(0, 1) == "\"")
            {
                strTmp = strTmp.Replace("\"", "");
            }
            return strTmp.Trim();
        }

        /*
    // ext = "*.bmp;*.dib;*.rle"           descr = BMP
    // ext = "*.jpg;*.jpeg;*.jpe;*.jfif"   descr = JPEG
    // ext = "*.gif"                       descr = GIF
    // ext = "*.tif;*.tiff"                descr = TIFF
    // ext = "*.png"                       descr = PNG
    */

        private static string[] ImgExt = new string[11]
{".bmp",".dib",".rle",".jpg",".jpeg",".jpe",".jfif",".gif",".tif",".tiff",".png" };

        public static bool bHasImgMarkup(string s)
        {
            return s.Contains("<img ");
        }
        public static bool IsUrlImage(string aLCase)
        {
            if (aLCase.Contains("image/serverpage"))
                return true; // must be from HP server
            foreach (string aImg in ImgExt)
            {
                if (aLCase.Contains(aImg)) return true;
            }
            return false;
        }

        // all images at HP must be on the server or approved by server
        public static bool IsPostableImage(string a)
        {
            string aLCase = a.ToLower();

            if (a.Contains(AssignedImageName)) return false;

            foreach (string aImg in ImgExt)
            {
                if (aLCase.Contains(aImg))
                {
                    return false;
                }
            }
            return true; ;
        }

        public static bool bFromGallery(string sLCase)
        {
            //if (sLCase.Contains("media/gallerypage/image-id")) return true; // not an image yet !!
            if (sLCase.Contains("image/serverpage")) return true;
            return false;
        }

        public static int AnyImageIndex(string s, int n)
        {
            int i = -1;
            foreach (string aImg in ImgExt)
            {
                i = s.IndexOf(aImg, n);
                if(i >= 0) return i + aImg.Length;
            }
            return i;
        }


        public static bool IsUrlVideo(string aLCase)
        {
            string[] VidExt = {".mp4",".avi",".mov",".wmv",".flv",".mkv",".webm",".mpeg",".mpg",".3gp",
                ".m4v",".vob",".ts",".rm",".rmv",".ogv"};
            if (aLCase.Contains("www.youtube.com/embed/")) return true;
            foreach (string aImg in VidExt)
            {
                if (aLCase.Contains(aImg)) return true;
            }
            return false;
        }

        public static bool bIsHTTP(string strIN)
        {
            string strUC = strIN.ToUpper();
            return (strUC.Contains("HTTPS:") || strUC.Contains("HTTP:"));
        }

        public static string strFill(int i, int n)
        {
            string strOut = "";
            int k = i % 52;
            for (int j = 0; j < n; j++)
                strOut += AllAlphas.Substring(k, 1); 
            return strOut;
        }

        // scripting into array r is row c is column sizes
        public static string strFillSubscript(int r, int c, int n)
        {
            int row = n / c;
            int col = n % c;
            int k = n % 52;
            string s ="R"+row.ToString();
            s += "C" + col.ToString() + "_";
            for (int j = 0; j < 4; j++)
                s += AllAlphas.Substring(k, 1);
            return s;
        }

        /*
         *
      x  https://www.amazon.com/s?k=wd+blue+sn570&hvadid=604496098449&hvdev=c&hvlocphy=9028097&hvnetw=g&hvqmt=e&hvrand=16170472703128286580&hvtargid=kwd-1440214831857&hydadcr=24329_13517663&tag=googhydr-20&ref=pd_sl_9paam7inoz_e
        https://www.amazon.com/Western-Digital-SN580-Internal-Solid/dp/B0C8XMH264/ref=asc_df_B0C8XMH264?tag=bingshoppinga-20&linkCode=df0&hvadid=80401975313450&hvnetw=o&hvqmt=e&hvbmt=be&hvdev=c&hvlocint=&hvlocphy=&hvtargid=pla-4584001439821208&th=1
        https://www.officedepot.com/a/products/6789792/Western-Digital-BLUE-SN570-Internal-NVMe/?mediacampaignid=71700000100485049_370762392&gclid=138b98ec20b212f5975afeedf0922222&gclsrc=3p.ds&msclkid=138b98ec20b212f5975afeedf0922222
        https://www.bhphotovideo.com/c/product/1673383-REG/seagate_st4000dm004_barracuda_4tb_3_5_5400.html?ap=y&smp=y&msclkid=c6b55c6469861e7e5dfd3a65b9749523
      x  https://www.seagate.com/products/nas-drives/ironwolf-pro-hard-drive/?sku=ST10000NTZ01&utm_campaign=nas-2024-shopping-global&utm_medium=sem&utm_source=google-shopping&utm_product=ironwolf-nas&utm_use_case=general&prodSrc=ironwolf-nas&use_case=general&gad_source=1&gclid=Cj0KCQjwwYSwBhDcARIsAOyL0fjBfMIzPXARUADRZgnhTNWFs4SOeDyf_vEmpeg1pimtKttTI1JuE_0aAqQ_EALw_wcB
        https://www.newegg.com/crucial-2tb-t500/p/20-156-389?Item=20-156-389
        https://www.westerndigital.com/products/internal-drives/wd-blue-sn580-nvme-ssd?cjdata=MXxOfDB8WXww&cjevent=e9592a2deadf11ee806c35760a1cb827&utm_medium=afl1&utm_source=cj&utm_content=Western+Digital+Clearance,+Canada&cp1=100357191&utm_campaign=ca-clearance&utm_term=02-03-2022&cp2=Microsoft+Shopping+(Bing+Rebates,+Coupons,+etc.)&sku=WDS250G3B0E
         */

        // can stop at ?
        private static string[] QListVendors = {
            ".westerndigital.",
            ".officedepot.",
            ".bhphotovideo.",
            ".amazon.", // but  not s? so put after the ?& test
            ".newegg."
        };
        private static string QVendor(string sUrl)
        {
            foreach(string s in QListVendors)
            {
                int i = sUrl.IndexOf(s);
                if (i > 0)
                {
                    int j = s.IndexOf('?');
                    if (j < 0) return sUrl;
                    return sUrl.Substring(0, j);    
                }
            }
            return "";
        }

        public static void SwapNL(ref TextBox tb)
        {
            if(tb.Text.Length > 0)
            {
                if (tb.Text.Contains("<br>"))
                    tb.Text = tb.Text.Replace("<br>", Environment.NewLine);
                else tb.Text = tb.Text.Replace(Environment.NewLine, "<br>");                   
            }
        }

        //insert a horizontal line
        public static void InsertHR(ref TextBox tbEdit)
        {
            string s1, s2;
            int i = tbEdit.SelectionStart;
            int j = tbEdit.SelectionLength;
            if (j != 0) return;
            int n = tbEdit.Text.Length;
            s1 = tbEdit.Text.Substring(0, i);
            s2 = tbEdit.Text.Substring(i);
            s1 += "<br><hr><br>" + s2;
            tbEdit.Text = s1;
            tbEdit.SelectionStart = i;
            tbEdit.SelectionLength = 0;
            tbEdit.Focus();
        }

        // add or remove bold text
        public static void AddBold(ref TextBox tbEdit)
        {
            string s1, s2, s3;
            int i = tbEdit.SelectionStart;
            int j = tbEdit.SelectionLength;
            if (j == 0) return;
            int n = tbEdit.Text.Length;
            s1 = tbEdit.Text.Substring(0, i);
            s2 = tbEdit.Text.Substring(i, j);
            s3 = tbEdit.Text.Substring(i + j);
            // user may have selected several bold so let them all be undone
            if (s2.Contains("<b>"))
            {
                s2 = s2.Replace("<b>", "");
                s2 = s2.Replace("</b>", "");
                tbEdit.Text = s1 + s2 + s3;
                tbEdit.SelectionStart = i;
                tbEdit.SelectionLength = j - (n - tbEdit.Text.Length);
            }
            else
            {
                s1 += "<b>";
                s2 += "</b>";
                tbEdit.Text = s1 + s2 + s3;
                tbEdit.SelectionStart = i;
                tbEdit.SelectionLength = j + 7;
            }
            tbEdit.Focus();
        }

        // add or remove bold text
        public static void RemoveSelectedNL(ref TextBox tbEdit)
        {
            string s1, s2, s3;
            int i = tbEdit.SelectionStart;
            int j = tbEdit.SelectionLength;
            if (j == 0) return;
            int n = tbEdit.Text.Length;
            s1 = tbEdit.Text.Substring(0, i);
            s2 = tbEdit.Text.Substring(i, j);
            s3 = tbEdit.Text.Substring(i + j);
            // user may have selected several bold so let them all be undone
            s2 = s2.Replace(Environment.NewLine, "");
            tbEdit.Text = s1 + s2 + s3;
            tbEdit.Focus();
        }


        public static void AddColor(ref TextBox tbEdit, string sColor)
        {
            string s1, s2, s3;
            int i = tbEdit.SelectionStart;
            int j = tbEdit.SelectionLength;
            if (j == 0) return;
            s1 = tbEdit.Text.Substring(0, i);
            s2 = tbEdit.Text.Substring(i, j);
            s3 = tbEdit.Text.Substring(i + j);
            if (s2.Contains("<")) return; // not going to restore any previous FUs
            string s = "<font color=\"" + sColor + "\">";
            int n = s.Length;
            s1 += s;
            s = "</font>";
            n += s.Length;
            s2 += s;
            tbEdit.Text = s1 + s2 + s3;
            tbEdit.SelectionStart = i;
            tbEdit.SelectionLength = j + n;
            tbEdit.Focus();
        }

        public static bool IsValidHtmlColor(string color)
        {
            string pattern = @"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";
            return Regex.IsMatch(color, pattern);
        }

        //insert newlines into textbox
        public static void AddNL(ref TextBox tbEdit, int n)
        {
            int i = tbEdit.SelectionStart;
            int j = tbEdit.Text.Length;
            if (i == 0)
            {
                if (j > 0)
                {
                    if (!tbEdit.Focused)
                    {
                        i = j;
                        tbEdit.Focus();
                    }
                }
            }
            tbEdit.Text = tbEdit.Text.Insert(i, "<br><br>".Substring(0, n * 4)); ;
            i += 4 * n;
            tbEdit.SelectionStart = i;
            tbEdit.SelectionLength = 0;
            tbEdit.Focus();
        }

        public static string FormTable(int rows, int cols, bool bFill, int iSize)
        {
            if (rows == 0 && cols == 0) return "";
            if (rows == 0) rows = 1;
            if (cols == 0) cols = 1;
            int jChar = 0;
            string r = rows > 9 ? "00" : "0";
            string c = cols > 9 ? "00" : "0";
            string UseBorder = iSize == 0 ? "<table>" : "<table border='" + iSize.ToString() + "' width=\"50%\">";


            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append(UseBorder);

            for (int i = 0; i < rows; i++)
            {
                htmlBuilder.Append("<tr>");
                for (int j = 0; j < cols; j++)
                {
                    string s = "R" + i.ToString(r) + "C" + j.ToString(c) + "_" + strFill(jChar, 4);
                    htmlBuilder.Append("<td>");
                    if (bFill) htmlBuilder.Append(s);
                    else htmlBuilder.Append("    ");
                    htmlBuilder.Append("</td>");
                    jChar++;
                }

                htmlBuilder.Append("</tr>");
            }
            htmlBuilder.Append("</table>");
            return htmlBuilder.ToString();
        }
        /*
         * https://www.google.com/search?q=hp+928511-001&sca_e  https://www.bing.com/search?q=sn570&qs=n
         * https://www.amazon.com/s?k=sn570&crid=3U  https://www.aliexpress.us/w/wholesale-sn570.html?spm=a2g0o.home.search.0
         * https://www.google.com/search?client=firefox-b-1-d&q=hp+50334-601  https://www.newegg.com/p/pl?d=wd+blue+sn570
         * https://www.amazon.com/Ediloca-Internal-Compatible-Ultrabooks-Computers/dp/B0B7QYZF9X/ref=sr_1
         * ? before & is used by amazon, wd, seagate, newegg but not google with that firefox
         */
        private static string dStr(string strIn, string strRef)
        {
            int i = strIn.IndexOf(strRef);
            return (i < 0) ? strIn.Trim() : strIn.Substring(0,i);
        }

        public static string dRef(string sUrl)
        {
            bool bWritten = false;
            string s = eRef(sUrl, ref bWritten);
            int n = s.Length;
            if (!bWritten && n > nLongestExpectedURL)
            {
                RecordLongUrl(sUrl);
            }
            return s;
        }

        private static void RecordLongUrl(string sUrl)
        {
            if (System.Diagnostics.Debugger.IsAttached || bRecordUnscrubbedURLs)
            {
                // keep track of which urls cannot be untracked of de-referenced
                using (StreamWriter writer = File.AppendText(WhereExe + "\\UrlDebug.txt"))
                {
                    writer.WriteLine(sUrl); // we are allowing newlines here
                }
            }
        }
        // clean url
        private static string eRef(string sUrl, ref bool bWritten)
        {
            int i,j;
            sUrl = dStr(sUrl,"/ref");
            string surl = sUrl.ToLower();
            if (surl.Contains(".youtube") || surl.Contains("support.hp.com"))return sUrl;

            i = surl.IndexOf("search?");
            if(i > 0)
            {
                j = sUrl.IndexOf("q=", i + 7);
                if (j < 0) return sUrl;
                i = sUrl.IndexOf('&', j);
                if(i < 0) return sUrl;
                return sUrl.Substring(0, i);
            }


            /*
                "https://www.ebay.com/sch/i.html?_nkw=" + "HP " + str + "&_sacat=58058"
                https://www.ebay.com/sch/i.html?_from=R40&_trksid=p2334524.m570.l1313&_nkw=hp+15-eb+bios+chip&_sacat=0&_odkw=hp+15-eb+bios+chip&_osacat=0
                https://www.ebay.com/sch/i.html?_nkw=hp+15-eb+bios+chip&_sacat=0
             */
            if (surl.Contains(".ebay"))
            {
                string sExpectURL = "https://www.ebay.com/sch/i.html?";
                string sLookForA = "&_nkw=";
                string sLookForB = "&_sacat=";
                string strLookup = "";
                i = sUrl.IndexOf(sExpectURL);
                if(i >= 0)
                {
                    i += sExpectURL.Length;
                    i = sUrl.IndexOf(sLookForA,i);
                    if(i != -1)
                    {
                        j = sUrl.IndexOf(sLookForB);
                        if(j != -1)
                        {
                            i += sLookForA.Length;
                            strLookup = sUrl.Substring(i, j - i);
                            string sOut = sExpectURL + "_nkw=" + strLookup + sLookForB + "0";
                            return sOut;
                        }
                    }
                }

            }

            if(surl.Contains(".aliexpress"))
            {
                i = sUrl.IndexOf('?');
                return (i < 0) ? sUrl : sUrl.Substring(0, i);
            }

            i = surl.IndexOf("#:~:text=");
            if (i > 0) return sUrl.Substring(0, i);


            i = surl.IndexOf("?utm_source=");  // gets bing and google
            if (i > 0) return sUrl.Substring(0, i);

            i = surl.IndexOf("?gclid=");  // gets crucial
            if (i > 0) return sUrl.Substring(0, i);

            i = sUrl.IndexOf(".pdf?"); // bing coupon problem
            if (i > 0) return sUrl.Substring(0, i+4);

            i = sUrl.IndexOf('&');
            j = sUrl.IndexOf('?');
            //return (i < 0) ? sUrl: sUrl.Substring(0, i);
            if (j < i) return sUrl.Substring(0, i);
            if (i < 0 && j < 0) return sUrl;    //nothing complicated so just return


            surl = QVendor(sUrl);
            if (surl != "") return surl;

            if (surl.Contains("https://parts.hp.com/hpparts/Default.aspx"))
                return "https://parts.hp.com/hpparts";

            RecordLongUrl(sUrl);
            return sUrl;        
        }

        // could have copied XML instead of HTML
        public static string ChangeBRtoNL(string s)
        {
            string sNL = Environment.NewLine;
            s = s.Replace("<br>", sNL);
            //s = s.Replace("</br>", sNL); // these is error
            s = s.Replace("<br/>", sNL);    // this is error but shows up in some HTML
            s = s.Replace("<br />", sNL);
            return s;
        }

        public static string NoTrailingNL(string s)
        {
            int i = s.Length;
            if (i < 2) return s;
            string t = s.Substring(i - 2);
            if (Environment.NewLine == t)
            {
                t = s.Substring(0, i-2);
                return NoTrailingNL(t);
            }
            t = s.Substring(i - 4);
            if (t == "<br>")
            {
                t = s.Substring(0, i - 4);
                return NoTrailingNL(t);
            }
            return s;
        }

        //If file does not exist then no need for newline on the append
        public static void FileAppendText(string strFN, string text)
        {
            string sFilePath = FNtoPath(strFN);
            bool b = NoFileThere(strFN);
            string strGEnd = (b ? "" : Environment.NewLine) + NoTrailingNL(text);
            using (StreamWriter writer = File.AppendText(sFilePath))
            {
                writer.Write(strGEnd);
                writer.Close();
            }
        }

        public static void ReplaceUrls(ref string sBody, bool MakeHyper)
        {
            int n = 0;
            bool b;
            CMarkup MyMarkup = new CMarkup();
            MyMarkup.Init(MakeHyper);
            while (true)
            {
                b = MyMarkup.FindUrl(n, ref sBody);
                if (!b) break;
                n++;
            }
            while (n > 0)
            {
                n--;
                CMarkup.cFiller cf = MyMarkup.cFillerList[n];
                sBody = sBody.Replace(cf.sFiller, cf.NewUrl);
            }
        }

        public static int LengthURL(ref string sBody, int iStart)
        {
            int n = -1;
            string s = sBody.Substring(iStart);
            foreach (char c in s)
            {
                n++;
                if (c == ' ') return n;
                if (c == '\n') return n;
                if (c == '\r') return n;
                if (c == '\t') return n;
                if (c == '<') return n;
            }
            return s.Length;
        }

    }




    public class CMarkup
    {
        private bool MakeHyper;
        public class cFiller
        {
            public string sFiller;
            public string OldUrl;
            public string NewUrl;
        }
        public List<cFiller> cFillerList;
        private void cReplace(int n, ref string sBody, int iLoc, int iLen)
        {
            cFiller cf = new cFiller();
            cf.OldUrl = sBody.Substring(iLoc, iLen);
            cf.sFiller = Utils.strFill(n, iLen);
            sBody = sBody.Replace(cf.OldUrl, cf.sFiller);
            string strClean = Utils.dRef(cf.OldUrl);
            cf.NewUrl = MakeHyper ? Utils.FormUrl(strClean, "") : strClean;
            cFillerList.Add(cf);
        }

        public void Init(bool bMakeHyper)
        {
            cFillerList = new List<cFiller>();
            MakeHyper = bMakeHyper;
        }


        public bool FindUrl(int nLooked, ref string s)
        {

            int iHTTP = 0, iLen = 0;
            string sTMP = s.ToLower();
            iHTTP = sTMP.IndexOf("http");
            if (iHTTP < 0) return false;
            iLen = Utils.LengthURL(ref s, iHTTP);
            string strFound = s.Substring(iHTTP, iLen);
            cReplace(nLooked, ref s, iHTTP, iLen);
            return true;
        }
    }

    internal class CSendNotepad
    {

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool BringWindowToTop(IntPtr hWnd);

        public void PasteToNotepad(string strText)
        {
            if (strText == "") return;
            // Let's start Notepad
            Process process = new Process();
            process.StartInfo.FileName = "C:\\Windows\\Notepad.exe";
            process.Start();
            Thread.Sleep(2000);
            Clipboard.SetText(strText);
            IntPtr hWnd = process.Handle;
            BringWindowToTop(hWnd);
            SendKeys.SendWait("^V");
        }
        public void PasteToNotepad(string strText, string strFile)
        {
            if (strText == "") return;
            // Let's start Notepad
            Process process = new Process();
            process.StartInfo.FileName = "C:\\Windows\\Notepad.exe";
            process.StartInfo.Arguments = strFile;
            process.Start();
            Thread.Sleep(2000);
            Clipboard.SetText(strText);
            IntPtr hWnd = process.Handle;
            BringWindowToTop(hWnd);
            SendKeys.SendWait("^{end}");
            SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait("^V");
        }
    }
    public class cMacroEach
    {
        public string sName;
        public string sBody;
        public string rBody;
    }



    public class CBody
    {
        public string File;     //PC, PRN, HP  same as strType
        public string Number;   //macro number 1..30 or more
        public string Name;     //macro name
        public string sBody;    //body of marco
        public string rBody;    //data record or none
        public string fKeys;    //keywords found separated by a space
        public int nWdsfKey;    //number of words in fKeys is the number of unique hits
        public bool bHasImages; //if true then have one or more images
    }

    public class dgvStruct
    {
        public int Inx { get; set; }
        public int HP_HTML_DIF_LOC { get; set; }
        public bool MoveM { get; set; }
        public bool HPerr { get; set; }
        public bool HPimage { get; set; }
        public bool HP_HTML_NO_DIFF { get; set; }
        public string MacName { get; set; }
        public string sErr { get; set; }
        public string sBody { get; set; }
        public string rBody { get; set; }
    }
    public class CFound
    {
        public string File { get; set; }
        public string Number { get; set; }
        public string Found { get; set; }    //number of keywords found
        public string Name { get; set; }
        public int WhereFound;
        public int WhichMatch; // bit 0 set = first match, bit 1 set = second, etc
        public bool MayHaveLanguage;
        public bool bWanted; // just want to see this file name one
    }

    public class CNewMac
    {
        public string sName;
        public string sBody;
        public string rBody;
        public void AddNB (string sn, string sb, string rb)
        {
            sName = sn;
            sBody = sb;
            rBody = rb;
        }
    }

    public class cQCmacros
    {
        public int LocInRF;
        public string sType;  // Q for quick watch, C for clipboard context
        public string sName;
        public string sBody;
    }

    public class cnDups
    {
        public string sFN_N;  // filename and number
        public string sUrl; // duplicate uri
    }
    public class cDupHTTP
    {
        public List<cnDups> nDups = new List<cnDups>();
        public List<string> sUrls = new List<string>();
        public int[] nHyper = new int[Utils.iNMacros];
        private List<string> GetHTTP(string sIn)
        {
            string sLC = sIn.ToLower();
            List<string> sOut = new List<string>();
            int i, j, k;
            i = sLC.IndexOf("http");
            if (i == -1) return sOut;
            i--;
            while (i >= 0)
            {
                string s = sLC.Substring(i, 1);    // terminator character quote, double quote, or >
                if (s == ">") s = "<";
                j = sLC.IndexOf(s, i + 1);  // ending terminator
                                            // <br> may note have an ending < so use endof string
                if (j == -1)
                {
                    s = sIn.Substring(i + 1, sLC.Length - i - 1);
                    sOut.Add(s);
                    return sOut;
                }
                k = j - i;
                s = sIn.Substring(i + 1, k - 1);
                sOut.Add(s);
                k = i + k;
                i = sLC.IndexOf("http", k);
                i--;
            }
            return sOut;
        }
        public int AddN(string sFN, string sMN, string sBody)
        {
            List<string> nList = GetHTTP(sBody);
            string t = sFN + "-" + sMN;
            int n = 0;
            foreach (string s in nList)
            {

                if (sUrls.Contains(s))
                {
                    n++;
                    if (nDups.Count == 0)
                    {
                        cnDups cn = new cnDups();
                        cn.sFN_N = t;
                        cn.sUrl = s;
                        nDups.Add(cn);
                    }
                    else
                    {
                        cnDups cn;
                        bool bAdd = true;
                        for (int i = 0; i < nDups.Count; i++)
                        {
                            cn = nDups[i];
                            if (cn.sUrl == s)
                            {
                                cn.sFN_N += " " + t;
                                bAdd = false;
                                break;
                            }
                        }
                        if (bAdd)
                        {
                            cn = new cnDups();
                            cn.sFN_N = t;
                            cn.sUrl = s;
                            nDups.Add(cn);
                        }
                    }
                }
                else sUrls.Add(s);
            }
            return n;
        }
    }


    internal class CSendCloud
    {

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool BringWindowToTop(IntPtr hWnd);
        private Process HPprocess;
        public void Init()
        {
            HPprocess = new Process();
            HPprocess.StartInfo.Verb = "runas";
            HPprocess.StartInfo.FileName ="C:\\Program Files\\WindowsApps\\AD2F1837.HPCloudRecoveryTool_2.7.8.0_x64__v10z8vjag6ke6\\CloudRecovery\\CloudRecovery.exe";
            HPprocess.Start();
        }
        public void PasteToCloud(string strText)
        {

            // Copy the text in the datafield to Clipboard
            Clipboard.SetText(strText);

            // Get the HP cloud Handle
            IntPtr hWnd = HPprocess.Handle;
            

            // Activate the Notepad Window
            BringWindowToTop(hWnd);

            // Use SendKeys to Paste
            SendKeys.Send("^V");
        }

        public class FileUtilities
        {
            public static DateTime? GetMostRecentFileLastWriteTime(string folderPath, string sFiles)
            {
                // Ensure the folder exists
                if (!Directory.Exists(folderPath))
                {
                    throw new DirectoryNotFoundException($"The directory '{folderPath}' does not exist.");
                }

                // Get all files in the directory
                var files = new DirectoryInfo(folderPath).GetFiles(sFiles);  

                if (files.Length == 0)
                {
                    return null; // No files found
                }

                // Get the most recent file's LastWriteTime
                var mostRecentFile = files.OrderByDescending(f => f.LastWriteTime).FirstOrDefault();

                return mostRecentFile?.LastWriteTime;
            }
        }

    }

    public class cUrls
    {
        public string sOrigHref;
        public string sOrigText;
        public string sProposedH;
        public string sProposedT;
        public string sOrigResult;
        public string sChangedResult;
        public string sButtonName = "";
        public string OriginalPageNumber;
        public string ProposedPageNumber;
        public int iOfst;
        public int iNxt;
        public bool bIsImage = false;
        public bool bIsSteps = false;
        public bool bIsPage = false;
        public bool bIsUrl = false;
        public bool bIsMacIDrecord = false;
        public bool bIsValid;
    }
}
