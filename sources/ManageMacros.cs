using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace MacroEditor
{
    public partial class ManageMacros : Form
    { 
        private string WhereExe;
        private string strType;
        private string sBody;
        private string sLoc = "";
        private int iRow, iCol;
        public int nAnyChanges { get; set; }
        private List<dgvStruct> DataTable;
        private cCheckSpell SpellCheck;
        private class CImageItems
        {
            public string ImageLocation { get; set; }
            public int MacroNum { get; set; }
        }

        List<CImageItems> MyItems;

        public ManageMacros(string rstrType, ref List<dgvStruct> rDataTable, ref cCheckSpell MySpellCheck)
        {
            InitializeComponent();
            SpellCheck = MySpellCheck;
            nAnyChanges = 0;
            DataTable = rDataTable;
            WhereExe = Utils.WhereExe;
            strType = rstrType;
            MyItems = new List<CImageItems>();
            int i = 0;
            pbImage.SizeMode = PictureBoxSizeMode.StretchImage;



            dgManage.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            sLoc = Utils.WhereExe;
            
            while (true)
            {
                if (i >= rDataTable.Count) return;
                sBody = rDataTable[i].sBody;
                if (sBody == ""|| sBody == null) break;
                //if (sBody.Contains(Utils.AssignedImageName))
                if (!Utils.IsPostableImage(sBody))
                {
                    AddImages(i);
                }
                i++;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Check if F3 key is pressed
            if (keyData == Keys.F3)
            {
                // Prevent the default behavior for F3
                return true;
            }

            // Call the base class method for other keys
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void AddRow(string strPath, int r)
        {
            CImageItems c3i = new CImageItems();
            c3i.ImageLocation = strPath;
            c3i.MacroNum = r+1;
            MyItems.Add(c3i);
        }

        private void AddImages(int r)
        {
            int j,i = sBody.IndexOf(Utils.AssignedImageName);
            int k = 0;
            int n = 0;
            string strUrl = "";
            while(i > 0)
            {
                j = sBody.IndexOf(".png", i);
                j += 4;
                strUrl = sBody.Substring(i, j - i);
                AddRow(strUrl,r);
                k++;
                i += (j-i);
                i = sBody.IndexOf(Utils.AssignedImageName,i);
                if (i < 0) break;
            }
            
            while (true)
            {
                j = Utils.AnyImageIndex(sBody, n);
                if(j < 0) break;
                i = sBody.LastIndexOf("http", j);
                if (i < 0) break;
                strUrl = sBody.Substring(i, j - i);
                n +=j;
                AddRow(strUrl,r);
                k++;
                if (n > sBody.Length) break;
            }
            dgManage.DataSource = MyItems.ToArray();
            dgManage.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgManage.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgManage.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgManage.Refresh();
            dgManage.Rows[0].Selected = true;
            ShowRow(0);
        }


        private void btnLogIn_Click(object sender, EventArgs e)
        {
            Utils.ShowMyPhotoAlbum();
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            Process.Start(WhereExe);
        }

        private void btnUpdateURL_Click(object sender, EventArgs e)
        {
            if (iRow < 0) return;
            if (dgManage.Rows.Count == 0) return;
            string strOld = dgManage.Rows[iRow].Cells[0].Value.ToString();
            if(strOld.Length == 0) return; // user forgot to exit
            int r = -1 + (int)dgManage.Rows[iRow].Cells[1].Value;
            string strNew;
            UpdateUrl uUrl = new UpdateUrl(strOld);
            uUrl.ShowDialog();
            strNew = uUrl.strResult;
            uUrl.Dispose();
            if (strNew == strOld) return;
            string sBodyEXC = DataTable[r].sBody.Replace(strOld, strNew);
            DataTable[r].sBody = sBodyEXC;
            nAnyChanges++;
            dgManage.Rows[iRow].Cells[0].Value = "";
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            HelpUpdate hu   = new HelpUpdate();
            hu.Show();
        }

        private void ShowRow(int j)
        {
            string sUrl = dgManage.Rows[j].Cells[0].Value.ToString();
            if(!sUrl.ToLower().Contains("http"))
                sUrl = sLoc + "/" + sUrl;
            pbImage.ImageLocation = sUrl;
        }


        private void SetLocation()
        {
            System.Drawing.Point ThisRC = dgManage.CurrentCellAddress;
            iRow = ThisRC.Y;
            iCol = ThisRC.X;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditHelp(string s)
        {
            if (Utils.bSpellingEnabled)
                SpellCheck.EditHelpDocs(s);
            else Utils.WordpadEdit(s);
        }


        private void ManageMacros_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            EditHelp("MANAGE");
        }

        private void btnURLuse_Click(object sender, EventArgs e)
        {
            if (iRow < 0) return;
            if (dgManage.Rows.Count == 0) return;
            string sRemoteUrl = dgManage.Rows[iRow].Cells[0].Value.ToString();
            System.Windows.Forms.Clipboard.SetText(sRemoteUrl);
        }

        private void dgManage_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            string sRemoteUrl = dgManage.Rows[iRow].Cells[0].Value.ToString();
            bool bIsRemote = sRemoteUrl.ToLower().Contains("http");
            btnURLuse.Enabled = bIsRemote;
            btnBrowseImage.Enabled = !bIsRemote;
        }

        private void dgManage_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SetLocation();
            ShowRow(iRow);
        }
    }
}
