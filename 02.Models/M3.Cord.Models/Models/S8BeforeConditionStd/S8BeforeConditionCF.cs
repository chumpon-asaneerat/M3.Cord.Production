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
	public class S8BeforeConditionCF
	{
		#region Public Proeprties

		public int? S8ConditionId { get; set; }
		public string ProductCode { get; set; }
		public string SolutionNameBath1 { get; set; }
		public string SolutionNameBath2 { get; set; }
		public bool? TempJacketDrumBath1SC { get; set; }
		public decimal? TempJacketDrumBath1 { get; set; }
		public bool? TempJacketDrumBath2SC { get; set; }
		public decimal? TempJacketDrumBath2 { get; set; }
		public bool? TempChemicalBath1SC { get; set; }
		public decimal? TempChemicalBath1 { get; set; }
		public bool? TempChemicalBath2SC { get; set; }
		public decimal? TempChemicalBath2 { get; set; }
		public bool? StretchDSC { get; set; }
		public decimal? StretchD { get; set; }
		public bool? StretchHSC { get; set; }
		public decimal? StretchH { get; set; }
		public bool? StretchNSC { get; set; }
		public decimal? StretchN { get; set; }
		public bool? TempDSC { get; set; }
		public decimal? TempD { get; set; }
		public bool? TempHNSC { get; set; }
		public decimal? TempHN { get; set; }
		public bool? SpeedSC { get; set; }
		public decimal? Speed { get; set; }
		public bool? ExhaustFanDryerSC { get; set; }
		public decimal? ExhaustFanDryer { get; set; }
		public bool? ExhaustFanHNSC { get; set; }
		public decimal? ExhaustFanHN { get; set; }
		public bool? CleanBath1 { get; set; }
		public bool? CleanBath2 { get; set; }
		public bool? CleanFront { get; set; }
		public bool? Cambox { get; set; }

		#endregion

		#region Static Methods

		public static NDbResult<S8BeforeConditionCF> GetCurrent()
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<S8BeforeConditionCF> ret = new NDbResult<S8BeforeConditionCF>();

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

			try
			{
				var item = cnn.Query<S8BeforeConditionCF>("GetS8BeforeConditionCF", p,
					commandType: CommandType.StoredProcedure).FirstOrDefault();
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
		/// <param name="value">The S8BeforeConditionCF item to save.</param>
		/// <returns></returns>
		public static NDbResult<S8BeforeConditionCF> Save(S8BeforeConditionCF value)
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<S8BeforeConditionCF> ret = new NDbResult<S8BeforeConditionCF>();

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

			p.Add("@S8ConditionId", value.S8ConditionId);
			p.Add("@ProductCode", value.ProductCode);
			p.Add("@SolutionNameBath1", value.SolutionNameBath1);
			p.Add("@SolutionNameBath2", value.SolutionNameBath2);
			p.Add("@TempJacketDrumBath1SC", value.TempJacketDrumBath1SC);
			p.Add("@TempJacketDrumBath1", value.TempJacketDrumBath1);
			p.Add("@TempJacketDrumBath2SC", value.TempJacketDrumBath2SC);
			p.Add("@TempJacketDrumBath2", value.TempJacketDrumBath2);
			p.Add("@TempChemicalBath1SC", value.TempChemicalBath1SC);
			p.Add("@TempChemicalBath1", value.TempChemicalBath1);
			p.Add("@TempChemicalBath2SC", value.TempChemicalBath2SC);
			p.Add("@TempChemicalBath2", value.TempChemicalBath2);
			p.Add("@StretchDSC", value.StretchDSC);
			p.Add("@StretchD", value.StretchD);
			p.Add("@StretchHSC", value.StretchHSC);
			p.Add("@StretchH", value.StretchH);
			p.Add("@StretchNSC", value.StretchNSC);
			p.Add("@StretchN", value.StretchN);
			p.Add("@TempDSC", value.TempDSC);
			p.Add("@TempD", value.TempD);
			p.Add("@TempHNSC", value.TempHNSC);
			p.Add("@TempHN", value.TempHN);
			p.Add("@SpeedSC", value.SpeedSC);
			p.Add("@Speed", value.Speed);
			p.Add("@ExhaustFanDryerSC", value.ExhaustFanDryerSC);
			p.Add("@ExhaustFanDryer", value.ExhaustFanDryer);
			p.Add("@ExhaustFanHNSC", value.ExhaustFanHNSC);
			p.Add("@ExhaustFanHN", value.ExhaustFanHN);
			p.Add("@CleanBath1", value.CleanBath1);
			p.Add("@CleanBath2", value.CleanBath2);
			p.Add("@CleanFront", value.CleanFront);
			p.Add("@Cambox", value.Cambox);

			p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
			p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

			try
			{
				cnn.Execute("SaveS8BeforeConditionCF", p, commandType: CommandType.StoredProcedure);
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

		public static NDbResult Delete(S8BeforeConditionCF value)
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
				cnn.Execute("DELETE FROM S8BeforeConditionCF WHERE ProductCode = @ProductCode", p, commandType: CommandType.Text);
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
