#region Using

using M3.Cord.Models;
using M3.Cord.Properties;
using NLib.Services;
using System;
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

        private List<FirstTwistMC> mcList = FirstTwistMC.Gets();

        #endregion

        #region Button Handlers

        private void cmdHome_Click(object sender, RoutedEventArgs e)
        {
            GotoMainMenu();
        }

        #endregion

        #region ListBox Handlers

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grid.SelectedIndex == -1)
                return;
            var FirstTwistMC = mcList[grid.SelectedIndex];
            UpdateMCStatus(FirstTwistMC);
        }

        #endregion

        #region Private Methods

        private void GotoMainMenu()
        {
            // Init Main Menu
            var page = M3CordApp.Pages.CordMainMenu;
            PageContentManager.Instance.Current = page;
        }

        private void RefreshMC()
        {
            grid.ItemsSource = null;
            grid.ItemsSource = mcList;
        }

        private void UpdateMCStatus(FirstTwistMC mc)
        {
            paMC.DataContext = null;
            paMC.DataContext = mc.Product;
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
            RefreshMC();
        }

        #endregion
    }
}
