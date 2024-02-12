﻿#region Using

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
    /// Interaction logic for DIPHistorySearchPage.xaml
    /// </summary>
    public partial class DIPHistorySearchPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DIPHistorySearchPage()
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
            M3CordApp.Pages.GotoProductionReportMenu();
        }

        private void cmdView_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (null == btn) return;
            var item = btn.DataContext as DIPPCCard;
            if (null == item) return;

            Edit(item);
        }

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            RefreshGrid();
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        #endregion

        #region TextBox Handlers

        private void txtLotNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                RefreshGrid();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                // clear trace no.
                txtLotNo.Text = string.Empty;
                e.Handled = true;
            }
        }

        #endregion

        #region Combobox Handlers

        private void cbItemYarns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshGrid();
        }

        private void cbMCCodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshGrid();
        }

        private void cbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshGrid();
        }

        private void cbCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var customer = (null != cbCustomers.SelectedItem) ?
                cbCustomers.SelectedItem as Customer : null;

            // Product
            cbProducts.ItemsSource = null;

            if (null != customer)
            {
                var products = Product.GetDipProducts(customer.CustomerName).Value();
                cbProducts.ItemsSource = products;
            }

            RefreshGrid();
        }

        #endregion

        #region Private Methods

        private void Edit(DIPPCCard item)
        {
            var page = M3CordApp.Pages.DIPOperationView;
            page.Setup(item);
            PageContentManager.Instance.Current = page;
        }

        private void ClearInputs()
        {
            dtDate.SelectedDate = new DateTime?();
            cbMCCodes.SelectedIndex = -1;
            cbItemYarns.SelectedIndex = -1;
            txtLotNo.Text = string.Empty;
            cbCustomers.SelectedIndex = -1;
            cbProducts.SelectedIndex = -1;
        }

        private void LoadComboBoxes()
        {
            // MC
            cbMCCodes.ItemsSource = null;

            var mcList = DIPMC.Gets("S-7").Value();
            cbMCCodes.ItemsSource = mcList;

            // Item Yarn
            cbItemYarns.ItemsSource = null;

            var itemYarns = CordItemYarn.Gets().Value();
            cbItemYarns.ItemsSource = itemYarns;

            // Customer
            cbCustomers.ItemsSource = null;

            var customers = Customer.GetDIPs().Value();
            cbCustomers.ItemsSource = customers;
        }

        private void RefreshGrid()
        {
            grid.ItemsSource = null;

            DateTime? createdate = dtDate.SelectedDate;

            var mc = (null != cbMCCodes.SelectedItem) ?
                cbMCCodes.SelectedItem as DIPMC : null;
            string sMCCode = (null != mc) ? mc.MCCode : null;

            string lotNo = (!string.IsNullOrEmpty(txtLotNo.Text)) ? txtLotNo.Text.Trim() : null;

            var itemYarn = (null != cbItemYarns.SelectedItem) ? 
                cbItemYarns.SelectedItem as CordItemYarn : null;
            string sItemYarn = (null != itemYarn) ? itemYarn.ItemYarn : null;

            var customer = (null != cbCustomers.SelectedItem) ?
                cbCustomers.SelectedItem as Customer : null;
            string sCustomer = (null != customer) ? customer.CustomerName : null;

            var product = (null != cbProducts.SelectedItem) ?
                cbProducts.SelectedItem as Product : null;
            string sProduct = (null != product) ? product.ProductCode : null;


            var results = DIPPCCard.Search(createdate, sMCCode, sItemYarn, lotNo, sCustomer, sProduct).Value();

            grid.ItemsSource = results;
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            LoadComboBoxes();
            ClearInputs();
            RefreshGrid();
        }

        #endregion
    }
}