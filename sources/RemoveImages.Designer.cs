namespace MacroEditor
{
    partial class RemoveImages
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoveImages));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelUnused = new System.Windows.Forms.Button();
            this.dgvUsedImages = new System.Windows.Forms.DataGridView();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbUsedLF = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsedImages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.btnDelUnused);
            this.groupBox3.Controls.Add(this.dgvUsedImages);
            this.groupBox3.Location = new System.Drawing.Point(24, 32);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(433, 406);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Advailable Image Files";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(156, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 32);
            this.label1.TabIndex = 5;
            this.label1.Text = "This deletes all the checked images.\r\nThe unchecked ones are being used.";
            // 
            // btnDelUnused
            // 
            this.btnDelUnused.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelUnused.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnDelUnused.Location = new System.Drawing.Point(26, 37);
            this.btnDelUnused.Name = "btnDelUnused";
            this.btnDelUnused.Size = new System.Drawing.Size(102, 26);
            this.btnDelUnused.TabIndex = 4;
            this.btnDelUnused.Text = "Delete All";
            this.btnDelUnused.UseVisualStyleBackColor = true;
            this.btnDelUnused.Click += new System.EventHandler(this.btnDelUnused_Click);
            // 
            // dgvUsedImages
            // 
            this.dgvUsedImages.AllowUserToAddRows = false;
            this.dgvUsedImages.AllowUserToDeleteRows = false;
            this.dgvUsedImages.AllowUserToResizeColumns = false;
            this.dgvUsedImages.AllowUserToResizeRows = false;
            this.dgvUsedImages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsedImages.Location = new System.Drawing.Point(26, 87);
            this.dgvUsedImages.Name = "dgvUsedImages";
            this.dgvUsedImages.Size = new System.Drawing.Size(358, 292);
            this.dgvUsedImages.TabIndex = 0;
            this.dgvUsedImages.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvUsedImages_CellMouseDoubleClick);
            this.dgvUsedImages.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsedImages_RowEnter);
            // 
            // pbImage
            // 
            this.pbImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbImage.Location = new System.Drawing.Point(489, 144);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(355, 294);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 6;
            this.pbImage.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Info;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(563, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 48);
            this.label2.TabIndex = 7;
            this.label2.Text = "The image you select can\r\nbe examined here. Uncheck\r\nit if you want to save it.";
            // 
            // lbUsedLF
            // 
            this.lbUsedLF.FormattingEnabled = true;
            this.lbUsedLF.Location = new System.Drawing.Point(911, 144);
            this.lbUsedLF.Name = "lbUsedLF";
            this.lbUsedLF.Size = new System.Drawing.Size(261, 290);
            this.lbUsedLF.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Info;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(918, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(245, 80);
            this.label3.TabIndex = 9;
            this.label3.Text = "These local image files were found in\r\nthe source body. (P) indicartes the file\r\n" +
    "is Present. (M) means it is Missing on the\r\ndisk drive.  Image files in the sign" +
    "ature \r\nSI are not listed here.";
            // 
            // RemoveImages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1213, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbUsedLF);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RemoveImages";
            this.Text = "View and Remove unused Images";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RemoveImages_FormClosing);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsedImages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDelUnused;
        private System.Windows.Forms.DataGridView dgvUsedImages;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbUsedLF;
        private System.Windows.Forms.Label label3;
    }
}