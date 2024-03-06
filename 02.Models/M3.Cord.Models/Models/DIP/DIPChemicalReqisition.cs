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
    public class DIPChemicalReqisition : NInpc
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        #endregion

        public SolidColorBrush TextColor { get { return BlackColor; } set { } }

        #region Public Properties

        public int? ReqId { get; set; }
        public int? DIPPCId { get; set; }
        public DateTime ReqDate { get; set; }

        public string ProductCode { get; set; }
        public string DIPLotNo { get; set; }
        public string SolutionName { get; set; }
        public string TankNo { get; set; }

        public decimal? Quantity { get; set; }

        public string S8LotNo { get; set; }

        public string UserName { get; set; }
        public string Chief { get; set; }

        public bool ShowVerify
        {
            get
            {
                return (ReqId.HasValue && ReqId.Value > 0);
            }
            set { }
        }

        public bool IsVerify
        {
            get { return !string.IsNullOrEmpty(Chief); }
            set { }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<DIPChemicalReqisition>> Gets(int DIPPCId,
            int? ReqId = new int?())
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<DIPChemicalReqisition>> rets = new NDbResult<List<DIPChemicalReqisition>>();

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
            p.Add("@DIPPCId", DIPPCId);
            p.Add("@ReqId", ReqId);
            try
            {
                var items = cnn.Query<DIPChemicalReqisition>("GetDIPChemicalReqisitions", p,
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

            if (null == rets.data)
            {
                // create empty list.
                rets.data = new List<DIPChemicalReqisition>();
            }

            return rets;
        }
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The DIPChemicalReqisition item to save.</param>
        /// <returns></returns>
        public static NDbResult<DIPChemicalReqisition> Save(DIPChemicalReqisition value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<DIPChemicalReqisition> ret = new NDbResult<DIPChemicalReqisition>();

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

            p.Add("@DIPPCId", value.DIPPCId);
            p.Add("@ReqDate", value.ReqDate);
            p.Add("@ProductCode", value.ProductCode);
            p.Add("@DIPLotNo", value.DIPLotNo);
            p.Add("@SolutionName", value.SolutionName);
            p.Add("@TankNo", value.TankNo);
            p.Add("@Quantity", value.Quantity);
            p.Add("@S8LotNo", value.S8LotNo);
            p.Add("@UserName", value.UserName);
            p.Add("@Chief", value.Chief);
            p.Add("@ReqId", value.ReqId, DbType.Int32, direction: ParameterDirection.InputOutput);
            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveDIPChemicalReqisition", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);

                // get pk
                value.ReqId = p.Get<int?>("@ReqId");

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
