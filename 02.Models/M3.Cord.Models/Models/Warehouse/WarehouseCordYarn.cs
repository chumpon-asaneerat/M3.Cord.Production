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
using NLib.Models;

#endregion

namespace M3.Cord.Models
{
    public class WarehouseCordYarn : NInpc
    {
        #region Public Properties

        public int G4IssueYarnPkId { get; set; }

        public DateTime? ReceiveDate { get; set; } = new DateTime?();
        public int? ReceiveBy { get; set; } = new int?();

        public string PalletNo { get; set; }
        public string TraceNo { get; set; }
        public string LotNo { get; set; }

        public string ItemYarn { get; set; }
        public string YarnType { get; set; }
        public string Item400 { get; set; }

        public decimal? WeightQty { get; set; }
        public decimal? ConeCH { get; set; }

        public string PalletType { get; set; }

        public string DeleteFlag { get; set; }
        public string FinishFlag { get; set; }

        public bool WHReceiveFlag { get; set; }
        public DateTime? WHReceiveDate { get; set; }

        public SolidColorBrush TextColor
        {
            get 
            {
                return (!WHReceiveFlag) ? ModelConsts.BlackColor : ModelConsts.RedColor;
            }
            set { }
        }

        public bool IsMark
        {
            get { return WHReceiveFlag; }
            set { }
        }

        #endregion

        #region Public Methods

        public void MarkReceive()
        {
            WHReceiveFlag = true;
            Raise(() => ReceiveDate);
            Raise(() => TextColor);
            Raise(() => IsMark);
        }

        public void UnmarkReceive()
        {
            WHReceiveFlag = false;
            Raise(() => ReceiveDate);
            Raise(() => TextColor);
            Raise(() => IsMark);
        }

        #endregion

        #region Static Methods

        public static NDbResult<List<WarehouseCordYarn>> SearchG4IssueYarns(
            DateTime? issueDate, string itemYarn)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<WarehouseCordYarn>> rets = new NDbResult<List<WarehouseCordYarn>>();

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
            p.Add("@IssueDate", issueDate);
            p.Add("@ItemYarn", itemYarn);
            //p.Add("@Item400", Item400);

            try
            {
                var items = cnn.Query<WarehouseCordYarn>("SearchG4IssueYarns", p,
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
                rets.data = new List<WarehouseCordYarn>();
            }

            return rets;
        }

        public static NDbResult G4IssueYarnReceive(WarehouseCordYarn value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult ret = new NDbResult();

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
            p.Add("@G4IssueYarnPkId", value.G4IssueYarnPkId);
            p.Add("@Receive", value.WHReceiveFlag);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("G4IssueYarnReceive", p,
                    commandType: CommandType.StoredProcedure);
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

        public static NDbResult G4IssueYarnReceive(List<WarehouseCordYarn> values)
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
                    var oRet = G4IssueYarnReceive(yarn);
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

        #endregion
    }
}
