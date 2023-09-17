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

using NLib.Services;
using M3.Cord.Models;
using NLib.Models;
using NLib;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for WarehouseYarnStockPage.xaml
    /// </summary>
    public partial class WarehouseYarnStockPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public WarehouseYarnStockPage()
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

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            RefreshGrid();
        }

        private void cmdReceiveAll_Click(object sender, RoutedEventArgs e)
        {
            WarehouseReceiveYarnService.Instance.ReceiveAll();
        }

        private void cmdReceive_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (null == btn) return;
            var item = btn.DataContext as WarehouseCordYarn;
            WarehouseReceiveYarnService.Instance.MarkReceive(item);
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (null == btn) return;
            var item = btn.DataContext as WarehouseCordYarn;
            WarehouseReceiveYarnService.Instance.UnmarkReceive(item);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            WarehouseReceiveYarnService.Instance.SaveReceiveItems();
            this.InvokeAction(() =>
            {
                RefreshGrid();
            });
        }

        #endregion

        #region TextBox Handlers

        private void txtRequestNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                RefreshGrid();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                txtRequestNo.Text = string.Empty;
                e.Handled = true;
            }
        }

        private void txtPalletNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                string palletNo = txtPalletNo.Text;
                MarkPallet(palletNo);
                // clear pallet no.
                txtPalletNo.Text = string.Empty;

                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                // clear pallet no.
                txtPalletNo.Text = string.Empty;
                e.Handled = true;
            }
        }

        #endregion

        #region Combobox Handlers

        private void cbItemYanrs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshGrid();
        }

        #endregion

        #region Private Methods

        private void ResetControls()
        {
            txtRequestNo.Text = string.Empty;
            txtPalletNo.Text = string.Empty;
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

        private void MarkPallet(string palletNo)
        {
            var item = WarehouseReceiveYarnService.Instance.FindByPalletNo(palletNo);
            if (null != item)
            {
                WarehouseReceiveYarnService.Instance.MarkReceive(item);
            }
        }


        private void RefreshGrid()
        {
            grid.ItemsSource = null;

            var itemYarn = (null != cbItemYanrs.SelectedItem) ?
                cbItemYanrs.SelectedItem as CordItemYarn : null;

            string sItemYarn = (null != itemYarn) ? itemYarn.ItemYarn : null;
            string sRequsetNo = (string.IsNullOrEmpty(txtRequestNo.Text)) ? null : txtRequestNo.Text.Trim();

            WarehouseReceiveYarnService.Instance.LoadIssueYarns(sItemYarn, sRequsetNo);

            grid.ItemsSource = WarehouseReceiveYarnService.Instance.IssueItems;
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            this.DataContext = WarehouseReceiveYarnService.Instance;

            ResetControls();
            LoadComboBoxes();

            RefreshGrid();
        }

        #endregion
    }
}
