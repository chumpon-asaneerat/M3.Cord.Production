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
    /// Interaction logic for AgeingConditionFromDIPPage.xaml
    /// </summary>
    public partial class AgeingConditionFromDIPPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public AgeingConditionFromDIPPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private S5ConditionDIPManager manager = null;

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
            SaveL();
        }

        private void cmdStart_Click(object sender, RoutedEventArgs e)
        {
            StartL();
        }

        private void cmdFinish_Click(object sender, RoutedEventArgs e)
        {
            FinishL();
        }

        private void cmdSave2_Click(object sender, RoutedEventArgs e)
        {
            SaveR();
        }

        private void cmdStart2_Click(object sender, RoutedEventArgs e)
        {
            StartR();
        }

        private void cmdFinish2_Click(object sender, RoutedEventArgs e)
        {
            FinishR();
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
                RefreshContextL();
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
                RefreshContextR();
            }
        }

        private void RefreshContextL()
        {
            s5L.DataContext = null;
            if (null != manager && null != manager.ConditionL)
            {
                var cond = manager.ConditionL;

                ChecnEnableButtonsL();
                s5L.DataContext = cond;
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
            }
        }

        private void RefreshContextR()
        {
            s5R.DataContext = null;
            if (null != manager && null != manager.ConditionR)
            {
                var cond = manager.ConditionR;

                ChecnEnableButtonsR();
                s5R.DataContext = cond;
                // Bind Pallet && Product Code
                //
                // Pallet 2
                if (!string.IsNullOrEmpty(cond.DoffNo1PalletCode))
                {
                    txtPalletNo2.Text = cond.DoffNo1PalletCode;
                    txtItemCode2.Text = cond.ProductCode1;
                }
                else
                {
                    txtPalletNo2.Text = string.Empty;
                    txtItemCode2.Text = string.Empty;
                }
            }
        }

        private void ChecnEnableButtonsL()
        {
            var user = M3CordApp.Current.User;

            bool isUser = (null != user && user.RoleId == 20) ? true : false;

            cmdSave.IsEnabled = false;
            cmdStart.IsEnabled = false;
            cmdFinish.IsEnabled = false;

            if (null != manager && null != manager.ConditionL)
            {
                var condition = manager.ConditionL;
                bool hasPallet = !string.IsNullOrWhiteSpace(condition.DoffNo1PalletCode);
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

                // lock textbox if already start.
                /*
                bool isStart = condition.StartingTimeStartAgeingTime.HasValue;
                txtPalletNo1.IsEnabled = !isStart;
                // allow to set pallet 2 after start
                //txtPalletNo2.IsEnabled = string.IsNullOrEmpty(condition.DoffNo2PalletCode);
                */
            }
        }

        private void ChecnEnableButtonsR()
        {
            var user = M3CordApp.Current.User;

            bool isUser = (null != user && user.RoleId == 20) ? true : false;

            cmdSave2.IsEnabled = false;
            cmdStart2.IsEnabled = false;
            cmdFinish2.IsEnabled = false;

            if (null != manager && null != manager.ConditionR)
            {
                var condition = manager.ConditionR;
                bool hasPallet = !string.IsNullOrWhiteSpace(condition.DoffNo1PalletCode);
                bool validStd = manager.HasStd && manager.IsMatchStd;

                if (isUser)
                {
                    cmdSave2.IsEnabled = hasPallet && validStd;

                    cmdSave2.Visibility = Visibility.Visible;
                    cmdStart2.Visibility = Visibility.Collapsed;
                    cmdFinish2.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cmdSave2.Visibility = Visibility.Collapsed;
                    cmdStart2.Visibility = Visibility.Visible;
                    cmdFinish2.Visibility = Visibility.Visible;

                    cmdStart2.IsEnabled = hasPallet && validStd &&
                        !condition.StartingTimeStartAgeingTime.HasValue;

                    cmdFinish2.IsEnabled = hasPallet && validStd &&
                        condition.StartingTimeStartAgeingTime.HasValue &&
                        condition.FinishTime.HasValue && !condition.OutTime.HasValue;
                }

                // lock textbox if already start.
                /*
                bool isStart = condition.StartingTimeStartAgeingTime.HasValue;
                txtPalletNo1.IsEnabled = !isStart;
                // allow to set pallet 2 after start
                //txtPalletNo2.IsEnabled = string.IsNullOrEmpty(condition.DoffNo2PalletCode);
                */
            }
        }

        private void SaveL()
        {
            if (null != manager)
            {
                bool ret = manager.SaveL();
                if (ret)
                    M3CordApp.Windows.SaveSuccess();
                else M3CordApp.Windows.SaveFailed();

                RefreshContextL();
            }

            M3CordApp.Pages.GotoCordMainMenu();
        }

        private void SaveR()
        {
            if (null != manager)
            {
                bool ret = manager.SaveR();
                if (ret)
                    M3CordApp.Windows.SaveSuccess();
                else M3CordApp.Windows.SaveFailed();

                RefreshContextR();
            }

            M3CordApp.Pages.GotoCordMainMenu();
        }

        private void StartL()
        {
            if (null != manager)
            {
                var ret = manager.StartL();
                var win = M3CordApp.Windows.MessageBox;
                string msg = (ret) ?
                    "Update Start Time success" + Environment.NewLine + "บันทึกข้อมูลเริ่มทำงานสำเร็จ" :
                    "Update Start Time failed" + Environment.NewLine + "บันทึกข้อมูลเริ่มทำงานไม่สำเร็จ";
                win.Setup(msg);
                win.ShowDialog();

            }

            M3CordApp.Pages.GotoCordMainMenu();
        }

        private void StartR()
        {
            if (null != manager)
            {
                var ret = manager.StartR();
                var win = M3CordApp.Windows.MessageBox;
                string msg = (ret) ?
                    "Update Start Time success" + Environment.NewLine + "บันทึกข้อมูลเริ่มทำงานสำเร็จ" :
                    "Update Start Time failed" + Environment.NewLine + "บันทึกข้อมูลเริ่มทำงานไม่สำเร็จ";
                win.Setup(msg);
                win.ShowDialog();

            }

            M3CordApp.Pages.GotoCordMainMenu();
        }

        private void FinishL()
        {
            if (null != manager && null != manager.ConditionL)
            {
                string errMSg;
                if (!manager.FinishL(out errMSg))
                {
                    var win1 = M3CordApp.Windows.MessageBoxOKCancel;
                    win1.Setup(errMSg);
                    win1.ShowDialog();
                    return;
                }

                RefreshContextL();

                var win = M3CordApp.Windows.MessageBoxOKCancel;
                win.Setup("Print Ageing Condition (S-5) ?");
                if (win.ShowDialog() == false)
                {
                    M3CordApp.Pages.GotoCordMainMenu();
                }

                var page = M3CordApp.Pages.S5ReportPreview;
                var item = S5ConditionPrintModel.Gets(manager.ConditionL.S5ConditionId).Value().FirstOrDefault();
                if (null != item)
                {
                    var items = new List<S5ConditionPrintModel>();
                    items.Add(item);

                    /*
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
                    */

                    page.Setup(items);
                    PageContentManager.Instance.Current = page;
                }
            }
        }

        private void FinishR()
        {
            if (null != manager && null != manager.ConditionR)
            {
                string errMSg;
                if (!manager.FinishR(out errMSg))
                {
                    var win1 = M3CordApp.Windows.MessageBoxOKCancel;
                    win1.Setup(errMSg);
                    win1.ShowDialog();
                    return;
                }

                RefreshContextR();

                var win = M3CordApp.Windows.MessageBoxOKCancel;
                win.Setup("Print Ageing Condition (S-5) ?");
                if (win.ShowDialog() == false)
                {
                    M3CordApp.Pages.GotoCordMainMenu();
                }

                var page = M3CordApp.Pages.S5ReportPreview;
                var item = S5ConditionPrintModel.Gets(manager.ConditionR.S5ConditionId).Value().FirstOrDefault();
                if (null != item)
                {
                    var items = new List<S5ConditionPrintModel>();
                    items.Add(item);

                    /*
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
                    */

                    page.Setup(items);
                    PageContentManager.Instance.Current = page;
                }
            }
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            manager = new S5ConditionDIPManager();
            manager.LoadL(); // load current
            manager.LoadR(); // load current
            manager.Refresh();
            RefreshContextL();
            RefreshContextR();
        }

        #endregion
    }
}
