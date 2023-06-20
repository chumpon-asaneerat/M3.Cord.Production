#region Using

using System;
using System.Collections.Generic;
using System.Windows;

using NLib;
using System.Data;
using System.Data.OleDb;
using M3.Cord.Models;

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

        #region Load/Closing

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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

        private void InitQueries()
        {
            txtQuery.Text = BCSPRFTP.AS400.GetQuery();
        }

        private void UpdateConnectStatus()
        {
            bool connected = AS400DbServer.Instance.Connected;
            cmdConnect.IsEnabled = !connected;
            cmdDisconnect.IsEnabled = connected;
        }

        private void Connect()
        {
            if (AS400DbServer.Instance.Connected)
            {
                MessageBox.Show("Already Connected.");
            }
            try
            {
                AS400DbServer.Instance.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            UpdateConnectStatus();
        }

        private void Disconnect()
        {
            AS400DbServer.Instance.Shutdown();
            UpdateConnectStatus();
        }

        private void ExecuteQuery(string query)
        {
            if (!AS400DbServer.Instance.Connected)
            {
                MessageBox.Show("No connection");
                return;
            }
            // reset.
            dbGrid.ItemsSource = null;

            // Bind to DataGrid
            if (null != dbGrid.ItemsSource && dbGrid.ItemsSource is DataTable)
            {
                var tbl = dbGrid.ItemsSource as DataTable;
                tbl.Dispose();
            }

            dbGrid.ItemsSource = null;
            txtTotalRows.Text = "-";

            var list = BCSPRFTP.AS400.Gets(query);
            dbGrid.ItemsSource = list;
            txtTotalRows.Text = list.Count.ToString("n0");
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
