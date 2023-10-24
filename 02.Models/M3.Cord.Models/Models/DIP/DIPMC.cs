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

#endregion

namespace M3.Cord.Models
{
    public class DIPMC : NInpc
    {
        #region Public Properties

        public string MCCode { get; set; }
        public string ProcessName { get; set; }
        public int StartCore { get; set; }
        public int EndCore { get; set; }

        #endregion

        #region Static Methods

        public static NDbResult<List<DIPMC>> Gets()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<DIPMC>> rets = new NDbResult<List<DIPMC>>();

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

            try
            {
                var items = cnn.Query<DIPMC>("GetDIPMCs", p,
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
                rets.data = new List<DIPMC>();
            }

            return rets;
        }

        #endregion
    }
}
