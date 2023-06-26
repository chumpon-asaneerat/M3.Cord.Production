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
    /// Interaction logic for M3DocumentMenuPage.xaml
    /// </summary>
    public partial class M3DocumentMenuPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public M3DocumentMenuPage()
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

        private void cmdS1ProductCondition_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.S1ProductCondition;
            PageContentManager.Instance.Current = page;
        }

        private void cmdS4x1ProductCondition_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.S4x1UpTwisterCondition;
            PageContentManager.Instance.Current = page;
        }

        private void cmdS4x2ProductCondition_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.S4x2UpTwisterCondition;
            PageContentManager.Instance.Current = page;
        }

        #endregion
    }
}
