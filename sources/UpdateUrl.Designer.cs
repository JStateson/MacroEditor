namespace MacroEditor
{
    partial class UpdateUrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateUrl));
            this.btnSaveExit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.tbInfo = new System.Windows.Forms.TextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbOld = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSaveExit
            // 
            this.btnSaveExit.Enabled = false;
            this.btnSaveExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveExit.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnSaveExit.Location = new System.Drawing.Point(606, 60);
            this.btnSaveExit.Name = "btnSaveExit";
            this.btnSaveExit.Size = new System.Drawing.Size(176, 38);
            this.btnSaveExit.TabIndex = 6;
            this.btnSaveExit.Text = "SAVE and exit";
            this.btnSaveExit.UseVisualStyleBackColor = true;
            this.btnSaveExit.Click += new System.EventHandler(this.btnSaveExit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCancel.Location = new System.Drawing.Point(606, 124);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(176, 38);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbUrl
            // 
            this.tbUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbUrl.Location = new System.Drawing.Point(37, 76);
            this.tbUrl.Multiline = true;
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbUrl.Size = new System.Drawing.Size(406, 73);
            this.tbUrl.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTest);
            this.groupBox1.Controls.Add(this.tbInfo);
            this.groupBox1.Controls.Add(this.btnApply);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbUrl);
            this.groupBox1.Location = new System.Drawing.Point(32, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(534, 317);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "URL from HP album";
            // 
            // btnTest
            // 
            this.btnTest.Enabled = false;
            this.btnTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTest.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnTest.Location = new System.Drawing.Point(33, 245);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(106, 38);
            this.btnTest.TabIndex = 12;
            this.btnTest.Text = "TEST IMG";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // tbInfo
            // 
            this.tbInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbInfo.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tbInfo.Location = new System.Drawing.Point(238, 175);
            this.tbInfo.Multiline = true;
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.Size = new System.Drawing.Size(227, 108);
            this.tbInfo.TabIndex = 11;
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnApply.Location = new System.Drawing.Point(33, 175);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(106, 38);
            this.btnApply.TabIndex = 10;
            this.btnApply.Text = "APPLY";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(115, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Paste URL and click apply";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbOld);
            this.groupBox2.Location = new System.Drawing.Point(32, 374);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(534, 100);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Local Path to remove";
            // 
            // tbOld
            // 
            this.tbOld.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOld.Location = new System.Drawing.Point(39, 39);
            this.tbOld.Name = "tbOld";
            this.tbOld.ReadOnly = true;
            this.tbOld.Size = new System.Drawing.Size(462, 22);
            this.tbOld.TabIndex = 0;
            // 
            // UpdateUrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 502);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UpdateUrl";
            this.Text = "UpdateUrl";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSaveExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbInfo;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbOld;
    }
}