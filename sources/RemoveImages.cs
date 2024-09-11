using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using static MacroEditor.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Xml.Linq;

namespace MacroEditor
{
    public partial class RemoveImages : Form
    {

        private List<CLocalFiles> LocalImageFiles;
        private string strTempFilePath = "";
        private string strTempFilename = "";
        private int iWidth, iHeight;
        private List<string> DelList;
        private List<string> sFoundcBody = new List<string>();
        private List<int> iLocFound = new List<int>();
        public RemoveImages(ref List<CBody> Rcb)
        {
            InitializeComponent();
            FillLocalImageTable();
            int iLoc = 0;
            foreach (CBody cb in Rcb)
            {
                List<int> positions = FindPatternPositions(cb.sBody, Utils.AssignedImageName);
                if (positions.Count == 0) continue;
                foreach(int i in positions)
                {
                    int j = cb.sBody.IndexOf(".png",i);
                    string e = "";
                    Debug.Assert(j >= 0);
                    string t = cb.sBody.Substring(i,j+4-i);
                    iLocFound.Add(iLoc);
                    if (File.Exists(Utils.WhereExe + "/" + t))
                        e = "(P) ";
                    else
                        e = "(M) ";
                    string s = cb.File.ToString() +"-" + cb.Number + " " + e + t; 
                    sFoundcBody.Add(s);
                }
                iLoc++;
            }
            foreach(string s in sFoundcBody)
            {
                lbUsedLF.Items.Add(s);
            }
            foreach (DataGridViewColumn column in dgvUsedImages.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private List<CLocalFiles> NumLocalImageFiles()
        {
            List<CLocalFiles> MyImages = new List<CLocalFiles>();
            string[] files = Directory.GetFiles(WhereExe, "LOCALIMAGEFILE-*.png");
            foreach (string s in files)
            {
                CLocalFiles cf = new CLocalFiles();
                cf.Name = Path.GetFileName(s);
                cf.NotUsed = true;
                MyImages.Add(cf);
            }
            return MyImages;
        }

        static List<int> FindPatternPositions(string text, string pattern)
        {
            List<int> positions = new List<int>();
            int position = text.IndexOf(pattern,StringComparison.OrdinalIgnoreCase);

            while (position != -1)
            {
                positions.Add(position);
                position = text.IndexOf(pattern, position + pattern.Length, StringComparison.OrdinalIgnoreCase);
            }

            return positions;
        }

        private void btnDelUnused_Click(object sender, EventArgs e)
        {
            int NumDeleted = 0;
            DelList = new List<string>();
            foreach (CLocalFiles cf in LocalImageFiles)
            {
                if (cf.NotUsed)
                {
                    string sFile = Utils.WhereExe + "\\" + cf.Name;
                    DelList.Add(sFile);
                    NumDeleted++;
                }
            }

            if(NumDeleted > 0)
            {
                LocalImageFiles.Clear();
                dgvUsedImages.DataSource = null;
                ClearPB();
            }
            foreach(string s in DelList)
            {
                try
                {
                    File.Delete(s);
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to delete file " + s);
                }
            }
            FillLocalImageTable();
        }

        private void FillLocalImageTable()
        {
            string strpath = "";
            string strTotal = "";
            int i = 0;
            int r = dgvUsedImages.Size.Width;
            LocalImageFiles = NumLocalImageFiles();
            foreach (string s in Utils.LocalMacroPrefix)
            {
                strpath = Utils.FNtoPath(s);
                if (!File.Exists(strpath)) continue;
                strTotal += File.ReadAllText(strpath);
            }
            strpath = Utils.FNtoPath("SI");
            if (File.Exists(strpath))
            {
                strTotal += File.ReadAllText(strpath);
            }
            foreach (CLocalFiles cf in LocalImageFiles)
            {
                if (strTotal.Contains(cf.Name))
                {
                    LocalImageFiles[i].NotUsed = false;
                }
                i++;
            }
            dgvUsedImages.DataSource = LocalImageFiles;
            dgvUsedImages.Columns[0].Width = 64;
            dgvUsedImages.Columns[1].Width = r - dgvUsedImages.Columns[0].Width - 36;
        }

        private void dgvUsedImages_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            strTempFilename = dgvUsedImages.Rows[e.RowIndex].Cells[1].Value.ToString();
            if (strTempFilename == "") return;
            strTempFilePath = Utils.WhereExe + "\\" + strTempFilename;
            if (pbImage.Image != null)
            {
                pbImage.Image.Dispose();
                pbImage.Image = null;
            }
            pbImage.Image = Image.FromFile(strTempFilePath);
        }

        private void RunBrowser(string strTemp)
        {
            Utils.ShowPageInBrowser("", strTemp);
        }

        private void ClearPB()
        {
            if (pbImage.Image != null)
            {
                pbImage.Image.Dispose();
                pbImage.Image = null;
            }
        }

        private void RemoveImages_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearPB();
        }

        private void dgvUsedImages_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            bool bFound = Utils.GetPixSize(strTempFilename, ref iHeight, ref iWidth);
            if (bFound)
            {
                string strImg = Utils.AssembleImage(strTempFilename, iHeight, iWidth, false);
                RunBrowser(strImg);
            }
        }
    }
}
