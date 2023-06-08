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
            Pallets = new List<CordProductPallet>();
        }

        #region Public Properties

        public int PKId { get; set; }

        public string CustomerName { get; set; } = "BANDO";
        public string ItemYarn { get; set; }
        public string ItemCode { get; set; } = "9H 0663L";
        public string Color { get; set; } = "ลายดาว";
        public string LotNo { get; set; }
        public decimal TargetQty { get; set; } = 540;
        public decimal CurrentQty { get; set; } = decimal.Zero;
        public string Flow { get; set; } = "S-1-1";

        public List<CordProductPallet> Pallets { get; set; }

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

        #endregion

        #region Static Methods

        public static CordProduct Create(int pkId,
            string customerName,
            string itemYarm, string itemCode,
            string color, string lotNo,
            decimal qty, string flow)
        {
            return new CordProduct()
            {
                PKId = pkId,
                CustomerName = customerName,
                ItemYarn = itemYarm,
                ItemCode = itemCode,
                Color = color,
                LotNo = lotNo,
                TargetQty = qty,
                Flow = flow
            };
        }

        public static List<CordProduct> GetCordProducts()
        {
            var rets = new List<CordProduct>();

            rets.Add(Create(1, "BANDO", "100-364-704M", "9H 0663L", "ลายดาว", "21091", 5400, "S-1-1"));

            return rets;
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
