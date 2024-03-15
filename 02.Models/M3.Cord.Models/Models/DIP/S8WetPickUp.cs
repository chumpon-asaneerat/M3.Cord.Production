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
    public class S8WetPickUp
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
        public decimal? SpeedSet { get; set; }
        public decimal? SpeedActual { get; set; }
        public decimal? StretchD { get; set; }
        public decimal? StretchH { get; set; }
        public decimal? StretchN { get; set; }
        public decimal? TempD { get; set; }
        public decimal? TempHN { get; set; }

        // Change Type
        public string AMV1 { get; set; }
        public string AMV2 { get; set; }
        public string AMV3 { get; set; }
        public string AMV4 { get; set; }
        public string AMAmp1 { get; set; }
        public string AMAmp2 { get; set; }
        public string AMAmp3 { get; set; }
        public string AMAmp4 { get; set; }

        public bool? AMBearing1 { get; set; }
        public bool? AMBearing2 { get; set; }
        public bool? AMBearing3 { get; set; }
        public bool? AMBearing4 { get; set; }
        public bool? AMMotorNoise1 { get; set; }
        public bool? AMMotorNoise2 { get; set; }
        public bool? AMMotorNoise3 { get; set; }
        public bool? AMMotorNoise4 { get; set; }
        public bool? AMNoVibration1 { get; set; }
        public bool? AMNoVibration2 { get; set; }
        public bool? AMNoVibration3 { get; set; }
        public bool? AMNoVibration4 { get; set; }
        public bool? BurnerD { get; set; }
        public bool? BurnerHN { get; set; }
        public bool? GasLeakD { get; set; }
        public bool? GasLeakHN { get; set; }
        public decimal? GasPresureD { get; set; }
        public decimal? GasPresureHN { get; set; }
        public decimal? AirPresureD { get; set; }
        public decimal? AirPresureHN { get; set; }
        public bool? ACMotorExhaustFanD { get; set; }
        public bool? ACMotorExhaustFanHN { get; set; }
        public bool? ACMotorSupplyD { get; set; }
        public bool? ACMotorSupplyHN { get; set; }
        public bool? ExhaustFan1 { get; set; }
        public bool? ExhaustFan2 { get; set; }
        public bool? ExhaustFanSupply1 { get; set; }
        public bool? ExhaustFanSupply2 { get; set; }
        public string Remark { get; set; }
        public string Opertor { get; set; }
        public string Leader { get; set; }

        #endregion

        #region Static Methods

        public static NDbResult<List<S8WetPickUp>> Gets(string ProductCode, string LotNo, DateTime? DoffingDate)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<S8WetPickUp>> ret = new NDbResult<List<S8WetPickUp>>();

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
                var item = cnn.Query<S8WetPickUp>("GetS8WetPickUps", p,
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
        /// <param name="value">The S8WetPickUp item to save.</param>
        /// <returns></returns>
        public static NDbResult<S8WetPickUp> Save(S8WetPickUp value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S8WetPickUp> ret = new NDbResult<S8WetPickUp>();

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
            p.Add("@SpeedSet", value.SpeedSet);
            p.Add("@SpeedActual", value.SpeedActual);
            p.Add("@StretchD", value.StretchD);
            p.Add("@StretchH", value.StretchH);
            p.Add("@StretchN", value.StretchN);
            p.Add("@TempD", value.TempD);
            p.Add("@TempHN", value.TempHN);
            p.Add("@AMV1", value.AMV1);
            p.Add("@AMV2", value.AMV2);
            p.Add("@AMV3", value.AMV3);
            p.Add("@AMV4", value.AMV4);
            p.Add("@AMAmp1", value.AMAmp1);
            p.Add("@AMAmp2", value.AMAmp2);
            p.Add("@AMAmp3", value.AMAmp3);
            p.Add("@AMAmp4", value.AMAmp4);
            p.Add("@AMBearing1", value.AMBearing1);
            p.Add("@AMBearing2", value.AMBearing2);
            p.Add("@AMBearing3", value.AMBearing3);
            p.Add("@AMBearing4", value.AMBearing4);
            p.Add("@AMMotorNoise1", value.AMMotorNoise1);
            p.Add("@AMMotorNoise2", value.AMMotorNoise2);
            p.Add("@AMMotorNoise3", value.AMMotorNoise3);
            p.Add("@AMMotorNoise4", value.AMMotorNoise4);
            p.Add("@AMNoVibration1", value.AMNoVibration1);
            p.Add("@AMNoVibration2", value.AMNoVibration2);
            p.Add("@AMNoVibration3", value.AMNoVibration3);
            p.Add("@AMNoVibration4", value.AMNoVibration4);
            p.Add("@BurnerD", value.BurnerD);
            p.Add("@BurnerHN", value.BurnerHN);
            p.Add("@GasLeakD", value.GasLeakD);
            p.Add("@GasLeakHN", value.GasLeakHN);
            p.Add("@GasPresureD", value.GasPresureD);
            p.Add("@GasPresureHN", value.GasPresureHN);
            p.Add("@AirPresureD", value.AirPresureD);
            p.Add("@AirPresureHN", value.AirPresureHN);
            p.Add("@ACMotorExhaustFanD", value.ACMotorExhaustFanD);
            p.Add("@ACMotorExhaustFanHN", value.ACMotorExhaustFanHN);
            p.Add("@ACMotorSupplyD", value.ACMotorSupplyD);
            p.Add("@ACMotorSupplyHN", value.ACMotorSupplyHN);
            p.Add("@ExhaustFan1", value.ExhaustFan1);
            p.Add("@ExhaustFan2", value.ExhaustFan2);
            p.Add("@ExhaustFanSupply1", value.ExhaustFanSupply1);
            p.Add("@ExhaustFanSupply2", value.ExhaustFanSupply2);
            p.Add("@Remark", value.Remark);
            p.Add("@Opertor", value.Opertor);
            p.Add("@Leader", value.Leader);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveS8WetPickup", p, commandType: CommandType.StoredProcedure);
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
