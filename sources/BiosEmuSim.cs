using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;
using System.Windows.Documents;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Runtime.InteropServices.ComTypes;
using System.Diagnostics;
using System.Security;
using System.Threading;
using System.Reflection;
using System.Xml.Linq;

namespace MacroEditor.sources
{

    /*
    <a jsname="UWckNb" href="https://pranx.com/bios/"
        data-ved="2ahUKEwjP7f_qp9uHAxU5HkQIHd_BN6cQFnoECBUQAQ"
        ping="/url?sa=t&amp;source=web&amp;rct=j&amp;opi=89978449&amp;
        url=https://pranx.com/bios/&amp;ved=2ahUKEwjP7f_qp9uHAxU5HkQIHd_BN6cQFnoECBUQAQ" >
        <h3 >Phoenix BIOS Setup Utility Simulator</h3></a>
    */

    //<a href="http://h10032.www1.hp.com/ctg/Manual/c06534544.pdf" target="_blank">stest</a>

    public partial class BiosEmuSim : Form
    {
        public class cBSFview
        {
            public int nInx { get; set; }
            public string sTEXT { get; set; }
            public string sHREF { get; set; }
        }

        private List<cBSFview> BSFsources = new List<cBSFview>();
        private string srcData = "BiosSimulators.txt";
        private string expHTML = "BiosSimulators.html";
        private string LsrcData = "";
        private string LexpHTML = "";
        private int nCurrentRow;
        private string sCurrentRowURL;
        private BindingSource bs = new BindingSource();
        private bool bChanged = false;
        private List<string> sRawText = new List<string>();
        private List<string> sRawHTML = new List<string>();
        private List<int> iInxText = new List<int>();
        private Color Blue;
        private Color Red = Color.Red;
        private List<string> sDocs = new List<string>();
        private List<string> tDocs = new List<string>();
        private int nDupDocs = 0;
        private static string sExpectName = "Interactive BIOS simulator ";
        private static string sBadSimName = "BIOS main page only: others missing ";
        private static string sNotSimName = "BIOS complete: But not a simulator ";
        private static string sIsDocument = "F10 notes: ";
        private static string[] sPossible = new string[4] { sExpectName, sBadSimName, sNotSimName, sIsDocument };
        private string SENfound = "";
        private int iLastFound = -1;
        private string sHeaderHTTP = "BIOS simulator / emulator (red for \"http:\" URLs)";
        private string sHeaderPDF = "BIOS simulator / emulator (red for missing .PDF)"; // green not visible
        private string ShowColor;
        private string sHeaderDUPS = "BIOS simulator / emulator (red for duplicates)"; // green not visible
        private string sLastSelectedItem = sExpectName;
        private int iOnce = 2;
        private string sDupID = "";
        private bool bNotSorted = false;
        public BiosEmuSim()
        {
            InitializeComponent();
            Blue = btnSave.ForeColor;
            LsrcData = Utils.WhereExe + "/" + srcData;
            LexpHTML = Utils.WhereExe + "/" + expHTML;
            cbSelText.DataSource = sPossible;
            LoadSimFiles();
            foreach (DataGridViewColumn column in dgvBIOS.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void LoadSimFiles() // (object sender, EventArgs e)
        {
            string sDefault = "<a href=\"https://pranx.com/bios\" target=\"_blank\">Phoenix CMOS Setup Utility</a>";
            int i = 0;
            string s, sH = "", sT = "";
            tbError.Text = "";
            BSFsources.Clear();
            sDocs.Clear();
            tDocs.Clear();
            nDupDocs = 0;
            SENfound = "";
            sRawText.Clear();
            sRawHTML.Clear();
            dgvBIOS.DataSource = null;
            dgvBIOS.Refresh();


            if (!File.Exists(LsrcData))
            {
                File.WriteAllText(LsrcData, sDefault);
            }
            StreamReader sr = new StreamReader(LsrcData);
            s = sr.ReadLine();
            while (s != null)
            {
                string t = s.Replace("  ", " ").Replace("\t", " ");
                if (s != t) MustSave(true);
                s = t;
                int r = nExtractHT(s, ref sH, ref sT);
                if (r == 0)
                {
                    nDupDocs += ExtractDoc(sH, i);
                    sRawHTML.Add(sH);
                    sRawText.Add(sT);
                    if (!StringsHas(ref sPossible, sT))
                    {
                        SENfound += (i + 1).ToString() + " ";
                    }
                }
                else
                {
                    tbError.Text += "error reading data at " + (i+1).ToString() + ":" + r.ToString();
                    break;
                }
                i++;
                s = sr.ReadLine();
            }
            bNotSorted = !MySort(sRawText);
            for (i = 0; i < sRawHTML.Count; i++)
            {
                int j = iInxText[i];
                cBSFview cBSF = new cBSFview();
                cBSF.sTEXT = sRawText[j];
                cBSF.sHREF = sRawHTML[j];
                cBSF.nInx = i + 1;
                BSFsources.Add(cBSF);
            }
            sr.Close();
            bs.DataSource = BSFsources;
            dgvBIOS.DataSource = bs;
            dgvBIOS.Columns[2].Visible = false;
            dgvBIOS.Columns[0].ReadOnly = true;
            dgvBIOS.Columns[1].ReadOnly = true;
            dgvBIOS.Columns[0].Width = 36;
            dgvBIOS.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvBIOS.Columns[1].HeaderText = sHeaderHTTP;
            dgvBIOS.Columns[0].HeaderText = "N";
            dgvBIOS.Refresh();
            if (SENfound != "")
            {
                tbError.Text += "\r\nmissing IDs at\r\n" + SENfound;
            }
            cbDups.Items.Clear();
            foreach (string sItem in tDocs)
            {
                cbDups.Items.Add(sItem);
            }
            if (cbDups.Items.Count > 0)
                cbDups.SelectedIndex = 0;
            else
            {
                rbMhttp.Checked = true;
                cbDups.Enabled = false;
            }
            if(bNotSorted)
            {
                MessageBox.Show("One or more items are not sorted\r\nPress SAVE Changes and restart",
                    "Caution: Items out of order");
            }
        }

        private void FlagInsecureURLs()
        {
            int i = 0;
            bool PDFmissing = rbMpdf.Checked;
            foreach (cBSFview cb in BSFsources)
            {
                if (rbMhttp.Checked)
                {
                    if (cb.sTEXT.Contains(".pdf"))
                        dgvBIOS.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                    else
                        dgvBIOS.Rows[i].Cells[0].Style.ForeColor = Color.Black;
                }
                if (rbMhttp.Checked)
                {
                    if (cb.sHREF.Contains("http:"))
                        dgvBIOS.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                    else
                        dgvBIOS.Rows[i].Cells[0].Style.ForeColor = Color.Black;
                }
                if (rbDups.Checked)
                {
                    if (cb.sHREF.Contains(sDupID))
                        dgvBIOS.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                    else
                        dgvBIOS.Rows[i].Cells[0].Style.ForeColor = Color.Black;
                }
            }
        }

        private void FlagInsecureURL()
        {
            int i = dgvBIOS.Rows.Count - 1;
            cBSFview cb = BSFsources[i];

            if (cb.sHREF.Contains("http:"))
                dgvBIOS.Rows[i].Cells[0].Style.ForeColor = Color.Red;
            else
                dgvBIOS.Rows[i].Cells[0].Style.ForeColor = Color.Black;
        }

        private int ExtractDoc(string s, int j)
        {
            int Rtn = 0;
            s = s.ToLower().Replace(".pdf", "");
            int i = s.IndexOf("manual/");
            if (i >= 0)
                s = s.Substring(i + 7);
            if (sDocs.Contains(s))
            {
                Rtn = 1;
                tbError.Text += "Dup: " + s + " at " + (j + 1).ToString() + Environment.NewLine;
                if (!tDocs.Contains(s))
                    tDocs.Add(s);
            }
            sDocs.Add(s);
            return Rtn;
        }

        private bool MySort(List<string> list)
        {
            int i = 0;
            bool bInOrder = true;
            var sortedIndices = list
     .Select((value, index) => new { Value = value, Index = index })
     .OrderBy(item => item.Value)
     .Select(item => item.Index)
     .ToList();
            foreach (var index in sortedIndices)
            {
                iInxText.Add(index);

            }
            foreach(int j in iInxText)
            {
                bInOrder &= (i == j);
                i++;
            }
            return bInOrder;
        }

        private string bingURL(string s)
        {
            int i = s.IndexOf("Manual/c");
            if (i == -1) return "";
            i++;
            int j = s.IndexOf("&", i);
            if (j == -1) return "";
            string t = s.Substring(0, j);
            i = t.LastIndexOf("http");
            if (i == -1) return "";
            return t.Substring(i);
        }

        // note all have that h3
        private int nGoogleExtractHT(ref string sHREF, ref string sTEXT)
        {
            string s = Utils.GetHPclipboard();
            string t = s.ToLower();
            int i = t.IndexOf("href=");
            if (i == -1) return 1;
            i += 5; // should be a quote or double quote
            string c = t.Substring(i, 1);
            if (!(c == "'" || c == "\"")) return 2;
            i++;
            int j = s.IndexOf(c, i);
            if (j == -1) return 3;
            string sBing = s.Substring(i, j - i);
            if (sBing.ToLower().Contains(".bing."))
            {
                string v = Utils.ConvertToHTML(sBing);
                v = bingURL(v);
                if (v == "") return 6;
                sHREF = v.Trim();
            }
            else
                sHREF = s.Substring(i, j - i);
            i = s.LastIndexOf("</h3>");
            if (i == -1)
                i = s.LastIndexOf("</a>");
            if (i == -1) return 4;
            s = s.Substring(0, i);
            j = s.LastIndexOf(">");
            if (j == -1) return 5;
            j++;
            s = s.Substring(j, i - j).Replace("...", "");
            sTEXT = s.Trim();
            return 0;
        }

        private int nExtractHT(string s, ref string sHREF, ref string sTEXT)
        {
            string ab = "_blank\">";
            int i = s.IndexOf("href=");
            if (i == -1) return 1;
            i += 5; // should be a quote or double quote
            string c = s.Substring(i, 1);
            if (!(c == "'" || c == "\"")) return 2;
            i++;
            int j = s.IndexOf(c, i);
            if (j == -1) return 3;
            sHREF = s.Substring(i, j - i);
            i = s.LastIndexOf("</a>");
            if (i == -1) return 4;
            j = s.LastIndexOf(ab);
            if (j == -1) return 5;
            j += ab.Length;
            string sU = s.Substring(j, i - j);
            string sT = sU.Replace("...", "").Trim();
            if (sU != sT)
            {
                MustSave(true);
            }
            sTEXT = sT;
            return 0;
        }

        private void btnClrIL_Click(object sender, EventArgs e)
        {
            tbError.Text = "";
            tbHref.Text = "";
            tbText.Text = sExpectName;
            tbURL.Text = "";
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            string sH = "";
            string sT = "";
            int r = nGoogleExtractHT(ref sH, ref sT);
            if (r == 0)
            {
                tbHref.Text = sH;
                tbText.Text = sT;
                tbURL.Text = Utils.FormUrl(tbHref.Text, tbText.Text);
            }
            else
            {
                tbError.Text = "error code " + r.ToString() + "\r\n";
            }
        }

        private int iHasItem(string sDN)
        {
            int i = 0;
            string s = tbText.Text;
            foreach (cBSFview cb in BSFsources)
            {
                if (cb.sTEXT == s) return i;
                i++;
            }
            i = sDocs.IndexOf(sDN);
            return i;
        }

        private void MustSave(bool b)
        {
            bChanged = b;
            btnSave.ForeColor = b ? Red : Blue;
        }

        private string GetDoc(string s)
        {
            string[] sEnds = new string[3] { "\\c", "/c", "/ish_" };
            s = s.Trim().ToLower();
            s = s.Replace(".pdf", "");
            int j = 0, i = 0;
            bool bFound = false;
            foreach (string e in sEnds)
            {
                i = s.LastIndexOf(e);
                if (i != -1)
                {
                    bFound = true;
                    break;
                }
                j++;
            }
            if (!bFound) return "";
            return s.Substring(i + sEnds[j].Length);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string sDocName = GetDoc(tbHref.Text);
            if (sDocName == "")
            {
                tbError.Text = "Unable to find a document";
                return;
            }
            string sT = tbText.Text;
            string sH = tbHref.Text;
            int i = iHasItem(sDocName) + 1;
            if (i > 0)
            {
                tbError.Text = "Already in table";
                dgvBIOS.Rows[i - 1].Selected = true;
            }
            else
            {
                cBSFview cBSF = new cBSFview();
                cBSF.sTEXT = sT;
                cBSF.sHREF = sH;
                cBSF.nInx = BSFsources.Count;
                BSFsources.Add(cBSF);
                sRawHTML.Add(sH);
                sRawText.Add(sT);
                tbURL.Text = Utils.FormUrl(sH, sT);
                tbText.Text = sLastSelectedItem;
                tbHref.Text = "";
                tbError.Text = "Added to table";
                bs.ResetBindings(false);
                MustSave(true);
                FlagInsecureURL();
                rbMhttp.Enabled = true;
            }
        }

        private void dgvBIOS_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (iOnce > 0)
            {
                iOnce--;
                return;
            }
            nCurrentRow = e.RowIndex;
            tbTextB.Text = BSFsources[nCurrentRow].sTEXT;
            tbHrefB.Text = BSFsources[nCurrentRow].sHREF;
            btnUpdateURL.Enabled = false;
            sCurrentRowURL = Utils.FormUrl(tbHrefB.Text, tbTextB.Text);
            ShowDOCproblems();
        }

        private void ShowDOCproblems()
        {
            tbError.Text = "";
            string s = tbHrefB.Text.ToLower();
            if (s.Contains("http:"))
                tbError.Text = "Insecure HTTP used" + Environment.NewLine;
            if (s.Contains(".pdf")) return;
            tbError.Text += ".pdf missing from document";
        }

        private string ForceHREF(string t, bool bIsDoc)
        {
            string s = t.Trim();
            if (s.Contains("http:") && cbForceHTTPS.Checked)
                s = s.Replace("http:", "https:");
            if ((!s.Contains(".pdf")) && cbForcePDF.Checked && !bIsDoc) return s + ".pdf";
            return s;
        }

        private void dgvBIOS_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string s = BSFsources[nCurrentRow].sHREF;
            bool b = BSFsources[nCurrentRow].sTEXT.Contains(sIsDocument);
            s = ForceHREF(s,b);
            //tbURL.Text = s;
            if (cbUseFF.Checked)
                Utils.RunFirefox(s);
            else
                Utils.LocalBrowser(s);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string strErr = Utils.BBCparse(tbURL.Text);
            if (strErr == "")
            {
                Utils.LocalBrowser(tbHref.Text);
            }
            else tbError.Text = strErr;
        }

        private bool StringsHas(ref string[] sS, string s) 
        {
            foreach(string t in sS)
            {
                if (s.Contains(t)) return true;
            }
            return false;
        }


        private void btnMakeURL_Click(object sender, EventArgs e)
        {
            if(!StringsHas(ref sPossible,tbText.Text))
            {
                tbError.Text += "text phrase not recognized\r\n";
            }
            tbURL.Text = Utils.FormUrl(tbHref.Text, tbText.Text);
        }

        
        private void btnExport_Click(object sender, EventArgs e)
        {
            int n = sExpectName.Length;
            bool bIsBAD = false;
            bool bIsNOT = false;
            bool bIsLIST = false;
            bool bIsDoc = false;
            List<string> CandidateList = new List<string>();
            List<string> CandidateBAD = new List<string>();
            List<string> CandidateNOT = new List<string>();
            List<string> CandidateDOC = new List<string>();
            int nCol = 3; // want 3 columns
            string LineOut = "";
            CandidateList.Clear();
            CandidateBAD.Clear();
            CandidateNOT.Clear();
            CandidateDOC.Clear();

            foreach (cBSFview cBSF in BSFsources)
            {
                string t = cBSF.sTEXT.Trim();
                string sHREF = cBSF.sHREF;
                bIsBAD = t.Contains(sBadSimName);
                bIsNOT = t.Contains(sNotSimName);   
                bIsLIST = t.Contains(sExpectName);
                bIsDoc = t.Contains(sIsDocument);
                if (!(bIsLIST | bIsBAD || bIsNOT || bIsDoc))
                {
                    continue;
                }

                if (!(sHREF.Contains(".pdf")) && cbForcePDF.Checked && !bIsDoc)
                {
                    sHREF += ".pdf";
                }

                if(bIsNOT)
                {
                    n = sNotSimName.Length;
                    string s = Utils.FormUrl(sHREF, t.Substring(n).Trim());
                    CandidateNOT.Add(s);
                }
                else if (bIsLIST)
                {
                    n = sExpectName.Length;
                    string s = Utils.FormUrl(sHREF, t.Substring(n).Trim());
                    CandidateList.Add(s);
                }
                else if (bIsDoc)
                {
                    n = sIsDocument.Length;
                    string s = Utils.FormUrl(sHREF, t.Substring(n).Trim());
                    CandidateDOC.Add(s);
                    continue;
                }
                else
                {
                    n = sBadSimName.Length;
                    string s = Utils.FormUrl(sHREF, t.Substring(n).Trim());
                    CandidateBAD.Add(s);
                }
            }
            LineOut = "<center><font size=\"6.0\">" + sExpectName + "for HP PCs</font></center><br>";
            LineOut += "Items marked <strong><font color='#FF0000'>[*]</font></strong> are older and depending on your browser may ask for authorization to run";
            LineOut += "<br>" + FormTable(nCol, ref CandidateList);
            if(CandidateNOT.Count > 0)
            {
                LineOut += "<br><center><font size=\"6.0\">" + sNotSimName + "</font></center>";
                LineOut += "<br>" +FormTable(nCol, ref CandidateNOT);
            }
            if (CandidateBAD.Count > 0)
            {
                LineOut += "<br><center><font size=\"6.0\">" + sBadSimName + "</font></center>";
                LineOut += "<br>" + FormTable(nCol, ref CandidateBAD);
            }

            if (CandidateDOC.Count > 0)
            {
                LineOut += "<br><center><font size=\"6.0\">" + sIsDocument + "</font></center>";
                LineOut += "<br>" + FormTable(nCol, ref CandidateDOC);
            }
            File.WriteAllText(LexpHTML, LineOut);
            tbError.Text = "Exported Good: " + (CandidateList.Count).ToString() + Environment.NewLine;
            tbError.Text += "Exported Not: " + (CandidateNOT.Count).ToString() + Environment.NewLine;
            tbError.Text += "Exported Bad:" + (CandidateBAD.Count).ToString() + Environment.NewLine;
            tbError.Text += "Exported DOC:" + (CandidateDOC.Count).ToString() + Environment.NewLine;
        }

        private string FormTable(int nCol, ref List<string> CandidateList)
        {
            string LineOut="";
            int nRows;
            nRows = Utils.MakeDivisible(CandidateList.Count, nCol);
            nRows /= nCol;
            int nRem = (nRows * nCol) - CandidateList.Count;
            if (nRem > 0)
            {
                for (int i = 0; i < nRem; i++)
                {
                    CandidateList.Add("");
                }
            }
            LineOut += "<table border='0'>";
            for (int i = 0; i < nRows; i++)
            {
                LineOut += "<tr>";
                for (int j = 0; j < nCol; j++)
                {
                    string u = CandidateList[i + j * nRows].Trim();
                    if (u != "")
                    {
                        if (u.ToLower().Contains("http:"))
                        {
                            u = "<strong><font color='#FF0000'>[*]</font></strong>" + u;

                        }
                        else
                        {
                            //u = "<font color='#008000'>[*]</font>" + u;
                            u = "<b>[*]</font></b>" + u;
                        }
                    }
                    else u = " ";
                    string aLine = "<td>" + u + "</td>";
                    LineOut += aLine;
                }
                LineOut += "</tr>";
            }
            LineOut += "</table> <br><br>";
            return LineOut;
        }

        private void SaveChanges()
        {
            using (StreamWriter writer = new StreamWriter(LsrcData, false))
            {
                foreach (cBSFview cBSF in BSFsources)
                {
                    string s = Utils.FormUrl(cBSF.sHREF, cBSF.sTEXT);
                    writer.WriteLine(s);
                }
                writer.Close();
            }
            MustSave(false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }


        private void PerformUpdate()
        {
            BSFsources[nCurrentRow].sHREF = tbHrefB.Text;
            BSFsources[nCurrentRow].sTEXT = tbTextB.Text;
        }

        private void btnUpdateURL_Click(object sender, EventArgs e)
        {
            PerformUpdate();
            bs.ResetBindings(false);
        }

        private void btnTestB_Click(object sender, EventArgs e)
        {
            Utils.LocalBrowser(tbHrefB.Text);
            string s = Utils.FormUrl(tbHrefB.Text, tbTextB.Text);
            bChanged = (s != sCurrentRowURL);
            btnUpdateURL.Enabled = bChanged;
        }

        private void BiosEmuSim_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(bChanged)
            {
                DialogResult dr = MessageBox.Show("You have not saved changes." + Environment.NewLine +
                    "Click OK to save",
                    "Click Cancel to return", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    SaveChanges();
                    e.Cancel = false;
                }

                else
                    e.Cancel = true;
            }
        }



        private void btnCLRatbHREF_Click(object sender, EventArgs e)
        {
            tbHref.Text = "";
        }

        private void btnCLRatbaTEXT_Click(object sender, EventArgs e)
        {
            tbText.Text = sLastSelectedItem;
        }

        private void dgvBIOS_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            MustSave(true);
        }

        private void btnCLRhb_Click(object sender, EventArgs e)
        {
            tbHrefB.Text = "";
        }

        private void btnCLRtb_Click(object sender, EventArgs e)
        {
            tbTextB.Text = "";
        }

        private void btnCanChg_Click(object sender, EventArgs e)
        {
            LoadSimFiles();
            MustSave(false);
        }

        private void dgvBIOS_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void RunSearch()
        {
            string s = tbKey.Text.ToLower();
            string[] sKeys = s.Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
            string kRes = "";
            int i = 0;
            iLastFound = -1;
            foreach (cBSFview cb in BSFsources)
            {
                s = cb.sTEXT.ToLower();
                bool b = true;
                foreach (string k in sKeys)
                {
                    b = b & (s.Contains(k));
                }
                if (b)
                {
                    iLastFound = i;
                    kRes += (i + 1).ToString() + " ";
                }

                i++;
            }
            if (kRes != "")
            {
                tbError.Text = "Found at location " + kRes;
                dgvBIOS.Rows[iLastFound].Selected = true;
                dgvBIOS.CurrentCell = dgvBIOS.Rows[iLastFound].Cells[0];
            }
            else
                tbError.Text = "not found";
            tbText.Text = sLastSelectedItem;
            tbHref.Text = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RunSearch();
        }

        private void tbKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                RunSearch();
            }
        }

        private void BiosEmuSim_Shown(object sender, EventArgs e)
        {
            FlagInsecureURLs();
        }

        private void dgvBIOS_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.ColumnIndex == 0)
            {
                if(ShowColor == "HTTP")
                {
                    if (BSFsources[e.RowIndex].sHREF.Contains("http:"))
                        e.CellStyle.ForeColor = Color.Red;
                    else e.CellStyle.ForeColor = Color.Black;
                }
                if(ShowColor == "PDF")
                {
                    if (!BSFsources[e.RowIndex].sHREF.Contains(".pdf"))
                        e.CellStyle.ForeColor = Color.Red;
                    else e.CellStyle.ForeColor = Color.Black;
                }
                if(ShowColor == "DUP")
                {
                    if (BSFsources[e.RowIndex].sHREF.Contains(sDupID))
                        e.CellStyle.ForeColor = Color.Red;
                    else e.CellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void SelectURLinfo(string sID)
        {
            ShowColor = sID;
            switch(sID)
            {
                case "PDF":
                    dgvBIOS.Columns[1].HeaderText = sHeaderPDF;
                    break;
                case "HTTP":
                    dgvBIOS.Columns[1].HeaderText = sHeaderHTTP;
                    break;
                case "DUP":
                    dgvBIOS.Columns[1].HeaderText = sHeaderDUPS;
                    break;
            }
            dgvBIOS.Refresh();
        }

        private void rbMpdf_CheckedChanged(object sender, EventArgs e)
        {
            SelectURLinfo("PDF");
        }

        private void rbMhttp_CheckedChanged(object sender, EventArgs e)
        {
            SelectURLinfo("HTTP");
        }

        private void rbDups_CheckedChanged(object sender, EventArgs e)
        {
            SelectURLinfo("DUP");
        }

        private void btnMarkBad_Click(object sender, EventArgs e)
        {
            if(tbTextB.Text.Contains(sExpectName))
            {
                tbTextB.Text = tbTextB.Text.Replace(sExpectName, sBadSimName);
                PerformUpdate();
                dgvBIOS.Rows[nCurrentRow].Cells[1].Value = tbTextB.Text;
                SaveChanges();
            }
            else  if (tbTextB.Text.Contains(sNotSimName))
            {
                tbTextB.Text = tbTextB.Text.Replace(sNotSimName, sBadSimName);
                PerformUpdate();
                dgvBIOS.Rows[nCurrentRow].Cells[1].Value = tbTextB.Text;
                SaveChanges();
            }
            else
            {
                MessageBox.Show("cannot find " + sExpectName + " or " + sNotSimName);
            }
        }

        private void btnNotSim_Click(object sender, EventArgs e)
        {
            if (tbTextB.Text.Contains(sExpectName))
            {
                tbTextB.Text = tbTextB.Text.Replace(sExpectName, sNotSimName);
                PerformUpdate();
                dgvBIOS.Rows[nCurrentRow].Cells[1].Value = tbTextB.Text;
                SaveChanges();
            }
            else if (tbTextB.Text.Contains(sNotSimName))
            {
                tbTextB.Text = tbTextB.Text.Replace(sBadSimName, sNotSimName);
                PerformUpdate();
                dgvBIOS.Rows[nCurrentRow].Cells[1].Value = tbTextB.Text;
                SaveChanges();
            }
            else if (tbTextB.Text.Contains(sIsDocument))
            {
                tbTextB.Text = tbTextB.Text.Replace(sExpectName, sIsDocument);
                PerformUpdate();
                dgvBIOS.Rows[nCurrentRow].Cells[1].Value = tbTextB.Text;
                SaveChanges();
            }
            else
            {
                MessageBox.Show("cannot find " + sExpectName + " or " + sNotSimName);
            }
        }
        private void cbDups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDups == null) return;
            sDupID = cbDups.SelectedItem.ToString();
            bs.ResetBindings(false);
        }

        private void cbSelText_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = cbSelText.SelectedIndex;
            sLastSelectedItem = cbSelText.Items[i].ToString();
            tbText.Text = sLastSelectedItem;
        }

        private void btnPClean_Click(object sender, EventArgs e)
        {
            string sDirty = Utils.ClipboardGetText();
            if (sDirty == "") return;
            Utils.ReplaceUrls(ref sDirty, false);
            tbHref.Text = sDirty;
        }

        private void btnClnTxt_Click(object sender, EventArgs e)
        {
            string s = Utils.ClipboardGetText().Replace(Environment.NewLine," ").Trim();
            string t = s.Replace("  ", " ");
            tbText.Text = sLastSelectedItem + s;
        }
    }
}
