using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace MacroEditor
{
    public partial class ShowErrors : Form
    {
        private string[] sErrors;
        private int[] iErrors;
        private int[] BodyFromRow; 
        private string sLoc = "";
        private List<dgvStruct> DataTable;

        //i is the row selected
        private void MakeBodyAvailable(int i)
        {
            if (i < 0) return;
            int j = BodyFromRow[i];
            File.WriteAllText(sLoc + "/MyHtml.txt", DataTable[j].sBody);
        }

        public ShowErrors(ref List<dgvStruct> rDataTable)
        {
            InitializeComponent();
            sLoc = Utils.WhereExe;
            DataTable = rDataTable;
            lbMacNames.Items.Clear();
            int i = 0, j = 0;
            int m = rDataTable.Count;
            sErrors = new string[m];
            iErrors = new int[m]; 
            BodyFromRow = new int[m];
            foreach (dgvStruct row in rDataTable)
            {
                if(row.sErr != "")
                {
                    lbMacNames.Items.Add(row.MacName);
                    sErrors[j] = row.sErr;
                    BodyFromRow[j] = i;
                    j++;
                }
            }
            btnFindErr.Text = lbMacNames.Items[0].ToString();
            MakeBodyAvailable(0);
            lbMacNames.SelectedIndex = 0;
        }

        private void lbMacNames_MouseClick(object sender, MouseEventArgs e)
        {
            int index = lbMacNames.IndexFromPoint(e.Location);
            lbMacNames.SelectedIndex = index;
        }

        private void lbMacNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            int r = lbMacNames.SelectedIndex;
            if (r < 0) return;
            tbError.Text = sErrors[r];
            btnFindErr.Text = lbMacNames.Items[r].ToString();
        }

        private void btnFindErr_Click(object sender, EventArgs e)
        {
            string strLoc = sLoc + "\\MyHtml.txt";
            MakeBodyAvailable(lbMacNames.SelectedIndex);
            Utils.NotepadViewer(strLoc);
        }
    }
}
