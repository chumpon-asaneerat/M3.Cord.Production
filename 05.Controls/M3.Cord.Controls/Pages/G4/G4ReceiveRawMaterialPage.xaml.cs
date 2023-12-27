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

using NLib.Models;
using NLib.Services;
using M3.Cord.Models;
using System.Windows.Interop;
using NLib.Wpf.Controls;
using System.Diagnostics;
using System.Reflection;
using NLib;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for G4ReceiveRawMaterialPage.xaml
    /// </summary>
    public partial class G4ReceiveRawMaterialPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public G4ReceiveRawMaterialPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button Handlers

        private void cmdHome_Click(object sender, RoutedEventArgs e)
        {
            M3CordApp.Pages.GotoCordMainMenu();
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            G4ReceiveYarnService.Instance.Clear();
            RefreshGrid();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            SaveItems();
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as FontAwesomeButton;
            if (null == btn) return;
            var item = btn.DataContext as G4Yarn;
            EditItem(item);
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as FontAwesomeButton;
            if (null == btn) return;
            var item = btn.DataContext as G4Yarn;
            DeleteItem(item);
        }

        private void cmdAS400_Click(object sender, RoutedEventArgs e)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            // connect
            AS400DbServer.Instance.Start();

            string msg = string.Empty;
            int iCnt = G4ReceiveYarnService.Instance.LoadFromAS400();
            if (iCnt != -1)
            {
                msg += "Successfully load data from AS400." + Environment.NewLine;
                msg += string.Format("Receive {0:n0} record(s).", iCnt);
                med.Info(msg);
            }
            else
            {
                msg = "Load data from AS400 failed.";
                med.Err(msg);
            }
            // disconnect
            AS400DbServer.Instance.Shutdown();

            var win = M3CordApp.Windows.MessageBox;
            win.Setup(msg);
            win.ShowDialog();
            RefreshGrid();
        }

        #endregion

        #region TextBox Handlers

        private void txtTraceNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                ReceivedTraceNo();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                // clear trace no.
                txtTraceNo.Text = string.Empty;
                e.Handled = true;
            }
        }

        #endregion

        #region Combobox Handlers

        private void cbItemYanrs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.InvokeAction(() =>
            {
                RefreshGrid();
            });
        }

        #endregion

        #region Private Methods

        private void ReceivedTraceNo()
        {
            string traceNo = txtTraceNo.Text.Trim();

            if (G4ReceiveYarnService.Instance.IsExist(traceNo))
            {
                // duplicate.
                txtTraceNo.Text = string.Empty;
                return;
            }

            var item = G4ReceiveYarnService.Instance.SerachByTranceNo(traceNo);
            if (null != item)
            {
                // set expired date
                if (!item.ExpiredDate.HasValue)
                {
                    DateTime itemDate = (item.EntryDate.HasValue) ? item.EntryDate.Value : DateTime.Today.Date;
                    DateTime expireDate = itemDate.AddYears(1).AddDays(-1);
                    item.ExpiredDate = expireDate;
                }

                var win = M3CordApp.Windows.G4ReceiveYarn;
                win.Setup(item);
                if (win.ShowDialog() == true)
                {
                    G4ReceiveYarnService.Instance.Add(item);
                    RefreshGrid();
                }
            }
            else
            {
                var win = M3CordApp.Windows.MessageBox;
                string msg = string.Format("Trace No: {0} not found.", traceNo);
                win.Setup(msg);
                win.ShowDialog();
            }
            // clear trace no.
            txtTraceNo.Text = string.Empty;
        }

        private void EditItem(G4Yarn item)
        {
            if (null == item) return;

            var win = M3CordApp.Windows.G4ReceiveYarn;
            win.Setup(item);
            if (win.ShowDialog() == true)
            {
                RefreshGrid();
            }
        }

        private void DeleteItem(G4Yarn item)
        {
            if (null == item) return;
            G4ReceiveYarnService.Instance.Remove(item);
            RefreshGrid();
        }

        private void SaveItems()
        {
            var success = G4ReceiveYarnService.Instance.SaveReceiveItems();

            // Show MessageBox
            string msg = (success) ? "Save Success" : "Save Failed";
            var win = M3CordApp.Windows.MessageBox;
            win.Setup(msg);
            win.ShowDialog();

            RefreshGrid();
        }

        private void LoadComboBoxes()
        {
            cbItemYanrs.ItemsSource = null;

            var itemYarns = CordItemYarn.Gets().Value();
            cbItemYanrs.ItemsSource = itemYarns;

            this.InvokeAction(() =>
            {
                if (null != itemYarns && itemYarns.Count > 0) cbItemYanrs.SelectedIndex = 0;
            });
        }

        public void RefreshGrid()
        {
            grid.ItemsSource = null;
            grid.ItemsSource = G4ReceiveYarnService.Instance.Receives;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup()
        {
            // init combobox
            LoadComboBoxes();
            // Set data context for show total(s).
            this.DataContext = G4ReceiveYarnService.Instance;
            G4ReceiveYarnService.Instance.Clear();
            RefreshGrid();
        }

        #endregion
    }
}
