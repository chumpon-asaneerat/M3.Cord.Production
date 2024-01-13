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
	public class ProductionRecordStd
	{
		#region Public Proeprties

		public string ProductCode { get; set; }
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
		public bool? TreatSC { get; set; }
		public decimal? Treat { get; set; }
		public bool? DoffingLengthSC { get; set; }
		public decimal? DoffingLength { get; set; }
		public bool? WeightSC { get; set; }
		public decimal? Weight { get; set; }
		public bool? SpindleSC { get; set; }
		public decimal? Spindle { get; set; }
		public bool? ProductionGoodSC { get; set; }
		public decimal? ProductionGood { get; set; }
		public bool? ProductionTotalSC { get; set; }
		public decimal? ProductionTotal { get; set; }


		#endregion

		#region Static Methods

		public static NDbResult<ProductionRecordStd> GetCurrent(string productCode)
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<ProductionRecordStd> ret = new NDbResult<ProductionRecordStd>();

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
				var item = cnn.Query<ProductionRecordStd>("GetProductionRecordStd", p,
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
		public static NDbResult<List<ProductionRecordStd>> Gets(string productCode)
		{
			NDbResult<List<ProductionRecordStd>> rets = new NDbResult<List<ProductionRecordStd>>();

			MethodBase med = MethodBase.GetCurrentMethod();

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
				var items = cnn.Query<ProductionRecordStd>("GetProductionRecordStd", p,
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

			return rets;
		}

		/// <summary>
		/// Save
		/// </summary>
		/// <param name="value">The ProductionRecordStd item to save.</param>
		/// <returns></returns>
		public static NDbResult<ProductionRecordStd> Save(ProductionRecordStd value)
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<ProductionRecordStd> ret = new NDbResult<ProductionRecordStd>();

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
			p.Add("@TreatSC", value.TreatSC);
			p.Add("@Treat", value.Treat);
			p.Add("@DoffingLengthSC", value.DoffingLengthSC);
			p.Add("@DoffingLength", value.DoffingLength);
			p.Add("@WeightSC", value.WeightSC);
			p.Add("@Weight", value.Weight);
			p.Add("@SpindleSC", value.SpindleSC);
			p.Add("@Spindle", value.Spindle);
			p.Add("@ProductionGoodSC", value.ProductionGoodSC);
			p.Add("@ProductionGood", value.ProductionGood);
			p.Add("@ProductionTotalSC", value.ProductionTotalSC);
			p.Add("@ProductionTotal", value.ProductionTotal);

			p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
			p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

			try
			{
				cnn.Execute("SaveProductionRecordStd", p, commandType: CommandType.StoredProcedure);
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

		public static NDbResult Delete(ProductionRecordStd value)
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
				cnn.Execute("DELETE FROM ProductionRecordStd WHERE ProductCode = @ProductCode", p, commandType: CommandType.Text);
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
