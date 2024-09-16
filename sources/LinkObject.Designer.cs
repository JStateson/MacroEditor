namespace MacroEditor
{
    partial class LinkObject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkObject));
            this.gbSelectType = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbNotImage = new System.Windows.Forms.RadioButton();
            this.rbimage = new System.Windows.Forms.RadioButton();
            this.tbRawUrl = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbNoBox = new System.Windows.Forms.RadioButton();
            this.rbSqueeze = new System.Windows.Forms.RadioButton();
            this.rbFitBox = new System.Windows.Forms.RadioButton();
            this.btnClear = new System.Windows.Forms.Button();
            this.gbPCTbw = new System.Windows.Forms.GroupBox();
            this.rb0pct = new System.Windows.Forms.RadioButton();
            this.rb50 = new System.Windows.Forms.RadioButton();
            this.rb100 = new System.Windows.Forms.RadioButton();
            this.btnAE = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbUrlText = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnApplyText = new System.Windows.Forms.Button();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCencel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.rbText = new System.Windows.Forms.RadioButton();
            this.gbSelectType.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbPCTbw.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // gbSelectType
            // 
            this.gbSelectType.Controls.Add(this.groupBox2);
            this.gbSelectType.Controls.Add(this.tbRawUrl);
            this.gbSelectType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSelectType.Location = new System.Drawing.Point(12, 32);
            this.gbSelectType.Name = "gbSelectType";
            this.gbSelectType.Size = new System.Drawing.Size(562, 199);
            this.gbSelectType.TabIndex = 0;
            this.gbSelectType.TabStop = false;
            this.gbSelectType.Text = "You Selected HTML (or item)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbText);
            this.groupBox2.Controls.Add(this.rbNotImage);
            this.groupBox2.Controls.Add(this.rbimage);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(398, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(148, 123);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "This Is";
            // 
            // rbNotImage
            // 
            this.rbNotImage.AutoSize = true;
            this.rbNotImage.Location = new System.Drawing.Point(20, 62);
            this.rbNotImage.Name = "rbNotImage";
            this.rbNotImage.Size = new System.Drawing.Size(105, 20);
            this.rbNotImage.TabIndex = 1;
            this.rbNotImage.TabStop = true;
            this.rbNotImage.Text = "Treat as URL";
            this.rbNotImage.UseVisualStyleBackColor = true;
            this.rbNotImage.CheckedChanged += new System.EventHandler(this.rbNotImage_CheckedChanged);
            // 
            // rbimage
            // 
            this.rbimage.AutoSize = true;
            this.rbimage.Location = new System.Drawing.Point(20, 34);
            this.rbimage.Name = "rbimage";
            this.rbimage.Size = new System.Drawing.Size(63, 20);
            this.rbimage.TabIndex = 0;
            this.rbimage.TabStop = true;
            this.rbimage.Text = "Image";
            this.rbimage.UseVisualStyleBackColor = true;
            this.rbimage.CheckedChanged += new System.EventHandler(this.rbimage_CheckedChanged);
            // 
            // tbRawUrl
            // 
            this.tbRawUrl.Location = new System.Drawing.Point(27, 30);
            this.tbRawUrl.Multiline = true;
            this.tbRawUrl.Name = "tbRawUrl";
            this.tbRawUrl.Size = new System.Drawing.Size(341, 141);
            this.tbRawUrl.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbNoBox);
            this.groupBox1.Controls.Add(this.rbSqueeze);
            this.groupBox1.Controls.Add(this.rbFitBox);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.gbPCTbw);
            this.groupBox1.Controls.Add(this.btnAE);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbUrlText);
            this.groupBox1.Controls.Add(this.btnTest);
            this.groupBox1.Controls.Add(this.btnApplyText);
            this.groupBox1.Controls.Add(this.tbResult);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 250);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(766, 346);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Compose URL or image";
            // 
            // rbNoBox
            // 
            this.rbNoBox.AutoSize = true;
            this.rbNoBox.Checked = true;
            this.rbNoBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNoBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.rbNoBox.Location = new System.Drawing.Point(213, 208);
            this.rbNoBox.Name = "rbNoBox";
            this.rbNoBox.Size = new System.Drawing.Size(82, 24);
            this.rbNoBox.TabIndex = 17;
            this.rbNoBox.TabStop = true;
            this.rbNoBox.Text = "No box";
            this.rbNoBox.UseVisualStyleBackColor = true;
            // 
            // rbSqueeze
            // 
            this.rbSqueeze.AutoSize = true;
            this.rbSqueeze.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSqueeze.ForeColor = System.Drawing.SystemColors.Highlight;
            this.rbSqueeze.Location = new System.Drawing.Point(213, 179);
            this.rbSqueeze.Name = "rbSqueeze";
            this.rbSqueeze.Size = new System.Drawing.Size(156, 24);
            this.rbSqueeze.TabIndex = 16;
            this.rbSqueeze.Text = "Squeze into box";
            this.rbSqueeze.UseVisualStyleBackColor = true;
            // 
            // rbFitBox
            // 
            this.rbFitBox.AutoSize = true;
            this.rbFitBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFitBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.rbFitBox.Location = new System.Drawing.Point(213, 149);
            this.rbFitBox.Name = "rbFitBox";
            this.rbFitBox.Size = new System.Drawing.Size(115, 24);
            this.rbFitBox.TabIndex = 15;
            this.rbFitBox.Text = "Fit in a box";
            this.rbFitBox.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnClear.Location = new System.Drawing.Point(657, 63);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(83, 36);
            this.btnClear.TabIndex = 14;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // gbPCTbw
            // 
            this.gbPCTbw.Controls.Add(this.rb0pct);
            this.gbPCTbw.Controls.Add(this.rb50);
            this.gbPCTbw.Controls.Add(this.rb100);
            this.gbPCTbw.Location = new System.Drawing.Point(494, 128);
            this.gbPCTbw.Name = "gbPCTbw";
            this.gbPCTbw.Size = new System.Drawing.Size(165, 113);
            this.gbPCTbw.TabIndex = 13;
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
            // btnAE
            // 
            this.btnAE.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAE.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnAE.Location = new System.Drawing.Point(30, 205);
            this.btnAE.Name = "btnAE";
            this.btnAE.Size = new System.Drawing.Size(122, 36);
            this.btnAE.TabIndex = 10;
            this.btnAE.Text = "Apply and exit";
            this.btnAE.UseVisualStyleBackColor = true;
            this.btnAE.Click += new System.EventHandler(this.btnAE_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Text for URL (can be omitted)";
            // 
            // tbUrlText
            // 
            this.tbUrlText.Location = new System.Drawing.Point(27, 67);
            this.tbUrlText.Multiline = true;
            this.tbUrlText.Name = "tbUrlText";
            this.tbUrlText.Size = new System.Drawing.Size(412, 55);
            this.tbUrlText.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tbUrlText, "Can be left blank if no text wanted");
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTest.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnTest.Location = new System.Drawing.Point(618, 21);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(122, 36);
            this.btnTest.TabIndex = 5;
            this.btnTest.Text = "Test Object";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnApplyText
            // 
            this.btnApplyText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplyText.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnApplyText.Location = new System.Drawing.Point(27, 139);
            this.btnApplyText.Name = "btnApplyText";
            this.btnApplyText.Size = new System.Drawing.Size(122, 36);
            this.btnApplyText.TabIndex = 4;
            this.btnApplyText.Text = "Form Object";
            this.btnApplyText.UseVisualStyleBackColor = true;
            this.btnApplyText.Click += new System.EventHandler(this.btnApplyText_Click);
            // 
            // tbResult
            // 
            this.tbResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbResult.Location = new System.Drawing.Point(27, 259);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.Size = new System.Drawing.Size(632, 70);
            this.tbResult.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tbResult, "This can be edited");
            // 
            // pbImage
            // 
            this.pbImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbImage.Image = ((System.Drawing.Image)(resources.GetObject("pbImage.Image")));
            this.pbImage.Location = new System.Drawing.Point(630, 32);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(256, 199);
            this.pbImage.TabIndex = 2;
            this.pbImage.TabStop = false;
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnApply.Location = new System.Drawing.Point(921, 71);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(122, 36);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "Apply and exit";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCencel
            // 
            this.btnCencel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCencel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCencel.Location = new System.Drawing.Point(921, 141);
            this.btnCencel.Name = "btnCencel";
            this.btnCencel.Size = new System.Drawing.Size(122, 36);
            this.btnCencel.TabIndex = 4;
            this.btnCencel.Text = "Cancel and exit";
            this.btnCencel.UseVisualStyleBackColor = true;
            this.btnCencel.Click += new System.EventHandler(this.btnCencel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Info;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(801, 329);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(222, 96);
            this.label3.TabIndex = 7;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // rbText
            // 
            this.rbText.AutoSize = true;
            this.rbText.Location = new System.Drawing.Point(20, 88);
            this.rbText.Name = "rbText";
            this.rbText.Size = new System.Drawing.Size(104, 20);
            this.rbText.TabIndex = 2;
            this.rbText.TabStop = true;
            this.rbText.Text = "Treat as Text";
            this.toolTip1.SetToolTip(this.rbText, "For just drawing a box\r\nabout the selected text");
            this.rbText.UseVisualStyleBackColor = true;
            // 
            // LinkObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 608);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCencel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbSelectType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LinkObject";
            this.Text = "LinkObject";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LinkObject_FormClosing);
            this.Shown += new System.EventHandler(this.LinkObject_Shown);
            this.gbSelectType.ResumeLayout(false);
            this.gbSelectType.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbPCTbw.ResumeLayout(false);
            this.gbPCTbw.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSelectType;
        private System.Windows.Forms.TextBox tbRawUrl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbNotImage;
        private System.Windows.Forms.RadioButton rbimage;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCencel;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnApplyText;
        private System.Windows.Forms.TextBox tbUrlText;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnAE;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbPCTbw;
        private System.Windows.Forms.RadioButton rb0pct;
        private System.Windows.Forms.RadioButton rb50;
        private System.Windows.Forms.RadioButton rb100;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RadioButton rbNoBox;
        private System.Windows.Forms.RadioButton rbSqueeze;
        private System.Windows.Forms.RadioButton rbFitBox;
        private System.Windows.Forms.RadioButton rbText;
    }
}