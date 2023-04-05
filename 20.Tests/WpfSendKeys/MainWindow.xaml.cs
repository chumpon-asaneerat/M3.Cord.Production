#region Using

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

#endregion

namespace WpfSendKeys
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        #region Button Handlers

        private void cmdSend_Click(object sender, RoutedEventArgs e)
        {
            var procs = Process.GetProcesses();
            foreach (var proc in procs) { Console.WriteLine(proc.ProcessName); }

            string txt = txtTextToSend.Text.Trim();
            SendKeys.Send("M3.Cord.QA.App", txt);
        }

        #endregion
    }

    public static class SendKeys
    {
        // import the function in your class
        [DllImport("User32.dll")]
        private static extern int SetForegroundWindow(IntPtr point);

        /// <summary>
        ///   Sends the specified key.
        /// </summary>
        /// <param name="appName">The app process name.</param>
        /// <param name="textToSend">The text to send.</param>
        public static void Send(string appName, string textToSend)
        {
            Process p = Process.GetProcessesByName(appName).FirstOrDefault();
            if (p != null)
            {
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);
                System.Windows.Forms.SendKeys.SendWait(textToSend);
            }
        }
    }
}
