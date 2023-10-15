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

namespace M3.Cord.Models
{
    public enum PalletType : int
    {
        Pallet = 0,
        Cart = 1
    }

    public enum PalletStatus : int
    {
        Create = 0,
        Ageing = 1,
        Dip = 2
    }

    public class PalletSetting : NInpc
    {
        private static Barcode BarcodeGenerator = null;

        static PalletSetting()
        {
            BarcodeGenerator = new Barcode();
            BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
            BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
            BarcodeGenerator.IncludeLabel = false;
        }

        #region Constructor

        public PalletSetting() : base()
        {
            Items = new List<PalletSettingItem>();
        }

        #endregion

        #region Public Properties

        public int? PalletId { get; set; }
        public int? PCTwist1Id { get; set; }
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
                        PalletCode, 400, 160);

                    results = NLib.Utils.ImageUtils.GetImage(img);
                }
                return results;
            }
            set { }
        }

        public DateTime? CreateDate { get; set; }
        public string TwistNo { get; set; }
        public int Counter { get; set; }
        public string Mts { get; set; }
        // Create UserId
        public int UserId { get; set; }
        // Runtime data
        public string UserName { get; set; }
        public PalletType PalletType { get; set; }
        public PalletStatus PalletStatus { get; set; }

        public int? PCId { get; set; }
        public DateTime? PCDate { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ItemYarn { get; set; }
        public string CordStructure { get; set; }
        public string TreatRoute { get; set; }
        public string TwistSpec { get; set; }

        public string ProductLotNo { get; set; }
        public string MCCode { get; set; }

        public string DoffNos { get; set; }

        public decimal? ActualQty { get; set; }
        public decimal? TargetQty { get; set; }

        public bool IsCart
        {
            get { return PalletType == PalletType.Cart; }
            set 
            { 
                if (value)
                {
                    PalletType = PalletType.Cart;
                }
                else
                {
                    PalletType = PalletType.Pallet;
                }
                Raise(() => this.PalletType);
                Raise(() => this.IsCart);
                Raise(() => this.IsPallet);
            }
        }

        public bool IsPallet
        {
            get { return PalletType == PalletType.Pallet; }
            set 
            {
                if (value)
                {
                    PalletType = PalletType.Pallet;
                }
                else
                {
                    PalletType = PalletType.Cart;
                }
                Raise(() => this.PalletType);
                Raise(() => this.IsCart);
                Raise(() => this.IsPallet);
            }
        }

        public SolidColorBrush TextColor
        {
            get
            {
                return ModelConsts.BlackColor;
            }
            set { }
        }

        public decimal? TotalCH { get; set; }

        public List<PalletSettingItem> Items { get; set; }

        #endregion

        #region Public Methods

        public void Calculate()
        {
            if (null != Items)
            {
                decimal totalCH = decimal.Zero;
                string doffStr = string.Empty;

                int cnt = 0;
                int max = Items.Count;
                foreach (var item in Items)
                {
                    totalCH += (item.CH.HasValue) ? item.CH.Value : decimal.Zero;
                    doffStr += "D" + item.DoffNo.ToString();
                    cnt++;
                    if (cnt < max)
                    {
                        doffStr += ", ";
                    }
                }

                TotalCH = totalCH;
                DoffNos = doffStr.Trim();

                Raise(() => this.TotalCH);
                Raise(() => this.DoffNos);
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<PalletSetting>> Gets()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<PalletSetting>> rets = new NDbResult<List<PalletSetting>>();

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
                var items = cnn.Query<PalletSetting>("GetPalletSettings", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList() : null;

                // load items
                data.ForEach(d => 
                {
                    if (d.PalletId.HasValue)
                    {
                        d.Items = PalletSettingItem.Gets(d.PalletId.Value).Value();
                        d.Calculate();
                    }
                });

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
                rets.data = new List<PalletSetting>();
            }

            return rets;
        }
        /// <summary>
        /// Search
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<PalletSetting>> Search(string productLotNo, 
            DateTime? begin, DateTime? end, string productCode)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<PalletSetting>> rets = new NDbResult<List<PalletSetting>>();

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
            p.Add("@ProductLotNo", productLotNo);
            p.Add("@beginDate", begin);
            p.Add("@endDate", end);
            p.Add("@ProductCode", productCode);

            try
            {
                var items = cnn.Query<PalletSetting>("SearchPalletSettings", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList() : null;

                // load items
                data.ForEach(d =>
                {
                    if (d.PalletId.HasValue)
                    {
                        d.Items = PalletSettingItem.Gets(d.PalletId.Value).Value();
                        d.Calculate();
                    }
                });

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
                rets.data = new List<PalletSetting>();
            }

            return rets;
        }
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The PalletSetting to save.</param>
        /// <returns></returns>
        public static NDbResult<PalletSetting> Save(PalletSetting value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<PalletSetting> ret = new NDbResult<PalletSetting>();

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
            p.Add("@PCTwist1Id", value.PCTwist1Id);
            p.Add("@PalletCode", value.PalletCode);
            p.Add("@CreateDate", value.CreateDate);
            p.Add("@TwistNo", value.TwistNo);
            p.Add("@Counter", value.Counter);
            p.Add("@Mts", value.Mts);
            p.Add("@UserId", value.UserId);
            p.Add("@PalletType", value.PalletType);
            p.Add("@PalletStatus", value.PalletStatus);

            p.Add("@PalletId", value.PalletId, DbType.Int32, direction: ParameterDirection.InputOutput);
            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SavePalletSetting", p, commandType: CommandType.StoredProcedure);
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
    }

    public class PalletSettingItem : NInpc
    {
        #region Internal Variables

        private decimal? _CH = new decimal?(0);

        #endregion

        private void Calculate()
        {
            if (SPNoStart.HasValue && SPNoEnd.HasValue)
            {
                CH = SPNoEnd.Value - SPNoStart.Value + 1;
                if (SPNoStart.Value == SPNoEnd.Value)
                {
                    SPNos = string.Format("{0}", SPNoStart.Value);
                }
                else
                {
                    SPNos = string.Format("{0} - {1}", SPNoStart.Value, SPNoEnd.Value);
                }
            }
            else if (!SPNoStart.HasValue && SPNoEnd.HasValue)
            {
                CH = 1;
                SPNos = string.Format("{0}", SPNoEnd.Value);
            }
            else if (SPNoStart.HasValue && !SPNoEnd.HasValue)
            {
                CH = 1;
                SPNos = string.Format("{0}", SPNoStart.Value);
            }
            else
            {
                CH = 0;
                SPNos = string.Empty;
            }
            Raise(() => this.CH);
            Raise(() => this.SPNos);
        }

        #region Public Properties

        public int PalletId { get; set; }
        public int DoffNo { get; set; }
        public decimal? CH 
        {
            get { return Get<decimal?>(); }
            set 
            {
                Set(value, () =>
                {
                });
            }
        }
        public int? SPNoStart 
        {
            get { return Get<int?>(); }
            set
            {
                Set(value, () =>
                {
                    Calculate();
                });
            }
        }
        public int? SPNoEnd 
        {
            get { return Get<int?>(); }
            set
            {
                Set(value, () =>
                {
                    Calculate();
                });
            }
        }

        public string SPNos { get; set; }

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
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<PalletSettingItem>> Gets(int palletId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<PalletSettingItem>> rets = new NDbResult<List<PalletSettingItem>>();

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
            p.Add("@PalletId", palletId);

            try
            {
                var items = cnn.Query<PalletSettingItem>("GetPalletSettingItems", p,
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
                rets.data = new List<PalletSettingItem>();
            }

            return rets;
        }
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The PalletSettingItem to save.</param>
        /// <returns></returns>
        public static NDbResult<PalletSettingItem> Save(PalletSettingItem value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<PalletSettingItem> ret = new NDbResult<PalletSettingItem>();

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
            p.Add("@PalletId", value.PalletId);
            p.Add("@DoffNo", value.DoffNo);
            p.Add("@CH", value.CH);
            p.Add("@SPNoStart", value.SPNoStart);
            p.Add("@SPNoEnd", value.SPNoEnd);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SavePalletSettingItem", p, commandType: CommandType.StoredProcedure);
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
