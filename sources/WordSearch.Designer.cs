namespace MacroEditor
{
    partial class WordSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WordSearch));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbTitleSearch = new System.Windows.Forms.ListBox();
            this.btnExitTitle = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbvAddLangRef = new System.Windows.Forms.CheckBox();
            this.btnShowCC = new System.Windows.Forms.Button();
            this.gbSelect = new System.Windows.Forms.GroupBox();
            this.gbMakeNew = new System.Windows.Forms.GroupBox();
            this.lbNewItems = new System.Windows.Forms.ListBox();
            this.btnMakeNew = new System.Windows.Forms.Button();
            this.gbAlltSearch = new System.Windows.Forms.GroupBox();
            this.btnHpYTsup = new System.Windows.Forms.Button();
            this.btnKbSearch = new System.Windows.Forms.Button();
            this.btnGoogle = new System.Windows.Forms.Button();
            this.btnMan = new System.Windows.Forms.Button();
            this.btnDrv = new System.Windows.Forms.Button();
            this.btnEbay = new System.Windows.Forms.Button();
            this.btnPrn = new System.Windows.Forms.Button();
            this.btnPC = new System.Windows.Forms.Button();
            this.cbOfferAlt = new System.Windows.Forms.CheckBox();
            this.cbHPKB = new System.Windows.Forms.CheckBox();
            this.gbParam = new System.Windows.Forms.GroupBox();
            this.gbRB = new System.Windows.Forms.GroupBox();
            this.rbEPhrase = new System.Windows.Forms.RadioButton();
            this.rbAnyMatch = new System.Windows.Forms.RadioButton();
            this.rbExactMatch = new System.Windows.Forms.RadioButton();
            this.btnExitToMac = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.cbIgnCase = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.gbFound = new System.Windows.Forms.GroupBox();
            this.tbMissing = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSelKey = new System.Windows.Forms.ComboBox();
            this.lbKeyFound = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvSearched = new System.Windows.Forms.DataGridView();
            this.tbKeywords = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbMakeNew.SuspendLayout();
            this.gbAlltSearch.SuspendLayout();
            this.gbParam.SuspendLayout();
            this.gbRB.SuspendLayout();
            this.gbFound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearched)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.cbvAddLangRef);
            this.groupBox1.Controls.Add(this.btnShowCC);
            this.groupBox1.Controls.Add(this.gbSelect);
            this.groupBox1.Controls.Add(this.gbMakeNew);
            this.groupBox1.Controls.Add(this.gbAlltSearch);
            this.groupBox1.Controls.Add(this.cbOfferAlt);
            this.groupBox1.Controls.Add(this.cbHPKB);
            this.groupBox1.Controls.Add(this.gbParam);
            this.groupBox1.Controls.Add(this.gbFound);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dgvSearched);
            this.groupBox1.Controls.Add(this.tbKeywords);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1308, 781);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Keyword match";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbTitleSearch);
            this.groupBox2.Controls.Add(this.btnExitTitle);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(1011, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(278, 558);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Title Search";
            // 
            // lbTitleSearch
            // 
            this.lbTitleSearch.FormattingEnabled = true;
            this.lbTitleSearch.ItemHeight = 20;
            this.lbTitleSearch.Location = new System.Drawing.Point(18, 230);
            this.lbTitleSearch.Name = "lbTitleSearch";
            this.lbTitleSearch.Size = new System.Drawing.Size(254, 304);
            this.lbTitleSearch.TabIndex = 11;
            this.lbTitleSearch.SelectedIndexChanged += new System.EventHandler(this.lbTitleSearch_SelectedIndexChanged);
            this.lbTitleSearch.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbTitleSearch_MouseDoubleClick);
            // 
            // btnExitTitle
            // 
            this.btnExitTitle.Enabled = false;
            this.btnExitTitle.ForeColor = System.Drawing.Color.Red;
            this.btnExitTitle.Location = new System.Drawing.Point(32, 167);
            this.btnExitTitle.Name = "btnExitTitle";
            this.btnExitTitle.Size = new System.Drawing.Size(192, 31);
            this.btnExitTitle.TabIndex = 10;
            this.btnExitTitle.Text = "EXIT and show macro";
            this.toolTip1.SetToolTip(this.btnExitTitle, "Bring up the selected macro after the form closes\r\nThis does not work if you have" +
        " unfinised edits");
            this.btnExitTitle.UseVisualStyleBackColor = true;
            this.btnExitTitle.Click += new System.EventHandler(this.btnExitTitle_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Info;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(209, 80);
            this.label4.TabIndex = 5;
            this.label4.Text = " Most likely search based\r\nonly on the title or name\r\nof the macro.  Case and oth" +
    "er\r\nkeyboard parameters are ignored\r\nDouble click to view macro";
            this.toolTip1.SetToolTip(this.label4, "Rows with  RF will list only those\r\nreferences that contain the match\r\nAll other " +
        "rows display the entire macro.");
            // 
            // cbvAddLangRef
            // 
            this.cbvAddLangRef.AutoSize = true;
            this.cbvAddLangRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbvAddLangRef.ForeColor = System.Drawing.Color.Green;
            this.cbvAddLangRef.Location = new System.Drawing.Point(344, 313);
            this.cbvAddLangRef.Name = "cbvAddLangRef";
            this.cbvAddLangRef.Size = new System.Drawing.Size(155, 20);
            this.cbvAddLangRef.TabIndex = 15;
            this.cbvAddLangRef.Text = "Add Language Ref";
            this.cbvAddLangRef.UseVisualStyleBackColor = true;
            this.cbvAddLangRef.Visible = false;
            // 
            // btnShowCC
            // 
            this.btnShowCC.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnShowCC.Location = new System.Drawing.Point(213, 196);
            this.btnShowCC.Name = "btnShowCC";
            this.btnShowCC.Size = new System.Drawing.Size(114, 30);
            this.btnShowCC.TabIndex = 14;
            this.btnShowCC.Text = "Show Codes";
            this.btnShowCC.UseVisualStyleBackColor = true;
            this.btnShowCC.Visible = false;
            this.btnShowCC.Click += new System.EventHandler(this.btnShowCC_Click);
            // 
            // gbSelect
            // 
            this.gbSelect.Location = new System.Drawing.Point(28, 703);
            this.gbSelect.Name = "gbSelect";
            this.gbSelect.Size = new System.Drawing.Size(951, 57);
            this.gbSelect.TabIndex = 12;
            this.gbSelect.TabStop = false;
            this.gbSelect.Visible = false;
            // 
            // gbMakeNew
            // 
            this.gbMakeNew.Controls.Add(this.lbNewItems);
            this.gbMakeNew.Controls.Add(this.btnMakeNew);
            this.gbMakeNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbMakeNew.ForeColor = System.Drawing.SystemColors.Highlight;
            this.gbMakeNew.Location = new System.Drawing.Point(434, 25);
            this.gbMakeNew.Name = "gbMakeNew";
            this.gbMakeNew.Size = new System.Drawing.Size(155, 194);
            this.gbMakeNew.TabIndex = 11;
            this.gbMakeNew.TabStop = false;
            this.gbMakeNew.Text = "Create new macro";
            this.gbMakeNew.Visible = false;
            // 
            // lbNewItems
            // 
            this.lbNewItems.FormattingEnabled = true;
            this.lbNewItems.ItemHeight = 16;
            this.lbNewItems.Location = new System.Drawing.Point(44, 59);
            this.lbNewItems.Name = "lbNewItems";
            this.lbNewItems.Size = new System.Drawing.Size(75, 100);
            this.lbNewItems.TabIndex = 8;
            // 
            // btnMakeNew
            // 
            this.btnMakeNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMakeNew.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnMakeNew.Location = new System.Drawing.Point(17, 25);
            this.btnMakeNew.Name = "btnMakeNew";
            this.btnMakeNew.Size = new System.Drawing.Size(90, 23);
            this.btnMakeNew.TabIndex = 7;
            this.btnMakeNew.Text = "GO make it";
            this.toolTip1.SetToolTip(this.btnMakeNew, "HP Knowledge base search");
            this.btnMakeNew.UseVisualStyleBackColor = true;
            this.btnMakeNew.Click += new System.EventHandler(this.btnMakeNew_Click);
            // 
            // gbAlltSearch
            // 
            this.gbAlltSearch.Controls.Add(this.btnHpYTsup);
            this.gbAlltSearch.Controls.Add(this.btnKbSearch);
            this.gbAlltSearch.Controls.Add(this.btnGoogle);
            this.gbAlltSearch.Controls.Add(this.btnMan);
            this.gbAlltSearch.Controls.Add(this.btnDrv);
            this.gbAlltSearch.Controls.Add(this.btnEbay);
            this.gbAlltSearch.Controls.Add(this.btnPrn);
            this.gbAlltSearch.Controls.Add(this.btnPC);
            this.gbAlltSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAlltSearch.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.gbAlltSearch.Location = new System.Drawing.Point(253, 25);
            this.gbAlltSearch.Name = "gbAlltSearch";
            this.gbAlltSearch.Size = new System.Drawing.Size(175, 194);
            this.gbAlltSearch.TabIndex = 10;
            this.gbAlltSearch.TabStop = false;
            this.gbAlltSearch.Text = "Search (HP)";
            this.gbAlltSearch.Visible = false;
            // 
            // btnHpYTsup
            // 
            this.btnHpYTsup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHpYTsup.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnHpYTsup.Location = new System.Drawing.Point(91, 152);
            this.btnHpYTsup.Name = "btnHpYTsup";
            this.btnHpYTsup.Size = new System.Drawing.Size(74, 23);
            this.btnHpYTsup.TabIndex = 7;
            this.btnHpYTsup.Text = "YouTube";
            this.toolTip1.SetToolTip(this.btnHpYTsup, "search hp youtube support");
            this.btnHpYTsup.UseVisualStyleBackColor = true;
            this.btnHpYTsup.Click += new System.EventHandler(this.btnHpYTsup_Click);
            // 
            // btnKbSearch
            // 
            this.btnKbSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKbSearch.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnKbSearch.Location = new System.Drawing.Point(91, 120);
            this.btnKbSearch.Name = "btnKbSearch";
            this.btnKbSearch.Size = new System.Drawing.Size(74, 23);
            this.btnKbSearch.TabIndex = 6;
            this.btnKbSearch.Text = "HP KB";
            this.toolTip1.SetToolTip(this.btnKbSearch, "HP Knowledge base search");
            this.btnKbSearch.UseVisualStyleBackColor = true;
            this.btnKbSearch.Click += new System.EventHandler(this.btnKbSearch_Click);
            // 
            // btnGoogle
            // 
            this.btnGoogle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoogle.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnGoogle.Location = new System.Drawing.Point(91, 88);
            this.btnGoogle.Name = "btnGoogle";
            this.btnGoogle.Size = new System.Drawing.Size(74, 23);
            this.btnGoogle.TabIndex = 5;
            this.btnGoogle.Text = "Google";
            this.btnGoogle.UseVisualStyleBackColor = true;
            this.btnGoogle.Click += new System.EventHandler(this.btnGoogle_Click);
            // 
            // btnMan
            // 
            this.btnMan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMan.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnMan.Location = new System.Drawing.Point(91, 57);
            this.btnMan.Name = "btnMan";
            this.btnMan.Size = new System.Drawing.Size(74, 23);
            this.btnMan.TabIndex = 4;
            this.btnMan.Text = "Manuals";
            this.btnMan.UseVisualStyleBackColor = true;
            this.btnMan.Click += new System.EventHandler(this.btnMan_Click);
            // 
            // btnDrv
            // 
            this.btnDrv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDrv.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnDrv.Location = new System.Drawing.Point(91, 25);
            this.btnDrv.Name = "btnDrv";
            this.btnDrv.Size = new System.Drawing.Size(74, 23);
            this.btnDrv.TabIndex = 3;
            this.btnDrv.Text = "Drivers";
            this.btnDrv.UseVisualStyleBackColor = true;
            this.btnDrv.Click += new System.EventHandler(this.btnDrv_Click);
            // 
            // btnEbay
            // 
            this.btnEbay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEbay.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnEbay.Location = new System.Drawing.Point(21, 88);
            this.btnEbay.Name = "btnEbay";
            this.btnEbay.Size = new System.Drawing.Size(53, 23);
            this.btnEbay.TabIndex = 2;
            this.btnEbay.Text = "EBay";
            this.btnEbay.UseVisualStyleBackColor = true;
            this.btnEbay.Click += new System.EventHandler(this.btnEbay_Click);
            // 
            // btnPrn
            // 
            this.btnPrn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrn.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnPrn.Location = new System.Drawing.Point(21, 57);
            this.btnPrn.Name = "btnPrn";
            this.btnPrn.Size = new System.Drawing.Size(53, 23);
            this.btnPrn.TabIndex = 1;
            this.btnPrn.Text = "Printer";
            this.btnPrn.UseVisualStyleBackColor = true;
            this.btnPrn.Click += new System.EventHandler(this.btnPrn_Click);
            // 
            // btnPC
            // 
            this.btnPC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPC.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnPC.Location = new System.Drawing.Point(21, 25);
            this.btnPC.Name = "btnPC";
            this.btnPC.Size = new System.Drawing.Size(53, 23);
            this.btnPC.TabIndex = 0;
            this.btnPC.Text = "PC";
            this.btnPC.UseVisualStyleBackColor = true;
            this.btnPC.Click += new System.EventHandler(this.btnPC_Click);
            // 
            // cbOfferAlt
            // 
            this.cbOfferAlt.AutoSize = true;
            this.cbOfferAlt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOfferAlt.ForeColor = System.Drawing.SystemColors.Highlight;
            this.cbOfferAlt.Location = new System.Drawing.Point(28, 84);
            this.cbOfferAlt.Name = "cbOfferAlt";
            this.cbOfferAlt.Size = new System.Drawing.Size(218, 24);
            this.cbOfferAlt.TabIndex = 9;
            this.cbOfferAlt.Text = "Offer alternatives on failure";
            this.toolTip1.SetToolTip(this.cbOfferAlt, "If enabled and the search\r\nturns up empty then one\r\ncan use google for printer\r\nP" +
        "C or a general search.");
            this.cbOfferAlt.UseVisualStyleBackColor = true;
            this.cbOfferAlt.CheckStateChanged += new System.EventHandler(this.cbOfferAlt_CheckStateChanged);
            // 
            // cbHPKB
            // 
            this.cbHPKB.AutoSize = true;
            this.cbHPKB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbHPKB.ForeColor = System.Drawing.SystemColors.Highlight;
            this.cbHPKB.Location = new System.Drawing.Point(28, 40);
            this.cbHPKB.Name = "cbHPKB";
            this.cbHPKB.Size = new System.Drawing.Size(199, 24);
            this.cbHPKB.TabIndex = 8;
            this.cbHPKB.Text = "Include HP KB in search";
            this.cbHPKB.UseVisualStyleBackColor = true;
            // 
            // gbParam
            // 
            this.gbParam.Controls.Add(this.gbRB);
            this.gbParam.Controls.Add(this.btnExitToMac);
            this.gbParam.Controls.Add(this.btnExit);
            this.gbParam.Controls.Add(this.cbIgnCase);
            this.gbParam.Controls.Add(this.btnSearch);
            this.gbParam.Location = new System.Drawing.Point(621, 25);
            this.gbParam.Name = "gbParam";
            this.gbParam.Size = new System.Drawing.Size(358, 281);
            this.gbParam.TabIndex = 7;
            this.gbParam.TabStop = false;
            this.gbParam.Text = "Keyword parameters";
            // 
            // gbRB
            // 
            this.gbRB.Controls.Add(this.rbEPhrase);
            this.gbRB.Controls.Add(this.rbAnyMatch);
            this.gbRB.Controls.Add(this.rbExactMatch);
            this.gbRB.ForeColor = System.Drawing.SystemColors.Control;
            this.gbRB.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.gbRB.Location = new System.Drawing.Point(21, 30);
            this.gbRB.Name = "gbRB";
            this.gbRB.Size = new System.Drawing.Size(298, 124);
            this.gbRB.TabIndex = 11;
            this.gbRB.TabStop = false;
            // 
            // rbEPhrase
            // 
            this.rbEPhrase.AutoSize = true;
            this.rbEPhrase.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbEPhrase.Location = new System.Drawing.Point(22, 53);
            this.rbEPhrase.Name = "rbEPhrase";
            this.rbEPhrase.Size = new System.Drawing.Size(232, 24);
            this.rbEPhrase.TabIndex = 10;
            this.rbEPhrase.Text = "Exact Phrase (or use quotes)";
            this.rbEPhrase.UseVisualStyleBackColor = true;
            // 
            // rbAnyMatch
            // 
            this.rbAnyMatch.AutoSize = true;
            this.rbAnyMatch.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbAnyMatch.Location = new System.Drawing.Point(22, 85);
            this.rbAnyMatch.Name = "rbAnyMatch";
            this.rbAnyMatch.Size = new System.Drawing.Size(149, 24);
            this.rbAnyMatch.TabIndex = 1;
            this.rbAnyMatch.Text = "Any partial match";
            this.rbAnyMatch.UseVisualStyleBackColor = true;
            // 
            // rbExactMatch
            // 
            this.rbExactMatch.AutoSize = true;
            this.rbExactMatch.Checked = true;
            this.rbExactMatch.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbExactMatch.Location = new System.Drawing.Point(22, 20);
            this.rbExactMatch.Name = "rbExactMatch";
            this.rbExactMatch.Size = new System.Drawing.Size(134, 24);
            this.rbExactMatch.TabIndex = 0;
            this.rbExactMatch.TabStop = true;
            this.rbExactMatch.Text = "Any exact word";
            this.rbExactMatch.UseVisualStyleBackColor = true;
            // 
            // btnExitToMac
            // 
            this.btnExitToMac.Enabled = false;
            this.btnExitToMac.ForeColor = System.Drawing.Color.Red;
            this.btnExitToMac.Location = new System.Drawing.Point(21, 230);
            this.btnExitToMac.Name = "btnExitToMac";
            this.btnExitToMac.Size = new System.Drawing.Size(192, 31);
            this.btnExitToMac.TabIndex = 9;
            this.btnExitToMac.Text = "EXIT and show macro";
            this.toolTip1.SetToolTip(this.btnExitToMac, "Bring up the selected macro after the form closes\r\nThis does not work if you have" +
        " unfinised edits");
            this.btnExitToMac.UseVisualStyleBackColor = true;
            this.btnExitToMac.Click += new System.EventHandler(this.btnExitToMac_Click);
            // 
            // btnExit
            // 
            this.btnExit.ForeColor = System.Drawing.Color.Red;
            this.btnExit.Location = new System.Drawing.Point(242, 230);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(77, 31);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cbIgnCase
            // 
            this.cbIgnCase.AutoSize = true;
            this.cbIgnCase.Checked = true;
            this.cbIgnCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIgnCase.Location = new System.Drawing.Point(43, 160);
            this.cbIgnCase.Name = "cbIgnCase";
            this.cbIgnCase.Size = new System.Drawing.Size(112, 24);
            this.cbIgnCase.TabIndex = 2;
            this.cbIgnCase.Text = "Ignore case";
            this.cbIgnCase.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnSearch.Location = new System.Drawing.Point(180, 174);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(118, 43);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "SEARCH";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // gbFound
            // 
            this.gbFound.Controls.Add(this.tbMissing);
            this.gbFound.Controls.Add(this.label3);
            this.gbFound.Controls.Add(this.cbSelKey);
            this.gbFound.Controls.Add(this.lbKeyFound);
            this.gbFound.Location = new System.Drawing.Point(621, 331);
            this.gbFound.Name = "gbFound";
            this.gbFound.Size = new System.Drawing.Size(358, 366);
            this.gbFound.TabIndex = 6;
            this.gbFound.TabStop = false;
            this.gbFound.Text = "Keywords found";
            // 
            // tbMissing
            // 
            this.tbMissing.Location = new System.Drawing.Point(199, 224);
            this.tbMissing.Multiline = true;
            this.tbMissing.Name = "tbMissing";
            this.tbMissing.Size = new System.Drawing.Size(128, 86);
            this.tbMissing.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Info;
            this.label3.Location = new System.Drawing.Point(17, 258);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 40);
            this.label3.TabIndex = 8;
            this.label3.Text = "Keywords missing\r\nor (i)gnored";
            // 
            // cbSelKey
            // 
            this.cbSelKey.FormattingEnabled = true;
            this.cbSelKey.ItemHeight = 20;
            this.cbSelKey.Location = new System.Drawing.Point(242, 69);
            this.cbSelKey.Name = "cbSelKey";
            this.cbSelKey.Size = new System.Drawing.Size(85, 28);
            this.cbSelKey.TabIndex = 6;
            this.cbSelKey.Visible = false;
            // 
            // lbKeyFound
            // 
            this.lbKeyFound.FormattingEnabled = true;
            this.lbKeyFound.HorizontalScrollbar = true;
            this.lbKeyFound.ItemHeight = 20;
            this.lbKeyFound.Location = new System.Drawing.Point(21, 69);
            this.lbKeyFound.Name = "lbKeyFound";
            this.lbKeyFound.Size = new System.Drawing.Size(192, 124);
            this.lbKeyFound.TabIndex = 0;
            this.lbKeyFound.DoubleClick += new System.EventHandler(this.lbKeyFound_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Info;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 284);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(246, 48);
            this.label2.TabIndex = 4;
            this.label2.Text = "Double click any row to view macro and\r\nClick \'FOUND\' or \'FILE\' to sort columns.\r" +
    "\nRF macros can have language changes";
            this.toolTip1.SetToolTip(this.label2, "Rows with  RF will list only those\r\nreferences that contain the match\r\nAll other " +
        "rows display the entire macro.");
            // 
            // dgvSearched
            // 
            this.dgvSearched.AllowUserToAddRows = false;
            this.dgvSearched.AllowUserToDeleteRows = false;
            this.dgvSearched.AllowUserToResizeColumns = false;
            this.dgvSearched.AllowUserToResizeRows = false;
            this.dgvSearched.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearched.Location = new System.Drawing.Point(19, 355);
            this.dgvSearched.MultiSelect = false;
            this.dgvSearched.Name = "dgvSearched";
            this.dgvSearched.ReadOnly = true;
            this.dgvSearched.RowHeadersVisible = false;
            this.dgvSearched.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSearched.Size = new System.Drawing.Size(561, 342);
            this.dgvSearched.TabIndex = 3;
            this.dgvSearched.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearched_CellDoubleClick);
            this.dgvSearched.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSearched_ColumnHeaderMouseClick);
            this.dgvSearched.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearched_RowEnter);
            // 
            // tbKeywords
            // 
            this.tbKeywords.Location = new System.Drawing.Point(28, 239);
            this.tbKeywords.Name = "tbKeywords";
            this.tbKeywords.Size = new System.Drawing.Size(561, 26);
            this.tbKeywords.TabIndex = 2;
            this.toolTip1.SetToolTip(this.tbKeywords, "all single characters are ignored");
            this.tbKeywords.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbKeywords_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Location = new System.Drawing.Point(24, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 40);
            this.label1.TabIndex = 1;
            this.label1.Text = "Keywords: press\r\nenter to search";
            // 
            // WordSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1332, 805);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WordSearch";
            this.Text = "WordSearch";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.WordSearch_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WordSearch_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbMakeNew.ResumeLayout(false);
            this.gbAlltSearch.ResumeLayout(false);
            this.gbParam.ResumeLayout(false);
            this.gbParam.PerformLayout();
            this.gbRB.ResumeLayout(false);
            this.gbRB.PerformLayout();
            this.gbFound.ResumeLayout(false);
            this.gbFound.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearched)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbKeywords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvSearched;
        private System.Windows.Forms.GroupBox gbFound;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox gbParam;
        private System.Windows.Forms.CheckBox cbIgnCase;
        private System.Windows.Forms.RadioButton rbAnyMatch;
        private System.Windows.Forms.RadioButton rbExactMatch;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ListBox lbKeyFound;
        private System.Windows.Forms.Button btnExitToMac;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox cbHPKB;
        private System.Windows.Forms.GroupBox gbAlltSearch;
        private System.Windows.Forms.CheckBox cbOfferAlt;
        private System.Windows.Forms.Button btnPC;
        private System.Windows.Forms.Button btnMan;
        private System.Windows.Forms.Button btnDrv;
        private System.Windows.Forms.Button btnEbay;
        private System.Windows.Forms.Button btnPrn;
        private System.Windows.Forms.Button btnGoogle;
        private System.Windows.Forms.Button btnKbSearch;
        private System.Windows.Forms.Button btnHpYTsup;
        private System.Windows.Forms.GroupBox gbMakeNew;
        private System.Windows.Forms.ListBox lbNewItems;
        private System.Windows.Forms.Button btnMakeNew;
        private System.Windows.Forms.RadioButton rbEPhrase;
        private System.Windows.Forms.GroupBox gbSelect;
        private System.Windows.Forms.Button btnShowCC;
        private System.Windows.Forms.CheckBox cbvAddLangRef;
        private System.Windows.Forms.ComboBox cbSelKey;
        private System.Windows.Forms.GroupBox gbRB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbMissing;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbTitleSearch;
        private System.Windows.Forms.Button btnExitTitle;
    }
}