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
using BarcodeLib;

#endregion

namespace M3.Cord.Models
{
    public class Solution : NInpc
    {
        #region Public Proeprties

        public string SolutionName { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Search By Product Code.
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<Solution>> SearchByProductCode(string productcode = null)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<Solution>> rets = new NDbResult<List<Solution>>();

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
            p.Add("@productcode", productcode);

            try
            {
                var items = cnn.Query<Solution>("ChGetSolutionByItem", p,
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
                rets.data = new List<Solution>();
            }

            return rets;
        }
        /// <summary>
        /// Search By Lot No.
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<Solution>> SearchByLotNo(string solutionlot = null)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<Solution>> rets = new NDbResult<List<Solution>>();

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
            p.Add("@solutionlot", string.IsNullOrWhiteSpace(solutionlot) ? null : solutionlot);
            try
            {
                var items = cnn.Query<Solution>("ChSearchSolutionLot", p,
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
                rets.data = new List<Solution>();
            }

            return rets;
        }

        #endregion
    }
}
