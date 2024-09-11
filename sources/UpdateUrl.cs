using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace MacroEditor
{
    public partial class UpdateUrl : Form
    {
        public string strResult { get; set; }
        public UpdateUrl(string rstrResult)
        {
            InitializeComponent();
            strResult = rstrResult;
            tbOld.Text = strResult;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string strUrl = tbUrl.Text;
            string aLCase = strUrl.ToLower();
            if (Utils.bFromGallery(aLCase))
            {
                btnSaveExit.Enabled = true;
                btnTest.Enabled = true;
            }
            else
            {
                tbInfo.Text = "This is not an image url from an HP album!";
                btnSaveExit.Enabled = false;
                btnTest.Enabled = false;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string strTemp = tbUrl.Text;
            if (strTemp == "") return;
            Utils.ShowPageInBrowser("",Utils.AssembleIMG(strTemp,false));
        }

        private void btnSaveExit_Click(object sender, EventArgs e)
        {
            strResult = tbUrl.Text.Trim();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
