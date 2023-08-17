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
using System.Reflection.Emit;

#endregion

namespace M3.Cord.Models
{
    public class YarnLoadSheetDoff : NInpc
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        #endregion

        #region Constructor

        public YarnLoadSheetDoff()
        {
            
        }

        #endregion

        #region Public Properties

        public int YarnLoadSheetDoffId { get; set; }
        public int YarnLoadSheetId { get; set; }
        public DateTime RecordDate { get; set; }
        public string DoffNos { get; set; }
        public string Shift { get; set; }
        // Readonly
        public string ItemYarn { get; set; }
        public string ProductLotNo { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The YarnLoadSheetDoff item to save.</param>
        /// <returns></returns>
        public static NDbResult<YarnLoadSheetDoff> Save(YarnLoadSheetDoff value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<YarnLoadSheetDoff> ret = new NDbResult<YarnLoadSheetDoff>();

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
            p.Add("@YarnLoadSheetId", value.YarnLoadSheetId);
            p.Add("@RecordDate", value.RecordDate);
            p.Add("@DoffNos", value.DoffNos);
            p.Add("@Shift", value.Shift);
            p.Add("@YarnLoadSheetDoffId", value.YarnLoadSheetDoffId, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveYarnLoadSheetDoff", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);
                // Set PK
                value.YarnLoadSheetDoffId = p.Get<int>("@YarnLoadSheetDoffId");
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
