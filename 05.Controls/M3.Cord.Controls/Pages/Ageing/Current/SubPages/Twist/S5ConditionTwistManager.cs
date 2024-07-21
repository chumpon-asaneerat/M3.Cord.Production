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
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

#endregion

namespace M3.Cord.Pages
{
    public class S5ConditionTwistManager
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public S5ConditionTwistManager() : base()
        {
        }

        #endregion

        #region Private Methods

        private PalletSetting GetPalletByCode(string palletCode)
        {
            return PalletSetting.Search(palletCode, PalletStatus.Create).Value();
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

        private PCTwist1 GetPCTwist1(PalletSetting pallet)
        {
            if (null == pallet || !pallet.PCTwist1Id.HasValue)
                return null;
            return PCTwist1.Get(pallet.PCTwist1Id.Value).Value();
        }

        private void VerifyCondition(out string errMsg)
        {
            errMsg = null;
            // pallet 1
            if (null != ConditionL)
            {
                if (!string.IsNullOrEmpty(ConditionL.DoffNo1PalletCode))
                {
                    // Find pallet setting for product code information
                    var pallet1 = GetPalletByCode(ConditionL.DoffNo1PalletCode);
                    var PC1 = GetPCTwist1(pallet1);
                    var customer = (null != PC1) ? PC1.CustomerName : null;
                    var code = (null != PC1) ? PC1.ProductCode : null;
                    var name = (null != PC1) ? PC1.ProductName : null;
                    if (string.IsNullOrEmpty(code))
                    {
                        errMsg = "Pallet Not found.";
                    }
                    else
                    {
                        //LotOrTraceNo1 = (null != PC1) ? PC1.ProductLotNo : null;
                    }

                    ConditionL.CustomerName = customer;
                    ConditionL.ProductCode1 = code;
                    ConditionL.ProductName1 = name;
                    ConditionL.DoffNo1MCNo = (null != pallet1) ? pallet1.MCCode : null;

                    if (null != pallet1)
                    {
                        pallet1.LoadItems();

                        string doffNos = string.Empty;
                        var doffs = pallet1.Items.Select(x => x.DoffNo).Distinct().ToList();
                        if (null != doffs)
                        {
                            doffs.Sort();
                            for (int i = 0; i < doffs.Count; ++i)
                            {
                                var doff = doffs[i];
                                doffNos += doff.ToString();
                                if (i < doffs.Count - 1) doffNos += ", ";
                            }
                        }

                        ConditionL.DoffNo1Doff = doffNos.Trim();
                        ConditionL.DoffNo1Qty = (pallet1.TotalCH.HasValue) ?
                            pallet1.TotalCH.Value.ToString("n0") : null;
                    }
                    else
                    {
                        ConditionL.DoffNo1Doff = null;
                        ConditionL.DoffNo1Qty = null;
                    }

                    Std1 = (!string.IsNullOrWhiteSpace(code)) ? GetStd(code) : null;
                }
                else
                {
                    ConditionL.ProductCode1 = null;
                    Std1 = null;
                }
            }

            // pallet 2
            if (null != ConditionR)
            {
                if (!string.IsNullOrEmpty(ConditionR.DoffNo1PalletCode))
                {
                    // Find pallet setting for product code information
                    var pallet2 = GetPalletByCode(ConditionR.DoffNo1PalletCode);
                    var PC2 = GetPCTwist1(pallet2);
                    var customer = (null != PC2) ? PC2.CustomerName : null;
                    var code = (null != PC2) ? PC2.ProductCode : null;
                    var name = (null != PC2) ? PC2.ProductName : null;
                    if (string.IsNullOrEmpty(code))
                    {
                        errMsg = "Pallet Not found.";
                    }
                    else
                    {
                        //LotOrTraceNo2 = (null != PC2) ? PC2.ProductLotNo : null;
                    }

                    ConditionR.CustomerName = customer;
                    ConditionR.ProductCode2 = code;
                    ConditionR.ProductName2 = name;
                    ConditionR.DoffNo1MCNo = (null != pallet2) ? pallet2.MCCode : null;

                    if (null != pallet2)
                    {
                        pallet2.LoadItems();

                        string doffNos = string.Empty;
                        var doffs = pallet2.Items.Select(x => x.DoffNo).Distinct().ToList();
                        if (null != doffs)
                        {
                            doffs.Sort();
                            for (int i = 0; i < doffs.Count; ++i)
                            {
                                var doff = doffs[i];
                                doffNos += doff.ToString();
                                if (i < doffs.Count - 1) doffNos += ", ";
                            }
                        }

                        ConditionR.DoffNo1Doff = doffNos.Trim();
                        ConditionR.DoffNo1Qty = (pallet2.TotalCH.HasValue) ?
                            pallet2.TotalCH.Value.ToString("n0") : null;
                    }
                    else
                    {
                        ConditionR.DoffNo1Doff = null;
                        ConditionR.DoffNo1Qty = null;
                    }

                    Std2 = (!string.IsNullOrWhiteSpace(code)) ? GetStd(code) : null;
                }
                else
                {
                    ConditionR.ProductCode2 = null;
                    Std2 = null;
                }
            }

            if (IsDuplicate())
            {
                errMsg = "หมายเลข Pallet ด้านซ้ายและด้านขวา ซ้ำกัน";
                return;
            }

            if (null != Std1 && null != Std2)
            {
                if (!IsMatchStd)
                {
                    errMsg = "Item Code ไม่สามารถ เข้า Ageing พร้อมกันได้";
                    return;
                }

                if (null != ConditionL) S5Condition.Assign(Std1, ConditionL);
                if (null != ConditionR) S5Condition.Assign(Std2, ConditionR);
            }
            else if (null != Std1 && null == Std2)
            {
                if (null != ConditionL) S5Condition.Assign(Std1, ConditionL);
                //if (null != ConditionR) S5Condition.Assign(Std1, ConditionR);
            }
            else if (null == Std1 && null != Std2)
            {
                //if (null != ConditionL) S5Condition.Assign(Std2, ConditionL);
                if (null != ConditionR) S5Condition.Assign(Std2, ConditionR);
            }
            else
            {

            }
        }

        #endregion

        #region Public Methods

        public bool IsDuplicate()
        {
            bool ret = false;
            if (null != ConditionL && null != ConditionR)
            {
                if (!string.IsNullOrEmpty(ConditionL.DoffNo1PalletCode) &&
                    !string.IsNullOrEmpty(ConditionR.DoffNo1PalletCode) &&
                    ConditionL.DoffNo1PalletCode.Trim() == ConditionR.DoffNo1PalletCode.Trim())
                {
                    ret = true;
                }
            }

            return ret;
        }

        public bool SetPallet1(string palletCode, out string message)
        {
            bool ret = false;
            message = null;

            if (null != ConditionL)
            {
                string code = (!string.IsNullOrEmpty(palletCode)) ? palletCode.Trim() : null;
                if (!string.IsNullOrWhiteSpace(code))
                {
                    ConditionL.DoffNo1PalletCode = code;
                }
                else
                {
                    ConditionL.DoffNo1PalletCode = null;
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

            if (null != ConditionR)
            {
                string code = (!string.IsNullOrEmpty(palletCode)) ? palletCode.Trim() : null;
                if (!string.IsNullOrWhiteSpace(code))
                {
                    ConditionR.DoffNo1PalletCode = code;
                }
                else
                {
                    ConditionR.DoffNo1PalletCode = null;
                }
                VerifyCondition(out message);
                if (string.IsNullOrEmpty(message))
                    ret = true;
            }

            return ret;
        }

        public void LoadL()
        {
            ConditionL = S5Condition.GetCurrent(FromSources.Twist, "L").Value();
            if (null == ConditionL)
            {
                ConditionL = new S5Condition();
                ConditionL.FromSource = FromSources.Twist;
            }

            if (null != ConditionL && string.IsNullOrEmpty(ConditionL.MCSide)) ConditionL.MCSide = "L";
        }

        public void LoadR()
        {
            ConditionR = S5Condition.GetCurrent(FromSources.Twist, "R").Value();
            if (null == ConditionR)
            {
                ConditionR = new S5Condition();
                ConditionR.FromSource = FromSources.Twist;
            }

            if (null != ConditionR && string.IsNullOrEmpty(ConditionR.MCSide)) ConditionR.MCSide = "R";
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

        public bool StartL()
        {
            bool ret = false;

            if (null != ConditionL)
            {
                var dt = DateTime.Now;
                ConditionL.StartingTimeStartAgeingTime = dt;
                var std = (null != Std1) ? Std1 : Std2;
                if (null != std)
                {
                    // auto set finish time.
                    double hrs = (std.SettingTimeSet.HasValue) ? (double)std.SettingTimeSet.Value : (double)0;
                    ConditionL.FinishTime = dt.AddHours(hrs);
                }

                S5Condition.Save(ConditionL);
                ret = true;
            }

            return ret;
        }

        public bool StartR()
        {
            bool ret = false;

            if (null != ConditionR)
            {
                var dt = DateTime.Now;
                ConditionR.StartingTimeStartAgeingTime = dt;
                var std = (null != Std1) ? Std1 : Std2;
                if (null != std)
                {
                    // auto set finish time.
                    double hrs = (std.SettingTimeSet.HasValue) ? (double)std.SettingTimeSet.Value : (double)0;
                    ConditionR.FinishTime = dt.AddHours(hrs);
                }

                S5Condition.Save(ConditionR);
                ret = true;
            }

            return ret;
        }

        public bool FinishL(out string message)
        {
            bool ret = false;
            message = null;
            if (null != ConditionL)
            {
                var dt = DateTime.Now;
                ConditionL.OutTime = dt;
                ConditionL.IssueBy = (null != M3CordApp.Current.User) ? M3CordApp.Current.User.FullName : null;
                ConditionL.IssueDate = DateTime.Now;

                S5Condition.Save(ConditionL);
                ret = true;
            }
            return ret;
        }

        public bool FinishR(out string message)
        {
            bool ret = false;
            message = null;
            if (null != ConditionR)
            {
                var dt = DateTime.Now;
                ConditionR.OutTime = dt;
                ConditionR.IssueBy = (null != M3CordApp.Current.User) ? M3CordApp.Current.User.FullName : null;
                ConditionR.IssueDate = DateTime.Now;

                S5Condition.Save(ConditionR);
                ret = true;
            }
            return ret;
        }

        public bool SaveL()
        {
            bool ret = false;

            if (null != ConditionL)
            {
                S5Condition.Save(ConditionL);
                ret = true;
            }

            return ret;
        }

        public bool SaveR()
        {
            bool ret = false;

            if (null != ConditionR)
            {
                S5Condition.Save(ConditionR);
                ret = true;
            }

            return ret;
        }

        #endregion

        #region Public Properties

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
                if (null != Std1 && null != Std2)
                {
                    // Check valid
                    bool b1 = Std1.SettingTemperatureSet == Std2.SettingTemperatureSet;
                    bool b2 = Std1.SettingTimeSet == Std2.SettingTimeSet;
                    return b1 && b2;
                }
                else if (null != Std1 && null == Std2)
                {
                    return true;
                }
                else if (null == Std1 && null != Std2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public S5Condition ConditionL
        {
            get; private set;
        }

        public S5Condition ConditionR
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
