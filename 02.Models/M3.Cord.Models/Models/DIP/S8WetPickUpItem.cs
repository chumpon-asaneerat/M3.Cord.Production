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
    public class S8WetPickUpItem
    {
        #region Public Properties

        public string ProductCode { get; set; }
        public string LotNo { get; set; }
        public DateTime? DoffingDate { get; set; }
        public int DoffNo { get; set; }
        public int TwistNo { get; set; }
        public int RowType { get; set; }

        public string NipPressure { get; set; }
        public decimal? WPU { get; set; }
        public decimal? WPUErr { get; set; }
        public decimal? WPUValue { get; set; }

        public decimal? FirstAmt { get; set; }
        public decimal? UseAmt { get; set; }
        public decimal? RestAmt { get; set; }
        public decimal? ThrowAmt { get; set; }

        public string Operator { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Remark { get; set; }

        #endregion

        #region Static Methods

        public static NDbResult<List<S8WetPickUpItem>> Gets(string ProductCode, string LotNo, 
            DateTime? DoffingDate, int TwistNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<S8WetPickUpItem>> ret = new NDbResult<List<S8WetPickUpItem>>();

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
            p.Add("@TwistNo", TwistNo);

            try
            {
                var item = cnn.Query<S8WetPickUpItem>("GetS8WetPickUpItems", p,
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
        public static NDbResult<S8WetPickUpItem> Save(S8WetPickUpItem value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S8WetPickUpItem> ret = new NDbResult<S8WetPickUpItem>();

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

            p.Add("@DoffingNo", value.DoffNo);
            p.Add("@TwistNo", value.TwistNo);
            p.Add("@RowType", value.RowType);
            p.Add("@NipPressure", value.NipPressure);
            p.Add("@WPU", value.WPU);
            p.Add("@WPUErr", value.WPUErr);
            p.Add("@WPUValue", value.WPUValue);

            p.Add("@FirstAmt", value.FirstAmt);
            p.Add("@UseAmt", value.UseAmt);
            p.Add("@RestAmt", value.RestAmt);
            p.Add("@ThrowAmt", value.ThrowAmt);

            p.Add("@Operator", value.Operator);
            p.Add("@UpdateDate", value.UpdateDate);
            p.Add("@Remark", value.Remark);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveS8WetPickupItem", p, commandType: CommandType.StoredProcedure);
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
