//#define USECB
using MacroEditor.Properties;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Xml.Linq;
using static MacroEditor.CMarkup;
using static MacroEditor.sources.cPrinter;
using static MacroEditor.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using Application = System.Windows.Forms.Application;
using Button = System.Windows.Forms.Button;
using ComboBox = System.Windows.Forms.ComboBox;
using Control = System.Windows.Forms.Control;
using Font = System.Drawing.Font;
using GroupBox = System.Windows.Forms.GroupBox;
using Label = System.Windows.Forms.Label;
using TextBox = System.Windows.Forms.TextBox;
using ToolTip = System.Windows.Forms.ToolTip;

// create printer templett


/*
<div style="text-align: justify;" this does not work at the HP forum
 */

namespace MacroEditor.sources
{
    public partial class cPrinter : Form
    {
        private string CurrentClip = "";
        private int iWorkingTab = -1;
        private bool bSwapBW = true;
        public string strResults {get; set;}
        public string strRecord { get; set; } // data base record
        public string strModels { get; set; } // number of model names
        private PrinterDB pDB;

        List<List<string>> PrinterListH; // the clip which is HTML or a bunch of steps or a page number
        List<List<string>> PrinterListT;  // the text to be clicked or the word Page or Document

        private string InfoLable = "The text boxes normally have the\r\nmodel name but you can replace\r\nthe model with a phrase.  That phrase\r\nwill be clicked on to view the object.\r\n";

        cSourceDestination SourceDestination;

        private ParseDevice MyLookup = new ParseDevice();

        List<string> lbButtons;
        List<string> lbTips;
        List<string> lbUrls;
        List<string> lbPhrases;
        List<string> lbPrinterLayout;
        List<List<string>> PrinterHttp;

        private FormPrinter fpNew = new FormPrinter();

        private string sWorkingContext = "";
        private cCheckSpell SpellCheck;


        // do not record Page, Doc for direct or wps as no devices are listed


        //LJ:234 is sample of key
        public cPrinter(ref PrinterDB printerDB, ref cCheckSpell MySpellCheck, string rMacname, string rMactype)
        {
            InitializeComponent();
            strRecord = "";
            strResults = "";
            strModels = "";
            tbModel.Text = rMacname;
            tbSys.Text = rMactype;
            SpellCheck = MySpellCheck;
            fpNew.Init();
            lbButtons = fpNew.lbButtons;            
            lbTips = fpNew.lbTips;
            lbUrls = fpNew.lbUrls;
            lbPrinterLayout = fpNew.lbPrinterLayout;
            lbPhrases = fpNew.lbPhrases;
            PrinterHttp = fpNew.PrinterHttp;
            fpNew.ClearHTTP();
            fpNew.ClearPrinterLists();
            SourceDestination = fpNew.SourceDestination;
            PrinterListH = fpNew.PrinterListH;
            PrinterListT = fpNew.PrinterListT;

            pDB = printerDB;
            strResults = "";
            strModels = "";
            AddSelButtons(ref gbVid, 1);            
        }

        public string GetUnsavedHeader()
        {
            if(strRecord == null) // nothing was saved
            {
                return "";
            }
            int i = strRecord.IndexOf(Environment.NewLine);
            Debug.Assert(i >= 0);
            if (i < 0) return "";
            return strRecord.Substring(0, i).Trim();
        }



        //  sName is "Change Me" or a new name if the clipboard has text in it
        public bool AddNewRecord(string sName, string sysType)
        {
            sWorkingContext = "NEW";
            string tName = ParseClip(sName);
            if(tName != "")
            {
                tbModel.Text = tName;
                tbProduct.Text = tName;
                RunUpdate();
            }
            return true;
        }



        public bool EditNewRecord(string rBody, string sBody)
        {

            string FmtOut = "";
            if (pDB.FormatRecord(rBody, ref FmtOut))
            {
                FillTableFromRecord();
                gbVid.Enabled = true;
                btnApplyExit.Enabled = true;
                return true;
            }
            return false;

        }


        private void ShowDateInfo(string sTimeStamp)
        {
            DateTime dateTime;
            if (sTimeStamp == "")
            {
                dateTime = DateTime.Now;
                sTimeStamp = dateTime.ToString("yyyyMMdd_HHmmss");
            }
            else
            {
                dateTime = DateTime.ParseExact(sTimeStamp,
                    "yyyyMMdd_HHmmss", CultureInfo.InvariantCulture);
            }
            //lbOrigTS.Text = "TimeStamp " + sTimeStamp;
            //tbTS.Text = dateTime.ToString("MMMM dd yyyy, hh:mm tt", CultureInfo.InvariantCulture);
        }

        private string ParseClip(string s)
        {
            if (s == UnNamedMacro) return UnNamedMacro;
            MyLookup.Parse(s);
            tbModel.Text = MyLookup.GetModel();
            tbProduct.Text = MyLookup.GetProductID();
            return tbModel.Text;
        }



        // iTag is the button tag.  return the corresponding sentence 
        private int NToPhrase(int iTag)
        {
            int n = 0;
            string s = lbButtons[iTag].ToString();
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

        // from Video Reset to Video\nReset
        private string SBs(string s)    // set button split
        {
            return s.Replace(' ', '\n');
        }


        private string SBc(string s)    //set button combine
        {
            return s.Replace('\n', ' ');
        }

        private void AddSelButtons(ref GroupBox gb, int iDir)
        {
            int i = 0;
            int n = 0;
            int iS = 28;
            int x = 2, y = 18;
            int iOv;
            int iOp = 32;
            gb.Controls.Clear();
            Label oInfo = new Label();
            oInfo.Text = InfoLable;
            oInfo.Height = 100;
            oInfo.Width = 280;
            oInfo.BackColor = Color.Khaki;
            oInfo.ForeColor = Color.Black;

            foreach (string s in lbButtons)
            {

                Button btn = new Button();
                btn.ForeColor = Color.Red;
                 

                Label lbl1 = new Label();
                Button lbl = new Button();
                Button lbl2 = new Button();
                toolTip1 = new ToolTip();
                toolTip1.SetToolTip(btn, lbTips[n]);
                btn.Text = SBs(s);
                btn.Width = 100;
                btn.Height = 72;
                btn.Tag = n;

                TextBox tbx = new TextBox();

                ComboBox ctbx = new ComboBox();

                ctbx.Height = iS;
                ctbx.Width = iS * 8; //78;// iS * 2;
                ctbx.Tag = n;

                tbx.Height = iS;
                tbx.Width = iS * 8; //78;// iS * 2;
                tbx.Tag = n;

                int k = SourceDestination.ExclusionList.IndexOf(s);
                if (k >= 0)
                {
                    int l = SourceDestination.ExclusionList[k].LastIndexOf(" ");
                    Debug.Assert(l >= 0);
                    tbx.Text = SourceDestination.ExclusionList[k].Substring(l + 1);
                    tbx.ReadOnly = true;
                    tbx.BackColor = Color.Khaki;

                    ctbx.Items.Add(SourceDestination.ExclusionList[k].Substring(l + 1));
                    ctbx.Enabled = false;
                    ctbx.BackColor = Color.Khaki;
                }   
                else
                {
                    tbx.Text = tbModel.Text;
                    ctbx.Items.Add(tbModel.Text);
                }

                iOv = iS * 8;
                lbl.Text = "-";
                lbl.ForeColor = Color.Red;
                //lbl.BackColor = Color.Khaki;
                lbl1.Text = "0";
                lbl1.Tag = n;
                lbl.Tag = n;
                lbl2.Text = "+";
                lbl2.Enabled = false;
                lbl2.ForeColor = Color.DarkGreen;
                //lbl2.BackColor = Color.Khaki;
                lbl2.Tag = n;

                if (iDir == 1)
                {
                    btn.Location = new System.Drawing.Point(x + i * (iOv + btn.Width + iOp), y);
                }
                else
                {
                    btn.Location = new System.Drawing.Point(x, y + i * (iOv + btn.Height + 40));
                }

                lbl2.Width = iS;
                lbl2.Height = iS;
                lbl1.Width = 16;
                lbl1.Height = 16;
                lbl.Width = iS;
                lbl.Height = iS;

                gb.Controls.Add(btn);
                lbl.Location = new System.Drawing.Point(x +  btn.Width + i * (iOv + btn.Width + iOp), y - 30 + btn.Height);

                lbl1.Location = new System.Drawing.Point(x + lbl2.Width + 8 + btn.Width + i * (iOv + btn.Width + iOp), y - 55 + btn.Height);

#if USECB
                ctbx.Location = new System.Drawing.Point(x + lbl2.Width + btn.Width + i * (iOv + btn.Width + iOp), y - 35 + btn.Height);
#else
                tbx.Location = new System.Drawing.Point(x + lbl2.Width + btn.Width + i * (iOv + btn.Width + iOp), y - 35 + btn.Height);
#endif

                lbl2.Location = new System.Drawing.Point(x + btn.Width + i * (iOv + btn.Width + iOp), y - 60 + btn.Height);

                lbl2.Font = new Font(label1.Font.FontFamily, 16);
                lbl.Font = new Font(label1.Font.FontFamily, 16);
                gb.Controls.Add(lbl);
                gb.Controls.Add(lbl1);
                gb.Controls.Add(lbl2);
#if USECB
                gb.Controls.Add(ctbx);
#else
                gb.Controls.Add(tbx);
#endif                
                btn.Click += Btn_Click;
                lbl2.MouseHover += Btn_MouseHover;
                lbl.Click += lbl_clr_Click;
                lbl.MouseHover += Btn_MouseHover;
                lbl2.Click += lbl2_add_Click;
                i++;
                if (i == 3)
                {
                    i = 0;
                    y += (1+i) * (btn.Height + 10);
                }
                n++;
            }
            oInfo.Location = new System.Drawing.Point(x + 750, y);// + 20);
            gb.Controls.Add(oInfo);
        }

        private void Btn_MouseHover(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                int n = (int)button.Tag;
                int c = PrinterListH[n].Count;
                if (c == 0) return;
                string s = "";
                foreach (string t in PrinterListH[n])
                    s += t + Environment.NewLine;
                tbInfoUrls.Text = s;
            }
        }
        private void MainButtonClicked(Button button)
        {
            string s = SBc(button.Text);
            if (fpNew.LookForDuplicates(s, CurrentClip))
            {
                return;
            }

            button.Enabled = false;
            int n = (int)button.Tag;
            button.ForeColor = Color.Blue;

            iWorkingTab = n;
            if (s.Contains("Page"))
            {
                if (CurrentClip.Length > 3)  // page numbers are 1 to 3 digits long
                {
                    CurrentClip = "0";
                }
                else
                {
                    int result = 0;
                    bool success = int.TryParse(CurrentClip, out result);
                    if (!success) CurrentClip = "0";
                }
            }

            fpNew.AddH_list(n,CurrentClip.Replace(Environment.NewLine, "<br>"));
            string ss = GetOperandText(n).Trim();
            int i = fpNew.AddT_list(n, ss);
            SetLabel(n, i.ToString(), s);
            fpNew.Reduce(s, iWorkingTab, GetWorkingText(iWorkingTab));
        }


        private void Btn_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                string t, s = SBc(button.Text);
                CurrentClip = Clipboard.GetText().Trim();
                t = Utils.NewItemHasErr(CurrentClip, s);
                if (t != "")
                {
                    MessageBox.Show(t, "Critical Error");
                    return;
                }
                MainButtonClicked(button);
            }
        }


        private void lbl_clr_Click(object sender, EventArgs e)
        {
            if (sender is Button label)
            {
                int n = (int)label.Tag;
                PrinterListT[n].Clear();
                PrinterListH[n].Clear();
                int i = NToPhrase(n);
                PrinterHttp[i].Clear();
                foreach (Control control in gbVid.Controls)
                {
                    if(control is Button bt)
                    {
                        int bTag = (int)bt.Tag;
                        if (bTag == n)
                        {
                            bt.ForeColor = Color.Red;
                            SetLabel(n, "0","");
                            ClearOneButtons(n);
                            break;
                        }
                    }
                }
            }
        }

        private void ClearAllButtons()
        {
            fpNew.ClearPrinterLists();
            foreach (Control control in gbVid.Controls)
            {
                if (control is Button bt)
                {
                    bt.ForeColor = Color.Red;
                    bt.Enabled = true;
                }
                if (control is Label ll)
                {
                    if (ll.Tag == null) continue;
                    ll.Text = "0";
                }
                if (control is TextBox tb)
                {
                    tb.Text = "";
                }
            }
        }



        private void UpdateModelText(string s)
        {
            foreach (Control control in gbVid.Controls)
            {
                if (control is TextBox tb)
                {
                    if (tb.ReadOnly) continue;
                    tb.Text = s;
                    int iDes = (int) tb.Tag;
                    int n = PrinterListT[iDes].Count;
                    if(n > 0)
                    {
                        string sOld = PrinterListT[iDes][0];
                        PrinterListT[iDes][0] = tb.Text;
                    }
                }
            }
        }

        private string GetWorkingText(int iWorkingTab)
        {
            foreach (Control control in gbVid.Controls)
            {
                if (control is TextBox tb)
                {
                    if (iWorkingTab == (int)tb.Tag)
                        return tb.Text;
                }
            }
            return "";
        }


        //look up the name and return the button
        //sNname must not have space in it
        private Button FindMainButton(string sName)
        {
            foreach (Control control in gbVid.Controls)
            {
                if (control is Button bt)
                {
                    if (bt.Text == sName) // if a + or a - not the button I want
                    {
                        return bt;
                    }

                }
            }
            return null;   
        }

        // find the add or "_" button
        private Button GetAddButton(int iTag)
        {
            foreach (Control control in gbVid.Controls)
            {
                if (control is Button bt)
                {
                    if (bt.Text.Length == 1) // + or -
                    {
                        if(bt.Text == "+" && (int)bt.Tag == iTag)
                        {
                            return bt;
                        }
                    }
                }
            }
            return null;
        }


        private void ClearOneButtons(int n)
        {
            string sSrc = "";
            foreach (Control control in gbVid.Controls)
            {
                if (control is Button bt)
                {
                    if (bt.Text.Length > 1) // if a a + or a - not the button I want
                    {
                        if (n == (int)bt.Tag)
                        {
                            PrinterListH[n].Clear();
                            PrinterListT[n].Clear();
                            bt.ForeColor = Color.Red;
                            bt.Enabled = true;
                            sSrc = SBc(bt.Text);
                            int m = SourceDestination.InxSrcPhrase(sSrc);
                            PrinterHttp[m].Clear(); ;
                        }
                    }

                }
                if (control is Label ll)
                {
                    if (ll.Tag == null) continue;
                    if (n == (int)ll.Tag)
                    {
                        ll.Text = "0";
                    }
                }
                if (control is TextBox tb)
                {
                    //if (n == (int)tb.Tag)                        tb.Text = "";
                }
            }
        }

        
        // get the text that is to be clicked on if clickable (Page and Document not clickable)
        private string GetOperandText(int n)
        {
            foreach (Control control in gbVid.Controls)
            {
                if (control is TextBox tb)
                {
                    if (n == (int)tb.Tag)
                        return tb.Text;
                }
                if (control is ComboBox ctb)
                {
                    if (n == (int)ctb.Tag)
                        return ctb.SelectedText;
                }
            }
            Debug.Assert(false);
            return "";
        }

        // just the opposite and needed as the test program needs to update that field
        private void SetOperandText(int n, string s)
        {
            foreach (Control control in gbVid.Controls)
            {
                if (control is TextBox tb)
                {
                    if (n == (int)tb.Tag)
                    {
                        if(!tb.ReadOnly)
                        {
                            tb.Text = s;
                        }
                        else
                        {

                        }
                        return;
                    }
                }
                if (control is ComboBox ctb)
                {
                    if (n == (int)ctb.Tag)
                    {
                        if (ctb.Enabled)
                        {
                            ctb.Items.Add(s);
                        }
                        else
                        {

                        }
                        return;
                    }
                }
            }
            Debug.Assert(false);
            return;
        }



        private void SetLabel(int itag, string v, string sName)
        {
            bool bBlock; ;
            foreach (Control control in gbVid.Controls)
            {
                if (control is Label lb)
                {
                    int bTag = (int)lb.Tag;
                    if (bTag == itag)
                    {
                        lb.Text = v;
                        bBlock = v != "0" && SourceDestination.AllowAdditionalItems(sName);
                        SetButton(itag, bBlock);
                        return;
                    }
                }
            }
            Debug.Assert(false);
        }


        // set the + button disabled for those items that only allow 1 explanation (page and document #0
        private void SetButton(int itag, bool b)
        {
            foreach (Control control in gbVid.Controls)
            {
                if (control is Button lb)
                {
                    int bTag = (int)lb.Tag;
                    if (bTag == itag && lb.Text == "+")
                    {
                        lb.Enabled = b ;
                        return;
                    }
                }
            }
            Debug.Assert(false);
        }

        private void AddButtonClicked(Button button)
        {
            int n = (int)button.Tag;
            iWorkingTab = n;
            string sClip = CurrentClip; //Clipboard.GetText();
            foreach (Control control in gbVid.Controls)
            {
                if (control is Button bt)
                {
                    int bTag = (int)bt.Tag;
                    if (bTag == n)
                    {

                        string sB = lbButtons[n].ToString();
                        if (fpNew.LookForDuplicates(sB, sClip))
                        {
                            return;
                        }

                        PrinterListH[n].Add(sClip);
                        string sTgt = GetOperandText(n);
                        PrinterListT[n].Add(sTgt);
                        SetLabel(n, PrinterListH[n].Count.ToString(), "");
                        string s = lbButtons[n].ToString();
                        fpNew.Reduce(s, iWorkingTab, GetWorkingText(iWorkingTab));
                        break;
                    }
                }
            }
        }

        private void lbl2_add_Click(object sender, EventArgs e)
        {

            if (sender is Button button)
            {
                CurrentClip = Clipboard.GetText().Trim();
                AddButtonClicked(button);
            }            
        }


        private string uFix(string s)
        {
            string t = s.Replace("@model@", tbModel.Text);
            return t.Replace("@productid@", tbProduct.Text);
        }


        private void bltnLKUP_Click(object sender, EventArgs e)
        {
            foreach(string s in MyLookup.sUrlSearchPR)
            {
                Utils.LocalBrowser(uFix(s));
            }
        }

        private void btnFindID_Click(object sender, EventArgs e)
        {
            Utils.LocalBrowser(MyLookup.SystemSearch(tbModel.Text));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbEdit.Clear();
        }

        private void btnShowPage_Click(object sender, EventArgs e)
        {
            string FmtOut = "";
            string sIgnore = "";
            if(fpNew.ApplyFormat(ref FmtOut, ref sIgnore))
            {
                tbEdit.Text = FmtOut;
                tabC.SelectTab(1);
                Utils.ShowRawBrowser(tbEdit.Text, tbSys.Text);
            }
        }

        private void btnCopyClip_Click(object sender, EventArgs e)
        {
            if (tbEdit.Text == "") return;
            Clipboard.SetText(tbEdit.Text.Replace(Environment.NewLine, "<br>"));
        }


        private void btnApply_Click(object sender, EventArgs e)
        {
            string FmtOut = "";
            string sIgnore = "";
            if (fpNew.ApplyFormat(ref FmtOut, ref sIgnore))
            {
                tbEdit.Text = FmtOut;
            }
            else tbEdit.Text = "";
        }

        private void btnCopyNote_Click(object sender, EventArgs e)
        {
            CSendNotepad SendNotepad = new CSendNotepad();
            SendNotepad.PasteToNotepad(tbEdit.Text);
        }

        private void ReplaceText(int iStart, int iLen, string strText)
        {
            string sPrefix = tbEdit.Text.Substring(0, iStart);
            string sSuffix = tbEdit.Text.Substring(iStart + iLen);
            tbEdit.Text = sPrefix + strText + sSuffix;
            Utils.ScrollToCaretPosition(tbEdit, iStart, strText.Length);
        }

        private void TbodyInsert(string sClip)
        {
            int i = tbEdit.SelectionStart;
            int j = tbEdit.SelectionLength;
            ReplaceText(i, j, sClip);
        }
        private void btnPasteHTML_Click(object sender, EventArgs e)
        {
            string s = Utils.GetHPclipboard().Trim();
            PasteHTML ph = new PasteHTML();
            string sOut = ph.ProcessClip(ref s);
            TbodyInsert(sOut.Trim());
        }

        private void btnClrLK_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            tbModel.Text = "";
            tbProduct.Text = "";
            tbEdit.Text = "";
            ClearAllButtons();
        }

        private void btnBR_Click(object sender, EventArgs e)
        {
            if(bSwapBW)
            {
                tbEdit.Text = tbEdit.Text.Replace("<br>",Environment.NewLine);
            }
            else
            {
                tbEdit.Text = tbEdit.Text.Replace(Environment.NewLine,"<br>");
            }
            bSwapBW = !bSwapBW;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            strResults = "";
            strModels = "";
            this.Close();
        }

        private void RunUpdate()
        {
            UpdateModelText(tbModel.Text);
            if(tbProduct.Text == "")
                tbProduct.Text = tbModel.Text;
            gbVid.Enabled = true;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            RunUpdate();
        }

        private void btnApplyExit_Click(object sender, EventArgs e)
        {
            string FmtOut = "";
            string strM = "";
            if (fpNew.ApplyFormat(ref FmtOut, ref strM))
            {
                strResults = FmtOut;
                tbEdit.Text = FmtOut;
                strModels = strM;
                strRecord = FormPrinterRecord();
                this.Close();
            }
            tabC.SelectTab(0);
        }

        private void lbHover_MouseHover(object sender, EventArgs e)
        {
            string s = Clipboard.GetText();
            if (s.Length > 256) s = "";
            tbShowClip.Text = s;
        }

        private void lbHover_MouseLeave(object sender, EventArgs e)
        {
            tbShowClip.Text = "";
        }
        private void SetTestString( int iTag, string s)
        {
            foreach (Control control in gbVid.Controls)
            {
                if (control is TextBox tb)
                {
                    if((int)tb.Tag == iTag)
                        tb.Text = s;
                }
            }
        }

        private void SetTextbox(int iTag, string s)
        {
            foreach (Control control in gbVid.Controls)
            {
                if (control is TextBox tb)
                {
                    if ((int)tb.Tag != iTag) continue;
                    tb.Text = s;
                    return;
                }
            }
        }

        private void ClickButton(int iTag,string sID, string s)
        {
            foreach (Control control in gbVid.Controls)
            {
                if (control is Button button)
                {
                    if ((int)button.Tag != iTag) continue;
                    string sBtxt = button.Text;
                    if(sBtxt == sID)
                    {
                        CurrentClip = s;
                        if (sID != "+")
                            MainButtonClicked(button);
                        else
                            AddButtonClicked(button);
                        return;
                    }
                }
            }
        
        }

        private void FillTableFromRecord()
        {
            int i, n;
            string e, sTok;
            ClearAll();
            foreach (cEachTag et in pDB.LastDBresult.RecordSet)
            {
                e = sTOe(et.TagName);
                sTok = SBs(et.TagName);
                n = et.SourceTEXT.Count;
                for (i = 0; i < n; i++)
                {
                    int nTag = ButtonNameToTag(et.TagName);
                    et.iTag = nTag;
                    SetTextbox(et.iTag,et.SourceTEXT[i]);
                    if(i == 0)
                    {
                        ClickButton(nTag, sTok, et.SourceHREF[i]);
                    }
                    else
                    {
                        ClickButton(nTag, "+", et.SourceHREF[i]);
                    }
                }
            }
        }

        private int ButtonNameToTag(string s)
        {
            return lbButtons.IndexOf(SBc(s));
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {

            int i = 0;
            

            foreach (string s in lbButtons)
            {

                string TestURL = "URL:" + i.ToString() + s;

                string sButtonName = SBs(s);
                Button bMain = FindMainButton(sButtonName);

                CurrentClip = "xxx";
                iWorkingTab = i;

                
                
                    SetOperandText(i, "xx");
                    MainButtonClicked(bMain);
                
                
                i++;
            }
        }


        private int CountRecords()
        {
            int n = 0;
            foreach(List<string> Rec in  PrinterListT)
            {
                if (Rec.Count > 0) n++;
            }
            return n;
        }

        private string FormPrinterRecord()
        {
            int nTotalTags = CountRecords();
            int iTag;
            bool b = pDB.CreateRecord(nTotalTags);
            foreach (string s in lbButtons)
            {
                string sButtonName = SBs(s);
                Button bMain = FindMainButton(sButtonName);
                iTag = (int)bMain.Tag;

                int cH = PrinterListH[iTag].Count;
                int cT = PrinterListT[iTag].Count;
                Debug.Assert(cH == cT);
                for (int cc = 0; cc < cH; cc++)
                {
                    pDB.AddNextRecord(iTag, s, PrinterListH[iTag][cc], PrinterListT[iTag][cc]);
                }
            }
            return pDB.FormRecord();
        }

        private bool IsValidFileName(string fileName)
        {
            char[] invalidChars = Path.GetInvalidFileNameChars();

            foreach (char c in invalidChars)
            {
                if (fileName.Contains(c))
                {
                    return false;
                }
            }

            return true;
        }

        private void EditHelp(string s)
        {            
           if (Utils.bSpellingEnabled)
                SpellCheck.EditHelpDocs(s);
           else Utils.WordpadEdit(s);
        }

        private void cPrinter_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            EditHelp("NEW-PRINTER");
        }

        private void tbInfoUrls_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = e.LinkText,
                UseShellExecute = true
            });
        }
    }
}
