using HtmlAgilityPack;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace MacroEditor
{
    public partial class SetText : Form
    {
        private string sLoc;
        private bool bBoxed = false;
        private string strBoxed = "";
        private int CurrentDemo = 0, TotalDemos = 4;
        private string sInfoDemo;
        //0: box url, 1: list, 2:image url, 3: image
        public string strResultOut { get; set; }
        public SetText(string strIn)
        {
            InitializeComponent();
            tbPrefix.Text = "";
            tbSuffix.Text = "";
            tbSelectedItem.Text = strIn;
            tbResult.Text = "";
            tbRawUrl.Text = "";
            gpTable.Enabled = (strIn == "");
            sLoc = Utils.WhereExe;
            cbPreFill.Checked = Properties.Settings.Default.FillAlpha;
            sInfoDemo = lbInfoTest.Text;
        }

        private void SetNextDemo()
        {
            CurrentDemo++;
            if (CurrentDemo == TotalDemos+1) CurrentDemo = 0;
            if (CurrentDemo == 0)
            {
                btnDemo.Text = "Click for demo";
                lbInfoTest.Text = sInfoDemo;
                ClearAll();
                btnTestD.Visible = false;
            }
            else
            {
                string s = (CurrentDemo == 4) ? ":click to exit demo" : ": click for demo" + (CurrentDemo + 1).ToString();
                btnDemo.Text = "Demo " + CurrentDemo.ToString() + s;
                btnTestD.Visible = true;
                FormDemo(CurrentDemo - 1);
                FormObject();
            }
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            strResultOut = tbResult.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            strResultOut = "";
            this.Close();
        }

        private string GetFormattedURL(string s)
        {
            if(s.Contains(Utils.sIsAlbum)) // is an image in an album
            {
                int i = cbSizeImage.SelectedIndex;
                if (i > 0)
                {
                    s+= Utils.sHasSize + cbSizeImage.SelectedItem.ToString();
                }
            }
            return s;
        }
        private void FormObject()
        {
            string strOut = "";
            string strTmp = tbRawUrl.Text.Trim();
            string sClean = cbCleanUrl.Checked ? Utils.dRef(strTmp) : strTmp;
            string sFmtRaw = GetFormattedURL(sClean);
            string strUC = tbRawUrl.Text.ToUpper();
            bool bHaveHTML = strUC.Contains("HTTPS:") || strUC.Contains("HTTP:");
            string strResult = "";
            tbResult.Text = "";

            strTmp = tbPrefix.Text.Trim();
            if (strTmp != "")
            {
                strOut = strTmp + " ";
            }
            if ((bHaveHTML))
            {
                if (cbMakeIMG.Checked)
                {
                    strOut += Utils.AssembleIMG(sFmtRaw, false);
                }
                else
                {
                    strOut += Utils.FormUrl(sFmtRaw, tbSelectedItem.Text.Trim());
                }
                strTmp = tbSuffix.Text.Trim();
                if (strTmp != "")
                {
                    strOut += " " + strTmp;
                }
            }
            else strOut = tbSelectedItem.Text.Trim();

            strResult = strOut.Trim();
            if(rbNoBox.Checked) tbResult.Text = strResult;
            else
            {
                tbResult.Text = rbSqueeze.Checked ? Utils.Form1CellTable(strResult, GetBoxWidth()) : Utils.Form1CellTableP(strResult,GetBoxWidth());
            }
            cbSizeImage.Visible = tbRawUrl.Text.Contains(Utils.sIsAlbum);
        }

        private string GetBoxWidth()
        {
            string sRtn = "";
            foreach(RadioButton rb in gbPCTbw.Controls)
            {
                if (rb.Checked)
                {
                    if (rb.Name != "rb0pct")
                        sRtn = rb.Text;
                    break;
                }
            }
            return sRtn;
        }

        private void btnApplyText_Click(object sender, EventArgs e)
        {
            FormObject();
        }

        private void RunBrowser()
        {
            string strTemp = tbResult.Text;
            if (strTemp == "" || bBoxed) strTemp = strBoxed;
            if (strTemp == "") return;
            Utils.ShowPageInBrowser("", strTemp);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            RunBrowser();
        }

        private void btnApplyTab_Click(object sender, EventArgs e)
        {
            int r, c;
            try
            {
                r = Convert.ToInt32(tbRows.Text);
                c = Convert.ToInt32(tbCols.Text);
            }
            catch
            {
                tbRows.Text = "1";
                tbCols.Text = "1";
                return;
            }
            tbResult.Text = Utils.FormTable(r, c, cbPreFill.Checked, 1);
        }

        private void SetText_Shown(object sender, EventArgs e)
        {
            tbRawUrl.Focus();
        }

        private void SetText_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.FillAlpha = cbPreFill.Checked;
            Properties.Settings.Default.Save();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbResult.Text = "";
        }


        private void FormDemo(int i)
        {
            switch (i)
            {
                case 0:
                    tbPrefix.Text = "You can ";
                    tbSuffix.Text = " to visit google";
                    tbSelectedItem.Text = "CLICK HERE";
                    tbRawUrl.Text = "https://www.google.com";
                    rbFitBox.Checked = true;
                    cbMakeIMG.Checked = false;
                    lbInfoTest.Text = "Click phrase to load google site";
                break;
                case 1:
                    tbPrefix.Text = "";
                    tbSuffix.Text = "";
                    tbSelectedItem.Text = "Some country codes:\r\n#A2M - Iceland - English\r\n#A2N - Saudi Arabia - Arabic'English";
                    tbRawUrl.Text = "";
                    rbSqueeze.Checked = true;
                    cbMakeIMG.Checked = false;
                    lbInfoTest.Text = "Show a small table";
                break;
                case 2:
                    tbPrefix.Text = "You can ";
                    tbSuffix.Text = " to see what to do with old Dell systems";
                    tbSelectedItem.Text = "Click Here";
                    tbRawUrl.Text = "https://h30434.www3.hp.com/t5/image/serverpage/image-id/368919i27FD57F55C2EC433";
                    rbSqueeze.Checked = true;
                    cbMakeIMG.Checked = false;
                    lbInfoTest.Text = "Click the phrase to see the image" +
                        Environment.NewLine + "You can set image size";
                    break;
                case 3:
                    tbPrefix.Text = "This is what linux thinks of Windows XP<br>";
                    tbSuffix.Text = "<br><br>Yes, Linux does suck!";
                    tbSelectedItem.Text = "";
                    tbRawUrl.Text = "https://h30434.www3.hp.com/t5/image/serverpage/image-id/372002iFA9364C9FF20F330";
                    rbNoBox.Checked = true;
                    cbMakeIMG.Checked = true;
                    cbSizeImage.SelectedIndex = 0;
                    lbInfoTest.Text = "Image is displayed inline with text" +
                        Environment.NewLine + "You can set image size";
                    break;
            }
        }

        private void btnDemo_Click(object sender, EventArgs e)
        {
            SetNextDemo();
        }

        private void ClearAll()
        {
            tbPrefix.Text = "";
            tbSuffix.Text = "";
            tbSelectedItem.Text = "";
            tbRawUrl.Text = "";
            cbMakeIMG.Checked = false;
            cbSizeImage.Visible = false;
        }

        private void btnTestD_Click(object sender, EventArgs e)
        {
            RunBrowser();
        }

        private void cbSizeImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormObject();
        }

        private void btnClrDemo_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
    }
}
