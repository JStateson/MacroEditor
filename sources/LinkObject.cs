using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Media.Imaging;


namespace MacroEditor
{
    public partial class LinkObject : Form
    {
        private string sLoc;
        private bool bBoxed = false;
        string strBoxed = "";
        public string strResultOut { get; set; }
        private bool bIsImage = false;
        private string strImgUrl;
        /*
        // ext = "*.bmp;*.dib;*.rle"           descr = BMP
        // ext = "*.jpg;*.jpeg;*.jpe;*.jfif"   descr = JPEG
        // ext = "*.gif"                       descr = GIF
        // ext = "*.tif;*.tiff"                descr = TIFF
        // ext = "*.png"                       descr = PNG
        */



        private void RBsetContext()
        {
            if (rbimage.Checked)
            {
                pbImage.ImageLocation = tbRawUrl.Text;
                tbUrlText.Enabled = false; // not used for images
            }
            else if(rbNotImage.Checked)
            {
                pbImage.ImageLocation = "";
                tbUrlText.Enabled = true;
            }
            else
            {
                tbUrlText.Enabled = false;
                tbRawUrl.Enabled = true;
            }
            tbResult.Text = "";
        }
        public LinkObject(string rstrIn)
        {
            strResultOut = "";
            bIsImage = Utils.IsUrlImage(rstrIn.ToLower());
            InitializeComponent();
            rbimage.Checked = bIsImage;
            rbNotImage.Checked = !bIsImage;
            tbRawUrl.Text = rstrIn;
            RBsetContext();
            sLoc = Utils.WhereExe;

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            strResultOut = bBoxed ? strBoxed : tbResult.Text;
            this.Close();
        }

        private void btnCencel_Click(object sender, EventArgs e)
        {
            strResultOut = "";
            this.Close();
        }


        private void FormObject()
        {
            string sTB = tbRawUrl.Text;   // do not trim as space have be wanted
            string strResult = "";
            if (rbimage.Checked)
            {
                strImgUrl = Utils.AssembleIMG(sTB, false);
                strResult = strImgUrl;
            }
            else if(rbNotImage.Checked)
            {
                string strTmp = tbUrlText.Text;
                string strUrl = Utils.dRef(sTB);
                if (strTmp == "") strTmp = strUrl;
                strResult = Utils.FormUrl(strUrl, strTmp);
            }
            else
            {
                strResult = tbRawUrl.Text;
            }
            if (rbNoBox.Checked) tbResult.Text = strResult;
            else
            {
                tbResult.Text = rbSqueeze.Checked ? Utils.Form1CellTable(strResult, GetBoxWidth()) : Utils.Form1CellTableP(strResult, GetBoxWidth());
            }
        }

        private void btnApplyText_Click(object sender, EventArgs e)
        {
            FormObject();
        }

        private void RunBrowser()
        {
            if (tbResult.Text == null) return;
            string strTemp = tbResult.Text;
            if (strTemp == "" || bBoxed) strTemp = strBoxed;
            if (strTemp == "") return;
            Utils.ShowPageInBrowser("", strTemp);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            FormObject();
            RunBrowser();
        }

        private void rbNotImage_CheckedChanged(object sender, EventArgs e)
        {
            RBsetContext();
        }

        private void rbimage_CheckedChanged(object sender, EventArgs e)
        {
            RBsetContext();
        }

        private void LinkObject_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (pbImage.Image != null)
            {
                pbImage.Image.Dispose();
                pbImage.Image = null;
            }
        }


        private void LinkObject_Shown(object sender, EventArgs e)
        {
            tbResult.Focus();
        }

        private void btnAE_Click(object sender, EventArgs e)
        {
            strResultOut = tbResult.Text;
            this.Close();
        }

        private string GetBoxWidth()
        {
            string sRtn = "";
            foreach (RadioButton rb in gbPCTbw.Controls)
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
    }
}
