namespace MacroEditor
{
    partial class MoveMacro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveMacro));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTotalCnt = new System.Windows.Forms.TextBox();
            this.btnMove = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.tbNFree = new System.Windows.Forms.TextBox();
            this.tbNumMoving = new System.Windows.Forms.TextBox();
            this.gbTo = new System.Windows.Forms.GroupBox();
            this.gbFrom = new System.Windows.Forms.GroupBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.gbTo);
            this.groupBox1.Controls.Add(this.gbFrom);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(25, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(935, 541);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Move macros to another file";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCopy);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tbTotalCnt);
            this.groupBox2.Controls.Add(this.btnMove);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnExit);
            this.groupBox2.Controls.Add(this.tbNFree);
            this.groupBox2.Controls.Add(this.tbNumMoving);
            this.groupBox2.Location = new System.Drawing.Point(609, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(293, 415);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Action";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Info;
            this.label3.Location = new System.Drawing.Point(17, 361);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Total Macro Count";
            // 
            // tbTotalCnt
            // 
            this.tbTotalCnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTotalCnt.Location = new System.Drawing.Point(155, 358);
            this.tbTotalCnt.Name = "tbTotalCnt";
            this.tbTotalCnt.ReadOnly = true;
            this.tbTotalCnt.Size = new System.Drawing.Size(113, 22);
            this.tbTotalCnt.TabIndex = 7;
            // 
            // btnMove
            // 
            this.btnMove.Enabled = false;
            this.btnMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMove.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnMove.Location = new System.Drawing.Point(20, 207);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(112, 37);
            this.btnMove.TabIndex = 4;
            this.btnMove.Text = "Move Them";
            this.btnMove.UseVisualStyleBackColor = true;
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Info;
            this.label2.Location = new System.Drawing.Point(20, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Space Available";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Location = new System.Drawing.Point(17, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Number Checked";
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnExit.Location = new System.Drawing.Point(20, 156);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 32);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Return";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tbNFree
            // 
            this.tbNFree.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNFree.Location = new System.Drawing.Point(155, 88);
            this.tbNFree.Name = "tbNFree";
            this.tbNFree.ReadOnly = true;
            this.tbNFree.Size = new System.Drawing.Size(69, 22);
            this.tbNFree.TabIndex = 1;
            // 
            // tbNumMoving
            // 
            this.tbNumMoving.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNumMoving.Location = new System.Drawing.Point(155, 33);
            this.tbNumMoving.Name = "tbNumMoving";
            this.tbNumMoving.ReadOnly = true;
            this.tbNumMoving.Size = new System.Drawing.Size(69, 22);
            this.tbNumMoving.TabIndex = 0;
            // 
            // gbTo
            // 
            this.gbTo.Location = new System.Drawing.Point(287, 56);
            this.gbTo.Name = "gbTo";
            this.gbTo.Size = new System.Drawing.Size(279, 447);
            this.gbTo.TabIndex = 1;
            this.gbTo.TabStop = false;
            this.gbTo.Text = "To";
            // 
            // gbFrom
            // 
            this.gbFrom.Location = new System.Drawing.Point(28, 56);
            this.gbFrom.Name = "gbFrom";
            this.gbFrom.Size = new System.Drawing.Size(227, 447);
            this.gbFrom.TabIndex = 0;
            this.gbFrom.TabStop = false;
            this.gbFrom.Text = "From";
            // 
            // btnCopy
            // 
            this.btnCopy.Enabled = false;
            this.btnCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopy.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCopy.Location = new System.Drawing.Point(23, 268);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(112, 37);
            this.btnCopy.TabIndex = 9;
            this.btnCopy.Text = "Copy Them";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // MoveMacro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 592);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MoveMacro";
            this.Text = "MoveMacro";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbFrom;
        private System.Windows.Forms.GroupBox gbTo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbNumMoving;
        private System.Windows.Forms.Button btnMove;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox tbNFree;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbTotalCnt;
        private System.Windows.Forms.Button btnCopy;
    }
}