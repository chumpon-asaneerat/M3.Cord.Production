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

        }

        #endregion

        #region Tab Handlers

        private void tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        public void Setup()
        {

        }

        #endregion
    }
}
