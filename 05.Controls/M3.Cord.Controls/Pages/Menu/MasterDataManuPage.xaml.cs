#region Using

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
    /// Interaction logic for MasterDataManuPage.xaml
    /// </summary>
    public partial class MasterDataManuPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MasterDataManuPage()
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

        private void cmdMainMenu_Click(object sender, RoutedEventArgs e)
        {
            M3CordApp.Pages.GotoCordMainMenu();
        }

        private void cmdCustomer_Click(object sender, RoutedEventArgs e)
        {
            // Customer Manage
            var page = M3CordApp.Pages.CustomerManage;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdProductItemCode_Click(object sender, RoutedEventArgs e)
        {
            // Product Item Code Manage
            var page = M3CordApp.Pages.ProductItemCodeManage;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdUser_Click(object sender, RoutedEventArgs e)
        {
            // User Manage
            var page = M3CordApp.Pages.UserManage;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdS1StdCondiction_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.S1StdManage;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdS4x1StdCondiction_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.S4x1StdManage;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdS4x2StdCondiction_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.S4x2StdManage;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmd5StdCondiction_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.S5StdManage;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdDipCondition_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.DippingStdManage;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdS8BeforeCondition_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.S8BeforeStdManage;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdS8ProductCondition_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.S8ProductionConditionItemStdManage;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdDipTimeTableCondition_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.DipTimeTableStdManage;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        #endregion
    }
}
