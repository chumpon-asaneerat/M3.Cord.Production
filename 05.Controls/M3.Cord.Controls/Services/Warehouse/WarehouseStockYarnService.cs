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
    /// The Warehouse Stock Yarn Service.
    /// </summary>
    public class WarehouseStockYarnService : NSingelton<WarehouseStockYarnService>
    {
        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        protected WarehouseStockYarnService() : base()
        {

        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~WarehouseStockYarnService()
        {

        }

        #endregion

        #region Internal Variables

        private int _totalPallet = 0;
        private decimal _totalWeight = decimal.Zero;
        private decimal _totalCH = decimal.Zero;

        //private List<WarehouseCordYarn> sources = null;
        private List<WarehouseCordYarn> items = null;

        #endregion

        #region Private Methods

        private void CalcTotals()
        {
            _totalPallet = 0;
            _totalWeight = decimal.Zero;
            _totalCH = 0;

            lock (this)
            {
                if (null != items)
                {
                    items.ForEach(item =>
                    {
                        if (item.IsMark)
                        {
                            ++_totalPallet;
                            _totalWeight += (item.WeightQty.HasValue) ? item.WeightQty.Value : decimal.Zero;
                            _totalCH += (item.ConeCH.HasValue) ? item.ConeCH.Value : decimal.Zero;
                        }
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

        /// <summary>
        /// Clear receive list.
        /// </summary>
        public void Clear()
        {
            lock (this)
            {
                items = new List<WarehouseCordYarn>();
            }
            CalcTotals();
        }

        public void LoadStockYarns(string itemYarn)
        {
            Clear();

            items = WarehouseCordYarn.SearchStockYarns(itemYarn).Value();
            CalcTotals();
        }

        #endregion

        #region Public Properties

        /// <summary>Gets Total Pallet.</summary>
        public int TotalPallet { get { return _totalPallet; } set { } }
        /// <summary>Gets Total Weight.</summary>
        public decimal TotalWeight { get { return _totalWeight; } set { } }
        /// <summary>Gets Total CH (Cheese).</summary>
        public decimal TotalCH { get { return _totalCH; } set { } }
        /// <summary>
        /// Gets Warehouse Cord Yarn Items (Raw Meterial) List.
        /// </summary>
        public List<WarehouseCordYarn> IssueItems
        {
            get { return items; }
            set { }
        }

        #endregion
    }
}
