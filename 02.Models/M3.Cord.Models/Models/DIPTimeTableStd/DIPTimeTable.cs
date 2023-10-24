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
	public class DIPTimeTable
	{
		#region Public Proeprties

		public string ProductCode { get; set; }
		public string PeriodTime { get; set; }
		public bool? S7Bobbin { get; set; }
		public decimal? S8CoolingWaterSystemBath1 { get; set; }
		public decimal? S8CoolingWaterSystemBath2 { get; set; }
		public string S8ChemicalWork { get; set; }
		public string S8ChemicalFilter { get; set; }
		public decimal? S8Speed { get; set; }
		public decimal? S8StretchD { get; set; }
		public decimal? S8StretchH { get; set; }
		public decimal? S8StretchN { get; set; }
		public decimal? S8TempD { get; set; }
		public decimal? S8TempHN { get; set; }
		public bool? S9GlideStatus { get; set; }
		public string Remark { get; set; }

		#endregion

		#region Static Methods

		public static NDbResult<DIPTimeTable> GetCurrent()
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<DIPTimeTable> ret = new NDbResult<DIPTimeTable>();

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
				var item = cnn.Query<DIPTimeTable>("GetDIPTimeTables", p,
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
		/// <param name="value">The DIPTimeTable item to save.</param>
		/// <returns></returns>
		public static NDbResult<DIPTimeTable> Save(DIPTimeTable value)
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<DIPTimeTable> ret = new NDbResult<DIPTimeTable>();

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
			p.Add("@PeriodTime", value.PeriodTime);
			p.Add("@S7Bobbin", value.S7Bobbin);
			p.Add("@S8CoolingWaterSystemBath1", value.S8CoolingWaterSystemBath1);
			p.Add("@S8CoolingWaterSystemBath2", value.S8CoolingWaterSystemBath2);
			p.Add("@S8ChemicalWork", value.S8ChemicalWork);
			p.Add("@S8ChemicalFilter", value.S8ChemicalFilter);
			p.Add("@S8Speed", value.S8Speed);
			p.Add("@S8StretchD", value.S8StretchD);
			p.Add("@S8StretchH", value.S8StretchH);
			p.Add("@S8StretchN", value.S8StretchN);
			p.Add("@S8TempD", value.S8TempD);
			p.Add("@S8TempHN", value.S8TempHN);
			p.Add("@S9GlideStatus", value.S9GlideStatus);
			p.Add("@Remark", value.Remark);

			p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
			p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

			try
			{
				cnn.Execute("SaveDIPTimeTable", p, commandType: CommandType.StoredProcedure);
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

		public static NDbResult Delete(DIPTimeTable value)
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
				cnn.Execute("DELETE FROM DIPTimeTable WHERE ProductCode = @ProductCode", p, commandType: CommandType.Text);
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
