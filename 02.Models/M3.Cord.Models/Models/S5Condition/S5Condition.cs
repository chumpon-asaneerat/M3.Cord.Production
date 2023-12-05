#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Reflection;

using System.Windows.Media;

using NLib;
using NLib.Models;

using Dapper;
using Newtonsoft.Json;

#endregion

namespace M3.Cord.Models
{
    public enum FromSources : int
    {
        RawMeterial = 0,
        Twist = 1,
        //Twist2 = 2,
        DIP = 4
    }

    public class S5Condition
    {
        #region Public Proeprties

        public int? S5ConditionId { get; set; }

        public string ProductCode { get; set; }
        public bool? MainSupplySteamPressureSC { get; set; }
        public decimal? MainSupplySteamPressureSet { get; set; }
        public decimal? MainSupplySteamPressureSetErr { get; set; }
        public decimal? MainSupplySteamPressureSetActual { get; set; }
        public decimal? MainSupplySteamPressureSpec { get; set; }
        public decimal? MainSupplySteamPressureSpecErr { get; set; }
        public decimal? MainSupplySteamPressureSpecActual { get; set; }
        public bool? AgeingSteamPressureSC { get; set; }
        public decimal? AgeingSteamPressureSet { get; set; }
        public decimal? AgeingSteamPressureSetErr { get; set; }
        public decimal? AgeingSteamPressureSetActual { get; set; }
        public decimal? AgeingSteamPressureSpec { get; set; }
        public decimal? AgeingSteamPressureSpecErr { get; set; }
        public decimal? AgeingSteamPressureActual { get; set; }
        public bool? SettingTemperatureSC { get; set; }
        public decimal? SettingTemperatureSet { get; set; }
        public decimal? SettingTemperatureSetErr { get; set; }
        public decimal? SettingTemperatureSetActual { get; set; }
        public decimal? SettingTemperatureSpec { get; set; }
        public decimal? SettingTemperatureSpecErr { get; set; }
        public decimal? SettingTemperatureSpecActual { get; set; }
        public bool? SettingTimeSC { get; set; }
        public decimal? SettingTimeSet { get; set; }
        public decimal? SettingTimeSetErr { get; set; }
        public decimal? SettingTimeSetActual { get; set; }
        public decimal? SettingTimeSpec { get; set; }
        public decimal? SettingTimeSpecErr { get; set; }
        public decimal? SettingTimeSpecActual { get; set; }
        public bool? LampStatusSystemSC { get; set; }
        public string LampStatusSystemSet { get; set; }
        public bool? LampStatusSystemSetActual { get; set; }
        public string LampStatusSystemSpec { get; set; }
        public bool? LampStatusSystemSpecActual { get; set; }
        public bool? SoudSystemTemperatureSC { get; set; }
        public string SoudSystemTemperatureSet { get; set; }
        public bool? SoudSystemTemperatureSetActual { get; set; }
        public string SoudSystemTemperatureSpec { get; set; }
        public bool? SoudSystemTemperatureSpecActual { get; set; }
        public bool? YarnTypeSC { get; set; }
        public string YarnTypeSet { get; set; }
        public string YarnTypeSetActual { get; set; }
        public string YarnTypeSpec { get; set; }
        public string YarnTypeSpecActual { get; set; }
        public bool? CordStructureSC { get; set; }
        public string CordStructureSet { get; set; }
        public string CordStructureSetActual { get; set; }
        public string CordStructureSpec { get; set; }
        public string CordStructureSpecActual { get; set; }
        public bool? TwistingNoSC { get; set; }
        public decimal? TwistingNoSet { get; set; }
        public decimal? TwistingNoSetErr { get; set; }
        public decimal? TwistingNoSetActual { get; set; }
        public decimal? TwistingNoSpec { get; set; }
        public decimal? TwistingNoSpecErr { get; set; }
        public decimal? TwistingNoSpecActual { get; set; }
        public bool? UnitWeightSC { get; set; }
        public decimal? UnitWeightSet { get; set; }
        public decimal? UnitWeightSetErr { get; set; }
        public decimal? UnitWeightSetActual { get; set; }
        public decimal? UnitWeightSpec { get; set; }
        public decimal? UnitWeightSpecErr { get; set; }
        public decimal? UnitWeightSpecActual { get; set; }
        public bool? StartingTimeSC { get; set; }
        public DateTime? StartingTimeSettingTime { get; set; }
        public DateTime? StartingTimeStartAgeingTime { get; set; }
        public bool? FinishTimeSC { get; set; }
        public DateTime? FinishTime { get; set; }
        public bool? OutTimeSC { get; set; }
        public DateTime? OutTime { get; set; }

        public bool? DoffNo1SC { get; set; }
        public string DoffNo1PalletCode { get; set; }
        public string DoffNo1TraceNo { get; set; }
        public string DoffNo1MCNo { get; set; }
        public string DoffNo1Doff { get; set; }
        public string DoffNo1Qty { get; set; }

        public bool? DoffNo2SC { get; set; }
        public string DoffNo2PalletCode { get; set; }
        public string DoffNo2TraceNo { get; set; }
        public string DoffNo2MCNo { get; set; }
        public string DoffNo2Doff { get; set; }
        public string DoffNo2Qty { get; set; }

        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CheckedBy { get; set; }
        public DateTime? CheckedDate { get; set; }
        public string ApproveBy { get; set; }
        public DateTime? ApproveDate { get; set; }

        public string ShiftLeader { get; set; }
        public string ProductionManager { get; set; }

        public string Remark { get; set; }

        public string ProductCode1 { get; set; }
        public string ProductCode2 { get; set; }

        public FromSources FromSource { get; set; }

        public string CustomerName { get; set; }
        public string ProductName1 { get; set; }
        public string ProductName2 { get; set; }
        public string IssueBy { get; set; }
        public DateTime? IssueDate { get; set; }

        #endregion

        #region Static Methods

        public static S5Condition Create(string productCode)
        {
            var inst = new S5Condition();
            var std = S5ConditionStd.Gets(productCode).Value().FirstOrDefault();
            Assign(std, inst);
            return inst;
        }

        public static void Assign(S5ConditionStd src, S5Condition dst)
        {
            if (null != src && null != dst)
            {
                dst.ProductCode = src.ProductCode;
                dst.MainSupplySteamPressureSC = src.MainSupplySteamPressureSC;
                dst.MainSupplySteamPressureSet = src.MainSupplySteamPressureSet;
                dst.MainSupplySteamPressureSetErr = src.MainSupplySteamPressureSetErr;
                dst.MainSupplySteamPressureSpec = src.MainSupplySteamPressureSpec;
                dst.MainSupplySteamPressureSpecErr = src.MainSupplySteamPressureSpecErr;
                dst.AgeingSteamPressureSC = src.AgeingSteamPressureSC;
                dst.AgeingSteamPressureSet = src.AgeingSteamPressureSet;
                dst.AgeingSteamPressureSetErr = src.AgeingSteamPressureSetErr;
                dst.AgeingSteamPressureSpec = src.AgeingSteamPressureSpec;
                dst.AgeingSteamPressureSpecErr = src.AgeingSteamPressureSpecErr;
                dst.SettingTemperatureSC = src.SettingTemperatureSC;
                dst.SettingTemperatureSet = src.SettingTemperatureSet;
                dst.SettingTemperatureSetErr = src.SettingTemperatureSetErr;
                dst.SettingTemperatureSpec = src.SettingTemperatureSpec;
                dst.SettingTemperatureSpecErr = src.SettingTemperatureSpecErr;
                dst.SettingTimeSC = src.SettingTimeSC;
                dst.SettingTimeSet = src.SettingTimeSet;
                dst.SettingTimeSetErr = src.SettingTimeSetErr;
                dst.SettingTimeSpec = src.SettingTimeSpec;
                dst.SettingTimeSpecErr = src.SettingTimeSpecErr;
                dst.LampStatusSystemSC = src.LampStatusSystemSC;
                dst.LampStatusSystemSet = src.LampStatusSystemSet;
                dst.LampStatusSystemSpec = src.LampStatusSystemSpec;
                dst.SoudSystemTemperatureSC = src.SoudSystemTemperatureSC;
                dst.SoudSystemTemperatureSet = src.SoudSystemTemperatureSet;
                dst.SoudSystemTemperatureSpec = src.SoudSystemTemperatureSpec;
                dst.YarnTypeSC = src.YarnTypeSC;
                dst.YarnTypeSet = src.YarnTypeSet;
                dst.YarnTypeSpec = src.YarnTypeSpec;
                dst.CordStructureSC = src.CordStructureSC;
                dst.CordStructureSet = src.CordStructureSet;
                dst.CordStructureSpec = src.CordStructureSpec;
                dst.TwistingNoSC = src.TwistingNoSC;
                dst.TwistingNoSet = src.TwistingNoSet;
                dst.TwistingNoSetErr = src.TwistingNoSetErr;
                dst.TwistingNoSpec = src.TwistingNoSpec;
                dst.TwistingNoSpecErr = src.TwistingNoSpecErr;
                dst.UnitWeightSC = src.UnitWeightSC;
                dst.UnitWeightSet = src.UnitWeightSet;
                dst.UnitWeightSetErr = src.UnitWeightSetErr;
                dst.UnitWeightSpec = src.UnitWeightSpec;
                dst.UnitWeightSpecErr = src.UnitWeightSpecErr;
            }
        }
        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<S5Condition>> Gets(int? S5ConditionId = new int?())
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<S5Condition>> rets = new NDbResult<List<S5Condition>>();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                rets.ErrNum = 8000;
                rets.ErrMsg = msg;

                return rets;
            }

            var p = new DynamicParameters();
            p.Add("@S5ConditionId", S5ConditionId);

            try
            {
                var items = cnn.Query<S5Condition>("GetS5Conditions", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList() : null;
                rets.Success(data);
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                rets.ErrNum = 9999;
                rets.ErrMsg = ex.Message;
            }

            if (null == rets.data)
            {
                // create empty list.
                rets.data = new List<S5Condition>();
            }

            return rets;
        }
        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<S5Condition> GetCurrent(FromSources fromSource)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S5Condition> ret = new NDbResult<S5Condition>();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                ret.ErrNum = 8000;
                ret.ErrMsg = msg;

                return ret;
            }

            var p = new DynamicParameters();
            p.Add("@FromSource", (int)fromSource);

            try
            {
                var item = cnn.Query<S5Condition>("GetCurrentS5Condition", p,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                var data = item;
                ret.Success(data);
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
            }

            return ret;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The S5Condition item to save.</param>
        /// <returns></returns>
        public static NDbResult<S5Condition> Save(S5Condition value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S5Condition> ret = new NDbResult<S5Condition>();

            if (null == value)
            {
                ret.ParameterIsNull();
                return ret;
            }

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                ret.ErrNum = 8000;
                ret.ErrMsg = msg;

                return ret;
            }

            var p = new DynamicParameters();

            p.Add("@ProductCode", value.ProductCode);
            p.Add("@MainSupplySteamPressureSC", value.MainSupplySteamPressureSC);
            p.Add("@MainSupplySteamPressureSet", value.MainSupplySteamPressureSet);
            p.Add("@MainSupplySteamPressureSetErr", value.MainSupplySteamPressureSetErr);
            p.Add("@MainSupplySteamPressureSetActual", value.MainSupplySteamPressureSetActual);
            p.Add("@MainSupplySteamPressureSpec", value.MainSupplySteamPressureSpec);
            p.Add("@MainSupplySteamPressureSpecErr", value.MainSupplySteamPressureSpecErr);
            p.Add("@MainSupplySteamPressureSpecActual", value.MainSupplySteamPressureSpecActual);
            p.Add("@AgeingSteamPressureSC", value.AgeingSteamPressureSC);
            p.Add("@AgeingSteamPressureSet", value.AgeingSteamPressureSet);
            p.Add("@AgeingSteamPressureSetErr", value.AgeingSteamPressureSetErr);
            p.Add("@AgeingSteamPressureSetActual", value.AgeingSteamPressureSetActual);
            p.Add("@AgeingSteamPressureSpec", value.AgeingSteamPressureSpec);
            p.Add("@AgeingSteamPressureSpecErr", value.AgeingSteamPressureSpecErr);
            p.Add("@AgeingSteamPressureActual", value.AgeingSteamPressureActual);
            p.Add("@SettingTemperatureSC", value.SettingTemperatureSC);
            p.Add("@SettingTemperatureSet", value.SettingTemperatureSet);
            p.Add("@SettingTemperatureSetErr", value.SettingTemperatureSetErr);
            p.Add("@SettingTemperatureSetActual", value.SettingTemperatureSetActual);
            p.Add("@SettingTemperatureSpec", value.SettingTemperatureSpec);
            p.Add("@SettingTemperatureSpecErr", value.SettingTemperatureSpecErr);
            p.Add("@SettingTemperatureSpecActual", value.SettingTemperatureSpecActual);
            p.Add("@SettingTimeSC", value.SettingTimeSC);
            p.Add("@SettingTimeSet", value.SettingTimeSet);
            p.Add("@SettingTimeSetErr", value.SettingTimeSetErr);
            p.Add("@SettingTimeSetActual", value.SettingTimeSetActual);
            p.Add("@SettingTimeSpec", value.SettingTimeSpec);
            p.Add("@SettingTimeSpecErr", value.SettingTimeSpecErr);
            p.Add("@SettingTimeSpecActual", value.SettingTimeSpecActual);
            p.Add("@LampStatusSystemSC", value.LampStatusSystemSC);
            p.Add("@LampStatusSystemSet", value.LampStatusSystemSet);
            p.Add("@LampStatusSystemSetActual", value.LampStatusSystemSetActual);
            p.Add("@LampStatusSystemSpec", value.LampStatusSystemSpec);
            p.Add("@LampStatusSystemSpecActual", value.LampStatusSystemSpecActual);
            p.Add("@SoudSystemTemperatureSC", value.SoudSystemTemperatureSC);
            p.Add("@SoudSystemTemperatureSet", value.SoudSystemTemperatureSet);
            p.Add("@SoudSystemTemperatureSetActual", value.SoudSystemTemperatureSetActual);
            p.Add("@SoudSystemTemperatureSpec", value.SoudSystemTemperatureSpec);
            p.Add("@SoudSystemTemperatureSpecActual", value.SoudSystemTemperatureSpecActual);
            p.Add("@YarnTypeSC", value.YarnTypeSC);
            p.Add("@YarnTypeSet", value.YarnTypeSet);
            p.Add("@YarnTypeSetActual", value.YarnTypeSetActual);
            p.Add("@YarnTypeSpec", value.YarnTypeSpec);
            p.Add("@YarnTypeSpecActual", value.YarnTypeSpecActual);
            p.Add("@CordStructureSC", value.CordStructureSC);
            p.Add("@CordStructureSet", value.CordStructureSet);
            p.Add("@CordStructureSetActual", value.CordStructureSetActual);
            p.Add("@CordStructureSpec", value.CordStructureSpec);
            p.Add("@CordStructureSpecActual", value.CordStructureSpecActual);
            p.Add("@TwistingNoSC", value.TwistingNoSC);
            p.Add("@TwistingNoSet", value.TwistingNoSet);
            p.Add("@TwistingNoSetErr", value.TwistingNoSetErr);
            p.Add("@TwistingNoSetActual", value.TwistingNoSetActual);
            p.Add("@TwistingNoSpec", value.TwistingNoSpec);
            p.Add("@TwistingNoSpecErr", value.TwistingNoSpecErr);
            p.Add("@TwistingNoSpecActual", value.TwistingNoSpecActual);
            p.Add("@UnitWeightSC", value.UnitWeightSC);
            p.Add("@UnitWeightSet", value.UnitWeightSet);
            p.Add("@UnitWeightSetErr", value.UnitWeightSetErr);
            p.Add("@UnitWeightSetActual", value.UnitWeightSetActual);
            p.Add("@UnitWeightSpec", value.UnitWeightSpec);
            p.Add("@UnitWeightSpecErr", value.UnitWeightSpecErr);
            p.Add("@UnitWeightSpecActual", value.UnitWeightSpecActual);
            p.Add("@StartingTimeSC", value.StartingTimeSC);
            p.Add("@StartingTimeSettingTime", value.StartingTimeSettingTime);
            p.Add("@StartingTimeStartAgeingTime", value.StartingTimeStartAgeingTime);
            p.Add("@FinishTimeSC", value.FinishTimeSC);
            p.Add("@FinishTime", value.FinishTime);
            p.Add("@OutTimeSC", value.OutTimeSC);
            p.Add("@OutTime", value.OutTime);
            p.Add("@DoffNo1SC", value.DoffNo1SC);
            p.Add("@DoffNo1PalletCode", value.DoffNo1PalletCode);
            p.Add("@DoffNo1TraceNo", value.DoffNo1TraceNo);
            p.Add("@DoffNo1MCNo", value.DoffNo1MCNo);
            p.Add("@DoffNo1Doff", value.DoffNo1Doff);
            p.Add("@DoffNo1Qty", value.DoffNo1Qty);
            p.Add("@DoffNo2SC", value.DoffNo2SC);
            p.Add("@DoffNo2PalletCode", value.DoffNo2PalletCode);
            p.Add("@DoffNo2TraceNo", value.DoffNo2TraceNo);
            p.Add("@DoffNo2MCNo", value.DoffNo2MCNo);
            p.Add("@DoffNo2Doff", value.DoffNo2Doff);
            p.Add("@DoffNo2Qty", value.DoffNo2Qty);

            p.Add("@UpdateBy", value.UpdateBy);

            p.Add("@Remark", value.Remark);

            p.Add("@ProductCode1", value.ProductCode1);
            p.Add("@ProductCode2", value.ProductCode2);

            p.Add("@FromSource", value.FromSource);

            p.Add("@CustomerName", value.CustomerName);
            p.Add("@ProductName1", value.ProductName1);
            p.Add("@ProductName2", value.ProductName2);

            p.Add("@IssueBy", value.IssueBy);
            p.Add("@IssueDate", value.IssueDate);

            p.Add("@S5ConditionId", value.S5ConditionId, DbType.Int32, direction: ParameterDirection.InputOutput);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveS5Condition", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);

                // Set error number/message
                ret.ErrNum = p.Get<int>("@errNum");
                ret.ErrMsg = p.Get<string>("@errMsg");
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
            }

            return ret;
        }

        public static NDbResult Delete(S5Condition value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult ret = new NDbResult();

            if (null == value)
            {
                ret.ParameterIsNull();
                return ret;
            }

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                ret.ErrNum = 8000;
                ret.ErrMsg = msg;

                return ret;
            }

            var p = new DynamicParameters();
            p.Add("@S5ConditionId", value.S5ConditionId);

            try
            {
                cnn.Execute("DELETE FROM S5Condition WHERE S5ConditionId = @S5ConditionId", p, commandType: CommandType.Text);
                ret.Success();
                // Set error number/message
                ret.ErrNum = p.Get<int>("@errNum");
                ret.ErrMsg = p.Get<string>("@errMsg");
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
            }

            return ret;
        }

        #endregion
    }
}
