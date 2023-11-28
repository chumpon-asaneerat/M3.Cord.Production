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
using static M3.Cord.Pages.DIPUI;

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
            if (null != manager)
            {
                string errMsg;
                if (!manager.SetPallet1(palletCode, out errMsg))
                {
                    
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
                var pc1 = manager.PC1;
                var pc2 = manager.PC2;

                ChecnEnableButtons();
                this.DataContext = cond;
                // Bind Pallet && Product Code
                //
                // Pallet 1
                if (!string.IsNullOrEmpty(cond.DoffNo1PalletCode))
                {
                    txtPalletNo1.Text = cond.DoffNo1PalletCode;
                    txtItemCode1.Text = (null != pc1) ? pc1.ProductCode : string.Empty;
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
                    txtItemCode2.Text = (null != pc2) ? pc2.ProductCode : string.Empty;
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

                if (isUser)
                {
                    cmdSave.IsEnabled = hasPallet;

                    cmdSave.Visibility = Visibility.Visible;
                    cmdStart.Visibility = Visibility.Collapsed;
                    cmdFinish.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cmdSave.Visibility = Visibility.Collapsed;
                    cmdStart.Visibility = Visibility.Visible;
                    cmdFinish.Visibility = Visibility.Visible;

                    cmdStart.IsEnabled = hasPallet && 
                        !condition.StartingTimeStartAgeingTime.HasValue;

                    cmdFinish.IsEnabled = hasPallet && 
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
                var items = S5ConditionPrintModel.Gets(manager.Condition.S5ConditionId).Value();
                page.Setup(items);
                PageContentManager.Instance.Current = page;
            }
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            manager = new S5ConditionManager();

            if (null == manager.Condition)
            {
                manager.Create();
            }
            manager.Refresh();
            RefreshContext();
        }

        #endregion
    }

    public class S5ConditionManager
    {
        #region Constructor

        public S5ConditionManager() : base()
        {

        }

        #endregion

        #region Private Methods

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

        private S5ConditionStd GetStd(string productCode)
        {
            if (string.IsNullOrWhiteSpace(productCode))
            {
                return null;
            }
            var stds = S5ConditionStd.Gets(productCode).Value();
            return (null != stds && stds.Count > 0) ? stds.FirstOrDefault() : null;
        }

        private void VerifyCondition()
        {
            if (null == Condition)
            {
                return;
            }
            // pallet 1
            if (!string.IsNullOrEmpty(Condition.DoffNo1PalletCode))
            {
                // Load pallet, pccard 1 information
                var pallet1 = GetPalletByCode(Condition.DoffNo1PalletCode);
                PC1 = GetPCTwist1(pallet1);
                Std1 = (null != PC1) ? GetStd(PC1.ProductCode) : null;
            }

            // pallet 2
            if (!string.IsNullOrEmpty(Condition.DoffNo2PalletCode))
            {
                // Load pallet, pccard 2 information
                var pallet2 = GetPalletByCode(Condition.DoffNo2PalletCode);
                PC2 = GetPCTwist1(pallet2);
                Std2 = (null != PC2) ? GetStd(PC2.ProductCode) : null;
            }

            if (null != Condition && null != Std1 && null != Std2)
            {
                // Check valid
                bool b1 = Std1.MainSupplySteamPressureSet != Std2.MainSupplySteamPressureSet;
                bool b2 = Std1.AgeingSteamPressureSet != Std2.AgeingSteamPressureSet;
                bool b3 = Std1.SettingTemperatureSet != Std2.SettingTemperatureSet;
                bool b4 = Std1.SettingTimeSet != Std2.SettingTimeSet;
                bool b5 = Std1.SettingTemperatureSet != Std2.SettingTemperatureSet;

                if (b1 && b2 && b3 && b4 && b5)
                {
                    var win = M3CordApp.Windows.MessageBox;
                    win.Setup("Item Code ไม่สามารถ เข้า Ageing พร้อมกันได้");
                    win.ShowDialog();
                    return;
                }

                S5Condition.Assign(Std1, Condition);
            }
            else if (null != Condition && null != Std1 && null == Std2)
            {
                S5Condition.Assign(Std1, Condition);
            }
            else if (null != Condition && null == Std1 && null != Std2)
            {
                S5Condition.Assign(Std2, Condition);
            }
            else
            {

            }
        }

        #endregion

        #region Public Methods

        public void Create()
        {
            Condition = new S5Condition();
        }

        public bool SetPallet1(string palletCode, out string message)
        {
            bool ret = false;
            message = null;

            Condition.DoffNo1PalletCode = (!string.IsNullOrEmpty(palletCode)) ? palletCode.Trim() : null;

            return ret;
        }

        public bool SetPallet2(string palletCode, out string message)
        {
            bool ret = false;
            message = null;

            Condition.DoffNo2PalletCode = (!string.IsNullOrEmpty(palletCode)) ? palletCode.Trim() : null;

            return ret;
        }

        public void Refresh()
        {
            VerifyCondition();
        }

        public bool Start()
        {
            bool ret = false;

            if (null != Condition)
            {
                Condition.StartingTimeStartAgeingTime = DateTime.Now;
                S5Condition.Save(Condition);
                ret = true;
            }

            return ret;
        }

        public bool Finish(out string message)
        {
            bool ret = false;
            message = null;

            return ret;
        }

        public bool Save()
        {
            bool ret = false;

            if (null != Condition)
            {
                S5Condition.Save(Condition);
                ret = true;
            }

            return ret;
        }

        #endregion

        #region Public Properties

        public S5Condition Condition
        {
            get; private set;
        }

        public PCTwist1 PC1 
        { 
            get; private set; 
        }

        public PCTwist1 PC2 
        { 
            get; private set; 
        }

        public S5ConditionStd Std1
        {
            get; private set;
        }

        public S5ConditionStd Std2
        {
            get; private set;
        }

        #endregion
    }
}
