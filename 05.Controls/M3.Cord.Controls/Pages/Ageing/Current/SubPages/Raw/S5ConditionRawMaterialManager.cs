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

            if (null != ConditionL)
            { 
                // pallet 1
                if (!string.IsNullOrEmpty(ConditionL.DoffNo1TraceNo))
                {
                    // Special case for Raw Material
                    // Item Yarn must be 1620-288-707 and Product Code must be 1800TW
                    var code = "1800TW";
                    var name = "BRAKE HOSE";

                    // find pallet in G4
                    var g4 = G4IssueYarn.SearchG4AgeingPallet(ConditionL.DoffNo1TraceNo).Value();
                    if (null != g4)
                    {
                        ConditionL.DoffNo1PalletCode = g4.PalletNo;
                        ConditionL.ProductCode1 = code;
                        ConditionL.ProductName1 = name;

                        Std1 = (!string.IsNullOrWhiteSpace(code)) ? GetStd(code) : null;
                    }
                    else
                    {
                        errMsg = "Trace No Not found.";
                        ConditionL.DoffNo1PalletCode = null;
                        ConditionL.ProductCode1 = null;
                        ConditionL.ProductName1 = null;
                        Std1 = null;
                    }
                }
                else
                {
                    ConditionL.DoffNo1PalletCode = null;
                    ConditionL.ProductCode1 = null;
                    ConditionL.ProductName1 = null;
                    Std1 = null;
                }
            }

            if (null != ConditionR)
            {
                // pallet 2
                if (!string.IsNullOrEmpty(ConditionR.DoffNo1TraceNo))
                {
                    // Special case for Raw Material
                    // Item Yarn must be 1620-288-707 and Product Code must be 1800TW
                    var code = "1800TW";
                    var name = "BRAKE HOSE";

                    // find pallet in G4
                    var g4 = G4IssueYarn.SearchG4AgeingPallet(ConditionR.DoffNo1TraceNo).Value();
                    if (null != g4)
                    {

                        ConditionR.DoffNo1PalletCode = g4.PalletNo;
                        ConditionR.ProductCode1 = code;
                        ConditionR.ProductName1 = name;
                        Std2 = (!string.IsNullOrWhiteSpace(code)) ? GetStd(code) : null;
                    }
                    else
                    {
                        errMsg = "Trace No Not found.";
                        ConditionR.DoffNo1PalletCode = null;
                        ConditionR.ProductCode1 = null;
                        ConditionR.ProductName1 = null;
                        Std2 = null;
                    }
                }
                else
                {
                    ConditionR.DoffNo1PalletCode = null;
                    ConditionR.ProductCode1 = null;
                    ConditionR.ProductName1 = null;
                    Std2 = null;
                }
            }

            if (IsDuplicate())
            {
                errMsg = "หมายเลข Trace No ด้านซ้ายและด้านขวา ซ้ำกัน";
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

                //Condition.LotOrTraceNo = LotOrTraceNo1;
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
                //Condition.LotOrTraceNo = null;
            }
        }

        #endregion

        #region Public Methods

        public bool IsDuplicate()
        {
            bool ret = false;
            if (null != ConditionL && null != ConditionR)
            {
                if (!string.IsNullOrEmpty(ConditionL.DoffNo1TraceNo) &&
                    !string.IsNullOrEmpty(ConditionR.DoffNo1TraceNo) &&
                    ConditionL.DoffNo1TraceNo.Trim() == ConditionR.DoffNo1TraceNo.Trim())
                {
                    ret = true;
                }
            }

            return ret;
        }

        public bool SetTrace1(string traceNo, out string message)
        {
            bool ret = false;
            message = null;

            if (null != ConditionL)
            {
                string code = (!string.IsNullOrEmpty(traceNo)) ? traceNo.Trim() : null;
                if (!string.IsNullOrWhiteSpace(code))
                {
                    ConditionL.DoffNo1TraceNo = code;
                }
                else
                {
                    ConditionL.DoffNo1TraceNo = null;
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

            if (null != ConditionR)
            {
                string code = (!string.IsNullOrEmpty(traceNo)) ? traceNo.Trim() : null;
                if (!string.IsNullOrWhiteSpace(code))
                {
                    ConditionR.DoffNo1TraceNo = code;
                }
                else
                {
                    ConditionR.DoffNo1TraceNo = null;
                }
                VerifyCondition(out message);
                if (string.IsNullOrEmpty(message))
                    ret = true;
            }

            return ret;
        }

        public void LoadL()
        {
            ConditionL = S5Condition.GetCurrent(FromSources.RawMeterial, "L").Value();
            if (null == ConditionL)
            {
                ConditionL = new S5Condition();
                ConditionL.FromSource = FromSources.RawMeterial;
            }

            if (null != ConditionL && string.IsNullOrEmpty(ConditionL.MCSide)) ConditionL.MCSide = "L";
        }

        public void LoadR()
        {
            ConditionR = S5Condition.GetCurrent(FromSources.RawMeterial, "R").Value();
            if (null == ConditionR)
            {
                ConditionR = new S5Condition();
                ConditionR.FromSource = FromSources.RawMeterial;
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

                // Need update G4 pallet status
                string userName = (null != M3CordApp.Current.User) ? M3CordApp.Current.User.FullName : null;

                var g4_1 = G4IssueYarn.SearchG4AgeingPallet(ConditionL.DoffNo1TraceNo).Value();
                if (null != g4_1)
                {
                    G4IssueYarn.MarkAgeing(g4_1.G4IssueYarnPkId, userName);
                }
                /*
                var g4_2 = G4IssueYarn.SearchG4AgeingPallet(ConditionL.DoffNo2TraceNo).Value();
                if (null != g4_2)
                {
                    G4IssueYarn.MarkAgeing(g4_2.G4IssueYarnPkId, userName);
                }
                */

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

                // Need update G4 pallet status
                string userName = (null != M3CordApp.Current.User) ? M3CordApp.Current.User.FullName : null;

                var g4_1 = G4IssueYarn.SearchG4AgeingPallet(ConditionR.DoffNo1TraceNo).Value();
                if (null != g4_1)
                {
                    G4IssueYarn.MarkAgeing(g4_1.G4IssueYarnPkId, userName);
                }
                /*
                var g4_2 = G4IssueYarn.SearchG4AgeingPallet(ConditionR.DoffNo2TraceNo).Value();
                if (null != g4_2)
                {
                    G4IssueYarn.MarkAgeing(g4_2.G4IssueYarnPkId, userName);
                }
                */

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
                    //bool b2 = Std1.SettingTimeSet == Std2.SettingTimeSet;
                    bool b2 = true;
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
