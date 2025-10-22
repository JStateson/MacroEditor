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
            this.KeyPreview = true;  // Ensure the form receives key events first
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();  // Close the form
            }
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
