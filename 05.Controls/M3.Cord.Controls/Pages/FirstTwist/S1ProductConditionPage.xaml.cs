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

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for S1ProductConditionPage.xaml
    /// </summary>
    public partial class S1ProductConditionPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public S1ProductConditionPage()
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
            // M3 Cord Document Menu
            /*
            var page = M3CordApp.Pages.M3CordDocumentMenu;
            PageContentManager.Instance.Current = page;
            */
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}
