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
        /// Constructor.
        /// </summary>
        public G4IssueRawMaterialPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        #endregion

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ResetControls();
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

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region TextBox Hqndlers

        private void txtRequsetNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter ||
                e.Key == Key.Return)
            {
                //RefreshGrid();
                e.Handled = true;
            }
        }

        private void txtPalletNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter ||
                e.Key == Key.Return)
            {
                //MarkIssue();
                e.Handled = true;
            }
        }

        #endregion

        #region Combobox Handlers

        private void cbItemYanrs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshGrid();
        }

        #endregion


        #region Private Methods

        private void ResetControls()
        {
            dtIssueDate.SelectedDate = DateTime.Now;
            txtRequsetNo.Text = string.Empty;
            txtPalletNo.Text = string.Empty;
        }

        private void LoadComboBoxes()
        {
            cbItemYanrs.ItemsSource = null;

            var itemYarns = CordItemYarn.Gets().Value();
            cbItemYanrs.ItemsSource = itemYarns;

            if (null != itemYarns && itemYarns.Count > 0) cbItemYanrs.SelectedIndex = 0;
        }

        private void RefreshGrid()
        {
            var itemYarn = (null != cbItemYanrs.SelectedItem) ? 
                cbItemYanrs.SelectedItem as CordItemYarn : null;

            grid.ItemsSource = null;

            if (null != itemYarn)
            {
                grid.ItemsSource = null;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup()
        {
            ResetControls();
            LoadComboBoxes();
            RefreshGrid();
        }

        #endregion
    }
}
