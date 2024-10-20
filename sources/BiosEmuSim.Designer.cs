namespace MacroEditor.sources
{
    partial class BiosEmuSim
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClnTxt = new System.Windows.Forms.Button();
            this.btnPClean = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbSelText = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbDups = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbKey = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCLRatbaTEXT = new System.Windows.Forms.Button();
            this.btnCLRatbHREF = new System.Windows.Forms.Button();
            this.btnMakeURL = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.btnClrIL = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbError = new System.Windows.Forms.TextBox();
            this.tbText = new System.Windows.Forms.TextBox();
            this.lbInfoTest = new System.Windows.Forms.Label();
            this.btnPaste = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.tbHref = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnNotSim = new System.Windows.Forms.Button();
            this.btnMarkBad = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbDups = new System.Windows.Forms.RadioButton();
            this.rbMhttp = new System.Windows.Forms.RadioButton();
            this.rbMpdf = new System.Windows.Forms.RadioButton();
            this.cbForcePDF = new System.Windows.Forms.CheckBox();
            this.cbForceHTTPS = new System.Windows.Forms.CheckBox();
            this.cbUseFF = new System.Windows.Forms.CheckBox();
            this.btnCanChg = new System.Windows.Forms.Button();
            this.btnCLRtb = new System.Windows.Forms.Button();
            this.btnCLRhb = new System.Windows.Forms.Button();
            this.btnTestB = new System.Windows.Forms.Button();
            this.tbTextB = new System.Windows.Forms.TextBox();
            this.tbHrefB = new System.Windows.Forms.TextBox();
            this.btnUpdateURL = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.dgvBIOS = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBIOS)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnClnTxt);
            this.groupBox1.Controls.Add(this.btnPClean);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbSelText);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbDups);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.btnCLRatbaTEXT);
            this.groupBox1.Controls.Add(this.btnCLRatbHREF);
            this.groupBox1.Controls.Add(this.btnMakeURL);
            this.groupBox1.Controls.Add(this.btnTest);
            this.groupBox1.Controls.Add(this.tbURL);
            this.groupBox1.Controls.Add(this.btnClrIL);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbError);
            this.groupBox1.Controls.Add(this.tbText);
            this.groupBox1.Controls.Add(this.lbInfoTest);
            this.groupBox1.Controls.Add(this.btnPaste);
            this.groupBox1.Controls.Add(this.btnApply);
            this.groupBox1.Controls.Add(this.tbHref);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox1.Location = new System.Drawing.Point(13, 17);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(498, 776);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Paste From Clipboard";
            // 
            // btnClnTxt
            // 
            this.btnClnTxt.Location = new System.Drawing.Point(157, 144);
            this.btnClnTxt.Name = "btnClnTxt";
            this.btnClnTxt.Size = new System.Drawing.Size(137, 23);
            this.btnClnTxt.TabIndex = 28;
            this.btnClnTxt.Text = "Paste Clean Text";
            this.toolTip1.SetToolTip(this.btnClnTxt, "Newlines are removed");
            this.btnClnTxt.UseVisualStyleBackColor = true;
            this.btnClnTxt.Click += new System.EventHandler(this.btnClnTxt_Click);
            // 
            // btnPClean
            // 
            this.btnPClean.Location = new System.Drawing.Point(157, 21);
            this.btnPClean.Name = "btnPClean";
            this.btnPClean.Size = new System.Drawing.Size(137, 23);
            this.btnPClean.TabIndex = 27;
            this.btnPClean.Text = "Paste Clean URL";
            this.toolTip1.SetToolTip(this.btnPClean, "Trace, rebate, coupons stripped");
            this.btnPClean.UseVisualStyleBackColor = true;
            this.btnPClean.Click += new System.EventHandler(this.btnPClean_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 234);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 16);
            this.label4.TabIndex = 26;
            this.label4.Text = "Select Text";
            // 
            // cbSelText
            // 
            this.cbSelText.FormattingEnabled = true;
            this.cbSelText.Location = new System.Drawing.Point(117, 231);
            this.cbSelText.Name = "cbSelText";
            this.cbSelText.Size = new System.Drawing.Size(220, 24);
            this.cbSelText.TabIndex = 25;
            this.cbSelText.SelectedIndexChanged += new System.EventHandler(this.cbSelText_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Info;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(84, 734);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 20);
            this.label3.TabIndex = 24;
            this.label3.Text = "Duplicates";
            // 
            // cbDups
            // 
            this.cbDups.FormattingEnabled = true;
            this.cbDups.Location = new System.Drawing.Point(191, 734);
            this.cbDups.Name = "cbDups";
            this.cbDups.Size = new System.Drawing.Size(127, 24);
            this.cbDups.TabIndex = 23;
            this.cbDups.SelectedIndexChanged += new System.EventHandler(this.cbDups_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.tbKey);
            this.groupBox3.Controls.Add(this.btnSearch);
            this.groupBox3.Location = new System.Drawing.Point(15, 613);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(427, 99);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search for systems";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Info;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(154, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 20);
            this.label2.TabIndex = 22;
            this.label2.Text = "Keywords:  800 G4 etc";
            // 
            // tbKey
            // 
            this.tbKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbKey.Location = new System.Drawing.Point(142, 56);
            this.tbKey.Margin = new System.Windows.Forms.Padding(4);
            this.tbKey.Name = "tbKey";
            this.tbKey.Size = new System.Drawing.Size(238, 26);
            this.tbKey.TabIndex = 21;
            this.tbKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbKey_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnSearch.Location = new System.Drawing.Point(26, 39);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(77, 32);
            this.btnSearch.TabIndex = 20;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnCLRatbaTEXT
            // 
            this.btnCLRatbaTEXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCLRatbaTEXT.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCLRatbaTEXT.Location = new System.Drawing.Point(16, 142);
            this.btnCLRatbaTEXT.Margin = new System.Windows.Forms.Padding(4);
            this.btnCLRatbaTEXT.Name = "btnCLRatbaTEXT";
            this.btnCLRatbaTEXT.Size = new System.Drawing.Size(77, 26);
            this.btnCLRatbaTEXT.TabIndex = 22;
            this.btnCLRatbaTEXT.Text = "CLEAR";
            this.btnCLRatbaTEXT.UseVisualStyleBackColor = true;
            this.btnCLRatbaTEXT.Click += new System.EventHandler(this.btnCLRatbaTEXT_Click);
            // 
            // btnCLRatbHREF
            // 
            this.btnCLRatbHREF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCLRatbHREF.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCLRatbHREF.Location = new System.Drawing.Point(20, 21);
            this.btnCLRatbHREF.Margin = new System.Windows.Forms.Padding(4);
            this.btnCLRatbHREF.Name = "btnCLRatbHREF";
            this.btnCLRatbHREF.Size = new System.Drawing.Size(77, 26);
            this.btnCLRatbHREF.TabIndex = 21;
            this.btnCLRatbHREF.Text = "CLEAR";
            this.btnCLRatbHREF.UseVisualStyleBackColor = true;
            this.btnCLRatbHREF.Click += new System.EventHandler(this.btnCLRatbHREF_Click);
            // 
            // btnMakeURL
            // 
            this.btnMakeURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMakeURL.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnMakeURL.Location = new System.Drawing.Point(10, 363);
            this.btnMakeURL.Margin = new System.Windows.Forms.Padding(4);
            this.btnMakeURL.Name = "btnMakeURL";
            this.btnMakeURL.Size = new System.Drawing.Size(95, 32);
            this.btnMakeURL.TabIndex = 20;
            this.btnMakeURL.Text = "Make URL";
            this.btnMakeURL.UseVisualStyleBackColor = true;
            this.btnMakeURL.Click += new System.EventHandler(this.btnMakeURL_Click);
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTest.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnTest.Location = new System.Drawing.Point(15, 555);
            this.btnTest.Margin = new System.Windows.Forms.Padding(4);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(77, 32);
            this.btnTest.TabIndex = 19;
            this.btnTest.Text = "TEST";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // tbURL
            // 
            this.tbURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbURL.Location = new System.Drawing.Point(142, 332);
            this.tbURL.Margin = new System.Windows.Forms.Padding(4);
            this.tbURL.Multiline = true;
            this.tbURL.Name = "tbURL";
            this.tbURL.ReadOnly = true;
            this.tbURL.Size = new System.Drawing.Size(326, 88);
            this.tbURL.TabIndex = 17;
            // 
            // btnClrIL
            // 
            this.btnClrIL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClrIL.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnClrIL.Location = new System.Drawing.Point(117, 555);
            this.btnClrIL.Margin = new System.Windows.Forms.Padding(4);
            this.btnClrIL.Name = "btnClrIL";
            this.btnClrIL.Size = new System.Drawing.Size(77, 32);
            this.btnClrIL.TabIndex = 11;
            this.btnClrIL.Text = "CLEAR";
            this.btnClrIL.UseVisualStyleBackColor = true;
            this.btnClrIL.Click += new System.EventHandler(this.btnClrIL_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(422, 150);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "TEXT";
            // 
            // tbError
            // 
            this.tbError.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbError.Location = new System.Drawing.Point(240, 448);
            this.tbError.Margin = new System.Windows.Forms.Padding(4);
            this.tbError.Multiline = true;
            this.tbError.Name = "tbError";
            this.tbError.ReadOnly = true;
            this.tbError.Size = new System.Drawing.Size(202, 139);
            this.tbError.TabIndex = 15;
            // 
            // tbText
            // 
            this.tbText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbText.Location = new System.Drawing.Point(15, 176);
            this.tbText.Margin = new System.Windows.Forms.Padding(4);
            this.tbText.Multiline = true;
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(456, 48);
            this.tbText.TabIndex = 13;
            // 
            // lbInfoTest
            // 
            this.lbInfoTest.AutoSize = true;
            this.lbInfoTest.BackColor = System.Drawing.SystemColors.Info;
            this.lbInfoTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInfoTest.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbInfoTest.Location = new System.Drawing.Point(422, 26);
            this.lbInfoTest.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbInfoTest.Name = "lbInfoTest";
            this.lbInfoTest.Size = new System.Drawing.Size(54, 20);
            this.lbInfoTest.TabIndex = 12;
            this.lbInfoTest.Text = "HREF";
            // 
            // btnPaste
            // 
            this.btnPaste.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPaste.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnPaste.Location = new System.Drawing.Point(11, 425);
            this.btnPaste.Margin = new System.Windows.Forms.Padding(4);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(103, 32);
            this.btnPaste.TabIndex = 5;
            this.btnPaste.Text = "Paste URL";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnApply.Location = new System.Drawing.Point(15, 474);
            this.btnApply.Margin = new System.Windows.Forms.Padding(4);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(110, 52);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Add to table";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // tbHref
            // 
            this.tbHref.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHref.Location = new System.Drawing.Point(16, 52);
            this.tbHref.Margin = new System.Windows.Forms.Padding(4);
            this.tbHref.Multiline = true;
            this.tbHref.Name = "tbHref";
            this.tbHref.Size = new System.Drawing.Size(456, 69);
            this.tbHref.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnNotSim);
            this.groupBox2.Controls.Add(this.btnMarkBad);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.cbForcePDF);
            this.groupBox2.Controls.Add(this.cbForceHTTPS);
            this.groupBox2.Controls.Add(this.cbUseFF);
            this.groupBox2.Controls.Add(this.btnCanChg);
            this.groupBox2.Controls.Add(this.btnCLRtb);
            this.groupBox2.Controls.Add(this.btnCLRhb);
            this.groupBox2.Controls.Add(this.btnTestB);
            this.groupBox2.Controls.Add(this.tbTextB);
            this.groupBox2.Controls.Add(this.tbHrefB);
            this.groupBox2.Controls.Add(this.btnUpdateURL);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.btnExport);
            this.groupBox2.Controls.Add(this.dgvBIOS);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox2.Location = new System.Drawing.Point(535, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(797, 775);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Double click to view";
            // 
            // btnNotSim
            // 
            this.btnNotSim.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNotSim.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnNotSim.Location = new System.Drawing.Point(635, 97);
            this.btnNotSim.Margin = new System.Windows.Forms.Padding(4);
            this.btnNotSim.Name = "btnNotSim";
            this.btnNotSim.Size = new System.Drawing.Size(127, 26);
            this.btnNotSim.TabIndex = 32;
            this.btnNotSim.Text = "Not Interactive";
            this.toolTip1.SetToolTip(this.btnNotSim, "PDF is not interactive\r\nmark as BIOS and save");
            this.btnNotSim.UseVisualStyleBackColor = true;
            this.btnNotSim.Click += new System.EventHandler(this.btnNotSim_Click);
            // 
            // btnMarkBad
            // 
            this.btnMarkBad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMarkBad.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnMarkBad.Location = new System.Drawing.Point(635, 57);
            this.btnMarkBad.Margin = new System.Windows.Forms.Padding(4);
            this.btnMarkBad.Name = "btnMarkBad";
            this.btnMarkBad.Size = new System.Drawing.Size(88, 26);
            this.btnMarkBad.TabIndex = 31;
            this.btnMarkBad.Text = "Mark Bad";
            this.toolTip1.SetToolTip(this.btnMarkBad, "PDF is missing pages");
            this.btnMarkBad.UseVisualStyleBackColor = true;
            this.btnMarkBad.Click += new System.EventHandler(this.btnMarkBad_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbDups);
            this.groupBox4.Controls.Add(this.rbMhttp);
            this.groupBox4.Controls.Add(this.rbMpdf);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(541, 481);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(208, 122);
            this.groupBox4.TabIndex = 30;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Color Red for missing";
            // 
            // rbDups
            // 
            this.rbDups.AutoSize = true;
            this.rbDups.Checked = true;
            this.rbDups.Location = new System.Drawing.Point(15, 90);
            this.rbDups.Name = "rbDups";
            this.rbDups.Size = new System.Drawing.Size(89, 20);
            this.rbDups.TabIndex = 2;
            this.rbDups.TabStop = true;
            this.rbDups.Text = "Duplicates";
            this.rbDups.UseVisualStyleBackColor = true;
            this.rbDups.CheckedChanged += new System.EventHandler(this.rbDups_CheckedChanged);
            // 
            // rbMhttp
            // 
            this.rbMhttp.AutoSize = true;
            this.rbMhttp.Location = new System.Drawing.Point(15, 31);
            this.rbMhttp.Name = "rbMhttp";
            this.rbMhttp.Size = new System.Drawing.Size(116, 20);
            this.rbMhttp.TabIndex = 1;
            this.rbMhttp.Text = "HTTP insecure";
            this.rbMhttp.UseVisualStyleBackColor = true;
            this.rbMhttp.CheckedChanged += new System.EventHandler(this.rbMhttp_CheckedChanged);
            // 
            // rbMpdf
            // 
            this.rbMpdf.AutoSize = true;
            this.rbMpdf.Location = new System.Drawing.Point(15, 60);
            this.rbMpdf.Name = "rbMpdf";
            this.rbMpdf.Size = new System.Drawing.Size(112, 20);
            this.rbMpdf.TabIndex = 0;
            this.rbMpdf.Text = "PDF extension";
            this.rbMpdf.UseVisualStyleBackColor = true;
            this.rbMpdf.CheckedChanged += new System.EventHandler(this.rbMpdf_CheckedChanged);
            // 
            // cbForcePDF
            // 
            this.cbForcePDF.AutoSize = true;
            this.cbForcePDF.Checked = true;
            this.cbForcePDF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbForcePDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbForcePDF.Location = new System.Drawing.Point(552, 655);
            this.cbForcePDF.Name = "cbForcePDF";
            this.cbForcePDF.Size = new System.Drawing.Size(91, 20);
            this.cbForcePDF.TabIndex = 29;
            this.cbForcePDF.Text = "force  pdf";
            this.toolTip1.SetToolTip(this.cbForcePDF, "If checked then any interactive bios\r\nmissing the externsion .pdf will be\r\nforced" +
        " to have .pdf.  Not used for\r\ngeneral documents");
            this.cbForcePDF.UseVisualStyleBackColor = true;
            // 
            // cbForceHTTPS
            // 
            this.cbForceHTTPS.AutoSize = true;
            this.cbForceHTTPS.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbForceHTTPS.Location = new System.Drawing.Point(552, 681);
            this.cbForceHTTPS.Name = "cbForceHTTPS";
            this.cbForceHTTPS.Size = new System.Drawing.Size(98, 20);
            this.cbForceHTTPS.TabIndex = 28;
            this.cbForceHTTPS.Text = "force https";
            this.toolTip1.SetToolTip(this.cbForceHTTPS, "If checked then any document\r\nhaving HTTP insetead of HTTPS\r\nwill be  forced to h" +
        "ave HTTPS");
            this.cbForceHTTPS.UseVisualStyleBackColor = true;
            // 
            // cbUseFF
            // 
            this.cbUseFF.AutoSize = true;
            this.cbUseFF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUseFF.Location = new System.Drawing.Point(552, 624);
            this.cbUseFF.Name = "cbUseFF";
            this.cbUseFF.Size = new System.Drawing.Size(110, 20);
            this.cbUseFF.TabIndex = 27;
            this.cbUseFF.Text = "Use FireFox";
            this.cbUseFF.UseVisualStyleBackColor = true;
            // 
            // btnCanChg
            // 
            this.btnCanChg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCanChg.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCanChg.Location = new System.Drawing.Point(541, 428);
            this.btnCanChg.Margin = new System.Windows.Forms.Padding(4);
            this.btnCanChg.Name = "btnCanChg";
            this.btnCanChg.Size = new System.Drawing.Size(149, 32);
            this.btnCanChg.TabIndex = 25;
            this.btnCanChg.Text = "Cancel Changes";
            this.btnCanChg.UseVisualStyleBackColor = true;
            this.btnCanChg.Click += new System.EventHandler(this.btnCanChg_Click);
            // 
            // btnCLRtb
            // 
            this.btnCLRtb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCLRtb.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCLRtb.Location = new System.Drawing.Point(34, 140);
            this.btnCLRtb.Margin = new System.Windows.Forms.Padding(4);
            this.btnCLRtb.Name = "btnCLRtb";
            this.btnCLRtb.Size = new System.Drawing.Size(77, 26);
            this.btnCLRtb.TabIndex = 24;
            this.btnCLRtb.Text = "CLEAR";
            this.btnCLRtb.UseVisualStyleBackColor = true;
            this.btnCLRtb.Click += new System.EventHandler(this.btnCLRtb_Click);
            // 
            // btnCLRhb
            // 
            this.btnCLRhb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCLRhb.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCLRhb.Location = new System.Drawing.Point(34, 23);
            this.btnCLRhb.Margin = new System.Windows.Forms.Padding(4);
            this.btnCLRhb.Name = "btnCLRhb";
            this.btnCLRhb.Size = new System.Drawing.Size(77, 26);
            this.btnCLRhb.TabIndex = 23;
            this.btnCLRhb.Text = "CLEAR";
            this.btnCLRhb.UseVisualStyleBackColor = true;
            this.btnCLRhb.Click += new System.EventHandler(this.btnCLRhb_Click);
            // 
            // btnTestB
            // 
            this.btnTestB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestB.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnTestB.Location = new System.Drawing.Point(576, 252);
            this.btnTestB.Margin = new System.Windows.Forms.Padding(4);
            this.btnTestB.Name = "btnTestB";
            this.btnTestB.Size = new System.Drawing.Size(114, 32);
            this.btnTestB.TabIndex = 20;
            this.btnTestB.Text = "Test Change";
            this.btnTestB.UseVisualStyleBackColor = true;
            this.btnTestB.Click += new System.EventHandler(this.btnTestB_Click);
            // 
            // tbTextB
            // 
            this.tbTextB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTextB.Location = new System.Drawing.Point(34, 174);
            this.tbTextB.Margin = new System.Windows.Forms.Padding(4);
            this.tbTextB.Multiline = true;
            this.tbTextB.Name = "tbTextB";
            this.tbTextB.Size = new System.Drawing.Size(543, 45);
            this.tbTextB.TabIndex = 15;
            // 
            // tbHrefB
            // 
            this.tbHrefB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHrefB.Location = new System.Drawing.Point(34, 57);
            this.tbHrefB.Margin = new System.Windows.Forms.Padding(4);
            this.tbHrefB.Multiline = true;
            this.tbHrefB.Name = "tbHrefB";
            this.tbHrefB.Size = new System.Drawing.Size(543, 66);
            this.tbHrefB.TabIndex = 14;
            // 
            // btnUpdateURL
            // 
            this.btnUpdateURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateURL.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnUpdateURL.Location = new System.Drawing.Point(576, 301);
            this.btnUpdateURL.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdateURL.Name = "btnUpdateURL";
            this.btnUpdateURL.Size = new System.Drawing.Size(114, 32);
            this.btnUpdateURL.TabIndex = 8;
            this.btnUpdateURL.Text = "Replace";
            this.btnUpdateURL.UseVisualStyleBackColor = true;
            this.btnUpdateURL.Click += new System.EventHandler(this.btnUpdateURL_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnSave.Location = new System.Drawing.Point(541, 376);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(149, 32);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "SAVE Changes";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnExport.Location = new System.Drawing.Point(587, 725);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(105, 32);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "Export";
            this.toolTip1.SetToolTip(this.btnExport, "Be sure to save changes, exit the\r\nprogram and restart before exporting.");
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dgvBIOS
            // 
            this.dgvBIOS.AllowUserToAddRows = false;
            this.dgvBIOS.AllowUserToResizeColumns = false;
            this.dgvBIOS.AllowUserToResizeRows = false;
            this.dgvBIOS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBIOS.Location = new System.Drawing.Point(24, 252);
            this.dgvBIOS.Name = "dgvBIOS";
            this.dgvBIOS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvBIOS.Size = new System.Drawing.Size(497, 503);
            this.dgvBIOS.TabIndex = 0;
            this.toolTip1.SetToolTip(this.dgvBIOS, "Red colored index are URLs\r\nthat use http instead of https\r\nor are missing the ex" +
        "tension\r\n.pdf from the document");
            this.dgvBIOS.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvBIOS_CellFormatting);
            this.dgvBIOS.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvBIOS_ColumnHeaderMouseClick);
            this.dgvBIOS.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBIOS_RowEnter);
            this.dgvBIOS.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvBIOS_UserDeletedRow);
            this.dgvBIOS.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvBIOS_MouseDoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Info;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(17, 278);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(287, 32);
            this.label5.TabIndex = 29;
            this.label5.Text = "Be sure to add the name of the notebook to the\r\nright of \"Interactive BIOS simula" +
    "tor\" in TEXT box";
            // 
            // BiosEmuSim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 806);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BiosEmuSim";
            this.Text = "BiosEmuSim";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BiosEmuSim_FormClosing);
            this.Shown += new System.EventHandler(this.BiosEmuSim_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBIOS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TextBox tbHref;
        private System.Windows.Forms.Button btnClrIL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbText;
        private System.Windows.Forms.Label lbInfoTest;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvBIOS;
        private System.Windows.Forms.TextBox tbError;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnMakeURL;
        private System.Windows.Forms.Button btnUpdateURL;
        private System.Windows.Forms.TextBox tbTextB;
        private System.Windows.Forms.TextBox tbHrefB;
        private System.Windows.Forms.Button btnTestB;
        private System.Windows.Forms.Button btnCLRatbaTEXT;
        private System.Windows.Forms.Button btnCLRatbHREF;
        private System.Windows.Forms.Button btnCLRtb;
        private System.Windows.Forms.Button btnCLRhb;
        private System.Windows.Forms.Button btnCanChg;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbKey;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox cbUseFF;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox cbForcePDF;
        private System.Windows.Forms.CheckBox cbForceHTTPS;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbMpdf;
        private System.Windows.Forms.RadioButton rbMhttp;
        private System.Windows.Forms.Button btnMarkBad;
        private System.Windows.Forms.RadioButton rbDups;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbDups;
        private System.Windows.Forms.Button btnNotSim;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbSelText;
        private System.Windows.Forms.Button btnPClean;
        private System.Windows.Forms.Button btnClnTxt;
        private System.Windows.Forms.Label label5;
    }
}