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
    /// Interaction logic for AgeingConditionFromRawMeterialPage.xaml
    /// </summary>
    public partial class AgeingConditionFromRawMeterialPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public AgeingConditionFromRawMeterialPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private S5ConditionManager manager = null;

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
            M3CordApp.Pages.GotoAgeingOperationMenu();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void cmdStart_Click(object sender, RoutedEventArgs e)
        {
            Start();
        }

        private void cmdFinish_Click(object sender, RoutedEventArgs e)
        {
            Finish();
        }

        #endregion

        #region TextBox Handlers

        private void txtPalletNo1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string palletCode = txtPalletNo1.Text;
                UpdatePallet1(palletCode);
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                txtPalletNo1.Text = string.Empty;
                UpdatePallet1(null);
                e.Handled = true;
            }
        }

        private void txtPalletNo2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string palletCode = txtPalletNo2.Text;
                UpdatePallet2(palletCode);
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                txtPalletNo2.Text = string.Empty;
                UpdatePallet2(null);
                e.Handled = true;
            }
        }

        #endregion

        #region Private Methods

        private void UpdatePallet1(string palletCode)
        {
            if (null != manager)
            {
                string errMsg;
                if (!manager.SetPallet1(palletCode, out errMsg))
                {
                    var win = M3CordApp.Windows.MessageBox;
                    win.Setup(errMsg);
                    win.ShowDialog();
                }
                RefreshContext();
            }
        }

        private void UpdatePallet2(string palletCode)
        {
            if (null != manager)
            {
                string errMsg;
                if (!manager.SetPallet2(palletCode, out errMsg))
                {
                    var win = M3CordApp.Windows.MessageBox;
                    win.Setup(errMsg);
                    win.ShowDialog();
                }
                RefreshContext();
            }
        }

        private void RefreshContext()
        {
            this.DataContext = null;
            if (null != manager && null != manager.Condition)
            {
                var cond = manager.Condition;

                ChecnEnableButtons();
                this.DataContext = cond;
                // Bind Pallet && Product Code
                //
                // Pallet 1
                if (!string.IsNullOrEmpty(cond.DoffNo1PalletCode))
                {
                    txtPalletNo1.Text = cond.DoffNo1PalletCode;
                    txtItemCode1.Text = cond.ProductCode1;
                }
                else
                {
                    txtPalletNo1.Text = string.Empty;
                    txtItemCode1.Text = string.Empty;
                }
                // Pallet 2
                if (!string.IsNullOrEmpty(cond.DoffNo2PalletCode))
                {
                    txtPalletNo2.Text = cond.DoffNo2PalletCode;
                    txtItemCode2.Text = cond.ProductCode2;
                }
                else
                {
                    txtPalletNo2.Text = string.Empty;
                    txtItemCode2.Text = string.Empty;
                }
            }
        }

        private void ChecnEnableButtons()
        {
            var user = M3CordApp.Current.User;

            bool isUser = (null != user && user.RoleId == 20) ? true : false;

            cmdSave.IsEnabled = false;
            cmdStart.IsEnabled = false;
            cmdFinish.IsEnabled = false;

            if (null != manager && null != manager.Condition)
            {
                var condition = manager.Condition;
                bool hasPallet = (!string.IsNullOrWhiteSpace(condition.DoffNo1PalletCode) ||
                    !string.IsNullOrWhiteSpace(condition.DoffNo2PalletCode));
                bool validStd = manager.HasStd && manager.IsMatchStd;

                if (isUser)
                {
                    cmdSave.IsEnabled = hasPallet && validStd;

                    cmdSave.Visibility = Visibility.Visible;
                    cmdStart.Visibility = Visibility.Collapsed;
                    cmdFinish.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cmdSave.Visibility = Visibility.Collapsed;
                    cmdStart.Visibility = Visibility.Visible;
                    cmdFinish.Visibility = Visibility.Visible;

                    cmdStart.IsEnabled = hasPallet && validStd &&
                        !condition.StartingTimeStartAgeingTime.HasValue;

                    cmdFinish.IsEnabled = hasPallet && validStd &&
                        condition.StartingTimeStartAgeingTime.HasValue &&
                        condition.FinishTime.HasValue && !condition.OutTime.HasValue;
                }
            }
        }

        private void Save()
        {
            if (null != manager)
            {
                manager.Save();
                RefreshContext();
            }

            M3CordApp.Pages.GotoCordMainMenu();
        }

        private void Start()
        {
            if (null != manager)
            {
                manager.Start();
                RefreshContext();
            }

            M3CordApp.Pages.GotoCordMainMenu();
        }

        private void Finish()
        {
            if (null != manager)
            {
                string errMSg;
                if (!manager.Finish(out errMSg))
                {
                    var win1 = M3CordApp.Windows.MessageBoxOKCancel;
                    win1.Setup(errMSg);
                    win1.ShowDialog();
                    return;
                }

                RefreshContext();

                var win = M3CordApp.Windows.MessageBoxOKCancel;
                win.Setup("Print Ageing Condition (S-5) ?");
                if (win.ShowDialog() == false)
                {
                    M3CordApp.Pages.GotoCordMainMenu();
                }

                var page = M3CordApp.Pages.S5ReportPreview;
                var item = S5ConditionPrintModel.Gets(manager.Condition.S5ConditionId).Value().FirstOrDefault();
                if (null != item)
                {
                    var items = new List<S5ConditionPrintModel>();
                    if (!string.IsNullOrEmpty(item.ProductCode1) &&
                        !string.IsNullOrEmpty(item.ProductCode2) &&
                        item.ProductCode1 != item.ProductCode1)
                    {
                        // Has both product code but not same
                        // require duplicate and update ProductCode to make 2 records
                        var item1 = item.ShallowCopy();
                        item1.ProductCode = item1.ProductCode1;

                        var item2 = item.ShallowCopy();
                        item2.ProductCode = item1.ProductCode2;

                        items.Add(item1);
                        items.Add(item2);
                    }
                    else
                    {
                        items.Add(item);
                    }
                    page.Setup(items);
                    PageContentManager.Instance.Current = page;
                }
            }
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            manager = new S5ConditionManager();
            manager.Load(); // load current
            manager.Refresh();
            RefreshContext();
        }

        #endregion
    }
}
