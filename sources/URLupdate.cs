using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using static MacroEditor.ChangeUrls;
using System.IO;

namespace MacroEditor.sources
{
    public partial class URLupdate : Form
    {
        private List<CBody> Cbodies;
        private List<string> sFiltered = new List<string>();

        BindingSource _bsFiltered = new BindingSource();

        private string sAll = "";
        private List<string> L_All = new List<string>();
        private List<string> S_All = new List<string>();
        private List<string> S_NAM = new List<string>();
        private List<string> E_All = new List<string>();
        private List<string> E_NAM = new List<string>();
        private List<string> MyLeftovers = new List<string>();  
        private List<int> L_FilterIndex = new List<int>();
        private List<int> S_FilterIndex = new List<int>();
        private List<int> E_FilterIndex = new List<int>();
        private List<string>NotExcluded = new List<string>();
        private List<string>NotExcludedIDs = new List<string>();
        private List<string>NotExcludedNames = new List<string>();
        private string sFile = "", sNum="", sName = "";
        private int ListSelectedIndex = -1;
        private string ListSelectedValue = "";
        private string[] CanSearch = new string[] { "manuals", "ftp", "images" };  // only ones that can work

        private ChangeUrls changeUrls = new ChangeUrls();
        private List<cFromToUrls> FromToUrls;

        public URLupdate(ref List<CBody> rCbodies)
        {
            InitializeComponent();
            Cbodies = rCbodies;

            _bsFiltered.DataSource = typeof(string);
            lbFiltered.DataSource = _bsFiltered;

            //sFiltered = new BindingList<string>();
            //lbFiltered.DataSource = sFiltered;
            InitFilter();
            this.Shown += LoadInitialFiles;
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

        private void UpdateBadUrlList()
        {
            FromToUrls = changeUrls.FromToUrls;
            if (FromToUrls == null) return;
            dgv.Rows.Clear();
            foreach (cFromToUrls ft in FromToUrls)
            {
                int inx = dgv.Rows.Add();
                dgv.Rows[inx].Cells[2].Value = ft.FromUrl;
                dgv.Rows[inx].Cells[0].Value = ft.GetWhereUsed(out int n);
                dgv.Rows[inx].Cells[0].Tag = n;
                dgv.Rows[inx].Cells[1].Value = ft.bValidated;
            }
        }

        private void GetBadUrls()
        {
            int nBad = changeUrls.ReadBadUrls(Utils.OldUrlList);
            dgv.Rows.Clear();
            if(nBad > 0)
            {
                UpdateBadUrlList();
            }
        }

        private void LoadInitialFiles(object sender, EventArgs e)
        {
            cbExclude.SelectedIndex = 0;
            cbFilter.SelectedIndex = 0;
            GetBadUrls();
        }

        private bool IsExcluded(string sUrl)
        {
            if (cbExclude == null) return false;
            foreach (string s in cbExclude.Items)
            {
                if (sUrl != null && s != null && sUrl.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsSelected(string url, ref string[] Items)
        {
            for (int i = 1; i < Items.Length; i++)
            {
                string s = Items[i];
                if (url != null && s != null && url.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsLeftover(string sUrl)
        {
            if (cbExclude == null) return false;
            foreach (string sss in cbFilter.Items)
            {
                if(sss == "excluded") continue;
                else
                {
                    string[] ss = sss.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach(string s in ss)
                    {
                        if(s.Contains(':'))continue;
                        string t = s;
                        if (!s.Contains('/')) t += ".com";
                        if (sUrl != null && s != null && sUrl.IndexOf(t, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        List<string> ExtractUrls(string sInput)
        {
            var urls = new List<string>();

            var matches = Regex.Matches(sInput, "href=\"(.*?)\"", RegexOptions.IgnoreCase);

            foreach (Match match in matches)
            {
                urls.Add(match.Groups[1].Value);
            }

            return urls;
        }


        private void ExtractHTTP(ref string s)
        {
            var urls = ExtractUrls(s);
            string sCode = "[" + sFile.PadRight(3) + sNum.PadLeft(3) + "] ";
            foreach (var url in urls)
            {

                if (IsExcluded(url))
                {
                    sFiltered.Add(url);
                    E_All.Add(sFile + " " + sNum);
                    E_NAM.Add(sName);
                }
                else
                {
                    sAll +=sCode + url + Environment.NewLine;
                    NotExcludedIDs.Add(sFile + " " + sNum);
                    NotExcluded.Add(url);
                    NotExcludedNames.Add(sName);
                    if (!IsLeftover(url))
                    {
                        MyLeftovers.Add(url);                      
                        L_All.Add(sFile + " " + sNum);
                    }
                }
            }
        }


        private void SetLeftovers()
        {
            // Pair each item with its original index
            var indexed = MyLeftovers
                .Select((value, index) => new { Value = value, OriginalIndex = index })
                .ToList();

            // Sort by value (case-insensitive)
            var sorted = indexed
                .OrderBy(x => x.Value, StringComparer.OrdinalIgnoreCase)
                .ToList();

            // Extract the sort indices
            List<int> sortIndices = sorted.Select(x => x.OriginalIndex).ToList();
            lbLeftover.Items.Clear();
            foreach (var x in sorted)
            {
                lbLeftover.Items.Add(x.Value);
            }

            foreach (var i in sortIndices)
            {
                L_FilterIndex.Add(i);
            }
        }

        private void InitFilter()
        {
            sAll = "";
            MyLeftovers.Clear();
            NotExcluded.Clear();
            foreach (CBody cb in Cbodies)
            {
                sFile = cb.File;
                sNum = cb.Number;
                sName = cb.Name;
                ExtractHTTP(ref cb.sBody);
                ExtractHTTP(ref cb.rBody);
            }
            tbAll.Text = sAll;
            SetLeftovers();
        }


        private void RunFilter(int nSelected)
        {
            var selectedItem = cbFilter.SelectedItem;
            S_FilterIndex.Clear();
            int j;
            S_All.Clear();
            S_NAM.Clear();
            if (selectedItem != null)
            {
                string svalue = selectedItem.ToString();
                if (svalue == "excluded")
                {
                    // Pair each item with its original index
                    var indexed = sFiltered
                        .Select((value, index) => new { Value = value, OriginalIndex = index })
                        .ToList();

                    // Sort by value (case-insensitive)
                    var sorted = indexed
                        .OrderBy(x => x.Value, StringComparer.OrdinalIgnoreCase)
                        .ToList();

                    // Extract the sort indices
                    List<int> sortIndices = sorted.Select(x => x.OriginalIndex).ToList();
                    sFiltered.Clear();
                    foreach (var x in sorted)
                    {
                        sFiltered.Add(x.Value);
                    }

                    foreach(var i in sortIndices)
                    {
                        E_FilterIndex.Add(i);
                    }

                    //lbFiltered.DataSource = null;
                    //lbFiltered.DataSource = sFiltered.ToList();

                    _bsFiltered.DataSource = null;
                    _bsFiltered.DataSource = sFiltered;
                }
                else
                {
                    string[] sItems = cbFilter.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    sFiltered.Clear();
                    j = 0;
                    foreach (string s in NotExcluded)
                    {
                        if (IsSelected(s, ref sItems))
                        {
                            sFiltered.Add(s);
                            string sCode = "";
                            S_NAM.Add(NotExcludedNames[j]);
                            S_All.Add(NotExcludedIDs[j]);
                        }
                        j++;
                    }

                    // Pair each item with its original index
                    var indexed = sFiltered
                        .Select((value, index) => new { Value = value, OriginalIndex = index })
                        .ToList();

                    // Sort by value (case-insensitive)
                    var sorted = indexed
                        .OrderBy(x => x.Value, StringComparer.OrdinalIgnoreCase)
                        .ToList();

                    // Extract the sort indices
                    List<int> sortIndices = sorted.Select(x => x.OriginalIndex).ToList();
                    sFiltered.Clear();
                    foreach (var x in sorted)
                    {
                        sFiltered.Add(x.Value);
                    }

                    foreach (var i in sortIndices)
                    {
                        S_FilterIndex.Add(i);
                    }

                    _bsFiltered.DataSource = null;
                    _bsFiltered.DataSource = sFiltered;

                }
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = cbFilter.Text;
            int i = s.IndexOf(':');
            if (i == -1)
            {
                tabPage2.Text = s;
            }
            else s= s.Substring(0, i);
            tabPage2.Text = s;
            RunFilter(cbFilter.SelectedIndex);
            btnTEST_F.Enabled = CanSearch.Contains(s);
        }

        private void btnAddItemExclude_Click(object sender, EventArgs e)
        {
            string sNew = cbExclude.Text;
            if(cbExclude.Items.Contains(sNew))
            {
                MessageBox.Show("Already in the list.");
                return;
            }
            cbExclude.Items.Add(sNew);          
            sFiltered.Clear();
            InitFilter();
            RunFilter(0);            
        }

        private void lbFiltered_DoubleClick(object sender, EventArgs e)
        {
            Utils.LocalBrowser(ListSelectedValue);
        }


        private void lbLeftover_DoubleClick(object sender, EventArgs e)
        {
            Utils.LocalBrowser(ListSelectedValue);
        }

        private void lbFiltered_DoubleClick_1(object sender, EventArgs e)
        {
            Utils.LocalBrowser(ListSelectedValue);
        }

        private void lbFiltered_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lbFiltered.SelectedIndex;
            ListSelectedValue = lbFiltered.SelectedItem as string;
            ListSelectedIndex = index;
            int j = ListSelectedIndex;
            string sID = tabPage2.Text;
            if (sID == "excluded")
            {
                int i = E_FilterIndex[j];
                string sTemp = E_All[i];
                int k = sTemp.IndexOf(' ');
                tbFILE_S.Text = sTemp.Substring(0, k);
                tbNUM_S.Text = sTemp.Substring(k + 1);
                lbMacName.Text = E_NAM[i];
            }
            else
            {
                int i = S_FilterIndex[j];
                string sTemp = S_All[i];
                int k = sTemp.IndexOf(' ');
                tbFILE_S.Text = sTemp.Substring(0, k);
                tbNUM_S.Text = sTemp.Substring(k + 1);
                lbMacName.Text = S_NAM[i];
            }
        }




        private bool DidRedirect = false;
        private async Task<bool> TryFetch(string s)
        {
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };
            using (var client = new HttpClient(handler))
            {
                var response = await client.GetAsync(s).ConfigureAwait(false); ;
                if ((int)response.StatusCode >= 300 && (int)response.StatusCode < 400 || (int)response.StatusCode == 404)
                {
                    DidRedirect = true;
                    return true;
                }
                else
                {
                    DidRedirect = false;
                }
            }
            return false;
        }


        public async Task<string> CheckYouTubeUrl(string url)
        {
            if (url == "https://www.youtube.com/@HPSupport/search") return "";

            HttpClient client = new HttpClient();


                client.DefaultRequestHeaders.TryAddWithoutValidation(
                "User-Agent",
                "Mozilla/5.0");

                try
                {
                    var response = await client.GetAsync(url);

                    var html = await response.Content.ReadAsStringAsync();

                    if (html.Contains("Video unavailable"))
                        return "Exists but unavailable";

                    if (html.Contains("This video is private"))
                        return "Private video";

                    if (html.Contains("This video isn't available anymore"))
                        return "Removed video";

                    if (response.IsSuccessStatusCode)
                        return "";// "Video exists";

                    return $"Other status: {(int)response.StatusCode}";
                }
                catch
                {
                    return "Invalid URL or network error";
                }
            
        }

        private bool bStopSearch = false;
        private void btnCancel_Click(object sender, EventArgs e)
        {
            bStopSearch = true;
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv.CurrentCell != null)
            {
                int rowIndex = dgv.CurrentCell.RowIndex;
                lbO_corrected.Text = dgv.Rows[rowIndex].Cells[2].Value.ToString();
                int n = (int) dgv.Rows[rowIndex].Cells[0].Tag;
                tbCntWhere.Text = n.ToString();
                cbMacIDs.Items.Clear();
                string[] NN = dgv.Rows[rowIndex].Cells[0].Value.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach(string s in NN)
                {
                    cbMacIDs.Items.Add(s);
                }
                if(n > 0)
                {
                    cbMacIDs.SelectedIndex = 0;
                }
            }

        }

        private void btnSaveBadList_Click(object sender, EventArgs e)
        {
            changeUrls.SaveBadUrls(Utils.OldUrlList);
            MessageBox.Show("You must exit this entire app before the bad urls show up");
        }

        private void btnRemoveHistory_Click(object sender, EventArgs e)
        {
            File.Delete(Utils.OldUrlList);
            GetBadUrls();
        }

        private async void btnTEST_F_Click(object sender, EventArgs e)
        {
            string sExists, sID = tabPage2.Text;

            int n = 0;
            int i = 0;
            string t;
            if (sID == "excluded") return;  // do not plan on search through the leftovers
            pbUrlErr.Maximum = sFiltered.Count;
            pbUrlErr.Value = 0;
            tb_F_err.Text = "0";
            bool bAny = false;
            bool bExists = true;
            foreach (string s in sFiltered)
            {
                int j = S_FilterIndex[i];
                string sTemp = S_All[j];
                int k = sTemp.IndexOf(' ');
                string debSys = sTemp.Substring(0, k);
                string debNum = sTemp.Substring(k+1);
                string debName = S_NAM[j];
                if (bStopSearch) break;
                lbFiltered.SelectedIndex = i;
                if (i >= 0)
                {
                    int visibleItems = lbFiltered.ClientSize.Height / lbFiltered.ItemHeight;
                    int topIndex = Math.Max(0, i - visibleItems / 2);
                    lbFiltered.TopIndex = topIndex;
                }
                bAny = false;
                bExists = true;
                switch (sID)
                {
                    case "manuals":
                        if (TryFetch(s).Result)
                        {
                            n++;
                            bAny = true;
                        }
                        break;

                    case "ftp":
                        sExists = await changeUrls.HttpFileExistsAsync(s);
                        bExists = (sExists == "");
                        if (!bExists)
                        {
                            n++;
                            string st = s.Replace("https://", "");
                            st = st.Replace("http://", "");
                            changeUrls.AddUrl(st, "", debSys, debName, debNum);
                        }
                        break;
                    case "document":
                        k = s.IndexOf('?');
                        t = s;
                        if (k != -1) t = s.Substring(k);
                        // cannot easily be done due to bot protection, so just add to the list of bad urls and let someone check them manually
                        break;
                    case "youtube":
//                        sExists = await CheckYouTubeUrl(s);
                        break;
                    case "images":
                        t = s;
                        k = s.IndexOf("/image-size");
                        if(k != -1) t = s.Substring(0,k);
                        sExists = await changeUrls.ExistsUsingAgent(t);
                        bExists = (sExists == "");
                        if (!bExists)
                        {
                            n++;
                            bAny = true;
                        }
                        break;
                }
                if(bAny)
                {
                    tb_F_err.Text = n.ToString();
                    changeUrls.AddUrl(s, "", debSys, debName, debNum);
                }

                pbUrlErr.Value++;
                Application.DoEvents();
                i++;
            }
            if(n>0)
            {
                UpdateBadUrlList();
            }
            pbUrlErr.Value = 0;
        }
        

        private void lbLeftover_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lbLeftover.SelectedIndex;
            ListSelectedValue = lbLeftover.SelectedItem as string;
            ListSelectedIndex = index;
            int j = ListSelectedIndex;
            int i = L_FilterIndex[j];
            string sTemp = L_All[i];
            int k = sTemp.IndexOf(' ');
            tbFILE_L.Text = sTemp.Substring(0, k);
            tbNUM_L.Text = sTemp.Substring(k + 1);  
        }
    }
}
