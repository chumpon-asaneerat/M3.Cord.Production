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
    public class CordProduct : NInpc
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        #endregion

        public CordProduct()
        {
            //Pallets = new List<CordProductPallet>();
        }

        #region Public Properties

        public int CordProductPkId { get; set; }
        public string ProductLotNo { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string ItemYarn { get; set; }
        public string ItemCode { get; set; }
        public string Item400 { get; set; }
        public string Color { get; set; }
        public decimal TargetQty { get; set; } = decimal.Zero;
        public decimal ActualQty { get; set; } = decimal.Zero;

        public bool FinishFlag { get; set; }
        public bool DeleteFlag { get; set; }

        //public List<CordProductPallet> Pallets { get; set; }

        public SolidColorBrush TextColor
        {
            get
            {
                return BlackColor;
            }
            set { }
        }

        public bool IsUsed { get; set; }

        private int _totalPallet = 0;
        private decimal _totalWeight = decimal.Zero;
        private decimal _totalCH = decimal.Zero;

        public void CalcTotals()
        {
            _totalPallet = 0;
            _totalWeight = decimal.Zero;
            _totalCH = 0;

            lock (this)
            {
                /*
                if (null != Pallets && Pallets.Count > 0)
                {
                    _totalPallet = Pallets.Count;
                    Pallets.ForEach(pallet =>
                    {
                        // add weight
                        var weight = (pallet.WeightQty.HasValue) ? pallet.WeightQty.Value : decimal.Zero;
                        _totalWeight += weight;
                        // add cheese count.
                        var cheeseCnt = (pallet.ConeCH.HasValue) ? pallet.ConeCH.Value : decimal.Zero;
                        _totalCH += cheeseCnt;
                    });
                }
                */
            }
            // Raise Events
            Raise(() => this.TotalPallet);
            Raise(() => this.TotalWeight);
            Raise(() => this.TotalCH);
        }

        /// <summary>Gets Total Pallet.</summary>
        [JsonIgnore]
        public int TotalPallet { get { return _totalPallet; } set { } }
        /// <summary>Gets Total Weight.</summary>
        [JsonIgnore]
        public decimal TotalWeight { get { return _totalWeight; } set { } }
        /// <summary>Gets Total CH (Cheese).</summary>
        [JsonIgnore]
        public decimal TotalCH { get { return _totalCH; } set { } }

        /*
        public void AddPallet(string palletNo, string traceNo, decimal coneCH, decimal qty)
        {
            if (null == Pallets) Pallets = new List<CordProductPallet>();
            Pallets.Add(new CordProductPallet() 
            { 
                PalletNo = palletNo,
                TraceNo = traceNo, 
                ConeCH = coneCH,
                WeightQty = qty
            });
            CalcTotals();
        }
        */
        #endregion

        #region Static Methods

        public static NDbResult<List<CordProduct>> Gets(
            string productLotNo = null, string customerName = null, string itemYarn = null)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordProduct>> rets = new NDbResult<List<CordProduct>>();

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
            p.Add("@ProductLotNo", string.IsNullOrWhiteSpace(productLotNo) ? null : productLotNo);
            p.Add("@CustomerName", string.IsNullOrWhiteSpace(customerName) ? null : customerName);
            p.Add("@ItemYarn", string.IsNullOrWhiteSpace(itemYarn) ? null : itemYarn);

            try
            {
                var items = cnn.Query<CordProduct>("GetCordProducts", p,
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
                rets.data = new List<CordProduct>();
            }

            return rets;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The CordProduct item to save.</param>
        /// <returns></returns>
        public static NDbResult<CordProduct> Save(CordProduct value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordProduct> ret = new NDbResult<CordProduct>();

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
            p.Add("@ProductLotNo", value.ProductLotNo);
            p.Add("@CustomerCode", value.CustomerCode);
            p.Add("@CustomerName", value.CustomerName);
            p.Add("@ItemYarn", value.ItemYarn);

            p.Add("@ItemYarn", value.ItemYarn);
            p.Add("@ItemCode", value.ItemCode);
            p.Add("@Item400", value.Item400);
            p.Add("@Color", value.Color);

            p.Add("@TargetQty", value.TargetQty);
            p.Add("@ActualQty", value.ActualQty);

            p.Add("@FinishFlag", value.FinishFlag);
            p.Add("@DeleteFlag", value.DeleteFlag);

            p.Add("@CordProductPkId", value.CordProductPkId, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveCordProduct", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);
                // Set PK
                value.CordProductPkId = p.Get<int>("@CordProductPkId");
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

    public class CordProductPallet
    {
        public string PalletNo { get; set; }
        public string TraceNo { get; set; }
        public decimal? ConeCH { get; set; }
        public decimal? WeightQty { get; set; }

        public SolidColorBrush TextColor
        {
            get
            {
                return ModelConsts.BlackColor;
            }
            set { }
        }
    }
}
