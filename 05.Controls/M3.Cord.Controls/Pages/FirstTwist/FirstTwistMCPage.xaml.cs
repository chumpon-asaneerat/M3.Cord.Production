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

            var win = M3CordApp.Windows.ChoosePCCardTwist1;
            win.Setup();
            if (win.ShowDialog() == false) return;
            if (null != win.SelectedPCCard)
            {
                AddNew(mc, win.SelectedPCCard);
            }
        }

        private void cmdLoadYarn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdNewCondition_Click(object sender, RoutedEventArgs e)
        {

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

        }

        #endregion

        #region Private Methods

        private void AddNew(FirstTwistMC mc, PCCard pccard)
        {
            if (null != mc && null != pccard)
            {
                /*
                var ret = YarnLoadSheet.AddNew(mc, product);
                if (ret.Ok)
                {

                }
                */
            }
            UpdateMCStatus(mc);
        }

        private void UpdateMCStatus(FirstTwistMC mc)
        {
            cmdAdd.IsEnabled = false;

            //loadSheet = null;
            paMC.DataContext = null;

            if (null != mc)
            {
                /*
                loadSheet = YarnLoadSheet.Get(mc.MCCode).Value();
                // Binding
                paMC.DataContext = loadSheet;
                cmdAdd.IsEnabled = (null == loadSheet);
                */
            }
            // update tabs data context for dynamic template switching
            tabs.DataContext = paMC.DataContext;

            this.InvokeAction(() =>
            {
                RefreshGrid();
            });
        }

        private void RefreshMC()
        {
            mcList.ItemsSource = null;
            selectedMC = null;
            mcList.ItemsSource = machines;
        }

        private void RefreshGrid()
        {

        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            machines = FirstTwistMC.Gets().Value();
            RefreshMC();
        }

        #endregion
    }
}
