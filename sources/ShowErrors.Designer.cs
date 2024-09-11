namespace MacroEditor
{
    partial class ShowErrors
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowErrors));
            this.lbMacNames = new System.Windows.Forms.ListBox();
            this.tbError = new System.Windows.Forms.TextBox();
            this.btnFindErr = new System.Windows.Forms.Button();
            this.gbShowErr = new System.Windows.Forms.GroupBox();
            this.gbShowErr.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbMacNames
            // 
            this.lbMacNames.FormattingEnabled = true;
            this.lbMacNames.Location = new System.Drawing.Point(22, 147);
            this.lbMacNames.Name = "lbMacNames";
            this.lbMacNames.Size = new System.Drawing.Size(215, 316);
            this.lbMacNames.TabIndex = 0;
            this.lbMacNames.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbMacNames_MouseClick);
            this.lbMacNames.SelectedIndexChanged += new System.EventHandler(this.lbMacNames_SelectedIndexChanged);
            // 
            // tbError
            // 
            this.tbError.Location = new System.Drawing.Point(365, 56);
            this.tbError.Multiline = true;
            this.tbError.Name = "tbError";
            this.tbError.Size = new System.Drawing.Size(451, 407);
            this.tbError.TabIndex = 1;
            // 
            // btnFindErr
            // 
            this.btnFindErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindErr.ForeColor = System.Drawing.Color.Red;
            this.btnFindErr.Location = new System.Drawing.Point(46, 38);
            this.btnFindErr.Name = "btnFindErr";
            this.btnFindErr.Size = new System.Drawing.Size(154, 23);
            this.btnFindErr.TabIndex = 2;
            this.btnFindErr.Text = "Macro name";
            this.btnFindErr.UseVisualStyleBackColor = true;
            this.btnFindErr.Click += new System.EventHandler(this.btnFindErr_Click);
            // 
            // gbShowErr
            // 
            this.gbShowErr.Controls.Add(this.btnFindErr);
            this.gbShowErr.Location = new System.Drawing.Point(37, 15);
            this.gbShowErr.Name = "gbShowErr";
            this.gbShowErr.Size = new System.Drawing.Size(291, 100);
            this.gbShowErr.TabIndex = 3;
            this.gbShowErr.TabStop = false;
            this.gbShowErr.Text = "Click name for errors";
            // 
            // ShowErrors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 512);
            this.Controls.Add(this.gbShowErr);
            this.Controls.Add(this.tbError);
            this.Controls.Add(this.lbMacNames);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowErrors";
            this.Text = "Macro Errors";
            this.gbShowErr.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbMacNames;
        private System.Windows.Forms.TextBox tbError;
        private System.Windows.Forms.Button btnFindErr;
        private System.Windows.Forms.GroupBox gbShowErr;
    }
}