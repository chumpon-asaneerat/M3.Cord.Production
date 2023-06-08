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
    /// Interaction logic for FirstTwistMCPage.xaml
    /// </summary>
    public partial class FirstTwistMCPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FirstTwistMCPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<FirstTwistMC> machines;
        private FirstTwistMC selectedMC;

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

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            if (null == selectedMC)
                return;
            var mc = selectedMC;

            /*
            var win = M3CordApp.Windows.ChooseCordProduct;
            win.Setup();
            if (win.ShowDialog() == false) return;
            if (null != win.SelectedProduct)
            {
                AddNew(mc, win.SelectedProduct);
            }
            */
        }

        #endregion

        #region ListBox Handlers

        private void mcList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != mcList.SelectedItem)
            {
                selectedMC = mcList.SelectedItem as FirstTwistMC;
                UpdateMCStatus(selectedMC);
            }
        }

        #endregion

        #region Private Methods

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

        private void ResetControls()
        {

        }

        private void RefreshMC()
        {
            selectedMC = null;
            mcList.ItemsSource = null;
            mcList.ItemsSource = machines;
        }

        private void UpdateMCStatus(FirstTwistMC mc)
        {
            //cmdAddNew.IsEnabled = false;

            //paMC.DataContext = null;
            if (null != mc)
            {
                //paMC.DataContext = mc.Product;
                //cmdAddNew.IsEnabled = (null == mc.Product);
            }
            RefreshGrid(mc);
        }

        private void RefreshGrid(FirstTwistMC mc)
        {
            /*
            doffGrid.ItemsSource = null;
            if (null == mc)
                return;
            doffGrid.ItemsSource = mc.RawMaterialSheets;
            */
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            Cord.LobaclDb.LoadMachines();
            machines = Cord.LobaclDb.Machines;

            ResetControls();
            RefreshMC();
        }

        #endregion
    }
}
