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
using System.Xml.Linq;

#endregion

namespace M3.Cord.Models
{
    public class S5ConditionPrintModel
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
        public string LotOrTraceNo { get; set; }

        public string ProductCode1 { get; set; }
        public string ProductCode2 { get; set; }

        #endregion

        #region Public Methods

        public S5ConditionPrintModel ShallowCopy()
        {
            S5ConditionPrintModel inst = (S5ConditionPrintModel)MemberwiseClone();
            return inst;
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<S5ConditionPrintModel>> Gets(int? S5ConditionId = new int?())
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<S5ConditionPrintModel>> rets = new NDbResult<List<S5ConditionPrintModel>>();

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
                var items = cnn.Query<S5ConditionPrintModel>("GetS5Conditions", p,
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
                rets.data = new List<S5ConditionPrintModel>();
            }

            return rets;
        }

        #endregion
    }
}
