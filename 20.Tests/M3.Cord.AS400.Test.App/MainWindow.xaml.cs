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
using System.Windows.Controls.Primitives;

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
            InitQueries();
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

        private void InitQueries()
        {
            string query = string.Empty;

            query += "SELECT #ANNUL AS ANNUL " + Environment.NewLine;
            query += "     , #FLAGS AS FLAGS " + Environment.NewLine;
            query += "     , #RECTY AS RECTY " + Environment.NewLine;
            query += "     , #CDSTO AS CDSTO " + Environment.NewLine;
            query += "     , #USRNM AS USRNM " + Environment.NewLine;
            query += "     , #DTTRA AS DTTRA " + Environment.NewLine;
            query += "     , #DTINP AS DTINP " + Environment.NewLine;
            query += "     , #CDEL0 AS CDEL0 " + Environment.NewLine;
            query += "     , #CDCON AS CDCON " + Environment.NewLine;
            query += "     , #BLELE AS BLELE " + Environment.NewLine;
            query += "     , #CDUM0 AS CDUM0 " + Environment.NewLine;
            query += "     , #CDKE1 AS CDKE1 " + Environment.NewLine;
            query += "     , #CDKE2 AS CDKE2 " + Environment.NewLine;
            query += "     , #CDKE3 AS CDKE3 " + Environment.NewLine;
            query += "     , #CDKE4 AS CDKE4 " + Environment.NewLine;
            query += "     , #CDKE5 AS CDKE5 " + Environment.NewLine;
            query += "     , #CDLOT AS CDLOT " + Environment.NewLine;
            query += "     , #CDTRA AS CDTRA " + Environment.NewLine;
            query += "     , #REFER AS REFER " + Environment.NewLine;
            query += "     , #LOCAT AS LOCAT " + Environment.NewLine;
            query += "     , #CDQUA AS CDQUA " + Environment.NewLine;
            query += "     , #QUACA AS QUACA " + Environment.NewLine;
            query += "     , #TECU1 AS TECU1 " + Environment.NewLine;
            query += "     , #TECU2 AS TECU2 " + Environment.NewLine;
            query += "     , #TECU3 AS TECU3 " + Environment.NewLine;
            query += "     , #TECU4 AS TECU4 " + Environment.NewLine;
            query += "     , #TECU5 AS TECU5 " + Environment.NewLine;
            query += "     , #TECU6 AS TECU6 " + Environment.NewLine;
            query += "     , #COMM0 AS COMM0 " + Environment.NewLine;
            query += "     , #DTORA AS DTORA " + Environment.NewLine;
            query += "  FROM BCSPRFTP " + Environment.NewLine;

            txtQuery.Text = query;
        }

        private void UpdateConnectStatus()
        {
            bool connected = (null != _conn && _conn.State == ConnectionState.Open);
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
                _conn = null;
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
            {
                MessageBox.Show("No connection");
                return;
            }
            // reset.
            dbGrid.ItemsSource = null;

            DataSet dataSet = new DataSet();
            try
            {
                OleDbDataAdapter adapter;
                try
                {
                    adapter = new OleDbDataAdapter(query, _conn);
                    adapter.Fill(dataSet, "BCSPRFTP");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.ToString());
            }

            // Bind to DataGrid
            if (null != dbGrid.ItemsSource && dbGrid.ItemsSource is DataTable)
            {
                var tbl = dbGrid.ItemsSource as DataTable;
                tbl.Dispose();
            }

            dbGrid.ItemsSource = null;
            txtTotalRows.Text = "-";

            if (null != dataSet && null != dataSet.Tables)
            {
                MessageBox.Show(string.Format("Table Count: {0}", dataSet.Tables.Count));
            }
            else
            {
                MessageBox.Show("No data set.");
            }

            if (null != dataSet && null != dataSet.Tables && dataSet.Tables.Count > 0)
            {
                var tbl = dataSet.Tables[0];
                if (null != tbl)
                {
                    dbGrid.ItemsSource = tbl.DefaultView;
                    txtTotalRows.Text = tbl.Rows.Count.ToString("n0");
                }
                else
                {
                    txtTotalRows.Text = "0";
                }
            }
        }

        #endregion
    }
}
