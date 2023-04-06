﻿#region Using

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
    /// Interaction logic for G4IssueRawMaterialPage.xaml
    /// </summary>
    public partial class G4IssueRawMaterialPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public G4IssueRawMaterialPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        #endregion

        #region Button Handlers

        private void cmdHome_Click(object sender, RoutedEventArgs e)
        {
            GotoMainMenu();
        }

        #endregion

        #region Textbox Handlers

        private void txtRequestNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter ||
                e.Key == System.Windows.Input.Key.Return)
            {
                RefreshGrid();
                e.Handled = true;
            }
        }

        #endregion

        #region DatePicker Handlers

        private void dtIssueDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshGrid();
        }

        #endregion

        #region Combobox Handlers

        private void cbIssueTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshGrid();
        }

        private void cbItemYarn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshGrid();
        }

        private void cbYarnType_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            var yarnTypes = new string[]
            {
                "All",
                "Weft",
                "Warp"
            };
            cbYarnType.ItemsSource = yarnTypes;
            cbYarnType.SelectedIndex = 0;

            var itemYarns = new string[]
            {
                "700-108-178E-TTS",
                "470-72-1781-JJ",
                "470-136-178E-APM",
                "470-136-178E-TTS"
            };
            cbItemYarn.ItemsSource = itemYarns;
            cbItemYarn.SelectedIndex = 0;

            var issUeTo = new string[]
            {
                "Wrap AB",
                "Weft AB",
                "Wrap AD",
                "Weft AD"
            };
            cbIssueTo.ItemsSource = issUeTo;
            cbIssueTo.SelectedIndex = 0;
        }

        private void RefreshGrid()
        {
            grid.ItemsSource = null;
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            LoadComboBoxes();
        }

        #endregion
    }
}
