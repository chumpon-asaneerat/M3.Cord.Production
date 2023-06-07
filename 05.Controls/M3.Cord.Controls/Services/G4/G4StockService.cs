#region Using

using System;
using System.Collections.Generic;

using NLib;
using NLib.Models;
using M3.Cord.Models;

#endregion

namespace M3.Cord
{
    /// <summary>
    /// The G4 Stock Service.
    /// </summary>
    public class G4StockService : NSingelton<G4StockService>
    {
        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        protected G4StockService() : base()
        {

        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~G4StockService()
        {

        }

        #endregion

        #region Internal Variables

        private int _totalPallet = 0;
        private decimal _totalWeight = decimal.Zero;
        private decimal _totalCH = decimal.Zero;

        private List<G4Yarn> _stocks = new List<G4Yarn>();

        #endregion

        #region Private Methods

        private void CalcTotals()
        {
            _totalPallet = 0;
            _totalWeight = decimal.Zero;
            _totalCH = 0;

            lock (this)
            {
                if (null != _stocks && _stocks.Count > 0)
                {
                    _totalPallet = _stocks.Count;
                    _stocks.ForEach(receive =>
                    {
                        // add weight
                        var weight = (receive.WeightQty.HasValue) ? receive.WeightQty.Value : decimal.Zero;
                        _totalWeight += weight;
                        // add cheese count.
                        var cheeseCnt = (receive.ConeCH.HasValue) ? receive.ConeCH.Value : decimal.Zero;
                        _totalCH += cheeseCnt;
                    });
                }
            }
            // Raise Events
            Raise(() => this.TotalPallet);
            Raise(() => this.TotalWeight);
            Raise(() => this.TotalCH);
        }

        #endregion

        #region Public Methods

        public void Search(string itemYarn, DateTime? ReceiveDate = new DateTime?())
        {
            _stocks = G4Yarn.GetReceiveYarnStocks(itemYarn, ReceiveDate).Value();

            CalcTotals();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Current Stock (Raw Meterial) List.
        /// </summary>
        public List<G4Yarn> Stocks
        {
            get { return _stocks; }
            set { }
        }
        /// <summary>Gets Total Pallet.</summary>
        public int TotalPallet { get { return _totalPallet; } set { } }
        /// <summary>Gets Total Weight.</summary>
        public decimal TotalWeight { get { return _totalWeight; } set { } }
        /// <summary>Gets Total CH (Cheese).</summary>
        public decimal TotalCH { get { return _totalCH; } set { } }

        #endregion
    }
}
