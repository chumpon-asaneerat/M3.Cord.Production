﻿#region Using

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

        public int Twist1LoadId { get; set; }
        public int SPNo { get; set; }

        public bool RawB { get; set; }
        public bool RawE { get; set; }
        public bool CrossB { get; set; }
        public bool CrossE { get; set; }
        public bool FormB { get; set; }
        public bool FormE { get; set; }
        public bool KebaB { get; set; }
        public bool KebaE { get; set; }
        public bool StainB { get; set; }
        public bool StainE { get; set; }
        public bool PaperTubeB { get; set; }
        public bool PaperTubeE { get; set; }
        public bool YarnNoB { get; set; }
        public bool YarnNoE { get; set; }
        public bool BBMarkB { get; set; }
        public bool BBMarkE { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<Twist1CheckSheet>> Gets(int PCTwist1Id)
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

            p.Add("@Twist1LoadId", value.Twist1LoadId);
            p.Add("@SPNo", value.SPNo);

            p.Add("@RawB", value.RawB);
            p.Add("@RawE", value.RawE);
            p.Add("@CrossB", value.CrossB);
            p.Add("@CrossE", value.CrossE);
            p.Add("@FormB", value.FormB);
            p.Add("@FormE", value.FormE);
            p.Add("@KebaB", value.KebaB);
            p.Add("@KebaE", value.KebaE);
            p.Add("@StainB", value.StainB);
            p.Add("@StainE", value.StainE);
            p.Add("@PaperTubeB", value.PaperTubeB);
            p.Add("@PaperTubeE", value.PaperTubeE);
            p.Add("@YarnNoB", value.YarnNoB);
            p.Add("@YarnNoE", value.YarnNoE);
            p.Add("@BBMarkB", value.BBMarkB);
            p.Add("@BBMarkE", value.BBMarkE);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveTwist1CheckSheet", p, commandType: CommandType.StoredProcedure);
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
