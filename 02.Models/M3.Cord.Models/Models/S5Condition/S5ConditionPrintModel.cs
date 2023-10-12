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
    public class S5ConditionPrintModel
    {
        #region Public Proeprties
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
        public string DoffNo1MCNo { get; set; }
        public string DoffNo1Doff { get; set; }
        public string DoffNo1Qty { get; set; }
        public bool? DoffNo2SC { get; set; }
        public string DoffNo2MCNo { get; set; }
        public string DoffNo2Doff { get; set; }
        public string DoffNo2Qty { get; set; }

        #endregion

        public static List<S5ConditionPrintModel> GetSamples()
        {
            var results = new List<S5ConditionPrintModel>();
            
            results.Add(new S5ConditionPrintModel() { DoffNo1Doff = "1", DoffNo1MCNo = "S-1-1" });
            results.Add(new S5ConditionPrintModel() { DoffNo1Doff = "2", DoffNo1MCNo = "S-4-1" });

            return results;
        }
    }
}
