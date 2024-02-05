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
    /// Interaction logic for ProductItemCodeManagePage.xaml
    /// </summary>
    public partial class ProductItemCodeManagePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ProductItemCodeManagePage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<Product> _items = null;

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
            string itemCode = txtItemCode.Text.Trim();
            txtItemCode.Text = string.Empty;
            if (string.IsNullOrEmpty(itemCode)) return;

            var item = new Product() { ProductId = new int?(), ProductCode = itemCode };
            Product.Save(item);

            this.InvokeAction(() =>
            {
                RefreshGrid();
            });
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as FontAwesomeButton;
            if (null == btn) return;
            var item = btn.DataContext as Product;
            Product.Delete(item);

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
                    Product.Save(item);
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
            _items = Product.Gets().Value();
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