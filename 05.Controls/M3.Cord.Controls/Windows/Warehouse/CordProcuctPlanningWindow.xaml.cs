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
using System.Windows.Shapes;

using NLib.Models;
using M3.Cord.Models;
using NLib;

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for CordProcuctPlanningWindow.xaml
    /// </summary>
    public partial class CordProcuctPlanningWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordProcuctPlanningWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private CordProduct _item = null;

        #endregion

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Item Yarn
            if (null != cbItemYanrs.SelectedItem && cbItemYanrs.SelectedItem is CordItemYarn)
            {
                var itemYarn = cbItemYanrs.SelectedItem as CordItemYarn;
                if (null != _item)
                {
                    _item.ItemYarn = itemYarn.ItemYarn;
                }
            }
            // Customer
            if (null != cbCustomers.SelectedItem && cbCustomers.SelectedItem is Customer)
            {
                var customer = cbCustomers.SelectedItem as Customer;
                if (null != _item)
                {
                    _item.CustomerName = customer.CustomerName;
                }
            }
            // Item Code
            if (null != cbItemCodes.SelectedItem && cbItemCodes.SelectedItem is ProductItemCode)
            {
                var itemCode = cbItemCodes.SelectedItem as ProductItemCode;
                if (null != _item)
                {
                    _item.ItemCode = itemCode.ItemCode;
                }
            }

            DialogResult = true;
        }

        #endregion

        #region Privete Methods

        private void LoadComboBoxes()
        {
            // Item Yarn
            cbItemYanrs.ItemsSource = null;

            var itemYarns = CordItemYarn.Gets().Value();
            cbItemYanrs.ItemsSource = itemYarns;

            // Customer
            cbCustomers.ItemsSource = null;
            var customers = Customer.Gets().Value();
            cbCustomers.ItemsSource = customers;

            // Item Code
            cbItemCodes.ItemsSource = null;
            var itemCodes = ProductItemCode.Gets().Value();
            cbItemCodes.ItemsSource = itemCodes;
        }

        #endregion

        #region Public Methods

        public void Setup(CordProduct item)
        {
            LoadComboBoxes();

            var itemYarns = cbItemYanrs.ItemsSource as List<CordItemYarn>;
            var customers = cbCustomers.ItemsSource as List<Customer>;
            var itemCodes = cbItemCodes.ItemsSource as List<ProductItemCode>;

            _item = item;
            this.DataContext = _item;

            if (null != _item)
            {
                _item.CalcTotals();

                // ItemYarn
                int idx = -1;
                if (null != itemYarns)
                {
                    idx = itemYarns.FindIndex(yarn => { return yarn.ItemYarn == item.ItemYarn; });
                }
                this.InvokeAction(() =>
                {
                    cbItemYanrs.SelectedIndex = idx;
                });

                // Customer
                int idx2 = -1;
                if (null != customers)
                {
                    idx2 = customers.FindIndex(customer => { return customer.CustomerName == item.CustomerName; });
                }
                this.InvokeAction(() =>
                {
                    cbCustomers.SelectedIndex = idx2;
                });

                // Item Code
                int idx3 = -1;
                if (null != itemCodes)
                {
                    idx3 = itemCodes.FindIndex(itemCode => { return itemCode.ItemCode == item.ItemCode; });
                }
                this.InvokeAction(() =>
                {
                    cbItemCodes.SelectedIndex = idx3;
                });
            }
        }

        #endregion
    }
}
