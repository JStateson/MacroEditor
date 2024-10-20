using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using static MacroEditor.Utils;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Collections;
using System.Xml.Linq;
using System.Diagnostics;

namespace MacroEditor
{

    public partial class Settings : Form
    {
        public eBrowserType eBrowser { get; set; }
        public bool bWantsExit { get; set; }
        public string userid { get; set; }
        private cMacroChanges xMC;
        private cMacroChanges xMV;
        private List<string> stDataSource = new List<string>();
        private List<long> stDateTime = new List<long>();
        private List<string> VstDataSource = new List<string>();
        private List<long> VstViewed = new List<long>();
        private DateTime ThisDT;
        private List<int> SrtInx;
        private List<TabPage> disabledTabs = new List<TabPage>();

        public Settings(eBrowserType reBrowser, string ruserid, int NumAttached, ref cMacroChanges RxMC, ref cMacroChanges RxMV)
        {
            InitializeComponent();
            userid = ruserid;
            tbUserID.Text = userid;
            eBrowser = reBrowser;
            switch (eBrowser)
            {
                case eBrowserType.eChrome: rbChrome.Checked = true; break;
                case eBrowserType.eFirefox: rbFirefox.Checked = true; break;
                case eBrowserType.eEdge: rbEdge.Checked = true; break;
            }

            if (Properties.Settings.Default.sPPrefix != "init")
                tbPP.Text = Properties.Settings.Default.sPPrefix;
            else
                Properties.Settings.Default.sPPrefix = tbPP.Text;


            if (Properties.Settings.Default.sMSuffix != "init")
                tbMSuffix.Text = Properties.Settings.Default.sMSuffix;
            else
                Properties.Settings.Default.sMSuffix = tbMSuffix.Text;

            tbLongAllowed.Text = Properties.Settings.Default.LongestExpectedURL.ToString();
            tbSpecialWord.Text = Properties.Settings.Default.SpecialWord;
            tbEmail.Text = Properties.Settings.Default.sEmail;
            cbSaveUNK.Checked = Properties.Settings.Default.SaveUnkUrls;
            cbDisableVPaste.Checked = Properties.Settings.Default.Vdisable;
            cbRepeatSearch.Checked = Properties.Settings.Default.WSrepeat;
            cbRFsticky.Checked = Properties.Settings.Default.AllowSTICKYedits;
            cbNoJust.Checked = Properties.Settings.Default.DoJust;
            cbEnabSpell.Checked = Properties.Settings.Default.UseSpellChecker;
            cbRetMac.Checked = Properties.Settings.Default.bStartupReturn;
            cbUsePrefix.Checked = Properties.Settings.Default.bUsePrefix;
            cbUseSuffix.Checked = Properties.Settings.Default.bUseSuffix;

            lbSaveLoc.Text = Utils.WhereExe + "\\UrlDebug.txt";
            CountUnkUrls();
            bWantsExit = false;
            xMC = RxMC;
            xMV = RxMV;
            cbFileN.DataSource = xMC.GetFNChanges();
            cbViewed.DataSource = xMV.GetFNViews();
            xMC.nSelectedRowIndex = -1;
            if (cbFileN.Items.Count > 0)
            {
                //string s = cbFileN.Items[0].ToString();
                //SrtInx = xMC.GetMNChanges(s, ref stDataSource, ref stDateTime);
                //gbChanged.Visible = true;
                //tabMacroInfo.
            }
            ckShowSplash.Checked = (Properties.Settings.Default.sSplash != Properties.Settings.Default.cSplash);
            cbDoSpell.Checked = Properties.Settings.Default.SpellCheckSave || cbEnabSpell.Enabled;
            cbDoSpell.Enabled = cbEnabSpell.Checked;
            xMC.sGoTo = "";
            tbPathMacro.Text = Properties.Settings.Default.HTTP_HP;
            cbDoNotRead.Checked = Properties.Settings.Default.IgnoreHTTP;
            cbDoNotRead.Enabled = (tbPathMacro.Text != "");
            if (Properties.Settings.Default.MacroArchive != "")
                tbPathMacro.Text = Properties.Settings.Default.HTTP_HP;
            tbPathBU.Text = Properties.Settings.Default.MacroArchive;
            cbAllowChange.SelectedIndex = Properties.Settings.Default.AllowChgInx;
            cbDays.SelectedIndex = Properties.Settings.Default.AllowDaysInx; 
        }


        private void DisableTab(TabControl tabControl, TabPage tabPage)
        {
            if (tabControl.TabPages.Contains(tabPage))
            {
                tabControl.TabPages.Remove(tabPage);
                disabledTabs.Add(tabPage);
            }
        }

        private void EnableTab(TabControl tabControl, TabPage tabPage)
        {
            if (disabledTabs.Contains(tabPage))
            {
                tabControl.TabPages.Add(tabPage);
                disabledTabs.Remove(tabPage);
            }
        }

        private void CountUnkUrls()
        {
            if (File.Exists(lbSaveLoc.Text))
            {
                string AllUnkUrls = File.ReadAllText(lbSaveLoc.Text);
                int count = 0;
                foreach (char c in AllUnkUrls)
                {
                    if (c == '\n')
                    {
                        count++;
                    }
                }
                tbURLcnt.Text = "Total unknown " + count.ToString();
                btnShowURL.Enabled = true;
                btnDelURL.Enabled = true;
            }
            else tbURLcnt.Text = "No saved URLs";
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveSettings()
        {
            if (rbChrome.Checked) eBrowser = eBrowserType.eChrome;
            if (rbEdge.Checked) eBrowser = eBrowserType.eEdge;
            if (rbFirefox.Checked) eBrowser = eBrowserType.eFirefox;
            Properties.Settings.Default.BrowserID = (int)eBrowser;
            Properties.Settings.Default.sPPrefix = tbPP.Text;
            Properties.Settings.Default.sMSuffix = tbMSuffix.Text;
            Properties.Settings.Default.LongestExpectedURL = Convert.ToInt32(tbLongAllowed.Text);
            Properties.Settings.Default.Vdisable = cbDisableVPaste.Checked;
            Properties.Settings.Default.WSrepeat = cbRepeatSearch.Checked;
            Properties.Settings.Default.AllowSTICKYedits = cbRFsticky.Checked;
            Properties.Settings.Default.SpellCheckSave = cbDoSpell.Checked;
            Properties.Settings.Default.DoJust = cbNoJust.Checked;
            Properties.Settings.Default.UseSpellChecker = cbEnabSpell.Checked;
            Properties.Settings.Default.bUsePrefix = cbUsePrefix.Checked;
            Properties.Settings.Default.bUseSuffix = cbUseSuffix.Checked;
            Utils.nLongestExpectedURL = Properties.Settings.Default.LongestExpectedURL;
            Utils.BrowserWanted = eBrowser;
            if (tbUserID.Text != "")
            {
                userid = tbUserID.Text;
                Properties.Settings.Default.UserID = userid;
            }
            Utils.VolunteerUserID = userid;
            Properties.Settings.Default.SaveUnkUrls = cbSaveUNK.Checked;
            Properties.Settings.Default.SpecialWord = tbSpecialWord.Text;
            Properties.Settings.Default.sEmail = tbEmail.Text;
            if (ckShowSplash.Checked)
                Properties.Settings.Default.cSplash = "";
            Properties.Settings.Default.IgnoreHTTP = cbDoNotRead.Checked;
            Properties.Settings.Default.AllowChgInx = cbAllowChange.SelectedIndex;
            Properties.Settings.Default.AllowDaysInx = cbDays.SelectedIndex;
            Properties.Settings.Default.bStartupReturn = cbRetMac.Checked;
            Properties.Settings.Default.Save();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
            this.Close();
        }


        private void btnDelURL_Click(object sender, EventArgs e)
        {
            File.Delete(lbSaveLoc.Text);
        }

        private void btnShowURL_Click(object sender, EventArgs e)
        {
            Utils.NotepadViewer(lbSaveLoc.Text);
        }



        private void ShowText(string strTemp)
        {
            if (strTemp == "") return;
            Utils.ShowPageInBrowser("", strTemp);
        }


        private void btnTestPP_Click(object sender, EventArgs e)
        {
            ShowText(tbPP.Text);
        }

        private void btnClrM_Click(object sender, EventArgs e)
        {
            lbEdited.DataSource = null;
            lbEdited.Invalidate();
            cbFileN.DataSource = null;
            cbFileN.Invalidate();
            xMC.ClearChanges();
            lbEdited.Items.Clear();
            cbFileN.Items.Clear();
            tbDateChg.Text = "";
        }

        private void btnDelDelViewed_Click(object sender, EventArgs e)
        {
            RemoveViewHistory();
        }

        private void RemoveViewHistory()
        {
            lbViewed.DataSource = null;
            lbViewed.Invalidate();
            cbViewed.DataSource = null;
            cbViewed.Invalidate();
            xMV.ClearChanges();
            lbViewed.Items.Clear();
            cbViewed.Items.Clear();
        }

        private void cbViewed_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = cbViewed.SelectedIndex;
            if (n < 0) return;
            string s = cbViewed.Items[n].ToString();
            List<string> xstDataSource = new List<string>();
            List<int> xstViewed = new List<int>();
            lbViewed.DataSource = null;
            lbViewed.Invalidate();
            SrtInx = xMV.GetMNViews(s, ref xstDataSource, ref xstViewed);
            VstDataSource.Clear();
            VstViewed.Clear();
            foreach (int i in SrtInx)
            {
                VstDataSource.Add(xstDataSource[i]);
                VstViewed.Add(xstViewed[i]);
            }
            lbViewed.DataSource = VstDataSource;
            lbViewed.Refresh();
        }

        private void cbFileN_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = cbFileN.SelectedIndex;
            if (n < 0) return;
            string s = cbFileN.Items[n].ToString();
            List<string> xstDataSource = new List<string>();
            List<long> xstDateTime = new List<long>();
            lbEdited.DataSource = null;
            lbEdited.Invalidate();
            SrtInx = xMC.GetMNChanges(s, ref xstDataSource, ref xstDateTime);
            stDataSource.Clear();
            stDateTime.Clear();
            foreach (int i in SrtInx)
            {
                stDataSource.Add(xstDataSource[i]);
                stDateTime.Add(xstDateTime[i]);
            }
            lbEdited.DataSource = stDataSource;
            lbEdited.Refresh();
            xMC.nSelectedRowIndex = 0;
        }


        private void btnExitSelect_Click(object sender, EventArgs e)
        {
            int n = xMC.nSelectedRowIndex;
            if (n < 0 || n >= lbEdited.Items.Count) { return; }
            string rowContent = lbEdited.Items[n].ToString();
            string sFN = cbFileN.Text;
            xMC.sGoTo = sFN + ":" + rowContent;
            this.Close();
        }

        private string sExtractChange(long l)
        {
            string s = xMC.TicksToHex(l);
            int n = 1 + xMC.GetZ000(s);
            return "(" + n.ToString() + ")";
        }

        private void lbEdited_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string formattedDate = date.ToString("MMMM dd yyyy, hh:mm tt");
            //  July 04 2024, 01:45 PM
            int n = lbEdited.SelectedIndex;
            if (n < 0) return;
            xMC.nSelectedRowIndex = n;
            string sChanges = sExtractChange(stDateTime[n]);
            ThisDT = new DateTime(stDateTime[n]);
            tbDateChg.Text = ThisDT.ToString("MMMM dd yyyy, hh:mm tt " + sChanges);
        }

        private void tabMacroInfo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnDelSel_Click(object sender, EventArgs e)
        {
            if (cbViewed.SelectedItem == null) return;
            string sFN = cbViewed.SelectedItem.ToString();
            bool bAny = false;
            foreach (string s in lbViewed.SelectedItems)
            {
                xMV.RemView(sFN, s);
                bAny = true;
            }
            if (bAny)
            {
                if (xMV.bIsEmpty())
                {
                    RemoveViewHistory();
                    return;
                }
                cbViewed.DataSource = null;
                cbViewed.Invalidate();
                cbViewed.DataSource = xMV.GetFNViews();
                cbViewed.Refresh();
            }
        }

        private void btnSavMS_Click(object sender, EventArgs e)
        {
            if (cbisPrinter.Checked)
                Properties.Settings.Default.sMSuffix = tbMSuffix.Text;
            else
                Properties.Settings.Default.NotPrnSuffix = tbMSuffix.Text;
            Properties.Settings.Default.Save();
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            xMV.SaveChanges();
            xMC.SaveChanges();
        }

        private void cbisPrinter_CheckedChanged(object sender, EventArgs e)
        {
            tbMSuffix.Text = cbisPrinter.Checked ? Properties.Settings.Default.sMSuffix : Properties.Settings.Default.NotPrnSuffix;
        }

        private void blnTestS_Click(object sender, EventArgs e)
        {
            ShowText(tbMSuffix.Text);
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            string[] sMisc = { "emoji.html", "HP_CountryCodes.html", "SiteMap.html", "MacroChanges.txt", "MacroViews.txt"};

            string zipPath = Utils.WhereExe + "/backup.zip";
            using (FileStream zipToOpen = new FileStream(zipPath, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                {
                    foreach (string s in Utils.ListAllTxt)
                    {
                        string file = Utils.WhereExe + "/" + s;
                        if (File.Exists(file))
                        {
                            archive.CreateEntryFromFile(file, Path.GetFileName(file));
                        }
                    }
                    foreach (string strName in sMisc)
                    {
                        string file = Utils.WhereExe + "/" + strName;
                        if (File.Exists(file))
                        {
                            archive.CreateEntryFromFile(file, Path.GetFileName(file));
                        }
                    }
                    archive.Dispose();
                }
            }
        }

        private void btnRestoreM_Click(object sender, EventArgs e)
        {
            string zipPath = Utils.WhereExe + "\\backup.zip";
            try
            {
                ZipArchive za = ZipFile.OpenRead(zipPath);
                foreach(ZipArchiveEntry zae in za.Entries)
                {
                    string s = Utils.WhereExe + "\\" + zae.FullName.ToString();
                    if (File.Exists(s))
                    {
                        File.Delete(s);
                    }
                }
                ZipFile.ExtractToDirectory(zipPath, Utils.WhereExe);
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message);
            }
        }

        private void btnResetApp_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("This app must exit for reset to work" + Environment.NewLine +
                "only internal app settings are changed" + Environment.NewLine + "no macros are affected", "Click OK to Reset", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                Properties.Settings.Default.Reset();
                bWantsExit = true;
                this.Close();
            }
        }

        private string FindFolder()
        {
            // Initialize a new instance of the FolderBrowserDialog class
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                // Set a description for the dialog
                folderBrowserDialog.Description = "Select a folder to save files";

                // Show the dialog and check if the user clicked OK
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    string selectedFolder = folderBrowserDialog.SelectedPath;
                    return selectedFolder;
                }
            }
            return "";
        }

        private void btnSetBUpath_Click(object sender, EventArgs e)
        {
            string sFolder = FindFolder();
            tbPathBU.Text = sFolder;
            Properties.Settings.Default.MacroArchive = sFolder;
            Properties.Settings.Default.Save();
        }

        private void btnViewBU_Click(object sender, EventArgs e)
        {
            string s = tbPathBU.Text;
            if(Directory.Exists(s))
                Process.Start(s);
        }

        private void DeleteZips()
        {
            string folderPath = tbPathBU.Text;
            if (Directory.Exists(folderPath))
            {
                string filePattern = "*.zip";

                try
                {
                    string[] files = Directory.GetFiles(folderPath, filePattern);
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void btnClrBU_Click(object sender, EventArgs e)
        {
            DeleteZips();
        }
    }
}