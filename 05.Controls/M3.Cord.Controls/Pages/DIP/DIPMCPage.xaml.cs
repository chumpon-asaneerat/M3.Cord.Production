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
    /// Interaction logic for DIPMCPage.xaml
    /// </summary>
    public partial class DIPMCPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DIPMCPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<DIPMC> machines;
        private DIPMC selectedMC;

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

        private void cmd_Click(object sender, RoutedEventArgs e)
        {
            var ctrl = sender as Button;
            var dc = (null != ctrl) ? ctrl.DataContext : null;
            selectedMC = (null != dc) ? dc as DIPMC : null;
            if (null != selectedMC)
            {
                // Show MC Operation Menu.
                var page = M3CordApp.Pages.DIPOperation;
                if (page.Setup(selectedMC))
                {
                    PageContentManager.Instance.Current = page;
                }
                else
                {
                    // DIP PC Card Not found
                }
            }
        }

        #endregion

        #region Private Methods

        private void RefreshMC()
        {
            mcList.ItemsSource = null;
            selectedMC = null;
            mcList.ItemsSource = machines;
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            machines = DIPMC.Gets("S-7").Value();
            RefreshMC();
        }

        #endregion
    }
}
