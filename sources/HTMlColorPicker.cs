using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;


namespace MacroEditor
{
    public partial class HTMlColorPicker : Form
    {
        private int Tab1 = -1;
        private int Tab2 = -1;
        public TextBox WantedColors { get; set; }

        public HTMlColorPicker(Font font)
        {
            InitializeComponent();
            lbColors.Font = font;
            lbInvCol.Font = font;
            FillBox();
        }
        private void FillBox()
        {
            /*
            lbColors.Items.AddRange(new string[]
            {
            "black", "silver", "gray", "white", "maroon", "red", "purple", "fuchsia",
            "green", "lime", "olive", "yellow", "navy", "blue", "teal", "aqua"
            });
            */
            for (int i = 0; i < lbColors.Items.Count; i++)
                lbColors.Items[i] = lbColors.Items[i].ToString().ToUpper();
        }

        private System.Drawing.Color cfn(string s)
        {
            return System.Drawing.Color.FromName(s);
        }
        private void SetListBoxHeight(ListBox listBox)
        {
            int itemHeight = listBox.GetItemHeight(0);
            int border = listBox.Height - listBox.ClientSize.Height;
            int totalItemHeight = (listBox.Items.Count * itemHeight) + border;
            listBox.Height = totalItemHeight;
        }


        private void lbColorsDrawItem(ListBox listBox, DrawItemEventArgs e, int nLB)
        {
            if (e.Index < 0) return;
            e.DrawBackground();
            System.Drawing.Color Fcolor = cfn("black");
            System.Drawing.Color Bcolor = cfn("black");
            string item = listBox.Items[e.Index].ToString();

            switch (nLB)
            {
                case 0 :
                    Fcolor = cfn(item);
                    Bcolor = cfn("white");
                    break;
                case 1 :
                    Fcolor = cfn("black");
                    Bcolor = cfn(item);
                    using (SolidBrush brush = new SolidBrush(Bcolor))
                    {
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    }
                    break;
            }

            if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
            {
                // Draw the focus rectangle if the item has focus
                e.DrawFocusRectangle();
            }


            using (SolidBrush brush = new SolidBrush(Fcolor))
            {
                e.Graphics.DrawString(item, e.Font, brush, e.Bounds);//, StringFormat.GenericDefault);
            }
            SetListBoxHeight(listBox);
        }


        private void lbColors_DrawItem(object sender, DrawItemEventArgs e)
        {
            lbColorsDrawItem((ListBox)sender, e, 0);
        }

        private void lbInvCol_DrawItem(object sender, DrawItemEventArgs e)
        {
            lbColorsDrawItem((ListBox)sender, e, 1);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {                
            if(Tab1 >= 0 && Tab2 >= 0)
                WantedColors = tbOut;
            this.Close();
        }

        private void lbColorsMeasureItem(object sender, MeasureItemEventArgs e)
        {
            
            if (e.Index >= 0)
            {
                // Get the text of the item
                string text = lbColors.Items[e.Index].ToString();

                // Get the font size of the ListBox
                float fontSize = lbColors.Font.Size;

                // Calculate the size of the text
                Size textSize = TextRenderer.MeasureText(text, lbColors.Font);
                e.ItemHeight = (int)(textSize.Height);
            }
        }
        private void lbColors_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            lbColorsMeasureItem(sender, e);
        }

        private void lbInvCol_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            lbColorsMeasureItem(sender, e);
        }

        private void lbColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tab1 = lbColors.SelectedIndex;
            tbOut.ForeColor = cfn(lbColors.Items[Tab1].ToString());
        }

        private void lbInvCol_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tab2 = lbInvCol.SelectedIndex;
            tbOut.BackColor = cfn(lbInvCol.Items[Tab2].ToString());
        }
    }
}
