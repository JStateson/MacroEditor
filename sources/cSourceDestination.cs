using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroEditor.sources
{
    public class cSourceDestination
    {

        public List<string> Src = new List<string>();
        public List<string> Des = new List<string>();
        public List<int> PullFromHere = new List<int>();
        public List<string>PushToHere = new List<string>(); // the 6 destinations in the templett
        public List<string> ListOfArgs = new List<string>();
        public List<string> ExclusionList = new List<string>();  // Page and Doc are to be uneditable
        public List<int> TagToTemplett = new List<int>();       // destination associated with tag

        public int GetDesINXfromSrc(string e)
        {
            int i = 0;
            foreach(string s in Src)
            {
                if (s.Contains(e)) return i;
                i++;
            }
            Debug.Assert(false);
            return -1;
        }

        public void AddSrc(string s)
        {
            string[] sS = s.Split(new char[] { ',' });
            string s1 = sS[0];
            string s2 = sS[1];
            if (s2 != "")
            {
                ExclusionList.Add(s1);  // do not allow multiples of these instructions
                ExclusionList.Add(s2);  // text matches the button text
                s2 = "@" + s2 + "@";
            }
            s1 = "@" + sS[0] + "@";
            ListOfArgs.Add(s1);
            if (s2 != "")
            {
                Src.Add(s1 + "," + s2);
                ListOfArgs.Add(s2);
            }
            else Src.Add(s1);
            string s3 = sS[2];
            Des.Add(s3);
            if (PushToHere.Count == 0)
                PushToHere.Add(s3);
            else
            {
                if(PushToHere.IndexOf(s3) == -1)
                        PushToHere.Add(s3);

            }
        }

        // this gives the index into the PrinterHttp
        //@Reset Video@ would be location 0
        public int InxSrcPhrase(string sPhrase)
        {
            int n = 0;
            foreach (string t in Src)
            {
                if (t.Contains(sPhrase)) return n;
                n++;
            }
            Debug.Assert(false);
            return n;
        }

        public bool AllowAdditionalItems(string s)
        {
            foreach (string t in ExclusionList)
            {
                if (t == s) return false;
            }
            return true;
        }
        public List<int> PullFromSrc(string sDes)
        {
            PullFromHere.Clear();
            int n = 0;
            foreach (string s in Des)
            {
                if (sDes == s)
                    PullFromHere.Add(n);
                n++;
            }
            return PullFromHere;
        }

    }
}
