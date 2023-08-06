#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Reflection;

using System.Windows.Media;

using NLib;

using Dapper;
using Newtonsoft.Json;
using System.Net;
using NLib.Models;
using System.Reflection.Emit;

#endregion

namespace M3.Cord.Models
{
    public class YarnLoadSheet : NInpc
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        #endregion

        #region Constructor

        public YarnLoadSheet()
        {
            
        }

        #endregion

        #region Public Properties

        public int YarnLoadSheetId { get; set; }
        public int CordProductPkId { get; set; }
        public string MCCode { get; set; }
        public bool DeleteFlag { get; set; }
        public bool FinishFlag { get; set; }

        public string ProductLotNo { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string ItemYarn { get; set; }
        public string ItemCode { get; set; }
        public string Item400 { get; set; }
        public string Color { get; set; }
        public decimal TargetQty { get; set; } = decimal.Zero;
        public decimal ActualQty { get; set; } = decimal.Zero;
        public string ProcessName { get; set; }
        public int DeckPerCore { get; set; }
        public int StartCore { get; set; }
        public int EndCore { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The YarnLoadSheet item to save.</param>
        /// <returns></returns>
        public static NDbResult<YarnLoadSheet> Save(YarnLoadSheet value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<YarnLoadSheet> ret = new NDbResult<YarnLoadSheet>();

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
            p.Add("@CordProductPkId", value.CordProductPkId);
            p.Add("@MCCode", value.MCCode);
            p.Add("@FinishFlag", value.FinishFlag);
            p.Add("@DeleteFlag", value.DeleteFlag);
            p.Add("@YarnLoadSheetId", value.YarnLoadSheetId, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveYarnLoadSheet", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);
                // Set PK
                value.YarnLoadSheetId = p.Get<int>("@YarnLoadSheetId");
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
        /// Get
        /// </summary>
        /// <param name="value">The MC Code.</param>
        /// <returns></returns>
        public static NDbResult<YarnLoadSheet> Get(string MCCode)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<YarnLoadSheet> ret = new NDbResult<YarnLoadSheet>();

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
            p.Add("@MCCode", MCCode);

            try
            {
                var items = cnn.Query<YarnLoadSheet>("GetYarnLoadSheets", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.FirstOrDefault() : null;
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
        /// AddNew
        /// </summary>
        /// <param name="value">The RawMaterialSheet item to save.</param>
        /// <returns></returns>
        public static NDbResult AddNew(FirstTwistMC mc, CordProduct product)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult ret = new NDbResult();

            if (null == mc || null == product)
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
            p.Add("@MCCode", mc.MCCode);
            p.Add("@CordProductPkId", product.CordProductPkId);
            p.Add("@FinishFlag", false);
            p.Add("@DeleteFlag", false);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveYarnLoadSheet", p, commandType: CommandType.StoredProcedure);
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
