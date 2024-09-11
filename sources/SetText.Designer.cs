namespace MacroEditor
{
    partial class SetText
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetText));
            this.gbSelectType = new System.Windows.Forms.GroupBox();
            this.tbSelectedItem = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbMakeIMG = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbCleanUrl = new System.Windows.Forms.CheckBox();
            this.tbRawUrl = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbPrefix = new System.Windows.Forms.TextBox();
            this.btnClrDemo = new System.Windows.Forms.Button();
            this.btnDemo = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbSuffix = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbSizeImage = new System.Windows.Forms.ComboBox();
            this.gbPCTbw = new System.Windows.Forms.GroupBox();
            this.rb0pct = new System.Windows.Forms.RadioButton();
            this.rb50 = new System.Windows.Forms.RadioButton();
            this.rb100 = new System.Windows.Forms.RadioButton();
            this.btnClear = new System.Windows.Forms.Button();
            this.rbNoBox = new System.Windows.Forms.RadioButton();
            this.rbSqueeze = new System.Windows.Forms.RadioButton();
            this.rbFitBox = new System.Windows.Forms.RadioButton();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnApplyText = new System.Windows.Forms.Button();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cbPreFill = new System.Windows.Forms.CheckBox();
            this.gpTable = new System.Windows.Forms.GroupBox();
            this.btnApplyTab = new System.Windows.Forms.Button();
            this.tbCols = new System.Windows.Forms.TextBox();
            this.tbRows = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnTestD = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lbInfoTest = new System.Windows.Forms.Label();
            this.gbSelectType.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.gbPCTbw.SuspendLayout();
            this.gpTable.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSelectType
            // 
            this.gbSelectType.Controls.Add(this.tbSelectedItem);
            this.gbSelectType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSelectType.Location = new System.Drawing.Point(12, 23);
            this.gbSelectType.Name = "gbSelectType";
            this.gbSelectType.Size = new System.Drawing.Size(499, 141);
            this.gbSelectType.TabIndex = 1;
            this.gbSelectType.TabStop = false;
            this.gbSelectType.Text = "Your Selected TEXT (if any)";
            // 
            // tbSelectedItem
            // 
            this.tbSelectedItem.Location = new System.Drawing.Point(6, 34);
            this.tbSelectedItem.Multiline = true;
            this.tbSelectedItem.Name = "tbSelectedItem";
            this.tbSelectedItem.Size = new System.Drawing.Size(448, 83);
            this.tbSelectedItem.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbMakeIMG);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbCleanUrl);
            this.groupBox1.Controls.Add(this.tbRawUrl);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(18, 188);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 189);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Enter the URL here (if wanted)";
            // 
            // cbMakeIMG
            // 
            this.cbMakeIMG.AutoSize = true;
            this.cbMakeIMG.Location = new System.Drawing.Point(19, 144);
            this.cbMakeIMG.Name = "cbMakeIMG";
            this.cbMakeIMG.Size = new System.Drawing.Size(117, 20);
            this.cbMakeIMG.TabIndex = 3;
            this.cbMakeIMG.Text = "Treat as Image";
            this.toolTip1.SetToolTip(this.cbMakeIMG, "Url is is an image.  The size of\r\nthe image can be set only if the\r\nimage came fr" +
        "om an HP album");
            this.cbMakeIMG.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Info;
            this.label4.Location = new System.Drawing.Point(163, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(308, 64);
            this.label4.TabIndex = 2;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // cbCleanUrl
            // 
            this.cbCleanUrl.AutoSize = true;
            this.cbCleanUrl.Checked = true;
            this.cbCleanUrl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCleanUrl.Location = new System.Drawing.Point(19, 103);
            this.cbCleanUrl.Name = "cbCleanUrl";
            this.cbCleanUrl.Size = new System.Drawing.Size(91, 20);
            this.cbCleanUrl.TabIndex = 1;
            this.cbCleanUrl.Text = "Clean URL";
            this.toolTip1.SetToolTip(this.cbCleanUrl, "strip unwanted referals");
            this.cbCleanUrl.UseVisualStyleBackColor = true;
            // 
            // tbRawUrl
            // 
            this.tbRawUrl.Location = new System.Drawing.Point(19, 37);
            this.tbRawUrl.Multiline = true;
            this.tbRawUrl.Name = "tbRawUrl";
            this.tbRawUrl.Size = new System.Drawing.Size(430, 45);
            this.tbRawUrl.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbPrefix);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(551, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(208, 141);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Prefix (if any and can be empty)";
            // 
            // tbPrefix
            // 
            this.tbPrefix.Location = new System.Drawing.Point(19, 34);
            this.tbPrefix.Multiline = true;
            this.tbPrefix.Name = "tbPrefix";
            this.tbPrefix.Size = new System.Drawing.Size(174, 68);
            this.tbPrefix.TabIndex = 0;
            // 
            // btnClrDemo
            // 
            this.btnClrDemo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClrDemo.ForeColor = System.Drawing.Color.Green;
            this.btnClrDemo.Location = new System.Drawing.Point(6, 123);
            this.btnClrDemo.Name = "btnClrDemo";
            this.btnClrDemo.Size = new System.Drawing.Size(107, 26);
            this.btnClrDemo.TabIndex = 2;
            this.btnClrDemo.Text = "Clear demo";
            this.btnClrDemo.UseVisualStyleBackColor = true;
            this.btnClrDemo.Click += new System.EventHandler(this.btnClrDemo_Click);
            // 
            // btnDemo
            // 
            this.btnDemo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDemo.ForeColor = System.Drawing.Color.Green;
            this.btnDemo.Location = new System.Drawing.Point(6, 32);
            this.btnDemo.Name = "btnDemo";
            this.btnDemo.Size = new System.Drawing.Size(208, 26);
            this.btnDemo.TabIndex = 1;
            this.btnDemo.Text = "Click for demo";
            this.btnDemo.UseVisualStyleBackColor = true;
            this.btnDemo.Click += new System.EventHandler(this.btnDemo_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbSuffix);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(551, 196);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(208, 134);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Suffix (if any and can be empty)";
            // 
            // tbSuffix
            // 
            this.tbSuffix.Location = new System.Drawing.Point(19, 32);
            this.tbSuffix.Multiline = true;
            this.tbSuffix.Name = "tbSuffix";
            this.tbSuffix.Size = new System.Drawing.Size(174, 77);
            this.tbSuffix.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbSizeImage);
            this.groupBox4.Controls.Add(this.gbPCTbw);
            this.groupBox4.Controls.Add(this.btnClear);
            this.groupBox4.Controls.Add(this.rbNoBox);
            this.groupBox4.Controls.Add(this.rbSqueeze);
            this.groupBox4.Controls.Add(this.rbFitBox);
            this.groupBox4.Controls.Add(this.btnApply);
            this.groupBox4.Controls.Add(this.btnTest);
            this.groupBox4.Controls.Add(this.btnApplyText);
            this.groupBox4.Controls.Add(this.tbResult);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(15, 396);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(744, 281);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "URL result is here";
            // 
            // cbSizeImage
            // 
            this.cbSizeImage.FormattingEnabled = true;
            this.cbSizeImage.Items.AddRange(new object[] {
            "Default Image Size",
            "tiny",
            "thumb",
            "small",
            "medium",
            "large"});
            this.cbSizeImage.Location = new System.Drawing.Point(555, 110);
            this.cbSizeImage.Name = "cbSizeImage";
            this.cbSizeImage.Size = new System.Drawing.Size(174, 24);
            this.cbSizeImage.TabIndex = 4;
            this.cbSizeImage.Text = "Default Image Size";
            this.toolTip1.SetToolTip(this.cbSizeImage, "This only works on  HP album images");
            this.cbSizeImage.Visible = false;
            this.cbSizeImage.SelectedIndexChanged += new System.EventHandler(this.cbSizeImage_SelectedIndexChanged);
            // 
            // gbPCTbw
            // 
            this.gbPCTbw.Controls.Add(this.rb0pct);
            this.gbPCTbw.Controls.Add(this.rb50);
            this.gbPCTbw.Controls.Add(this.rb100);
            this.gbPCTbw.Location = new System.Drawing.Point(359, 21);
            this.gbPCTbw.Name = "gbPCTbw";
            this.gbPCTbw.Size = new System.Drawing.Size(165, 113);
            this.gbPCTbw.TabIndex = 12;
            this.gbPCTbw.TabStop = false;
            this.gbPCTbw.Text = "Box width (%)";
            // 
            // rb0pct
            // 
            this.rb0pct.AutoSize = true;
            this.rb0pct.Checked = true;
            this.rb0pct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb0pct.ForeColor = System.Drawing.SystemColors.Highlight;
            this.rb0pct.Location = new System.Drawing.Point(38, 84);
            this.rb0pct.Name = "rb0pct";
            this.rb0pct.Size = new System.Drawing.Size(74, 20);
            this.rb0pct.TabIndex = 13;
            this.rb0pct.TabStop = true;
            this.rb0pct.Text = "Default";
            this.rb0pct.UseVisualStyleBackColor = true;
            // 
            // rb50
            // 
            this.rb50.AutoSize = true;
            this.rb50.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb50.ForeColor = System.Drawing.SystemColors.Highlight;
            this.rb50.Location = new System.Drawing.Point(38, 55);
            this.rb50.Name = "rb50";
            this.rb50.Size = new System.Drawing.Size(41, 20);
            this.rb50.TabIndex = 12;
            this.rb50.Text = "50";
            this.rb50.UseVisualStyleBackColor = true;
            // 
            // rb100
            // 
            this.rb100.AutoSize = true;
            this.rb100.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb100.ForeColor = System.Drawing.SystemColors.Highlight;
            this.rb100.Location = new System.Drawing.Point(38, 25);
            this.rb100.Name = "rb100";
            this.rb100.Size = new System.Drawing.Size(49, 20);
            this.rb100.TabIndex = 11;
            this.rb100.Text = "100";
            this.rb100.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnClear.Location = new System.Drawing.Point(650, 72);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(79, 24);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // rbNoBox
            // 
            this.rbNoBox.AutoSize = true;
            this.rbNoBox.Checked = true;
            this.rbNoBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNoBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.rbNoBox.Location = new System.Drawing.Point(188, 87);
            this.rbNoBox.Name = "rbNoBox";
            this.rbNoBox.Size = new System.Drawing.Size(82, 24);
            this.rbNoBox.TabIndex = 10;
            this.rbNoBox.TabStop = true;
            this.rbNoBox.Text = "No box";
            this.rbNoBox.UseVisualStyleBackColor = true;
            // 
            // rbSqueeze
            // 
            this.rbSqueeze.AutoSize = true;
            this.rbSqueeze.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSqueeze.ForeColor = System.Drawing.SystemColors.Highlight;
            this.rbSqueeze.Location = new System.Drawing.Point(188, 58);
            this.rbSqueeze.Name = "rbSqueeze";
            this.rbSqueeze.Size = new System.Drawing.Size(156, 24);
            this.rbSqueeze.TabIndex = 9;
            this.rbSqueeze.Text = "Squeze into box";
            this.rbSqueeze.UseVisualStyleBackColor = true;
            // 
            // rbFitBox
            // 
            this.rbFitBox.AutoSize = true;
            this.rbFitBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFitBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.rbFitBox.Location = new System.Drawing.Point(188, 28);
            this.rbFitBox.Name = "rbFitBox";
            this.rbFitBox.Size = new System.Drawing.Size(115, 24);
            this.rbFitBox.TabIndex = 8;
            this.rbFitBox.Text = "Fit in a box";
            this.rbFitBox.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnApply.Location = new System.Drawing.Point(34, 78);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(122, 36);
            this.btnApply.TabIndex = 6;
            this.btnApply.Text = "Apply and exit";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTest.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnTest.Location = new System.Drawing.Point(607, 28);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(122, 24);
            this.btnTest.TabIndex = 7;
            this.btnTest.Text = "Test Object";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnApplyText
            // 
            this.btnApplyText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplyText.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnApplyText.Location = new System.Drawing.Point(34, 26);
            this.btnApplyText.Name = "btnApplyText";
            this.btnApplyText.Size = new System.Drawing.Size(122, 36);
            this.btnApplyText.TabIndex = 6;
            this.btnApplyText.Text = "Form Object";
            this.btnApplyText.UseVisualStyleBackColor = true;
            this.btnApplyText.Click += new System.EventHandler(this.btnApplyText_Click);
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(22, 162);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.Size = new System.Drawing.Size(707, 100);
            this.tbResult.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCancel.Location = new System.Drawing.Point(1049, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(122, 36);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel and exit";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbPreFill
            // 
            this.cbPreFill.AutoSize = true;
            this.cbPreFill.Location = new System.Drawing.Point(23, 105);
            this.cbPreFill.Name = "cbPreFill";
            this.cbPreFill.Size = new System.Drawing.Size(130, 20);
            this.cbPreFill.TabIndex = 5;
            this.cbPreFill.Text = "Fill alphabetically";
            this.toolTip1.SetToolTip(this.cbPreFill, "Fill with letters indicating the row and column numvber");
            this.cbPreFill.UseVisualStyleBackColor = true;
            // 
            // gpTable
            // 
            this.gpTable.Controls.Add(this.cbPreFill);
            this.gpTable.Controls.Add(this.btnApplyTab);
            this.gpTable.Controls.Add(this.tbCols);
            this.gpTable.Controls.Add(this.tbRows);
            this.gpTable.Controls.Add(this.label2);
            this.gpTable.Controls.Add(this.label1);
            this.gpTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpTable.Location = new System.Drawing.Point(785, 57);
            this.gpTable.Name = "gpTable";
            this.gpTable.Size = new System.Drawing.Size(386, 149);
            this.gpTable.TabIndex = 8;
            this.gpTable.TabStop = false;
            this.gpTable.Text = "Create a table";
            // 
            // btnApplyTab
            // 
            this.btnApplyTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplyTab.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnApplyTab.Location = new System.Drawing.Point(204, 54);
            this.btnApplyTab.Name = "btnApplyTab";
            this.btnApplyTab.Size = new System.Drawing.Size(122, 36);
            this.btnApplyTab.TabIndex = 4;
            this.btnApplyTab.Text = "Form Object";
            this.btnApplyTab.UseVisualStyleBackColor = true;
            this.btnApplyTab.Click += new System.EventHandler(this.btnApplyTab_Click);
            // 
            // tbCols
            // 
            this.tbCols.Location = new System.Drawing.Point(114, 68);
            this.tbCols.Name = "tbCols";
            this.tbCols.Size = new System.Drawing.Size(57, 22);
            this.tbCols.TabIndex = 3;
            this.tbCols.Text = "1";
            // 
            // tbRows
            // 
            this.tbRows.Location = new System.Drawing.Point(114, 23);
            this.tbRows.Name = "tbRows";
            this.tbRows.Size = new System.Drawing.Size(57, 22);
            this.tbRows.TabIndex = 2;
            this.tbRows.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Columns";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rows";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Info;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(782, 409);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(389, 240);
            this.label3.TabIndex = 6;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // btnTestD
            // 
            this.btnTestD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestD.ForeColor = System.Drawing.Color.Green;
            this.btnTestD.Location = new System.Drawing.Point(6, 76);
            this.btnTestD.Name = "btnTestD";
            this.btnTestD.Size = new System.Drawing.Size(107, 26);
            this.btnTestD.TabIndex = 3;
            this.btnTestD.Text = "Test Demo";
            this.btnTestD.UseVisualStyleBackColor = true;
            this.btnTestD.Visible = false;
            this.btnTestD.Click += new System.EventHandler(this.btnTestD_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lbInfoTest);
            this.groupBox5.Controls.Add(this.btnDemo);
            this.groupBox5.Controls.Add(this.btnTestD);
            this.groupBox5.Controls.Add(this.btnClrDemo);
            this.groupBox5.Location = new System.Drawing.Point(785, 228);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(363, 167);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Demo Tests";
            // 
            // lbInfoTest
            // 
            this.lbInfoTest.AutoSize = true;
            this.lbInfoTest.BackColor = System.Drawing.SystemColors.Info;
            this.lbInfoTest.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbInfoTest.Location = new System.Drawing.Point(133, 83);
            this.lbInfoTest.Name = "lbInfoTest";
            this.lbInfoTest.Size = new System.Drawing.Size(190, 39);
            this.lbInfoTest.TabIndex = 10;
            this.lbInfoTest.Text = "Click to run 4 demos of hyperlinks\r\nGoto website, Create table, \r\nClick to see im" +
    "age, and an inline image";
            // 
            // SetText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1183, 700);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gpTable);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbSelectType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetText";
            this.Text = "SetText";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetText_FormClosing);
            this.Shown += new System.EventHandler(this.SetText_Shown);
            this.gbSelectType.ResumeLayout(false);
            this.gbSelectType.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.gbPCTbw.ResumeLayout(false);
            this.gbPCTbw.PerformLayout();
            this.gpTable.ResumeLayout(false);
            this.gpTable.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSelectType;
        private System.Windows.Forms.TextBox tbSelectedItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbRawUrl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbPrefix;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbSuffix;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnApplyText;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox gpTable;
        private System.Windows.Forms.Button btnApplyTab;
        private System.Windows.Forms.TextBox tbCols;
        private System.Windows.Forms.TextBox tbRows;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbPreFill;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbCleanUrl;
        private System.Windows.Forms.RadioButton rbSqueeze;
        private System.Windows.Forms.RadioButton rbFitBox;
        private System.Windows.Forms.RadioButton rbNoBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDemo;
        private System.Windows.Forms.Button btnClrDemo;
        private System.Windows.Forms.CheckBox cbMakeIMG;
        private System.Windows.Forms.GroupBox gbPCTbw;
        private System.Windows.Forms.RadioButton rb0pct;
        private System.Windows.Forms.RadioButton rb50;
        private System.Windows.Forms.RadioButton rb100;
        private System.Windows.Forms.Button btnTestD;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cbSizeImage;
        private System.Windows.Forms.Label lbInfoTest;
    }
}