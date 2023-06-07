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

using NLib;
using NLib.Models;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using System.Windows.Controls.Primitives;
using System.CodeDom;

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

        private void cmdExport_Click(object sender, RoutedEventArgs e)
        {
            Export();
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

        public class BCSPRFTP
        {
            /// <summary>Flags</summary>
            public string ANNUL { get; set; }
            /// <summary>Flags</summary>
            public string FLAGS { get; set; }
            /// <summary>Type</summary>
            public string RECTY { get; set; }
            /// <summary>Warehouse</summary>
            public string CDSTO { get; set; }
            /// <summary>User Name</summary>
            public string USRNM { get; set; }
            /// <summary>Movement Date</summary>
            public string DTTRA { get; set; }
            /// <summary>Entry Date</summary>
            public string DTINP { get; set; }
            /// <summary>Roll No</summary>
            public string CDEL0 { get; set; }
            /// <summary>Pallet No</summary>
            public string CDCON { get; set; }
            /// <summary>Quantity</summary>
            public string BLELE { get; set; }
            /// <summary>Unit</summary>
            public string CDUM0 { get; set; }
            /// <summary>Item Code</summary>
            public string CDKE1 { get; set; }
            /// <summary>Production Type</summary>
            public string CDKE2 { get; set; }
            /// <summary>N/A</summary>
            public string CDKE3 { get; set; }
            /// <summary>N/A</summary>
            public string CDKE4 { get; set; }
            /// <summary>N/A</summary>
            public string CDKE5 { get; set; }
            /// <summary>Lot No</summary>
            public string CDLOT { get; set; }
            /// <summary>Transaction Code</summary>
            public string CDTRA { get; set; }
            /// <summary>Document No</summary>
            public string REFER { get; set; }
            /// <summary>Location</summary>
            public string LOCAT { get; set; }
            /// <summary>Grade</summary>
            public string CDQUA { get; set; }
            /// <summary>Defect Group</summary>
            public string QUACA { get; set; }
            /// <summary>Technical Data 1</summary>
            public string TECU1 { get; set; }
            /// <summary>Technical Data 2</summary>
            public string TECU2 { get; set; }
            /// <summary>Technical Data 3</summary>
            public string TECU3 { get; set; }
            /// <summary>Technical Data 4</summary>
            public string TECU4 { get; set; }
            /// <summary>Technical Data 5</summary>
            public string TECU5 { get; set; }
            /// <summary>Technical Data 6</summary>
            public string TECU6 { get; set; }
            /// <summary>Comment</summary>
            public string COMM0 { get; set; }
            /// <summary>Last Update Time</summary>
            public string DTORA { get; set; }
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
            query += " WHERE #CDSTO = '3G' " + Environment.NewLine;

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
                Console.WriteLine(string.Format("Table Count: {0}", dataSet.Tables.Count));
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
                    var list = CreateList(tbl);
                    dbGrid.ItemsSource = list;
                    txtTotalRows.Text = list.Count.ToString("n0");
                }
                else
                {
                    txtTotalRows.Text = "0";
                }
            }
        }

        private string GetRow(DataRow row, string columnName)
        {
            return (null != row && null != row[columnName]) ? row[columnName].ToString() : null;
        }

        private List<BCSPRFTP> CreateList(DataTable tbl)
        {
            var items = new List<BCSPRFTP>();
            if (null != tbl && null != tbl.Rows)
            {
                int cnt = tbl.Rows.Count;
                for (int i = 0; i < cnt; i++)
                {
                    var row = tbl.Rows[i];
                    var inst = new BCSPRFTP();

                    inst.ANNUL = GetRow(row, "ANNUL");
                    inst.FLAGS = GetRow(row, "FLAGS");
                    inst.RECTY = GetRow(row, "RECTY");
                    inst.CDSTO = GetRow(row, "CDSTO");
                    inst.USRNM = GetRow(row, "USRNM");
                    inst.DTTRA = GetRow(row, "DTTRA");
                    inst.DTINP = GetRow(row, "DTINP");
                    inst.CDEL0 = GetRow(row, "CDEL0");
                    inst.CDCON = GetRow(row, "CDCON");
                    inst.BLELE = GetRow(row, "BLELE");
                    inst.CDUM0 = GetRow(row, "CDUM0");
                    inst.CDKE1 = GetRow(row, "CDKE1");
                    inst.CDKE2 = GetRow(row, "CDKE2");
                    inst.CDKE3 = GetRow(row, "CDKE3");
                    inst.CDKE4 = GetRow(row, "CDKE4");
                    inst.CDKE5 = GetRow(row, "CDKE5");
                    inst.CDLOT = GetRow(row, "CDLOT");
                    inst.CDTRA = GetRow(row, "CDTRA");
                    inst.REFER = GetRow(row, "REFER");
                    inst.LOCAT = GetRow(row, "LOCAT");
                    inst.CDQUA = GetRow(row, "CDQUA");
                    inst.QUACA = GetRow(row, "QUACA");
                    inst.TECU1 = GetRow(row, "TECU1");
                    inst.TECU2 = GetRow(row, "TECU2");
                    inst.TECU3 = GetRow(row, "TECU3");
                    inst.TECU4 = GetRow(row, "TECU4");
                    inst.TECU5 = GetRow(row, "TECU5");
                    inst.TECU6 = GetRow(row, "TECU6");
                    inst.COMM0 = GetRow(row, "COMM0");
                    inst.DTORA = GetRow(row, "DTORA");

                    items.Add(inst);
                }
            }

            return items;
        }

        private void Export()
        {
            if (null != dbGrid.ItemsSource && dbGrid.ItemsSource is List<BCSPRFTP>)
            {
                var list = dbGrid.ItemsSource as List<BCSPRFTP>;
                if (null != list)
                {
                    string fileName = Dialogs.SaveDialog();
                    if (string.IsNullOrEmpty(fileName))
                        return;

                    list.SaveToFile(fileName, false);
                }
            }
        }

        #endregion
    }

    #region Dialogs class

    public class Dialogs
    {
        #region Show Save Excel Dialog

        /// <summary>
        /// Show Save Excel File Dialog.
        /// </summary>
        /// <param name="defaultFileName">The Default File Name.</param>
        /// <returns>Returns FileName if user choose file otherwise return null.</returns>
        public static string SaveDialog(string defaultFileName)
        {
            return SaveDialog(null, null, "กรุณาระบุขื่อ json file ที่ต้องการนำส่งออกข้อมูล", defaultFileName);
        }
        /// <summary>
        /// Show Save Excel File Dialog.
        /// </summary>
        /// <param name="title">The Dialog Title.</param>
        /// <param name="initDir">The initial directory path.</param>
        /// <returns>Returns FileName if user choose file otherwise return null.</returns>
        public static string SaveDialog(string title = "กรุณาระบุขื่อ json file ที่ต้องการนำส่งออกข้อมูล",
            string initDir = null)
        {
            return SaveDialog(null, title, initDir);
        }
        /// <summary>
        /// Show Save Excel File Dialog.
        /// </summary>
        /// <param name="owner">The owner window.</param>
        /// <param name="title">The Dialog Title.</param>
        /// <param name="initDir">The initial directory path.</param>
        /// <param name="defaultFileName">The Default File Name.</param>
        /// <returns>Returns FileName if user choose file otherwise return null.</returns>
        public static string SaveDialog(Window owner,
            string title = "กรุณาเลือก json file ที่ต้องการนำเข้าข้อมูล",
            string initDir = null,
            string defaultFileName = "")
        {
            string fileName = null;

            // setup dialog options
            var sd = new Microsoft.Win32.SaveFileDialog();
            sd.InitialDirectory = initDir;
            sd.Title = string.IsNullOrEmpty(title) ? "กรุณาระบุขื่อ json file ที่ต้องการนำส่งออกข้อมูล" : title;
            sd.Filter = "Json Files(*.json)|*.json";
            sd.FileName = defaultFileName;
            var ret = sd.ShowDialog(owner) == true;
            if (ret)
            {
                // assigned to FileName
                fileName = sd.FileName;
            }
            sd = null;

            return fileName;
        }

        #endregion
    }

    #endregion
}
