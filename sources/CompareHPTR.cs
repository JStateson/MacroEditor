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
using System.Windows.Media.Converters;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Xml.Linq;
using Microsoft.Office.Interop.Word;

namespace MacroEditor.sources
{
    public partial class CompareHPTR : Form
    {
        private int iLocBody = 0;
        private int iLocName = 0;
        private int iLenBody = 0;
        private int iLenName = 0;
        private int iNextOne = -1;
        public List<dgvStruct> HTTPDataTable;
        public string WhichTable = "HP";
        public CompareHPTR(ref List<dgvStruct> rHTTPDataTable, string ID)
        {
            InitializeComponent();
            HTTPDataTable = rHTTPDataTable;
            WhichTable = ID;
            switch(ID)
            {
                case "HP":
                    gbTR.Text = "Transfer Macros";
                    break;
                case "HTTP":
                    gbTR.Text = "Original HP Macros";
                    break;
            }
            Init();
            dgvDiff.DataSource = mDiffList;
            dgvDiff.Columns[0].HeaderText = "N";
            dgvDiff.Columns[1].HeaderText = "Name";
            dgvDiff.Columns[2].HeaderText = "Body";
            dgvDiff.Columns[0].FillWeight = 36;

            foreach (DataGridViewColumn column in dgvDiff.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }


        int nActualHP;
        int nActualTR;
        public class cEachTbMn
        {
            public int nLoc { get; set; }
            public bool bMN { get; set; }    // macro name difference
            public bool bMB { get; set; }    // macro bod difference

        }
        public List<cEachTbMn> mDiffList = new List<cEachTbMn>();


        public List<cMacroEach> HPlist;
        public List<cMacroEach> TRlist;


        private void FormDiffTable()
        {
            cMacroEach cHP;
            cMacroEach cTR;
            bool bM, bB;
            for (int i = 0; i < Utils.HPmaxNumber; i++)
            {
                cHP = HPlist[i];
                cTR = TRlist[i];
                bB = cHP.sBody != cTR.sBody;
                bM = cHP.sName != cTR.sName;
                if (bB || bM)
                {
                    cEachTbMn etm = new cEachTbMn();
                    etm.bMB = bB;
                    etm.bMN = bM;
                    etm.nLoc = i + 1;
                    mDiffList.Add(etm);
                }
            }
        }

        public void Init()
        {
            TRlist = new List<cMacroEach>();
            HPlist = new List<cMacroEach>();
            if (WhichTable == "HP")
            {
                nActualTR = LoadFile("TR", ref TRlist);
            }
            else
            {
                foreach(dgvStruct dgv in HTTPDataTable)
                {
                    cMacroEach cME = new cMacroEach();
                    cME.sBody = dgv.sBody;
                    cME.sName = dgv.MacName;
                    TRlist.Add(cME);
                }
            }
            nActualHP = LoadFile("HP", ref HPlist);
            FormDiffTable();
        }




        private int LoadFile(string strFN, ref List<cMacroEach> ThisList)
        {
            int i;
            int n = Utils.ReadFile(strFN, ref ThisList);
            if (n == 0) return 0;
            i = Utils.HPmaxNumber - n;
            if (i > 0)
            {
                for (int j = 0; j < i; j++)
                {
                    cMacroEach cME = new cMacroEach();
                    cME.sName = "";
                    cME.sBody = "";
                    ThisList.Add(cME);
                }

            }
            return n;
        }



        private void dgvDiff_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int nSel = mDiffList[e.RowIndex].nLoc - 1;
            tbHPbody.Text = HPlist[nSel].sBody;
            tbHPname.Text = HPlist[nSel].sName;
            tbTRbody.Text = TRlist[nSel].sBody;
            tbTRname.Text = TRlist[nSel].sName;
            RunFirst();
        }


        private void RunBrowser(string s, string h)
        {
            if (s == "") return;
            
            Utils.ShowPageInBrowser("", h + s );
        }

        private int FindFirstDiff(ref string s1, ref string s2)
        {
            int minLength = Math.Min(s1.Length, s2.Length);
            for (int i = 0; i < minLength; i++)
            {
                if (s1[i] != s2[i])
                {
                    return i;
                }
            }

            if (s1.Length != s2.Length)
            {
                return minLength;
            }

            return -1; // Strings are identical
        }

        private int FindFirstDifference(string s1, string s2, ref int iLen)
        {
            int i = FindFirstDiff(ref s1, ref s2);
            if (i < 0) return i;
            string s = s1;
            if (s1.Length < s2.Length) s = s2;
            int j = s.IndexOf(" ", i);
            if (j < 0) j = 2;
            if (j > 10) j = 10;
            iLen = j;
            return i;
        }

        private void NextDiffBody()
        {
            iNextOne++;
            if (iNextOne >= 2)
                iNextOne = 0;
            switch (iNextOne)
            {
                case 0:
                    Utils.ScrollToCaretPosition(tbHPbody, iLocBody, iLenBody);
                    break;
                case 1:
                    Utils.ScrollToCaretPosition(tbTRbody, iLocBody, iLenBody);
                    break;
            }
        }

        private void NextDiffName()
        {
            iNextOne++;
            if (iNextOne >= 2)
                iNextOne = 0;
            switch (iNextOne)
            {
                case 0:
                    tbHPname.Select(iLocName, iLenName);
                    tbHPname.Focus();
                    break;
                case 1:
                    tbTRname.Select(iLocName, iLenName);
                    tbTRname.Focus();
                    break;
            }
        }

        private void NextDiff4()
        {
            iNextOne++;
            if (iNextOne >= 4)
                iNextOne = 0;
            switch (iNextOne)
            {
                case 0:
                    Utils.ScrollToCaretPosition(tbHPbody, iLocBody,iLenBody);
                    break;
                case 1:
                    Utils.ScrollToCaretPosition(tbTRbody, iLocBody, iLenBody);
                    break;
                case 2:
                    tbHPname.Select(iLocName, iLenName);
                    tbHPname.Focus();
                    break;
                case 3:
                    tbTRname.Select(iLocName, iLenName);
                    tbTRname.Focus();
                    break;
            }
        }

        private void RunFirst()
        {
            iLenBody = 0;
            iLenName = 0;
            iLocBody = FindFirstDifference(tbHPbody.Text, tbTRbody.Text, ref iLenBody);
            iLocName = FindFirstDifference(tbHPname.Text, tbTRname.Text, ref iLenName);
            if (iLocBody >= 0 && iLocName >= 0)
            {
                NextDiff4();
            }
            else
            {
                if (iLocName >= 0) NextDiffName();
                if (iLocBody >= 0) NextDiffBody();
            }
        }

        private void btnFirstH_Click(object sender, EventArgs e)
        {
            RunFirst();
        }

        private void btnShowH_Click(object sender, EventArgs e)
        {
            RunBrowser(tbHPbody.Text, "<b>====HP Local====</b>" + Environment.NewLine);
        }

        private void btnShowT_Click(object sender, EventArgs e)
        {
            string h = (WhichTable == "HTTP") ? "<b>====HTTP Original====</b>" : "<b>====From Transfer====</b>";
            RunBrowser(tbTRbody.Text, h + Environment.NewLine);
        }

        private void btnCopyTR_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbTRbody.Text);
        }

        private void btnHPclip_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbHPbody.Text);
        }

        private void btnShowBoth_Click(object sender, EventArgs e)
        {
            RunBrowser(tbHPbody.Text, "<b>====HP Local====</b>" + Environment.NewLine);
            string h = (WhichTable == "HTTP") ? "<b>====HTTP Original====</b>" : "<b>====From Transfer====</b>";
            RunBrowser(tbTRbody.Text, h + Environment.NewLine);
        }
    }
}

