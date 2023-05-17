#region Using

using M3.Cord.Models;
using M3.Cord.Properties;
using NLib.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for FirstTwistMCPage.xaml
    /// </summary>
    public partial class FirstTwistMCPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public FirstTwistMCPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<FirstTwistMC> mcList;
        private FirstTwistMC selectedMC;

        #endregion

        #region Button Handlers

        private void cmdHome_Click(object sender, RoutedEventArgs e)
        {
            GotoMainMenu();
        }

        private void cmdAddNew_Click(object sender, RoutedEventArgs e)
        {
            if (null == selectedMC)
                return;
            var mc = selectedMC;

            var win = M3CordApp.Windows.ChooseCordProduct;
            win.Setup();
            if (win.ShowDialog() == false) return;
            if (null != win.SelectedProduct)
            {
                AddNew(mc, win.SelectedProduct);
            }
        }

        private void cmdPrepare_Click(object sender, RoutedEventArgs e)
        {
            if (null == selectedMC)
                return;
            var mc = selectedMC;

            var win = M3CordApp.Windows.YarnLoading;
            win.Setup();
            if (win.ShowDialog() == false) return;

            if (null != mc)
            {
                mc.RawMaterialSheets = RawMaterialSheetItem.GetRawMaterialSheets("S-1");
                RefreshGrid(mc);
            }
        }

        #endregion

        #region ListBox Handlers

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != grid.SelectedItem)
            {
                selectedMC = grid.SelectedItem as FirstTwistMC;
                UpdateMCStatus(selectedMC);
            }
        }

        #endregion

        #region Private Methods

        private void GotoMainMenu()
        {
            // Init Main Menu
            var page = M3CordApp.Pages.CordMainMenu;
            PageContentManager.Instance.Current = page;
        }

        private void AddNew(FirstTwistMC mc, CordProduct product)
        {
            if (null != product)
            {
                product.IsUsed = true; // mark is used.
                Cord.LobaclDb.SaveCordProducts();
            }

            mc.Product = product;
            UpdateMCStatus(mc);
            Cord.LobaclDb.SaveMachines();
            
        }

        private void RefreshMC()
        {
            selectedMC = null;
            grid.ItemsSource = null;
            grid.ItemsSource = mcList;
        }

        private void UpdateMCStatus(FirstTwistMC mc)
        {
            cmdAddNew.IsEnabled = false;

            paMC.DataContext = null;
            if (null != mc)
            {
                paMC.DataContext = mc.Product;
                cmdAddNew.IsEnabled = (null == mc.Product);
            }
            RefreshGrid(mc);
        }

        private void RefreshGrid(FirstTwistMC mc)
        {
            doffGrid.ItemsSource = null;
            if (null == mc)
                return;
            doffGrid.ItemsSource = mc.RawMaterialSheets;
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            Cord.LobaclDb.LoadMachines();
            mcList = Cord.LobaclDb.Machines;

            RefreshMC();
        }

        #endregion
    }
}
