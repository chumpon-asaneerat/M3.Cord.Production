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
using System.Windows.Controls.Primitives;

#endregion

namespace M3.Cord.Models
{
    public class S8x2WetPickUp
    {
        #region Public Proeprties

        public string ProductCode { get; set; }
        public string LotNo { get; set; }
        public DateTime? DoffingDate { get; set; }
        public string CustomerName { get; set; }
        public string FirstDip1 { get; set; }
        public string SolutionName { get; set; }
        public string FirstDip2 { get; set; }
        public string SolutionLotNo { get; set; }
        public decimal? PullMotorActSpeed1 { get; set; }
        public decimal? PullMotorActSpeed2 { get; set; }
        public decimal? PullMotorActSpeed3 { get; set; }
        public decimal? PullMotorActSpeed4 { get; set; }
        public decimal? PullMotorActSpeed5 { get; set; }
        public decimal? PullMotorActSpeed6 { get; set; }
        public decimal? PullMotorSpeedSetPoint1 { get; set; }
        public decimal? PullMotorSpeedSetPoint2 { get; set; }
        public decimal? PullMotorSpeedSetPoint3 { get; set; }
        public decimal? PullMotorSpeedSetPoint4 { get; set; }
        public decimal? PullMotorSpeedSetPoint5 { get; set; }
        public decimal? PullMotorSpeedSetPoint6 { get; set; }
        public decimal? PullMotorActCurrent1 { get; set; }
        public decimal? PullMotorActCurrent2 { get; set; }
        public decimal? PullMotorActCurrent3 { get; set; }
        public decimal? PullMotorActCurrent4 { get; set; }
        public decimal? PullMotorActCurrent5 { get; set; }
        public decimal? PullMotorActCurrent6 { get; set; }
        public decimal? PullMotorPctCurrent1 { get; set; }
        public decimal? PullMotorPctCurrent2 { get; set; }
        public decimal? PullMotorPctCurrent3 { get; set; }
        public decimal? PullMotorPctCurrent4 { get; set; }
        public decimal? PullMotorPctCurrent5 { get; set; }
        public decimal? PullMotorPctCurrent6 { get; set; }
        public decimal? OvenCirculatingFanActSpeed1 { get; set; }
        public decimal? OvenCirculatingFanActSpeed2 { get; set; }
        public decimal? OvenExhaustFanActSpeed1 { get; set; }
        public decimal? OvenExhaustFanActSpeed2 { get; set; }
        public decimal? OvenExhaustFanFrontActSpeed { get; set; }
        public decimal? OvenExhaustFanBackActSpeed { get; set; }
        public decimal? OvenCirculatingFanSpeedSetpoint1 { get; set; }
        public decimal? OvenCirculatingFanSpeedSetpoint2 { get; set; }
        public decimal? OvenExhaustFanSpeedSetpoint1 { get; set; }
        public decimal? OvenExhaustFanSpeedSetpoint2 { get; set; }
        public decimal? OvenExhaustFanFrontSpeedSetpoint { get; set; }
        public decimal? OvenExhaustFanBackSpeedSetpoint { get; set; }
        public decimal? OvenCirculatingFanActCurrent1 { get; set; }
        public decimal? OvenCirculatingFanActCurrent2 { get; set; }
        public decimal? OvenExhaustFanActCurrent1 { get; set; }
        public decimal? OvenExhaustFanActCurrent2 { get; set; }
        public decimal? OvenExhaustFanFrontActCurrent { get; set; }
        public decimal? OvenExhaustFanBackActCurrent { get; set; }
        public decimal? OvenCirculatingFanPctCurrent1 { get; set; }
        public decimal? OvenCirculatingFanPctCurrent2 { get; set; }
        public decimal? OvenExhaustFanPctCurrent1 { get; set; }
        public decimal? OvenExhaustFanPctCurrent2 { get; set; }
        public decimal? OvenExhaustFanFrontPctCurrent { get; set; }
        public decimal? OvenExhaustFanBackPctCurrent { get; set; }
        public decimal? MotorActStretch1x2 { get; set; }
        public decimal? MotorActStretch3x4 { get; set; }
        public decimal? MotorActStretch4x5 { get; set; }
        public decimal? MotorActStretch1x6 { get; set; }
        public decimal? MotorSetPoint1x2 { get; set; }
        public decimal? MotorSetPoint3x4 { get; set; }
        public decimal? MotorSetPoint4x5 { get; set; }
        public decimal? MotorSetPoint1x6 { get; set; }
        public decimal? MotorActTensionTotal1x2 { get; set; }
        public decimal? MotorActTensionTotal3x4 { get; set; }
        public decimal? MotorActTensionTotal4x5 { get; set; }
        public decimal? MotorActTensionTotal1x6 { get; set; }
        public decimal? MotorSingleTension1x2 { get; set; }
        public decimal? MotorSingleTension3x4 { get; set; }
        public decimal? MotorSingleTension4x5 { get; set; }
        public decimal? MotorSingleTension1x6 { get; set; }
        public string Remark { get; set; }
        public string Opertor { get; set; }
        public string Leader { get; set; }

        #endregion

        #region Static Methods

        public static NDbResult<List<S8x2WetPickUp>> Gets(string ProductCode, string LotNo, DateTime? DoffingDate)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<S8x2WetPickUp>> ret = new NDbResult<List<S8x2WetPickUp>>();

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
            p.Add("@ProductCode", ProductCode);
            p.Add("@LotNo", LotNo);
            p.Add("@DoffingDate", DoffingDate);

            try
            {
                var item = cnn.Query<S8x2WetPickUp>("GetS8x2WetPickUps", p,
                    commandType: CommandType.StoredProcedure).ToList();
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
        /// <param name="value">The S8x2WetPickUp item to save.</param>
        /// <returns></returns>
        public static NDbResult<S8x2WetPickUp> Save(S8x2WetPickUp value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S8x2WetPickUp> ret = new NDbResult<S8x2WetPickUp>();

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
            p.Add("@LotNo", value.LotNo);
            p.Add("@DoffingDate", value.DoffingDate);
            p.Add("@CustomerName", value.CustomerName);
            p.Add("@FirstDip1", value.FirstDip1);
            p.Add("@SolutionName", value.SolutionName);
            p.Add("@FirstDip2", value.FirstDip2);
            p.Add("@SolutionLotNo", value.SolutionLotNo);
            p.Add("@PullMotorActSpeed1", value.PullMotorActSpeed1);
            p.Add("@PullMotorActSpeed2", value.PullMotorActSpeed2);
            p.Add("@PullMotorActSpeed3", value.PullMotorActSpeed3);
            p.Add("@PullMotorActSpeed4", value.PullMotorActSpeed4);
            p.Add("@PullMotorActSpeed5", value.PullMotorActSpeed5);
            p.Add("@PullMotorActSpeed6", value.PullMotorActSpeed6);
            p.Add("@PullMotorSpeedSetPoint1", value.PullMotorSpeedSetPoint1);
            p.Add("@PullMotorSpeedSetPoint2", value.PullMotorSpeedSetPoint2);
            p.Add("@PullMotorSpeedSetPoint3", value.PullMotorSpeedSetPoint3);
            p.Add("@PullMotorSpeedSetPoint4", value.PullMotorSpeedSetPoint4);
            p.Add("@PullMotorSpeedSetPoint5", value.PullMotorSpeedSetPoint5);
            p.Add("@PullMotorSpeedSetPoint6", value.PullMotorSpeedSetPoint6);
            p.Add("@PullMotorActCurrent1", value.PullMotorActCurrent1);
            p.Add("@PullMotorActCurrent2", value.PullMotorActCurrent2);
            p.Add("@PullMotorActCurrent3", value.PullMotorActCurrent3);
            p.Add("@PullMotorActCurrent4", value.PullMotorActCurrent4);
            p.Add("@PullMotorActCurrent5", value.PullMotorActCurrent5);
            p.Add("@PullMotorActCurrent6", value.PullMotorActCurrent6);
            p.Add("@PullMotorPctCurrent1", value.PullMotorPctCurrent1);
            p.Add("@PullMotorPctCurrent2", value.PullMotorPctCurrent2);
            p.Add("@PullMotorPctCurrent3", value.PullMotorPctCurrent3);
            p.Add("@PullMotorPctCurrent4", value.PullMotorPctCurrent4);
            p.Add("@PullMotorPctCurrent5", value.PullMotorPctCurrent5);
            p.Add("@PullMotorPctCurrent6", value.PullMotorPctCurrent6);
            p.Add("@OvenCirculatingFanActSpeed1", value.OvenCirculatingFanActSpeed1);
            p.Add("@OvenCirculatingFanActSpeed2", value.OvenCirculatingFanActSpeed2);
            p.Add("@OvenExhaustFanActSpeed1", value.OvenExhaustFanActSpeed1);
            p.Add("@OvenExhaustFanActSpeed2", value.OvenExhaustFanActSpeed2);
            p.Add("@OvenExhaustFanFrontActSpeed", value.OvenExhaustFanFrontActSpeed);
            p.Add("@OvenExhaustFanBackActSpeed", value.OvenExhaustFanBackActSpeed);
            p.Add("@OvenCirculatingFanSpeedSetpoint1", value.OvenCirculatingFanSpeedSetpoint1);
            p.Add("@OvenCirculatingFanSpeedSetpoint2", value.OvenCirculatingFanSpeedSetpoint2);
            p.Add("@OvenExhaustFanSpeedSetpoint1", value.OvenExhaustFanSpeedSetpoint1);
            p.Add("@OvenExhaustFanSpeedSetpoint2", value.OvenExhaustFanSpeedSetpoint2);
            p.Add("@OvenExhaustFanFrontSpeedSetpoint", value.OvenExhaustFanFrontSpeedSetpoint);
            p.Add("@OvenExhaustFanBackSpeedSetpoint", value.OvenExhaustFanBackSpeedSetpoint);
            p.Add("@OvenCirculatingFanActCurrent1", value.OvenCirculatingFanActCurrent1);
            p.Add("@OvenCirculatingFanActCurrent2", value.OvenCirculatingFanActCurrent2);
            p.Add("@OvenExhaustFanActCurrent1", value.OvenExhaustFanActCurrent1);
            p.Add("@OvenExhaustFanActCurrent2", value.OvenExhaustFanActCurrent2);
            p.Add("@OvenExhaustFanFrontActCurrent", value.OvenExhaustFanFrontActCurrent);
            p.Add("@OvenExhaustFanBackActCurrent", value.OvenExhaustFanBackActCurrent);
            p.Add("@OvenCirculatingFanPctCurrent1", value.OvenCirculatingFanPctCurrent1);
            p.Add("@OvenCirculatingFanPctCurrent2", value.OvenCirculatingFanPctCurrent2);
            p.Add("@OvenExhaustFanPctCurrent1", value.OvenExhaustFanPctCurrent1);
            p.Add("@OvenExhaustFanPctCurrent2", value.OvenExhaustFanPctCurrent2);
            p.Add("@OvenExhaustFanFrontPctCurrent", value.OvenExhaustFanFrontPctCurrent);
            p.Add("@OvenExhaustFanBackPctCurrent", value.OvenExhaustFanBackPctCurrent);
            p.Add("@MotorActStretch1x2", value.MotorActStretch1x2);
            p.Add("@MotorActStretch3x4", value.MotorActStretch3x4);
            p.Add("@MotorActStretch4x5", value.MotorActStretch4x5);
            p.Add("@MotorActStretch1x6", value.MotorActStretch1x6);
            p.Add("@MotorSetPoint1x2", value.MotorSetPoint1x2);
            p.Add("@MotorSetPoint3x4", value.MotorSetPoint3x4);
            p.Add("@MotorSetPoint4x5", value.MotorSetPoint4x5);
            p.Add("@MotorSetPoint1x6", value.MotorSetPoint1x6);
            p.Add("@MotorActTensionTotal1x2", value.MotorActTensionTotal1x2);
            p.Add("@MotorActTensionTotal3x4", value.MotorActTensionTotal3x4);
            p.Add("@MotorActTensionTotal4x5", value.MotorActTensionTotal4x5);
            p.Add("@MotorActTensionTotal1x6", value.MotorActTensionTotal1x6);
            p.Add("@MotorSingleTension1x2", value.MotorSingleTension1x2);
            p.Add("@MotorSingleTension3x4", value.MotorSingleTension3x4);
            p.Add("@MotorSingleTension4x5", value.MotorSingleTension4x5);
            p.Add("@MotorSingleTension1x6", value.MotorSingleTension1x6);
            p.Add("@Remark", value.Remark);
            p.Add("@Opertor", value.Opertor);
            p.Add("@Leader", value.Leader);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveS8x2WetPickup", p, commandType: CommandType.StoredProcedure);
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

        #endregion
    }
}
