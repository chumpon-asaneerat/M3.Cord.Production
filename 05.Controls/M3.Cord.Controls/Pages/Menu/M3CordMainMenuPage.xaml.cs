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
            var page = M3CordApp.Pages.AgeingOperation;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdDipping_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            // Dipping
            var page = M3CordApp.Pages.DIPMC;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdMasterDataMenu_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;

            if (null == M3CordApp.Current.User)
            {
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("ไม่พบข้อมูลผู้ใช้ในระบบ");
                msgbox.ShowDialog();
                return;
            }
            if (M3CordApp.Current.User.RoleId > 10)
            {
                // Role 1 : Admin
                // Role 10 : Supervisor
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("ผู้ใช้ปัจจุบันไม่มีสิทธิเข้าถึงหน้าจอนี้ได้");
                msgbox.ShowDialog();
                return;
            }

            // Master Data
            var page = M3CordApp.Pages.MasterDataMenu;
            PageContentManager.Instance.Current = page;
        }

        private void cmdProductinReport_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;

            if (null == M3CordApp.Current.User)
            {
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("ไม่พบข้อมูลผู้ใช้ในระบบ");
                msgbox.ShowDialog();
                return;
            }
            if (M3CordApp.Current.User.RoleId > 10)
            {
                // Role 1 : Admin
                // Role 10 : Supervisor
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("ผู้ใช้ปัจจุบันไม่มีสิทธิเข้าถึงหน้าจอนี้ได้");
                msgbox.ShowDialog();
                return;
            }

            // Production Report
            var page = M3CordApp.Pages.ProductionReport;
            //page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdChemicalRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Solution Main Menu
            var page = M3CordApp.Pages.SolutionMenu;
            //page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdQA_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            // Check Stock Yarn
            var page = M3CordApp.Pages.QAMenu;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdTestUI_Click(object sender, RoutedEventArgs e)
        {
            // Test UI Page
            var page = M3CordApp.Pages.TestUI;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            // when open main menu signout all case
            SignInManager.Instance.Signout();
        }

        #endregion
    }
}
