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
using System.Runtime.InteropServices.WindowsRuntime;

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

    public class S5Source
    {
        public int? Id { get; set; }
        public string Source { get; set; }

        public static List<S5Source> Gets()
        {
            return new List<S5Source>() 
            {
                new S5Source() { Id = new int?(), Source = "All" },
                new S5Source() { Id = 0, Source = "Raw" },
                new S5Source() { Id = 1, Source = "Twist" },
                new S5Source() { Id = 4, Source = "DIP" }
            };
        }
    }

    public class S5Condition : NInpc
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        #endregion

        public SolidColorBrush TextColor { get { return BlackColor; } set { } }

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

        #region LampStatusSystemSetActual with Yes/No Runtime properties

        public bool? LampStatusSystemSetActual 
        {
            get { return Get<bool?>(); }
            set
            {
                Set(value, () => 
                {
                    Raise(() => LampStatusSystemSetActualY);
                    Raise(() => LampStatusSystemSetActualN);
                });
            }
        }

        public bool? LampStatusSystemSetActualY
        {
            get
            {
                if (LampStatusSystemSetActual.HasValue)
                    return (LampStatusSystemSetActual.Value == true);
                else return LampStatusSystemSetActual;
            }
            set { LampStatusSystemSetActual = true; }
        }

        public bool? LampStatusSystemSetActualN
        {
            get
            {
                if (LampStatusSystemSetActual.HasValue)
                    return (LampStatusSystemSetActual.Value == false);
                else return LampStatusSystemSetActual;
            }
            set { LampStatusSystemSetActual = false; }
        }

        #endregion

        public string LampStatusSystemSpec { get; set; }

        #region LampStatusSystemSpecActual with Yes/No Runtime properties

        public bool? LampStatusSystemSpecActual
        {
            get { return Get<bool?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => LampStatusSystemSpecActualY);
                    Raise(() => LampStatusSystemSpecActualN);
                });
            }
        }

        public bool? LampStatusSystemSpecActualY
        {
            get
            {
                if (LampStatusSystemSpecActual.HasValue)
                    return (LampStatusSystemSpecActual.Value == true);
                else return LampStatusSystemSpecActual;
            }
            set { LampStatusSystemSpecActual = true; }
        }

        public bool? LampStatusSystemSpecActualN
        {
            get
            {
                if (LampStatusSystemSpecActual.HasValue)
                    return (LampStatusSystemSpecActual.Value == false);
                else return LampStatusSystemSpecActual;
            }
            set { LampStatusSystemSpecActual = false; }
        }

        #endregion

        public bool? SoudSystemTemperatureSC { get; set; }
        public string SoudSystemTemperatureSet { get; set; }

        #region SoudSystemTemperatureSetActual with Yes/No Runtime properties

        public bool? SoudSystemTemperatureSetActual
        {
            get { return Get<bool?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => SoudSystemTemperatureSetActualY);
                    Raise(() => SoudSystemTemperatureSetActualN);
                });
            }
        }

        public bool? SoudSystemTemperatureSetActualY
        {
            get
            {
                if (SoudSystemTemperatureSetActual.HasValue)
                    return (SoudSystemTemperatureSetActual.Value == true);
                else return SoudSystemTemperatureSetActual;
            }
            set { SoudSystemTemperatureSetActual = true; }
        }

        public bool? SoudSystemTemperatureSetActualN
        {
            get
            {
                if (SoudSystemTemperatureSetActual.HasValue)
                    return (SoudSystemTemperatureSetActual.Value == false);
                else return SoudSystemTemperatureSetActual;
            }
            set { SoudSystemTemperatureSetActual = false; }
        }

        #endregion

        public string SoudSystemTemperatureSpec { get; set; }

        #region SoudSystemTemperatureSpecActual with Yes/No Runtime properties

        public bool? SoudSystemTemperatureSpecActual
        {
            get { return Get<bool?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => SoudSystemTemperatureSpecActualY);
                    Raise(() => SoudSystemTemperatureSpecActualN);
                });
            }
        }

        public bool? SoudSystemTemperatureSpecActualY
        {
            get
            {
                if (SoudSystemTemperatureSpecActual.HasValue)
                    return (SoudSystemTemperatureSpecActual.Value == true);
                else return SoudSystemTemperatureSpecActual;
            }
            set { SoudSystemTemperatureSpecActual = true; }
        }

        public bool? SoudSystemTemperatureSpecActualN
        {
            get
            {
                if (SoudSystemTemperatureSpecActual.HasValue)
                    return (SoudSystemTemperatureSpecActual.Value == false);
                else return SoudSystemTemperatureSpecActual;
            }
            set { SoudSystemTemperatureSpecActual = false; }
        }

        #endregion

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

        public string PalletOrTrace1
        {
            get
            {
                if (FromSource == FromSources.RawMeterial)
                    return DoffNo1TraceNo;
                else return DoffNo1PalletCode;
            }
        }

        public bool? DoffNo2SC { get; set; }
        public string DoffNo2PalletCode { get; set; }
        public string DoffNo2TraceNo { get; set; }
        public string DoffNo2MCNo { get; set; }
        public string DoffNo2Doff { get; set; }
        public string DoffNo2Qty { get; set; }

        public string PalletOrTrace2
        {
            get 
            {
                if (FromSource == FromSources.RawMeterial)
                    return DoffNo2TraceNo;
                else return DoffNo2PalletCode;
            }
        }

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

        public string SFromSource
        {
            get 
            {
                if (FromSource == FromSources.RawMeterial)
                    return "Raw";
                else if (FromSource == FromSources.Twist)
                    return "Twist";
                else if (FromSource == FromSources.DIP)
                    return "DIP";
                else return null;
            }
            set { }
        }

        public string CustomerName { get; set; }
        public string ProductName1 { get; set; }
        public string ProductName2 { get; set; }
        public string IssueBy { get; set; }
        public DateTime? IssueDate { get; set; }

        public string MCSide { get; set; }

        #endregion

        #region Static Methods

        public static S5Condition Create(string productCode, string mcSide)
        {
            var inst = new S5Condition();
            var std = S5ConditionStd.Gets(productCode).Value().FirstOrDefault();
            Assign(std, inst);
            inst.MCSide = mcSide;
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

                // Spec = Set
                dst.MainSupplySteamPressureSpec = src.MainSupplySteamPressureSet;
                dst.MainSupplySteamPressureSpecErr = src.MainSupplySteamPressureSetErr;
                dst.AgeingSteamPressureSpec = src.AgeingSteamPressureSet;
                dst.AgeingSteamPressureSpecErr = src.AgeingSteamPressureSetErr;
                dst.SettingTemperatureSpec = src.SettingTemperatureSet;
                dst.SettingTemperatureSpecErr = src.SettingTemperatureSetErr;
                dst.SettingTimeSpec = src.SettingTimeSet;
                dst.SettingTimeSpecErr = src.SettingTimeSetErr;
                dst.YarnTypeSpec = src.YarnTypeSet;
                dst.CordStructureSpec = src.CordStructureSet;
                dst.TwistingNoSpec = src.TwistingNoSet;
                dst.TwistingNoSpecErr = src.TwistingNoSetErr;
                dst.UnitWeightSpec = src.UnitWeightSet;
                dst.UnitWeightSpecErr = src.UnitWeightSetErr;
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
        /// Get Current
        /// </summary>
        /// <returns></returns>
        public static NDbResult<S5Condition> GetCurrent(FromSources fromSource, string mcSide)
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
            p.Add("@MCSide", mcSide);
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

                // Set PK
                value.S5ConditionId = p.Get<dynamic>("@S5ConditionId");
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
        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<S5Condition>> Search(
            DateTime? IssueDate = new DateTime?(),
            int? FromSource = new int?(),
            string ProductCode = null,
            string CustomerName = null,
            string PalletOrTraceNo = null
            )
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
            p.Add("@IssueDate", IssueDate);
            p.Add("@FromSource", FromSource);
            p.Add("@ProductCode", ProductCode);
            p.Add("@CustomerName", CustomerName);
            p.Add("@PalletOrTraceNo", PalletOrTraceNo);

            try
            {
                var items = cnn.Query<S5Condition>("SearchAgeing", p,
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

        #endregion
    }
}
