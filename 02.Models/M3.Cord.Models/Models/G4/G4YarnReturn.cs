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
using Newtonsoft.Json.Linq;
using System.Diagnostics;

#endregion

namespace M3.Cord.Models
{
    public class YarnGrade
    {
        public string Grade { get; set; }

        public static List<YarnGrade> Gets()
        {
            return new List<YarnGrade>()
            {
                new YarnGrade { Grade = "A" },
                new YarnGrade { Grade = "C" }
            };
        }
    }

    public class G4YarnReturnSummary
    {
        public decimal TotalPallet { get; set; } = decimal.Zero;
        public decimal TotalWeight { get; set; } = decimal.Zero;
        public decimal TotalCH { get; set; } = decimal.Zero;
    }

    public class G4YarnReturn : NInpc
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets Primary Key.
        /// </summary>
        public int? G4YarnPkId { get; set; }
        /// <summary>
        /// Gets or sets Entry Date.
        /// </summary>
        public DateTime? EntryDate { get; set; }
        /// <summary>
        /// Gets or sets TraceNo (รหัสจาก supplier).
        /// </summary>
        public string TraceNo { get; set; }
        /// <summary>
        /// Gets or sets PalletNo (รหัสแท่นวาง).
        /// </summary>
        public string PalletNo { get; set; }
        /// <summary>
        /// Gets or sets LotNo (รหัส Lot สำหรับอ้างอิงในกระบวนการผลิต).
        /// </summary>
        public string LotNo { get; set; }
        /// <summary>
        /// Gets or sets ItemYarn (รหัสเส้นด้าย).
        /// </summary>
        public string ItemYarn { get; set; }
        /// <summary>
        /// Gets or sets Pallet Type (ประเภท Pallet 'F' อาจจะย่อมาจาก Flat).
        /// </summary>
        public string PalletType { get; set; }
        /// <summary>
        /// Gets or sets Item Code (จากระบบ AS400 or D326).
        /// </summary>
        public string Item400 { get; set; }

        /// <summary>
        /// Gets or sets Cone CH (จำนวนลูกต่อ Pallet)
        /// </summary>
        public decimal? ConeCH { get; set; }
        /// <summary>
        /// Gets or sets Kg Per CH (น้ำหน้ก Kg ต่อลูก)
        /// </summary>
        public decimal? KgPerCH { get; set; }
        /// <summary>
        /// Gets or sets Weight Unit (ปรกติเป็น KG)
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// Gets or sets Weight Qty (น้ำหนักรวม).
        /// </summary>
        public decimal? WeightQty { get; set; }
        /// <summary>
        /// Gets or sets Remain Qty (น้ำหนักที่เหลือ).
        /// </summary>
        public decimal? RemainQty { get; set; }

        /// <summary>
        /// Gets or sets Received Date.
        /// </summary>
        public DateTime? ReceiveDate { get; set; }
        /// <summary>
        /// Gets or sets Received By.
        /// </summary>
        public int? ReceiveBy { get; set; }
        /// <summary>
        /// Gets or sets Full Name.
        /// </summary>
        public string ReceiveByFullName { get; set; }
        /// <summary>
        /// Gets or sets User Name.
        /// </summary>
        public string ReceiveByUserName { get; set; }
        /// <summary>
        /// Gets or sets Update Date (not used).
        /// </summary>
        public DateTime? UpdateDate { get; set; }
        /// <summary>
        /// Gets or sets Movement Date (not used).
        /// </summary>
        public DateTime? MovementDate { get; set; }

        /// <summary>
        /// Gets or sets Expired Date.
        /// </summary>
        public DateTime? ExpiredDate { get; set; }
        /// <summary>
        /// Gets or sets Direction No.
        /// </summary>
        public string DirectionNo { get; set; }

        /// <summary>
        /// Gets or sets Finish Flag.
        /// </summary>
        public bool? FinishFlag { get; set; }
        /// <summary>
        /// Gets or sets Update Flag.
        /// </summary>
        public bool? UpdateFlag { get; set; }
        /// <summary>
        /// Gets or sets Delete Flag.
        /// </summary>
        public bool? DeleteFlag { get; set; }

        public string From
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ItemYarn))
                {
                    return string.Empty;
                }
                else
                {
                    // Gets From from itemcode table
                    if (ItemYarn.Contains("OKA"))
                    {
                        return "Japan";
                    }
                    else
                    {
                        return "Thailand";
                    }
                }
            }
            set { }
        }

        /// <summary>
        /// Gets or sets is item selected (for GO Label print).
        /// </summary>
        public bool Selected
        {
            get { return this.Get<bool>(); }
            set
            {
                this.Set(value, () =>
                {
                    if (null != OnSelectedChanged) OnSelectedChanged(value);
                });
            }
        }

        public Action<bool> OnSelectedChanged { get; set; }

        public int RowNo { get; set; }
        public string NewTraceNo { get; set; }
        public string Operator { get; set; }
        public string Grade { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Get yarn in stocks by trace no.
        /// </summary>
        /// <param name="traceNo"></param>
        /// <param name="ItemYarn"></param>
        /// <returns></returns>
        public static NDbResult<List<G4YarnReturn>> SearchYarnStocks(string traceNo, string ItemYarn = null)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<G4YarnReturn>> rets = new NDbResult<List<G4YarnReturn>>();

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
            p.Add("@TraceNo", traceNo);
            p.Add("@ItemYarn", ItemYarn);

            try
            {
                var items = cnn.Query<G4YarnReturn>("SearchG4YarnStockByTraceNo", p,
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

            return rets;
        }

        #endregion
    }
}
