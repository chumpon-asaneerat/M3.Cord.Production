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
        private RawMaterialSheet rawMatSheet;

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

            var win = M3CordApp.Windows.ChooseCordProduct;
            win.Setup();
            if (win.ShowDialog() == false) return;
            if (null != win.SelectedProduct)
            {
                AddNew(mc, win.SelectedProduct);
            }
        }

        private void cmdLoadYarn_Click(object sender, RoutedEventArgs e)
        {
            // Prepare Doff
            if (null == selectedMC || null == rawMatSheet)
                return;
            var mc = selectedMC;

            var win = M3CordApp.Windows.PrepareDoff;
            win.Setup(rawMatSheet);
            if (win.ShowDialog() == false) return;
            var items = win.Items;
            if (null != items)
            {
                items.ForEach(item => 
                { 
                    var ret = RawMaterialSheetItem.Save(item); 
                    if (!ret.Ok)
                    {
                        Console.WriteLine(ret.ErrMsg);
                    }
                });
            }
        }

        private void cmdNewCondition_Click(object sender, RoutedEventArgs e)
        {
            // Prepare Doff
            if (null == selectedMC || null == rawMatSheet)
                return;
            var mc = selectedMC;

            var win = M3CordApp.Windows.S1Condition;
            win.Setup(mc, rawMatSheet);
            if (win.ShowDialog() == false) return;
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

        #region Tab Handlers

        private void tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshGrid();
        }

        #endregion

        #region Private Methods

        private void AddNew(FirstTwistMC mc, CordProduct product)
        {
            if (null != mc && null != product)
            {
                var ret = RawMaterialSheet.AddNew(mc, product);
                if (ret.Ok)
                {

                }
            }
            UpdateMCStatus(mc);
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
            cmdAdd.IsEnabled = false;

            rawMatSheet = null;
            paMC.DataContext = null;

            if (null != mc)
            {
                rawMatSheet = RawMaterialSheet.Get(mc.MCCode).Value();
                // Binding
                paMC.DataContext = rawMatSheet;
                cmdAdd.IsEnabled = (null == rawMatSheet);
            }
            // update tabs data context for dynamic template switching
            tabs.DataContext = paMC.DataContext;

            this.InvokeAction(() => 
            {
                RefreshGrid();
            });
        }

        private void RefreshGrid()
        {
            if (tabs.SelectedIndex == 0)
            {
                gridRawMat.ItemsSource = null;
                if (null == rawMatSheet)
                    return;
                gridRawMat.ItemsSource = RawMaterialSheetItem.Gets(rawMatSheet.RawMaterialSheetId).Value();
            }
            else if (tabs.SelectedIndex == 1)
            {

            }
            else if (tabs.SelectedIndex == 2)
            {

            }
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            machines = FirstTwistMC.Gets().Value();

            ResetControls();
            RefreshMC();
        }

        #endregion
    }
}
