#region Using

using M3.Cord.Models;
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

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for ChooseCordProductWindow.xaml
    /// </summary>
    public partial class ChooseCordProductWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ChooseCordProductWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<CordProduct> allItems;

        #endregion

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdChoose_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (null == btn) return;
            var item = btn.DataContext as CordProduct;
            if (null == item) return;

            SelectedProduct = item;

            DialogResult = true;
        }

        #endregion

        #region Privete Methods

        private void RefreshGrid()
        {
            // Reload all items
            allItems = LobaclDb.Products;

            grid.ItemsSource = null;

            List<CordProduct> items;
            items = allItems.FindAll((item) => !item.IsUsed);

            grid.ItemsSource = items;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup()
        {
            if (null == LobaclDb.Products || LobaclDb.Products.Count <= 0)
            {
                LobaclDb.LoadCordProducts();
            }

            SelectedProduct = null;
            RefreshGrid();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Selected Product.
        /// </summary>
        public CordProduct SelectedProduct { get; private set; }

        #endregion
    }
}
