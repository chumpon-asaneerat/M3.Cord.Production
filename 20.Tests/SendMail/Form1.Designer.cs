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
            this.rbMailTo = new System.Windows.Forms.RadioButton();
            this.rbSMTP = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSender = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRecipients = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBody = new System.Windows.Forms.TextBox();
            this.cmdSend = new System.Windows.Forms.Button();
            this.chkEnableCredential = new System.Windows.Forms.CheckBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // rbMailTo
            // 
            this.rbMailTo.AutoSize = true;
            this.rbMailTo.Checked = true;
            this.rbMailTo.Location = new System.Drawing.Point(12, 12);
            this.rbMailTo.Name = "rbMailTo";
            this.rbMailTo.Size = new System.Drawing.Size(92, 20);
            this.rbMailTo.TabIndex = 0;
            this.rbMailTo.TabStop = true;
            this.rbMailTo.Text = "Use mailto";
            this.rbMailTo.UseVisualStyleBackColor = true;
            // 
            // rbSMTP
            // 
            this.rbSMTP.AutoSize = true;
            this.rbSMTP.Location = new System.Drawing.Point(141, 12);
            this.rbSMTP.Name = "rbSMTP";
            this.rbSMTP.Size = new System.Drawing.Size(130, 20);
            this.rbSMTP.TabIndex = 1;
            this.rbSMTP.Text = "Use SMTP Client";
            this.rbSMTP.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sender:";
            // 
            // txtSender
            // 
            this.txtSender.Location = new System.Drawing.Point(12, 68);
            this.txtSender.Name = "txtSender";
            this.txtSender.Size = new System.Drawing.Size(285, 22);
            this.txtSender.TabIndex = 3;
            this.txtSender.Text = "noreply@example.com";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Recipient(s):";
            // 
            // txtRecipients
            // 
            this.txtRecipients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRecipients.Location = new System.Drawing.Point(12, 202);
            this.txtRecipients.Multiline = true;
            this.txtRecipients.Name = "txtRecipients";
            this.txtRecipients.Size = new System.Drawing.Size(776, 90);
            this.txtRecipients.TabIndex = 5;
            this.txtRecipients.Text = "chumponsenate@outlook.com, chumponsenate@yahoo.com, chumponsenate@gmail.com";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 300);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Mail Body:";
            // 
            // txtBody
            // 
            this.txtBody.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBody.Location = new System.Drawing.Point(15, 322);
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            this.txtBody.Size = new System.Drawing.Size(776, 159);
            this.txtBody.TabIndex = 7;
            this.txtBody.Text = "Hello World";
            // 
            // cmdSend
            // 
            this.cmdSend.Location = new System.Drawing.Point(664, 501);
            this.cmdSend.Name = "cmdSend";
            this.cmdSend.Size = new System.Drawing.Size(124, 23);
            this.cmdSend.TabIndex = 8;
            this.cmdSend.Text = "Send";
            this.cmdSend.UseVisualStyleBackColor = true;
            this.cmdSend.Click += new System.EventHandler(this.cmdSend_Click);
            // 
            // chkEnableCredential
            // 
            this.chkEnableCredential.AutoSize = true;
            this.chkEnableCredential.Location = new System.Drawing.Point(12, 105);
            this.chkEnableCredential.Name = "chkEnableCredential";
            this.chkEnableCredential.Size = new System.Drawing.Size(118, 20);
            this.chkEnableCredential.TabIndex = 9;
            this.chkEnableCredential.Text = "Has Credential";
            this.chkEnableCredential.UseVisualStyleBackColor = true;
            this.chkEnableCredential.CheckedChanged += new System.EventHandler(this.chkEnableCredential_CheckedChanged);
            // 
            // txtUserName
            // 
            this.txtUserName.Enabled = false;
            this.txtUserName.Location = new System.Drawing.Point(12, 151);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(163, 22);
            this.txtUserName.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "User Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(181, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(181, 151);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(163, 22);
            this.txtPassword.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 536);
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
            this.Controls.Add(this.rbSMTP);
            this.Controls.Add(this.rbMailTo);
            this.Name = "Form1";
            this.Text = "Send Mail Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbMailTo;
        private System.Windows.Forms.RadioButton rbSMTP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSender;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRecipients;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBody;
        private System.Windows.Forms.Button cmdSend;
        private System.Windows.Forms.CheckBox chkEnableCredential;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPassword;
    }
}

