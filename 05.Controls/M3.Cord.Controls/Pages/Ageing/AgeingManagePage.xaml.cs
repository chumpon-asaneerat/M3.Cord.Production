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
    /// Interaction logic for AgeingManagePage.xaml
    /// </summary>
    public partial class AgeingManagePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public AgeingManagePage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private PalletSetting pallet1 = null;
        private PalletSetting pallet2 = null;

        private PCTwist1 pcCard1 = null;
        private PCTwist1 pcCard2 = null;

        private S5ConditionStd std1 = null;
        private S5ConditionStd std2 = null;

        private S5Condition condition = null;

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

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void cmdStart_Click(object sender, RoutedEventArgs e)
        {
            Start();
        }

        private void cmdEnd_Click(object sender, RoutedEventArgs e)
        {
            End();
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
        }

        private void txtPalletNo2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string palletCode = txtPalletNo2.Text;
                UpdatePallet2(palletCode);
                e.Handled = true;
            }
        }

        #endregion

        #region Private Methods

        private void UpdatePallet1(string palletCode)
        {
            if (null != condition)
            {
                condition.DoffNo1PalletCode = (!string.IsNullOrEmpty(palletCode)) ? palletCode.Trim() : null;
                VerifyCondition();
            }

            /*
            if (string.IsNullOrEmpty(palletCode))
            {
                txtItemCode1.Text = string.Empty;
                return;
            }

            
            if (null != pallet1)
            {
                pcCard1 = PCTwist1.Get(pallet1.PCTwist1Id.Value).Value();
                if (null != pcCard1)
                {
                    txtItemCode1.Text = pcCard1.ProductCode;
                    var stds = S5ConditionStd.Gets(pcCard1.ProductCode).Value();
                    std1 = (null != stds && stds.Count > 0) ? stds.FirstOrDefault() : null;
                    VerifyCondition();
                }
            }
            */
        }

        private void UpdatePallet2(string palletCode)
        {
            if (null != condition)
            {
                condition.DoffNo2PalletCode = (!string.IsNullOrEmpty(palletCode)) ? palletCode.Trim() : null;
                VerifyCondition();
            }
        }

        private void VerifyCondition()
        {
            if (null == condition)
            {
                return;
            }
            // pallet 1
            if (!string.IsNullOrEmpty(condition.DoffNo1PalletCode))
            {
                // Load pallet, pccard 1 information
                pallet1 = GetPalletByCode(condition.DoffNo1PalletCode);
                pcCard1 = GetPCTwist1(pallet1);
                std1 = GetStd(pcCard1);

                // update text box
                txtPalletNo1.Text = condition.DoffNo1PalletCode;
                txtItemCode1.Text = (null != pcCard1) ? pcCard1.ProductCode : string.Empty;
            }
            else
            {
                // update text box
                txtPalletNo1.Text = string.Empty;
                txtItemCode1.Text = string.Empty;
            }

            // pallet 2
            if (!string.IsNullOrEmpty(condition.DoffNo2PalletCode))
            {
                // Load pallet, pccard 2 information
                pallet2 = GetPalletByCode(condition.DoffNo2PalletCode);
                pcCard2 = GetPCTwist1(pallet1);
                std2 = GetStd(pcCard2);

                // update text box
                txtPalletNo2.Text = condition.DoffNo2PalletCode;
                txtItemCode2.Text = (null != pcCard2) ? pcCard2.ProductCode : string.Empty;
            }
            else
            {
                // update text box
                txtPalletNo2.Text = string.Empty;
                txtItemCode2.Text = string.Empty;
            }

            if (null != std1 && null != std2)
            {
                // Check valid
                bool b1 = std1.MainSupplySteamPressureSet != std2.MainSupplySteamPressureSet;
                bool b2 = std1.AgeingSteamPressureSet != std2.AgeingSteamPressureSet;
                bool b3 = std1.SettingTemperatureSet != std2.SettingTemperatureSet;
                bool b4 = std1.SettingTimeSet != std2.SettingTimeSet;
                bool b5 = std1.SettingTemperatureSet != std2.SettingTemperatureSet;

                if (b1 && b2 && b3 && b4 && b5)
                {
                    var win = M3CordApp.Windows.MessageBox;
                    win.Setup("Item Code ไม่สามารถ เข้า Ageing พร้อมกันได้");
                    win.ShowDialog();
                    return;
                }

                S5Condition.Assign(std1, condition);
            }
            else if (null != std1 && null == std2)
            {
                S5Condition.Assign(std1, condition);
            }
            else if (null == std1 && null != std2)
            {
                S5Condition.Assign(std2, condition);
            }
            else
            {

            }

            RefreshContext();
        }

        private void RefreshContext()
        {
            this.DataContext = null;
            if (null != condition)
            {
                ChecnEnableButtons();

                this.DataContext = condition;
            }
        }

        private void ChecnEnableButtons()
        {
            var user = M3CordApp.Current.User;

            bool isUser = (null != user && user.RoleId == 20) ? true : false;

            cmdSave.IsEnabled = false;
            cmdStart.IsEnabled = false;
            cmdEnd.IsEnabled = false;
            cmdFinish.IsEnabled = false;

            if (null != condition)
            {
                bool hasPallet = (!string.IsNullOrWhiteSpace(condition.DoffNo1PalletCode) ||
                    !string.IsNullOrWhiteSpace(condition.DoffNo2PalletCode));

                if (isUser)
                {
                    cmdSave.IsEnabled = hasPallet;

                    cmdSave.Visibility = Visibility.Visible;
                    cmdStart.Visibility = Visibility.Collapsed;
                    cmdEnd.Visibility = Visibility.Collapsed;
                    cmdFinish.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cmdSave.Visibility = Visibility.Collapsed;
                    cmdStart.Visibility = Visibility.Visible;
                    cmdEnd.Visibility = Visibility.Visible;
                    cmdFinish.Visibility = Visibility.Visible;

                    cmdStart.IsEnabled = hasPallet && 
                        !condition.StartingTimeStartAgeingTime.HasValue;

                    cmdEnd.IsEnabled = hasPallet && 
                        condition.StartingTimeStartAgeingTime.HasValue &&
                        !condition.FinishTime.HasValue;

                    cmdFinish.IsEnabled = hasPallet && 
                        condition.StartingTimeStartAgeingTime.HasValue &&
                        condition.FinishTime.HasValue && !condition.OutTime.HasValue;
                }
            }
        }

        private PalletSetting GetPalletByCode(string palletCode)
        {
            return PalletSetting.Search(palletCode, PalletStatus.Create).Value();
        }

        private PCTwist1 GetPCTwist1(PalletSetting pallet)
        {
            if (null == pallet || !pallet.PCTwist1Id.HasValue)
                return null;
            return PCTwist1.Get(pallet.PCTwist1Id.Value).Value();
        }

        private S5ConditionStd GetStd(PCTwist1 pcCard)
        {
            if (null == pcCard || string.IsNullOrWhiteSpace(pcCard.ProductCode))
            {
                return null;
            }
            var stds = S5ConditionStd.Gets(pcCard.ProductCode).Value();
            return (null != stds && stds.Count > 0) ? stds.FirstOrDefault() : null;
        }

        private void Save()
        {
            if (null != condition)
            {
                S5Condition.Save(condition);
                RefreshContext();
            }

            M3CordApp.Pages.GotoCordMainMenu();
        }

        private void Start()
        {
            if (null != condition)
            {
                condition.StartingTimeStartAgeingTime = DateTime.Now;
                S5Condition.Save(condition);
                RefreshContext();
            }

            M3CordApp.Pages.GotoCordMainMenu();
        }

        private void End()
        {
            if (null != condition)
            {
                condition.FinishTime = DateTime.Now;
                S5Condition.Save(condition);
                RefreshContext();
            }

            M3CordApp.Pages.GotoCordMainMenu();
        }

        private void Finish()
        {
            if (null != condition)
            {
                condition.OutTime = DateTime.Now;
                S5Condition.Save(condition);
                RefreshContext();


                var win = M3CordApp.Windows.MessageBoxOKCancel;
                win.Setup("Print Ageing Condition (S-5) ?");
                if (win.ShowDialog() == false)
                {
                    M3CordApp.Pages.GotoCordMainMenu();
                }

                var page = M3CordApp.Pages.S5ReportPreview;
                var items = S5ConditionPrintModel.Gets(condition.S5ConditionId).Value();
                page.Setup(items);
                PageContentManager.Instance.Current = page;
            }
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            condition = S5Condition.GetCurrent().Value();
            if (null == condition)
            {
                condition = new S5Condition();
            }

            VerifyCondition();
            RefreshContext();

            if (!string.IsNullOrEmpty(condition.DoffNo1PalletCode))
            {
                // Load pallet, pccard 1 information
                pallet1 = GetPalletByCode(condition.DoffNo1PalletCode);
                pcCard1 = GetPCTwist1(pallet1);
            }
            else
            {
                txtPalletNo1.Text = string.Empty;
                txtItemCode1.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(condition.DoffNo2PalletCode))
            {
                // Load pallet, pccard 2 information
                pallet2 = GetPalletByCode(condition.DoffNo2PalletCode);
                pcCard2 = GetPCTwist1(pallet1);
            }
            else
            {
                txtPalletNo2.Text = string.Empty;
                txtItemCode2.Text = string.Empty;
            }
        }

        #endregion
    }
}
