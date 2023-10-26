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
    public class S9CleanCheckSheetItem : NInpc
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        #endregion

        public SolidColorBrush TextColor { get { return BlackColor; } set { } }

        #region Public Properties

        public int CleanId { get; set; }
        public int SPNo { get; set; }

        public bool? CheckSplitter { get; set; }
        public bool? CheckRollerCeramic { get; set; }
        public bool? CheckSlidebar { get; set; }
        public bool? CheckRollerCambox { get; set; }
        public bool? CheckTopGuide { get; set; }
        public bool? CheckBottomGuide { get; set; }
        public bool? CheckRollerFrontS9 { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<S9CleanCheckSheetItem>> Gets(int? CleanId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<S9CleanCheckSheetItem>> rets = new NDbResult<List<S9CleanCheckSheetItem>>();

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
            p.Add("@CleanId", CleanId);
            try
            {
                var items = cnn.Query<S9CleanCheckSheetItem>("GetS9CleanCheckSheetItems", p,
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
                rets.data = new List<S9CleanCheckSheetItem>();
            }

            return rets;
        }
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The DIPMaterialCheckSheetItem to save.</param>
        /// <returns></returns>
        public static NDbResult<S9CleanCheckSheetItem> Save(S9CleanCheckSheetItem value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S9CleanCheckSheetItem> ret = new NDbResult<S9CleanCheckSheetItem>();

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

            p.Add("@CleanId", value.CleanId);
            p.Add("@SPNo", value.SPNo);

            p.Add("@CheckSplitter", value.CheckSplitter);
            p.Add("@CheckRollerCeramic", value.CheckRollerCeramic);
            p.Add("@CheckSlidebar", value.CheckSlidebar);
            p.Add("@CheckRollerCambox", value.CheckRollerCambox);
            p.Add("@CheckTopGuide", value.CheckTopGuide);
            p.Add("@CheckBottomGuide", value.CheckBottomGuide);
            p.Add("@CheckRollerFrontS9", value.CheckRollerFrontS9);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveS9CleanCheckSheetItem", p, commandType: CommandType.StoredProcedure);
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
