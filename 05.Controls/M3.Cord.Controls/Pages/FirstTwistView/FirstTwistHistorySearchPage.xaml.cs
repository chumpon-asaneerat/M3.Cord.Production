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
    /// Interaction logic for FirstTwistHistorySearchPage.xaml
    /// </summary>
    public partial class FirstTwistHistorySearchPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FirstTwistHistorySearchPage()
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
            M3CordApp.Pages.GotoProductionReportMenu();
        }

        private void cmdView_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (null == btn) return;
            var item = btn.DataContext as PCCard;
            if (null == item) return;
        }

        #endregion

        #region TextBox Handlers

        private void txtCustomer_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                RefreshGrid();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                // clear trace no.
                txtCustomer.Text = string.Empty;
                e.Handled = true;
            }
        }

        private void txtLotNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                RefreshGrid();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                // clear trace no.
                txtLotNo.Text = string.Empty;
                e.Handled = true;
            }
        }

        #endregion

        #region Combobox Handlers

        private void cbItemYarns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshGrid();
        }

        #endregion

        #region Private Methods

        private void ResetControls()
        {

        }

        private void LoadComboBoxes()
        {
            /*
            cbItemYanrs.ItemsSource = null;

            var itemYarns = CordItemYarn.Gets().Value();
            cbItemYarns.ItemsSource = itemYarns;

            this.InvokeAction(() =>
            {
                if (null != itemYarns && itemYarns.Count > 0) cbItemYarns.SelectedIndex = 0;
            });
            */
        }

        private void RefreshGrid()
        {
            grid.ItemsSource = null;
            /*
            var itemYarn = (null != cbItemYarns.SelectedItem) ?
                cbItemYarns.SelectedItem as CordItemYarn : null;
            */
            string lotNo = txtLotNo.Text.Trim();
            string customer = txtCustomer.Text.Trim();

            PCCardService.Instance.Search(lotNo, customer);

            grid.ItemsSource = PCCardService.Instance.PCCards;
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            ResetControls();
            LoadComboBoxes();
            RefreshGrid();
        }

        #endregion
    }
}
