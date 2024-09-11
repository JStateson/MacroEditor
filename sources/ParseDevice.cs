using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
/*
all of this came from background.js in my HP_Search project
*/
namespace MacroEditor.sources
{
    internal class ParseDevice
    {
        private string sProductID;
        private string sModel;

        public string[] sUrlSearchPR = //9
        {
            "https://support.hp.com/us-en/deviceSearch?q=@model@&origin=pdp",
            "https://youtube.com/@HPSupport/search?query=@model@",
            "https://www.google.com/search?q=HP+printer+@model@+youtube+network+connect",
            "https://www.google.com/search?q=HP+@model@+youtube+Wi-Fi+direct",
            "https://www.google.com/search?q=HP+@model@+printer+factory+reset+youtube",
            "https://www.google.com/search?q=Setup+And+User+Guides+HP+support+@model@+printer",
            "https://www.google.com/search?q=hp+support+printer+software+and+drivers+download+@model@",
            "https://partsurfer.hp.com/?searchtext=@model@",
            "https://www.google.com/search?q=HP+printer+@model@+disassembly+youtube"
        };

        public string[] sUrlSearchPC =  //9
{
            "https://support.hp.com/us-en/deviceSearch?q=@model@&origin=pdp",
            "https://youtube.com/@HPSupport/search?query=@model@",
            "https://www.google.com/search?q=Setup+And+User+Guides+HP+support+@model@",
            "https://www.google.com/search?q=hp+support+software+and+drivers+download+pc+@model@",
            "https://partsurfer.hp.com/?searchtext=@model@",
            "https://www.google.com/search?q=HP+laptop+notebook+@model@+disassembly+youtube",
        };

        private string[] sUrlSearchPRID =
        {
            "@IDReference@","@IDYouTube@","@IDNetwork Connect@","@IDWiFi Connect@","@IDReset Printer@",
            "@IDDocuments@","@IDSoftware@","@IDParts@","@IDAssemblyPR@"
        };

        private string[] sUrlSearchPCID =
        {
            "@IDReference@","@IDYouTube@",
            "@IDDocuments@","@IDSoftware@","@IDParts@","@IDAssemblyPR@"
        };

        public string[] sTemplettID =
        {
            "@IDReset Printer@","@IDNetwork Connect@","@IDSoftware@","@IDDocuments@","@IDYouTube@",
            "@IDReference@","@IDParts@" ,"@IDAssemblyPC@","@IDAssemblyPR@","@IDWiFi Connect@"
        };


        public string SystemSearch(string sSub)
        {
            return sUrlSearchPC[0].Replace("@model@", sSub);
        }

        public string GetSearchUrl(string id, string idValue)
        {
            int i = 0;
            string t;
            foreach(string s in sUrlSearchPRID)
            {
                if(s == id)
                {
                    t = sUrlSearchPR[i];
                    return t.Replace("@model@", idValue);
                }
                i++;
            }
            return "";
        }

        public string Parse(string ist)
        {
            sProductID = "";
            sModel = "";

            if (ist.Length > 48)  //clipboard may have garbage--------------------^ 48
            {
                int i = ist.IndexOf("Currently Viewing:");
                if (i == -1)
                {
                    return "";
                }
            }
            string sInitial = ist.Trim();
            ist = " " + sInitial + " ";
            string str1 = CurrentlyViewing(sInitial);
            if (str1 == "") str1 = ist;
            string str = RemoveCommonItems(str1);
            str = FixSpace(str);
            string strID = str;
            if (str1 == "")
            {
                string str2 = HasBothItems(str);
                if (str2 != "")
                {
                    int i = str2.IndexOf("(*)");
                    if (i >= 0)
                    {
                        strID = str2.Substring(i + 3);
                        str = RemoveJunk(str.Substring(0, i));
                    }
                }
            }
            else
            {
                string str2 = HasBothItems(str1);
                if (str2 != "")
                {
                    int i = str2.IndexOf("(*)");
                    if (i > 0)
                    {
                        strID = str2.Substring(i + 3);
                        str = RemoveCommonItems(" " + str2.Substring(0, i));
                    }
                }
            }
            /*
            if(sInitial == str) // no change is unusual
            {
                if(strID.Length > 7) // product ID is 7 long
                {
                    str = "";
                    strID = str;
                }
            }
            */
            sProductID = strID;
            sModel = str;
            return str;
        }

        private string MyReplace(string sIN, string sLC, string sP)
        {
            string s = sP;
            int n = s.Length;
            string b = "                     ";
            int i = sLC.IndexOf(s);
            if (i < 0) return sIN;
            if (i == 0)
            {
                return b.Substring(0, n) + sIN.Substring(n);
            }
            else
            {
                return sIN.Substring(0, i) + b.Substring(0, n) + sIN.Substring(i + n);
            }
        }

        // MyReplace does not change var t so duplicate require additional t = s 
        private string RemoveCommonItems(string strIn)
        {
            string s = "" + strIn + " ";
            string t = s.ToLower();
            int i = t.IndexOf(" inch ");
            if (i > 0)
            {
                s = strIn.Substring(i + 6);
                t = s.ToLower();
            }
            s = MyReplace(s, t, "\"");
            t = s.ToLower();
            s = MyReplace(s, t, "\"");
            t = s.ToLower();
            s = MyReplace(s, t, " omen by ");
            s = MyReplace(s, t, " hp ");
            s = MyReplace(s, t, " by ");
            s = MyReplace(s, t, " pc ");
            s = MyReplace(s, t, " omen ");
            s = MyReplace(s, t, " aio ");
            s = MyReplace(s, t, " laptop ");
            s = MyReplace(s, t, " notebook ");
            s = MyReplace(s, t, " obelisk ");
            s = MyReplace(s, t, " desktop ");
            s = MyReplace(s, t, " printer ");
            s = MyReplace(s, t, " all-in-one ");
            s = MyReplace(s, t, " officejet ");
            s = MyReplace(s, t, " advantage ");
            s = MyReplace(s, t, " ink ");
            s = MyReplace(s, t, " plus ");
            s = MyReplace(s, t, " laserjet ");
            s = MyReplace(s, t, " deskjet ");
            s = MyReplace(s, t, " color ");
            s = MyReplace(s, t, " pavilion ");
            s = MyReplace(s, t, " convertible ");
            s = MyReplace(s, t, " compaq ");
            s = MyReplace(s, t, " product: ");
            s = MyReplace(s, t, " gaming ");
            s = MyReplace(s, t, " currently viewing: ");
            s = MyReplace(s, t, " multifunction ");

            t = s.Replace("  ", " ");
            while (t != s)
            {
                s = t;
                t = s.Replace("  ", " ");
            }
            return RemoveJunk(s);
        }

        //Currently Viewing: "HP Laptop PC 15-dw3000 (31R08AV)" in "Notebook Hardware and Upgrade Que
        private string CurrentlyViewing(string strIn)
        {
            string s = strIn;
            string t = "Currently Viewing: \""; // could be HP or omen and any case
            int i = s.IndexOf(t);
            if (i < 0) return "";
            i += t.Length;
            int j = s.IndexOf("\"", i);
            if (i < 0) return "";
            t = s.Substring(i, j - i);
            int k = t.IndexOf(" ");
            if(k == -1)
                return t;
            return t.Substring(k);
        }

        private bool isNumber(string value)
        {
            bool isNumeric = int.TryParse(value, out _);
            return isNumeric;
        }

        private string  FixSpace(string str)
        {
            var n = str.IndexOf(' ');
            if (n < 2) return str; // 9- is smallest but could be 23m- or 3 chars before a missing dash
            string  s = str.Substring(0, 2);
            //return s; returned 2 digits
            if (isNumber(s))
            {
                s = str.Substring(0, n) + "-";
                s += str.Substring(n + 1);
                return s;
            }
            return str;
        }

        //"15-xxxx (yyyyyyy)"
        private string  HasBothItems(string str)
        {
            int i, j, n;
            string strID = "";
            string strModel = "";
            str = str.Trim();
            n = str.Length;
            if (n < 16) return "";
            i = str.IndexOf('(');
            if (i < 0) return "";
            j = str.IndexOf(')', i);
            if (j < 0) return "";
            n = j - i;      // might want to remove country code
            if (n != 8) return "";
            strID = str.Substring(i + 1, j - i - 1);
            strModel = str.Substring(0, i).Trim();
            strModel = FixSpace(strModel);
            str = strModel + "(*)" + strID;
            if (str.Length < 15) return "";
            return str;
        }

        private string HasID(string str)
        {
            int i, j, n;
            string strID = "";
            str = str.Trim();
            n = str.Length;
            if (n < 9) return "";
            i = str.IndexOf('(');
            if (i < 0) return "";
            j = str.IndexOf(')', i);
            if (j < 0) return "";
            n = j - i;
            if (n != 8) return "";
            strID = str.Substring(i + 1, j - i - 1);
            return strID;
        }


        private string RemoveJunk(string str)
        {
            int n = 0;
            char res = ' ';
            string str0;
            str = str.Trim();
            str0 = str;

            //remove trailing periods or commas
            n = str.Length - 1;
            if (n < 0) return "";
            res = str[n];
            if (res == '.' || res == ',') str0 = str.Substring(0, n);

            while (str0 != str)
            {
                str = str0;
                n = str.Length - 1;
                res = str[n];
                if (res == '.' || res == ',') str0 = str.Substring(0, n);
            }
            return str.Trim();
        }
        public string GetProductID()
        {
            if (sProductID == "") return sModel;
            return sProductID;
        }
        public string GetModel()
        {
            if (sModel == "") return sProductID;
            return sModel; 
        }
    }
}
