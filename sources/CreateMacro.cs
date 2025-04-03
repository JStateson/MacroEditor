using AxWMPLib;
using MacroEditor.sources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;


/*
Images used should be PNG but JPG should work
if you paste an image into a macro it will be named LOCALIMAGEFILE-X-Y.PNG
where X is PC, PRN or HP
and Y is 0..29
It's path will be the location of the executable program.  A tool (dialogbox "manage") can help
with replacing the LOCALIMAGEFILE with one in the HP album after you upload the local file to your HP album
*/


namespace MacroEditor
{
    public partial class CreateMacro : Form
    {
        public string strResultOut { get; set; }  
        private bool bIsPath = false;   // if a file on the disk then is true
        private string ExeFolder;
        private string strType = "";
        private bool bIsVideo = false;
        private bool bIsImage = false;
        private int videoWidth = 0;
        private int videoHeight = 0;
        private string ImageLocation = "";  // photo or video
        private bool bHaveVideoSize = false;
        private double wImageAspect;
        double maxPix = 800;
        double minPix = 100;
        public CreateMacro(string rstrType)
        {
            InitializeComponent();
            ExeFolder = Utils.WhereExe;
            strType = rstrType;
            //axWMP.URL = "C:/Users/josep/Videos/2000_to_2011TU.mp4";
            axWMP.PlayStateChange += axWMP_PlayStateChange;
            cbVSize.SelectedIndex = 0;
            cbImgSize.SelectedIndex = 0;
        }

        private void axWMP_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            // Check if the media is in the 'Playing' state
            if (e.newState == (int)WMPLib.WMPPlayState.wmppsPlaying)
            {
                videoWidth = axWMP.currentMedia.imageSourceWidth;
                videoHeight = axWMP.currentMedia.imageSourceHeight;
                FindImageSizes();
            }

        }

        private void FindImageSizes()
        {
            int w, h;
            double x;
            if(bIsVideo)
            {
                tbWidth.Text = videoWidth.ToString();
                tbHeight.Text = videoHeight.ToString();
                wImageAspect = (double) videoWidth / videoHeight;
            }
            else
            {
                w = pbImage.Image.Width;
                tbWidth.Text = w.ToString("F2");
                h = pbImage.Image.Height;
                tbHeight.Text = h.ToString("F2");
                wImageAspect = (double)pbImage.Image.Width / pbImage.Image.Height;
                if (w > maxPix)
                {
                    x = (double)maxPix / wImageAspect;
                    h = Convert.ToInt32(x);
                    w = Convert.ToInt32(maxPix);
                }
                SetDefaultSlider((double)h);
            }           
        }

        private void SliderComboVisible(bool bHaveAlbum)
        {
            cbImgSize.Visible = bHaveAlbum;
            gbNotServer.Visible = !bHaveAlbum;
            lbH.Visible = !bHaveAlbum;
            lbW.Visible = !bHaveAlbum;
            tbH.Visible = !bHaveAlbum;
            tbW.Visible = !bHaveAlbum;
        }

        private void TryUseImage()
        {
            ImageLocation = Utils.RemoveOuterQuotes(tbUrlText.Text);
            SliderComboVisible(ImageLocation.Contains(Utils.sIsAlbum));
            bIsImage = true;
            if (!Utils.IsUrlImage(ImageLocation))
            {
                bIsImage = false;
                bIsVideo = true;
                if (!Utils.IsUrlVideo(ImageLocation))
                {
                    bIsVideo = false;
                }
            }
            btnApply.Enabled = true;
            bIsPath = !Utils.bIsHTTP(ImageLocation);
            
            if(bIsImage)
            {
                tcImaging.SelectedIndex = 0;
                pbImage.ImageLocation = ImageLocation;
            }
            if(bIsVideo)
            {
                tcImaging.SelectedIndex = 1;
                axWMP.URL = ImageLocation;
                if (bIsPath)
                    btnApply.Enabled = false;   // not supporting local video files!
            }
        }

        private void btnFormRemote_Click(object sender, EventArgs e)
        {
            TryUseImage();
        }

        private string EmbedVideo(string sUrl, string sTitle, string sOptions,  bool bB)
        {
            string sW = videoWidth.ToString();
            string sH = videoHeight.ToString();
            string sOut = "<iframe ";
            string sBorderSize =  bB ? "1" : "0";
            if (bHaveVideoSize)
                sOut += "width=\"" + sW + "\" height=\"" + sH + "\" ";
            string s = sOut + "src=\"" + sUrl + "\"";
            if(sTitle != "")
                s+= " title=\"" + sTitle + "\"";
            //s += " frameborder=\"" + sBorderSize + "\"";
            s += " width=\"" + sBorderSize + "\"";
            if (sOptions != "")
                s += sOptions;
            s+= " allowfullscreen></iframe>";
            return s;
        }


        private void AddImage()
        {
            strResultOut = bIsImage ? GetImage() : GetVideo();
        }

        private string GetImage()
        {
            string s, strImageName = "", strImagePath = "";
            int Width = 200, Height = 200;  //default unless image-size is present
            if (bIsPath) // do not know H or W so default is used
            {
                strImageName = Utils.GetNextImageFile(strType, ExeFolder);
                strImagePath = ExeFolder + "/" + strImageName;
                pbImage.Image.Save(strImagePath, ImageFormat.Png);
                Width = pbImage.Image.Width;
                Height = pbImage.Image.Height;
            }
            else
            {
                strImageName = ImageLocation;
            }

            if (strImageName.Contains(Utils.sIsAlbum))
            {
                s = Utils.AssembleImage(strImageName, cbImgSize.SelectedItem.ToString(),cbFormBorder.Checked);
            }
            else
            {
                if(cbDNU.Checked)
                {
                    s = Utils.AssembleIMG(strImageName, cbFormBorder.Checked);
                }
                else
                {
                    double w = Convert.ToDouble(tbWidth.Text);
                    double h = Convert.ToDouble(tbHeight.Text);
                    Width = Convert.ToInt32(w);
                    Height = Convert.ToInt32(h);
                    s = Utils.AssembleImage(strImageName, Height, Width, cbFormBorder.Checked);
                }                 
            }
            return s;
        }


        private void btnApply_Click(object sender, EventArgs e)
        {
            AddImage();
            this.Close();
        }

        private void btnCencel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                tcImaging.SelectedIndex = 0;
                pbImage.Image = Clipboard.GetImage(); 
                pbImage.SizeMode = PictureBoxSizeMode.CenterImage;// .Zoom; // .StretchImage;
                bIsPath = true;
                btnApply.Enabled = true; 
                SliderComboVisible(false);
                FindImageSizes();
                bIsImage = true;
                bIsVideo = false;
            }
            else
            {
                MessageBox.Show("Empty clipboard: Please copy image using windows key + 'S'\r\nOr any screen capture tool such as snagit");
            }            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            pbImage.ImageLocation = null;
            pbImage.Image = null;
            axWMP.URL = null;
            ImageLocation = "";
            tbUrlText.Text = "";
            tbWidth.Text = "";
            tbHeight.Text = "";
            cbVBorder.Checked = false;
        }

        private void pbImage_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            btnApply.Enabled = true;
            FindImageSizes();
        }

      
        private void btnBrowseImg_Click(object sender, EventArgs e)
        {
            var codecs = ImageCodecInfo.GetImageEncoders();
            var codecFilter = "Image Files|";
            string strFilename = "";
            foreach (var codec in codecs)
            {
                codecFilter += codec.FilenameExtension + ";";
            }
            ofd.Filter = codecFilter;
            ofd.DefaultExt = ".png";
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                strFilename = ofd.FileName;
                tbUrlText.Text = strFilename;
            }
        }


        private string GetVideo()
        {
            string sV = ImageLocation; //"https://www.youtube.com/embed/lFB3mWUSnJMhttps://www.youtube.com/embed/lFB3mWUSnJM";
            string sO = "";// "allow=\"accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share\" referrerpolicy=\"strict-origin-when-cross-origin\"";
            string stitle = tbVTitle.Text; //"Print on an HP printer from a Chromebook | HP Support";
            bool bBorder = cbVBorder.Checked;
            string sOut = EmbedVideo(sV, stitle, sO, bBorder);
            return sOut;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if(bIsImage)
            {
                Utils.ShowPageInBrowser("", GetImage());
            }
            if(bIsVideo)
            {
                Utils.ShowPageInBrowser("", GetVideo());
            }
        }

        private void btnClrIL_Click(object sender, EventArgs e)
        {
            tbUrlText.Text = "";
        }

        private bool SetVideoSize()
        {
            int iW = 0;
            int iH = 0;
            if (cbVSize.SelectedIndex == 0) return false;   // do not want size set
            bool bHaveSize = int.TryParse(tbWidth.Text, out iW);
            bHaveSize &= int.TryParse(tbHeight.Text, out iH);
            if (cbVSize.SelectedIndex == 1)
            {
                if (bHaveSize && (iW > 0) && (iH > 0))
                {
                    videoHeight = iH;
                    videoWidth = iW;
                    return true;
                }
                return false;
            }

            if( cbVSize.SelectedIndex != 1)
            {
                if(cbVSize.SelectedIndex == 2)
                {
                    videoHeight = 240;
                    videoWidth = 320;
                    return true;
                }
                videoHeight = 480;
                videoWidth = 640;
                return true;
            }
            return false;
        }

        private void btnVApply_Click(object sender, EventArgs e)
        {
            bHaveVideoSize = SetVideoSize();
        }

        // this is used only for video, not photo!!!  Oct 22, 2024
        private void SetDefaultSlider(double v)
        {
            double x = minPix * (v - minPix) / ((maxPix / wImageAspect) - minPix);
            if (x < 0) x = minPix;// Oct 22, 2024
            if (x > 100) x = 100;
            trackBar1.Value = Convert.ToInt32(x);
        }

        private void SetWantedSize(int i)
        {
            double v, x = (double)i;
            //h = x*(maxPix - minPix * wImageAspect) / minPix + minPix * wImageAspect;
            v = (x * ((maxPix / wImageAspect) - minPix) / minPix) + minPix;
            tbH.Text = v.ToString("F2");
            tbW.Text = (wImageAspect * v).ToString("F2");
        }


        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            int i = trackBar1.Value;
            SetWantedSize(i);
        }

        private void btnApplySlider_Click(object sender, EventArgs e)
        {
            tbWidth.Text = tbW.Text;
            tbHeight.Text = tbH.Text;
        }

        private void btnResetAspect_Click(object sender, EventArgs e)
        {
            FindImageSizes();
        }
    }
}
