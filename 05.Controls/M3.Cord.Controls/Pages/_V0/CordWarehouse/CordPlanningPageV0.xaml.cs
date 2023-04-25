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

namespace M3.Cord.Pages.V0
{
    /// <summary>
    /// Interaction logic for CordPlanningPage.xaml
    /// </summary>
    public partial class CordPlanningPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordPlanningPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<CordProduct> items = CordProduct.GetCordProducts();

        #endregion

        #region Button Handlers

        private void cmdHome_Click(object sender, RoutedEventArgs e)
        {
            GotoMainMenu();
        }

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            RefreshGrid();
        }

        private void cmdAddNew_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Private Methods

        private void GotoMainMenu()
        {
            // Init Main Menu
            var page = M3CordApp.Pages.CordMainMenu;
            PageContentManager.Instance.Current = page;
        }

        private void LoadComboBoxes()
        {
            var itemYarns = new string[]
            {
                "700-108-178E-TTS",
                "470-72-1781-JJ",
                "470-136-178E-APM",
                "470-136-178E-TTS"
            };
            cbItemYarn.ItemsSource = itemYarns;
            cbItemYarn.SelectedIndex = 0;
        }

        private void RefreshGrid()
        {
            grid.ItemsSource = null;

            grid.ItemsSource = items;
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            LoadComboBoxes();
            RefreshGrid();
        }

        #endregion
    }
}
