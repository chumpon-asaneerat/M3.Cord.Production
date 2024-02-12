#region Using

using Dapper;
using NLib;
using NLib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

#endregion

namespace M3.Cord.Models
{
    public class NDbModel : NInpc
    {
        public static NDbResult CallSP(
            string spName,
            Action<DynamicParameters> initParameters = null,
            Action<DynamicParameters> resultParameters = null)
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
            if (null != initParameters)
            {
                initParameters(p);
            }

            try
            {
                cnn.Execute(spName, p, commandType: CommandType.StoredProcedure);
                ret.Success();

                if (null != resultParameters)
                {
                    resultParameters(p);
                }
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

        public static NDbResult<T> CallSP<T>(
            string spName, T inst,
            Action<DynamicParameters, T> initParameters = null,
            Action<DynamicParameters, T> resultParameters = null) 
            where T : class, new()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<T> ret = new NDbResult<T>();

            if (null == inst)
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
            if (null != initParameters)
            {
                initParameters(p, inst);
            }

            try
            {
                cnn.Execute(spName, p, commandType: CommandType.StoredProcedure);
                ret.Success(inst);

                if (null != resultParameters)
                {
                    resultParameters(p, inst);
                }
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

        public static NDbResult<List<T>> GetSPs<T>(string spName,
            Action<DynamicParameters> initParameters = null)
            where T : class, new()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<T>> rets = new NDbResult<List<T>>();

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
            if (null != initParameters)
            {
                initParameters(p);
            }

            try
            {
                var items = cnn.Query<T>(spName, p,
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
                rets.data = new List<T>();
            }

            return rets;
        }
    }
}
