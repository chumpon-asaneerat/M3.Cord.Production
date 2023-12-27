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

        #endregion

        #region Combobox Handlers

        private void cbItemYanrs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshGrid();
        }

        #endregion

        #region Private Methods

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

        private void RefreshGrid()
        {
            grid.ItemsSource = null;

            var itemYarn = (null != cbItemYanrs.SelectedItem) ?
                cbItemYanrs.SelectedItem as CordItemYarn : null;

            string sItemYarn = (null != itemYarn) ? itemYarn.ItemYarn : null;
            string requestNo = !string.IsNullOrWhiteSpace(txtRequsetNo.Text) ? txtRequsetNo.Text.Trim() : null;
            DateTime? whReceiveDate = dtWHReceiveDate.SelectedDate;

            WarehouseStockYarnService.Instance.LoadStockYarns(sItemYarn, requestNo, whReceiveDate);

            grid.ItemsSource = WarehouseStockYarnService.Instance.IssueItems;
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            this.DataContext = WarehouseStockYarnService.Instance;

            LoadComboBoxes();

            RefreshGrid();
        }

        #endregion

        private void txtRequsetNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
