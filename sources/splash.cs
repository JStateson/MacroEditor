using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacroEditor.sources
{
    public partial class splash : Form
    {
        public splash()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            string sCode = tbSplashCode.Text.Trim();
            Properties.Settings.Default.cSplash = sCode;
            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
