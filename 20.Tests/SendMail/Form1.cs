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

        }

        public void SendSMTP()
        {

        }

        #endregion
    }

    public class EMailSender
    {

    }
}
