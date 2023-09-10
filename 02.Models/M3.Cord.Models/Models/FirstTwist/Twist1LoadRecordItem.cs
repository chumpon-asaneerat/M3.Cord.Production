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
using System.Windows.Controls;
using Newtonsoft.Json.Linq;

#endregion

namespace M3.Cord.Models
{
    public class Twist1LoadRecordItem : NInpc
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        #endregion

        #region Public Properties

        public int Twist1LoadId { get; set; }
        public int SPNo { get; set; }
        public int DeckNo { get; set; }
        public string PalletNo { get; set; }
        public string TraceNo { get; set; }
        public string YarnBarcode { get; set; }

        public SolidColorBrush TextColor { get { return BlackColor; } set { } }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<Twist1LoadRecordItem>> Gets(int Twist1LoadId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<Twist1LoadRecordItem>> rets = new NDbResult<List<Twist1LoadRecordItem>>();

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
            p.Add("@PCTwist1Id", Twist1LoadId);
            try
            {
                var items = cnn.Query<Twist1LoadRecordItem>("GetTwist1LoadRecordItems", p,
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
                rets.data = new List<Twist1LoadRecordItem>();
            }

            return rets;
        }
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The Twist1LoadRecordItem item to save.</param>
        /// <returns></returns>
        public static NDbResult<Twist1LoadRecordItem> Save(Twist1LoadRecordItem value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<Twist1LoadRecordItem> ret = new NDbResult<Twist1LoadRecordItem>();

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
            p.Add("@Twist1LoadId", value.Twist1LoadId);
            p.Add("@SPNo", value.SPNo);
            p.Add("@DeckNo", value.DeckNo);
            p.Add("@PalletNo", value.PalletNo);
            p.Add("@TraceNo", value.TraceNo);
            p.Add("@YarnBarcode", value.YarnBarcode);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveTwist1LoadRecordItem", p, commandType: CommandType.StoredProcedure);
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
