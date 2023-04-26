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

#endregion

namespace M3.Cord.Models
{
    public class G4Yarn : NInpc
    {
        #region Public Properties

        public int? PkId { get; set; }
        public DateTime? EntryDate { get; set; }
        public string ItemYarn { get; set; }
        public string PalletNo { get; set; }
        public string YarnType { get; set; }
        public decimal? WeightQty { get; set; }
        public string LotNo { get; set; }
        public string TraceNo { get; set; }
        public decimal? KgPerCH { get; set; }
        public decimal? ConeCH { get; set; }
        public string PalletType { get; set; }
        public string Item400 { get; set; }
        public string Unit { get; set; }
        public decimal? RemainQty { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public string ReceiveBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? MovementDate { get; set; }
        public bool? Verify { get; set; }

        public bool? Packing { get; set; } = new bool?();
        public bool? Clean { get; set; } = new bool?();
        public bool? Tearing { get; set; } = new bool?();
        public bool? Falldown { get; set; } = new bool?();
        public bool? Certification { get; set; } = new bool?();
        public bool? Invoice { get; set; } = new bool?();
        public bool? IdentifyArea { get; set; } = new bool?();
        public bool? AmountPallet { get; set; } = new bool?();

        public string Other { get; set; }
        public string Action { get; set; }

        public bool? FinishFlag { get; set; }
        public bool? DeleteFlag { get; set; }


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

        public bool IsOK
        {
            get { return (Verify.HasValue && Verify.Value); }
            set { }
        }
        public bool IsNG
        {
            get { return (Verify.HasValue && !Verify.Value); }
            set { }
        }

        #endregion

        #region Static Methods

        public static NDbResult<G4Yarn> Save(G4Yarn value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<G4Yarn> ret = new NDbResult<G4Yarn>();

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
            p.Add("@ItemYarn", value.ItemYarn);
            p.Add("@PalletNo", value.PalletNo);
            p.Add("@YarnType", value.YarnType);
            p.Add("@WeightQty", value.WeightQty);
            p.Add("@LotNo", value.LotNo);
            p.Add("@TraceNo", value.TraceNo);
            p.Add("@KgPerCH", value.KgPerCH);
            p.Add("@ConeCH", value.ConeCH);
            p.Add("@PalletType", value.PalletType);
            p.Add("@Item400", value.Item400);
            p.Add("@Unit", value.Unit);
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
            p.Add("@DeleteFlag", value.DeleteFlag);
            p.Add("@PkId", value.PkId, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveG4Yarn", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);
                // Set PK
                value.PkId = p.Get<int>("@PkId");
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

        #endregion
    }
}
