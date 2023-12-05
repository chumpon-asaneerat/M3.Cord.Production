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
    public class S5ConditionStd
    {
        #region Public Proeprties
        public string ProductCode { get; set; }
        public bool? MainSupplySteamPressureSC { get; set; }
        public decimal? MainSupplySteamPressureSet { get; set; }
        public decimal? MainSupplySteamPressureSetErr { get; set; }
        public decimal? MainSupplySteamPressureSpec { get; set; }
        public decimal? MainSupplySteamPressureSpecErr { get; set; }
        public bool? AgeingSteamPressureSC { get; set; }
        public decimal? AgeingSteamPressureSet { get; set; }
        public decimal? AgeingSteamPressureSetErr { get; set; }
        public decimal? AgeingSteamPressureSpec { get; set; }
        public decimal? AgeingSteamPressureSpecErr { get; set; }
        public bool? SettingTemperatureSC { get; set; }
        public decimal? SettingTemperatureSet { get; set; }
        public decimal? SettingTemperatureSetErr { get; set; }
        public decimal? SettingTemperatureSpec { get; set; }
        public decimal? SettingTemperatureSpecErr { get; set; }
        public bool? SettingTimeSC { get; set; }
        public decimal? SettingTimeSet { get; set; }
        public decimal? SettingTimeSetErr { get; set; }
        public decimal? SettingTimeSpec { get; set; }
        public decimal? SettingTimeSpecErr { get; set; }
        public bool? LampStatusSystemSC { get; set; }
        public string LampStatusSystemSet { get; set; }
        public string LampStatusSystemSpec { get; set; }
        public bool? SoudSystemTemperatureSC { get; set; }
        public string SoudSystemTemperatureSet { get; set; }
        public string SoudSystemTemperatureSpec { get; set; }
        public bool? YarnTypeSC { get; set; }
        public string YarnTypeSet { get; set; }
        public string YarnTypeSpec { get; set; }
        public bool? CordStructureSC { get; set; }
        public string CordStructureSet { get; set; }
        public string CordStructureSpec { get; set; }
        public bool? TwistingNoSC { get; set; }
        public decimal? TwistingNoSet { get; set; }
        public decimal? TwistingNoSetErr { get; set; }
        public decimal? TwistingNoSpec { get; set; }
        public decimal? TwistingNoSpecErr { get; set; }
        public bool? UnitWeightSC { get; set; }
        public decimal? UnitWeightSet { get; set; }
        public decimal? UnitWeightSetErr { get; set; }
        public decimal? UnitWeightSpec { get; set; }
        public decimal? UnitWeightSpecErr { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<S5ConditionStd>> Gets(string productCode)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<S5ConditionStd>> rets = new NDbResult<List<S5ConditionStd>>();

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
            p.Add("@ProductCode", productCode);

            try
            {
                var items = cnn.Query<S5ConditionStd>("GetS5ConditionStd", p,
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
                rets.data = new List<S5ConditionStd>();
            }

            return rets;
        }

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<S5ConditionStd>> GetsByDIPCode(string productCode)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<S5ConditionStd>> rets = new NDbResult<List<S5ConditionStd>>();

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
            p.Add("@DIPProductCode", productCode);

            try
            {
                var items = cnn.Query<S5ConditionStd>("GetS5ConditionStdByDIPCode", p,
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
                rets.data = new List<S5ConditionStd>();
            }

            return rets;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The S5ConditionStd item to save.</param>
        /// <returns></returns>
        public static NDbResult<S5ConditionStd> Save(S5ConditionStd value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S5ConditionStd> ret = new NDbResult<S5ConditionStd>();

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
            p.Add("@MainSupplySteamPressureSpec", value.MainSupplySteamPressureSpec);
            p.Add("@MainSupplySteamPressureSpecErr", value.MainSupplySteamPressureSpecErr);
            p.Add("@AgeingSteamPressureSC", value.AgeingSteamPressureSC);
            p.Add("@AgeingSteamPressureSet", value.AgeingSteamPressureSet);
            p.Add("@AgeingSteamPressureSetErr", value.AgeingSteamPressureSetErr);
            p.Add("@AgeingSteamPressureSpec", value.AgeingSteamPressureSpec);
            p.Add("@AgeingSteamPressureSpecErr", value.AgeingSteamPressureSpecErr);
            p.Add("@SettingTemperatureSC", value.SettingTemperatureSC);
            p.Add("@SettingTemperatureSet", value.SettingTemperatureSet);
            p.Add("@SettingTemperatureSetErr", value.SettingTemperatureSetErr);
            p.Add("@SettingTemperatureSpec", value.SettingTemperatureSpec);
            p.Add("@SettingTemperatureSpecErr", value.SettingTemperatureSpecErr);
            p.Add("@SettingTimeSC", value.SettingTimeSC);
            p.Add("@SettingTimeSet", value.SettingTimeSet);
            p.Add("@SettingTimeSetErr", value.SettingTimeSetErr);
            p.Add("@SettingTimeSpec", value.SettingTimeSpec);
            p.Add("@SettingTimeSpecErr", value.SettingTimeSpecErr);
            p.Add("@LampStatusSystemSC", value.LampStatusSystemSC);
            p.Add("@LampStatusSystemSet", value.LampStatusSystemSet);
            p.Add("@LampStatusSystemSpec", value.LampStatusSystemSpec);
            p.Add("@SoudSystemTemperatureSC", value.SoudSystemTemperatureSC);
            p.Add("@SoudSystemTemperatureSet", value.SoudSystemTemperatureSet);
            p.Add("@SoudSystemTemperatureSpec", value.SoudSystemTemperatureSpec);
            p.Add("@YarnTypeSC", value.YarnTypeSC);
            p.Add("@YarnTypeSet", value.YarnTypeSet);
            p.Add("@YarnTypeSpec", value.YarnTypeSpec);
            p.Add("@CordStructureSC", value.CordStructureSC);
            p.Add("@CordStructureSet", value.CordStructureSet);
            p.Add("@CordStructureSpec", value.CordStructureSpec);
            p.Add("@TwistingNoSC", value.TwistingNoSC);
            p.Add("@TwistingNoSet", value.TwistingNoSet);
            p.Add("@TwistingNoSetErr", value.TwistingNoSetErr);
            p.Add("@TwistingNoSpec", value.TwistingNoSpec);
            p.Add("@TwistingNoSpecErr", value.TwistingNoSpecErr);
            p.Add("@UnitWeightSC", value.UnitWeightSC);
            p.Add("@UnitWeightSet", value.UnitWeightSet);
            p.Add("@UnitWeightSetErr", value.UnitWeightSetErr);
            p.Add("@UnitWeightSpec", value.UnitWeightSpec);
            p.Add("@UnitWeightSpecErr", value.UnitWeightSpecErr);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveS5ConditionStd", p, commandType: CommandType.StoredProcedure);
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

        public static NDbResult Delete(S5ConditionStd value)
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
            p.Add("@ProductCode", value.ProductCode);

            try
            {
                cnn.Execute("DELETE FROM S5ConditionStd WHERE ProductCode = @ProductCode", p, commandType: CommandType.Text);
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
