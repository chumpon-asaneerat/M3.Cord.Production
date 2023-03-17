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

        private void cmdSend_Click(object sender, EventArgs e)
        {
            bool isSMTP = rbSMTP.Checked;
            if (!isSMTP) 
            { 
                SendMailTo(); 
            }
            else
            {
                SendSMTP();
            }
        }

        #endregion

        #region Private Methods

        public void SendMailTo()
        {
            string sender = txtSender.Text;
            string recipients = txtRecipients.Text;
            string body = txtBody.Text;
            string user = (chkEnableCredential.Checked) ? txtUserName.Text : null;
            string pwd = (chkEnableCredential.Checked) ? txtPassword.Text : null;

            EMailSender.MailTo.Send(sender, recipients, body, user, pwd);
        }

        public void SendSMTP()
        {
            string sender = txtSender.Text;
            string recipients = txtRecipients.Text;
            string body = txtBody.Text;
            string user = (chkEnableCredential.Checked) ? txtUserName.Text : null;
            string pwd = (chkEnableCredential.Checked) ? txtPassword.Text : null;

            EMailSender.SMTP.Send(sender, recipients, body, user, pwd);
        }

        #endregion
    }
}

namespace SendMail
{
    using System.Diagnostics;
    using System.Net;
    using System.Net.Mail;
    using static SendMail.EMailSender;

    public class EMailSender
    {
        public class MailTo
        {
            public static void Send(string sender, string recipients, string body,
                string user = null, string password = null)
            {
                string[] targets = recipients.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                MailMessage message = new MailMessage();
                message.From = new MailAddress(sender);
                foreach (string target in targets)
                {
                    message.To.Add(new MailAddress(target));
                }
                message.Subject = "Test";
                message.IsBodyHtml = true; // to make message body as html
                message.Body = body;
                string url = message.ToUrl();
                try
                {
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public class SMTP
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
                    smtp.Host = "smtp-mail.outlook.com";  //for outlook host
                    //smtp.Host = "smtp.yahoo.com"; //for yahoo host
                    //smtp.Port = 587;
                    smtp.Port = 25;
                    smtp.EnableSsl = true;

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

    public static class Mailto
    {
        public static string ToUrl(this MailMessage message) =>
            "mailto:?" + string.Join("&", Parameters(message));

        static IEnumerable<string> Parameters(MailMessage message)
        {
            if (message.To.Any())
                yield return "to=" + Recipients(message.To);

            if (message.CC.Any())
                yield return "cc=" + Recipients(message.CC);

            if (message.Bcc.Any())
                yield return "bcc=" + Recipients(message.Bcc);

            if (!string.IsNullOrWhiteSpace(message.Subject))
                yield return "subject=" + Uri.EscapeDataString(message.Subject);

            if (!string.IsNullOrWhiteSpace(message.Body))
                yield return "body=" + Uri.EscapeDataString(message.Body);
        }

        static string Recipients(MailAddressCollection addresses) =>
            string.Join(",", from r in addresses
                             select Uri.EscapeDataString(r.Address));
    }
}

