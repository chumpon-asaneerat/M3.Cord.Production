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
    public class S4x2ConditionStd
    {
        #region Public Proeprties
        public string ProductCode { get; set; }
        public bool? FullPackageModeSC { get; set; }
        public string FullPackageMode { get; set; }
        public bool? LengthSC { get; set; }
        public decimal? Length { get; set; }
        public decimal? LengthErr { get; set; }
        public bool? WindAngleShiftFunctionSC { get; set; }
        public string WindAngleShiftFunction { get; set; }
        public bool? AmountOfDisturbSC { get; set; }
        public string AmountOfDisturb { get; set; }
        public bool? WindAngleSC { get; set; }
        public decimal? WindAngle { get; set; }
        public bool? DistuebAccelerationTimeSC { get; set; }
        public decimal? DistuebAccelerationTime { get; set; }
        public bool? DistuebDccelerationTimeSC { get; set; }
        public decimal? DistuebDccelerationTime { get; set; }
        public bool? TensorSettingSC { get; set; }
        public string TensorSetting { get; set; }
        public bool? FeedRollerSettingSC { get; set; }
        public string FeedRollerSetting { get; set; }
        public bool? BollonSettingSC { get; set; }
        public string BollonSetting { get; set; }
        public bool? MethodOfKnotSC { get; set; }
        public string MethodOfKnot { get; set; }
        public bool? WindQuantitySC { get; set; }
        public decimal? WindQuantity { get; set; }
        public decimal? WindQuantityErr { get; set; }
        public bool? DoffTimeSC { get; set; }
        public decimal? DoffTime { get; set; }
        public decimal? DoffTimeErr { get; set; }
        public bool? ProductQuantitySC { get; set; }
        public decimal? ProductQuantityKg { get; set; }
        public decimal? ProductQuantityDay { get; set; }
        public bool? CalculatedTwistingNumberSC { get; set; }
        public decimal? CalculatedTwistingNumber { get; set; }
        public decimal? CalculatedTwistingNumberErr { get; set; }
        public bool? NumberOfSpindleRotationSC { get; set; }
        public decimal? NumberOfSpindleRotation { get; set; }
        public decimal? NumberOfSpindleRotationErr { get; set; }
        public bool? YarnSpeedSC { get; set; }
        public decimal? YarnSpeed { get; set; }
        public decimal? YarnSpeedErr { get; set; }
        public bool? SpecialMentionSC { get; set; }
        public string SpecialMention { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<S4x2ConditionStd>> Gets(string productCode)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<S4x2ConditionStd>> rets = new NDbResult<List<S4x2ConditionStd>>();

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
                var items = cnn.Query<S4x2ConditionStd>("GetS4x2ConditionStd", p,
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
                rets.data = new List<S4x2ConditionStd>();
            }

            return rets;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The S4x2ConditionStd item to save.</param>
        /// <returns></returns>
        public static NDbResult<S4x2ConditionStd> Save(S4x2ConditionStd value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S4x2ConditionStd> ret = new NDbResult<S4x2ConditionStd>();

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
            p.Add("@FullPackageModeSC", value.FullPackageModeSC);
            p.Add("@FullPackageMode", value.FullPackageMode);
            p.Add("@LengthSC", value.LengthSC);
            p.Add("@Length", value.Length);
            p.Add("@LengthErr", value.LengthErr);
            p.Add("@WindAngleShiftFunctionSC", value.WindAngleShiftFunctionSC);
            p.Add("@WindAngleShiftFunction", value.WindAngleShiftFunction);
            p.Add("@AmountOfDisturbSC", value.AmountOfDisturbSC);
            p.Add("@AmountOfDisturb", value.AmountOfDisturb);
            p.Add("@WindAngleSC", value.WindAngleSC);
            p.Add("@WindAngle", value.WindAngle);
            p.Add("@DistuebAccelerationTimeSC", value.DistuebAccelerationTimeSC);
            p.Add("@DistuebAccelerationTime", value.DistuebAccelerationTime);
            p.Add("@DistuebDccelerationTimeSC", value.DistuebDccelerationTimeSC);
            p.Add("@DistuebDccelerationTime", value.DistuebDccelerationTime);
            p.Add("@TensorSettingSC", value.TensorSettingSC);
            p.Add("@TensorSetting", value.TensorSetting);
            p.Add("@FeedRollerSettingSC", value.FeedRollerSettingSC);
            p.Add("@FeedRollerSetting", value.FeedRollerSetting);
            p.Add("@BollonSettingSC", value.BollonSettingSC);
            p.Add("@BollonSetting", value.BollonSetting);
            p.Add("@MethodOfKnotSC", value.MethodOfKnotSC);
            p.Add("@MethodOfKnot", value.MethodOfKnot);
            p.Add("@WindQuantitySC", value.WindQuantitySC);
            p.Add("@WindQuantity", value.WindQuantity);
            p.Add("@WindQuantityErr", value.WindQuantityErr);
            p.Add("@DoffTimeSC", value.DoffTimeSC);
            p.Add("@DoffTime", value.DoffTime);
            p.Add("@DoffTimeErr", value.DoffTimeErr);
            p.Add("@ProductQuantitySC", value.ProductQuantitySC);
            p.Add("@ProductQuantityKg", value.ProductQuantityKg);
            p.Add("@ProductQuantityDay", value.ProductQuantityDay);
            p.Add("@CalculatedTwistingNumberSC", value.CalculatedTwistingNumberSC);
            p.Add("@CalculatedTwistingNumber", value.CalculatedTwistingNumber);
            p.Add("@CalculatedTwistingNumberErr", value.CalculatedTwistingNumberErr);
            p.Add("@NumberOfSpindleRotationSC", value.NumberOfSpindleRotationSC);
            p.Add("@NumberOfSpindleRotation", value.NumberOfSpindleRotation);
            p.Add("@NumberOfSpindleRotationErr", value.NumberOfSpindleRotationErr);
            p.Add("@YarnSpeedSC", value.YarnSpeedSC);
            p.Add("@YarnSpeed", value.YarnSpeed);
            p.Add("@YarnSpeedErr", value.YarnSpeedErr);
            p.Add("@SpecialMentionSC", value.SpecialMentionSC);
            p.Add("@SpecialMention", value.SpecialMention);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveS4x2ConditionStd", p, commandType: CommandType.StoredProcedure);
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

        public static NDbResult Delete(S4x2ConditionStd value)
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
                cnn.Execute("DELETE FROM S4x2ConditionStd WHERE ProductCode = @ProductCode", p, commandType: CommandType.Text);
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
