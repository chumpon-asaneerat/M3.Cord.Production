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
    /// Interaction logic for PalletSettingManagePage.xaml
    /// </summary>
    public partial class PalletSettingManagePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PalletSettingManagePage()
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

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            txtProductLotNo.Text = string.Empty;
            dtBegin.SelectedDate = new DateTime?();
            dtEnd.SelectedDate = new DateTime?();
            cbProducts.SelectedIndex = -1;

            RefreshGrid();
        }

        private void cmdCreate_Click(object sender, RoutedEventArgs e)
        {
            var win = M3CordApp.Windows.CreatePallet;
            if (win.ShowDialog() == true && null != win.Pallet)
            {
                // Show Print Preview
                var page = M3CordApp.Pages.PalletSettingPreview;
                var items = new List<PalletSetting>();
                items.Add(win.Pallet);
                page.Setup(items);
                PageContentManager.Instance.Current = page;
            }
        }

        #endregion

        #region Combobox Handlers

        private void cbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshGrid();
        }

        #endregion

        #region TextBox Handlers

        private void txtProductLotNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            {
                RefreshGrid();
                e.Handled = true;
            }
        }

        #endregion

        #region Private Methods

        private void RefreshGrid()
        {
            grid.ItemsSource = null;

            string productLotNo = txtProductLotNo.Text;
            DateTime? begin = dtBegin.SelectedDate;
            DateTime? end = dtEnd.SelectedDate;
            Product product = cbProducts.SelectedItem as Product;
            string productCode = (null != product) ? product.ProductCode : null;
            
            grid.ItemsSource = PalletSetting.Search(productLotNo, begin, end, productCode).Value();
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            cbProducts.ItemsSource = Product.Gets().Value();
            RefreshGrid();
        }

        #endregion
    }
}
