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
    /// Interaction logic for SolutionSlipManagePage.xaml
    /// </summary>
    public partial class SolutionSlipManagePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SolutionSlipManagePage()
        {
            InitializeComponent();
        }

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
            //M3CordApp.Pages.GotoProductionReportMenu();
        }

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            RefreshGrid();
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdCreate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdRePrint_Click(object sender, RoutedEventArgs e)
        {
            /*
            var button = sender as Button;
            var ctx = (null != button) ? button.DataContext : null;
            var item = (null != ctx) ? ctx as PalletSetting : null;
            if (null == item) return;

            // Show Print Preview
            var page = M3CordApp.Pages.PalletSettingPreview;
            var items = new List<PalletSetting>();
            items.Add(item);
            page.Setup(items, true); // reprint
            PageContentManager.Instance.Current = page;
            */
        }

        #endregion

        #region Combobox Handlers

        private void cbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshGrid();
        }

        #endregion

        #region TextBox Handlers

        private void txtProductLotNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RefreshGrid();
                e.Handled = true;
            }
        }

        #endregion

        #region Private Methods

        private void RefreshGrid()
        {

        }

        #endregion
    }
}
