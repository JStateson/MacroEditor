using MacroEditor;
using MacroEditor.sources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Threading;

namespace MacroEditor.sources
{
    public class cEachTag
    {
        public int iTag;
        public string TagName;
        public List<string> SourceHREF = new List<string>();
        public List<string> SourceTEXT = new List<string>();
    }

    public class cDBresult
    {
        private int i;
        private int j;
        private string CurrentTag;
        public string sName;
        public string sysType;
        public string sTimeStamp;
        public bool IsPrinter;
        public int TotalTags;
        public List<cEachTag> RecordSet = new List<cEachTag>();
    }

    public class PrinterDB
    {
        private string pathFolders = ""; 
        public string LastRecordWritten = "";
        public string LastKeyWritten = "";

        public cDBresult LastDBresult = new cDBresult();
        public string LastDBkey;


        public string GetDeletedName(string strType, string sOldName)
        {
            string MacName = strType + "_" + sOldName.Replace(".txt", "");
            int GiveUP = 10;
            for (int i = 1; i < GiveUP; i++)
            {
                string sTry = MacName + "_" + i.ToString() + ".txt";
                string sPath = Utils.WhereExe + "/deleted/" + sTry;
                if (File.Exists(sPath))continue;
                return sPath.Replace(".txt", "");
            }
            Debug.Assert(false);
            return Utils.GetDateTimeName("");
        }


        public bool InitDB()
        {
            pathFolders = Utils.WhereExe;
            string t;
            try
            {
                if (!Directory.Exists(pathFolders))
                {
                    Directory.CreateDirectory(pathFolders);
                }
                t = pathFolders + "\\deleted";
                if (!Directory.Exists(t))
                    Directory.CreateDirectory(t);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        private string PathFromKey(string k)
        {
            int i = k.IndexOf(':');
            Debug.Assert(i >= 0);
            return pathFolders + "\\" + k.Substring(0, i) + "\\" + k.Substring(i + 1) + ".txt";
        }
        public bool WriteRawPrn(string sKey, ref string sOutput)
        {
            try
            {
                File.WriteAllText(PathFromKey(sKey), sOutput);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }



        private bool DBParse(ref string sIn, ref string sOut, ref int inx)
        {
            int n = sIn.Length;
            if (inx >= n) return false;
            int i = sIn.IndexOf(Environment.NewLine, inx);
            if (i < 0)  // need to handle end of file when no newline at end
            {
                sOut = sIn.Substring(inx);
                return true;
            }
            sOut = sIn.Substring(inx, i - inx);
            inx = i + Environment.NewLine.Length;
            if (inx < n) return true;
            return false;
        }

        //key, num tags, tag1, number entries, entry 1, entery 2, ... tag2 newline is separator
        //<!-- @MACRO@:(@key@) TimeStamp=DDDDDDDD_TTTTTT -->
        public cDBresult ParseRecord(ref string CurrentRecord)
        {
            bool a = false;
            bool b = false;
            int i;
            int jTraverse = 0;
            int NumberTags = 0;

            cDBresult dBresult = new cDBresult();
            dBresult.sysType = "";
            dBresult.sName = "";
            if (CurrentRecord == "") return null;

            //b = DBParse(ref CurrentRecord, ref sComment, ref jTraverse);
            //b |= Utils.IsValidComment(ref sComment, ref dBresult.sName, ref dBresult.sysType,
            //    ref dBresult.sKey, ref dBresult.IsPrinter, ref dBresult.sTimeStamp);

            //b &= DBParse(ref CurrentRecord, ref KeyRead, ref jTraverse);
            //b &= (KeyRead == dBresult.sKey);
            //Debug.Assert(b);
            string sNumTag = "";
            b = DBParse(ref CurrentRecord, ref sNumTag, ref jTraverse);
            b &= int.TryParse(sNumTag, out NumberTags);
            dBresult.TotalTags = NumberTags;
            Debug.Assert(b);
            for (i = 0; i < NumberTags && b; i++)
            {
                cEachTag et = new cEachTag();
                string sTag = "";
                et.iTag = 0;
                a = DBParse(ref CurrentRecord, ref sTag, ref jTraverse);
                a &= int.TryParse(sTag, out et.iTag);
                a &= DBParse(ref CurrentRecord, ref et.TagName, ref jTraverse);
                int n = 0;
                a &= DBParse(ref CurrentRecord, ref sTag, ref jTraverse);
                a &= int.TryParse(sTag, out n);
                string sTEXT = "";
                string sHREF = "";
                for (int k = 0; k < n && a; k++)
                {
                    a &= DBParse(ref CurrentRecord, ref sHREF, ref jTraverse);
                    et.SourceHREF.Add(sHREF);
                    a &= DBParse(ref CurrentRecord, ref sTEXT, ref jTraverse);
                    et.SourceTEXT.Add(sTEXT);
                }
                dBresult.RecordSet.Add(et);
                b &= a;
            }
            return dBresult;
        }

        // create, add form and then write 
        public bool CreateRecord(int nTags)
        {
            LastDBresult.TotalTags = nTags;
            LastDBresult.RecordSet.Clear();

            return true;
        }
        public void AddNextRecord(int iTag, string TagName, string sHttp, string sText)
        {
            cEachTag et;
            if (LastDBresult.RecordSet.Count == 0)
            {
                et = new cEachTag();
                et.iTag = iTag;
                et.TagName = TagName;
                et.SourceHREF.Add(sHttp);
                et.SourceTEXT.Add(sText);
                LastDBresult.RecordSet.Add(et);
                return;
            }
            foreach (cEachTag et1 in LastDBresult.RecordSet)
            {
                if (et1.iTag == iTag)
                {
                    et1.SourceHREF.Add(sHttp);
                    et1.SourceTEXT.Add(sText);
                    return;
                }
            }
            et = new cEachTag();
            et.iTag = iTag;
            et.TagName = TagName;
            et.SourceHREF.Add(sHttp);
            et.SourceTEXT.Add(sText);
            LastDBresult.RecordSet.Add(et);
        }

        //key, num tags, tag1, number entries, entry 1, entry 2, ... tag2 newline is separator
        // an entry is a pair of string, the HREF and the TEXT also using newline
        public string FormRecord()
        {
            //string sKey = DBresult.sKey.Trim();
            string sBody = "";
            sBody += LastDBresult.RecordSet.Count.ToString() + Environment.NewLine;
            Debug.Assert(LastDBresult.TotalTags == LastDBresult.RecordSet.Count);
            foreach (cEachTag et in LastDBresult.RecordSet)
            {
                sBody += et.iTag.ToString() + Environment.NewLine;
                sBody += et.TagName.ToString() + Environment.NewLine;
                int n = et.SourceHREF.Count;
                sBody += n.ToString() + Environment.NewLine;
                for (int i = 0; i < n; i++)
                {
                    sBody += et.SourceHREF[i] + Environment.NewLine;
                    sBody += et.SourceTEXT[i] + Environment.NewLine;
                }
            }
            return sBody.Trim();
        }

        public void GetFormatFromRecord(ref cDBresult dbR, ref string FmtOut)
        {
            FormPrinter fpNew = new FormPrinter();
            fpNew.Init();
        }

        public bool FormatRecord(string sRawIn, ref string FmtOut)
        { 
            LastDBresult = ParseRecord(ref sRawIn);
            if (LastDBresult == null) return false;
            return FormatParsedRecord(ref LastDBresult, ref FmtOut);
        }

        public bool FormatParsedRecord(ref cDBresult dbResult, ref string FmtOut)
        {
            int i, n;
            FormPrinter fpNew = new FormPrinter();
            fpNew.Init();

            foreach (cEachTag et in dbResult.RecordSet)
            {
                string e = Utils.sTOe(et.TagName);
                int iDes = fpNew.SourceDestination.GetDesINXfromSrc(e); // Reset Video  
                string sDes = fpNew.SourceDestination.Des[iDes]; // becomes @Reset Printer@
                int iTgt = fpNew.SourceDestination.PushToHere.IndexOf(sDes); // index to that http table
                n = et.SourceTEXT.Count;
                for (i = 0; i < n; i++)
                {
                    int iTag = et.iTag;
                    iTag = fpNew.TagNameToPhrase(et.TagName);
                    fpNew.PrinterListH[iTag].Add(et.SourceHREF[i]);
                    fpNew.PrinterListT[iTag].Add(et.SourceTEXT[i]);
                    fpNew.SourceDestination.TagToTemplett.Add(iTgt);  //may not be needed ??
                    string s = et.TagName;
                    fpNew.Reduce(s, iTag, et.SourceTEXT[i]);
                }
            }
            return fpNew.CreateFormData(ref FmtOut);
        }

    }
}
