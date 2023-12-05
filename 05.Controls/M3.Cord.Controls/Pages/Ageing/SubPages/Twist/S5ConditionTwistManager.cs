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
                    errMsg = "Pallet Not found.";
                }
                else
                {
                    //LotOrTraceNo1 = (null != PC1) ? PC1.ProductLotNo : null;
                }

                Condition.ProductCode1 = code;
                Std1 = (!string.IsNullOrWhiteSpace(code)) ? GetStd(code) : null;
            }
            else
            {
                Condition.ProductCode1 = null;
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
                    errMsg = "Pallet Not found.";
                }
                else
                {
                    //LotOrTraceNo2 = (null != PC2) ? PC2.ProductLotNo : null;
                }

                Condition.ProductCode2 = code;
                Std2 = (!string.IsNullOrWhiteSpace(code)) ? GetStd(code) : null;
            }
            else
            {
                Condition.ProductCode2 = null;
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

        public void Load()
        {
            Condition = S5Condition.GetCurrent(FromSources.Twist).Value();
            if (null == Condition)
            {
                Condition = new S5Condition();
                Condition.FromSource = FromSources.Twist;
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

        public S5Condition Condition
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
