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
    /// Interaction logic for CordMainMenu.xaml
    /// </summary>
    public partial class CordMainMenuPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordMainMenuPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        private void cmdReceiveRawMaterial_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            /*
            // G4 - Receive Raw Material
            var page = M3CordApp.Pages.G4ReceiveRawMaterial;
            page.Setup();
            PageContentManager.Instance.Current = page;
            */
        }

        private void cmdIssueRawMaterial_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            /*
            // G4 - Issue Raw Material
            var page = M3CordApp.Pages.G4IssueRawMaterial;
            page.Setup();
            PageContentManager.Instance.Current = page;
            */
        }

        private void cmdCheckStock_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            /*
            // G4 - Check Stock
            var page = M3CordApp.Pages.G4Stock;
            page.Setup();
            PageContentManager.Instance.Current = page;
            */
        }

        private void cmdReceiveReturnMaterial_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            /*
            // G4 - Receive Return Material
            */
        }

        private void cmdReceiveYarn_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            /*
            // Cord Receive Yarn
            var page = M3CordApp.Pages.WareHouseReceiveYarn;
            page.Setup();
            PageContentManager.Instance.Current = page;
            */
        }

        private void cmdPlaning_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            /*
            // Cord Planning
            var page = M3CordApp.Pages.WarehousePlanning;
            page.Setup();
            PageContentManager.Instance.Current = page;
            */
        }

        private void cmdFirstTwist_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            // First Twist
            /*
            var page = M3CordApp.Pages.FirstTwistMC;
            page.Setup();
            PageContentManager.Instance.Current = page;
            */
        }

        private void cmdAging_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            // Ageing
        }

        private void cmdDipping_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            // Dipping
        }

        #endregion
    }
}
