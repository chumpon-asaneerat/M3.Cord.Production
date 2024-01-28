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
    public class LabelCHS9Item
    {
        #region Public Properties

        public string DIPLotNo { get; set; }
        public int DoffNo { get; set; }
        public string ProductCode { get; set; }
        public int? SPNo { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<LabelCHS9Item>> Gets(string LotNo, int? doffNo, int? SPStart, int? SPEnd)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<LabelCHS9Item>> rets = new NDbResult<List<LabelCHS9Item>>();

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
            p.Add("@LotNo", LotNo);
            p.Add("@DoffNo", doffNo);
            p.Add("@SPStart", SPStart);
            p.Add("@SPEnd", SPEnd);

            try
            {
                var items = cnn.Query<LabelCHS9Item>("GetLabelCHS9Items", p,
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
                rets.data = new List<LabelCHS9Item>();
            }

            return rets;
        }

        #endregion
    }
}
