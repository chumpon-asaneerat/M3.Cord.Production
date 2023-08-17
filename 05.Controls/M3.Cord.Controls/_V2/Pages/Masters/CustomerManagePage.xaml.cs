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
using NLib.Wpf.Controls;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for CustomerManagePage.xaml
    /// </summary>
    public partial class CustomerManagePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CustomerManagePage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<Customer> _items = null;

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
            M3CordApp.Pages.GotoCordMasterMenu();
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            string customerName = txtCustomerName.Text.Trim();
            txtCustomerName.Text = string.Empty;
            if (string.IsNullOrEmpty(customerName)) return;

            var item = new Customer() { CustomerId = new int?(), CustomerName = customerName };
            Customer.Save(item);

            this.InvokeAction(() =>
            {
                RefreshGrid();
            });
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as FontAwesomeButton;
            if (null == btn) return;
            var item = btn.DataContext as Customer;
            Customer.Delete(item);

            this.InvokeAction(() =>
            {
                RefreshGrid();
            });
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (null != _items && _items.Count > 0)
            {
                foreach (var item in _items)
                {
                    Customer.Save(item);
                }
                this.InvokeAction(() =>
                {
                    RefreshGrid();
                });
            }
        }

        #endregion

        #region Private Methods

        private void RefreshGrid()
        {
            grid.ItemsSource = null;
            _items = Customer.Gets().Value();
            grid.ItemsSource = _items;
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            RefreshGrid();
        }

        #endregion
    }
}
