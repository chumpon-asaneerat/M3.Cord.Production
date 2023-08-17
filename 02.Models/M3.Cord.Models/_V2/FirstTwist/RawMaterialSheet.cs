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
using System.Windows.Controls.Primitives;

#endregion

namespace M3.Cord.Models
{
    public class RawMaterialSheet : NInpc
    {
        #region Public Properties

        public int RawMaterialSheetId { get; set; }
        public string MCCode { get; set; }
        public int CordProductPkId { get; set; }
        public string ProductLotNo { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }

        public string ItemYarn { get; set; }
        public string ItemCode { get; set; }
        public string Item400 { get; set; }

        public string Color { get; set; }

        public decimal? TargetQty { get; set; }
        public decimal? ActualQty { get; set; }

        public bool DeleteFlag { get; set; }
        public bool FinishFlag { get; set; }

        public SolidColorBrush TextColor
        {
            get { return ModelConsts.BlackColor; }
            set { }
        }

        #endregion

        #region Static Methods

        public static NDbResult<RawMaterialSheet> Get(string mcCode)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<RawMaterialSheet> ret = new NDbResult<RawMaterialSheet>();

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
            p.Add("@MCCode", mcCode);

            try
            {
                var items = cnn.Query<RawMaterialSheet>("GetRawMaterialSheet", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.FirstOrDefault() : null;
                ret.Success(data);
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
            }

            if (null == ret.data)
            {
                // create empty list.
                ret.data = null;
            }

            return ret;
        }

        /// <summary>
        /// AddNew
        /// </summary>
        /// <param name="value">The RawMaterialSheet item to save.</param>
        /// <returns></returns>
        public static NDbResult AddNew(FirstTwistMC mc, CordProduct product)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult ret = new NDbResult();

            if (null == mc || null == product)
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
            p.Add("@MCCode", mc.MCCode);
            p.Add("@CordProductPkId", product.CordProductPkId);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("NewRawMaterialSheet", p, commandType: CommandType.StoredProcedure);
                ret.Success();
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

    public class RawMaterialSheetItem : NInpc
    {
        #region Public Properties

        public int RawMaterialSheetId { get; set; }
        public int Seq { get; set; }

        public DateTime? ProductionDate { get; set; } = DateTime.Now;
        public string ProductLotNo { get; set; }
        public string ItemYarn { get; set; }
        public string PalletNo { get; set; }
        public string TraceNo { get; set; }
        
        public int InputCH { get; set; }
        public string DoffNos { get; set; }
        public string SPNos { get; set; }

        public bool DeleteFlag { get; set; }
        public bool FinishFlag { get; set; }

        public SolidColorBrush TextColor
        {
            get { return ModelConsts.BlackColor; }
            set { }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<RawMaterialSheetItem>> Gets(int rawMaterialSheetId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<RawMaterialSheetItem>> rets = new NDbResult<List<RawMaterialSheetItem>>();

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
            p.Add("@RawMaterialSheetId", rawMaterialSheetId);

            try
            {
                var items = cnn.Query<RawMaterialSheetItem>("GetRawMaterialSheetItems", p,
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
                rets.data = new List<RawMaterialSheetItem>();
            }

            return rets;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The RawMaterialSheetItem to save.</param>
        /// <returns></returns>
        public static NDbResult<RawMaterialSheetItem> Save(RawMaterialSheetItem value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<RawMaterialSheetItem> ret = new NDbResult<RawMaterialSheetItem>();

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
            p.Add("@RawMaterialSheetId", value.RawMaterialSheetId);
            p.Add("@Seq", null, DbType.Int32, direction: ParameterDirection.InputOutput);

            p.Add("@ProductionDate", value.ProductionDate);
            p.Add("@ItemYarn", value.ItemYarn);
            p.Add("@PalletNo", value.PalletNo);
            p.Add("@TraceNo", value.TraceNo);
            p.Add("@InputCH", value.InputCH);
            p.Add("@DoffNos", value.DoffNos);
            p.Add("@SPNos", value.SPNos);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveRawMaterialSheetItem", p, commandType: CommandType.StoredProcedure);
                // Set PK
                value.Seq = p.Get<int>("@Seq");
                // Set error number/message
                ret.ErrNum = p.Get<int>("@errNum");
                ret.ErrMsg = p.Get<string>("@errMsg");

                ret.Success(value);
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
