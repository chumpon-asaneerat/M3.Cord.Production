#region Using

using M3.Cord.Models;
using NLib.Services;
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

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for CordPlanningPage.xaml
    /// </summary>
    public partial class CordPlanningPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordPlanningPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<CordProduct> allItems;

        #endregion

        #region Button Handlers

        private void cmdHome_Click(object sender, RoutedEventArgs e)
        {
            GotoMainMenu();
        }

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            RefreshGrid();
        }

        private void cmdAddNew_Click(object sender, RoutedEventArgs e)
        {
            var item = new CordProduct();

            var win = M3CordApp.Windows.CordProductPlanning;
            win.Setup(item);
            if (win.ShowDialog() == false) return;

            Cord.LobaclDb.Products.Add(item); // append to local db.
            Cord.LobaclDb.SaveCordProducts();

            RefreshGrid();
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (null == btn) return;
            var item = btn.DataContext as CordProduct;
            if (null == item) return;

            var win = M3CordApp.Windows.CordProductPlanning;
            win.Setup(item);
            if (win.ShowDialog() == false) return;

            Cord.LobaclDb.SaveCordProducts();

            RefreshGrid();
        }

        #endregion

        #region ComboBox Handlers

        private void cbItemYarn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshGrid();
        }

        #endregion

        #region Private Methods

        private void GotoMainMenu()
        {
            // Init Main Menu
            var page = M3CordApp.Pages.CordMainMenu;
            PageContentManager.Instance.Current = page;
        }

        private void LoadComboBoxes()
        {
            cbItemYarn.ItemsSource = LobaclDb.ItemYarns;
            cbItemYarn.SelectedIndex = 0;
        }

        private void RefreshGrid()
        {
            // Reload all items
            allItems = LobaclDb.Products;

            grid.ItemsSource = null;

            List<CordProduct> items;

            if (cbItemYarn.SelectedIndex != -1)
            {
                string itemYarn = LobaclDb.ItemYarns[cbItemYarn.SelectedIndex];

                items = allItems.FindAll((item) => item.ItemYarn == itemYarn);
            }
            else items = allItems;

            grid.ItemsSource = items;
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            Cord.LobaclDb.LoadCordProducts();

            LoadComboBoxes();
            RefreshGrid();
        }

        #endregion
    }
}
