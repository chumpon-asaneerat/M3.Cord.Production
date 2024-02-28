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
    public class S1ConditionStd
    {
        #region Public Proeprties
        public string ProductCode { get; set; }
        public bool? RingDiameterSC { get; set; }
        public string RingDiameter { get; set; }
        public bool? TwistChangeGearSC { get; set; }
        public decimal? TwistChangeGearI { get; set; }
        public decimal? TwistChangeGearJ { get; set; }
        public decimal? TwistChangeGearK { get; set; }
        public bool? LifterChangeGearSC { get; set; }
        public decimal? LifterChangeGearA { get; set; }
        public decimal? LifterChangeGearB { get; set; }
        public bool? OuterDiameterSC { get; set; }
        public decimal? OuterDiameter { get; set; }
        public bool? TravellerNoSC { get; set; }
        public string TravellerNo { get; set; }
        public bool? CouterUnitSC { get; set; }
        public decimal? CouterUnit { get; set; }
        public decimal? CouterUnitErr { get; set; }
        public bool? CouterUnit2SC { get; set; }
        public decimal? CouterUnit2 { get; set; }
        public decimal? CouterUnit2Err { get; set; }
        public bool? CouterWeightSC { get; set; }
        public decimal? CouterWeight { get; set; }
        public decimal? CouterWeightErr { get; set; }
        public bool? CouterWeight2SC { get; set; }
        public decimal? CouterWeight2 { get; set; }
        public decimal? CouterWeight2Err { get; set; }
        public bool? CounterSystemSC { get; set; }
        public string CounterSystem { get; set; }
        public bool? SenserYarnBreakSC { get; set; }
        public string SenserYarnBreak { get; set; }
        public bool? CalculatedTwistingNumberSC { get; set; }
        public decimal? CalculatedTwistingNumber { get; set; }
        public string CalculatedTwistingNumberType { get; set; }
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
        public static NDbResult<List<S1ConditionStd>> Gets(string productCode)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<S1ConditionStd>> rets = new NDbResult<List<S1ConditionStd>>();

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
                var items = cnn.Query<S1ConditionStd>("GetS1ConditionStd", p,
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
                rets.data = new List<S1ConditionStd>();
            }

            return rets;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The S1ConditionStandard item to save.</param>
        /// <returns></returns>
        public static NDbResult<S1ConditionStd> Save(S1ConditionStd value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S1ConditionStd> ret = new NDbResult<S1ConditionStd>();

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
            p.Add("@RingDiameterSC", value.RingDiameterSC);
            p.Add("@RingDiameter", value.RingDiameter);
            p.Add("@TwistChangeGearSC", value.TwistChangeGearSC);
            p.Add("@TwistChangeGearI", value.TwistChangeGearI);
            p.Add("@TwistChangeGearJ", value.TwistChangeGearJ);
            p.Add("@TwistChangeGearK", value.TwistChangeGearK);
            p.Add("@LifterChangeGearSC", value.LifterChangeGearSC);
            p.Add("@LifterChangeGearA", value.LifterChangeGearA);
            p.Add("@LifterChangeGearB", value.LifterChangeGearB);
            p.Add("@OuterDiameterSC", value.OuterDiameterSC);
            p.Add("@OuterDiameter", value.OuterDiameter);
            p.Add("@TravellerNoSC", value.TravellerNoSC);
            p.Add("@TravellerNo", value.TravellerNo);
            p.Add("@CouterUnitSC", value.CouterUnitSC);
            p.Add("@CouterUnit", value.CouterUnit);
            p.Add("@CouterUnitErr", value.CouterUnitErr);
            p.Add("@CouterUnit2SC", value.CouterUnit2SC);
            p.Add("@CouterUnit2", value.CouterUnit2);
            p.Add("@CouterUnit2Err", value.CouterUnit2Err);
            p.Add("@CouterWeightSC", value.CouterWeightSC);
            p.Add("@CouterWeight", value.CouterWeight);
            p.Add("@CouterWeightErr", value.CouterWeightErr);
            p.Add("@CouterWeight2SC", value.CouterWeight2SC);
            p.Add("@CouterWeight2", value.CouterWeight2);
            p.Add("@CouterWeight2Err", value.CouterWeight2Err);
            p.Add("@CounterSystemSC", value.CounterSystemSC);
            p.Add("@CounterSystem", value.CounterSystem);
            p.Add("@SenserYarnBreakSC", value.SenserYarnBreakSC);
            p.Add("@SenserYarnBreak", value.SenserYarnBreak);
            p.Add("@CalculatedTwistingNumberSC", value.CalculatedTwistingNumberSC);
            p.Add("@CalculatedTwistingNumber", value.CalculatedTwistingNumber);
            p.Add("@CalculatedTwistingNumberType", value.CalculatedTwistingNumberType);
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
                cnn.Execute("SaveS1ConditionStd", p, commandType: CommandType.StoredProcedure);
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

        public static NDbResult Delete(S1ConditionStd value)
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
                cnn.Execute("DELETE FROM S1ConditionStd WHERE ProductCode = @ProductCode", p, commandType: CommandType.Text);
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
