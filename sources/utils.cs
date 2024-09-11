//#define SPECIAL1

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Documents;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;

namespace MacroEditor
{
    public partial class utils : Form
    {
        private string sLoc;
        private bool bDoingExample = false;
        private Font courierFont = new Font("Courier New", 8);
#if SPECIAL1
        private string sColor = "";
#endif
        int r30, c30;   // number of rows and columns in that small data grid table
        private int RowsExpected = 0;
        private int ColsExpected = 0;
        private int nGathered = 0;
        private string[] sEach;
        private List<string[]> sMyTable = new List<string[]>();
        private cCheckSpell SpellCheck;
        public utils(ref cCheckSpell MySpeller)
        {
            InitializeComponent();
            SpellCheck = MySpeller;
            sLoc = Utils.WhereExe;
#if SPECIAL1
            cbUseDelims.Checked = true;
            cbUseWhiteSpace.Checked = true;
            cbUseBorder.Checked = true;
#endif
        }

        private string strDemo6r4c = "HP#\tPegatron#\tHP_codename\tDL\r\nIPISB-CH2\t2AB5\tChicago\t\thttps://drive.google.com/file/d/1aF0dFarCTE6FCGLThelI6oSfs-09RyAs\r\nIPISB-CH\t2AB6\tCleveland-GL8\thttps://drive.google.com/file/d/1cX1Rlt8VqArq9V3N5hVy0HPGCFSputsr\r\nIPISB-CU\t2AC2\tCarmel2\t\thttps://drive.google.com/file/d/1u_bz92UD2BFwc87Qd7uavfTXa8Ga25vC\r\nHP#\tFoxconn#\tHP_codename\tDL\r\nH-Cupertino\t2ABF\tCupertino2\thttps://drive.google.com/file/d/1yMRtaFx5NJw3sb06o3oozqthz22MhoEf";
        private void FormDemo()
        {
            tbRows.Text = "6";
            tbCols.Text = "4";
            cbUseDelims.Checked = false;
            cbUseWhiteSpace.Checked = true;
            cbFormHL.Checked = true;
            cbUseBorder.Checked = true;
            CSendNotepad SendNotepad = new CSendNotepad();
            SendNotepad.PasteToNotepad(strDemo6r4c);    // this pastes to Clipboard
            btnFillCol.PerformClick();
        }

        private void ClearTable()
        {
            sMyTable = new List<string[]>();
            tbScratch.Text = "";
        }

        private void EnableRC()
        {
            RowsExpected = 0;
            ColsExpected = 0;
            nGathered = 0;
            btnFillRow.Enabled = true;
            btnFillCol.Enabled = true;
            ClearTable();
        }

        private void MakeResult()
        {
            string strUrl = Utils.dRef(tbURL.Text);
            string strTmp = tbTEXT.Text;
            if (strTmp == "") strTmp = strUrl;
            if (rbIsImg.Checked)
                tbResult.Text = Utils.AssembleIMG(strUrl, false);
            else tbResult.Text = Utils.FormUrl(strUrl, strTmp);
            if (bDoingExample)
            {  //<table border="1" width="50%"><tr><td>atest</td></tr></table>
                string s = "Your <b>example</b> follows below this horizontal line<hr>";
                s += tbResult.Text;
                s += "<hr>Your example is above.  ";
                s += "<font color='#006400'>I am DarkGreen and <b>bold</b></font><br>";
                s += "Put me in a <table border='1' width='%50'><tr><td>box if you are <b>BOLD</b></td></tr></table>";
                s += "<br>Click the adjacent 'Show In Browser' to see it";
                tbScratch.Text = s;
            }
        }

        private void btnCvt_Click(object sender, EventArgs e)
        {
            MakeResult();
        }



        private void btnClip_Click(object sender, EventArgs e)
        {
            if (tbResult.Text == "") return;
            Clipboard.SetText(tbResult.Text);
        }

        private void btnClrHREF_Click(object sender, EventArgs e)
        {
            tbTEXT.Text = "";
        }

        private void btnClearURL_Click(object sender, EventArgs e)
        {
            tbURL.Text = "";
        }

        private void btnClrScratch_Click(object sender, EventArgs e)
        {
            EnableRC();
        }

        private void showExampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bDoingExample = true;
            string Samp1 = "Click to see how to find the release date of an hp laptop";
            string Samp2 = "https://www.lifewire.com/how-to-find-the-serial-number-of-an-hp-laptop-5189844#:~:text=You%20can%20determine%20your%20laptop's,week%20of%20the%20year%202020.";
            if (rbLnkToImg.Checked)
            {
                Samp1 = "Click to see an Expert Icon";
                Samp2 = "https://h30434.www3.hp.com/html/@029DD5B9D9DE7F854E52DDBD91AAAAD9/rank_icons/expert.png";
            }
            if (rbIsImg.Checked)
            {
                Samp1 = "This field is ignored";
                Samp2 = "https://h30434.www3.hp.com/html/@029DD5B9D9DE7F854E52DDBD91AAAAD9/rank_icons/expert.png";
            }
            tbTEXT.Text = Samp1;
            tbURL.Text = Samp2;
            MakeResult();
            bDoingExample = false;
        }

        private void ShowBrowser(string s)
        {
            if (s == "") return;
            Utils.ShowPageInBrowser("", s);
        }

        private void btnShowBrowser_Click(object sender, EventArgs e)
        {
            ShowBrowser(tbResult.Text);
        }


        private int RefreshTable()
        {
            int i = cbUseBorder.Checked ? 1 : 0;
            try
            {
                r30 = Convert.ToInt32(tbRows.Text);
                c30 = Convert.ToInt32(tbCols.Text);
            }
            catch
            {
                ClearDGV();
                return 1;
            }
            int n = r30 * c30;
            tbScratch.Text = Utils.FormTable(r30, c30, true, i);
            return n;
        }

        private void btnApplyTab_Click(object sender, EventArgs e)
        {
            int n = RefreshTable();
            if (cbPreFill.Checked && (n <= 30))
            {
                dgv.Rows.Clear();
                for (int i = 0; i < n; i++)
                {
                    //DataGridViewRow row = new DataGridViewRow();
                    dgv.Rows.Add(Utils.strFillSubscript(r30, c30, i), "");
                }

            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            ShowBrowser(tbScratch.Text);
        }

        private void cbPreFill_CheckStateChanged(object sender, EventArgs e)
        {
            dgv.Visible = cbPreFill.Checked;
        }

        private int NumLinesInDGV()
        {
            int n = 0;
            int NumEntries = 0;
            int i = 0;
            int[] nCnt = new int[c30*r30];
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (null == row.Cells[1].Value) break;
                string s = row.Cells[1].Value.ToString();
                string[] ss = s.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                nCnt[NumEntries] = ss.Length;
                n = Math.Max(n, ss.Length);
                NumEntries++;
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                string sNL = Environment.NewLine + "&nbsp;";
                if (null == row.Cells[1].Value) break;
                string s = row.Cells[1].Value.ToString();
                int nDif = n - nCnt[i];
                if(nDif > 0)
                {
                    for(int j = 0; j < nDif;j++)
                    {
                        s += sNL;
                    }
                    dgv.Rows[i].Cells[1].Value = s;
                }
                i++;
            }
            return NumEntries;
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                if(RowsExpected>0)
                {
                    ClearTable();
                    FillTable();
                    return;
                }
                return;
            }
            RefreshTable();
            int n = NumLinesInDGV();
            string sBig = tbScratch.Text;
            string s1, s2;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (null == row.Cells[0].Value) break;
                s1 = row.Cells[0].Value.ToString();
                if (null == row.Cells[1].Value) break;
                s2 = row.Cells[1].Value.ToString();
                sBig = sBig.Replace(s1, s2== "" ? "&nbsp;" : s2);
            }
            tbScratch.Text = sBig;
        }

        private void btnCopyScratch_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbScratch.Text);
        }

        private void btnShowScratch_Click(object sender, EventArgs e)
        {
            ShowBrowser(tbScratch.Text);
        }

        private void btnFillRow_Click(object sender, EventArgs e)
        {
            btnFillCol.Enabled = false;
            nGathered = GatherClipboardText(cbRemE_tab.Checked);
            if (ColsExpected == 0) ColsExpected = nGathered;
            FillTable();
        }
        

        private void btnFillCol_Click(object sender, EventArgs e)
        {
            btnFillRow.Enabled = false;
            nGathered = GatherClipboardText(cbRemE_tab.Checked);
            if (RowsExpected == 0) RowsExpected = nGathered;
            FillTable();
        }

        private void AddTable(string[] sOne)
        {
            int i = 0, n = RowsExpected;
            string s;
            if (n == 0) n = ColsExpected;
            if (n != nGathered)
            {
                DialogResult Res1 = MessageBox.Show("wrong number of items for row or column","Click Yes to supply missing",MessageBoxButtons.YesNo);
                if(DialogResult.No == Res1) return;
            }
            string[] sNew = new string[n];
            for(int m = 0; m < n; m++)
            {
                if (m < nGathered) s = sOne[m];
                else s = "&nbsp";
                string t = s.Trim();
                if(cbAddNLtoHTML.Checked || cbFormHL.Checked)
                {
                    string u = t.ToLower();
                    int j = u.IndexOf("http:");
                    if (j < 0) j = u.IndexOf("https:");
                    if(j >= 0)
                    {                        
                        if(cbAddNLtoHTML.Checked && j!= 0)
                        {
                            t = t.Insert(j, "<br>"); // no need for new line if first word of a line
                            j += 4;
                        }
                        if(cbFormHL.Checked)
                        {
                            int k = Utils.LengthURL(ref t, j);
                            u = t.Substring(j, k);
                            Debug.Assert(k > 0, "cannot find url");
                            string v = Utils.FormUrl(u, "");
                            u= t.Replace(u, v);
                            t = u;
                        }
                    }
                }
                if(cbRemE_tab.Checked)
                {
                    if (t == "") continue;
                    sNew[i] = t;
                    i++;
                }
                else
                {
                    if (t == "") t = "&nbsp;";
                    sNew[i] = t;
                    i++;
                }
            }
            if (i == n) sMyTable.Add(sNew);
            else
            {
                string[] truncatedArray = new string[i];
                Array.Copy(sNew, truncatedArray, i);
                sMyTable.Add(truncatedArray);
            }
            FormTable();
        }

        private string[] DoReqSplit(string s)
        {
            string t = s.Trim();
            StringSplitOptions sso = cbRemE_tab.Checked ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
            if (cbUseDelims.Checked && cbUseWhiteSpace.Checked)
            {
                return t.Split(new char[] { ' ', '\t', ',' }, sso);
            }
            if (cbUseDelims.Checked)
            {
                return t.Split(new char[] { ',' }, sso);
            }
            if (cbUseWhiteSpace.Checked)
            {
                return t.Split(new char[] { ' ', '\t' }, sso);
            }
            return null;
        }

        private void FillTable()
        {
            if (cbUseDelims.Checked || cbUseWhiteSpace.Checked)
            {
                if (sEach.Length == 0) return;
                string[] sFirst = DoReqSplit(sEach[0]);
                int n = sFirst.Length;
                List<string[]> list = new List<string[]>();
                foreach (string s in sEach)
                {
                    sFirst = DoReqSplit(s);
                    if (n != sFirst.Length)
                    {
                        MessageBox.Show("missing delimiter in your paste");
                        return;
                    }
                    list.Add(sFirst);
                }
                string[] sOne = new string[list.Count];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        sOne[j] = list[j][i];
                    }
                    AddTable(sOne);
                }
            }
            else AddTable(sEach);
        }

        private void FormTable()
        {
            string sPre = "<td>";
            string sSuf = "</td>";
            string sStrCol = "<tr>";
            string sEndCol = "</tr>";
            string sSt = cbUseBorder.Checked ? "<table border='1' width='50%'>" : "<table width='50%'>";
            string sSe = "</table>";
            string sOut = sSt;
            if (RowsExpected > 0)
            {
                int cE = sMyTable.Count;
                if (sMyTable.Count == 0) return;
                if (sMyTable[0].Length == 0) return;
                int rE = RowsExpected;
                for (int r = 0; r < RowsExpected; r++)
                {
#if SPECIAL1
                    int i = 0;
#endif
                    sOut += sStrCol;
#if SPECIAL1
                    sColor = sMyTable[cE - 1][r];
#endif
                    for (int c = 0; c < cE; c++)
                    {
                        string s = sMyTable[c][r];
#if SPECIAL1
                        s = FormSpecial(i, s);
                        i++;
#endif
                        sOut += sPre + s + sSuf;
                    }
                    sOut += sEndCol;
                }
                sOut += sSe;
            }
            else  // columns
            {
                int rE = sMyTable.Count;
                int cE = ColsExpected;
                for (int r = 0; r < rE; r++)
                {
                    sOut += sStrCol;
                    for (int c = 0; c < cE; c++)
                    {
                        string s = sMyTable[r][c];
                        sOut += sPre + s + sSuf;
                    }
                    sOut += sEndCol;
                }
                sOut += sSe;
            }
            tbScratch.Text = sOut;
        }

        // if checked b=true then remove unwanted lines
        private int GatherClipboardText(bool b)
        {
            string sIn = Utils.ClipboardGetText();
            // remove emptys is needed because \r\n is newline
            if (b) sEach = sIn.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            else sEach = sIn.Split(new[] { '\n', '\r' });
            return sEach.Length;
        }

        private void btnClearTab_Click(object sender, EventArgs e)
        {
            EnableRC();
        }

        private void btnShowTab_Click(object sender, EventArgs e)
        {
            ShowBrowser(tbScratch.Text);
        }

        private void ClearDGV()
        {
            dgv.Rows.Clear();
            tbCols.Text = "1";
            tbRows.Text = "1";
        }

        private void btnCleardgv_Click(object sender, EventArgs e)
        {
            ClearDGV();
        }

        private void utils_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            EditHelp("UTILS");
        }

        private void btnApplyList_Click(object sender, EventArgs e)
        {
            CopyListToScratch();
        }

        private void CopyListToScratch()
        {
            string sOut = rbBullet.Checked ? "<ul>" : "<ol>";
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (null == row.Cells[1].Value) break;
                string s = row.Cells[1].Value.ToString();
                sOut += "<li>" + s + "</li>";
            }
            sOut += rbBullet.Checked ? "</ul>" : "</ol>";
            //<table border='1'><tr><td>R0C0_AAAA</td></tr></table>
            if (cbFrameList.Checked)
            {
                sOut = "<table border='1' width='50%'><tr><td>" + sOut + "</td></tr></table>";
            }
            tbScratch.Text = sOut;
        }

        private void btnClrList_Click(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
        }

        private void bltnShowList_Click(object sender, EventArgs e)
        {
            ShowBrowser(tbScratch.Text);
        }

        private void btnFillList_Click(object sender, EventArgs e)
        {
            nGathered = GatherClipboardText(cbRemE_list.Checked);
            dgv.Rows.Clear();
            foreach (string s in sEach)
            {
                dgv.Rows.Add(dgv.Rows.Count + 1, s);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                dgv.Rows.Clear();
                dgv.Visible = true;
                dgv.AllowUserToAddRows = true;
                dgv.AllowUserToDeleteRows = true;
            }
            else
            {
                dgv.Rows.Clear();
                dgv.Visible = cbPreFill.Checked;
                dgv.AllowUserToAddRows = false;
                dgv.AllowUserToDeleteRows = false;
            }
            if(tabControl1.SelectedIndex == 2)
            {
                tbColorResult.ForeColor = tbTextToColor.ForeColor;
                tbColorResult.BackColor = tbTextToColor.BackColor;
            }
        }

        private void btnAdd1New_Click(object sender, EventArgs e)
        {
            Utils.AddNL(ref tbScratch, 1);
        }

        private void btnAdd2New_Click(object sender, EventArgs e)
        {
            Utils.AddNL(ref tbScratch, 2);
        }

        private void btnInvertNL_Click(object sender, EventArgs e)
        {
            Utils.SwapNL(ref tbScratch);
        }

        private void cbUseBorder_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbUseBorder.Checked)
            {
                tbScratch.Text = tbScratch.Text.Replace("<table>", "table border='1'>");
            }
            else
            {
                tbScratch.Text = tbScratch.Text.Replace("<table border='1' width='50%'>", "<table width='50%'>");
            }
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            //string htmlColor = ColorTranslator.ToHtml(selectedColor);
        }

        private void btnShowB_Click(object sender, EventArgs e)
        {
            ShowBrowser(tbColorResult.Text);
        }

        private void btnBackGround_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color selectedColor = colorDialog.Color;
                    tbColorResult.BackColor = selectedColor;
                }
            }
        }

        private void btnForeColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color selectedColor = colorDialog.Color;
                    tbColorResult.ForeColor = selectedColor;
                }
            }
        }

        private void btnSetFont_Click(object sender, EventArgs e)
        {
            using (FontDialog fontDialog = new FontDialog())
            {
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    Font selectedFont = fontDialog.Font;
                    tbColorResult.Font = selectedFont;
                }
            }
            tbColorResult.Text = tbTextToColor.Text;
        }

        private void btnApplyCol_Click(object sender, EventArgs e)
        {
            tbColorResult.Text = tbTextToColor.Text;
            HTMlColorPicker cp = new HTMlColorPicker(tbColorResult.Font);
            cp.ShowDialog();
            if(cp.WantedColors != null)
            {
                tbColorResult.ForeColor = cp.WantedColors.ForeColor;
                tbColorResult.BackColor = cp.WantedColors.BackColor;
            }
            cp.Dispose();
        }

        private void btnShowCode_Click(object sender, EventArgs e)
        {
            
            string s = Utils.ApplyColors1(ref tbColorResult, cbUseSpace.Checked, cbBoxHTML.Checked);
            tbScratch.Text =  s;
        }

        private void btnDemoTB_Click(object sender, EventArgs e)
        {
            FormDemo();
        }

        private bool GetRowCol(ref int row, ref int col)
        {
            int r, c;
            try
            {
                r = Convert.ToInt32(tbRows.Text);
                c = Convert.ToInt32(tbCols.Text);
            }
            catch
            {
                row = 1;
                col = 1;
                return false;
            }
            row = r;
            col = c;
            return true;
        }

        private void FormLRTD(string sIn)
        {
            cbUseWhiteSpace.Checked = true;
            cbUseDelims.Checked = false;
            sEach = sIn.Split(new[] { '\n', '\r' });
            btnFillRow.Enabled = false;
            nGathered = sEach.Length;
            if (RowsExpected == 0) RowsExpected = nGathered;
            FillTable();
        }


        private void SetRowsFromColumns(int nItems, int nColumnsWanted)
        {
            int nRows = Utils.MakeDivisible(nItems, nColumnsWanted) / nColumnsWanted;
            Debug.Assert(nRows > 0,"Problem with bean counter");
            tbRows.Text = nRows.ToString();
            tbCols.Text = nColumnsWanted.ToString();
        }

        private void btnLRTD_Click(object sender, EventArgs e)
        {
            int Row=1, Col=1;
            if(GetRowCol(ref Row, ref Col))
            {
                string sIn = Utils.ClipboardGetText();
                string[] sOt = sIn.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                int nActual = sOt.Length;
                int nExpect = Row * Col;
                if (nExpect >= nActual)
                {
                    int n = 0;
                    sIn = "";
                    for(int i = 0; i < Row; i++)
                    {
                        for(int j = 0; j < Col; j++)
                        {
                            string s = "";
                            if (n >= nActual)
                            {
                                s = "&nbsp;";
                            }
                            else s = sOt[n];
                            sIn += s + " ";
                            n++;
                        }
                        if(n < nExpect)
                            sIn += "\n";
                    }
                    FormLRTD(sIn);
                }
                else
                {
                    SetRowsFromColumns(nActual, 10);
                }
            }
        }

        private void btnNewC_Click(object sender, EventArgs e)
        {
            string sFindSize = "";
            string s;
            string sWa = cbBold.Checked ? "<b>" : "";
            string sWb = cbBold.Checked ? "</b>" : "";

            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                sFindSize = clickedButton.Text;
            }
            else return;
            cbUseSpace.Checked = true;
            tbColorResult.Font = courierFont;
             
            s =  "<span style=\"font-family: 'Courier New'; font-size: " + sFindSize + ";\">" + sWa + tbTextToColor.Text.Replace(" ", "&nbsp;") + sWb +"</span>";
            if (cbBoxCol.Checked)
            {
                s = "<table border='1'><tr><td>" + s + "</td></tr></table>";
            }
            tbColorResult.Text = s;
            tbScratch.Text = tbColorResult.Text;
        }

        private void btnFrmClip(object sender, EventArgs e)
        {
            tbTextToColor.Text = Clipboard.GetText();
        }

        private void btnClrCol_Click(object sender, EventArgs e)
        {
            tbTextToColor.Text = "";
            tbColorResult.Text = "";
            tbTextToColor.Font = gbTxtCol.Font;
        }

        private void EditHelp(string s)
        {
            if (Utils.bSpellingEnabled)
                SpellCheck.EditHelpDocs(s);
            else Utils.WordpadEdit(s);
        }


#if SPECIAL1
        private string FormSpecial(int i, string s)
        {
            string r = s;
            // expecting Cyan' ForeGround ForeGroundBold Background BackgroundBold '#00FFFF
            // from ChatGPT HTML palette list replacing ":" with the above words in the quote '
            if (i == 0) return s;
            if(i == 5) // note that HP forum no longer allows background colors
            {
                //return "&lt;span style='background-color: " + sColor + "; color: white;'&gt;" + "&lt;b&gt;ForeGroundBold&lt;b&gt;" + "&lt;/span&gt;"; //  used with white
                return "&lt;font color='" + sColor + "'&gt;" +"ForeGround" + "&lt;/font&gt;"; // used with 'black'
            }

            switch (i)
            {
                case 1: r = "<font color='" + sColor + "'>" + s + "</font>";
                    break;
                case 2: r  = "<font color='" + sColor + "'><b>" + s + "</b></font>";
                    break;
                    // below white was black (or viceversa) for first run of SPECIAL1 and
                case 3: r = "<span style='background-color: " + sColor + "; color: black;'>" + s + "</span>";
                    break;   
                case 4: r = "<span style='background-color: " + sColor + "; color: black;'><b>" + s + "</b></span>";
                    break;
            }
            return r;
        }
#endif
    }
}
/* the below was used with 'SPECIAL1'
Black: #000000
White: #FFFFFF
Red: #FF0000
Green: #008000
Blue: #0000FF
Yellow: #FFFF00
Cyan: #00FFFF
Magenta: #FF00FF
Silver: #C0C0C0
Gray: #808080
Maroon: #800000
Olive: #808000
Purple: #800080
Teal: #008080
Navy: #000080
Orange: #FFA500
Aquamarine: #7FFFD4
Turquoise: #40E0D0
Lime: #00FF00
Fuchsia: #FF00FF
Indigo: #4B0082
Violet: #EE82EE
Pink: #FFC0CB
Peach: #FFDAB9
Beige: #F5F5DC
 */
/*
 common emoji 
&#x1f60D;
&#x1f60E;
&#x1f60F;
&#x1f610;
&#x1f611;
&#x1f612;
&#x1f613;
&#x1f614;
&#x1f615;
&#x1f616;
&#x1f617;
&#x1f618;
&#x1f619;
&#x1f61A;
&#x1f61B;
&#x1f61C;
&#x1f61D;
&#x1f61E;
&#x1f61F;
&#x1f620;
&#x1f621;
&#x1f622;
&#x1f623;
&#x1f624;
&#x1f625;
&#x1f626;
&#x1f627;
&#x1f628;
&#x1f629;
&#x1f62A;
&#x1f62B;
&#x1f62C;
&#x1f62D;
&#x1f62E;
&#x1f62F;
&#x1f630;
&#x1f631;
&#x1f632;
&#x1f633;
&#x1f634;
&#x1f635;
&#x1f636;
&#x1f637;
*/