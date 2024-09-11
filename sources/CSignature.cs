using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Security.Policy;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace MacroEditor
{
    public partial class CSignature : Form
    {
        private string SigLoc = "";
        private string SigFilename = "";
        private string DefaultSig = "<div><font face=\"hpsimplified, arial, sans-serif\" size=\"2.5\">I am a community volunteer.<br/><strong><font color=\"#000000\" size=\"2.75\">If you found the answer helpful and/or you want to say “thanks”? </font></strong>Click the <strong><font color=\"#0059d6\" size=\"2.75\">“ Yes ” box below </font></strong><img src=\"https://h30467.www3.hp.com/t5/image/serverpage/image-id/71238i8585EF0CF97FB353/image-dimensions/50x27?v&#61;v2\" />Did I help solve the problem?<strong><font color=\"#f80000\" size=\"2.75\"> don´t forget to </font></strong>click <strong><font color=\" green \" size=\"2.75\">“ Accept as a solution”</font> </strong><img src=\"https://h30467.www3.hp.com/t5/image/serverpage/image-id/71236i432711946C879F03/image-dimensions/129x32?v&#61;v2\">, someone who has the same query may find this solution and be helped by it.</font></div>";
        private int rowIndexFromMouseDown;
        private DataGridViewRow draggedRow;

        public int CurrentRowSelected = 0; // this corresponds to the body of the text and the name selected
        private cCheckSpell SpellCheck;


        private void ShowSig(int iRow)
        {
            if (iRow < 0) return;
            CurrentRowSelected = iRow;
            tbBody.Text = dgvSigList.Rows[CurrentRowSelected].Cells[1].Value.ToString();
        }

        public CSignature(ref cCheckSpell MySpellCheck)
        {
            InitializeComponent();
            SpellCheck = MySpellCheck;
            SigLoc = Utils.WhereExe;
            SigFilename = SigLoc + "\\signatures.txt";
            ReadSIG();
            foreach (DataGridViewColumn column in dgvSigList.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private string FormNewSig(string sName, string sBody)
        {
            string strDefault = sName + Environment.NewLine;
            strDefault += sBody + Environment.NewLine;
            return strDefault;
        }


        private void CreateSigFromDefault()
        {
            string strDefault = FormNewSig("jys1", DefaultSig);
            Utils.WriteAllText(SigFilename, strDefault);
        }

        private void ReadSIG()
        {
            if (!File.Exists(SigFilename))
            {
                CreateSigFromDefault();
            }
            dgvSigList.Rows.Clear();
            FillTable();
        }

        private int FillTable()
        {
            int i = 0;
            if (File.Exists(SigFilename))
            {
                StreamReader sr = new StreamReader(SigFilename);
                string sName = sr.ReadLine();
                string sBody = sr.ReadLine();
                dgvSigList.Rows.Add(sName,sBody);

                while (true)
                {
                    i++;
                    sName = sr.ReadLine();
                    if (sName == null) break;
                    if (sName == "") break;
                    sBody = sr.ReadLine();
                    if(sBody == null) break;
                    if (sBody == "") break;
                    dgvSigList.Rows.Add(sName,sBody);
                }
                sr.Close();
            }
            dgvSigList.Columns[0].Width = dgvSigList.Width;
            ShowSig(0);
            return i;
        }


        private void RunBrowser()
        {
            string strTemp = tbBody.Text;
            if (strTemp == "") return;
            Utils.ShowRawBrowser(strTemp,"");
        }

        private void btnShowBrowser_Click(object sender, EventArgs e)
        {
            RunBrowser();
        }

        private void blnAdd_Click(object sender, EventArgs e)
        {
            tbBody.Text = "ChangeMe";
            dgvSigList.Rows.Add("ChangeMe",tbBody.Text);
        }

        private void btnDelSig_Click(object sender, EventArgs e)
        {
            int i = dgvSigList.CurrentRow.Index;
            dgvSigList.Rows.RemoveAt(i);
        }

        private void btnSaveEdits_Click(object sender, EventArgs e)
        {
            int i = 0;
            string strOut = "";
            foreach (DataGridViewRow row in dgvSigList.Rows)
            {
                string Name = row.Cells[0].Value.ToString().Trim();
                string strBody = row.Cells[1].Value.ToString().Trim();
                strOut += FormNewSig(Name, strBody);
                i++;
            }
            Utils.WriteAllText(SigFilename, strOut);
        }

        private void bltnSaveBack_Click(object sender, EventArgs e)
        {
            if (tbBody.Text == null) return;
            tbBody.Text = Utils.RemoveNL(tbBody.Text);
            dgvSigList.Rows[CurrentRowSelected].Cells[1].Value = tbBody.Text;
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            utils MyUtils = new utils(ref SpellCheck);
            MyUtils.Show();
        }

        private void CopyToNotepad(string s)
        {
            CSendNotepad SendNotepad = new CSendNotepad();
            string npTitle = dgvSigList.CurrentRow.Cells[0].Value.ToString() + Environment.NewLine + s;
            SendNotepad.PasteToNotepad(npTitle);
        }

        private void btnToNote_Click(object sender, EventArgs e)
        {
            CopyToNotepad(tbBody.Text);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbBody.Text = "";
        }

        private void btnAdd1New_Click(object sender, EventArgs e)
        {
            Utils.AddNL(ref tbBody, 1);
        }

        private void btnAdd2New_Click(object sender, EventArgs e)
        {
            Utils.AddNL(ref tbBody, 2);
        }
        
        private void TestBBC()
        {
            if (tbBody.Text == null) return;
            string strTemp = Utils.RemoveNL(tbBody.Text);
            string strErr = Utils.BBCparse(strTemp);
            if (strErr == "")
            {
                return;
            }
            DialogResult Res1 = MessageBox.Show(strErr, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        private void btnTestXML_Click(object sender, EventArgs e)
        {
            TestBBC();
        }

        private void btnPasteImg_Click(object sender, EventArgs e)
        {
            string sUrl,sImg;

            if(Clipboard.ContainsImage())
            {
                PictureBox pbImage = new PictureBox();
                pbImage.SizeMode = PictureBoxSizeMode.Zoom; // .StretchImage;
                pbImage.Image = Clipboard.GetImage();
                sUrl = Utils.GetNextImageFile("SI", Utils.WhereExe);
                string sImagePath = Utils.WhereExe + "/" + sUrl;
                pbImage.Image.Save(sImagePath, ImageFormat.Png);
                //int iWidth = pbImage.Image.Width;
                //int iHeight = pbImage.Image.Height;
                sImg = Utils.AssembleIMG(sUrl,false);
                tbBody.Text += sImg;
            }
            else
            {
                sUrl = Utils.ClipboardGetText();
                if (Utils.IsUrlImage(sUrl))
                {
                    sImg = Utils.AssembleIMG(sUrl,false);
                    tbBody.Text += sImg;
                }
            }
        }

        private void btnNLtoNotepad_Click(object sender, EventArgs e)
        {

            CopyToNotepad(Utils.ChangeBRtoNL(tbBody.Text));
        }

        private void dgvSigList_DragDrop(object sender, DragEventArgs e)
        {
            Point clientPoint = dgvSigList.PointToClient(new Point(e.X, e.Y));
            int rowIndexOfTarget = dgvSigList.HitTest(clientPoint.X, clientPoint.Y).RowIndex;
            if (rowIndexOfTarget < 0) return;
            if (e.Data.GetDataPresent(typeof(DataGridViewRow)) && rowIndexOfTarget != rowIndexFromMouseDown)
            {
                DataGridViewRow rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
                dgvSigList.Rows.RemoveAt(rowIndexFromMouseDown);
                dgvSigList.Rows.Insert(rowIndexOfTarget, rowToMove);
            }
        }

        private void dgvSigList_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dgvSigList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                rowIndexFromMouseDown = dgvSigList.HitTest(e.X, e.Y).RowIndex;
                if (rowIndexFromMouseDown >= 0)
                {
                    Size dragSize = SystemInformation.DragSize;
                    draggedRow = dgvSigList.Rows[rowIndexFromMouseDown];
                    dgvSigList.DoDragDrop(draggedRow, DragDropEffects.Move);
                    ShowSig(rowIndexFromMouseDown);
                    if (e.Clicks == 2) RunBrowser();
                }
            }
        }

        private void EditHelp(string s)
        {
            if (Utils.bSpellingEnabled)
                SpellCheck.EditHelpDocs(s);
            else Utils.WordpadEdit(s);
        }


        private void CSignature_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            EditHelp("SIG");            
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            Utils.AddBold(ref tbBody);
        }

        private void btnInvertNL_Click(object sender, EventArgs e)
        {
            Utils.SwapNL(ref tbBody);
        }

        private void btnFixBR_Click(object sender, EventArgs e)
        {
            tbBody.Text = Utils.ChangeBRtoNL(tbBody.Text);
        }

        private void dgvSigList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                ShowSig(e.RowIndex);
            }
        }
    }
}
