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
using System.Windows;

#endregion

namespace M3.Cord.Models
{
    public static class TwistUtils
    {
        public class GetTwist1CheckSheetLastDoff
        {
            #region Public Properties

            public int PCTwist1Id { get; set; }
            public int DoffNo { get; set; }

            #endregion

            #region Static Methods

            /// <summary>
            /// Get
            /// </summary>
            /// <param name="PCTwist1Id"></param>
            /// <param name="mcCode"></param>
            /// <param name="testFlag"></param>
            /// <returns></returns>
            public static NDbResult<GetTwist1CheckSheetLastDoff> Get(int PCTwist1Id,
                string mcCode, bool testFlag)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<GetTwist1CheckSheetLastDoff> rets = new NDbResult<GetTwist1CheckSheetLastDoff>();

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
                p.Add("@MCCode", mcCode);
                p.Add("@TestFlag", testFlag);
                try
                {
                    var items = cnn.Query<GetTwist1CheckSheetLastDoff>("GetTwist1CheckSheetLastDoff", p,
                        commandType: CommandType.StoredProcedure);
                    var data = (null != items) ? items.FirstOrDefault() : null;
                    rets.Success(data);
                }
                catch (Exception ex)
                {
                    med.Err(ex);
                    // Set error number/message
                    rets.ErrNum = 9999;
                    rets.ErrMsg = ex.Message;
                }

                return rets;
            }

            #endregion
        }

        public class GetTwist1LoadRecordLastDoff
        {
            #region Public Properties

            public int PCTwist1Id { get; set; }
            public int DoffNo { get; set; }

            #endregion

            #region Static Methods

            /// <summary>
            /// Get
            /// </summary>
            /// <param name="PCTwist1Id"></param>
            /// <param name="mcCode"></param>
            /// <param name="testFlag"></param>
            /// <returns></returns>
            public static NDbResult<GetTwist1LoadRecordLastDoff> Get(int PCTwist1Id,
                string mcCode, bool testFlag)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<GetTwist1LoadRecordLastDoff> rets = new NDbResult<GetTwist1LoadRecordLastDoff>();

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
                p.Add("@MCCode", mcCode);
                p.Add("@TestFlag", testFlag);
                try
                {
                    var items = cnn.Query<GetTwist1LoadRecordLastDoff>("GetTwist1LoadRecordLastDoff", p,
                        commandType: CommandType.StoredProcedure);
                    var data = (null != items) ? items.FirstOrDefault() : null;
                    rets.Success(data);
                }
                catch (Exception ex)
                {
                    med.Err(ex);
                    // Set error number/message
                    rets.ErrNum = 9999;
                    rets.ErrMsg = ex.Message;
                }

                return rets;
            }

            #endregion
        }

        public class GetTwist1OptsLastDoff
        {
            #region Public Properties

            public int PCTwist1Id { get; set; }
            public int DoffNo { get; set; }

            #endregion

            #region Static Methods

            /// <summary>
            /// Get
            /// </summary>
            /// <param name="PCTwist1Id"></param>
            /// <param name="mcCode"></param>
            /// <param name="testFlag"></param>
            /// <returns></returns>
            public static NDbResult<GetTwist1OptsLastDoff> Get(int PCTwist1Id,
                string mcCode, bool testFlag)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<GetTwist1OptsLastDoff> rets = new NDbResult<GetTwist1OptsLastDoff>();

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
                p.Add("@MCCode", mcCode);
                p.Add("@TestFlag", testFlag);
                try
                {
                    var items = cnn.Query<GetTwist1OptsLastDoff>("GetTwist1OptsLastDoff", p,
                        commandType: CommandType.StoredProcedure);
                    var data = (null != items) ? items.FirstOrDefault() : null;
                    rets.Success(data);
                }
                catch (Exception ex)
                {
                    med.Err(ex);
                    // Set error number/message
                    rets.ErrNum = 9999;
                    rets.ErrMsg = ex.Message;
                }

                return rets;
            }

            #endregion
        }
    }
}
