using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MacroEditor.CMarkup;

namespace MacroEditor.sources
{
    public partial class ShowDups : Form
    {

        private Color fDark;
        private List<cnDups> nDups;
        private List<CBody> cBodies;
        private List<int> iIndex = new List<int>();
        private string sSelButton = "All";
        private List<string> lMacs = new List<string>();
        private List<int> iMacs = new List<int>();
        public string strFN { get; set; }
        public int iMN { get; set; }
        private cCheckSpell SpellCheck;

        public ShowDups(ref cDupHTTP DupHTTP, ref List<CBody> rCB, ref cCheckSpell MySpellCheck)
        {
            InitializeComponent();
            strFN = "";
            iMN = -1;
            nDups = DupHTTP.nDups;
            cBodies = rCB;
            SpellCheck = MySpellCheck;
            AddSelButtons(ref gbSelect, 1, ref DupHTTP);
            foreach (cnDups cn in DupHTTP.nDups)
            {
                string[] sS = cn.sFN_N.Trim().Split(new char[] { ' ' });
                foreach (string u in sS)
                {
                    int i = lMacs.IndexOf(u);
                    if (i < 0)
                    {
                        lMacs.Add(u);
                        iMacs.Add(1);
                    }
                    else
                    {
                        iMacs[i]++;
                    }
                }
            }
            LoadAll();
        }

        private void LoadAll()
        {
            iIndex.Clear();
            for (int i = 0; i < nDups.Count; i++)
            {
                iIndex.Add(i);
                lbUrls.Items.Add(nDups[i].sUrl);
            }
            SelectUrl(0);
            tbNum.Text = iIndex.Count().ToString();
        }

        private void LoadFile(string FN)
        {
            iIndex.Clear();
            int i = 0;
            foreach(cnDups cn in nDups)
            {
                if(cn.sFN_N.Contains(FN))
                {
                    iIndex.Add(i);
                    lbUrls.Items.Add(cn.sUrl);
                }
                i++;
            }
            SelectUrl(0);
            tbNum.Text = iIndex.Count().ToString();
        }

        private void SelectUrl(int u)
        {
            if (lbUrls.Items.Count == 0) return;
            lbUrls.SelectedIndex = u;
        }
        private void AddSelButtons(ref GroupBox gb, int iDir, ref cDupHTTP DupHTTP)
        {
            int i = 0;
            int n = 0;
            foreach(int k in DupHTTP.nHyper)
            {
                n += k;
            }
            int x = 12, y = 18;
            gb.Controls.Clear();
            Button btn = new Button();
            Label lbl = new Label();
            lbl.Text = n.ToString().PadLeft(6);
            btn.Text = "All";
            btn.Width = 50;
            btn.Height = 30;
            lbl.Width = btn.Width;
            lbl.Height = 20;
            lbl.BackColor = Color.Khaki;
            if (iDir == 1)
            {
                btn.Location = new System.Drawing.Point(x + i * (btn.Width + 10), y);
                lbl.Location = new System.Drawing.Point(x + i * (lbl.Width + 10), y + lbl.Height + 10);
            }
            else
            {
                btn.Location = new System.Drawing.Point(x, y + i * (btn.Width));
                //lbl.Location = new System.Drawing.Point(x + i * (btn.Width + 10), y + btn.Height + 10);
            }
            gb.Controls.Add(btn);
            gb.Controls.Add(lbl);
            btn.Click += Btn_Click;
            fDark = btn.ForeColor;
            i++;
            foreach (string s in Utils.LocalMacroPrefix)
            {
                btn = new Button();
                lbl = new Label();
                btn.Text = s;
                btn.Width = 50;
                btn.Height = 30; 
                lbl.Width = btn.Width;
                lbl.Height = 20;
                lbl.BackColor = Color.Khaki;
                if (iDir == 1)
                {
                    btn.Location = new System.Drawing.Point(x + i * (btn.Width + 10), y);
                    lbl.Location = new System.Drawing.Point(x + i * (lbl.Width + 10), y + lbl.Height + 10);
                    int q = DupHTTP.nHyper[i - 1];
                    lbl.Text = q.ToString().PadLeft(6);
                    btn.Enabled = q > 0;
                }
                else
                {
                    btn.Location = new System.Drawing.Point(x, y + i * (btn.Width));
                }
                gb.Controls.Add(btn);
                gb.Controls.Add(lbl);
                btn.Click += Btn_Click;
                i++;
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                sSelButton = button.Text;
                lbUrls.Items.Clear();
                if( sSelButton == "All")
                {
                    LoadAll();
                }
                else
                {
                    LoadFile(sSelButton);
                }
            }
        }

        private void lbUrls_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = lbUrls.SelectedIndex;
            tbGoTo.Text = (n+1).ToString();
            string s = nDups[iIndex[n]].sFN_N;
            string[] sUrls = s.Split(new char[] { ' ' });
            if (sSelButton != "All")
            {
                string sOut = "";
                foreach(string t  in sUrls)
                {
                    if (t.Contains(sSelButton))
                        sOut += t + " ";
                }
                sUrls = sOut.Trim().Split(new char[] { ' ' });
            }
            for(int i = 0; i < sUrls.Length;i++)
            {
                s = sUrls[i];
                n = lMacs.IndexOf(s);
                int j = iMacs[n];
                if (j > 1) sUrls[i]  += " (" + j.ToString() + ")";
            }


            lbMacroID.DataSource = sUrls;
        }

        private void RunGoTo()
        {
            string s = tbGoTo.Text.Trim();
            int n = 0;
            if (!int.TryParse(s, out n))
            {
                n = 0;
            }

            if (n >= lbUrls.Items.Count)
                n = lbUrls.Items.Count - 1;
            if (n < 0) n = 0;
            lbUrls.SelectedIndex = n - 1;
        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            RunGoTo();
        }

        private void lbMacroID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = lbMacroID.SelectedIndex;
        }

        private void ShowBody(string FN, string MN)
        {
            foreach(CBody cb in cBodies)
            {
                if(cb.File == FN && cb.Number == MN)
                {
                    string s = cb.sBody;
                    Utils.ShowPageInBrowser("", s);
                    return;
                }
            }
        }

        private string[] getSs()
        {
            string s = lbMacroID.SelectedItems[0].ToString();
            int i = s.IndexOf(" ");
            if (i >= 0)
                s = s.Substring(0, i);
            string[] sS = s.Split(new char[] { '-' });
            return sS;
        }

        private void lbMacroID_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string[] sS = getSs();
            ShowBody(sS[0], sS[1]);
        }

        private void tbGoTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                RunGoTo();
            }
        }

        private void btnExitEdit_Click(object sender, EventArgs e)
        {
            string[] sS = getSs();
            strFN = sS[0];
            iMN = Convert.ToInt32(sS[1]) -1;
            this.Close();
        }

        private void LoadURLs(string sType)
        {
            iIndex.Clear();
            lbUrls.Items.Clear();
            for (int i = 0; i < nDups.Count; i++)
            {
                string s = nDups[i].sUrl;
                switch(sType)
                {
                    case "http:":
                        if (!s.Contains(sType)) continue;
                        break;
                    case "image":
                        if (!Utils.IsUrlImage(s.ToLower())) continue;   
                        break;
                }
                iIndex.Add(i);
                lbUrls.Items.Add(s);
            }
            SelectUrl(0);
            tbNum.Text = iIndex.Count().ToString();
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            LoadURLs("image");
        }

        private void btnNS_Click(object sender, EventArgs e)
        {
            LoadURLs("http:");
        }

        private void EditHelp(string s)
        {
            if (Utils.bSpellingEnabled)
                SpellCheck.EditHelpDocs(s);
            else Utils.WordpadEdit(s);
        }

        private void ShowDups_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            EditHelp("UTILS");
        }

    }
}
