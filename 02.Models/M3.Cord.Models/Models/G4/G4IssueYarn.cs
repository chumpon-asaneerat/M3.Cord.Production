#define ENABLE_SEND400

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
using System.Diagnostics;
using NLib.Models;
using Newtonsoft.Json.Linq;
using System.Drawing.Drawing2D;
using System.Globalization;
using static NLib.IO.Folders;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Media.Media3D;
using System.Windows;
using System.Threading;

#endregion

namespace M3.Cord.Models
{
    public class G4IssueYarn : NInpc
    {
        #region Public Properties

        public int G4IssueYarnPkId { get; set; }
        public string RequestNo { get; set; }
        public DateTime? IssueDate { get; set; }
        public string IssueBy { get; set; }
        public string PalletNo { get; set; }
        public string TraceNo { get; set; }
        public decimal? WeightQty { get; set; }
        public decimal? ConeCH { get; set; }
        public string PalletType { get; set; }
        public string Remark { get; set; }
        public DateTime? EditDate { get; set; }
        public int? EditBy { get; set; } = new int?();
        public bool DeleteFlag { get; set; }
        public bool FinishFlag { get; set; }
        public bool WHReceiveFlag { get; set; }

        // FROM G4YARN
        public int G4YarnPkId { get; set; }
        public DateTime? EntryDate { get; set; }
        public string LotNo { get; set; }
        public string ItemYarn { get; set; }
        public string YarnType { get; set; }
        public string Item400 { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public int? ReceiveBy { get; set; }

        public string DirectionNo { get; set; }

        /// <summary>
        /// Gets or sets Expired Date.
        /// </summary>
        public DateTime? ExpiredDate { get; set; }

        public bool Verify { get; set; }

        public SolidColorBrush TextColor
        {
            get 
            {
                return (string.IsNullOrEmpty(RequestNo)) ? ModelConsts.BlackColor : ModelConsts.RedColor;
            }
            set { }
        }

        public bool IsMark
        {
            get { return !string.IsNullOrEmpty(RequestNo); }
            set { }
        }

        public bool CanRemove
        {
            get { return G4IssueYarnPkId == 0; }
            set { }
        }

        #endregion

        #region Public Methods

        public void MarkIssue(string requestId, string issueBy, 
            //string issueTo, 
            DateTime? issueDate)
        {
            RequestNo = requestId;
            IssueBy = issueBy;
            //IssueTo = issueTo;
            IssueDate = issueDate;

            Raise(() => this.IsMark);
            Raise(() => this.TextColor);
        }

        public void UnmarkIssue()
        {
            RequestNo = null;
            IssueBy = null;
            //IssueTo = null;
            IssueDate = new DateTime?();

            Raise(() => this.IsMark);
            Raise(() => this.TextColor);
        }

        #endregion

        #region Static Methods

        public static NDbResult<List<G4IssueYarn>> GetG4IssueYarns(string RequestNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<G4IssueYarn>> rets = new NDbResult<List<G4IssueYarn>>();

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
            p.Add("@RequestNo", RequestNo);

            try
            {
                var items = cnn.Query<G4IssueYarn>("GetG4IssueYarns", p,
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
                rets.data = new List<G4IssueYarn>();
            }

            return rets;
        }

        public static NDbResult Save(G4IssueYarn value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<G4IssueYarn> ret = new NDbResult<G4IssueYarn>();

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
            p.Add("@RequestNo", value.RequestNo);
            p.Add("@PalletNo", value.PalletNo);
            p.Add("@TraceNo", value.TraceNo);
            p.Add("@WeightQty", value.WeightQty);
            p.Add("@ConeCH", value.ConeCH);
            p.Add("@PalletType", value.PalletType);
            p.Add("@IssueDate", value.IssueDate);
            p.Add("@IssueBy", value.IssueBy);

            //p.Add("@G4IssueYarnPkId", value.G4IssueYarnPkId, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("G4InsertUpdateIssueYarn", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);
                // Set PK
                //value.G4IssueYarnPkId = p.Get<int>("@G4IssueYarnPkId");
                // Set error number/message
                ret.ErrNum = p.Get<int>("@errNum");
                ret.ErrMsg = p.Get<string>("@errMsg");

                // success then send to AS400
                if (ret.ErrNum == 0)
                {
                    SendIssueToAS400(value);
                }
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

        public static void SendIssueToAS400(G4IssueYarn value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            if (null == value)
                return;

#if ENABLE_SEND400
            string dIssue = value.IssueDate.Value.ToString("yyyyMMdd",
                CultureInfo.CreateSpecificCulture("en-US"));
            string dSend = DateTime.Now.ToString("yyyyMMddHHmm",
                CultureInfo.CreateSpecificCulture("en-US"));

            double defPound = 2.2046;
            string sPound = null;
            if (value.WeightQty.HasValue)
            {
                decimal? dPound = Utils.MathEx.Round(
                    Convert.ToDecimal(Convert.ToDouble(value.WeightQty.Value) * (double)defPound), 2);
                sPound = dPound.ToString();
            }

            BCSPRFTP item = new BCSPRFTP();

            item.ANNUL = string.Empty;
            item.CDSTO = "3G";
            item.FLAGS = "R";
            item.RECTY = "S";
            item.USRNM = "PGMR";
            item.CDTRA = "SB"; // The ISSUETO is SB

            item.DTTRA = dIssue;
            item.DTINP = dIssue;

            item.CDUM0 = "KG";
            item.LOCAT = "GD";
            item.CDQUA = "1";
            item.COMM0 = string.Empty;
            item.QUACA = string.Empty;

            item.REFER = !string.IsNullOrEmpty(value.RequestNo) ? value.RequestNo.Trim() : null;

            item.CDEL0 = !string.IsNullOrEmpty(value.PalletNo) ? value.PalletNo.Trim() : null;
            item.CDCON = !string.IsNullOrEmpty(value.PalletNo) ? value.PalletNo.Trim() : null;
            item.BLELE = (value.WeightQty.HasValue) ? value.WeightQty.Value.ToString() : null;
            item.CDKE1 = !string.IsNullOrEmpty(value.Item400) ? value.Item400.Trim() : null;
            item.CDKE2 = string.Empty;
            item.CDKE3 = string.Empty;
            item.CDKE4 = string.Empty;
            item.CDKE5 = string.Empty;
            item.CDLOT = !string.IsNullOrEmpty(value.LotNo) ? value.LotNo.Trim() : null;

            item.TECU1 = sPound;
            item.TECU2 = sPound;
            item.TECU3 = (value.ConeCH.HasValue) ? value.ConeCH.Value.ToString() : null;
            item.TECU4 = (value.WeightQty.HasValue) ? value.WeightQty.Value.ToString() : null;
            item.TECU5 = !string.IsNullOrEmpty(value.DirectionNo) ? value.DirectionNo.Trim() : null;
            item.TECU6 = !string.IsNullOrEmpty(value.TraceNo) ? value.TraceNo.Trim() : null;

            item.DTORA = dSend;

            bool success = BCSPRFTP.AS400.Issue(item);
            if (success)
            {
                med.Info("Send To AS400 success.");
            }
            else
            {
                med.Info("Send To AS400 failed.");
            }
#endif
        }

        public static NDbResult Save(List<G4IssueYarn> values)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult ret = new NDbResult();

            if (null == values)
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

            try
            {
                int iErrCnt = 0;
                foreach (var yarn in values)
                {
                    var oRet = Save(yarn);
                    if (null == oRet || oRet.HasError) // something error.
                    {
                        ++iErrCnt;
                    }
                }

                if (iErrCnt == 0)
                {
                    ret.Success();
                }
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

        public static NDbResult<List<G4IssueYarn>> SearchG4IssueYarns(
            DateTime? IssueDate = new DateTime?(), 
            string ItemYarn = null, 
            string Item400 = null)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<G4IssueYarn>> rets = new NDbResult<List<G4IssueYarn>>();

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
            p.Add("@IssueDate", IssueDate);
            p.Add("@ItemYarn", ItemYarn);
            p.Add("@Item400", Item400);

            try
            {
                var items = cnn.Query<G4IssueYarn>("SearchG4IssueYarns", p,
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
                rets.data = new List<G4IssueYarn>();
            }

            return rets;
        }

        public static NDbResult<G4IssueYarn> SearchWarehousePallet(string PalletNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<G4IssueYarn> rets = new NDbResult<G4IssueYarn>();

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
            p.Add("@PalletNo", PalletNo);

            try
            {
                var items = cnn.Query<G4IssueYarn>("SearchWarehousePallet", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList().FirstOrDefault() : null;
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
                rets.data = null;
            }

            return rets;
        }

        #endregion
    }
}
