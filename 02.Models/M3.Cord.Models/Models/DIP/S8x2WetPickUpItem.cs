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
using System.Windows;

#endregion

namespace M3.Cord.Models
{
    public class S8x2WetPickUpItem : NInpc
    {
        #region Private Methods

        private void CalcRestAmt()
        {
            RestAmt = FirstAmt.HasValue ? UseAmt.HasValue ? (FirstAmt.Value - UseAmt.Value) : FirstAmt.Value : new decimal?();
            Raise(() => this.RestAmt);
        }

        private void CalcRestAccumAmt()
        {
            if (RestAmt.HasValue)
            {
                RestAccumAmt = RestAmt;
                if (PrevAmt.HasValue)
                {
                    RestAccumAmt += PrevAmt.Value;
                }
                if (AddAmt.HasValue)
                {
                    RestAccumAmt += AddAmt.Value;
                }
                Raise(() => this.RestAccumAmt);
            }
        }

        #endregion

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

        public Visibility FirstAmtVisible
        {
            get { return PrevAmt.HasValue ? Visibility.Collapsed : Visibility.Visible; }
            set { }
        }
        public Visibility FirstAccumAmtVisible
        {
            get { return PrevAmt.HasValue ? Visibility.Visible : Visibility.Collapsed; }
            set { }
        }

        public decimal? PrevAmt
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcRestAmt();
                    CalcRestAccumAmt();
                });
            }
        }

        public decimal? FirstAccumAmt
        {
            get
            {
                return FirstAmt.HasValue ? PrevAmt.HasValue ? FirstAmt.Value + PrevAmt.Value : FirstAmt.Value : new decimal?();
            }
            set { }
        }

        public decimal? RestAccumAmt
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {

                });
            }
        }

        public decimal? FirstAmt 
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () => 
                {
                    CalcRestAmt();
                    CalcRestAccumAmt();
                });
            }
        }
        public decimal? AddAmt
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcRestAmt();
                    CalcRestAccumAmt();
                });
            }
        }
        public decimal? UseAmt 
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcRestAmt();
                    CalcRestAccumAmt();
                });
            }
        }
        public decimal? RestAmt 
        {
            get { return Get<decimal?>(); }
            set 
            { 
                Set(value, () => 
                {
                    CalcRestAccumAmt();
                }); 
            }
        }
        public decimal? ThrowAmt { get; set; }

        public string Operator { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Remark { get; set; }

        #endregion

        #region Static Methods

        public static NDbResult<List<S8x2WetPickUpItem>> Gets(string ProductCode, string LotNo, 
            DateTime? DoffingDate, int doffNo, int TwistNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<S8x2WetPickUpItem>> ret = new NDbResult<List<S8x2WetPickUpItem>>();

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
            p.Add("@DoffNo", doffNo);
            p.Add("@TwistNo", TwistNo);

            try
            {
                var item = cnn.Query<S8x2WetPickUpItem>("GetS8x2WetPickUpItems", p,
                    commandType: CommandType.StoredProcedure).ToList();
                
                if (null != item && item.Count > 0)
                {
                    decimal accum = decimal.Zero;
                    S8x2WetPickUpItem prev = null;
                    S8x2WetPickUpItem curr = null;
                    for (int i = 0; i < item.Count; i++)
                    {
                        if (i > 0) prev = item[i - 1];
                        curr = item[i];
                        accum += (null != prev) ? prev.RestAmt.Value : decimal.Zero;
                        curr.PrevAmt = (accum > decimal.Zero) ? accum : new decimal?();
                    }
                }

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

        public static NDbResult<S8x2WetPickUpItem> GetLast(string ProductCode, string LotNo,
            int TwistNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S8x2WetPickUpItem> ret = new NDbResult<S8x2WetPickUpItem>();

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
            p.Add("@TwistNo", TwistNo);

            try
            {
                var item = cnn.Query<S8x2WetPickUpItem>("GetS8x2WetPickUpLastItem", p,
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
        /// <param name="value">The S8WetPickUp item to save.</param>
        /// <returns></returns>
        public static NDbResult<S8x2WetPickUpItem> Save(S8x2WetPickUpItem value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S8x2WetPickUpItem> ret = new NDbResult<S8x2WetPickUpItem>();

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
                cnn.Execute("SaveS8x2WetPickupItem", p, commandType: CommandType.StoredProcedure);
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
