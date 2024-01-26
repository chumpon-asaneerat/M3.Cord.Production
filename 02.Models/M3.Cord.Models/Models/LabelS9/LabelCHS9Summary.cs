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
using System.ComponentModel;

#endregion

using BarcodeLib;

namespace M3.Cord.Models
{
    public class LabelCHS9Summary
    {
        #region Public Properties

        public int? DIPPCId { get; set; }
        public string CustomerName { get; set; }
        public string ProductCode { get; set; }
        public string ItemYarn { get; set; }
        public string MCCode { get; set; }
        public int? AppearId { get; set; }

        public int? SPStart { get; set; }
        public int? SPEnd { get; set; }
        public int? TotalGood { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Search
        /// </summary>
        /// <returns></returns>
        public static NDbResult<LabelCHS9Summary> Search(string lotNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<LabelCHS9Summary> ret = new NDbResult<LabelCHS9Summary>();

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
            p.Add("@LotNo", lotNo);

            try
            {
                var item = cnn.Query<LabelCHS9Summary>("GetLabelCHS9Summary", p,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                var data = item;

                ret.Success(data);
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
