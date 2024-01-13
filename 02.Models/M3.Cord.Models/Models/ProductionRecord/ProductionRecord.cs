﻿#region Using

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
	public class ProductionRecord
	{
		#region Public Proeprties
		public DateTime? RecordDate { get; set; }
		public bool? ProcessHS { get; set; }
		public bool? ProcessDIP { get; set; }
		public string Customer { get; set; }
		public decimal? Counter { get; set; }
		public decimal? CounterErr { get; set; }
		public string CordStructure { get; set; }
		public string ProductCode { get; set; }
		public string LotNo { get; set; }
		public string Bath1SolutionName { get; set; }
		public decimal? Bath1NipFront { get; set; }
		public decimal? Bath1NipBack { get; set; }
		public decimal? Bath1NipBackErr { get; set; }
		public string Bath2SolutionName { get; set; }
		public decimal? Bath2NipFront { get; set; }
		public decimal? Bath2NipBack { get; set; }
		public decimal? Bath2NipBackErr { get; set; }
		public bool? Sofner { get; set; }
		public bool? DarwNip { get; set; }
		public string PaperTubeColorUse { get; set; }
		public decimal? TensionD { get; set; }
		public decimal? TensionH { get; set; }
		public decimal? TensionN { get; set; }
		public string TensionWinder { get; set; }
		public decimal? GasBefore { get; set; }
		public decimal? GasAfter { get; set; }
		public decimal? GasTotal { get; set; }
		public decimal? CoolingWarterBefore { get; set; }
		public decimal? CoolingWarterAfter { get; set; }
		public decimal? CoolingWarterTotal { get; set; }
		public decimal? AirPressureBefore { get; set; }
		public decimal? AirPressureAfter { get; set; }
		public decimal? AirPressureTotal { get; set; }
		public decimal? Bath1Before { get; set; }
		public decimal? Bath1After { get; set; }
		public decimal? Bath1WPU { get; set; }
		public decimal? Bath2Before { get; set; }
		public decimal? Bath2After { get; set; }
		public decimal? Bath2WPU { get; set; }
		public decimal? TempDZone1 { get; set; }
		public decimal? TempDZone2 { get; set; }
		public decimal? TempDZone3 { get; set; }
		public decimal? TempHNZone1 { get; set; }
		public decimal? TempHNZone2 { get; set; }
		public decimal? TempHNZone3 { get; set; }
		public decimal? TempHNZone4 { get; set; }
		public decimal? TempHNZone5 { get; set; }
		public decimal? TempHNZone6 { get; set; }
		public string SectionHead { get; set; }
		public string SectionManager { get; set; }

		#endregion

		#region Static Methods

		public static NDbResult<ProductionRecord> GetCurrent()
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<ProductionRecord> ret = new NDbResult<ProductionRecord>();

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
				var item = cnn.Query<ProductionRecord>("GetProductionRecord", p,
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
		/// <param name="value">The ProductionRecord item to save.</param>
		/// <returns></returns>
		public static NDbResult<ProductionRecord> Save(ProductionRecord value)
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<ProductionRecord> ret = new NDbResult<ProductionRecord>();

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

			p.Add("@RecordDate", value.RecordDate);
			p.Add("@ProcessHS", value.ProcessHS);
			p.Add("@ProcessDIP", value.ProcessDIP);
			p.Add("@Customer", value.Customer);
			p.Add("@Counter", value.Counter);
			p.Add("@CounterErr", value.CounterErr);
			p.Add("@CordStructure", value.CordStructure);
			p.Add("@ProductCode", value.ProductCode);
			p.Add("@LotNo", value.LotNo);
			p.Add("@Bath1SolutionName", value.Bath1SolutionName);
			p.Add("@Bath1NipFront", value.Bath1NipFront);
			p.Add("@Bath1NipBack", value.Bath1NipBack);
			p.Add("@Bath1NipBackErr", value.Bath1NipBackErr);
			p.Add("@Bath2SolutionName", value.Bath2SolutionName);
			p.Add("@Bath2NipFront", value.Bath2NipFront);
			p.Add("@Bath2NipBack", value.Bath2NipBack);
			p.Add("@Bath2NipBackErr", value.Bath2NipBackErr);
			p.Add("@Sofner", value.Sofner);
			p.Add("@DarwNip", value.DarwNip);
			p.Add("@PaperTubeColorUse", value.PaperTubeColorUse);
			p.Add("@TensionD", value.TensionD);
			p.Add("@TensionH", value.TensionH);
			p.Add("@TensionN", value.TensionN);
			p.Add("@TensionWinder", value.TensionWinder);
			p.Add("@GasBefore", value.GasBefore);
			p.Add("@GasAfter", value.GasAfter);
			p.Add("@GasTotal", value.GasTotal);
			p.Add("@CoolingWarterBefore", value.CoolingWarterBefore);
			p.Add("@CoolingWarterAfter", value.CoolingWarterAfter);
			p.Add("@CoolingWarterTotal", value.CoolingWarterTotal);
			p.Add("@AirPressureBefore", value.AirPressureBefore);
			p.Add("@AirPressureAfter", value.AirPressureAfter);
			p.Add("@AirPressureTotal", value.AirPressureTotal);
			p.Add("@Bath1Before", value.Bath1Before);
			p.Add("@Bath1After", value.Bath1After);
			p.Add("@Bath1WPU", value.Bath1WPU);
			p.Add("@Bath2Before", value.Bath2Before);
			p.Add("@Bath2After", value.Bath2After);
			p.Add("@Bath2WPU", value.Bath2WPU);
			p.Add("@TempDZone1", value.TempDZone1);
			p.Add("@TempDZone2", value.TempDZone2);
			p.Add("@TempDZone3", value.TempDZone3);
			p.Add("@TempHNZone1", value.TempHNZone1);
			p.Add("@TempHNZone2", value.TempHNZone2);
			p.Add("@TempHNZone3", value.TempHNZone3);
			p.Add("@TempHNZone4", value.TempHNZone4);
			p.Add("@TempHNZone5", value.TempHNZone5);
			p.Add("@TempHNZone6", value.TempHNZone6);
			p.Add("@SectionHead", value.SectionHead);
			p.Add("@SectionManager", value.SectionManager);

			p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
			p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

			try
			{
				cnn.Execute("SaveProductionRecord", p, commandType: CommandType.StoredProcedure);
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

		public static NDbResult Delete(ProductionRecord value)
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
			p.Add("@LotNo", value.LotNo);

			try
			{
				cnn.Execute("DELETE FROM ProductionRecord WHERE ProductCode = @ProductCode And LotNo = @LotNo", p, commandType: CommandType.Text);
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
