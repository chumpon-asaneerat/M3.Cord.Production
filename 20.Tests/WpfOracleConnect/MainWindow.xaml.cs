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
using System.Windows.Navigation;
using System.Windows.Shapes;

#endregion

using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;

namespace WpfOracleConnect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded/Unloaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Disconnect();
        }

        #endregion

        #region Button Handlers

        private void cmdConnect_Click(object sender, RoutedEventArgs e)
        {
            Connect();
        }

        #endregion

        #region Private Methods

        private void Connect()
        {
            // create connection
            OracleConnection con = new OracleConnection();
            // create connection string using builder
            OracleConnectionStringBuilder ocsb = new OracleConnectionStringBuilder();
            ocsb.DataSource = string.Format("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST={0})(PORT={1}))(CONNECT_DATA=(SERVICE_NAME={2})))",
                txtServerName.Text, 1521, txtServiceName.Text);
            ocsb.UserID = txtUserName.Text;
            ocsb.Password = txtPassword.Text;
            txtConnectionString.Text = ocsb.ConnectionString;
            /*
            // connect
            con.ConnectionString = ocsb.ConnectionString;
            con.Open();
            Console.WriteLine("Connection established (" + con.ServerVersion + ")");
            */
        }

        private void Disconnect()
        {

        }

        #endregion
    }
}
