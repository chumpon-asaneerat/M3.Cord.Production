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
    public class CordItemCode
    {
        #region Public Proeprties

        public int? PkId { get; set; }
        public string ItemCode { get; set; }
        public string ItemWeaving { get; set; }
        public string ItemYarn { get; set; }
        public string ItemGroup { get; set; }
        public string ItemPrepare { get; set; }
        public string YarnCode { get; set; }
        public string WidthCode { get; set; }
        public decimal? ItemWidth { get; set; }
        public decimal? CoreWeight { get; set; }
        public decimal? FullWeight { get; set; }
        public decimal? WidthWeaving { get; set; }
        public string WeaveType { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static NDbResult<CordItemCode> Save(CordItemCode value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordItemCode> ret = new NDbResult<CordItemCode>();

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
            p.Add("@ItemCode", value.ItemCode);
            p.Add("@ItemWeaving", value.ItemWeaving);
            p.Add("@ItemYarn", value.ItemYarn);
            p.Add("@ItemGroup", value.ItemGroup);
            p.Add("@ItemPrepare", value.ItemPrepare);
            p.Add("@YarnCode", value.YarnCode);
            p.Add("@WidthCode", value.WidthCode);
            p.Add("@ItemWidth", value.ItemWidth);
            p.Add("@CoreWeight", value.CoreWeight);
            p.Add("@FullWeight", value.FullWeight);
            p.Add("@WidthWeaving", value.WidthWeaving);
            p.Add("@WeaveType", value.WeaveType);
            p.Add("@PkId", value.PkId, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveItemCode", p, commandType: CommandType.StoredProcedure);
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
        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<CordItemCode>> Gets()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordItemCode>> rets = new NDbResult<List<CordItemCode>>();

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
            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                var items = cnn.Query<CordItemCode>("GetItemCodes", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList() : null;
                rets.Success(data);

                // Set error number/message
                rets.ErrNum = p.Get<int>("@errNum");
                rets.ErrMsg = p.Get<string>("@errMsg");
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
                rets.data = new List<CordItemCode>();
            }

            return rets;
        }

        #endregion
    }

    public class CordItemYarn
    {
        #region Public Proeprties

        public string ItemYarn { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<CordItemYarn>> Gets()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordItemYarn>> rets = new NDbResult<List<CordItemYarn>>();

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
            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                var items = cnn.Query<CordItemYarn>("GetItemYarns", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList() : null;
                rets.Success(data);

                // Set error number/message
                rets.ErrNum = p.Get<int>("@errNum");
                rets.ErrMsg = p.Get<string>("@errMsg");
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
                rets.data = new List<CordItemYarn>();
            }

            return rets;
        }

        #endregion
    }
}
