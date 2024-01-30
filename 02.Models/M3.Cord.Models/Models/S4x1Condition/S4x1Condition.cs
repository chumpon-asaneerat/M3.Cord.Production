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
    public class S4x1Condition
    {
        #region Public Proeprties

        public int? S4x1ConditionId { get; set; }
        public int? PCTwist1Id { get; set; }

        public string ProductCode { get; set; }
        public bool? TwistChangeGearSC { get; set; }
        public decimal? TwistChangeGearE { get; set; }
        public decimal? TwistChangeGearEActual { get; set; }
        public decimal? TwistChangeGearF { get; set; }
        public decimal? TwistChangeGearFActual { get; set; }
        public decimal? TwistChangeGearG { get; set; }
        public decimal? TwistChangeGearGActual { get; set; }
        public decimal? TwistChangeGearH { get; set; }
        public decimal? TwistChangeGearHActual { get; set; }
        public bool? MotorPulleyDiaSC { get; set; }
        public decimal? MotorPulleyDia { get; set; }
        public decimal? MotorPulleyDiaActual { get; set; }
        public bool? DrivingPulleyDiaSC { get; set; }
        public decimal? DrivingPulleyDia { get; set; }
        public decimal? DrivingPulleyDiaActual { get; set; }
        public bool? OverFeedSC { get; set; }
        public decimal? OverFeedRate { get; set; }
        public decimal? OverFeedRateActual { get; set; }
        public decimal? OverFeedGear { get; set; }
        public decimal? OverFeedGearActual { get; set; }
        public bool? WindAngleSC { get; set; }
        public decimal? WindAngleDeg { get; set; }
        public decimal? WindAngleDegActual { get; set; }
        public decimal? WindAngleGearA { get; set; }
        public decimal? WindAngleGearAActual { get; set; }
        public decimal? WindAngleGearB { get; set; }
        public decimal? WindAngleGearBActual { get; set; }

        public bool? TensorDialSetSC { get; set; }
        public decimal? TensorDialSet { get; set; }
        public decimal? TensorDialSetActual { get; set; }

        public bool? NumberOfLoopSC { get; set; }
        public decimal? NumberOfLoop { get; set; }
        public decimal? NumberOfLoopErr { get; set; }
        public decimal? NumberOfLoopActual { get; set; }
        public bool? MethodOfKnotSC { get; set; }
        public string MethodOfKnot { get; set; }
        public string MethodOfKnotActual { get; set; }
        public bool? WindQuantitySC { get; set; }
        public decimal? WindQuantity { get; set; }
        public decimal? WindQuantityErr { get; set; }
        public decimal? WindQuantityActual { get; set; }
        public bool? LengthSC { get; set; }
        public decimal? Length { get; set; }
        public decimal? LengthErr { get; set; }
        public decimal? LengthActual { get; set; }
        public bool? DoffTimeSC { get; set; }
        public decimal? DoffTime { get; set; }
        public decimal? DoffTimeErr { get; set; }
        public decimal? DoffTimeActual { get; set; }
        public bool? ProductQuantitySC { get; set; }
        public decimal? ProductQuantityKg { get; set; }
        public decimal? ProductQuantityKgActual { get; set; }
        public decimal? ProductQuantityDay { get; set; }
        public decimal? ProductQuantityDayActual { get; set; }
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

        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CheckedBy { get; set; }
        public DateTime? CheckedDate { get; set; }
        public string ApproveBy { get; set; }
        public DateTime? ApproveDate { get; set; }

        public string ShiftLeader { get; set; }
        public string ProductionManager { get; set; }

        // below properties from pc card
        public string MCCode { get; set; }
        public string ProductLotNo { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public string ItemYarn { get; set; }
        public string CordStructure { get; set; }
        public string TreatRoute { get; set; }
        public string TwistSpec { get; set; }
        public DateTime? IssueDate { get; set; }
        public string IssueBy { get; set; }

        #endregion

        #region Static Methods

        public static S4x1Condition Create(string productCode)
        {
            var inst = new S4x1Condition();
            var std = S4x1ConditionStd.Gets(productCode).Value().FirstOrDefault();
            Assign(std, inst);
            return inst;
        }

        public static void Assign(S4x1ConditionStd src, S4x1Condition dst)
        {
            if (null != src && null != dst)
            {
                dst.ProductCode = src.ProductCode;
                dst.TwistChangeGearSC = src.TwistChangeGearSC;
                dst.TwistChangeGearE = src.TwistChangeGearE;
                dst.TwistChangeGearF = src.TwistChangeGearF;
                dst.TwistChangeGearG = src.TwistChangeGearG;
                dst.TwistChangeGearH = src.TwistChangeGearH;
                dst.MotorPulleyDiaSC = src.MotorPulleyDiaSC;
                dst.MotorPulleyDia = src.MotorPulleyDia;
                dst.DrivingPulleyDiaSC = src.DrivingPulleyDiaSC;
                dst.DrivingPulleyDia = src.DrivingPulleyDia;
                dst.OverFeedSC = src.OverFeedSC;
                dst.OverFeedRate = src.OverFeedRate;
                dst.OverFeedGear = src.OverFeedGear;
                dst.WindAngleSC = src.WindAngleSC;
                dst.WindAngleDeg = src.WindAngleDeg;
                dst.WindAngleGearA = src.WindAngleGearA;
                dst.WindAngleGearB = src.WindAngleGearB;
                dst.TensorDialSetSC = src.TensorDialSetSC;
                dst.TensorDialSet = src.TensorDialSet;
                dst.NumberOfLoopSC = src.NumberOfLoopSC;
                dst.NumberOfLoop = src.NumberOfLoop;
                dst.NumberOfLoopErr = src.NumberOfLoopErr;
                dst.MethodOfKnotSC = src.MethodOfKnotSC;
                dst.MethodOfKnot = src.MethodOfKnot;
                dst.WindQuantitySC = src.WindQuantitySC;
                dst.WindQuantity = src.WindQuantity;
                dst.WindQuantityErr = src.WindQuantityErr;
                dst.LengthSC = src.LengthSC;
                dst.Length = src.Length;
                dst.LengthErr = src.LengthErr;
                dst.DoffTimeSC = src.DoffTimeSC;
                dst.DoffTime = src.DoffTime;
                dst.DoffTimeErr = src.DoffTimeErr;
                dst.ProductQuantitySC = src.ProductQuantitySC;
                dst.ProductQuantityKg = src.ProductQuantityKg;
                dst.ProductQuantityDay = src.ProductQuantityDay;
                dst.CalculatedTwistingNumberSC = src.CalculatedTwistingNumberSC;
                dst.CalculatedTwistingNumber = src.CalculatedTwistingNumber;
                dst.CalculatedTwistingNumberErr = src.CalculatedTwistingNumberErr;
                dst.NumberOfSpindleRotationSC = src.NumberOfSpindleRotationSC;
                dst.NumberOfSpindleRotation = src.NumberOfSpindleRotation;
                dst.NumberOfSpindleRotationErr = src.NumberOfSpindleRotationErr;
                dst.YarnSpeedSC = src.YarnSpeedSC;
                dst.YarnSpeed = src.YarnSpeed;
                dst.YarnSpeedErr = src.YarnSpeedErr;
                dst.SpecialMentionSC = src.SpecialMentionSC;
                dst.SpecialMention = src.SpecialMention;
            }
        }
        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<S4x1Condition>> Gets(int? PCTwist1Id, int? S4x1ConditionId = new int?())
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<S4x1Condition>> rets = new NDbResult<List<S4x1Condition>>();

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
            p.Add("@PCTwist1Id", PCTwist1Id);
            p.Add("@S4x1ConditionId", S4x1ConditionId);

            try
            {
                var items = cnn.Query<S4x1Condition>("GetS4x1Conditions", p,
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
                rets.data = new List<S4x1Condition>();
            }

            return rets;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The S4x1Condition item to save.</param>
        /// <returns></returns>
        public static NDbResult<S4x1Condition> Save(S4x1Condition value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S4x1Condition> ret = new NDbResult<S4x1Condition>();

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

            p.Add("@PCTwist1Id", value.PCTwist1Id);
            p.Add("@ProductCode", value.ProductCode);
            p.Add("@TwistChangeGearSC", value.TwistChangeGearSC);
            p.Add("@TwistChangeGearE", value.TwistChangeGearE);
            p.Add("@TwistChangeGearEActual", value.TwistChangeGearEActual);
            p.Add("@TwistChangeGearF", value.TwistChangeGearF);
            p.Add("@TwistChangeGearFActual", value.TwistChangeGearFActual);
            p.Add("@TwistChangeGearG", value.TwistChangeGearG);
            p.Add("@TwistChangeGearGActual", value.TwistChangeGearGActual);
            p.Add("@TwistChangeGearH", value.TwistChangeGearH);
            p.Add("@TwistChangeGearHActual", value.TwistChangeGearHActual);
            p.Add("@MotorPulleyDiaSC", value.MotorPulleyDiaSC);
            p.Add("@MotorPulleyDia", value.MotorPulleyDia);
            p.Add("@MotorPulleyDiaActual", value.MotorPulleyDiaActual);
            p.Add("@DrivingPulleyDiaSC", value.DrivingPulleyDiaSC);
            p.Add("@DrivingPulleyDia", value.DrivingPulleyDia);
            p.Add("@DrivingPulleyDiaActual", value.DrivingPulleyDiaActual);
            p.Add("@OverFeedSC", value.OverFeedSC);
            p.Add("@OverFeedRate", value.OverFeedRate);
            p.Add("@OverFeedRateActual", value.OverFeedRateActual);
            p.Add("@OverFeedGear", value.OverFeedGear);
            p.Add("@OverFeedGearActual", value.OverFeedGearActual);
            p.Add("@WindAngleSC", value.WindAngleSC);
            p.Add("@WindAngleDeg", value.WindAngleDeg);
            p.Add("@WindAngleDegActual", value.WindAngleDegActual);
            p.Add("@WindAngleGearA", value.WindAngleGearA);
            p.Add("@WindAngleGearAActual", value.WindAngleGearAActual);
            p.Add("@WindAngleGearB", value.WindAngleGearB);
            p.Add("@WindAngleGearBActual", value.WindAngleGearBActual);

            p.Add("@TensorDialSetSC", value.TensorDialSetSC);
            p.Add("@TensorDialSet", value.TensorDialSet);
            p.Add("@TensorDialSetActual", value.TensorDialSetActual);

            p.Add("@NumberOfLoopSC", value.NumberOfLoopSC);
            p.Add("@NumberOfLoop", value.NumberOfLoop);
            p.Add("@NumberOfLoopErr", value.NumberOfLoopErr);
            p.Add("@NumberOfLoopActual", value.NumberOfLoopActual);
            p.Add("@MethodOfKnotSC", value.MethodOfKnotSC);
            p.Add("@MethodOfKnot", value.MethodOfKnot);
            p.Add("@MethodOfKnotActual", value.MethodOfKnotActual);
            p.Add("@WindQuantitySC", value.WindQuantitySC);
            p.Add("@WindQuantity", value.WindQuantity);
            p.Add("@WindQuantityErr", value.WindQuantityErr);
            p.Add("@WindQuantityActual", value.WindQuantityActual);
            p.Add("@LengthSC", value.LengthSC);
            p.Add("@Length", value.Length);
            p.Add("@LengthErr", value.LengthErr);
            p.Add("@LengthActual", value.LengthActual);
            p.Add("@DoffTimeSC", value.DoffTimeSC);
            p.Add("@DoffTime", value.DoffTime);
            p.Add("@DoffTimeErr", value.DoffTimeErr);
            p.Add("@DoffTimeActual", value.DoffTimeActual);
            p.Add("@ProductQuantitySC", value.ProductQuantitySC);
            p.Add("@ProductQuantityKg", value.ProductQuantityKg);
            p.Add("@ProductQuantityKgActual", value.ProductQuantityKgActual);
            p.Add("@ProductQuantityDay", value.ProductQuantityDay);
            p.Add("@ProductQuantityDayActual", value.ProductQuantityDayActual);
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

            p.Add("@UpdateBy", value.UpdateBy);

            p.Add("@S4x1ConditionId", value.S4x1ConditionId, DbType.Int32, direction: ParameterDirection.InputOutput);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveS4x1Condition", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);

                // Set PK
                value.S4x1ConditionId = p.Get<dynamic>("@S4x1ConditionId");
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

        public static NDbResult Delete(S4x1Condition value)
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
            p.Add("@S4x1ConditionId", value.S4x1ConditionId);

            try
            {
                cnn.Execute("DELETE FROM S4x1Condition WHERE S4x1ConditionId = @S4x1ConditionId", p, commandType: CommandType.Text);
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
