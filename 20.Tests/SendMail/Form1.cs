#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace SendMail
{
    public partial class Form1 : Form
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region Checkbox Handlers

        private void chkEnableCredential_CheckedChanged(object sender, EventArgs e)
        {
            txtUserName.Enabled = chkEnableCredential.Checked;
            txtPassword.Enabled = chkEnableCredential.Checked;
        }

        #endregion

        #region Button Handlers

        private void cmdYahoo_Click(object sender, EventArgs e)
        {
            txtServerName.Text = "smtp.mail.yahoo.com";
            txtPortNo.Text = "587";
        }

        private void cmdGmail_Click(object sender, EventArgs e)
        {
            txtServerName.Text = "smtp.gmail.com";
            txtPortNo.Text = "587";
        }

        private void cmdOutlook_Click(object sender, EventArgs e)
        {
            txtServerName.Text = "smtp-mail.outlook.com";
            txtPortNo.Text = "25";
        }

        private void cmdToray_Click(object sender, EventArgs e)
        {
            txtServerName.Text = "10.251.208.31";
            txtPortNo.Text = "25";
        }

        private void cmdSend_Click(object sender, EventArgs e)
        {
            SendSMTP();
        }

        #endregion

        #region Private Methods

        public void SendSMTP()
        {
            SendMailConfig cfg = new SendMailConfig();
            // SMTP Server
            try
            {
                cfg.Server.HostName = txtServerName.Text.Trim();
                cfg.Server.PortNo = int.Parse(txtPortNo.Text.Trim());
                cfg.Server.EnableCredential = chkEnableCredential.Checked;
                cfg.Server.UserName = txtUserName.Text.Trim();
                cfg.Server.Password = txtPassword.Text.Trim();
                cfg.Server.UseSSL = chkSSL.Checked;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Mail Adddress
            try
            {
                cfg.Address.Sender = txtSender.Text.Trim();
                string[] targets = txtRecipients.Text.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries);
                cfg.Address.Recipients.AddRange(targets);
                string recipients = txtRecipients.Text;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Mail Message
            string title = txtTitle.Text;
            string body = txtBody.Text;
            SMTPMailSender.Send(cfg, title, body);
        }

        #endregion
    }
}

namespace SendMail
{
    using System.Net;
    using System.Net.Mail;

    #region Configs

    #region SendMailConfig

    /// <summary>
    /// The SMTPServerConfig class.
    /// </summary>
    public class SMTPServerConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SMTPServerConfig() : base() 
        { 
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets SMTP Host Name or IP.
        /// </summary>
        public string HostName { get; set; }
        /// <summary>
        /// Gets or sets SMTP Port No.
        /// </summary>
        public int PortNo { get; set; }

        /// <summary>
        /// Gets or set enable credential;
        /// </summary>
        public bool EnableCredential { get; set; }
        /// <summary>
        /// Gets or sets SMTP user name (i.e. email address).
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or set SMTP password.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets Use SSL.
        /// </summary>
        public bool UseSSL { get; set; }

        #endregion
    }

    #endregion

    #region MailAddressConfig

    /// <summary>
    /// The MailAddressConfig class.
    /// </summary>
    public class MailAddressConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MailAddressConfig() : base()
        {
            Recipients = new List<string>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets mail sender;
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// Gets or sets list of recipient.
        /// </summary>
        public List<string> Recipients { get; set; }

        #endregion
    }

    #endregion

    #region SendMailConfig

    /// <summary>
    /// The SendMailConfig class.
    /// </summary>
    public class SendMailConfig
    {
        #region Constructor

        public SendMailConfig() : base() 
        {
            Server = new SMTPServerConfig();
            Address = new MailAddressConfig();
        }

        #endregion

        #region Public Properties

        public SMTPServerConfig Server { get; set; }
        public MailAddressConfig Address { get; set; }

        #endregion
    }

    #endregion

    #endregion

    public class SMTPMailSender
    {
        public static void Send(SendMailConfig cfg, string title, string body)
        {
            if (null == cfg)
                return;

            // Set protocol.            
            ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(cfg.Address.Sender);
                foreach (string target in cfg.Address.Recipients)
                {
                    message.To.Add(new MailAddress(target));
                }

                message.IsBodyHtml = true; // to make message body as html

                message.SubjectEncoding = Encoding.UTF8;
                message.Subject = title;

                message.BodyEncoding = Encoding.UTF8;
                message.Body = body;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = cfg.Server.HostName;

                smtp.Port = cfg.Server.PortNo;
                smtp.EnableSsl = cfg.Server.UseSSL;

                if (cfg.Server.EnableCredential)
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(cfg.Server.UserName, cfg.Server.Password);
                }
                else
                {
                    smtp.UseDefaultCredentials = true;
                }
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                smtp.Dispose();
                smtp = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

