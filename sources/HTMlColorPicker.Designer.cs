namespace MacroEditor
{
    partial class HTMlColorPicker
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
            this.lbColors = new System.Windows.Forms.ListBox();
            this.lbInvCol = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.tbOut = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbColors
            // 
            this.lbColors.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbColors.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbColors.FormattingEnabled = true;
            this.lbColors.Items.AddRange(new object[] {
            "black",
            "silver",
            "gray",
            "white",
            "maroon",
            "red",
            "purple",
            "fuchsia",
            "green",
            "lime",
            "olive",
            "yellow",
            "navy",
            "blue",
            "teal",
            "aqua"});
            this.lbColors.Location = new System.Drawing.Point(26, 140);
            this.lbColors.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbColors.Name = "lbColors";
            this.lbColors.Size = new System.Drawing.Size(170, 407);
            this.lbColors.TabIndex = 0;
            this.lbColors.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbColors_DrawItem);
            this.lbColors.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lbColors_MeasureItem);
            this.lbColors.SelectedIndexChanged += new System.EventHandler(this.lbColors_SelectedIndexChanged);
            // 
            // lbInvCol
            // 
            this.lbInvCol.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbInvCol.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInvCol.FormattingEnabled = true;
            this.lbInvCol.Items.AddRange(new object[] {
            "black",
            "silver",
            "gray",
            "white",
            "maroon",
            "red",
            "purple",
            "fuchsia",
            "green",
            "lime",
            "olive",
            "yellow",
            "navy",
            "blue",
            "teal",
            "aqua"});
            this.lbInvCol.Location = new System.Drawing.Point(253, 140);
            this.lbInvCol.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbInvCol.Name = "lbInvCol";
            this.lbInvCol.Size = new System.Drawing.Size(170, 407);
            this.lbInvCol.TabIndex = 1;
            this.lbInvCol.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbInvCol_DrawItem);
            this.lbInvCol.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lbInvCol_MeasureItem);
            this.lbInvCol.SelectedIndexChanged += new System.EventHandler(this.lbInvCol_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnApply);
            this.groupBox1.Controls.Add(this.tbOut);
            this.groupBox1.Controls.Add(this.lbInvCol);
            this.groupBox1.Controls.Add(this.lbColors);
            this.groupBox1.Location = new System.Drawing.Point(28, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 612);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select the left side color first then the right side then click APPLY";
            // 
            // btnApply
            // 
            this.btnApply.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnApply.Location = new System.Drawing.Point(49, 40);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(116, 36);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "Apply and exit";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // tbOut
            // 
            this.tbOut.Location = new System.Drawing.Point(229, 45);
            this.tbOut.Name = "tbOut";
            this.tbOut.Size = new System.Drawing.Size(194, 26);
            this.tbOut.TabIndex = 2;
            this.tbOut.Text = "These are my COLORS";
            // 
            // HTMlColorPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 692);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "HTMlColorPicker";
            this.Text = "HTMlColorPicker";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbColors;
        private System.Windows.Forms.ListBox lbInvCol;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbOut;
        private System.Windows.Forms.Button btnApply;
    }
}