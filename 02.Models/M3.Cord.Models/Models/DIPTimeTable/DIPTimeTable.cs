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
		
		public int RowType { get; set; } = 1;
		public DateTime? PeriodTime { get; set; }

        public string LotNo { get; set; }

        public bool? S7BobbinSC { get; set; }
		public bool? S7Bobbin { get; set; }

        public bool? S8CoolingWaterSystemBath1SC { get; set; }
        public decimal? S8CoolingWaterSystemBath1Min { get; set; }
        public decimal? S8CoolingWaterSystemBath1Max { get; set; }
        public decimal? S8CoolingWaterSystemBath1Value { get; set; }

        public bool? S8CoolingWaterSystemBath2SC { get; set; }
        public decimal? S8CoolingWaterSystemBath2Min { get; set; }
        public decimal? S8CoolingWaterSystemBath2Max { get; set; }
        public decimal? S8CoolingWaterSystemBath2Value { get; set; }

        public bool? S8ChemicalWorkSC { get; set; }
        public bool? S8ChemicalWork { get; set; }
        public bool? S8ChemicalFilterSC { get; set; }
        public bool? S8ChemicalFilter { get; set; }

        public bool? S8SpeedSC { get; set; }
        public decimal? S8Speed { get; set; }
        public decimal? S8SpeedErr { get; set; }
        public decimal? S8SpeedValue { get; set; }

        public string S8SpeedS
        {
            get 
            { 
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    fmt += (S8Speed.HasValue) ? S8Speed.Value.ToString("n2") : "";
                    fmt += " ± ";
                    fmt += (S8SpeedErr.HasValue) ? S8SpeedErr.Value.ToString("n2") : "";
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!S8SpeedSC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (S8SpeedSC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return (S8SpeedValue.HasValue) ? S8SpeedValue.Value.ToString("n2") : "";
                }
                else
                {
                    return (S8SpeedValue.HasValue) ? S8SpeedValue.Value.ToString("n2") : "";
                }
            }
            set { }
        }

        public bool? S8StretchDSC { get; set; }
        public decimal? S8StretchD { get; set; }
        public decimal? S8StretchDErr { get; set; }
        public decimal? S8StretchDValue { get; set; }

        public bool? S8StretchHSC { get; set; }
        public decimal? S8StretchH { get; set; }
        public decimal? S8StretchHErr { get; set; }
        public decimal? S8StretchHValue { get; set; }

        public bool? S8StretchNSC { get; set; }
        public decimal? S8StretchN { get; set; }
        public decimal? S8StretchNErr { get; set; }
        public decimal? S8StretchNValue { get; set; }

        public bool? S8TempDSC { get; set; }
        public decimal? S8TempD { get; set; }
        public decimal? S8TempDErr { get; set; }
        public decimal? S8TempDValue { get; set; }

        public bool? S8TempHNSC { get; set; }
        public decimal? S8TempHN { get; set; }
        public decimal? S8TempHNErr { get; set; }
        public decimal? S8TempHNValue { get; set; }

        public bool? S9GlideStatusSC { get; set; }
        public bool? S9GlideStatus { get; set; }

		public string Remark { get; set; }
        public string CheckBy { get; set; }
        public DateTime? CheckDate { get; set; }

        #endregion

        #region Static Methods

        public static DIPTimeTable Create(string productCode)
        {
            var inst = new DIPTimeTable();
            var std = DIPTimeTableStd.Gets(productCode).Value().FirstOrDefault();
            Assign(std, inst);
            return inst;
        }

        public static void Assign(DIPTimeTableStd src, DIPTimeTable dst)
        {
            if (null != src && null != dst)
            {
                dst.ProductCode = src.ProductCode;
                dst.S7BobbinSC = src.S7BobbinSC;
                dst.S8CoolingWaterSystemBath1SC = src.S8CoolingWaterSystemBath1SC;
                dst.S8CoolingWaterSystemBath1Min = src.S8CoolingWaterSystemBath1Min;
                dst.S8CoolingWaterSystemBath1Max = src.S8CoolingWaterSystemBath1Max;
                dst.S8CoolingWaterSystemBath2SC = src.S8CoolingWaterSystemBath2SC;
                dst.S8CoolingWaterSystemBath2Min = src.S8CoolingWaterSystemBath2Min;
                dst.S8CoolingWaterSystemBath2Max = src.S8CoolingWaterSystemBath2Max;
                dst.S8ChemicalWorkSC = src.S8ChemicalWorkSC;
                dst.S8ChemicalFilterSC = src.S8ChemicalFilterSC;
                dst.S8SpeedSC = src.S8SpeedSC;
                dst.S8Speed = src.S8Speed;
                dst.S8SpeedErr = src.S8SpeedErr;
                dst.S8StretchDSC = src.S8StretchDSC;
                dst.S8StretchD = src.S8StretchD;
                dst.S8StretchDErr = src.S8StretchDErr;
                dst.S8StretchHSC = src.S8StretchHSC;
                dst.S8StretchH = src.S8StretchH;
                dst.S8StretchHErr = src.S8StretchHErr;
                dst.S8StretchNSC = src.S8StretchNSC;
                dst.S8StretchN = src.S8StretchN;
                dst.S8StretchNErr = src.S8StretchNErr;
                dst.S8TempDSC = src.S8TempDSC;
                dst.S8TempD = src.S8TempD;
                dst.S8TempDErr = src.S8TempDErr;
                dst.S8TempHNSC = src.S8TempHNSC;
                dst.S8TempHN = src.S8TempHN;
                dst.S8TempHNErr = src.S8TempHNErr;
                dst.S9GlideStatusSC = src.S9GlideStatusSC;
            }
        }

        public static NDbResult<List<DIPTimeTable>> Gets(DateTime date)
        {
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<List<DIPTimeTable>> ret = new NDbResult<List<DIPTimeTable>>();

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
			p.Add("@Date", date);

			try
			{
				var items = cnn.Query<DIPTimeTable>("GetDIPTimeTables", p,
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
				ret.data = new List<DIPTimeTable>();
            }

			return ret;
		}

        public static NDbResult<List<string>> GetLots(DateTime date)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<string>> ret = new NDbResult<List<string>>();

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
            p.Add("@Date", date);

            try
            {
                var items = cnn.Query<string>("GetDIPTimeTableLotLists", p,
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
                ret.data = new List<string>();
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
            p.Add("@RowType", value.RowType);
            p.Add("@PeriodTime", value.PeriodTime);
            p.Add("@LotNo", value.LotNo);

            p.Add("@S7BobbinSC", value.S7BobbinSC);
            p.Add("@S7Bobbin", value.S7Bobbin);

			p.Add("@S8CoolingWaterSystemBath1SC", value.S8CoolingWaterSystemBath1SC);
            p.Add("@S8CoolingWaterSystemBath1Min", value.S8CoolingWaterSystemBath1Min);
            p.Add("@S8CoolingWaterSystemBath1Max", value.S8CoolingWaterSystemBath1Max);
            p.Add("@S8CoolingWaterSystemBath1Value", value.S8CoolingWaterSystemBath1Value);
            
            p.Add("@S8CoolingWaterSystemBath2SC", value.S8CoolingWaterSystemBath2SC);
            p.Add("@S8CoolingWaterSystemBath2Min", value.S8CoolingWaterSystemBath2Min);
            p.Add("@S8CoolingWaterSystemBath2Max", value.S8CoolingWaterSystemBath2Max);
            p.Add("@S8CoolingWaterSystemBath2Value", value.S8CoolingWaterSystemBath2Value);

			p.Add("@S8ChemicalWorkSC", value.S8ChemicalWorkSC);
            p.Add("@S8ChemicalWork", value.S8ChemicalWork);
            p.Add("@S8ChemicalFilterSC", value.S8ChemicalFilterSC);
            p.Add("@S8ChemicalFilter", value.S8ChemicalFilter);

			p.Add("@S8SpeedSC", value.S8SpeedSC);
            p.Add("@S8Speed", value.S8Speed);
            p.Add("@S8SpeedErr", value.S8SpeedErr);
            p.Add("@S8SpeedValue", value.S8SpeedValue);

            p.Add("@S8StretchDSC", value.S8StretchDSC);
            p.Add("@S8StretchD", value.S8StretchD);
            p.Add("@S8StretchDErr", value.S8StretchDErr);
            p.Add("@S8StretchDValue", value.S8StretchDValue);

            p.Add("@S8StretchHSC", value.S8StretchHSC);
            p.Add("@S8StretchH", value.S8StretchH);
            p.Add("@S8StretchHErr", value.S8StretchHErr);
            p.Add("@S8StretchHValue", value.S8StretchHValue);

            p.Add("@S8StretchNSC", value.S8StretchNSC);
            p.Add("@S8StretchN", value.S8StretchN);
            p.Add("@S8StretchNErr", value.S8StretchNErr);
            p.Add("@S8StretchNValue", value.S8StretchNValue);

            p.Add("@S8TempDSC", value.S8TempDSC);
            p.Add("@S8TempD", value.S8TempD);
            p.Add("@S8TempDErr", value.S8TempDErr);
            p.Add("@S8TempDValue", value.S8TempDValue);

            p.Add("@S8TempHNSC", value.S8TempHNSC);
            p.Add("@S8TempHN", value.S8TempHN);
            p.Add("@S8TempHNErr", value.S8TempHNErr);            
            p.Add("@S8TempHNValue", value.S8TempHNValue);

            p.Add("@S9GlideStatusSC", value.S9GlideStatusSC);
            p.Add("@S9GlideStatus", value.S9GlideStatus);

            p.Add("@Remark", value.Remark);
            p.Add("@CheckBy", value.CheckBy);
            p.Add("@CheckDate", value.CheckDate);

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

		#endregion
	}
}
