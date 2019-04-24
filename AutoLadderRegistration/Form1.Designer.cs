namespace AutoLadderRegistration
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.txt_pw = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_chars = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.chk_autoLogin = new System.Windows.Forms.CheckBox();
            this.nud_ladderCount = new System.Windows.Forms.NumericUpDown();
            this.chk_regCount = new System.Windows.Forms.CheckBox();
            this.lbl_ladderEntryCount = new System.Windows.Forms.Label();
            this.lbl_status = new System.Windows.Forms.Label();
            this.rtb_log = new System.Windows.Forms.RichTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.cmb_server = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ladderCount)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password:";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(243, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // txt_username
            // 
            this.txt_username.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txt_username.Location = new System.Drawing.Point(69, 40);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(168, 20);
            this.txt_username.TabIndex = 3;
            this.txt_username.Text = "";
            // 
            // txt_pw
            // 
            this.txt_pw.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txt_pw.Location = new System.Drawing.Point(69, 64);
            this.txt_pw.Name = "txt_pw";
            this.txt_pw.Size = new System.Drawing.Size(168, 20);
            this.txt_pw.TabIndex = 4;
            this.txt_pw.Text = "";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Character:";
            // 
            // cmb_chars
            // 
            this.cmb_chars.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmb_chars.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_chars.FormattingEnabled = true;
            this.cmb_chars.Location = new System.Drawing.Point(68, 90);
            this.cmb_chars.Name = "cmb_chars";
            this.cmb_chars.Size = new System.Drawing.Size(169, 21);
            this.cmb_chars.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(243, 89);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Select";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // chk_autoLogin
            // 
            this.chk_autoLogin.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.chk_autoLogin.AutoSize = true;
            this.chk_autoLogin.Location = new System.Drawing.Point(14, 123);
            this.chk_autoLogin.Name = "chk_autoLogin";
            this.chk_autoLogin.Size = new System.Drawing.Size(73, 17);
            this.chk_autoLogin.TabIndex = 8;
            this.chk_autoLogin.Text = "Auto login";
            this.chk_autoLogin.UseVisualStyleBackColor = true;
            // 
            // nud_ladderCount
            // 
            this.nud_ladderCount.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.nud_ladderCount.Location = new System.Drawing.Point(243, 120);
            this.nud_ladderCount.Name = "nud_ladderCount";
            this.nud_ladderCount.Size = new System.Drawing.Size(75, 20);
            this.nud_ladderCount.TabIndex = 9;
            this.nud_ladderCount.ValueChanged += new System.EventHandler(this.nud_ladderCount_ValueChanged);
            // 
            // chk_regCount
            // 
            this.chk_regCount.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.chk_regCount.AutoSize = true;
            this.chk_regCount.Location = new System.Drawing.Point(161, 121);
            this.chk_regCount.Name = "chk_regCount";
            this.chk_regCount.Size = new System.Drawing.Size(76, 17);
            this.chk_regCount.TabIndex = 10;
            this.chk_regCount.Text = "Reg count";
            this.chk_regCount.UseVisualStyleBackColor = true;
            // 
            // lbl_ladderEntryCount
            // 
            this.lbl_ladderEntryCount.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lbl_ladderEntryCount.AutoSize = true;
            this.lbl_ladderEntryCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ladderEntryCount.Location = new System.Drawing.Point(155, 148);
            this.lbl_ladderEntryCount.Name = "lbl_ladderEntryCount";
            this.lbl_ladderEntryCount.Size = new System.Drawing.Size(66, 31);
            this.lbl_ladderEntryCount.TabIndex = 11;
            this.lbl_ladderEntryCount.Text = "0 / 0";
            // 
            // lbl_status
            // 
            this.lbl_status.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lbl_status.AutoSize = true;
            this.lbl_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_status.Location = new System.Drawing.Point(11, 148);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(116, 31);
            this.lbl_status.TabIndex = 12;
            this.lbl_status.Text = "Status...";
            // 
            // rtb_log
            // 
            this.rtb_log.BackColor = System.Drawing.SystemColors.Info;
            this.rtb_log.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtb_log.Location = new System.Drawing.Point(0, 187);
            this.rtb_log.Name = "rtb_log";
            this.rtb_log.ReadOnly = true;
            this.rtb_log.Size = new System.Drawing.Size(331, 129);
            this.rtb_log.TabIndex = 13;
            this.rtb_log.Text = "";
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button3.Location = new System.Drawing.Point(244, 10);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = "Connect";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // cmb_server
            // 
            this.cmb_server.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmb_server.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_server.FormattingEnabled = true;
            this.cmb_server.Items.AddRange(new object[] {
            "211.43.158.240:14300",
            "211.43.158.241:14300",
            "211.43.158.242:14300",
            "211.43.158.243:14300",
            "211.43.158.244:14300",
            "211.43.158.245:14300"});
            this.cmb_server.Location = new System.Drawing.Point(69, 11);
            this.cmb_server.Name = "cmb_server";
            this.cmb_server.Size = new System.Drawing.Size(169, 21);
            this.cmb_server.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Server:";
            // 
            // button4
            // 
            this.button4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button4.Location = new System.Drawing.Point(243, 156);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 17;
            this.button4.Text = "Start";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 316);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.cmb_server);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rtb_log);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.lbl_ladderEntryCount);
            this.Controls.Add(this.chk_regCount);
            this.Controls.Add(this.nud_ladderCount);
            this.Controls.Add(this.chk_autoLogin);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.cmb_chars);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_pw);
            this.Controls.Add(this.txt_username);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Auto DN Ladder Registration";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nud_ladderCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox txt_username;
        public System.Windows.Forms.TextBox txt_pw;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cmb_chars;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.CheckBox chk_autoLogin;
        public System.Windows.Forms.NumericUpDown nud_ladderCount;
        public System.Windows.Forms.CheckBox chk_regCount;
        public System.Windows.Forms.Label lbl_ladderEntryCount;
        public System.Windows.Forms.Label lbl_status;
        public System.Windows.Forms.RichTextBox rtb_log;
        public System.Windows.Forms.Button button3;
        public System.Windows.Forms.ComboBox cmb_server;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Button button4;
    }
}

