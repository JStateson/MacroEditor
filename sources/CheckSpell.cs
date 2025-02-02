using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Xml.Schema;
using Word = Microsoft.Office.Interop.Word;

namespace MacroEditor
{
    public class cCheckSpell
    {
        private Word.Application _wordApp;
        private Word.Document tempDoc;
        private Word.Range range;
        private string sPathWords;
        private string sAllowedWords;
        private void AddWord(string w)
        {
            sAllowedWords += w + ",";
        }
        private bool bIsAllowed(string w)
        {
            if (sAllowedWords.Contains("," + w + ",")) return true;
            return false;
        }

        private bool IsDocumentOpen(Word.Application wordApp, string filePath)
        {
            foreach (Word.Document doc in wordApp.Documents)
            {
                if (doc.FullName.Equals(filePath, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public void  EditHelpDocs(string file)
        {
            //Word.Application wordApp = new Word.Application();
            //wordApp.Visible = true;
            //_wordApp.Visible = true;
            string filePath = Utils.WhereExe + "\\" + Utils.GetHelpFile(file);
            bool isOpen = IsDocumentOpen(_wordApp, filePath);
            if (isOpen)
            {
                return;
            }
            object aFilePath = filePath; // ?????why?????
            Word.Document doc = _wordApp.Documents.Open(
                ref aFilePath,
                ReadOnly: false,            // Open in read-write mode
                Visible: false,              // Make the document visible
                OpenAndRepair: true,       // Do not attempt to auto-repair the document
                NoEncodingDialog: true      // Suppress encoding dialogs
            );

            Word.Window docWindow = doc.Windows[1];
            docWindow.Visible = true;
            //doc.CheckSpelling();
        }

        public bool Init()
        {
            int n=0;
            if (!Properties.Settings.Default.UseSpellChecker) return false;
            string rpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string sPath = Path.Combine(rpath,"Microsoft\\word");
            string[] unsavedFiles = Directory.GetFiles(sPath, "*.asd");
            _wordApp = new Word.Application();
            _wordApp.Visible = false; // Keep Word invisible
             
            try
            {
                string sfilePath = Utils.WhereExe + "\\" + Utils.ScratchSpellFile;
                object filePath = sfilePath; 
                if(!File.Exists(sfilePath))
                {
                    tempDoc = _wordApp.Documents.Add();
                    tempDoc.SaveAs2(sfilePath);
                    tempDoc.Close();
                }
                //tempDoc = _wordApp.Documents.Add();
                tempDoc = _wordApp.Documents.Open(
                    ref filePath,
                    ReadOnly: true,            // Open in read-write mode
                    Visible: false,              // Make the document invisible
                    OpenAndRepair: false,       // Do not attempt to auto-repair the document
                    NoEncodingDialog: true      // Suppress encoding dialogs
                );
                string sName = tempDoc.Name;
                //Document1
                //012345678
                // each open app has its own Document
                //n = Convert.ToInt32(sName.Substring(8));
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                if (ex.ErrorCode == unchecked((int)0x800A16C1)) // Error code for "file is already open"
                {
                    MessageBox.Show("Document is already open.");
                }
                else
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            if(n > 3) // 2 indicates I running the debug and the release
            {
                DialogResult dr = MessageBox.Show("multiple unsaved word documents are open" + n.ToString(), "OK to delete?",MessageBoxButtons.OKCancel);
                if(DialogResult.OK == dr)
                {
                    foreach(string s in unsavedFiles)
                    {
                        MessageBox.Show("deleting " + s);
                        File.Delete(s);
                    }
                }
            }
            range = tempDoc.Range();
            sPathWords = Utils.WhereExe + "\\" + Utils.SpellList;
            if(!File.Exists(sPathWords))
            {
                sAllowedWords = ",SWSetup,diskdrive,mediaType,powershell,memorychip,devicelocator,MemoryType,partnumber,";
            }
            else sAllowedWords = File.ReadAllText(sPathWords);
            return true;
        }




        private bool DoExit1()
        {
            if(!Properties.Settings.Default.UseSpellChecker) return false;
            File.WriteAllText(sPathWords, sAllowedWords);
            if(tempDoc  != null)
            {
                tempDoc.Close(false);
                Marshal.ReleaseComObject(tempDoc);
                tempDoc = null;
            }
            if (_wordApp != null)
            {
                _wordApp.Quit();
                Marshal.ReleaseComObject(_wordApp);
                _wordApp = null;
            }

            // Force garbage collection
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return true;
        }

        public bool DoExit()
        {
            if(!Properties.Settings.Default.UseSpellChecker) return false;
            try
            {
                // Cleanup resources - close document and application
                if (tempDoc != null)
                {
                    tempDoc.Close(false); // Close without saving changes
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(tempDoc);
                }

                if (_wordApp != null)
                {
                    _wordApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(_wordApp);
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                //MessageBox.Show("COM Exception during cleanup: " + ex.Message); // file was closed
            }
            catch (Exception ex)
            {
                MessageBox.Show("General Exception during cleanup: " + ex.Message);
            }
            finally
            {
                // Ensure all COM objects are released and garbage collected
                tempDoc = null;
                _wordApp = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return true;
        }

        public string[] RunSpellList(string sText)
        {
            string strOut = "";
            range.Text = PreFilter(sText);
            List<string> WordList = new List<string>();
            Word.ProofreadingErrors errors = tempDoc.SpellingErrors;
            if (errors.Count > 0)
            {
                foreach (Word.Range error in errors)
                {
                    string w = error.Text;
                    if(WordList.Count == 0) WordList.Add(w);
                    if (WordList.Contains(w)) continue;
                    WordList.Add(w);
                }

                foreach (string w in WordList)
                {
                    if (bIsAllowed(w)) continue;
                    strOut += w + " ";
                }
            }
            if(strOut != "")
            {
                MessageBox.Show("Spell errors: " + strOut);
            }
            return strOut.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private bool bFoundCrap(ref string s)
        {
            string[] sA = { "<a ", "<img "," class=\"","<iframe ","<!--"};
            string[] sB = { "</a>", ">", "\"", "</iframe>", "-->" };
            string[] sR = {"<tbody>","</tbody>","<tr>","<td>","</td>","</tr>","</table>","<hr>","</hr>",
                "<ul>","<li>", "</ul>", "</li>", "&nbsp;","<ol>","</ol>" };            
            int n = sA.Length;
            foreach(string r in sR)
            {
                s = s.Replace(r, " - ");
            }
            string t = "";
            for (int i = 0; i < n; i++)
            {
                int j = s.IndexOf(sA[i]);
                if (j < 0) continue;
                int k = s.IndexOf(sB[i], j + sA[i].Length);
                if(k < 0) continue;
                if(i == 0)
                {
                    // if href text has no html then check spelling
                    int iBackL = s.LastIndexOf(">", k);
                    Debug.Assert(iBackL != -1);
                    string sTEXT = s.Substring(iBackL + 1,k - iBackL - 1);
                    if(!sTEXT.Contains("http"))
                    {
                        t = s.Substring(0, j) + " - " + sTEXT + " - " + s.Substring(k + sB[i].Length);
                        s = t;
                        return true;
                    }
                }
                t = s.Substring(0, j) + s.Substring(k + sB[i].Length);
                s = t;
                return true;
            }
            return false;
        }

        private string PreFilter(string s)
        {
            bool bRun = true;
            while (bRun)
            {
                bRun = bFoundCrap(ref s);
            }
            return s;
        }

        public void DoCheck(string sText)
        {
            range.Text = PreFilter(sText);
            Word.ProofreadingErrors errors = tempDoc.SpellingErrors;
            if (errors.Count > 0)
            {
                foreach (Word.Range error in errors)
                {
                    string s = error.Text;
                    if (bIsAllowed(s)) continue;
                    DialogResult dr = MessageBox.Show($"Misspelled word: {s}", "Click Yes to accept, NO to continue", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        AddWord(s);
                    }
                    if (dr == DialogResult.Cancel) break;
                }
            }
        }
    }
}
