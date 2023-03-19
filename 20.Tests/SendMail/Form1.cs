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
            txtServerName.Text = "smtp.yahoo.com";
            txtPortNo.Text = "587";
        }

        private void cmdGmail_Click(object sender, EventArgs e)
        {
            txtServerName.Text = "smtp.gmail.com";
            txtPortNo.Text = "587";
        }

        private void cmdOutlook_Click(object sender, EventArgs e)
        {
            txtServerName.Text = "smtp-mail.gmail.com";
            txtPortNo.Text = "587";
        }

        private void cmdToray_Click(object sender, EventArgs e)
        {

        }

        private void cmdSend_Click(object sender, EventArgs e)
        {
            SendSMTP();
        }

        #endregion

        #region Private Methods

        public void SendSMTP()
        {
            string hostName = txtServerName.Text;

            string sender = txtSender.Text.Trim();
            string recipients = txtRecipients.Text;
            string body = txtBody.Text;
            string user = (chkEnableCredential.Checked) ? txtUserName.Text : null;
            string pwd = (chkEnableCredential.Checked) ? txtPassword.Text : null;

            SMTPMailSender.Send(sender, recipients, body, user, pwd);
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
        public static void Send(string sender, string recipients, string body,
            string user = null, string password = null)
        {
            bool hasCredential = (!string.IsNullOrWhiteSpace(user) && !string.IsNullOrWhiteSpace(password));
            string[] targets = recipients.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(sender);
                foreach (string target in targets)
                {
                    message.To.Add(new MailAddress(target));
                }
                message.Subject = "Test";
                message.IsBodyHtml = true; // to make message body as html
                message.Body = body;

                SmtpClient smtp = new SmtpClient();
                //smtp.Host = "smtp.gmail.com"; //for gmail host
                //smtp.Host = "smtp-mail.outlook.com";  //for outlook host
                smtp.Host = "smtp.yahoo.com"; //for yahoo host
                                              //smtp.Port = 587;
                smtp.Port = 25;
                smtp.EnableSsl = false;

                if (hasCredential)
                {
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new NetworkCredential(user, password);
                }
                else
                {
                    smtp.UseDefaultCredentials = false;
                }
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

