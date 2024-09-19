using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacroEditor
{
    partial class AboutBox : Form
    {

        LinkLabel dynamicLinkLabel = new LinkLabel();
        string sUrl = "https://github.com/JStateson/MacroEditor/issues";
        public AboutBox()
        {
            //dynamicLinkLabel.BackColor = Color.Red;
            int n = sUrl.Length;
            dynamicLinkLabel.ForeColor = Color.Blue;
            dynamicLinkLabel.Text = "Click here to report a problem";
            dynamicLinkLabel.Name = "DynamicLinkLabel";
            dynamicLinkLabel.Font = new Font("Georgia", 12);
            dynamicLinkLabel.Height = 30;
            dynamicLinkLabel.Width = 300;
            dynamicLinkLabel.LinkArea = new LinkArea(0, n);
            dynamicLinkLabel.Links.Add(n, 9, sUrl);
            dynamicLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(LinkedLabelClicked);
            string strTmp = "This program allows volunteers to create and edit help macros: adding links and images.\r\nSource code is at github/jstateson.  Anyone can use this tool or distribute it.\r\nYou can edit the source code and build the app as long I am attributed as the author.\r\nJuly 14, 2024 build date.  josephy@stateson.net";
            strTmp += " Total Macros for use (" + Utils.TotalNumberMacros.ToString() + ")";
            InitializeComponent();
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = strTmp; // AssemblyDescription;
            tableLayoutPanel.Controls.Add(dynamicLinkLabel, 3,6);
        }

        private void LinkedLabelClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dynamicLinkLabel.LinkVisited = true;
            System.Diagnostics.Process.Start(sUrl);
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
    }
}
