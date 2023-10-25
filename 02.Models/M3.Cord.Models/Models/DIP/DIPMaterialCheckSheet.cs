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
    public class DIPMaterialCheckSheet : NInpc
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        #endregion

        public SolidColorBrush TextColor { get { return BlackColor; } set { } }

        #region Public Properties

        public int? MaterialCheckId { get; set; }
        public int? DIPPCId { get; set; }
        public DateTime CheckDate { get; set; }

        public string MCCode { get; set; }

        public int DoffNo { get; set; }

        public string ShiftName { get; set; }
        public string UserName { get; set; }

        public List<DIPMaterialCheckSheetItem> Items { get; set; }

        #endregion

        #region Public Methods

        public void LoadItems()
        {
            if (MaterialCheckId.HasValue && MaterialCheckId.Value > 0)
            {
                this.Items = DIPMaterialCheckSheetItem.Gets(MaterialCheckId.Value).Value();
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<DIPMaterialCheckSheet>> Gets(int DIPPCId,
            int? MaterialCheckId = new int?())
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<DIPMaterialCheckSheet>> rets = new NDbResult<List<DIPMaterialCheckSheet>>();

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
            p.Add("@DIPPCId", DIPPCId);
            p.Add("@MaterialCheckId", MaterialCheckId);
            try
            {
                var items = cnn.Query<DIPMaterialCheckSheet>("GetDIPMaterialCheckSheets", p,
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
                rets.data = new List<DIPMaterialCheckSheet>();
            }

            return rets;
        }
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The DIPMaterialCheckSheet item to save.</param>
        /// <returns></returns>
        public static NDbResult<DIPMaterialCheckSheet> Save(DIPMaterialCheckSheet value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<DIPMaterialCheckSheet> ret = new NDbResult<DIPMaterialCheckSheet>();

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

            p.Add("@DIPPCId", value.DIPPCId);
            p.Add("@CheckDate", value.CheckDate);
            p.Add("@MCCode", value.MCCode);
            p.Add("@DoffNo", value.DoffNo);
            p.Add("@ShiftName", value.ShiftName);
            p.Add("@UserName", value.UserName);

            p.Add("@MaterialCheckId", value.MaterialCheckId, DbType.Int32, direction: ParameterDirection.InputOutput);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveDIPMaterialCheckSheet", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);

                // get pk
                value.MaterialCheckId = p.Get<int?>("@MaterialCheckId");

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
