namespace MacroEditor.sources
{
    partial class DisplayText
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
            this.tbEdit = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbEdit
            // 
            this.tbEdit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbEdit.Location = new System.Drawing.Point(12, 30);
            this.tbEdit.Multiline = true;
            this.tbEdit.Name = "tbEdit";
            this.tbEdit.Size = new System.Drawing.Size(754, 549);
            this.tbEdit.TabIndex = 0;
            // 
            // DisplayText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 591);
            this.Controls.Add(this.tbEdit);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DisplayText";
            this.Text = "DisplayText";
            this.SizeChanged += new System.EventHandler(this.DisplayText_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbEdit;
    }
}