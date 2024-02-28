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
	public class DIPTimeTableStd
	{
		#region Public Proeprties

		public string ProductCode { get; set; }
		public bool? S7BobbinSC { get; set; } = false;
        public bool? S8CoolingWaterSystemBath1SC { get; set; }
		public decimal? S8CoolingWaterSystemBath1Min { get; set; }
		public decimal? S8CoolingWaterSystemBath1Max { get; set; }
		public bool? S8CoolingWaterSystemBath2SC { get; set; } = false;
        public decimal? S8CoolingWaterSystemBath2Min { get; set; }
		public decimal? S8CoolingWaterSystemBath2Max { get; set; }
        public bool? S8ChemicalWorkSC { get; set; } = false;
        public bool? S8ChemicalFilterSC { get; set; } = false;
        public bool? S8SpeedSC { get; set; } = false;
        public decimal? S8Speed { get; set; }
		public decimal? S8SpeedErr { get; set; }
		public bool? S8StretchDSC { get; set; } = false;
        public decimal? S8StretchD { get; set; }
		public decimal? S8StretchDErr { get; set; }
		public bool? S8StretchHSC { get; set; } = false;
        public decimal? S8StretchH { get; set; }
		public decimal? S8StretchHErr { get; set; }
		public bool? S8StretchNSC { get; set; } = false;
        public decimal? S8StretchN { get; set; }
		public decimal? S8StretchNErr { get; set; }
		public bool? S8TempDSC { get; set; } = false;
        public decimal? S8TempD { get; set; }
		public decimal? S8TempDErr { get; set; }
		public bool? S8TempHNSC { get; set; } = false;
        public decimal? S8TempHN { get; set; }
		public decimal? S8TempHNErr { get; set; }
		public bool? S9GlideStatusSC { get; set; } = false;

        #endregion

        #region Static Methods

        public static NDbResult<List<DIPTimeTableStd>> Gets(string productCode)
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<List<DIPTimeTableStd>> ret = new NDbResult<List<DIPTimeTableStd>>();

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
				var items = cnn.Query<DIPTimeTableStd>("GetDIPTimeTableStd", p,
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
                ret.data = new List<DIPTimeTableStd>();
            }

			return ret;
		}

		/// <summary>
		/// Save
		/// </summary>
		/// <param name="value">The DIPTimeTableStd item to save.</param>
		/// <returns></returns>
		public static NDbResult<DIPTimeTableStd> Save(DIPTimeTableStd value)
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<DIPTimeTableStd> ret = new NDbResult<DIPTimeTableStd>();

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
			p.Add("@S7BobbinSC", value.S7BobbinSC);
			p.Add("@S8CoolingWaterSystemBath1SC", value.S8CoolingWaterSystemBath1SC);
			p.Add("@S8CoolingWaterSystemBath1Min", value.S8CoolingWaterSystemBath1Min);
			p.Add("@S8CoolingWaterSystemBath1Max", value.S8CoolingWaterSystemBath1Max);
			p.Add("@S8CoolingWaterSystemBath2SC", value.S8CoolingWaterSystemBath2SC);
			p.Add("@S8CoolingWaterSystemBath2Min", value.S8CoolingWaterSystemBath2Min);
			p.Add("@S8CoolingWaterSystemBath2Max", value.S8CoolingWaterSystemBath2Max);
			p.Add("@S8ChemicalWorkSC", value.S8ChemicalWorkSC);
			p.Add("@S8ChemicalFilterSC", value.S8ChemicalWorkSC);
			p.Add("@S8SpeedSC", value.S8SpeedSC);
			p.Add("@S8Speed", value.S8Speed);
			p.Add("@S8SpeedErr", value.S8SpeedErr);
			p.Add("@S8StretchDSC", value.S8StretchDSC);
			p.Add("@S8StretchD", value.S8StretchD);
			p.Add("@S8StretchDErr", value.S8StretchDErr);
			p.Add("@S8StretchHSC", value.S8StretchHSC);
			p.Add("@S8StretchH", value.S8StretchH);
			p.Add("@S8StretchHErr", value.S8StretchHErr);
			p.Add("@S8StretchNSC", value.S8StretchNSC);
			p.Add("@S8StretchN", value.S8StretchN);
			p.Add("@S8StretchNErr", value.S8StretchNErr);
			p.Add("@S8TempDSC", value.S8TempDSC);
			p.Add("@S8TempD", value.S8TempD);
			p.Add("@S8TempDErr", value.S8TempDErr);
			p.Add("@S8TempHNSC", value.S8TempHNSC);
			p.Add("@S8TempHN", value.S8TempHN);
			p.Add("@S8TempHNErr", value.S8TempHNErr);
			p.Add("@S9GlideStatusSC", value.S9GlideStatusSC);

			p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
			p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

			try
			{
				cnn.Execute("SaveDIPTimeTableStd", p, commandType: CommandType.StoredProcedure);
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

		public static NDbResult Delete(DIPTimeTableStd value)
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
				cnn.Execute("DELETE FROM DIPTimeTableStd WHERE ProductCode = @ProductCode", p, commandType: CommandType.Text);
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
