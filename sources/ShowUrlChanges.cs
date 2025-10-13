using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MacroEditor.ChangeUrls.cFilesNeedingChanges;

namespace MacroEditor
{
    internal partial class ShowUrlChanges : Form
    {
        ChangeUrls changeUrls = null;
        List<string> TheseFiles = new List<string>();
        List<string> ThoseNames = new List<string>();
        public ShowUrlChanges(ref ChangeUrls rchangeUrls)
        {
            InitializeComponent();
            changeUrls = rchangeUrls;
            lbFromUrl.Text = changeUrls.fnc.FromUrl;
            lbToUrl.Text = changeUrls.fnc.ToUrl;
            for(int i = 0; i < changeUrls.fnc.soc.Count; i++)
            {
                int j = Utils.IndexMacName(changeUrls.fnc.soc[i].Filecode);
                string sNiceName = Utils.LocalMacroFullName[j];                
                TheseFiles.Add(sNiceName);
            }
            lbFilenames.DataSource = TheseFiles;
        }

        private void lbFilenames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbFilenames == null) return;
            string sName = lbFilenames.SelectedItem.ToString();
            int i = Utils.IndexFromFullName(sName);
            string sCode = Utils.LocalMacroPrefix[i];
            ThoseNames.Clear();
            foreach(cSetOfChanges soc in changeUrls.fnc.soc)
            {
                if(soc.Filecode == sCode)
                {
                    i = 0;
                    foreach(string sMN in soc.OldMacroNames)
                    {
                        string sNum = (soc.OldMacroNumber[i] + "# ").PadRight(4);
                        ThoseNames.Add(sMN);
                        i++;
                    }
                    lbMacroNames.DataSource = null;
                    lbMacroNames.DataSource = ThoseNames;
                    break;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            changeUrls.fnc.ChangeApproved = false;
            this.Close();
        }

        private void btnAppAll_Click(object sender, EventArgs e)
        {
            changeUrls.fnc.ChangeApproved = true;
            
            foreach (cSetOfChanges soc in changeUrls.fnc.soc)
            {                
                string strFN = soc.Filecode;
                string sPathname = Utils.FNtoPath(strFN);
                string sBuf = File.ReadAllText(sPathname);
                sBuf = sBuf.Replace(changeUrls.fnc.FromUrl, changeUrls.fnc.ToUrl);
                File.WriteAllText(sPathname, sBuf);
            }
            changeUrls.fnc.FromUrl = changeUrls.fnc.ToUrl;
            changeUrls.SignalAllGoodUrls();
            this.Close();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            Utils.GetNextArchive("ALL");
        }
    }
}
