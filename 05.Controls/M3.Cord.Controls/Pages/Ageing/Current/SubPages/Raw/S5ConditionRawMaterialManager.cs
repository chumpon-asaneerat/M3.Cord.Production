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
using System.Xml.Linq;

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
            if (!string.IsNullOrEmpty(Condition.DoffNo1TraceNo))
            {
                // Special case for Raw Material
                // Item Yarn must be 1620-288-707 and Product Code must be 1800TW
                var code = "1800TW";
                var name = "BRAKE HOSE";

                // find pallet in G4
                var g4 = G4IssueYarn.SearchG4AgeingPallet(Condition.DoffNo1TraceNo).Value();
                if (null != g4)
                {
                    Condition.DoffNo1PalletCode = g4.PalletNo;
                    Condition.ProductCode1 = code;
                    Condition.ProductName1 = name;

                    Std1 = (!string.IsNullOrWhiteSpace(code)) ? GetStd(code) : null;
                }
                else
                {
                    errMsg = "Trace No Not found.";
                    Condition.DoffNo1PalletCode = null;
                    Condition.ProductCode1 = null;
                    Condition.ProductName1 = null;
                    Std1 = null;
                }
            }
            else
            {
                Condition.DoffNo1PalletCode = null;
                Condition.ProductCode1 = null;
                Condition.ProductName1 = null;
                Std1 = null;
            }

            // pallet 2
            if (!string.IsNullOrEmpty(Condition.DoffNo2TraceNo))
            {
                // Special case for Raw Material
                // Item Yarn must be 1620-288-707 and Product Code must be 1800TW
                var code = "1800TW";
                var name = "BRAKE HOSE";

                // find pallet in G4
                var g4 = G4IssueYarn.SearchG4AgeingPallet(Condition.DoffNo2TraceNo).Value();
                if (null != g4)
                {

                    Condition.DoffNo2PalletCode = g4.PalletNo;
                    Condition.ProductCode2 = code;
                    Condition.ProductName2 = name;
                    Std2 = (!string.IsNullOrWhiteSpace(code)) ? GetStd(code) : null;
                }
                else
                {
                    errMsg = "Trace No Not found.";
                    Condition.DoffNo2PalletCode = null;
                    Condition.ProductCode2 = null;
                    Condition.ProductName2 = null;
                    Std2 = null;
                }
            }
            else
            {
                Condition.DoffNo2PalletCode = null;
                Condition.ProductCode2 = null;
                Condition.ProductName2 = null;
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
                //Condition.LotOrTraceNo = LotOrTraceNo1;
            }
            else if (null != Condition && null != Std1 && null == Std2)
            {
                S5Condition.Assign(Std1, Condition);
                //Condition.LotOrTraceNo = LotOrTraceNo1;
            }
            else if (null != Condition && null == Std1 && null != Std2)
            {
                S5Condition.Assign(Std2, Condition);
                //Condition.LotOrTraceNo = LotOrTraceNo2;
            }
            else
            {
                //Condition.LotOrTraceNo = null;
            }
        }

        #endregion

        #region Public Methods

        public bool SetTrace1(string traceNo, out string message)
        {
            bool ret = false;
            message = null;

            if (null != Condition1)
            {
                string code = (!string.IsNullOrEmpty(traceNo)) ? traceNo.Trim() : null;
                if (!string.IsNullOrWhiteSpace(code))
                {
                    Condition1.DoffNo1TraceNo = code;
                }
                else
                {
                    Condition1.DoffNo1TraceNo = null;
                }
                VerifyCondition(out message);
                if (string.IsNullOrEmpty(message))
                    ret = true;
            }

            return ret;
        }

        public bool SetTrace2(string traceNo, out string message)
        {
            bool ret = false;
            message = null;

            if (null != Condition2)
            {
                string code = (!string.IsNullOrEmpty(traceNo)) ? traceNo.Trim() : null;
                if (!string.IsNullOrWhiteSpace(code))
                {
                    Condition2.DoffNo1TraceNo = code;
                }
                else
                {
                    Condition2.DoffNo1TraceNo = null;
                }
                VerifyCondition(out message);
                if (string.IsNullOrEmpty(message))
                    ret = true;
            }

            return ret;
        }

        public void Load()
        {
            Condition1 = S5Condition.GetCurrent(FromSources.RawMeterial).Value();
            if (null == Condition1)
            {
                Condition1 = new S5Condition();
                Condition1.FromSource = FromSources.RawMeterial;
            }
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
                Condition.IssueBy = (null != M3CordApp.Current.User) ? M3CordApp.Current.User.FullName : null;
                Condition.IssueDate = DateTime.Now;

                // Need update G4 pallet status
                string userName = (null != M3CordApp.Current.User) ? M3CordApp.Current.User.FullName : null;

                var g4_1 = G4IssueYarn.SearchG4AgeingPallet(Condition.DoffNo1TraceNo).Value();
                if (null != g4_1)
                {
                    G4IssueYarn.MarkAgeing(g4_1.G4IssueYarnPkId, userName);
                }
                var g4_2 = G4IssueYarn.SearchG4AgeingPallet(Condition.DoffNo2TraceNo).Value();
                if (null != g4_2)
                {
                    G4IssueYarn.MarkAgeing(g4_2.G4IssueYarnPkId, userName);
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

        public S5Condition Condition1
        {
            get; private set;
        }

        public S5Condition Condition2
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
