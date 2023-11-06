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
using System.Windows.Markup;

namespace M3.Cord.Models
{
    public enum DIPPalletStatus : int
    {
        All = -1,
        Create = 0
    }

    public class DIPPalletSlip : NInpc
    {
        private static Barcode BarcodeGenerator = null;

        static DIPPalletSlip()
        {
            BarcodeGenerator = new Barcode();
            BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
            BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
            BarcodeGenerator.IncludeLabel = false;
        }

        #region Constructor

        public DIPPalletSlip() : base()
        {
            //Items = new List<PalletSettingItem>();
        }

        #endregion

        #region Public Properties

        public int? PalletId { get; set; }
        public int? DIPPCId { get; set; }
        public string PalletCode { get; set; }
        // Pallet barcode
        public byte[] PalletCodeImage
        {
            get
            {
                byte[] results = null;
                if (!string.IsNullOrWhiteSpace(PalletCode))
                {
                    System.Drawing.Image img = BarcodeGenerator.Encode(BarcodeGenerator.EncodedType,
                        PalletCode, 400, 100);

                    results = NLib.Utils.ImageUtils.GetImage(img);
                }
                return results;
            }
            set { }
        }

        public DateTime? CreateDate { get; set; }
        public string TwistNo { get; set; }
        public int Counter { get; set; }
        public string UserName { get; set; }
        public DateTime? PCDate { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ItemYarn { get; set; }
        public string CordStructure { get; set; }
        public string TreatRoute { get; set; }
        public string TwistSpec { get; set; }

        public string MCCode 
        {
            get 
            {
                // change S7 to S8
                return (string.IsNullOrWhiteSpace(S7MCCode)) ? string.Empty : S7MCCode.Replace("7", "8"); 
            }
            set { }
        }

        // runtime properties
        public string ProductLotNo 
        {
            get { return DIPLotNo; }
            set { }
        }
        // runtime properties
        public string DoffNos 
        {
            get 
            { 
                return string.IsNullOrWhiteSpace(DoffNo) ? string.Empty : DoffNo.ToString(); 
            }
            set { }
        }

        public decimal? ActualQty { get; set; }
        public decimal? ActualWeight { get; set; }

        public DIPPalletStatus PalletStatus { get; set; }

        public string PalletStatusText
        {
            get
            {
                if (PalletStatus == DIPPalletStatus.Create)
                {
                    return "DIP";
                }
                else
                {
                    return "Create";
                }
            }
            set { }
        }

        // wrapper
        public string DIPLotNo { get; set; }
        public string DoffNo { get; set; }
        public string S7MCCode { get; set; }

        public SolidColorBrush TextColor
        {
            get
            {
                return ModelConsts.BlackColor;
            }
            set { }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Search
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<DIPPalletSlip>> Search(string dipLotNo,
            DateTime? begin, DateTime? end, string productCode,
            DIPPalletStatus palletStatus)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<DIPPalletSlip>> rets = new NDbResult<List<DIPPalletSlip>>();

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

            int? status = new int?();
            if (palletStatus != DIPPalletStatus.All)
            {
                status = new int?((int)palletStatus);
            }

            var p = new DynamicParameters();
            p.Add("@DIPLotNo", dipLotNo);
            p.Add("@beginDate", begin);
            p.Add("@endDate", end);
            p.Add("@ProductCode", productCode);
            p.Add("@PalletStatus", status);

            try
            {
                var items = cnn.Query<DIPPalletSlip>("SearchDIPPalletSlips", p,
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
                rets.data = new List<DIPPalletSlip>();
            }

            return rets;
        }
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The DIPPalletSlip to save.</param>
        /// <returns></returns>
        public static NDbResult<DIPPalletSlip> Save(DIPPalletSlip value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<DIPPalletSlip> ret = new NDbResult<DIPPalletSlip>();

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
            p.Add("@PalletCode", value.PalletCode);
            p.Add("@CreateDate", value.CreateDate);
            p.Add("@TwistNo", value.TwistNo);
            p.Add("@Counter", value.Counter);
            
            p.Add("@ActualQty", value.ActualQty);
            p.Add("@ActualWeight", value.ActualWeight);

            p.Add("@UserName", value.UserName);
            p.Add("@PalletStatus", value.PalletStatus);

            p.Add("@PalletId", value.PalletId, DbType.Int32, direction: ParameterDirection.InputOutput);
            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveDIPPalletSlip", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);

                // Set PK
                value.PalletId = p.Get<int?>("@PalletId");
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

        /*
        public static List<DIPPalletSlip> GetSamples()
        {
            var rets = new List<DIPPalletSlip>();
            rets.Add(new DIPPalletSlip
            {
                ActualQty = 126,
                ActualWeight = decimal.Parse("5.12"),
                CordStructure = @"P123//22/22",
                Counter = 29000,
                CreateDate = DateTime.Now,
                CustomerName = "BANDO",
                MCCode = "S-8-1",
                DoffNos = "D2",
                ItemYarn = "123-456-789-0",
                PalletCode = "P-2023-XXX-1",
                ProductLotNo = "293019A",
                ProductName = "9H3099AA",
                UserName = "Administrator User",
                TwistNo = "222",
                TwistSpec = "4444"
            });
            return rets;
        }
        */
    }
}
