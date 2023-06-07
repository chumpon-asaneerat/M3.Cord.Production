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

        public int CordYarnPkId { get; set; }

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

        public SolidColorBrush TextColor
        {
            get 
            {
                return (!ReceiveDate.HasValue) ? ModelConsts.BlackColor : ModelConsts.RedColor;
            }
            set { }
        }

        public bool IsMark
        {
            get { return ReceiveDate.HasValue; }
            set { }
        }

        #endregion

        #region Public Methods

        public void MarkReceive(DateTime? receiveDate)
        {
            ReceiveDate = receiveDate;
            Raise(() => ReceiveDate);
            Raise(() => TextColor);
            Raise(() => IsMark);
        }

        public void UnmarkReceive()
        {
            ReceiveDate = new DateTime?();
            Raise(() => ReceiveDate);
            Raise(() => TextColor);
            Raise(() => IsMark);
        }

        #endregion

        #region Static Methods
        /*
        public static NDbResult<List<WarehouseCordYarn>> GetCordYarns()
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
            p.Add("@IssueDate", IssueDate);
            p.Add("@ItemYarn", ItemYarn);
            p.Add("@Item400", Item400);

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
        */
        #endregion
    }
}
