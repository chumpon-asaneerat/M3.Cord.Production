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
    /// Interaction logic for M3CordMainMenuPage.xaml
    /// </summary>
    public partial class M3CordMainMenuPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public M3CordMainMenuPage()
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

        private void cmdReceiveRawMaterial_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            // G4 - Receive Raw Material
            var page = M3CordApp.Pages.G4ReceiveRawMaterial;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdIssueRawMaterial_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            // G4 - Issue Raw Material
            var page = M3CordApp.Pages.G4IssueRawMaterial;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdCheckStock_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            // G4 - Check Stock
            var page = M3CordApp.Pages.G4Stock;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdReceiveReturnMaterial_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            // G4 - Receive Return Material
            var page = M3CordApp.Pages.G4ReceiveReturnMeterial;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdReceiveYarn_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            // Warehouse Receive Yarn
            var page = M3CordApp.Pages.WarehouseReceiveYarn;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdPCCard_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            // PC Card
            var page = M3CordApp.Pages.PCCardManage;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdCheckStockYarn_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            // Check Stock Yarn
            var page = M3CordApp.Pages.WarehouseYarnStock;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdFirstTwist_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            // First Twist
            var page = M3CordApp.Pages.FirstTwistMC;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdAging_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            // Ageing
            var page = M3CordApp.Pages.AgeingManage;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdDipping_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            // Dipping
            var page = M3CordApp.Pages.DIPOperation;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdMasterDataMenu_Click(object sender, RoutedEventArgs e)
        {
            // Master Data
            var page = M3CordApp.Pages.MasterDataMenu;
            PageContentManager.Instance.Current = page;
        }

        private void cmdPalletSetting_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            // Pallet Setting
            var page = M3CordApp.Pages.PalletSettingManage;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdChemicalRecipe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdCordDocumentMenu_Click(object sender, RoutedEventArgs e)
        {
            // M3 Cord Document Menu
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            // when open main menu signout all case
            SignInManager.Instance.Signout();
        }

        #endregion

        private void cmdDIPSlip_Click(object sender, RoutedEventArgs e)
        {
            var items = DIPPalletSlip.GetSamples();
            // G4 - Issue Raw Material
            var page = M3CordApp.Pages.DIPPalletSlipPreview;
            page.Setup(items, false);
            PageContentManager.Instance.Current = page;
        }
    }
}
