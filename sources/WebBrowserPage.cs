using System.Windows.Forms;

namespace MacroEditor
{
    public partial class WebBrowserPage : Form
    {
        public WebBrowserPage(string strIn)
        {
            InitializeComponent();
            webBrowser.DocumentText = strIn;
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
    }
}
