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
	public class ProductionRecordDetail
	{
		#region Public Proeprties
		public string ProductCode { get; set; }
		public string LotNo { get; set; }
		public DateTime? DoffingDate { get; set; }
		public int? DoffingNo { get; set; }
		public decimal? StretchD { get; set; }
		public decimal? StretchH { get; set; }
		public decimal? StretchN { get; set; }
		public decimal? TempD { get; set; }
		public decimal? TempHN { get; set; }
		public decimal? Speed { get; set; }
		public decimal? Treat { get; set; }
		public decimal? DoffingLength { get; set; }
		public decimal? Weight { get; set; }
		public decimal? Spindle { get; set; }
		public decimal? ProductionGood { get; set; }
		public decimal? ProductionTotal { get; set; }
		public decimal? ProductionCut { get; set; }
		public decimal? PositionCordCutCreel { get; set; }
		public decimal? PositionCordCutCS { get; set; }
		public decimal? PositionCordCutWinder { get; set; }
		public decimal? PositionCordCutWasteYarn { get; set; }
		public DateTime? CheckTimeStart { get; set; }
		public DateTime? CheckTimeFinish { get; set; }
		public DateTime? CheckTimeRecord { get; set; }
		public string Opertor { get; set; }
		public string Leader { get; set; }

		#endregion

		#region Static Methods
		public static NDbResult<ProductionRecordDetail> GetCurrent(string productCode, string lotNo)
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<ProductionRecordDetail> ret = new NDbResult<ProductionRecordDetail>();

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
			p.Add("@LotNo", lotNo);
			try
			{
				var item = cnn.Query<ProductionRecordDetail>("GetProductionRecordDetail", p,
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

		public static NDbResult<List<ProductionRecordDetail>> Gets(string lotNo, string productCode)
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<List<ProductionRecordDetail>> rets = new NDbResult<List<ProductionRecordDetail>>();

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
			p.Add("@LotNo", lotNo);

			try
			{
				var items = cnn.Query<ProductionRecordDetail>("GetProductionRecordDetail", p,
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
		/// <param name="value">The ProductionRecordDetail item to save.</param>
		/// <returns></returns>
		public static NDbResult<ProductionRecordDetail> Save(ProductionRecordDetail value)
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<ProductionRecordDetail> ret = new NDbResult<ProductionRecordDetail>();

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
			p.Add("@DoffingNo", value.DoffingNo);
			p.Add("@StretchD", value.StretchD);
			p.Add("@StretchH", value.StretchH);
			p.Add("@StretchN", value.StretchN);
			p.Add("@TempD", value.TempD);
			p.Add("@TempHN", value.TempHN);
			p.Add("@Speed", value.Speed);
			p.Add("@Treat", value.Treat);
			p.Add("@DoffingLength", value.DoffingLength);
			p.Add("@Weight", value.Weight);
			p.Add("@Spindle", value.Spindle);
			p.Add("@ProductionGood", value.ProductionGood);
			p.Add("@ProductionTotal", value.ProductionTotal);
			p.Add("@ProductionCut", value.ProductionCut);
			p.Add("@PositionCordCutCreel", value.PositionCordCutCreel);
			p.Add("@PositionCordCutCS", value.PositionCordCutCS);
			p.Add("@PositionCordCutWinder", value.PositionCordCutWinder);
			p.Add("@PositionCordCutWasteYarn", value.PositionCordCutWasteYarn);
			p.Add("@CheckTimeStart", value.CheckTimeStart);
			p.Add("@CheckTimeFinish", value.CheckTimeFinish);
			p.Add("@CheckTimeRecord", value.CheckTimeRecord);
			p.Add("@Opertor", value.Opertor);
			p.Add("@Leader", value.Leader);

			p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
			p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

			try
			{
				cnn.Execute("SaveProductionRecordDetail", p, commandType: CommandType.StoredProcedure);
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

		public static NDbResult Delete(ProductionRecordDetail value)
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
				cnn.Execute("DELETE FROM ProductionRecordDetail WHERE ProductCode = @ProductCode And LotNo = @LotNo", p, commandType: CommandType.Text);
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
