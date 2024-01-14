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
	public class S8BeforeConditionStd
	{
		#region Public Proeprties

		public string ProductCode { get; set; }
        public bool? SolutionNameBath1SC { get; set; }
        public string SolutionNameBath1 { get; set; }
        public bool? SolutionNameBath2SC { get; set; }
        public string SolutionNameBath2 { get; set; }
		public bool? TempJacketDrumBath1SC { get; set; }
		public decimal? TempJacketDrumBath1Min { get; set; }
		public decimal? TempJacketDrumBath1Max { get; set; }
		public bool? TempJacketDrumBath2SC { get; set; }
		public decimal? TempJacketDrumBath2Min { get; set; }
		public decimal? TempJacketDrumBath2Max { get; set; }
		public bool? TempChemicalBath1SC { get; set; }
		public decimal? TempChemicalBath1Min { get; set; }
		public decimal? TempChemicalBath1Max { get; set; }
		public bool? TempChemicalBath2SC { get; set; }
		public decimal? TempChemicalBath2Min { get; set; }
		public decimal? TempChemicalBath2Max { get; set; }
		public bool? StretchDSC { get; set; }
		public decimal? StretchD { get; set; }
		public decimal? StretchDErr { get; set; }
		public bool? StretchHSC { get; set; }
		public decimal? StretchH { get; set; }
		public decimal? StretchHErr { get; set; }
		public bool? StretchNSC { get; set; }
		public decimal? StretchN { get; set; }
		public decimal? StretchNErr { get; set; }
		public bool? TempDSC { get; set; }
		public decimal? TempD { get; set; }
		public decimal? TempDErr { get; set; }
		public bool? TempHNSC { get; set; }
		public decimal? TempHN { get; set; }
		public decimal? TempHNErr { get; set; }
		public bool? SpeedSC { get; set; }
		public decimal? Speed { get; set; }
		public decimal? SpeedErr { get; set; }
		public bool? ExhaustFanDryerSC { get; set; }
		public decimal? ExhaustFanDryer { get; set; }
		public bool? ExhaustFanHNSC { get; set; }
		public decimal? ExhaustFanHN { get; set; }
		public bool? CleanBath1SC { get; set; }
		public bool? CleanBath2SC { get; set; }
		public bool? CleanFrontSC { get; set; }
		public bool? CamboxSC { get; set; }

		#endregion

		#region Static Methods

        public static NDbResult<List<S8BeforeConditionStd>> Gets(string productCode)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<S8BeforeConditionStd>> ret = new NDbResult<List<S8BeforeConditionStd>>();

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
            p.Add("@ProductCode", productCode);

            try
            {
                var items = cnn.Query<S8BeforeConditionStd>("GetS8BeforeConditionStd", p,
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
                ret.data = new List<S8BeforeConditionStd>();
            }

            return ret;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The S8BeforeConditionStd item to save.</param>
        /// <returns></returns>
        public static NDbResult<S8BeforeConditionStd> Save(S8BeforeConditionStd value)
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<S8BeforeConditionStd> ret = new NDbResult<S8BeforeConditionStd>();

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
			p.Add("@SolutionNameBath1SC", value.SolutionNameBath1SC);
            p.Add("@SolutionNameBath1", value.SolutionNameBath1);
            p.Add("@SolutionNameBath2SC", value.SolutionNameBath2SC);
            p.Add("@SolutionNameBath2", value.SolutionNameBath2);
			p.Add("@TempJacketDrumBath1SC", value.TempJacketDrumBath1SC);
			p.Add("@TempJacketDrumBath1Min", value.TempJacketDrumBath1Min);
			p.Add("@TempJacketDrumBath1Max", value.TempJacketDrumBath1Max);
			p.Add("@TempJacketDrumBath2SC", value.TempJacketDrumBath2SC);
			p.Add("@TempJacketDrumBath2Min", value.TempJacketDrumBath2Min);
			p.Add("@TempJacketDrumBath2Max", value.TempJacketDrumBath2Max);
			p.Add("@TempChemicalBath1SC", value.TempChemicalBath1SC);
			p.Add("@TempChemicalBath1Min", value.TempChemicalBath1Min);
			p.Add("@TempChemicalBath1Max", value.TempChemicalBath1Max);
			p.Add("@TempChemicalBath2SC", value.TempChemicalBath2SC);
			p.Add("@TempChemicalBath2Min", value.TempChemicalBath2Min);
			p.Add("@TempChemicalBath2Max", value.TempChemicalBath2Max);
			p.Add("@StretchDSC", value.StretchDSC);
			p.Add("@StretchD", value.StretchD);
			p.Add("@StretchDErr", value.StretchDErr);
			p.Add("@StretchHSC", value.StretchHSC);
			p.Add("@StretchH", value.StretchH);
			p.Add("@StretchHErr", value.StretchHErr);
			p.Add("@StretchNSC", value.StretchNSC);
			p.Add("@StretchN", value.StretchN);
			p.Add("@StretchNErr", value.StretchNErr);
			p.Add("@TempDSC", value.TempDSC);
			p.Add("@TempD", value.TempD);
			p.Add("@TempDErr", value.TempDErr);
			p.Add("@TempHNSC", value.TempHNSC);
			p.Add("@TempHN", value.TempHN);
			p.Add("@TempHNErr", value.TempHNErr);
			p.Add("@SpeedSC", value.SpeedSC);
			p.Add("@Speed", value.Speed);
			p.Add("@SpeedErr", value.SpeedErr);
			p.Add("@ExhaustFanDryerSC", value.ExhaustFanDryerSC);
			p.Add("@ExhaustFanDryer", value.ExhaustFanDryer);
			p.Add("@ExhaustFanHNSC", value.ExhaustFanHNSC);
			p.Add("@ExhaustFanHN", value.ExhaustFanHN);
			p.Add("@CleanBath1SC", value.CleanBath1SC);
			p.Add("@CleanBath2SC", value.CleanBath2SC);
			p.Add("@CleanFrontSC", value.CleanFrontSC);
			p.Add("@CamboxSC", value.CamboxSC);

			p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
			p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

			try
			{
				cnn.Execute("SaveS8BeforeConditionStd", p, commandType: CommandType.StoredProcedure);
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

		public static NDbResult Delete(S8BeforeConditionStd value)
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
				cnn.Execute("DELETE FROM S8BeforeConditionStd WHERE ProductCode = @ProductCode", p, commandType: CommandType.Text);
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
