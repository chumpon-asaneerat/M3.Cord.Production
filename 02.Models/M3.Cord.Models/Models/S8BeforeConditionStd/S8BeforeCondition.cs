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
	public class S8BeforeCondition
	{
		#region Public Proeprties

		public int? S8BeforeId { get; set; }
        public int? DIPPCId { get; set; }

        public string ProductCode { get; set; }

        public int RowType { get; set; }
        public string LotNo { get; set; }

        public bool? SolutionNameBath1SC { get; set; }
        public string SolutionNameBath1 { get; set; }
        public string SolutionNameBath1Value { get; set; }

        public bool? SolutionNameBath2SC { get; set; }
        public string SolutionNameBath2 { get; set; }
        public string SolutionNameBath2Value { get; set; }

        public bool? TempJacketDrumBath1SC { get; set; }
        public decimal? TempJacketDrumBath1Min { get; set; }
        public decimal? TempJacketDrumBath1Max { get; set; }
        public decimal? TempJacketDrumBath1 { get; set; }

        public bool? TempJacketDrumBath2SC { get; set; }
        public decimal? TempJacketDrumBath2Min { get; set; }
        public decimal? TempJacketDrumBath2Max { get; set; }
        public decimal? TempJacketDrumBath2 { get; set; }


        public bool? TempChemicalBath1SC { get; set; }
        public decimal? TempChemicalBath1Min { get; set; }
        public decimal? TempChemicalBath1Max { get; set; }
        public decimal? TempChemicalBath1 { get; set; }

        public bool? TempChemicalBath2SC { get; set; }
        public decimal? TempChemicalBath2Min { get; set; }
        public decimal? TempChemicalBath2Max { get; set; }
        public decimal? TempChemicalBath2 { get; set; }

        public bool? StretchDSC { get; set; }
        public decimal? StretchD { get; set; }
        public decimal? StretchDErr { get; set; }
        public decimal? StretchDValue { get; set; }

        public bool? StretchHSC { get; set; }
        public decimal? StretchH { get; set; }
        public decimal? StretchHErr { get; set; }
        public decimal? StretchHValue { get; set; }

        public bool? StretchNSC { get; set; }
        public decimal? StretchN { get; set; }
        public decimal? StretchNErr { get; set; }
        public decimal? StretchNValue { get; set; }

        public bool? TempDSC { get; set; }
        public decimal? TempD { get; set; }
        public decimal? TempDErr { get; set; }
        public decimal? TempDValue { get; set; }

        public bool? TempHNSC { get; set; }
        public decimal? TempHN { get; set; }
        public decimal? TempHNErr { get; set; }
        public decimal? TempHNValue { get; set; }

        public bool? SpeedSC { get; set; }
        public decimal? Speed { get; set; }
        public decimal? SpeedErr { get; set; }
        public decimal? SpeedValue { get; set; }

        public bool? ExhaustFanDryerSC { get; set; }
        public decimal? ExhaustFanDryer { get; set; }
        public decimal? ExhaustFanDryerValue { get; set; }

        public bool? ExhaustFanHNSC { get; set; }
        public decimal? ExhaustFanHN { get; set; }
        public decimal? ExhaustFanHNValue { get; set; }

        public bool? CleanBath1SC { get; set; }
        public bool? CleanBath1 { get; set; }
        public bool? CleanBath2SC { get; set; }
        public bool? CleanBath2 { get; set; }
        public bool? CleanFrontSC { get; set; }
        public bool? CleanFront { get; set; }
        public bool? CamboxSC { get; set; }
        public bool? Cambox { get; set; }

		public string CheckBy { get; set; }
        public DateTime? CheckDate { get; set; }
        public string VerifyBy { get; set; }
        public DateTime? VerifyDate { get; set; }

        #endregion

        #region Static Methods

        public static S8BeforeCondition Create(string productCode)
        {
            var inst = new S8BeforeCondition();
            var std = S8BeforeConditionStd.Gets(productCode).Value().FirstOrDefault();
            Assign(std, inst);
            return inst;
        }

        public static void Assign(S8BeforeConditionStd src, S8BeforeCondition dst)
        {
            if (null != src && null != dst)
            {
                dst.ProductCode = src.ProductCode;
                dst.SolutionNameBath1SC = src.SolutionNameBath1SC;
                dst.SolutionNameBath1 = src.SolutionNameBath1;
                dst.SolutionNameBath2SC = src.SolutionNameBath2SC;
                dst.SolutionNameBath2 = src.SolutionNameBath2;
                dst.TempJacketDrumBath1SC = src.TempJacketDrumBath1SC;
                dst.TempJacketDrumBath1Min = src.TempJacketDrumBath1Min;
                dst.TempJacketDrumBath1Max = src.TempJacketDrumBath1Max;
                dst.TempJacketDrumBath2SC = src.TempJacketDrumBath2SC;
                dst.TempJacketDrumBath2Min = src.TempJacketDrumBath2Min;
                dst.TempJacketDrumBath2Max = src.TempJacketDrumBath2Max;
                dst.TempChemicalBath1SC = src.TempChemicalBath1SC;
                dst.TempChemicalBath1Min = src.TempChemicalBath1Min;
                dst.TempChemicalBath1Max = src.TempChemicalBath1Max;
                dst.TempChemicalBath2SC = src.TempChemicalBath2SC;
                dst.TempChemicalBath2Min = src.TempChemicalBath2Min;
                dst.TempChemicalBath2Max = src.TempChemicalBath2Max;
                dst.StretchDSC = src.StretchDSC;
                dst.StretchD = src.StretchD;
                dst.StretchDErr = src.StretchDErr;
                dst.StretchHSC = src.StretchHSC;
                dst.StretchH = src.StretchH;
                dst.StretchHErr = src.StretchHErr;
                dst.StretchNSC = src.StretchNSC;
                dst.StretchN = src.StretchN;
                dst.StretchNErr = src.StretchNErr;
                dst.TempDSC = src.TempDSC;
                dst.TempD = src.TempD;
                dst.TempDErr = src.TempDErr;
                dst.TempHNSC = src.TempHNSC;
                dst.TempHN = src.TempHN;
                dst.TempHNErr = src.TempHNErr;
                dst.SpeedSC = src.SpeedSC;
                dst.Speed = src.Speed;
                dst.SpeedErr = src.SpeedErr;
                dst.ExhaustFanDryerSC = src.ExhaustFanDryerSC;
                dst.ExhaustFanDryer = src.ExhaustFanDryer;
                dst.ExhaustFanHNSC = src.ExhaustFanHNSC;
                dst.ExhaustFanHN = src.ExhaustFanHN;
                dst.CleanBath1SC = src.CleanBath1SC;
                dst.CleanBath2SC = src.CleanBath2SC;
                dst.CleanFrontSC = src.CleanFrontSC;
                dst.CamboxSC = src.CamboxSC;
            }
        }

        public static NDbResult<List<S8BeforeCondition>> Gets(int? DIPPCId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<S8BeforeCondition>> ret = new NDbResult<List<S8BeforeCondition>>();

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
            p.Add("@DIPPCId", DIPPCId);

            try
            {
                var items = cnn.Query<S8BeforeCondition>("GetS8BeforeConditions", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList() : null;
                ret.Success(data);
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
                ret.data = new List<S8BeforeCondition>();
            }

            return ret;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The S8BeforeConditionItem item to save.</param>
        /// <returns></returns>
        public static NDbResult<S8BeforeCondition> Save(S8BeforeCondition value)
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<S8BeforeCondition> ret = new NDbResult<S8BeforeCondition>();

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

			p.Add("@DIPPCId", value.DIPPCId);
			
            p.Add("@ProductCode", value.ProductCode);
            p.Add("@RowType", value.RowType);
            p.Add("@LotNo", value.LotNo);

			p.Add("@SolutionNameBath1SC", value.SolutionNameBath1SC);
            p.Add("@SolutionNameBath1", value.SolutionNameBath1);            
			p.Add("@SolutionNameBath1Value", value.SolutionNameBath1Value);

            p.Add("@SolutionNameBath2SC", value.SolutionNameBath2SC);
            p.Add("@SolutionNameBath2", value.SolutionNameBath2);
            p.Add("@SolutionNameBath2Value", value.SolutionNameBath2Value);

            p.Add("@TempJacketDrumBath1SC", value.TempJacketDrumBath1SC);
            p.Add("@TempJacketDrumBath1Min", value.TempJacketDrumBath1Min);
            p.Add("@TempJacketDrumBath1Max", value.TempJacketDrumBath1Max);
            p.Add("@TempJacketDrumBath1", value.TempJacketDrumBath1);

            p.Add("@TempJacketDrumBath2SC", value.TempJacketDrumBath2SC);
            p.Add("@TempJacketDrumBath2Min", value.TempJacketDrumBath2Min);
            p.Add("@TempJacketDrumBath2Max", value.TempJacketDrumBath2Max);
            p.Add("@TempJacketDrumBath2", value.TempJacketDrumBath2);

            p.Add("@TempChemicalBath1SC", value.TempChemicalBath1SC);
            p.Add("@TempChemicalBath1Min", value.TempChemicalBath1Min);
            p.Add("@TempChemicalBath1Max", value.TempChemicalBath1Max);
            p.Add("@TempChemicalBath1", value.TempChemicalBath1);
            
			p.Add("@TempChemicalBath2SC", value.TempChemicalBath2SC);
            p.Add("@TempChemicalBath2Min", value.TempChemicalBath2Min);
            p.Add("@TempChemicalBath2Max", value.TempChemicalBath2Max);
            p.Add("@TempChemicalBath2", value.TempChemicalBath2);

			p.Add("@StretchDSC", value.StretchDSC);
            p.Add("@StretchD", value.StretchD);
            p.Add("@StretchDErr", value.StretchDErr);
            p.Add("@StretchDValue", value.StretchDValue);

            p.Add("@StretchHSC", value.StretchHSC);
            p.Add("@StretchH", value.StretchH);
            p.Add("@StretchHErr", value.StretchHErr);
            p.Add("@StretchHValue", value.StretchHValue);

            p.Add("@StretchNSC", value.StretchNSC);
            p.Add("@StretchN", value.StretchN);
            p.Add("@StretchNErr", value.StretchNErr);
            p.Add("@StretchNValue", value.StretchNValue);

            p.Add("@TempDSC", value.TempDSC);
            p.Add("@TempD", value.TempD);
            p.Add("@TempDErr", value.TempDErr);
            p.Add("@TempDValue", value.TempDValue);

            p.Add("@TempHNSC", value.TempHNSC);
            p.Add("@TempHN", value.TempHN);
            p.Add("@TempHNErr", value.TempHNErr);
            p.Add("@TempHNValue", value.TempHNValue);

            p.Add("@SpeedSC", value.SpeedSC);
            p.Add("@Speed", value.Speed);
            p.Add("@SpeedErr", value.SpeedErr);
            p.Add("@SpeedValue", value.SpeedValue);

            p.Add("@ExhaustFanDryerSC", value.ExhaustFanDryerSC);
            p.Add("@ExhaustFanDryer", value.ExhaustFanDryer);
            p.Add("@ExhaustFanDryerValue", value.ExhaustFanDryerValue);

            p.Add("@ExhaustFanHNSC", value.ExhaustFanHNSC);
            p.Add("@ExhaustFanHN", value.ExhaustFanHN);
            p.Add("@ExhaustFanHNValue", value.ExhaustFanHNValue);

            p.Add("@CleanBath1SC", value.CleanBath1SC);
            p.Add("@CleanBath1", value.CleanBath1);

            p.Add("@CleanBath2SC", value.CleanBath2SC);
            p.Add("@CleanBath2", value.CleanBath2);

            p.Add("@CleanFrontSC", value.CleanFrontSC);
            p.Add("@CleanFront", value.CleanFront);

            p.Add("@CamboxSC", value.CamboxSC);
            p.Add("@Cambox", value.Cambox);

            p.Add("@CheckBy", value.CheckBy);
            p.Add("@CheckDate", value.CheckDate);
            p.Add("@VerifyBy", value.VerifyBy);
            p.Add("@VerifyDate", value.VerifyDate);

            p.Add("@S8BeforeId", value.S8BeforeId, DbType.Int32, direction: ParameterDirection.InputOutput);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
			p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

			try
			{
				cnn.Execute("SaveS8BeforeCondition", p, commandType: CommandType.StoredProcedure);
				ret.Success(value);

                // Set PK
                value.S8BeforeId = p.Get<dynamic>("@S8BeforeId");
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

		public static NDbResult Delete(S8BeforeCondition value)
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
				cnn.Execute("DELETE FROM S8BeforeConditionItem WHERE ProductCode = @ProductCode", p, commandType: CommandType.Text);
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
