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
    public class Twist1LoadRecord
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        #endregion

        #region Public Properties

        public int? Twist1LoadId { get; set; }
        public int? PCTwist1Id { get; set; }
        public DateTime ProductionDate { get; set; }
        public bool TestFlag { get; set; }
        public int DoffNo { get; set; }
        public string ItemYarn { get; set; }
        public string ShiftName { get; set; }
        public string Chief { get; set; }
        public string Remark { get; set; }
        public bool Keiba { get; set; }
        public bool Traverse { get; set; }
        public bool Crossing { get; set; }
        public bool ScrapeTube { get; set; }
        public bool Form { get; set; }
        public bool Stain { get; set; }

        public SolidColorBrush TextColor { get { return BlackColor; } set { } }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<Twist1LoadRecord>> Gets(int PCTwist1Id)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<Twist1LoadRecord>> rets = new NDbResult<List<Twist1LoadRecord>>();

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
            try
            {
                var items = cnn.Query<Twist1LoadRecord>("GetTwist1LoadRecords", p,
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
                rets.data = new List<Twist1LoadRecord>();
            }

            return rets;
        }
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The Twist1LoadRecord item to save.</param>
        /// <returns></returns>
        public static NDbResult<Twist1LoadRecord> Save(Twist1LoadRecord value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<Twist1LoadRecord> ret = new NDbResult<Twist1LoadRecord>();

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
            p.Add("@TestFlag", value.TestFlag);
            p.Add("@DoffNo", value.DoffNo);
            p.Add("@ItemYarn", value.ItemYarn);
            p.Add("@ShiftName", value.ShiftName);
            p.Add("@Chief", value.Chief);
            p.Add("@Remark", value.Remark);

            p.Add("@Keiba", value.Keiba);
            p.Add("@Traverse", value.Traverse);
            p.Add("@Crossing", value.Crossing);
            p.Add("@ScrapeTube", value.ScrapeTube);
            p.Add("@Form", value.Form);
            p.Add("@Stain", value.Stain);

            p.Add("@Twist1LoadId", value.Twist1LoadId, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveTwist1LoadRecord", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);
                // Set PK
                value.Twist1LoadId = p.Get<int>("@Twist1LoadId");
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
