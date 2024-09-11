namespace MacroEditor
{
    partial class CreateMacro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateMacro));
            this.btnCencel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClrIL = new System.Windows.Forms.Button();
            this.btnBrowseImg = new System.Windows.Forms.Button();
            this.btnFormRemote = new System.Windows.Forms.Button();
            this.tbUrlText = new System.Windows.Forms.TextBox();
            this.btnPaste = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.cbFormBorder = new System.Windows.Forms.CheckBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.tcImaging = new System.Windows.Forms.TabControl();
            this.tpImage = new System.Windows.Forms.TabPage();
            this.gbNotServer = new System.Windows.Forms.GroupBox();
            this.btnApplySlider = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.btnResetAspect = new System.Windows.Forms.Button();
            this.gbImSizeServer = new System.Windows.Forms.GroupBox();
            this.lbH = new System.Windows.Forms.Label();
            this.lbW = new System.Windows.Forms.Label();
            this.tbW = new System.Windows.Forms.TextBox();
            this.cbImgSize = new System.Windows.Forms.ComboBox();
            this.tbH = new System.Windows.Forms.TextBox();
            this.tpVideo = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.cbVSize = new System.Windows.Forms.ComboBox();
            this.cbVBorder = new System.Windows.Forms.CheckBox();
            this.btnVApply = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbVTitle = new System.Windows.Forms.TextBox();
            this.axWMP = new AxWMPLib.AxWindowsMediaPlayer();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbHeight = new System.Windows.Forms.TextBox();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbDNU = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tcImaging.SuspendLayout();
            this.tpImage.SuspendLayout();
            this.gbNotServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.gbImSizeServer.SuspendLayout();
            this.tpVideo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWMP)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCencel
            // 
            this.btnCencel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCencel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCencel.Location = new System.Drawing.Point(24, 498);
            this.btnCencel.Name = "btnCencel";
            this.btnCencel.Size = new System.Drawing.Size(122, 36);
            this.btnCencel.TabIndex = 9;
            this.btnCencel.Text = "Cancel and exit";
            this.btnCencel.UseVisualStyleBackColor = true;
            this.btnCencel.Click += new System.EventHandler(this.btnCencel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnApply.Location = new System.Drawing.Point(24, 431);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(122, 36);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "Apply and exit";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // pbImage
            // 
            this.pbImage.Image = ((System.Drawing.Image)(resources.GetObject("pbImage.Image")));
            this.pbImage.Location = new System.Drawing.Point(30, 17);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(568, 433);
            this.pbImage.TabIndex = 7;
            this.pbImage.TabStop = false;
            this.pbImage.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pbImage_LoadCompleted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClrIL);
            this.groupBox1.Controls.Add(this.btnBrowseImg);
            this.groupBox1.Controls.Add(this.btnFormRemote);
            this.groupBox1.Controls.Add(this.tbUrlText);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(24, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 198);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Image Location";
            // 
            // btnClrIL
            // 
            this.btnClrIL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClrIL.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnClrIL.Location = new System.Drawing.Point(235, 102);
            this.btnClrIL.Name = "btnClrIL";
            this.btnClrIL.Size = new System.Drawing.Size(79, 36);
            this.btnClrIL.TabIndex = 11;
            this.btnClrIL.Text = "CLEAR";
            this.btnClrIL.UseVisualStyleBackColor = true;
            this.btnClrIL.Click += new System.EventHandler(this.btnClrIL_Click);
            // 
            // btnBrowseImg
            // 
            this.btnBrowseImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseImg.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnBrowseImg.Location = new System.Drawing.Point(15, 76);
            this.btnBrowseImg.Name = "btnBrowseImg";
            this.btnBrowseImg.Size = new System.Drawing.Size(163, 36);
            this.btnBrowseImg.TabIndex = 5;
            this.btnBrowseImg.Text = "Browse for image";
            this.btnBrowseImg.UseVisualStyleBackColor = true;
            this.btnBrowseImg.Click += new System.EventHandler(this.btnBrowseImg_Click);
            // 
            // btnFormRemote
            // 
            this.btnFormRemote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFormRemote.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnFormRemote.Location = new System.Drawing.Point(15, 130);
            this.btnFormRemote.Name = "btnFormRemote";
            this.btnFormRemote.Size = new System.Drawing.Size(163, 36);
            this.btnFormRemote.TabIndex = 4;
            this.btnFormRemote.Text = "Use Above Image";
            this.btnFormRemote.UseVisualStyleBackColor = true;
            this.btnFormRemote.Click += new System.EventHandler(this.btnFormRemote_Click);
            // 
            // tbUrlText
            // 
            this.tbUrlText.Location = new System.Drawing.Point(15, 39);
            this.tbUrlText.Name = "tbUrlText";
            this.tbUrlText.Size = new System.Drawing.Size(343, 22);
            this.tbUrlText.TabIndex = 0;
            // 
            // btnPaste
            // 
            this.btnPaste.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPaste.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnPaste.Location = new System.Drawing.Point(165, 226);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(227, 36);
            this.btnPaste.TabIndex = 5;
            this.btnPaste.Text = "Paste Image From Clipboard";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnClear.Location = new System.Drawing.Point(24, 343);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(122, 36);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ofd
            // 
            this.ofd.FileName = "openFileDialog1";
            // 
            // cbFormBorder
            // 
            this.cbFormBorder.AutoSize = true;
            this.cbFormBorder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFormBorder.ForeColor = System.Drawing.SystemColors.Highlight;
            this.cbFormBorder.Location = new System.Drawing.Point(236, 447);
            this.cbFormBorder.Name = "cbFormBorder";
            this.cbFormBorder.Size = new System.Drawing.Size(91, 20);
            this.cbFormBorder.TabIndex = 11;
            this.cbFormBorder.Text = "Form Box";
            this.cbFormBorder.UseVisualStyleBackColor = true;
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTest.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnTest.Location = new System.Drawing.Point(305, 290);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(87, 36);
            this.btnTest.TabIndex = 15;
            this.btnTest.Text = "TEST";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // tcImaging
            // 
            this.tcImaging.Controls.Add(this.tpImage);
            this.tcImaging.Controls.Add(this.tpVideo);
            this.tcImaging.Location = new System.Drawing.Point(507, 12);
            this.tcImaging.Name = "tcImaging";
            this.tcImaging.SelectedIndex = 0;
            this.tcImaging.Size = new System.Drawing.Size(632, 642);
            this.tcImaging.TabIndex = 18;
            // 
            // tpImage
            // 
            this.tpImage.Controls.Add(this.gbNotServer);
            this.tpImage.Controls.Add(this.gbImSizeServer);
            this.tpImage.Controls.Add(this.pbImage);
            this.tpImage.Location = new System.Drawing.Point(4, 22);
            this.tpImage.Name = "tpImage";
            this.tpImage.Padding = new System.Windows.Forms.Padding(3);
            this.tpImage.Size = new System.Drawing.Size(624, 616);
            this.tpImage.TabIndex = 0;
            this.tpImage.Text = "Image";
            this.tpImage.UseVisualStyleBackColor = true;
            // 
            // gbNotServer
            // 
            this.gbNotServer.Controls.Add(this.btnApplySlider);
            this.gbNotServer.Controls.Add(this.trackBar1);
            this.gbNotServer.Controls.Add(this.btnResetAspect);
            this.gbNotServer.Location = new System.Drawing.Point(30, 471);
            this.gbNotServer.Name = "gbNotServer";
            this.gbNotServer.Size = new System.Drawing.Size(342, 124);
            this.gbNotServer.TabIndex = 26;
            this.gbNotServer.TabStop = false;
            this.gbNotServer.Text = "Image Size";
            // 
            // btnApplySlider
            // 
            this.btnApplySlider.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplySlider.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnApplySlider.Location = new System.Drawing.Point(181, 18);
            this.btnApplySlider.Name = "btnApplySlider";
            this.btnApplySlider.Size = new System.Drawing.Size(74, 29);
            this.btnApplySlider.TabIndex = 27;
            this.btnApplySlider.Text = "Apply";
            this.btnApplySlider.UseVisualStyleBackColor = true;
            this.btnApplySlider.Click += new System.EventHandler(this.btnApplySlider_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(46, 55);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(260, 45);
            this.trackBar1.TabIndex = 26;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // btnResetAspect
            // 
            this.btnResetAspect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetAspect.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnResetAspect.Location = new System.Drawing.Point(82, 18);
            this.btnResetAspect.Name = "btnResetAspect";
            this.btnResetAspect.Size = new System.Drawing.Size(74, 29);
            this.btnResetAspect.TabIndex = 25;
            this.btnResetAspect.Text = "Reset";
            this.btnResetAspect.UseVisualStyleBackColor = true;
            this.btnResetAspect.Click += new System.EventHandler(this.btnResetAspect_Click);
            // 
            // gbImSizeServer
            // 
            this.gbImSizeServer.Controls.Add(this.lbH);
            this.gbImSizeServer.Controls.Add(this.lbW);
            this.gbImSizeServer.Controls.Add(this.tbW);
            this.gbImSizeServer.Controls.Add(this.cbImgSize);
            this.gbImSizeServer.Controls.Add(this.tbH);
            this.gbImSizeServer.Location = new System.Drawing.Point(422, 464);
            this.gbImSizeServer.Name = "gbImSizeServer";
            this.gbImSizeServer.Size = new System.Drawing.Size(176, 131);
            this.gbImSizeServer.TabIndex = 24;
            this.gbImSizeServer.TabStop = false;
            this.gbImSizeServer.Text = "Image Size";
            // 
            // lbH
            // 
            this.lbH.AutoSize = true;
            this.lbH.BackColor = System.Drawing.SystemColors.Info;
            this.lbH.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbH.Location = new System.Drawing.Point(34, 91);
            this.lbH.Name = "lbH";
            this.lbH.Size = new System.Drawing.Size(46, 16);
            this.lbH.TabIndex = 25;
            this.lbH.Text = "Height";
            // 
            // lbW
            // 
            this.lbW.AutoSize = true;
            this.lbW.BackColor = System.Drawing.SystemColors.Info;
            this.lbW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbW.Location = new System.Drawing.Point(34, 59);
            this.lbW.Name = "lbW";
            this.lbW.Size = new System.Drawing.Size(41, 16);
            this.lbW.TabIndex = 24;
            this.lbW.Text = "Width";
            // 
            // tbW
            // 
            this.tbW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbW.Location = new System.Drawing.Point(95, 53);
            this.tbW.Name = "tbW";
            this.tbW.Size = new System.Drawing.Size(61, 22);
            this.tbW.TabIndex = 22;
            this.tbW.Text = "  ";
            // 
            // cbImgSize
            // 
            this.cbImgSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbImgSize.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.cbImgSize.FormattingEnabled = true;
            this.cbImgSize.Items.AddRange(new object[] {
            "default",
            "tiny",
            "thumb",
            "small",
            "medium",
            "large"});
            this.cbImgSize.Location = new System.Drawing.Point(71, 19);
            this.cbImgSize.Name = "cbImgSize";
            this.cbImgSize.Size = new System.Drawing.Size(85, 21);
            this.cbImgSize.TabIndex = 21;
            this.cbImgSize.Visible = false;
            // 
            // tbH
            // 
            this.tbH.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbH.Location = new System.Drawing.Point(95, 88);
            this.tbH.Name = "tbH";
            this.tbH.Size = new System.Drawing.Size(61, 22);
            this.tbH.TabIndex = 23;
            // 
            // tpVideo
            // 
            this.tpVideo.Controls.Add(this.label5);
            this.tpVideo.Controls.Add(this.cbVSize);
            this.tpVideo.Controls.Add(this.cbVBorder);
            this.tpVideo.Controls.Add(this.btnVApply);
            this.tpVideo.Controls.Add(this.label3);
            this.tpVideo.Controls.Add(this.tbVTitle);
            this.tpVideo.Controls.Add(this.axWMP);
            this.tpVideo.Location = new System.Drawing.Point(4, 22);
            this.tpVideo.Name = "tpVideo";
            this.tpVideo.Padding = new System.Windows.Forms.Padding(3);
            this.tpVideo.Size = new System.Drawing.Size(624, 616);
            this.tpVideo.TabIndex = 1;
            this.tpVideo.Text = "Video";
            this.tpVideo.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Info;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(454, 427);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 64);
            this.label5.TabIndex = 27;
            this.label5.Text = "Size of video or\r\nselect EDIT to set\r\nfrom Width/Height\r\nlist on main page";
            // 
            // cbVSize
            // 
            this.cbVSize.FormattingEnabled = true;
            this.cbVSize.Items.AddRange(new object[] {
            "Default",
            "Edited",
            "320x240",
            "640x480"});
            this.cbVSize.Location = new System.Drawing.Point(457, 512);
            this.cbVSize.Name = "cbVSize";
            this.cbVSize.Size = new System.Drawing.Size(92, 21);
            this.cbVSize.TabIndex = 26;
            // 
            // cbVBorder
            // 
            this.cbVBorder.AutoSize = true;
            this.cbVBorder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbVBorder.ForeColor = System.Drawing.SystemColors.Highlight;
            this.cbVBorder.Location = new System.Drawing.Point(27, 545);
            this.cbVBorder.Name = "cbVBorder";
            this.cbVBorder.Size = new System.Drawing.Size(73, 20);
            this.cbVBorder.TabIndex = 25;
            this.cbVBorder.Text = "Border";
            this.cbVBorder.UseVisualStyleBackColor = true;
            // 
            // btnVApply
            // 
            this.btnVApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVApply.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnVApply.Location = new System.Drawing.Point(301, 531);
            this.btnVApply.Name = "btnVApply";
            this.btnVApply.Size = new System.Drawing.Size(82, 34);
            this.btnVApply.TabIndex = 24;
            this.btnVApply.Text = "Apply";
            this.btnVApply.UseVisualStyleBackColor = true;
            this.btnVApply.Click += new System.EventHandler(this.btnVApply_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Info;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 443);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 32);
            this.label3.TabIndex = 19;
            this.label3.Text = "Video Title (or none)\r\nPlaying sets video size";
            // 
            // tbVTitle
            // 
            this.tbVTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVTitle.Location = new System.Drawing.Point(18, 489);
            this.tbVTitle.Name = "tbVTitle";
            this.tbVTitle.Size = new System.Drawing.Size(228, 22);
            this.tbVTitle.TabIndex = 18;
            // 
            // axWMP
            // 
            this.axWMP.Enabled = true;
            this.axWMP.Location = new System.Drawing.Point(27, 17);
            this.axWMP.Name = "axWMP";
            this.axWMP.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWMP.OcxState")));
            this.axWMP.Size = new System.Drawing.Size(577, 402);
            this.axWMP.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Info;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 16);
            this.label2.TabIndex = 23;
            this.label2.Text = "Height";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 16);
            this.label1.TabIndex = 22;
            this.label1.Text = "Width";
            // 
            // tbHeight
            // 
            this.tbHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHeight.Location = new System.Drawing.Point(79, 60);
            this.tbHeight.Name = "tbHeight";
            this.tbHeight.ReadOnly = true;
            this.tbHeight.Size = new System.Drawing.Size(61, 22);
            this.tbHeight.TabIndex = 21;
            // 
            // tbWidth
            // 
            this.tbWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbWidth.Location = new System.Drawing.Point(79, 25);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.ReadOnly = true;
            this.tbWidth.Size = new System.Drawing.Size(61, 22);
            this.tbWidth.TabIndex = 20;
            this.tbWidth.Text = "  ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbDNU);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tbWidth);
            this.groupBox2.Controls.Add(this.tbHeight);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(187, 523);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(268, 100);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Video/Image size";
            // 
            // cbDNU
            // 
            this.cbDNU.AutoSize = true;
            this.cbDNU.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDNU.ForeColor = System.Drawing.SystemColors.Highlight;
            this.cbDNU.Location = new System.Drawing.Point(158, 37);
            this.cbDNU.Name = "cbDNU";
            this.cbDNU.Size = new System.Drawing.Size(92, 17);
            this.cbDNU.TabIndex = 24;
            this.cbDNU.Text = "Do Not Use";
            this.cbDNU.UseVisualStyleBackColor = true;
            // 
            // CreateMacro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 666);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tcImaging);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.cbFormBorder);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.btnCencel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateMacro";
            this.Text = "CreateMacro";
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tcImaging.ResumeLayout(false);
            this.tpImage.ResumeLayout(false);
            this.gbNotServer.ResumeLayout(false);
            this.gbNotServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.gbImSizeServer.ResumeLayout(false);
            this.gbImSizeServer.PerformLayout();
            this.tpVideo.ResumeLayout(false);
            this.tpVideo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWMP)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCencel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.Button btnFormRemote;
        private System.Windows.Forms.TextBox tbUrlText;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnBrowseImg;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.CheckBox cbFormBorder;
        private System.Windows.Forms.Button btnTest;
        private AxWMPLib.AxWindowsMediaPlayer axWMP;
        private System.Windows.Forms.TabControl tcImaging;
        private System.Windows.Forms.TabPage tpImage;
        private System.Windows.Forms.TabPage tpVideo;
        private System.Windows.Forms.TextBox tbVTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbHeight;
        private System.Windows.Forms.TextBox tbWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnVApply;
        private System.Windows.Forms.Button btnClrIL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbVSize;
        private System.Windows.Forms.CheckBox cbVBorder;
        private System.Windows.Forms.ComboBox cbImgSize;
        private System.Windows.Forms.GroupBox gbImSizeServer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox gbNotServer;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button btnResetAspect;
        private System.Windows.Forms.TextBox tbW;
        private System.Windows.Forms.TextBox tbH;
        private System.Windows.Forms.Label lbH;
        private System.Windows.Forms.Label lbW;
        private System.Windows.Forms.Button btnApplySlider;
        private System.Windows.Forms.CheckBox cbDNU;
    }
}