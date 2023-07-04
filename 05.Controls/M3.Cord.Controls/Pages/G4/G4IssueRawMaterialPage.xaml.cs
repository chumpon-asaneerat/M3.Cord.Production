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
            G4IssueYarnService.Instance.Clear();
            this.InvokeAction(() =>
            {
                RefreshGrid();
            });
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            bool success = G4IssueYarnService.Instance.SaveIssueYarns();
            // Show MessageBox
            string msg = (success) ? "Save Success" : "Save Failed";
            var win = M3CordApp.Windows.MessageBox;
            win.Setup(msg);
            win.ShowDialog();

            if (success)
            {
                this.InvokeAction(() =>
                {
                    ResetControls();
                    RefreshGrid();
                    txtRequsetNo.FocusControl();
                });
            }
        }

        #endregion

        #region TextBox Hqndlers

        private void txtRequsetNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter ||
                e.Key == Key.Return)
            {
                CheckEnable();

                G4IssueYarnService.Instance.IssueDate = dtIssueDate.SelectedDate;

                this.InvokeAction(() =>
                {
                    RefreshGrid();
                    txtPalletNo.FocusControl();
                });
                e.Handled = true;
            }
        }

        private void txtPalletNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter ||
                e.Key == Key.Return)
            {
                string palletNo = txtPalletNo.Text.Trim();

                txtPalletNo.Text = string.Empty; // clear input
                string requestNo = txtRequsetNo.Text.Trim();

                if (string.IsNullOrWhiteSpace(requestNo))
                {
                    // No request no specificed.
                    string msg = "Please specific Request No.";
                    var win = M3CordApp.Windows.MessageBox;
                    win.Setup(msg);
                    win.ShowDialog();

                    this.InvokeAction(() =>
                    {
                        txtRequsetNo.FocusControl();
                    });

                    e.Handled = true;
                    return;
                }

                G4IssueYarnService.Instance.IssueDate = dtIssueDate.SelectedDate;
                G4IssueYarnService.Instance.MarkIssue(requestNo, palletNo);

                this.InvokeAction(() =>
                {
                    RefreshGrid();
                });
                e.Handled = true;
            }
        }

        #endregion

        #region Combobox Handlers

        private void cbItemYanrs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.InvokeAction(() =>
            {
                RefreshGrid();
            });
        }

        #endregion

        #region Private Methods

        private void ResetControls()
        {
            dtIssueDate.SelectedDate = DateTime.Now;
            txtRequsetNo.Text = string.Empty;
            txtPalletNo.Text = string.Empty;

            CheckEnable();
        }

        private void CheckEnable()
        {
            bool hasRequestId = (!string.IsNullOrEmpty(txtRequsetNo.Text));

            txtPalletNo.IsEnabled = true;
            dtIssueDate.IsEnabled = true;
            cbItemYanrs.IsEnabled = true;
            /*
            txtPalletNo.IsEnabled = hasRequestId;
            dtIssueDate.IsEnabled = hasRequestId;
            cbItemYanrs.IsEnabled = hasRequestId;
            */
        }

        private void LoadComboBoxes()
        {
            cbItemYanrs.ItemsSource = null;

            var itemYarns = CordItemYarn.Gets().Value();
            cbItemYanrs.ItemsSource = itemYarns;

            this.InvokeAction(() =>
            {
                if (null != itemYarns && itemYarns.Count > 0) cbItemYanrs.SelectedIndex = 0;
            });
        }

        private void RefreshGrid()
        {
            var itemYarn = (null != cbItemYanrs.SelectedItem) ? 
                cbItemYanrs.SelectedItem as CordItemYarn : null;

            string requestNo = txtRequsetNo.Text.Trim();
            G4IssueYarnService.Instance.RequestNo = requestNo;
            G4IssueYarnService.Instance.IssueDate = dtIssueDate.SelectedDate;

            grid.ItemsSource = null;

            if (null != itemYarn)
            {
                if (null != itemYarn)
                {
                    G4IssueYarnService.Instance.LoadYanStocks(itemYarn.ItemYarn);
                }
            }

            grid.ItemsSource = G4IssueYarnService.Instance.IssueItems;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup()
        {
            this.DataContext = G4IssueYarnService.Instance;

            ResetControls();
            LoadComboBoxes();
            RefreshGrid();

            this.InvokeAction(() =>
            {
                txtRequsetNo.FocusControl();
            });
        }

        #endregion
    }
}
