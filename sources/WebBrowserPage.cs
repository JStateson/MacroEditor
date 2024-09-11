using System.Windows.Forms;

namespace MacroEditor
{
    public partial class WebBrowserPage : Form
    {
        public WebBrowserPage(string strIn)
        {
            InitializeComponent();
            webBrowser.DocumentText = strIn;
        }
    }
}
