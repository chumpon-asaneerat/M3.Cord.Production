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
using System.Security.Cryptography;

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
                    txtItemCode1.Text = manager.ProductCode1;
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
                    txtItemCode2.Text = manager.ProductCode2;
                }
                else
                {
                    txtPalletNo2.Text = string.Empty;
                    txtItemCode2.Text = string.Empty;
                }
                // Bind Lot/Trace No
                if (!string.IsNullOrEmpty(cond.LotOrTraceNo))
                {
                    txtLotOrTraceNo.Text = cond.LotOrTraceNo;
                }
                else
                {
                    txtLotOrTraceNo.Text = string.Empty;
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

        private void VerifyCondition(out string errMsg)
        {
            errMsg = null;
            if (null == Condition)
            {
                return;
            }
            // pallet 1
            if (!string.IsNullOrEmpty(Condition.DoffNo1PalletCode))
            {
                // Find pallet setting for product code information
                var pallet1 = GetPalletByCode(Condition.DoffNo1PalletCode);
                var PC1 = GetPCTwist1(pallet1);
                var code = (null != PC1) ? PC1.ProductCode : null;
                if (string.IsNullOrEmpty(code))
                {
                    // find pallet in G4
                    var g4 = G4IssueYarn.SearchG4AgeingPallet(Condition.DoffNo1PalletCode).Value();
                    if (null != g4)
                    {
                        // Special case for Raw Material
                        // Item Yarn must be 1620-288-707 and Product Code must be 1800TW
                        code = "1800TW";
                        
                        IsLotNo = false;
                        LotOrTraceNo1 = g4.TraceNo;
                    }
                    else
                    {
                        errMsg = "Pallet Not found.";
                    }
                }
                else
                {
                    IsLotNo = true;
                    LotOrTraceNo1 = (null != PC1) ? PC1.ProductLotNo : null;
                }

                ProductCode1 = code;
                Std1 = (!string.IsNullOrWhiteSpace(code)) ? GetStd(code) : null;
            }
            else
            {
                ProductCode1 = null;
                Std1 = null;
            }

            // pallet 2
            if (!string.IsNullOrEmpty(Condition.DoffNo2PalletCode))
            {
                // Find pallet setting for product code information
                var pallet2 = GetPalletByCode(Condition.DoffNo2PalletCode);
                var PC2 = GetPCTwist1(pallet2);
                var code = (null != PC2) ? PC2.ProductCode : null;
                if (string.IsNullOrEmpty(code))
                {
                    // find pallet in G4
                    var g4 = G4IssueYarn.SearchG4AgeingPallet(Condition.DoffNo2PalletCode).Value();
                    if (null != g4)
                    {
                        // Special case for Raw Material
                        // Item Yarn must be 1620-288-707 and Product Code must be 1800TW
                        code = "1800TW";
                        
                        IsLotNo = false;
                        LotOrTraceNo2 = g4.TraceNo;
                    }
                    else
                    {
                        errMsg = "Pallet Not found.";
                    }
                }
                else
                {
                    IsLotNo = true;
                    LotOrTraceNo2 = (null != PC2) ? PC2.ProductLotNo : null;
                }

                ProductCode2 = code;
                Std2 = (!string.IsNullOrWhiteSpace(code)) ? GetStd(code) : null;
            }
            else
            {
                ProductCode2 = null;
                Std2 = null;
            }

            if (null != Condition && null != Std1 && null != Std2)
            {
                if (!IsMatchStd)
                {
                    errMsg = "Item Code ไม่สามารถ เข้า Ageing พร้อมกันได้";
                    return;
                }

                S5Condition.Assign(Std1, Condition);
                Condition.LotOrTraceNo = LotOrTraceNo1;
            }
            else if (null != Condition && null != Std1 && null == Std2)
            {
                S5Condition.Assign(Std1, Condition);
                Condition.LotOrTraceNo = LotOrTraceNo1;
            }
            else if (null != Condition && null == Std1 && null != Std2)
            {
                S5Condition.Assign(Std2, Condition);
                Condition.LotOrTraceNo = LotOrTraceNo2;
            }
            else
            {
                Condition.LotOrTraceNo = null;
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

            if (null != Condition)
            {
                string code = (!string.IsNullOrEmpty(palletCode)) ? palletCode.Trim() : null;
                if (!string.IsNullOrWhiteSpace(code))
                {
                    Condition.DoffNo1PalletCode = code;
                }
                else
                {
                    Condition.DoffNo1PalletCode = null;
                }
                VerifyCondition(out message);
                if (string.IsNullOrEmpty(message))
                    ret = true;
            }

            return ret;
        }

        public bool SetPallet2(string palletCode, out string message)
        {
            bool ret = false;
            message = null;

            if (null != Condition)
            {
                string code = (!string.IsNullOrEmpty(palletCode)) ? palletCode.Trim() : null;
                if (!string.IsNullOrWhiteSpace(code))
                {
                    Condition.DoffNo2PalletCode = code;
                }
                else
                {
                    Condition.DoffNo2PalletCode = null;
                }
                VerifyCondition(out message);
                if (string.IsNullOrEmpty(message)) 
                    ret = true;
            }

            return ret;
        }

        public void Refresh()
        {
            string message;
            VerifyCondition(out message);
            if (!string.IsNullOrEmpty(message))
            {
                // something error.
            }
        }

        public bool HasStd
        {
            get 
            {
                var std = (null != Std1) ? Std1 : Std2;
                return null != std;
            }
        }

        public bool IsMatchStd
        {
            get
            {
                if (null != Condition && null != Std1 && null != Std2)
                {
                    // Check valid
                    bool b1 = Std1.SettingTemperatureSet == Std2.SettingTemperatureSet;
                    bool b2 = Std1.SettingTimeSet == Std2.SettingTimeSet;
                    return b1 && b2;
                }
                else if (null != Condition && null != Std1 && null == Std2)
                {
                    return true;
                }
                else if (null != Condition && null == Std1 && null != Std2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Start()
        {
            bool ret = false;

            if (null != Condition)
            {
                var dt = DateTime.Now;
                Condition.StartingTimeStartAgeingTime = dt;
                var std = (null != Std1) ? Std1 : Std2;
                if (null != std)
                {
                    // auto set finish time.
                    double hrs = (std.SettingTimeSet.HasValue) ? (double)std.SettingTimeSet.Value : (double)0;
                    Condition.FinishTime = dt.AddHours(hrs);
                }

                S5Condition.Save(Condition);
                ret = true;
            }

            return ret;
        }

        public bool Finish(out string message)
        {
            bool ret = false;
            message = null;
            if (null != Condition)
            {
                var dt = DateTime.Now;
                Condition.OutTime = dt;
                S5Condition.Save(Condition);
                ret = true;
            }
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

        public string ProductCode1 
        { 
            get; private set; 
        }

        public string ProductCode2
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

        public bool IsLotNo
        {
            get; private set;
        }

        public string LotOrTraceNo1
        {
            get; private set;
        }

        public string LotOrTraceNo2
        {
            get; private set;
        }

        #endregion
    }
}
