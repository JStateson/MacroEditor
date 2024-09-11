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
    public partial class DisplayText : Form
    {
        public DisplayText(string sText)
        {
            InitializeComponent();
            tbEdit.Text = sText;
        }

        private void DisplayText_SizeChanged(object sender, EventArgs e)
        {
            int iWidth = DisplayText.ActiveForm.Width;
            int iHeight = DisplayText.ActiveForm.Height;
            tbEdit.Width = iWidth - 100;
            tbEdit.Height = iHeight - 100;
        }
    }
}
