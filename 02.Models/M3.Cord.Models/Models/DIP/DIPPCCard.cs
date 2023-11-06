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
using System.Web;

#endregion

namespace M3.Cord.Models
{
    public class DIPPCCard
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        #endregion

        public SolidColorBrush TextColor { get { return BlackColor; } set { } }

        #region Public Properties

        public int? DIPPCId { get; set; }

        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }

        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ItemYarn { get; set; }
        public string CordStructure { get; set; }
        public string TreatRoute { get; set; }
        public string TwistSpec { get; set; }

        public string DIPLotNo { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? FinishTime { get; set; }

        public bool? FinishFlag { get; set; }
        public bool? DeleteFlag { get; set; }

        public string MCCode { get; set; }
        public int DoffNo { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public static NDbResult<DIPPCCard> Get(int? dipPCId = new int?())
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<DIPPCCard> rets = new NDbResult<DIPPCCard>();

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
            p.Add("@DIPPCId", dipPCId);
            try
            {
                var items = cnn.Query<DIPPCCard>("GetDIPPCCards", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList().FirstOrDefault() : null;
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
                rets.data = null;
            }

            return rets;
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public static NDbResult<DIPPCCard> Current(string mcCode)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<DIPPCCard> rets = new NDbResult<DIPPCCard>();

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
            p.Add("@MCCode", mcCode);

            try
            {
                var items = cnn.Query<DIPPCCard>("GetLastDIPPCCard", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList().FirstOrDefault() : null;
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
                rets.data = null;
            }

            return rets;
        }
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The DIP PCCard item to save.</param>
        /// <returns></returns>
        public static NDbResult<DIPPCCard> Save(DIPPCCard value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<DIPPCCard> ret = new NDbResult<DIPPCCard>();

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
            p.Add("@CustomerId", value.CustomerId);
            p.Add("@ProductCode", value.ProductCode);
            p.Add("@DIPLotNo", value.DIPLotNo);
            p.Add("@ItemYarn", value.ItemYarn);

            p.Add("@FinishFlag", value.FinishFlag);
            p.Add("@DeleteFlag", value.DeleteFlag);

            p.Add("@MCCode", value.MCCode);
            p.Add("@DoffNo", value.DoffNo);
            p.Add("@CreateBy", value.CreateBy);
            p.Add("@CreateDate", value.CreateDate);

            p.Add("@DIPPCId", value.DIPPCId, DbType.Int32, direction: ParameterDirection.InputOutput);
            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveDIPPCCard", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);
                // Set PK
                value.DIPPCId = p.Get<int?>("@DIPPCId");
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

        public static NDbResult Start(int dipPCId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult ret = new NDbResult();

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
            p.Add("@DIPPCId", dipPCId);

            try
            {
                cnn.Execute("StartDIP", p, commandType: CommandType.StoredProcedure);
                ret.Success();
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

        public static NDbResult End(int dipPCId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult ret = new NDbResult();

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
            p.Add("@DIPPCId", dipPCId);

            try
            {
                cnn.Execute("EndDIP", p, commandType: CommandType.StoredProcedure);
                ret.Success();
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

        public static NDbResult Finish(int dipPCId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult ret = new NDbResult();

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
            p.Add("@DIPPCId", dipPCId);

            try
            {
                cnn.Execute("FinishDIP", p, commandType: CommandType.StoredProcedure);
                ret.Success();
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
