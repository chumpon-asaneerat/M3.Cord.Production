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
    public class S9AppearanceCheckSheetItem : NInpc
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        #endregion

        public SolidColorBrush TextColor { get { return BlackColor; } set { } }

        #region Public Properties

        public int AppearId { get; set; }
        public int SPNo { get; set; }

        public bool CheckGood { get; set; } = false;
        public bool CheckBad { get; set; } = false;
        public bool Check2Color { get; set; } = false;
        public bool CheckKeiba { get; set; } = false;
        public bool CheckWeight { get; set; } = false;
        public bool CheckFrontTwist { get; set; } = false;
        public bool CheckBackTwist { get; set; } = false;
        public bool CheckSnarl { get; set; } = false;
        public bool CheckTube { get; set; } = false;

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<S9AppearanceCheckSheetItem>> Gets(int? AppearId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<S9AppearanceCheckSheetItem>> rets = new NDbResult<List<S9AppearanceCheckSheetItem>>();

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
            p.Add("@AppearId", AppearId);
            try
            {
                var items = cnn.Query<S9AppearanceCheckSheetItem>("GetS9AppearanceCheckSheetItems", p,
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
                rets.data = new List<S9AppearanceCheckSheetItem>();
            }

            return rets;
        }
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The S9AppearanceCheckSheetItem to save.</param>
        /// <returns></returns>
        public static NDbResult<S9AppearanceCheckSheetItem> Save(S9AppearanceCheckSheetItem value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S9AppearanceCheckSheetItem> ret = new NDbResult<S9AppearanceCheckSheetItem>();

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

            p.Add("@AppearId", value.AppearId);
            p.Add("@SPNo", value.SPNo);

            p.Add("@CheckGood", value.CheckGood);
            p.Add("@CheckBad", value.CheckBad);
            p.Add("@Check2Color", value.Check2Color);
            p.Add("@CheckKeiba", value.CheckKeiba);
            p.Add("@CheckWeight", value.CheckWeight);
            p.Add("@CheckFrontTwist", value.CheckFrontTwist);
            p.Add("@CheckBackTwist", value.CheckBackTwist);
            p.Add("@CheckSnarl", value.CheckSnarl);
            p.Add("@CheckTube", value.CheckTube);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveS9AppearanceCheckSheetItem", p, commandType: CommandType.StoredProcedure);
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
