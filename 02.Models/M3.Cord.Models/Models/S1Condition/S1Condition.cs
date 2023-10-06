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
    public class S1Condition
    {
        #region Public Proeprties
        public string ProductCode { get; set; }
        public bool? RingDiameterSC { get; set; }
        public string RingDiameter { get; set; }
        public string RingDiameterActual { get; set; }
        public bool? TwistChangeGearSC { get; set; }
        public decimal? TwistChangeGearI { get; set; }
        public decimal? TwistChangeGearIActual { get; set; }
        public decimal? TwistChangeGearJ { get; set; }
        public decimal? TwistChangeGearJActual { get; set; }
        public decimal? TwistChangeGearK { get; set; }
        public decimal? TwistChangeGearKActual { get; set; }
        public bool? LifterChangeGearSC { get; set; }
        public decimal? LifterChangeGearA { get; set; }
        public decimal? LifterChangeGearAActual { get; set; }
        public decimal? LifterChangeGearB { get; set; }
        public decimal? LifterChangeGearBActual { get; set; }
        public bool? OuterDiameterSC { get; set; }
        public decimal? OuterDiameter { get; set; }
        public decimal? OuterDiameterActual { get; set; }
        public bool? TravellerNoSC { get; set; }
        public string TravellerNo { get; set; }
        public string TravellerNoActual { get; set; }
        public bool? CouterUnitSC { get; set; }
        public decimal? CouterUnit { get; set; }
        public decimal? CouterUnitErr { get; set; }
        public decimal? CouterUnitActual { get; set; }
        public bool? CouterUnit2SC { get; set; }
        public decimal? CouterUnit2 { get; set; }
        public decimal? CouterUnit2Err { get; set; }
        public decimal? CouterUnit2Actual { get; set; }
        public bool? CouterWeightSC { get; set; }
        public decimal? CouterWeight { get; set; }
        public decimal? CouterWeightErr { get; set; }
        public decimal? CouterWeightActual { get; set; }
        public bool? CouterWeight2SC { get; set; }
        public decimal? CouterWeight2 { get; set; }
        public decimal? CouterWeight2Actual { get; set; }
        public decimal? CouterWeight2Err { get; set; }
        public bool? CounterSystemSC { get; set; }
        public string CounterSystem { get; set; }
        public bool? CounterSystemActual { get; set; }
        public bool? SenserYarnBreakSC { get; set; }
        public string SenserYarnBreak { get; set; }
        public bool? SenserYarnBreakActual { get; set; }
        public bool? CalculatedTwistingNumberSC { get; set; }
        public decimal? CalculatedTwistingNumber { get; set; }
        public decimal? CalculatedTwistingNumberErr { get; set; }
        public decimal? CalculatedTwistingNumberActual { get; set; }
        public bool? NumberOfSpindleRotationSC { get; set; }
        public decimal? NumberOfSpindleRotation { get; set; }
        public decimal? NumberOfSpindleRotationErr { get; set; }
        public decimal? NumberOfSpindleRotationActual { get; set; }
        public bool? YarnSpeedSC { get; set; }
        public decimal? YarnSpeed { get; set; }
        public decimal? YarnSpeedErr { get; set; }
        public decimal? YarnSpeedActual { get; set; }
        public bool? SpecialMentionSC { get; set; }
        public string SpecialMention { get; set; }
        public string SpecialMentionActual { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<S1Condition>> Gets()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<S1Condition>> rets = new NDbResult<List<S1Condition>>();

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

            try
            {
                var items = cnn.Query<S1Condition>("GetS1Condition", p,
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
                rets.data = new List<S1Condition>();
            }

            return rets;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The S1Condition item to save.</param>
        /// <returns></returns>
        public static NDbResult<S1Condition> Save(S1Condition value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S1Condition> ret = new NDbResult<S1Condition>();

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
            p.Add("@RingDiameterActual", value.RingDiameterActual);
            p.Add("@TwistChangeGearSC", value.TwistChangeGearSC);
            p.Add("@TwistChangeGearI", value.TwistChangeGearI);
            p.Add("@TwistChangeGearIActual", value.TwistChangeGearIActual);
            p.Add("@TwistChangeGearJ", value.TwistChangeGearJ);
            p.Add("@TwistChangeGearJActual", value.TwistChangeGearJActual);
            p.Add("@TwistChangeGearK", value.TwistChangeGearK);
            p.Add("@TwistChangeGearKActual", value.TwistChangeGearKActual);
            p.Add("@LifterChangeGearSC", value.LifterChangeGearSC);
            p.Add("@LifterChangeGearA", value.LifterChangeGearA);
            p.Add("@LifterChangeGearAActual", value.LifterChangeGearAActual);
            p.Add("@LifterChangeGearB", value.LifterChangeGearB);
            p.Add("@LifterChangeGearBActual", value.LifterChangeGearBActual);
            p.Add("@OuterDiameterSC", value.OuterDiameterSC);
            p.Add("@OuterDiameter", value.OuterDiameter);
            p.Add("@OuterDiameterActual", value.OuterDiameterActual);
            p.Add("@TravellerNoSC", value.TravellerNoSC);
            p.Add("@TravellerNo", value.TravellerNo);
            p.Add("@TravellerNoActual", value.TravellerNoActual);
            p.Add("@CouterUnitSC", value.CouterUnitSC);
            p.Add("@CouterUnit", value.CouterUnit);
            p.Add("@CouterUnitErr", value.CouterUnitErr);
            p.Add("@CouterUnitActual", value.CouterUnitActual);
            p.Add("@CouterUnit2SC", value.CouterUnit2SC);
            p.Add("@CouterUnit2", value.CouterUnit2);
            p.Add("@CouterUnit2Err", value.CouterUnit2Err);
            p.Add("@CouterUnit2Actual", value.CouterUnit2Actual);
            p.Add("@CouterWeightSC", value.CouterWeightSC);
            p.Add("@CouterWeight", value.CouterWeight);
            p.Add("@CouterWeightErr", value.CouterWeightErr);
            p.Add("@CouterWeightActual", value.CouterWeightActual);
            p.Add("@CouterWeight2SC", value.CouterWeight2SC);
            p.Add("@CouterWeight2", value.CouterWeight2);
            p.Add("@CouterWeight2Actual", value.CouterWeight2Actual);
            p.Add("@CouterWeight2Err", value.CouterWeight2Err);
            p.Add("@CounterSystemSC", value.CounterSystemSC);
            p.Add("@CounterSystem", value.CounterSystem);
            p.Add("@CounterSystemActual", value.CounterSystemActual);
            p.Add("@SenserYarnBreakSC", value.SenserYarnBreakSC);
            p.Add("@SenserYarnBreak", value.SenserYarnBreak);
            p.Add("@SenserYarnBreakActual", value.SenserYarnBreakActual);
            p.Add("@CalculatedTwistingNumberSC", value.CalculatedTwistingNumberSC);
            p.Add("@CalculatedTwistingNumber", value.CalculatedTwistingNumber);
            p.Add("@CalculatedTwistingNumberErr", value.CalculatedTwistingNumberErr);
            p.Add("@CalculatedTwistingNumberActual", value.CalculatedTwistingNumberActual);
            p.Add("@NumberOfSpindleRotationSC", value.NumberOfSpindleRotationSC);
            p.Add("@NumberOfSpindleRotation", value.NumberOfSpindleRotation);
            p.Add("@NumberOfSpindleRotationErr", value.NumberOfSpindleRotationErr);
            p.Add("@NumberOfSpindleRotationActual", value.NumberOfSpindleRotationActual);
            p.Add("@YarnSpeedSC", value.YarnSpeedSC);
            p.Add("@YarnSpeed", value.YarnSpeed);
            p.Add("@YarnSpeedErr", value.YarnSpeedErr);
            p.Add("@YarnSpeedActual", value.YarnSpeedActual);
            p.Add("@SpecialMentionSC", value.SpecialMentionSC);
            p.Add("@SpecialMention", value.SpecialMention);
            p.Add("@SpecialMentionActual", value.SpecialMentionActual);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveS1Condition", p, commandType: CommandType.StoredProcedure);
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

        public static NDbResult Delete(S1Condition value)
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
                cnn.Execute("DELETE FROM S1Condition WHERE ProductCode = @ProductCode", p, commandType: CommandType.Text);
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
