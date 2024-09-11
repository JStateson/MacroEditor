namespace MacroEditor.sources
{
    partial class CompareHPTR
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
            this.dgvDiff = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnHPclip = new System.Windows.Forms.Button();
            this.tbHPname = new System.Windows.Forms.TextBox();
            this.tbHPbody = new System.Windows.Forms.TextBox();
            this.gbTR = new System.Windows.Forms.GroupBox();
            this.btnCopyTR = new System.Windows.Forms.Button();
            this.tbTRname = new System.Windows.Forms.TextBox();
            this.tbTRbody = new System.Windows.Forms.TextBox();
            this.btnFirstH = new System.Windows.Forms.Button();
            this.btnShowH = new System.Windows.Forms.Button();
            this.btnShowT = new System.Windows.Forms.Button();
            this.btnShowBoth = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiff)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbTR.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDiff
            // 
            this.dgvDiff.AllowUserToAddRows = false;
            this.dgvDiff.AllowUserToDeleteRows = false;
            this.dgvDiff.AllowUserToResizeColumns = false;
            this.dgvDiff.AllowUserToResizeRows = false;
            this.dgvDiff.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDiff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDiff.Location = new System.Drawing.Point(16, 15);
            this.dgvDiff.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDiff.Name = "dgvDiff";
            this.dgvDiff.ReadOnly = true;
            this.dgvDiff.Size = new System.Drawing.Size(375, 658);
            this.dgvDiff.TabIndex = 0;
            this.dgvDiff.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDiff_RowEnter);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnHPclip);
            this.groupBox1.Controls.Add(this.tbHPname);
            this.groupBox1.Controls.Add(this.tbHPbody);
            this.groupBox1.Location = new System.Drawing.Point(540, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(788, 311);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Local HP Macros";
            // 
            // btnHPclip
            // 
            this.btnHPclip.Location = new System.Drawing.Point(629, 29);
            this.btnHPclip.Name = "btnHPclip";
            this.btnHPclip.Size = new System.Drawing.Size(145, 23);
            this.btnHPclip.TabIndex = 6;
            this.btnHPclip.Text = "Copy to clipboard";
            this.btnHPclip.UseVisualStyleBackColor = true;
            this.btnHPclip.Click += new System.EventHandler(this.btnHPclip_Click);
            // 
            // tbHPname
            // 
            this.tbHPname.Location = new System.Drawing.Point(143, 30);
            this.tbHPname.Name = "tbHPname";
            this.tbHPname.Size = new System.Drawing.Size(410, 22);
            this.tbHPname.TabIndex = 1;
            // 
            // tbHPbody
            // 
            this.tbHPbody.Location = new System.Drawing.Point(33, 68);
            this.tbHPbody.Multiline = true;
            this.tbHPbody.Name = "tbHPbody";
            this.tbHPbody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbHPbody.Size = new System.Drawing.Size(741, 216);
            this.tbHPbody.TabIndex = 0;
            // 
            // gbTR
            // 
            this.gbTR.Controls.Add(this.btnCopyTR);
            this.gbTR.Controls.Add(this.tbTRname);
            this.gbTR.Controls.Add(this.tbTRbody);
            this.gbTR.Location = new System.Drawing.Point(530, 363);
            this.gbTR.Name = "gbTR";
            this.gbTR.Size = new System.Drawing.Size(798, 310);
            this.gbTR.TabIndex = 2;
            this.gbTR.TabStop = false;
            this.gbTR.Text = "TR macros";
            // 
            // btnCopyTR
            // 
            this.btnCopyTR.Location = new System.Drawing.Point(639, 23);
            this.btnCopyTR.Name = "btnCopyTR";
            this.btnCopyTR.Size = new System.Drawing.Size(145, 23);
            this.btnCopyTR.TabIndex = 7;
            this.btnCopyTR.Text = "Copy to clipboard";
            this.btnCopyTR.UseVisualStyleBackColor = true;
            this.btnCopyTR.Click += new System.EventHandler(this.btnCopyTR_Click);
            // 
            // tbTRname
            // 
            this.tbTRname.Location = new System.Drawing.Point(139, 25);
            this.tbTRname.Name = "tbTRname";
            this.tbTRname.Size = new System.Drawing.Size(410, 22);
            this.tbTRname.TabIndex = 3;
            // 
            // tbTRbody
            // 
            this.tbTRbody.Location = new System.Drawing.Point(29, 63);
            this.tbTRbody.Multiline = true;
            this.tbTRbody.Name = "tbTRbody";
            this.tbTRbody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbTRbody.Size = new System.Drawing.Size(755, 216);
            this.tbTRbody.TabIndex = 2;
            // 
            // btnFirstH
            // 
            this.btnFirstH.Location = new System.Drawing.Point(413, 331);
            this.btnFirstH.Name = "btnFirstH";
            this.btnFirstH.Size = new System.Drawing.Size(75, 48);
            this.btnFirstH.TabIndex = 3;
            this.btnFirstH.Text = "Next\r\nDiff";
            this.btnFirstH.UseVisualStyleBackColor = true;
            this.btnFirstH.Click += new System.EventHandler(this.btnFirstH_Click);
            // 
            // btnShowH
            // 
            this.btnShowH.Location = new System.Drawing.Point(413, 153);
            this.btnShowH.Name = "btnShowH";
            this.btnShowH.Size = new System.Drawing.Size(75, 48);
            this.btnShowH.TabIndex = 4;
            this.btnShowH.Text = "Show\r\nPage";
            this.btnShowH.UseVisualStyleBackColor = true;
            this.btnShowH.Click += new System.EventHandler(this.btnShowH_Click);
            // 
            // btnShowT
            // 
            this.btnShowT.Location = new System.Drawing.Point(413, 510);
            this.btnShowT.Name = "btnShowT";
            this.btnShowT.Size = new System.Drawing.Size(75, 48);
            this.btnShowT.TabIndex = 5;
            this.btnShowT.Text = "Show\r\nPage";
            this.btnShowT.UseVisualStyleBackColor = true;
            this.btnShowT.Click += new System.EventHandler(this.btnShowT_Click);
            // 
            // btnShowBoth
            // 
            this.btnShowBoth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowBoth.Location = new System.Drawing.Point(424, 45);
            this.btnShowBoth.Name = "btnShowBoth";
            this.btnShowBoth.Size = new System.Drawing.Size(75, 48);
            this.btnShowBoth.TabIndex = 6;
            this.btnShowBoth.Text = "Show\r\nBoth";
            this.btnShowBoth.UseVisualStyleBackColor = true;
            this.btnShowBoth.Click += new System.EventHandler(this.btnShowBoth_Click);
            // 
            // CompareHPTR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1359, 699);
            this.Controls.Add(this.btnShowBoth);
            this.Controls.Add(this.btnShowT);
            this.Controls.Add(this.btnShowH);
            this.Controls.Add(this.btnFirstH);
            this.Controls.Add(this.gbTR);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvDiff);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CompareHPTR";
            this.Text = "CompareHPTR";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiff)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbTR.ResumeLayout(false);
            this.gbTR.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDiff;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbTR;
        private System.Windows.Forms.TextBox tbHPname;
        private System.Windows.Forms.TextBox tbHPbody;
        private System.Windows.Forms.TextBox tbTRname;
        private System.Windows.Forms.TextBox tbTRbody;
        private System.Windows.Forms.Button btnFirstH;
        private System.Windows.Forms.Button btnShowH;
        private System.Windows.Forms.Button btnShowT;
        private System.Windows.Forms.Button btnHPclip;
        private System.Windows.Forms.Button btnCopyTR;
        private System.Windows.Forms.Button btnShowBoth;
    }
}