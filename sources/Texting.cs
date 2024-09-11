using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacroEditor.sources
{
    public partial class Texting : Form
    {
        public Texting()
        {
            InitializeComponent();
        
        }

        private void ReplaceText(int iStart, int iLen, string strText)
        {
            string sPrefix = tbBody.Text.Substring(0, iStart);
            string sSuffix = tbBody.Text.Substring(iStart + iLen);
            tbBody.Text = sPrefix + strText + sSuffix;
            Utils.ScrollToCaretPosition(tbBody, iStart, strText.Length);
        }

        private void TbodyInsert(string sClip)
        {
            int i = tbBody.SelectionStart;
            int j = tbBody.SelectionLength;
            ReplaceText(i, j, sClip);
        }

        private void btnCopyFrom_Click(object sender, EventArgs e)
        {
            string s = Utils.GetHPclipboard();
            string t = Properties.Settings.Default.DoJust ? Utils.JustifiedText(s) : s;
            TbodyInsert(t);
        }

        private void btnFromHP_Click(object sender, EventArgs e)
        {
            string s = Utils.GetHPclipboard().Trim();
            PasteHTML ph = new PasteHTML();
            string sOut = ph.ProcessClip(ref s);
            TbodyInsert(sOut.Trim());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbBody.Clear();
        }

        private void btnJust_Click(object sender, EventArgs e)
        {
            int i = tbBody.SelectionStart;
            int j = tbBody.SelectionLength;
            string sSa = "<div style=\"text-align: justify;\">";
            string sSb = "</div>";
            if (j == 0) return;
            string s = sSa + tbBody.SelectedText + sSb;
            s = Utils.JustSpan(s);
            if(cbFrameIT.Checked)
            {
                s = Utils.Form1CellTable(s, "");
            }
            TbodyInsert(s);
        }

        private void btnBoxIT_Click(object sender, EventArgs e)
        {
            int i = tbBody.SelectionStart;
            int j = tbBody.SelectionLength;
            if (j == 0) return;
            string s = tbBody.SelectedText;
            s = Utils.Form1CellTable(s, "");
            TbodyInsert(s);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            Utils.ShellHTML(tbBody.Text, false);
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

        private void btnRed_Click(object sender, EventArgs e)
        {
            if (SetFGcolor(tbColorCode.Text))
            {
                string s = tbColorCode.Text;    // may have changed
                Utils.AddColor(ref tbBody, s);
            }
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            Utils.AddBold(ref tbBody);
        }

        private void btnColors_Click(object sender, EventArgs e)
        {
            Utils.ShowPageInBrowser("", Utils.sHTMLcolors);
        }

        private void bltnHR_Click(object sender, EventArgs e)
        {
            Utils.InsertHR(ref tbBody);
        }

        private void btnCopyToClip_Click(object sender, EventArgs e)
        {
            Utils.CopyHTML(tbBody.Text);
        }
    }
}
