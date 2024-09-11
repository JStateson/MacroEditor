using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Ink;
using System.Xml.Linq;

/*
 * 
 * associate macro "Key Replacement", "HW" with the contents of the clipboardl
 * the existing macro cannot be removed or deleted when associated
 * @clipboard@ is replaced with the clipboard
 * @arg0@  @arg1@ etc are argruments available.
 * 
 * */


namespace MacroEditor.sources
{
    public partial class associate : Form
    {
        public List<cMacroEach> RFlist = new List<cMacroEach>();
        private bool bSwapNL = true;
        private int CurrentValidInx = -1;
        private bool bTableExists = false;
        public class cEachTbMn
        {
            public int nLoc { get; set; }
            public bool bFound { get; set; }
            public string sName { get; set; }    // macro name
            public string sCode { get; set; }    // macro substitution code
        }
        public List<cEachTbMn> mHaveTable = new List<cEachTbMn>();
        public bool bChanged { get; set; }

        public string[] ClipboardMacros { get; set; }
        private int LookupName(string s)
        {
            int n = 0;
            foreach(cMacroEach me in RFlist)
            {
                if (me.sName == s) return n;
                n++;
            }
            return -1;
        }
        private void LoadTable()
        {
            int n = 0;
            cEachTbMn et;
            foreach (string line in File.ReadLines(Utils.FNtoPath("Ass")))
            {
                string[] sS = line.Split(',');
                et = new cEachTbMn();
                et.sName = sS[0].Trim();
                et.sCode = sS[1].Trim();
                et.nLoc = LookupName(et.sName);
                et.bFound = et.nLoc != -1;
                if (et.bFound) n++;
                mHaveTable.Add(et);
            }
            ClipboardMacros = new string[n];
            n = 0;
            foreach(cEachTbMn et1 in mHaveTable)
            {
                if(et1.bFound)ClipboardMacros[n] = et1.sName;
                n++;
            }
            dgv.DataSource = mHaveTable;
            dgv.Columns[0].HeaderText = "Loc";
            dgv.Columns[1].HeaderText = "Found";
            dgv.Columns[2].HeaderText = "Macro Name";
            dgv.Columns[3].HeaderText = "Sub Code";
            dgv.Columns[0].FillWeight = 16;
            dgv.Columns[1].FillWeight = 20;
            dgv.Columns[3].FillWeight = 40;
        }

        public associate()
        {
            InitializeComponent();
            int n = Utils.ReadFile("RF", ref RFlist);
            bTableExists = n > 0;
            if (!bTableExists) return;
            bTableExists = File.Exists(Utils.FNtoPath("Ass"));
            if(!bTableExists) return;
            LoadTable();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int n = (int) dgv.Rows[e.RowIndex].Cells[0].Value;
            AllowButtons(n >= 0);
            if (n < 0)
            {
                tbEdit.Text = "";
                return;
            }
            tbEdit.Text = RFlist[n].sBody;
            CurrentValidInx = n;
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            if(bSwapNL)
            {
                tbEdit.Text = tbEdit.Text.Replace("<br>", Environment.NewLine);
            }
            else
            {
                tbEdit.Text = tbEdit.Text.Replace(Environment.NewLine, "<br>");
            }
            bSwapNL = !bSwapNL;
        }

        private void AllowButtons(bool b)
        {
            btnSave.Enabled = b;
            btnShow.Enabled = b;
        }

        private void SaveChanges()
        {
            string strOut = "";
            
            foreach(cMacroEach me in RFlist)
            {
                strOut += me.sName + Environment.NewLine;
                strOut += me.sBody + Environment.NewLine;
            }
            Utils.WriteAllText(Utils.FNtoPath("RF"), strOut);
            bChanged = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string s = tbEdit.Text.Replace(Environment.NewLine, "<br>");
            RFlist[CurrentValidInx].sBody = s;
            SaveChanges();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            string s = tbEdit.Text;
            string t = Clipboard.GetText();
            s = s.Replace("@clipboard@", t);
            Utils.ShowPageInBrowser("", s);
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            tbClip.Text = Clipboard.GetText();
        }
    }
}
