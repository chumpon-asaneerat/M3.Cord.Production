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
using System.Net.NetworkInformation;
using System.Xml.Linq;

#endregion

namespace M3.Cord.Models
{
    public class PCCard
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        #endregion

        #region Public Proeprties

        public int PCId { get; set; }
        public string PINo { get; set; }
        public DateTime? @PCDate { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ItemYarn { get; set; }
        public string CordStructure { get; set; }
        public string ProductLotNo { get; set; }
        public decimal? TargetQty { get; set; }
        public bool? FinishFlag { get; set; }
        public bool? DeleteFlag { get; set; }

        public SolidColorBrush TextColor { get { return BlackColor; } set { } }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<PCCard>> Gets(string productLotNo = null, string customerName = null)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<PCCard>> rets = new NDbResult<List<PCCard>>();

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
            p.Add("@ProductLotNo", string.IsNullOrWhiteSpace(productLotNo) ? null : productLotNo);
            p.Add("@CustomerName", string.IsNullOrWhiteSpace(customerName) ? null : customerName);
            try
            {
                var items = cnn.Query<PCCard>("GetPCCards", p,
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
                rets.data = new List<PCCard>();
            }

            return rets;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The PCCard item to save.</param>
        /// <returns></returns>
        public static NDbResult<PCCard> Save(PCCard value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<PCCard> ret = new NDbResult<PCCard>();

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
            p.Add("@PINo", value.PINo);
            p.Add("@PCDate", value.PCDate);
            p.Add("@CustomerId", value.CustomerId);
            p.Add("@ProductCode", value.ProductCode);
            p.Add("@ProductLotNo", value.ProductLotNo);
            p.Add("@TargetQty", value.@TargetQty);
            p.Add("@FinishFlag", value.FinishFlag);
            p.Add("@DeleteFlag", value.DeleteFlag);
            p.Add("@PCId", value.PCId, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SavePCCard", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);
                // Set PK
                value.PCId = p.Get<int>("@PCId");
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

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="value">The PCCard item to delete.</param>
        /// <returns></returns>
        public static NDbResult Delete(PCCard value)
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
            p.Add("@PCId", value.PCId);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("DeletePCCard", p, commandType: CommandType.StoredProcedure);
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
