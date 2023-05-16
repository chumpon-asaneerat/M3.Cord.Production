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
using M3.Cord.Models.V0;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for G4StockPage.xaml
    /// </summary>
    public partial class G4StockPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public G4StockPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<G4StockYarn> sources = G4StockYarn.GetG4StockYarns();
        private List<G4StockYarn> items = null;

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

            string itemYarn = cbItemYarn.SelectedItem as string;
            items = sources.FindAll((item) =>
            {
                return (itemYarn != null && item.ItemYarn == itemYarn);
            });

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
