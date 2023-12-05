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
    public class S5ConditionRawMaterialManager
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public S5ConditionRawMaterialManager() : base()
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

        #endregion

        #region Public Methods

        public void Load()
        {
            Condition = S5Condition.GetCurrent(FromSources.RawMeterial).Value();
            if (null == Condition)
            {
                Condition = new S5Condition();
                Condition.FromSource = FromSources.RawMeterial;
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
