#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SignInWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private string sUserName = string.Empty;
        private string sPassword = string.Empty;

        #endregion

        #region Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUserName.Focus();
        }

        #endregion

        #region Button Handlers

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            SignIn();
        }

        #endregion

        #region Private Methods

        private void FocusControl(TextBox ctrl)
        {
            if (null == ctrl)
                return;
            // Set focus to text box this invoked when the application has rendered
            Dispatcher.BeginInvoke(new Action(() =>
            {
                ctrl.SelectAll();
                ctrl.Focus();
            }), System.Windows.Threading.DispatcherPriority.Render);
        }
        private void FocusControl(PasswordBox ctrl)
        {
            if (null == ctrl)
                return;
            // Set focus to text box this invoked when the application has rendered
            Dispatcher.BeginInvoke(new Action(() =>
            {
                ctrl.SelectAll();
                ctrl.Focus();
            }), System.Windows.Threading.DispatcherPriority.Render);
        }

        private void SignIn()
        {
            string userName = txtUserName.Text;
            if (string.IsNullOrWhiteSpace(userName))
            {
                var win = M3CordApp.Windows.MessageBox;
                win.Setup("กรุณาป้อน ชื่อบัญชีผู้ใช้");
                win.ShowDialog();

                FocusControl(txtUserName);
                return;
            }

            string password = txtPassword.Password;
            if (string.IsNullOrWhiteSpace(password))
            {
                var win = M3CordApp.Windows.MessageBox;
                win.Setup("กรุณาป้อน รหัสผ่าน");
                win.ShowDialog();

                FocusControl(txtPassword);
                return;
            }

            SignInStatus status = SignInManager.Instance.SignIn(userName, password);

            bool success = false;
            string msg = string.Empty;
            switch (status)
            {
                case SignInStatus.UserNotFound:
                    msg = "ไม่พบข้อมูล ชื่อผู้ใช้งาน กรุณาตรวจสอบ";
                    break;
                case SignInStatus.InvalidPassword:
                    msg = "รหัสผ่านไม่ถูกต้อง กรุณาตรวจสอบ";
                    break;
                case SignInStatus.Success:
                    success = true;
                    break;
            }

            if (!success)
            {
                // login failed show message.
                var win = M3CordApp.Windows.MessageBox;
                win.Setup(msg);
                win.ShowDialog();

                if (status == SignInStatus.InvalidPassword)
                    FocusControl(txtPassword);
                else FocusControl(txtUserName);
            }

            DialogResult = success;
        }

        #endregion
    }
}
