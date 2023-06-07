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
    public class G4Yarn : NInpc
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
        /// Gets or sets Yarn Type (ประเภทการนำไปทอเป็นผ้า Warp, Warp).
        /// </summary>
        public string YarnType { get; set; }
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
        /// Gets or sets is Verify.
        /// </summary>
        public bool? Verify { get; set; }
        /// <summary>
        /// Gets or sets has Packing problem.
        /// </summary>
        public bool Packing { get; set; }
        /// <summary>
        /// Gets or sets has Clean problem.
        /// </summary>
        public bool Clean { get; set; }
        /// <summary>
        /// Gets or sets has Tearing problem.
        /// </summary>
        public bool Tearing { get; set; }
        /// <summary>
        /// Gets or sets has Falldown problem.
        /// </summary>
        public bool Falldown { get; set; }
        /// <summary>
        /// Gets or sets has Certification problem.
        /// </summary>
        public bool Certification { get; set; }
        /// <summary>
        /// Gets or sets has Invoice problem.
        /// </summary>
        public bool Invoice { get; set; }
        /// <summary>
        /// Gets or sets has Identify Area problem.
        /// </summary>
        public bool IdentifyArea { get; set; }
        /// <summary>
        /// Gets or sets has Amount Pallet problem.
        /// </summary>
        public bool AmountPallet { get; set; }
        /// <summary>
        /// Gets or sets Other (defect).
        /// </summary>
        public string Other { get; set; }
        /// <summary>
        /// Gets or sets Action (defect).
        /// </summary>
        public string Action { get; set; }

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

        /// <summary>
        /// Gets or sets OK flag.
        /// </summary>
        public bool OK
        {
            get { return (Verify.HasValue && Verify.Value); }
            set
            {
                if (value)
                {
                    Verify = true;
                }
                Raise(() => OK);
                Raise(() => NG);
            }
        }
        /// <summary>
        /// Gets or sets NG flag.
        /// </summary>
        public bool NG
        {
            get { return (Verify.HasValue && !Verify.Value); }
            set
            {
                if (value)
                {
                    Verify = false;
                }
                Raise(() => OK);
                Raise(() => NG);
            }
        }

        /// <summary>
        /// Gets Is OK.
        /// </summary>
        public bool IsOK
        {
            get { return (Verify.HasValue && Verify.Value); }
            set { }
        }
        /// <summary>
        /// Gets Is NG.
        /// </summary>
        public bool IsNG
        {
            get { return (Verify.HasValue && !Verify.Value); }
            set { }
        }

        public SolidColorBrush TextColor
        {
            get { return ModelConsts.BlackColor; }
            set { }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The G4Yarn item to save.</param>
        /// <returns></returns>
        public static NDbResult<G4Yarn> Save(G4Yarn value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<G4Yarn> ret = new NDbResult<G4Yarn>();

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
            p.Add("@EntryDate", value.EntryDate);
            p.Add("@TraceNo", value.TraceNo);
            p.Add("@PalletNo", value.PalletNo);
            p.Add("@LotNo", value.LotNo);

            p.Add("@ItemYarn", value.ItemYarn);
            p.Add("@YarnType", value.YarnType);
            p.Add("@PalletType", value.PalletType);
            p.Add("@Item400", value.Item400);

            p.Add("@ConeCH", value.ConeCH);
            p.Add("@KgPerCH", value.KgPerCH);
            p.Add("@Unit", value.Unit);
            p.Add("@WeightQty", value.WeightQty);
            p.Add("@RemainQty", value.RemainQty);

            p.Add("@ReceiveDate", value.ReceiveDate);
            p.Add("@ReceiveBy", value.ReceiveBy);
            p.Add("@UpdateDate", value.UpdateDate);
            p.Add("@MovementDate", value.MovementDate);

            p.Add("@Verify", value.Verify);
            p.Add("@Packing", value.Packing);
            p.Add("@Clean", value.Clean);
            p.Add("@Tearing", value.Tearing);
            p.Add("@Falldown", value.Falldown);
            p.Add("@Certification", value.Certification);
            p.Add("@Invoice", value.Invoice);
            p.Add("@IdentifyArea", value.IdentifyArea);
            p.Add("@AmountPallet", value.AmountPallet);
            p.Add("@Other", value.Other);
            p.Add("@Action", value.Action);
            p.Add("@FinishFlag", value.FinishFlag);
            p.Add("@UpdateFlag", value.UpdateFlag);
            p.Add("@DeleteFlag", value.DeleteFlag);
            p.Add("@G4YarnPkId", value.G4YarnPkId, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveG4Yarn", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);
                // Set PK
                value.G4YarnPkId = p.Get<int>("@G4YarnPkId");
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
        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<G4Yarn>> Gets()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<G4Yarn>> rets = new NDbResult<List<G4Yarn>>();

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

            try
            {
                var items = cnn.Query<G4Yarn>("GetG4Yarns", p,
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
                rets.data = new List<G4Yarn>();
            }

            return rets;
        }
        /// <summary>
        /// Get by Trace No.
        /// </summary>
        /// <returns></returns>
        public static NDbResult<G4Yarn> Get(string traceNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<G4Yarn> rets = new NDbResult<G4Yarn>();

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

            try
            {
                var items = cnn.Query<G4Yarn>("GetG4Yarns", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.FirstOrDefault() : null;
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
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="values">The list of G4Yarn item to save.</param>
        /// <returns></returns>
        public static NDbResult Save(List<G4Yarn> values)
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
        /// <summary>
        /// Get yarn in stocks.
        /// </summary>
        /// <param name="ItemYarn"></param>
        /// <param name="EntryDate"></param>
        /// <param name="YarnType"></param>
        /// <returns></returns>
        public static NDbResult<List<G4Yarn>> SearchYarnStocks(string ItemYarn, 
            DateTime? EntryDate = new DateTime?(),
            string YarnType = null)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<G4Yarn>> rets = new NDbResult<List<G4Yarn>>();

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
            p.Add("@ItemYarn", ItemYarn);
            p.Add("@EntryDate", EntryDate);
            p.Add("@YarnType", YarnType);

            try
            {
                var items = cnn.Query<G4Yarn>("SearchG4YarnStock", p,
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
