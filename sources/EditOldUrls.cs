using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media.Animation;

namespace MacroEditor.sources
{


    public partial class EditOldUrls : Form
    {
        public string sBodyOut { get; set; }
        public string DataRecordOut { get; set; }
        public string FormattedDataOut { get; set; }
        private cMyUrls mU;
        private string rText;
        private int nSelectedM = -1;
        private string sLBatext;
        private Color lbFC;
        private string[] sImgOpt;
        private bool bIsImage = false;
        private bool bhasMacroID = false;
        private PrinterDB pDB;
        private cDBresult rDB;
        private List<string>MacTagNames = new List<string>();
        private bool bInMacroRecAddMode = false;
        private string sAddModeItem = "";
        private string sAddDoc = "";
        private string sAddPage = "";
        private string sKey = "";
        private string sName = "";
        private string sType = "";
        private int StartMacOld = 0;
        private int EndMacOld = 0;
        private List<string> lbButtons;
        private string DataFileRecord;

        public EditOldUrls(string rRText, string rDataFileRecord, ref PrinterDB RpDB)
        {
            InitializeComponent();
            pDB = RpDB;
            rText = rRText;
            sLBatext = gbText.Text;
            DataFileRecord = rDataFileRecord;
            UpdateAllURLs();
        }


        private void UpdateAllURLs()
        {
            DataRecordOut = "";
            cbMacroList.Items.Clear();
            bhasMacroID = DataFileRecord.Length > 0;
            gpMissing.Enabled = bhasMacroID;
            sImgOpt = Utils.sDifSiz.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < sImgOpt.Length; i++)
            {
                sImgOpt[i] = Utils.sHasSize + sImgOpt[i];
            }
            mU = new cMyUrls();
            if (bhasMacroID)
            {
                cEachTag et, LastET;
                CountMissingItems(ref DataFileRecord);
                rDB = pDB.ParseRecord(ref DataFileRecord);
                int iOfst = 0;   // offset into the rDB list of http and text
                int iNxt = 0;   // the index into HTML or TEXT 
                while (iOfst < rDB.RecordSet.Count)
                {
                    et = rDB.RecordSet[iOfst];
                    if (et.TagName == "Direct Page" || et.TagName == "WPS Page")
                    {
                        LastET = et;
                        iOfst++;
                        iNxt = 0;
                        et = rDB.RecordSet[iOfst];
                        AddPageDocRecord(et, LastET, iOfst, iNxt);
                        iOfst++;
                    }
                    else
                    {
                        while (iNxt < et.SourceHREF.Count)
                        {
                            AddTokenRecord(et, iOfst, iNxt);
                            iNxt++;
                        }
                        iOfst++;
                        iNxt = 0;
                    }
                }
                StartMacOld = mU.Add(rText);
                EndMacOld = mU.UrlInfo.Count;
                for (int i = StartMacOld; i < EndMacOld; i++)
                {
                    cbMacroList.Items.Add("Macro " + (i + 1).ToString().PadLeft(2));
                }
                cbMacroList.SelectedIndex = mU.UrlInfo.Count > 0 ? 0 : -1;
                nSelectedM = cbMacroList.SelectedIndex;
                sLBatext = gbText.Text;
                lbFC = lbChanged.ForeColor;
                return;
            }
            else
            {
                StartMacOld = 0;
                mU.Init(rText);
                EndMacOld = mU.UrlInfo.Count;
            }


            for (int i = 0; i < mU.UrlInfo.Count; i++)
            {
                cbMacroList.Items.Add("Macro " + (i + 1).ToString().PadLeft(2));
            }
            cbMacroList.SelectedIndex = mU.UrlInfo.Count > 0 ? 0 : -1;
            nSelectedM = cbMacroList.SelectedIndex;
            sLBatext = gbText.Text;
            lbFC = lbChanged.ForeColor;

        }

        private void CountMissingItems(ref string sRec)
        {
            FormPrinter fpNew = new FormPrinter();
            fpNew.Init();
            List<string> sPossible = new List<string>();
            lbButtons = fpNew.lbButtons;
            fpNew = null;
            int n = lbButtons.Count;
            int i = 0;

            while (i < n)
            {
                string s = lbButtons[i];
                if (sRec.Contains(s))
                {
                    i++;
                    continue;
                }
                if(s.Contains("Page"))
                {
                    i++;
                    Debug.Assert(i < n);
                    string t = lbButtons[i];
                    Debug.Assert(t.Contains("Doc"));
                    sPossible.Add(t + "," + s);
                }
                else
                {
                    sPossible.Add(s);
                }
                i++;
            }
            cbMissing.DataSource = sPossible;
            if (cbMissing.Items.Count > 0)
            {
                gpMissing.Visible = true;
                cbMissing.SelectedIndex = 0;
            }
        }

        private string FormName(int n, string name)
        {
            return name.Replace(" ", "_") + ":" + (n + 1).ToString();
        }

        private void AddPageDocRecord(cEachTag et, cEachTag LastET, int iOfst, int iDoc) // iPage is always the last of the previous iTag
        {
            string sName = FormName(iDoc, et.TagName);
            MacTagNames.Add(sName);
            cbMacroList.Items.Add(sName);
            cUrls cu = new cUrls();
            cu.sButtonName = et.TagName;
            cu.bIsMacIDrecord = true;
            cu.bIsPage = true;
            cu.bIsUrl = true;
            cu.OriginalPageNumber = LastET.SourceHREF.Last();
            cu.ProposedPageNumber = cu.OriginalPageNumber;
            cu.sProposedT = et.SourceTEXT[iDoc]; // should have used User Manual instead of "Doc"  originally !!!!!          
            cu.sOrigText = cu.sProposedT;
            cu.sProposedH = et.SourceHREF[iDoc];
            cu.sOrigHref = cu.sProposedH;
            cu.sOrigResult = Utils.FormUrl(cu.sOrigHref, cu.sOrigText);
            cu.sChangedResult = cu.sOrigResult;
            mU.UrlInfo.Add(cu);
        }

        private void AddTokenRecord(cEachTag et, int iOfst, int iNxt)
        {
            string sName = FormName(iNxt, et.TagName); // fpNew.lbButtons[et.iTag]);
            MacTagNames.Add(sName);
            cbMacroList.Items.Add(sName);
            cUrls cu = new  cUrls();
            cu.iOfst = iOfst;
            cu.iNxt = iNxt;
            cu.sButtonName = et.TagName;
            cu.bIsMacIDrecord = true;
            cu.bIsUrl = true;
            cu.sProposedT = et.SourceTEXT[iNxt];
            cu.sOrigText = cu.sProposedT;
            cu.sProposedH = et.SourceHREF[iNxt];
            cu.sOrigHref = cu.sProposedH;
            if(et.TagName.Contains("Steps"))
            {
                cu.sOrigResult = Utils.FormNumList(cu.sOrigHref); //Utils.Form1CellTable(cu.sOrigHref,"");
                cu.sChangedResult = cu.sOrigResult;
                cu.bIsSteps = true;
                cu.bIsUrl = false;
            }
            else
            {
                cu.sOrigResult = Utils.FormUrl(cu.sOrigHref, cu.sOrigText);
                cu.sChangedResult = cu.sOrigResult;
                cu.bIsImage = (cu.sOrigHref.IndexOf("<img ") != -1);
                if(bIsImage)
                {
                    cu.bIsSteps = false;
                    cu.bIsPage = false;
                    cu.bIsUrl = true; // still a url ??
                }
            }
            mU.UrlInfo.Add(cu);
        }


        private void SetMacVisible(int i)
        {
            if (mU.UrlInfo[i].bIsPage)
            {
                gpPage.Visible = true; 
                tbPageN.Text = mU.UrlInfo[i].ProposedPageNumber;
            }
            else gpPage.Visible = false;
            if(mU.UrlInfo[i].bIsMacIDrecord)
            {
                gpTag.Visible = true;
                tbTagName.Text = mU.UrlInfo[i].sButtonName;
                gbText.Visible = !mU.UrlInfo[i].bIsSteps;
            }
            else gpTag.Visible = false;
            btnDelSelected.Visible = gpTag.Visible;
        }

        private void ShowImgTip()
        {
            gbText.Text = sLBatext;
            bIsImage = false;
            if (mU.UrlInfo[nSelectedM].bIsImage)
            {
                if (mU.UrlInfo[nSelectedM].sOrigHref.Contains(Utils.sIsAlbum))
                {
                    string s = "Put just before the quote \" delimiter below" + Environment.NewLine;
                    for (int i = 0; i < sImgOpt.Length; i++)
                    {
                        s+= sImgOpt[i] + Environment.NewLine;
                    }
                    tbT.Text = s;
                    gbText.Text = "Image options:";
                }
                else tbT.Text = "No HP options this image";
                bIsImage = true;
            }
        }

        private void cbMacroList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMacroList.SelectedIndex < 0) return;
            nSelectedM = cbMacroList.SelectedIndex;
            if (nSelectedM >= 0)
            {
                tbT.Text = mU.UrlInfo[nSelectedM].sProposedT;
                if (mU.UrlInfo[nSelectedM].sButtonName.Contains("Steps"))
                {
                    tbH.Text = mU.UrlInfo[nSelectedM].sProposedH.Replace("<br>", Environment.NewLine);
                }
                else 
                    tbH.Text = mU.UrlInfo[nSelectedM].sProposedH;
                tbResult.Text = mU.UrlInfo[nSelectedM].sChangedResult;
                tbT.ReadOnly = mU.UrlInfo[nSelectedM].bIsImage;
                if(mU.UrlInfo[nSelectedM].bIsImage)
                {
                    tbT.ReadOnly = true;
                }
                ShowChange();
                SetMacVisible(nSelectedM);
                ShowImgTip();
                btnShowNotes.Enabled = (nSelectedM >= StartMacOld) && (nSelectedM < EndMacOld);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Utils.ShowPageInBrowser("",tbResult.Text);
        }

        private void ShowChange()
        {
            string sC = mU.UrlInfo[nSelectedM].sChangedResult;
            string sO = mU.UrlInfo[nSelectedM].sOrigResult;
            if (tbResult.Text == sO)
            {
                lbChanged.Text = "URL not changed";
                lbChanged.ForeColor = btnCancelExit.ForeColor;

            }
            else
            { 
                if(tbResult.Text == sC) lbChanged.Text = "Changed URL saved";
                else lbChanged.Text = "URL not saved";
                lbChanged.ForeColor = lbFC;
            }
        }

        private void btnCancelChg_Click(object sender, EventArgs e)
        {
            if(nSelectedM >= 0)
            {
                tbT.Text = mU.UrlInfo[nSelectedM].sOrigText;
                tbH.Text = mU.UrlInfo[nSelectedM].sOrigHref;
                tbResult.Text = mU.UrlInfo[nSelectedM].sOrigResult;
                mU.UrlInfo[nSelectedM].sChangedResult = tbResult.Text;
                ShowChange();
            }

        }

        private bool IsBadItem(string sIn, string TagNam)
        {
            if (sIn == Utils.UnNamedMacro) return true;
            string t = Utils.NewItemHasErr(sIn, TagNam);
            if (t == "") return false;
            MessageBox.Show(t, "Critical Error");
            return true;
        }


        // sIn has newlines

        private void FormChange()
        {
            string t = "";
            string sH = tbH.Text;
            string sT = tbT.Text;
            cUrls cu = mU.UrlInfo[nSelectedM];

            if (bhasMacroID && cu.bIsMacIDrecord)
            {
                cu.bIsValid = false;    // assume the worst
                string sTagName = tbTagName.Text;
                switch (sTagName)
                {
                    case "Direct Page":
                    case "WPS Page":
                        string PageNumber = tbPageN.Text;
                        if (IsBadItem(PageNumber, sTagName)) return;
                        if (IsBadItem(sH, "Doc")) return;
                        cu.sProposedH = sH;
                        cu.sProposedT = "User Manual";
                        cu.ProposedPageNumber = PageNumber;
                        tbResult.Text = Utils.FormUrl(sH, sT);
                        cu.sChangedResult = tbResult.Text;
                        break;
                    case "Reset Steps":
                        if(IsBadItem(sH, sTagName)) return;
                        cu.sProposedH = sH;
                        cu.sProposedT = ""; // this is table width
                        tbResult.Text = Utils.FormNumList(sH).Replace(Environment.NewLine,"<br>");
                        cu.sChangedResult = tbResult.Text;
                        break;
                    default:
                        if (IsBadItem(sH, sTagName)) return;
                        cu.sProposedT = sT;
                        cu.sProposedH = sH;
                        tbResult.Text = Utils.FormUrl(sH, sT);
                        cu.sChangedResult= tbResult.Text;
                        break;
                }
                cu.bIsValid = true;
            }
            else
            {
                cu = mU.UrlInfo[nSelectedM];
                string s = "";

                if (cu.bIsImage)
                {
                    s = Utils.AssembleIMG(tbH.Text, false);
                }
                else
                {       
                    if(cu.bIsUrl)
                    {
                        s = Utils.FormUrl(tbH.Text, tbT.Text);
                        if(cu.bIsPage)
                        {
                            cu.ProposedPageNumber = tbPageN.Text;
                            t = Environment.NewLine + "Page " + tbPageN.Text;
                        }
                    }
                    else
                    {
                        // not a url nor an image so steps
                        s = Utils.Form1CellTable(tbH.Text,"");
                    }

                }
                tbResult.Text = s + t;
            }
            ShowChange();
        }

        private int FindOldEA(string TagName)
        {
            int i = 0;
            foreach(cEachTag ea in rDB.RecordSet)
            {
                if (ea.TagName == TagName) return i;
                i++;
            }
            return -1;
        }

        private void SaveChange()
        {
            cUrls cu = mU.UrlInfo[nSelectedM];



            if (nSelectedM >= EndMacOld && cu.bIsMacIDrecord)
            {
                string sTH = mU.UrlInfo[nSelectedM].sProposedH;
                string sTT = mU.UrlInfo[nSelectedM].sProposedT;

                sTT = sTT.Replace(Environment.NewLine, "<br>");

                string sTagName = tbTagName.Text;
                int i = FindOldEA(sTagName);
                if(i == -1)
                {
                    cEachTag ea = new cEachTag();
                    ea.TagName = sTagName;
                    ea.iTag = lbButtons.IndexOf(sTagName);
                    ea.SourceHREF.Add(sTH);
                    ea.SourceTEXT.Add(sTT);
                    rDB.RecordSet.Add(ea);
                    cbMacroList.Items[nSelectedM] = FormName(0, ea.TagName);
                }
                else
                {
                    cEachTag ea = rDB.RecordSet[i];
                    int n = ea.SourceTEXT.Count;
                    ea.SourceHREF.Add(sTH);
                    ea.SourceTEXT.Add(sTT);
                    cbMacroList.Items[nSelectedM] = FormName(n, ea.TagName);
                }
            }
            else
            {
                mU.UrlInfo[nSelectedM].sProposedH = tbH.Text.Trim();
                mU.UrlInfo[nSelectedM].sProposedT = tbT.Text.Trim();
                mU.UrlInfo[nSelectedM].sChangedResult = tbResult.Text.Replace("<br>", Environment.NewLine);
            }

            ShowChange();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
                SaveChange();
        }


        private string FormRecord()
        {
            int nTotalTags = rDB.RecordSet.Count;
            CopyBackRec();  // this create the Href and the Text
            bool b = pDB.CreateRecord(nTotalTags);
            foreach (cEachTag et in rDB.RecordSet)
            {
                int iTag = et.iTag;
                for (int i = 0; i < et.SourceTEXT.Count; i++)
                {
                    pDB.AddNextRecord(iTag, et.TagName, et.SourceHREF[i], et.SourceTEXT[i]);
                }
            }
            return pDB.FormRecord();
        }

        private void ApplyExit()
        {
            string FmtOut = "";

            sBodyOut = mU.GetUpdated(StartMacOld, EndMacOld);
            if (bhasMacroID)
            {                
                DataRecordOut = FormRecord();
                if (pDB.FormatParsedRecord(ref rDB, ref FmtOut))
                {
                    FormattedDataOut = FmtOut;
                    this.Close();
                    return;
                }
                else return;
            }
            this.Close();
        }


        private void CopyBackRec()
        {
            cUrls cu;
            int iOfst = 0;
            int iCU = 0;
            cEachTag LastET;
            foreach (cEachTag et in rDB.RecordSet)
            {
                if(et.TagName.Contains(" Page"))
                {
                    LastET = et;
                    iOfst++;
                    continue;
                }
                for (int i = 0; i < et.SourceTEXT.Count; i++)
                {
                    cu = mU.UrlInfo[iCU];
                    // need to add not as there could be several
                    rDB.RecordSet[iOfst].SourceHREF[i] = cu.sProposedH.Replace(Environment.NewLine, "<br>");
                    rDB.RecordSet[iOfst].SourceTEXT[i] = cu.sProposedT; //.Replace(Environment.NewLine, "<br>");
                    if (cu.bIsPage)
                    {
                        rDB.RecordSet[iOfst - 1].SourceHREF[0] = cu.ProposedPageNumber;
                    }
                    iCU++;
                }
                LastET = et;
                iOfst++;
            }
        }

        private void btnCancelExit_Click(object sender, EventArgs e)
        {
            sBodyOut = "";
            this.Close();
        }


        private void btnClrH_Click(object sender, EventArgs e)
        {
            tbH.Text = "";
        }

        private void btnCanH_Click(object sender, EventArgs e)
        {
            /*
            if (bInMacroRecAddMode)
            {
                string s = tbTagName.Text;
                AddNR(s);
            }
            else
                */
            if (tbTagName.Text.Contains("Steps"))
                tbH.Text = mU.UrlInfo[nSelectedM].sOrigHref.Replace("<br>", Environment.NewLine);
            else
                tbH.Text = mU.UrlInfo[nSelectedM].sOrigHref;
        }

        private void btnClrT_Click(object sender, EventArgs e)
        {
            tbT.Text = "";
        }

        private void btnCanT_Click(object sender, EventArgs e)
        {
            /*
            if (bInMacroRecAddMode)
            {
                string s = tbTagName.Text;
                AddNR(s);
            }
            else
            */
            {
                tbT.Text = mU.UrlInfo[nSelectedM].sOrigText;
                tbPageN.Text = mU.UrlInfo[nSelectedM].OriginalPageNumber;
            }
        }

        private void btnForm_Click(object sender, EventArgs e)
        {
            if(nSelectedM >= 0)
               FormChange();
        }

        private void btnShowNotes_Click(object sender, EventArgs e)
        {
            string sOut = mU.ResidualName();
            DisplayText dt = new DisplayText(sOut);
            dt.Show();
        }

        private void ClearNewAdd()
        {
            tbH.Text = "";
            tbT.Text = "";
            tbResult.Text = "";
        }


        private void btnCreateNR_Click(object sender, EventArgs e)
        {
            btnAddNR.Visible = true;
            tbH.Text = "";
            tbT.Text = "";
            tbResult.Text = "";
            bInMacroRecAddMode = true;
            tbInfo.Text = "Select the item you want to add\r\nand click Add Selected";          
        }


        private void AddNR(string s)
        {
            tbH.Text = "";
            tbT.Text = "";
            tbH.Enabled = true;
            tbT.Enabled = true;
            //btnCancelChg.Enabled = false;
            //btnCancelExit.Enabled = false;
            //btnClrT.Enabled = false;    
            if (s.Contains("Page"))
            {
                int i = s.IndexOf(',');
                Debug.Assert(i >= 0);
                sAddDoc = s.Substring(0, i);
                sAddPage = s.Substring(i + 1);
                tbT.Text = "Doc";
                tbH.Text = Utils.UnNamedMacro;
                tbInfo.Text = "Put the url to the document at HREF and change the -1 at \"Page Number\"\r\n to the page number. " +
                    "Then click \"Form Changes\" then verify by clicking\r\n \"Test Changes\" then \"Save Changes\"." ;
                tbTagName.Text = sAddDoc;
                tbPageN.Text = "-1";
                gpPage.Visible = true;
                tbT.Enabled = false;
            }
            else
            {
                sAddDoc = "";
                sAddPage = "";
                gpPage.Visible = false;
                switch (s)
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
                    case "Driver":
                    case "Scanner":
                    case "YouTube":
                        tbInfo.Text = "Put the url to the document at HREF and the text to be clicked on at \"Text\"\r\n" +
    "Then click \"Form Changes\" then verify by clicking \"Test Changes\" then\r\n\"Save Changes\"";
                        break;
                    case "Reset Steps":
                        tbT.Enabled = false;
                        tbH.Text = "";
                        tbT.Text = "";
                        tbInfo.Text = "Enter the steps to reset the printer at location HREF";
                        break;
                }
            }            
        }


        private void btnAddNR_Click(object sender, EventArgs e)
        {
            string s = cbMissing.SelectedItem as string;
            sAddModeItem = s;
            AddNR(s);
            AddItem(s);
            cbMacroList.SelectedIndex  = cbMacroList.Items.Count - 1; 
        }

        private void AddItem(string TagName)
        {
            cUrls cu = new cUrls();
            cu.bIsMacIDrecord = true;
            cu.sButtonName = TagName;
            cu.sOrigHref = Utils.UnNamedMacro;
            cu.sOrigText = Utils.UnNamedMacro;
            cu.sProposedH = Utils.UnNamedMacro;
            cu.sProposedT = Utils.UnNamedMacro;
            cu.sChangedResult = Utils.UnNamedMacro;
            cu.sOrigResult = Utils.UnNamedMacro;
            cu.bIsSteps = TagName.Contains("Steps");
            mU.UrlInfo.Add(cu);
            cbMacroList.Items.Add(TagName);
        }

        private void cbMissing_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearNewAdd();
        }

        private void btnApplyExit_Click(object sender, EventArgs e)
        {
            ApplyExit();
        }

        private void btnDelSelected_Click(object sender, EventArgs e)
        {
            int iOfst = mU.UrlInfo[nSelectedM].iOfst;
            int iNxt = mU.UrlInfo[nSelectedM].iNxt;
            //mU.UrlInfo.RemoveAt(nSelectedM);
            cEachTag et = rDB.RecordSet[iOfst];
            et.SourceTEXT[iNxt] = "";
            et.SourceHREF[iNxt] = "";
            //cbMacroList.Items.RemoveAt(nSelectedM);
            int n = et.SourceHREF.Count;
            if (iNxt == (n - 1))
            {
                et.SourceHREF.RemoveAt(n - 1);
                et.SourceTEXT.RemoveAt(n - 1);
            }
            else
            {
                while (iNxt < (n))
                {
                    et.SourceTEXT[iNxt] = et.SourceTEXT[iNxt + 1];
                    et.SourceHREF[iNxt] = et.SourceHREF[iNxt + 1];
                    iNxt++;
                }
            }
            rDB.RecordSet.RemoveAt(iOfst);
            DataFileRecord = FormRecord();
            UpdateAllURLs();
        }
    }
}
