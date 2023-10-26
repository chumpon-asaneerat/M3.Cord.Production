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
using System.Windows.Controls.Primitives;

#endregion

namespace M3.Cord.Models
{
    public class DIPMaterialCheckSheetItem : NInpc
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        #endregion

        public SolidColorBrush TextColor { get { return BlackColor; } set { } }

        #region Public Properties

        public int MaterialCheckId { get; set; }
        public int SPNo { get; set; }

        public string LotNo { get; set; }
        public int CHNo { get; set; }

        public bool CheckYarnNo { get; set; } = false;
        public bool CheckYanScrap { get; set; } = false;
        public bool CheckYarnBall { get; set; } = false;
        public bool CheckCover { get; set; } = false;
        public bool CheckSensor { get; set; } = false;
        public bool CheckDustFilter { get; set; } = false;

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<DIPMaterialCheckSheetItem>> Gets(int? MaterialCheckId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<DIPMaterialCheckSheetItem>> rets = new NDbResult<List<DIPMaterialCheckSheetItem>>();

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
            p.Add("@MaterialCheckId", MaterialCheckId);
            try
            {
                var items = cnn.Query<DIPMaterialCheckSheetItem>("GetDIPMaterialCheckSheetItems", p,
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
                rets.data = new List<DIPMaterialCheckSheetItem>();
            }

            return rets;
        }
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The DIPMaterialCheckSheetItem to save.</param>
        /// <returns></returns>
        public static NDbResult<DIPMaterialCheckSheetItem> Save(DIPMaterialCheckSheetItem value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<DIPMaterialCheckSheetItem> ret = new NDbResult<DIPMaterialCheckSheetItem>();

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

            p.Add("@MaterialCheckId", value.MaterialCheckId);
            p.Add("@SPNo", value.SPNo);

            p.Add("@LotNo", value.LotNo);
            p.Add("@CHNo", value.CHNo);

            p.Add("@CheckYarnNo", value.CheckYarnNo);
            p.Add("@CheckYanScrap", value.CheckYanScrap);
            p.Add("@CheckYarnBall", value.CheckYarnBall);
            p.Add("@CheckCover", value.CheckCover);
            p.Add("@CheckSensor", value.CheckSensor);
            p.Add("@CheckDustFilter", value.CheckDustFilter);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveDIPMaterialCheckSheetItem", p, commandType: CommandType.StoredProcedure);
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
