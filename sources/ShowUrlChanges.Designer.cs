namespace MacroEditor
{
    partial class ShowUrlChanges
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbFromUrl = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbToUrl = new System.Windows.Forms.Label();
            this.lbFilenames = new System.Windows.Forms.ListBox();
            this.lbMacroNames = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnAppAll = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbFromUrl);
            this.groupBox1.Location = new System.Drawing.Point(12, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(765, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "From Url";
            // 
            // lbFromUrl
            // 
            this.lbFromUrl.AutoSize = true;
            this.lbFromUrl.Location = new System.Drawing.Point(33, 46);
            this.lbFromUrl.Name = "lbFromUrl";
            this.lbFromUrl.Size = new System.Drawing.Size(35, 13);
            this.lbFromUrl.TabIndex = 0;
            this.lbFromUrl.Text = "label1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbToUrl);
            this.groupBox2.Location = new System.Drawing.Point(12, 152);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(765, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "To Url";
            // 
            // lbToUrl
            // 
            this.lbToUrl.AutoSize = true;
            this.lbToUrl.Location = new System.Drawing.Point(33, 46);
            this.lbToUrl.Name = "lbToUrl";
            this.lbToUrl.Size = new System.Drawing.Size(35, 13);
            this.lbToUrl.TabIndex = 0;
            this.lbToUrl.Text = "label1";
            // 
            // lbFilenames
            // 
            this.lbFilenames.FormattingEnabled = true;
            this.lbFilenames.Location = new System.Drawing.Point(12, 337);
            this.lbFilenames.Name = "lbFilenames";
            this.lbFilenames.Size = new System.Drawing.Size(157, 147);
            this.lbFilenames.TabIndex = 2;
            this.lbFilenames.SelectedIndexChanged += new System.EventHandler(this.lbFilenames_SelectedIndexChanged);
            // 
            // lbMacroNames
            // 
            this.lbMacroNames.FormattingEnabled = true;
            this.lbMacroNames.HorizontalScrollbar = true;
            this.lbMacroNames.Location = new System.Drawing.Point(228, 337);
            this.lbMacroNames.Name = "lbMacroNames";
            this.lbMacroNames.Size = new System.Drawing.Size(197, 147);
            this.lbMacroNames.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 303);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name of file";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(225, 303);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Macro Name";
            // 
            // btnCancel
            // 
            this.btnCancel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCancel.Location = new System.Drawing.Point(174, 41);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnAppAll);
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Location = new System.Drawing.Point(481, 382);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(296, 102);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "This will exit the form";
            // 
            // btnAppAll
            // 
            this.btnAppAll.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnAppAll.Location = new System.Drawing.Point(48, 32);
            this.btnAppAll.Name = "btnAppAll";
            this.btnAppAll.Size = new System.Drawing.Size(88, 41);
            this.btnAppAll.TabIndex = 7;
            this.btnAppAll.Text = "Approve\r\nall changes";
            this.btnAppAll.UseVisualStyleBackColor = true;
            this.btnAppAll.Click += new System.EventHandler(this.btnAppAll_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackup.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnBackup.Location = new System.Drawing.Point(685, 284);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(92, 51);
            this.btnBackup.TabIndex = 8;
            this.btnBackup.Text = "Backup\r\nall files";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // ShowUrlChanges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 574);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbMacroNames);
            this.Controls.Add(this.lbFilenames);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ShowUrlChanges";
            this.Text = "ShowUrlChanges";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbFromUrl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbToUrl;
        private System.Windows.Forms.ListBox lbFilenames;
        private System.Windows.Forms.ListBox lbMacroNames;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnAppAll;
        private System.Windows.Forms.Button btnBackup;
    }
}