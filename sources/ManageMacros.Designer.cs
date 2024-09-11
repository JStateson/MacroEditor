namespace MacroEditor
{
    partial class ManageMacros
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageMacros));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnURLuse = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnUpdateURL = new System.Windows.Forms.Button();
            this.btnBrowseImage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLogIn = new System.Windows.Forms.Button();
            this.dgManage = new System.Windows.Forms.DataGridView();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgManage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnURLuse
            // 
            this.btnURLuse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnURLuse.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnURLuse.Location = new System.Drawing.Point(17, 485);
            this.btnURLuse.Name = "btnURLuse";
            this.btnURLuse.Size = new System.Drawing.Size(176, 38);
            this.btnURLuse.TabIndex = 10;
            this.btnURLuse.Text = "Or click for above URL";
            this.toolTip1.SetToolTip(this.btnURLuse, "Use this if the is a URL in the view\r\nLocal images must be browsed but\r\nURLs on o" +
        "n the internet already\r\nClipboard has the URL");
            this.btnURLuse.UseVisualStyleBackColor = true;
            this.btnURLuse.Click += new System.EventHandler(this.btnURLuse_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnURLuse);
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.btnUpdateURL);
            this.groupBox1.Controls.Add(this.btnBrowseImage);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnLogIn);
            this.groupBox1.Controls.Add(this.dgManage);
            this.groupBox1.Location = new System.Drawing.Point(12, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(447, 568);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Macro List and URL";
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnExit.Location = new System.Drawing.Point(295, 419);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(109, 38);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnUpdateURL
            // 
            this.btnUpdateURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateURL.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnUpdateURL.Location = new System.Drawing.Point(246, 350);
            this.btnUpdateURL.Name = "btnUpdateURL";
            this.btnUpdateURL.Size = new System.Drawing.Size(158, 38);
            this.btnUpdateURL.TabIndex = 8;
            this.btnUpdateURL.Text = "Update Image URL";
            this.btnUpdateURL.UseVisualStyleBackColor = true;
            this.btnUpdateURL.Click += new System.EventHandler(this.btnUpdateURL_Click);
            // 
            // btnBrowseImage
            // 
            this.btnBrowseImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseImage.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnBrowseImage.Location = new System.Drawing.Point(17, 419);
            this.btnBrowseImage.Name = "btnBrowseImage";
            this.btnBrowseImage.Size = new System.Drawing.Size(176, 38);
            this.btnBrowseImage.TabIndex = 7;
            this.btnBrowseImage.Text = "Browse to image file";
            this.btnBrowseImage.UseVisualStyleBackColor = true;
            this.btnBrowseImage.Click += new System.EventHandler(this.btnBrowseImage_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Select any row below for image";
            // 
            // btnLogIn
            // 
            this.btnLogIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogIn.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnLogIn.Location = new System.Drawing.Point(17, 350);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(176, 38);
            this.btnLogIn.TabIndex = 5;
            this.btnLogIn.Text = "Login to your Albums";
            this.btnLogIn.UseVisualStyleBackColor = true;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // dgManage
            // 
            this.dgManage.AllowUserToAddRows = false;
            this.dgManage.AllowUserToDeleteRows = false;
            this.dgManage.AllowUserToResizeRows = false;
            this.dgManage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            this.dgManage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgManage.Location = new System.Drawing.Point(17, 70);
            this.dgManage.MultiSelect = false;
            this.dgManage.Name = "dgManage";
            this.dgManage.ReadOnly = true;
            this.dgManage.RowHeadersWidth = 16;
            this.dgManage.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgManage.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgManage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgManage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgManage.ShowCellToolTips = false;
            this.dgManage.Size = new System.Drawing.Size(387, 250);
            this.dgManage.TabIndex = 4;
            this.dgManage.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgManage_CellContentClick);
            this.dgManage.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgManage_RowEnter);
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(487, 109);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(482, 418);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 4;
            this.pbImage.TabStop = false;
            // 
            // btnHelp
            // 
            this.btnHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnHelp.Location = new System.Drawing.Point(568, 38);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(169, 38);
            this.btnHelp.TabIndex = 6;
            this.btnHelp.Text = "Click for help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // ManageMacros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 621);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManageMacros";
            this.Text = "ManageMacros";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.ManageMacros_HelpButtonClicked);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgManage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgManage;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Button btnLogIn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpdateURL;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnURLuse;
        private System.Windows.Forms.Button btnBrowseImage;
    }
}