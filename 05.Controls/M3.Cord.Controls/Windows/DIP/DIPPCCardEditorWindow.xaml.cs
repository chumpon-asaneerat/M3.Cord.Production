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
using System.Windows.Shapes;

using NLib.Models;
using M3.Cord.Models;
using NLib;

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for DIPPCCardEditorWindow.xaml
    /// </summary>
    public partial class DIPPCCardEditorWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DIPPCCardEditorWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private DIPPCCard _item = null;

        #endregion

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Customer
            if (null != cbCustomers.SelectedItem && cbCustomers.SelectedItem is Customer)
            {
                var customer = cbCustomers.SelectedItem as Customer;
                if (null != _item)
                {
                    _item.CustomerId = customer.CustomerId;
                    _item.CustomerName = customer.CustomerName;
                }
            }
            // Item Code
            if (null != cbItemCodes.SelectedItem && cbItemCodes.SelectedItem is Product)
            {
                var itemCode = cbItemCodes.SelectedItem as Product;
                if (null != _item)
                {
                    _item.ProductCode = itemCode.ProductCode;
                    _item.ProductName = itemCode.ProductName;
                    _item.ItemYarn = itemCode.ItemYarn;
                    _item.CordStructure = itemCode.CordStructure;
                }
            }

            DialogResult = true;
        }

        #endregion

        #region Combobox Handlers

        private void cbCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null == cbCustomers.ItemsSource)
                return;
            var customer = cbCustomers.SelectedItem as Customer;
            if (null == customer)
                return;
            // Item Code
            cbItemCodes.ItemsSource = null;
            var products = Product.Gets(customer.CustomerName).Value();
            cbItemCodes.ItemsSource = products;

            if (null != _item)
            {
                // Product
                int idx3 = -1;
                if (null != products)
                {
                    idx3 = products.FindIndex(product => { return product.ProductCode == _item.ProductCode; });
                }
                this.InvokeAction(() =>
                {
                    cbItemCodes.SelectedIndex = idx3;
                    if (idx3 > -1)
                    {
                        var product = products[idx3];
                    }
                    else
                    {
                        if (products.Count > 0) cbItemCodes.SelectedIndex = 0;
                    }
                });
            }
        }

        #endregion

        #region Privete Methods

        private void LoadComboBoxes()
        {
            // Customer
            cbCustomers.ItemsSource = null;
            var customers = Customer.Gets().Value();
            cbCustomers.ItemsSource = customers;

            // Item Code
            cbItemCodes.ItemsSource = null;
        }

        #endregion

        #region Public Methods

        public void Setup(DIPPCCard item)
        {
            if (null != item)
            {
                page.HeaderText = "Setup DIP Product for " + item.MCCode;
            }
            else page.HeaderText = "Setup DIP Product";

            LoadComboBoxes();

            var customers = cbCustomers.ItemsSource as List<Customer>;
            var products = cbItemCodes.ItemsSource as List<Product>;

            _item = item;
            this.DataContext = _item;

            if (null != _item)
            {
                // Customer
                int idx2 = -1;
                if (null != customers)
                {
                    idx2 = customers.FindIndex(customer => { return customer.CustomerName == item.CustomerName; });
                }
                this.InvokeAction(() =>
                {
                    cbCustomers.SelectedIndex = idx2;
                    if (idx2 > -1)
                    {
                        var customer = customers[idx2];
                    }
                });

                // Product
                int idx3 = -1;
                if (null != products)
                {
                    idx3 = products.FindIndex(product => { return product.ProductCode == item.ProductCode; });
                }
                this.InvokeAction(() =>
                {
                    cbItemCodes.SelectedIndex = idx3;
                    if (idx3 > -1)
                    {
                        var product = products[idx3];
                    }
                });
            }
        }

        #endregion
    }
}
