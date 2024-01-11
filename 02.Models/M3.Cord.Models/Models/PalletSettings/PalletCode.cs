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
using System.ComponentModel;

#endregion

namespace M3.Cord.Models
{
    public class PalletCode
    {
        #region Public Properties

        public string MCCode { get; set; }
        public int Year { get; set; }
        public int LastId { get; set; }

        #endregion

        #region Static Methods

        public static NDbResult<PalletCode> GetLastId(string mcCode, int? year = new int?())
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<PalletCode> ret = new NDbResult<PalletCode>();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                // Set error number/message
                ret.ErrNum = 8000;
                ret.ErrMsg = msg;

                return ret;
            }

            int cYear = (year.HasValue) ? year.Value : DateTime.Today.Year;
            var p = new DynamicParameters();
            p.Add("@MCCode", mcCode);
            p.Add("@Year", cYear);

            try
            {
                var item = cnn.Query<PalletCode>("GetPalletCode", p,
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
        /// <returns></returns>
        public static NDbResult UpdateLastId(string mcCode, int newId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<PalletSettingItem> ret = new NDbResult<PalletSettingItem>();

            if (string.IsNullOrEmpty(mcCode))
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
            int year = DateTime.Now.Year;

            var p = new DynamicParameters();
            p.Add("@MCCode", mcCode);
            p.Add("@Year", year);
            p.Add("@LastId", newId);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("UpdatePalletCodeLastId", p, commandType: CommandType.StoredProcedure);
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
