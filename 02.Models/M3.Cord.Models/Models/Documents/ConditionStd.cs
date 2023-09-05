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

#endregion

namespace M3.Cord.Models
{
    public enum ConditionParamTypes : int
    {
        String = 1, 
        Number = 2, 
        NumberRange = 3
    }

    public class ConditionStd : NInpc
    {
        #region Public Properties

        public string ProcessName { get; set; }
        public string ProductCode { get; set; }
        public string ParamName { get; set; }
        public ConditionParamTypes ParamType { get; set; }
        public bool? SC { get; set; }
        public string StdValueS { get; set; }
        public decimal? StdValueD { get; set; }
        public decimal? StdValueE { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The PCCard item to save.</param>
        /// <returns></returns>
        public static NDbResult<ConditionStd> Save(ConditionStd value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<ConditionStd> ret = new NDbResult<ConditionStd>();

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
            p.Add("@ProcessName", value.ProcessName);
            p.Add("@ProductCode", value.ProductCode);
            p.Add("@ParamName", value.ParamName);
            p.Add("@ParamType", value.ParamType);
            p.Add("@SC", value.SC);
            p.Add("@StdValueS", value.StdValueS);
            p.Add("@StdValueD", value.StdValueD);
            p.Add("@StdValueE", value.StdValueE);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveConditionStd", p, commandType: CommandType.StoredProcedure);
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
        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<ConditionStd>> Gets(string ProcessName, string ProductCode)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<ConditionStd>> rets = new NDbResult<List<ConditionStd>>();

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
            p.Add("@ProcessName", ProcessName);
            p.Add("@ProductCode", ProductCode);
            try
            {
                var items = cnn.Query<ConditionStd>("GetConditionStds", p,
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
                rets.data = new List<ConditionStd>();
            }

            return rets;
        }

        #endregion
    }
}
