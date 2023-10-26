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
    public class S7CreelCheckSheetItem : NInpc
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        #endregion

        public SolidColorBrush TextColor { get { return BlackColor; } set { } }

        #region Public Properties

        public int CreelId { get; set; }
        public int SPNo { get; set; }

        public bool CheckUnstable { get; set; } = false;
        public bool CheckNotReachEnd { get; set; } = false;
        public bool CheckNotStraight { get; set; } = false;
        public bool CheckHasSound { get; set; } = false;

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<S7CreelCheckSheetItem>> Gets(int? CreelId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<S7CreelCheckSheetItem>> rets = new NDbResult<List<S7CreelCheckSheetItem>>();

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
            p.Add("@CreelId", CreelId);
            try
            {
                var items = cnn.Query<S7CreelCheckSheetItem>("GetS7CreelCheckSheetItems", p,
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
                rets.data = new List<S7CreelCheckSheetItem>();
            }

            return rets;
        }
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The DIPMaterialCheckSheetItem to save.</param>
        /// <returns></returns>
        public static NDbResult<S7CreelCheckSheetItem> Save(S7CreelCheckSheetItem value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S7CreelCheckSheetItem> ret = new NDbResult<S7CreelCheckSheetItem>();

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

            p.Add("@CreelId", value.CreelId);
            p.Add("@SPNo", value.SPNo);

            p.Add("@CheckUnstable", value.CheckUnstable);
            p.Add("@CheckNotReachEnd", value.CheckNotReachEnd);
            p.Add("@CheckNotStraight", value.CheckNotStraight);
            p.Add("@CheckHasSound", value.CheckHasSound);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveS7CreelCheckSheetItem", p, commandType: CommandType.StoredProcedure);
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
