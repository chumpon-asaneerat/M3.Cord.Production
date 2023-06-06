#region Using

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using System.Data;
using System.Data.OleDb;
using System.Data.Common;

#endregion

namespace M3.Cord.AS400.Test.App
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

        #region Internal Variables

        private OleDbConnection _conn = null;

        #endregion

        #region Load/Closing

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitConnectionString();
            UpdateConnectStatus();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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

        private void cmdExecuteQuery_Click(object sender, RoutedEventArgs e)
        {
            string query = txtQuery.Text;
            ExecuteQuery(query);
        }

        #endregion

        #region Private Methods

        private void InitConnectionString()
        {
            string dataSource = "172.20.7.16";
            string userId = "PLM";
            string pwd = "LUCKY";
            string defaultColl = "TESLUCDAT";

            string conn = string.Format(
                "Provider=IBMDA400;Data Source={0};User Id={1};Password={2};Default Collection={3};", 
                dataSource, userId, pwd, defaultColl);
            txtConnectionString.Text = conn;
        }

        private void UpdateConnectStatus()
        {
            bool connected = (null != _conn && _conn.State != ConnectionState.Open);
            cmdConnect.IsEnabled = !connected;
            cmdDisconnect.IsEnabled = connected;
        }

        private void Connect()
        {
            if (null != _conn)
            {
                MessageBox.Show("Already Connected.");
            }
            _conn = new OleDbConnection();
            _conn.ConnectionString = txtConnectionString.Text;
            try
            {
                _conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            UpdateConnectStatus();
        }

        private void Disconnect()
        {
            if (null != _conn) 
            {
                try
                {
                    _conn.Close();
                    _conn.Dispose();
                }
                catch (Exception) { }
            }
            _conn = null;

            UpdateConnectStatus();
        }

        private void ExecuteQuery(string query)
        {
            if (null == _conn)
                return;
            // reset.
            dbGrid.ItemsSource = null;

            DataSet dataSet = null;
            try
            {
                using (var cmd = _conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;

                    OleDbDataAdapter adapter = new OleDbDataAdapter();
                    try
                    {
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
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.ToString());
            }
        }


        #endregion
    }
}
