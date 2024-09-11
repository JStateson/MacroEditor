namespace MacroEditor.sources
{
    partial class ShowDups
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gbDup = new System.Windows.Forms.GroupBox();
            this.btnExitEdit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbMacroID = new System.Windows.Forms.ListBox();
            this.lbUrls = new System.Windows.Forms.ListBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.tbNum = new System.Windows.Forms.TextBox();
            this.tbGoTo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbSelect = new System.Windows.Forms.GroupBox();
            this.btnImage = new System.Windows.Forms.Button();
            this.btnNS = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gbDup.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDup
            // 
            this.gbDup.Controls.Add(this.btnExitEdit);
            this.gbDup.Controls.Add(this.label3);
            this.gbDup.Controls.Add(this.label2);
            this.gbDup.Controls.Add(this.lbMacroID);
            this.gbDup.Location = new System.Drawing.Point(13, 13);
            this.gbDup.Margin = new System.Windows.Forms.Padding(4);
            this.gbDup.Name = "gbDup";
            this.gbDup.Padding = new System.Windows.Forms.Padding(4);
            this.gbDup.Size = new System.Drawing.Size(389, 440);
            this.gbDup.TabIndex = 1;
            this.gbDup.TabStop = false;
            this.gbDup.Text = "Files Duplicated";
            // 
            // btnExitEdit
            // 
            this.btnExitEdit.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnExitEdit.Location = new System.Drawing.Point(197, 307);
            this.btnExitEdit.Name = "btnExitEdit";
            this.btnExitEdit.Size = new System.Drawing.Size(104, 23);
            this.btnExitEdit.TabIndex = 12;
            this.btnExitEdit.Text = "Exit to Macro";
            this.btnExitEdit.UseVisualStyleBackColor = true;
            this.btnExitEdit.Click += new System.EventHandler(this.btnExitEdit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Info;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(194, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 128);
            this.label3.TabIndex = 11;
            this.label3.Text = "Duplicates in a page are\r\nshown in parenthesis.\r\nDuplicates are counted\r\nin pairs" +
    " so 3 duplicates\r\nmean there are 3 pairs of\r\nidentical URLs.  Consider\r\nreplacin" +
    "g a URL with a\r\nClick Me text.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Info;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(39, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 32);
            this.label2.TabIndex = 9;
            this.label2.Text = "Double click to view\r\nthe macro body";
            // 
            // lbMacroID
            // 
            this.lbMacroID.FormattingEnabled = true;
            this.lbMacroID.ItemHeight = 16;
            this.lbMacroID.Location = new System.Drawing.Point(56, 94);
            this.lbMacroID.Name = "lbMacroID";
            this.lbMacroID.Size = new System.Drawing.Size(120, 324);
            this.lbMacroID.TabIndex = 8;
            this.lbMacroID.SelectedIndexChanged += new System.EventHandler(this.lbMacroID_SelectedIndexChanged);
            this.lbMacroID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbMacroID_MouseDoubleClick);
            // 
            // lbUrls
            // 
            this.lbUrls.FormattingEnabled = true;
            this.lbUrls.ItemHeight = 16;
            this.lbUrls.Location = new System.Drawing.Point(459, 73);
            this.lbUrls.Name = "lbUrls";
            this.lbUrls.Size = new System.Drawing.Size(692, 324);
            this.lbUrls.TabIndex = 3;
            this.lbUrls.SelectedIndexChanged += new System.EventHandler(this.lbUrls_SelectedIndexChanged);
            // 
            // btnTest
            // 
            this.btnTest.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnTest.Location = new System.Drawing.Point(830, 429);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(149, 23);
            this.btnTest.TabIndex = 4;
            this.btnTest.Text = "Show URL in page";
            this.btnTest.UseVisualStyleBackColor = true;
            // 
            // btnGoTo
            // 
            this.btnGoTo.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnGoTo.Location = new System.Drawing.Point(510, 430);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(55, 23);
            this.btnGoTo.TabIndex = 5;
            this.btnGoTo.Text = "Go To";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // tbNum
            // 
            this.tbNum.Location = new System.Drawing.Point(572, 29);
            this.tbNum.Margin = new System.Windows.Forms.Padding(4);
            this.tbNum.Name = "tbNum";
            this.tbNum.Size = new System.Drawing.Size(67, 22);
            this.tbNum.TabIndex = 2;
            // 
            // tbGoTo
            // 
            this.tbGoTo.Location = new System.Drawing.Point(572, 430);
            this.tbGoTo.Margin = new System.Windows.Forms.Padding(4);
            this.tbGoTo.Name = "tbGoTo";
            this.tbGoTo.Size = new System.Drawing.Size(70, 22);
            this.tbGoTo.TabIndex = 6;
            this.tbGoTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbGoTo_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(456, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Total URLs";
            // 
            // gbSelect
            // 
            this.gbSelect.Location = new System.Drawing.Point(13, 470);
            this.gbSelect.Name = "gbSelect";
            this.gbSelect.Size = new System.Drawing.Size(986, 92);
            this.gbSelect.TabIndex = 8;
            this.gbSelect.TabStop = false;
            this.gbSelect.Text = "Select File or all";
            // 
            // btnImage
            // 
            this.btnImage.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnImage.Location = new System.Drawing.Point(1059, 525);
            this.btnImage.Name = "btnImage";
            this.btnImage.Size = new System.Drawing.Size(61, 23);
            this.btnImage.TabIndex = 0;
            this.btnImage.Text = "Images";
            this.toolTip1.SetToolTip(this.btnImage, "All image urals");
            this.btnImage.UseVisualStyleBackColor = true;
            this.btnImage.Click += new System.EventHandler(this.btnImage_Click);
            // 
            // btnNS
            // 
            this.btnNS.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnNS.Location = new System.Drawing.Point(1048, 482);
            this.btnNS.Name = "btnNS";
            this.btnNS.Size = new System.Drawing.Size(87, 23);
            this.btnNS.TabIndex = 1;
            this.btnNS.Text = "Not Secure";
            this.toolTip1.SetToolTip(this.btnNS, "All HTTP: urals");
            this.btnNS.UseVisualStyleBackColor = true;
            this.btnNS.Click += new System.EventHandler(this.btnNS_Click);
            // 
            // ShowDups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 585);
            this.Controls.Add(this.btnNS);
            this.Controls.Add(this.gbSelect);
            this.Controls.Add(this.btnImage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbGoTo);
            this.Controls.Add(this.btnGoTo);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.lbUrls);
            this.Controls.Add(this.tbNum);
            this.Controls.Add(this.gbDup);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowDups";
            this.Text = "ShowDups";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.ShowDups_HelpButtonClicked);
            this.gbDup.ResumeLayout(false);
            this.gbDup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox gbDup;
        private System.Windows.Forms.ListBox lbUrls;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.TextBox tbNum;
        private System.Windows.Forms.TextBox tbGoTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbMacroID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbSelect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnExitEdit;
        private System.Windows.Forms.Button btnNS;
        private System.Windows.Forms.Button btnImage;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}