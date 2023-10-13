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
    public class Twist1CheckSheet : NInpc
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        #endregion

        public SolidColorBrush TextColor { get { return BlackColor; } set { } }

        #region Public Properties

        public int? Twist1CheckId { get; set; }
        public int PCTwist1Id { get; set; }
        public DateTime? ProductionDate { get; set; }
        public DateTime? ConditionDate { get; set; }
        public bool TestFlag { get; set; }
        public int DoffNo { get; set; }
        public string ItemYarn { get; set; }
        public string ShiftName { get; set; }
        public int UserId { get; set; }
        public string Chief { get; set; }
        public string Remark { get; set; }
        // Runtime only
        public string ProductLotNo { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<Twist1CheckSheet>> Gets(int PCTwist1Id, 
            int? Twist1CheckId = new int?())
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<Twist1CheckSheet>> rets = new NDbResult<List<Twist1CheckSheet>>();

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
            p.Add("@PCTwist1Id", PCTwist1Id);
            p.Add("@Twist1CheckId", Twist1CheckId);
            try
            {
                var items = cnn.Query<Twist1CheckSheet>("GetTwist1CheckSheets", p,
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
                rets.data = new List<Twist1CheckSheet>();
            }

            return rets;
        }
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The Twist1CheckSheet item to save.</param>
        /// <returns></returns>
        public static NDbResult<Twist1CheckSheet> Save(Twist1CheckSheet value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<Twist1CheckSheet> ret = new NDbResult<Twist1CheckSheet>();

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

            p.Add("@PCTwist1Id", value.PCTwist1Id);
            p.Add("@ProductionDate", value.ProductionDate);
            p.Add("@ConditionDate", value.ConditionDate);
            p.Add("@TestFlag", value.TestFlag);
            p.Add("@DoffNo", value.DoffNo);
            p.Add("@ItemYarn", value.ItemYarn);
            p.Add("@ShiftName", value.ShiftName);
            p.Add("@UserId", value.UserId);
            p.Add("@Chief", value.Chief);
            p.Add("@Remark", value.Remark);

            p.Add("@Twist1CheckId", value.Twist1CheckId, DbType.Int32, direction: ParameterDirection.InputOutput); 

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveTwist1CheckSheet", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);

                // get pk
                value.Twist1CheckId = p.Get<int?>("@Twist1CheckId");

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
