#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

        #region Internal Variables

        OracleConnection con = null;

        #endregion

        #region Loaded/Unloaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitControls();
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

        private void cmdDisconnect_Click(object sender, RoutedEventArgs e)
        {
            Disconnect();
        }

        private void cmdExecute_Click(object sender, RoutedEventArgs e)
        {
            Execute();
        }

        #endregion

        #region Private Methods

        private void InitControls()
        {
            txtCommandText.Text = "SELECT * FROM G3_YARN WHERE rownum <= 100";
        }

        private void Connect()
        {
            if (null != con)
                return; // already connect.

            // create connection
            con = new OracleConnection();
            // create connection string using builder
            OracleConnectionStringBuilder ocsb = new OracleConnectionStringBuilder();
            ocsb.DataSource = string.Format("(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST={0})(PORT={1}))(CONNECT_DATA=(SERVICE_NAME={2})))",
                txtServerName.Text, 1521, txtServiceName.Text);
            ocsb.UserID = txtUserName.Text;
            ocsb.Password = txtPassword.Text;
            txtConnectionString.Text = ocsb.ConnectionString;
            // connect
            con.ConnectionString = ocsb.ConnectionString;
            con.Open();

            UpdateStatus();
        }

        private void Disconnect()
        {
            if (null == con)
                return; // already disconnect.
            con.Close();
            con.Dispose();
            con = null;

            txtConnectionString.Text = string.Empty;

            UpdateStatus();
        }

        private void UpdateStatus()
        {
            txtStatus.Text = (null != con && con.State == System.Data.ConnectionState.Open) ?
                "Connected" : "Disconncted";
        }

        private void Execute()
        {
            if (null == con || con.State != System.Data.ConnectionState.Open)
                return; // not connect.

            string commandText = txtCommandText.Text;

            DataSet dataSet = null;
            using (var cmd =  new OracleCommand(commandText, con))
            {
                cmd.BindByName = true; // required for call a stored procedure with named parameters
                cmd.CommandText = commandText;
                cmd.CommandType = System.Data.CommandType.Text;
                
                OracleDataAdapter adapter = new OracleDataAdapter();
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        dataSet = new DataSet();
                        adapter.SelectCommand = cmd;
                        adapter.Fill(dataSet);
                    }

                    reader.Close();
                }
            }

            if (null != dbgrid.ItemsSource && dbgrid.ItemsSource is DataTable)
            {
                var tbl = dbgrid.ItemsSource as DataTable;
                tbl.Dispose();
            }

            dbgrid.ItemsSource = null;

            if (null != dataSet && null != dataSet.Tables && dataSet.Tables.Count > 0)
            {
                var tbl = dataSet.Tables[0];
                if (null != tbl)
                {
                    dbgrid.ItemsSource = tbl.DefaultView;
                }
            }
        }

        #endregion
    }
}
