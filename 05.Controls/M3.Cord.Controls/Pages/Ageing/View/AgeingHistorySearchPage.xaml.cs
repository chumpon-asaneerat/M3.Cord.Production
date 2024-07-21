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
    /// Interaction logic for AgeingHistorySearchPage.xaml
    /// </summary>
    public partial class AgeingHistorySearchPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public AgeingHistorySearchPage()
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
            var item = btn.DataContext as S5Condition;
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

        private void txtPalletOrTrace_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                RefreshGrid();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                // clear trace no.
                txtPalletOrTrace.Text = string.Empty;
                e.Handled = true;
            }
        }

        #endregion

        #region Combobox Handlers

        private void cbSources_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fromSource = (null != cbSources.SelectedItem) ?
                cbSources.SelectedItem as S5Source : null;

            cbCustomers.ItemsSource = null;

            if (null != fromSource)
            {
                if (fromSource.Id.HasValue && fromSource.Id == 0)
                {
                    // RAW
                    var customers = Customer.Gets().Value();
                    cbCustomers.ItemsSource = customers;
                }
                else if (fromSource.Id.HasValue && fromSource.Id == 1)
                {
                    // TWIST
                    var customers = Customer.Gets().Value();
                    cbCustomers.ItemsSource = customers;
                }
                else if (fromSource.Id.HasValue && fromSource.Id == 4)
                {
                    // DIP
                    var customers = Customer.GetDIPs().Value();
                    cbCustomers.ItemsSource = customers;
                }
                else
                {
                    cbCustomers.ItemsSource = new List<Customer>();
                }
            }

            //RefreshGrid();
        }

        private void cbMCSides_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //RefreshGrid();
        }

        private void cbItemYarns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //RefreshGrid();
        }

        private void cbMCCodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           //RefreshGrid();
        }

        private void cbCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fromSource = (null != cbSources.SelectedItem) ?
                cbSources.SelectedItem as S5Source : null;

            cbProducts.ItemsSource = null;

            if (null != fromSource)
            {
                if (fromSource.Id.HasValue && fromSource.Id == 0)
                {
                    // RAW
                    var products = new List<Product>()
                    {
                        // Fixed
                        new Product() { ProductCode = "1800TW", ProductName = "BRAKE HOSE" }
                    };
                    cbProducts.ItemsSource = products;
                }
                else if (fromSource.Id.HasValue && fromSource.Id == 1)
                {
                    // TWIST
                    var products = Product.Gets().Value();
                    cbProducts.ItemsSource = products;
                }
                else if (fromSource.Id.HasValue && fromSource.Id == 4)
                {
                    var customer = (null != cbCustomers.SelectedItem) ?
                        cbCustomers.SelectedItem as Customer : null;
                    string sCustomer = (null != customer) ? customer.CustomerName : null;
                    // DIP
                    var products = Product.GetDipProducts(sCustomer).Value();
                    cbProducts.ItemsSource = products;
                }
                else
                {
                    cbProducts.ItemsSource = new List<Product>();
                }
            }

            //RefreshGrid();
        }

        private void cbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //RefreshGrid();
        }

        #endregion

        #region Private Methods

        private void Edit(S5Condition item)
        {
            if (null == item)
                return;

            if (item.FromSource == FromSources.RawMeterial)
            {
                var page = M3CordApp.Pages.AgeingConditionFromRawMaterialView;
                page.Setup(item);
                PageContentManager.Instance.Current = page;
            }
            else if (item.FromSource == FromSources.Twist)
            {
                var page = M3CordApp.Pages.AgeingConditionFromTwistingView;
                page.Setup(item);
                PageContentManager.Instance.Current = page;
            }
            else if (item.FromSource == FromSources.DIP)
            {
                var page = M3CordApp.Pages.AgeingConditionFromDIPView;
                page.Setup(item);
                PageContentManager.Instance.Current = page;
            }
        }

        private void ClearInputs()
        {
            dtIssueDate.SelectedDate = new DateTime?();
            cbSources.SelectedIndex = -1;
            cbMCSides.SelectedIndex = -1;
            cbItemYarns.SelectedIndex = -1;
            txtPalletOrTrace.Text = string.Empty;
            cbCustomers.ItemsSource = null;
            cbCustomers.SelectedIndex = -1;
            cbProducts.ItemsSource = null;
            cbProducts.SelectedIndex = -1;
        }

        private void LoadComboBoxes()
        {
            // From Srouce
            cbSources.ItemsSource = null;

            var fromSources = S5Source.Gets();
            cbSources.ItemsSource = fromSources;

            var mcSides = S5Side.Gets();
            cbMCSides.ItemsSource = mcSides;

            // Item Yarn
            cbItemYarns.ItemsSource = null;

            var itemYarns = CordItemYarn.Gets().Value();
            cbItemYarns.ItemsSource = itemYarns;

            // Customer
            cbCustomers.ItemsSource = null;
            // Product
            cbProducts.ItemsSource = null;
        }

        private void RefreshGrid()
        {
            grid.ItemsSource = null;

            DateTime? issuedate = dtIssueDate.SelectedDate;

            var fromSource = (null != cbSources.SelectedItem) ?
                cbSources.SelectedItem as S5Source : null;
            int? iFromSource = (null != fromSource) ? fromSource.Id : new int?();

            var mcSide = (null != cbMCSides.SelectedItem) ?
                cbMCSides.SelectedItem as S5Side : null;
            string sSide = (null != mcSide) ? mcSide.Code : null;

            string palletOrTrace = (!string.IsNullOrEmpty(txtPalletOrTrace.Text)) ? txtPalletOrTrace.Text.Trim() : null;

            var itemYarn = (null != cbItemYarns.SelectedItem) ? 
                cbItemYarns.SelectedItem as CordItemYarn : null;
            string sItemYarn = (null != itemYarn) ? itemYarn.ItemYarn : null;

            var customer = (null != cbCustomers.SelectedItem) ?
                cbCustomers.SelectedItem as Customer : null;
            string sCustomer = (null != customer) ? customer.CustomerName : null;

            var product = (null != cbProducts.SelectedItem) ?
                cbProducts.SelectedItem as Product : null;
            string sProduct = (null != product) ? product.ProductCode : null;

            int pCnt = 0;

            if (issuedate.HasValue) pCnt++;
            if (iFromSource.HasValue) pCnt++;
            if (null != palletOrTrace) pCnt++;
            if (null != sItemYarn) pCnt++;
            if (null != sCustomer) pCnt++;
            if (null != sProduct) pCnt++;

            if (pCnt <= 0)
            {
                var win = M3CordApp.Windows.MessageBox;

                string msg = "Please enter at least one search condition";
                win.Setup(msg);
                win.ShowDialog();
                return;
            }

            var results = S5Condition.Search(issuedate, iFromSource, sSide, sProduct, sCustomer, palletOrTrace).Value();

            grid.ItemsSource = results;
        }

        #endregion

        #region Public Methods

        public void Setup(bool refresh = false)
        {
            LoadComboBoxes();
            ClearInputs();

            if (refresh)
            {
                RefreshGrid();
            }
        }

        #endregion
    }
}
