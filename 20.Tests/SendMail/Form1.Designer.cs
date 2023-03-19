namespace SendMail
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
            this.txtSender = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRecipients = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBody = new System.Windows.Forms.TextBox();
            this.cmdSend = new System.Windows.Forms.Button();
            this.chkEnableCredential = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPortNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkSSL = new System.Windows.Forms.CheckBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmdYahoo = new System.Windows.Forms.Button();
            this.cmdGmail = new System.Windows.Forms.Button();
            this.cmdOutlook = new System.Windows.Forms.Button();
            this.cmdToray = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sender:";
            // 
            // txtSender
            // 
            this.txtSender.Location = new System.Drawing.Point(12, 191);
            this.txtSender.Name = "txtSender";
            this.txtSender.Size = new System.Drawing.Size(285, 22);
            this.txtSender.TabIndex = 6;
            this.txtSender.Text = "noreply@example.com";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Recipient(s):";
            // 
            // txtRecipients
            // 
            this.txtRecipients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRecipients.Location = new System.Drawing.Point(12, 244);
            this.txtRecipients.Multiline = true;
            this.txtRecipients.Name = "txtRecipients";
            this.txtRecipients.Size = new System.Drawing.Size(771, 63);
            this.txtRecipients.TabIndex = 7;
            this.txtRecipients.Text = "chumponsenate@outlook.com, chumponsenate@yahoo.com, chumponsenate@gmail.com";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 369);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Mail Body:";
            // 
            // txtBody
            // 
            this.txtBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBody.Location = new System.Drawing.Point(13, 391);
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            this.txtBody.Size = new System.Drawing.Size(771, 163);
            this.txtBody.TabIndex = 9;
            this.txtBody.Text = "Hello World";
            // 
            // cmdSend
            // 
            this.cmdSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSend.Location = new System.Drawing.Point(660, 568);
            this.cmdSend.Name = "cmdSend";
            this.cmdSend.Size = new System.Drawing.Size(124, 23);
            this.cmdSend.TabIndex = 10;
            this.cmdSend.Text = "Send";
            this.cmdSend.UseVisualStyleBackColor = true;
            this.cmdSend.Click += new System.EventHandler(this.cmdSend_Click);
            // 
            // chkEnableCredential
            // 
            this.chkEnableCredential.AutoSize = true;
            this.chkEnableCredential.Location = new System.Drawing.Point(12, 60);
            this.chkEnableCredential.Name = "chkEnableCredential";
            this.chkEnableCredential.Size = new System.Drawing.Size(118, 20);
            this.chkEnableCredential.TabIndex = 2;
            this.chkEnableCredential.Text = "Has Credential";
            this.chkEnableCredential.UseVisualStyleBackColor = true;
            this.chkEnableCredential.CheckedChanged += new System.EventHandler(this.chkEnableCredential_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(182, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(182, 106);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(163, 22);
            this.txtPassword.TabIndex = 4;
            // 
            // txtUserName
            // 
            this.txtUserName.Enabled = false;
            this.txtUserName.Location = new System.Drawing.Point(13, 106);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(163, 22);
            this.txtUserName.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "User Name:";
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(13, 30);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(165, 22);
            this.txtServerName.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "SMTP Server (Host/IP):";
            // 
            // txtPortNo
            // 
            this.txtPortNo.Location = new System.Drawing.Point(184, 30);
            this.txtPortNo.Name = "txtPortNo";
            this.txtPortNo.Size = new System.Drawing.Size(70, 22);
            this.txtPortNo.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(181, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "Port:";
            // 
            // chkSSL
            // 
            this.chkSSL.AutoSize = true;
            this.chkSSL.Location = new System.Drawing.Point(13, 140);
            this.chkSSL.Name = "chkSSL";
            this.chkSSL.Size = new System.Drawing.Size(82, 20);
            this.chkSSL.TabIndex = 5;
            this.chkSSL.Text = "Use SSL";
            this.chkSSL.UseVisualStyleBackColor = true;
            // 
            // txtTitle
            // 
            this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitle.Location = new System.Drawing.Point(12, 337);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(771, 22);
            this.txtTitle.TabIndex = 8;
            this.txtTitle.Text = "Example mail";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 316);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 16);
            this.label8.TabIndex = 19;
            this.label8.Text = "Mail Title:";
            // 
            // cmdYahoo
            // 
            this.cmdYahoo.Location = new System.Drawing.Point(270, 28);
            this.cmdYahoo.Name = "cmdYahoo";
            this.cmdYahoo.Size = new System.Drawing.Size(75, 23);
            this.cmdYahoo.TabIndex = 20;
            this.cmdYahoo.TabStop = false;
            this.cmdYahoo.Text = "Yahoo";
            this.cmdYahoo.UseVisualStyleBackColor = true;
            this.cmdYahoo.Click += new System.EventHandler(this.cmdYahoo_Click);
            // 
            // cmdGmail
            // 
            this.cmdGmail.Location = new System.Drawing.Point(351, 28);
            this.cmdGmail.Name = "cmdGmail";
            this.cmdGmail.Size = new System.Drawing.Size(75, 23);
            this.cmdGmail.TabIndex = 21;
            this.cmdGmail.TabStop = false;
            this.cmdGmail.Text = "Gmail";
            this.cmdGmail.UseVisualStyleBackColor = true;
            this.cmdGmail.Click += new System.EventHandler(this.cmdGmail_Click);
            // 
            // cmdOutlook
            // 
            this.cmdOutlook.Location = new System.Drawing.Point(432, 28);
            this.cmdOutlook.Name = "cmdOutlook";
            this.cmdOutlook.Size = new System.Drawing.Size(75, 23);
            this.cmdOutlook.TabIndex = 22;
            this.cmdOutlook.TabStop = false;
            this.cmdOutlook.Text = "Outlook";
            this.cmdOutlook.UseVisualStyleBackColor = true;
            this.cmdOutlook.Click += new System.EventHandler(this.cmdOutlook_Click);
            // 
            // cmdToray
            // 
            this.cmdToray.Location = new System.Drawing.Point(513, 28);
            this.cmdToray.Name = "cmdToray";
            this.cmdToray.Size = new System.Drawing.Size(75, 23);
            this.cmdToray.TabIndex = 23;
            this.cmdToray.TabStop = false;
            this.cmdToray.Text = "Toray";
            this.cmdToray.UseVisualStyleBackColor = true;
            this.cmdToray.Click += new System.EventHandler(this.cmdToray_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 609);
            this.Controls.Add(this.cmdToray);
            this.Controls.Add(this.cmdOutlook);
            this.Controls.Add(this.cmdGmail);
            this.Controls.Add(this.cmdYahoo);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.chkSSL);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPortNo);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkEnableCredential);
            this.Controls.Add(this.cmdSend);
            this.Controls.Add(this.txtBody);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtRecipients);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSender);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Send Mail Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSender;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRecipients;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBody;
        private System.Windows.Forms.Button cmdSend;
        private System.Windows.Forms.CheckBox chkEnableCredential;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPortNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkSSL;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button cmdYahoo;
        private System.Windows.Forms.Button cmdGmail;
        private System.Windows.Forms.Button cmdOutlook;
        private System.Windows.Forms.Button cmdToray;
    }
}

