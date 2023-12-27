﻿#region Using

using System;
using System.Collections.Generic;

using NLib;
using NLib.Models;
using M3.Cord.Models;

#endregion

namespace M3.Cord
{
    /// <summary>
    /// The G4 Receive Yarn Service.
    /// </summary>
    public class G4ReceiveYarnService : NSingelton<G4ReceiveYarnService>
    {
        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        protected G4ReceiveYarnService() : base()
        {

        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~G4ReceiveYarnService()
        {
            
        }

        #endregion

        #region Internal Variables

        private int _totalPallet = 0;
        private decimal _totalWeight = decimal.Zero;
        private decimal _totalCH = decimal.Zero;

        private List<G4Yarn> _receives = new List<G4Yarn>();

        #endregion

        #region Private Methods

        private void CalcTotals()
        {
            _totalPallet = 0;
            _totalWeight = decimal.Zero;
            _totalCH = 0;

            lock (this)
            {
                if (null != _receives && _receives.Count > 0)
                {
                    _totalPallet = _receives.Count;
                    _receives.ForEach(receive =>
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

        /// <summary>
        /// Clear receive list.
        /// </summary>
        public void Clear()
        {
            lock (this)
            {
                _receives = new List<G4Yarn>();
            }
            CalcTotals();
        }
        /// <summary>
        /// Add item to current receive list.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(G4Yarn item)
        {
            if (null == item) return;
            if (null == _receives) return;
            lock (this)
            {
                _receives.Add(item);
            }
            CalcTotals();
        }
        /// <summary>
        /// Remove item from current receive list.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Remove(G4Yarn item) 
        {
            if (null == item) return;
            if (null == _receives) return;

            lock (this)
            {
                int idx = _receives.IndexOf(item);
                if (idx == -1) return;
                _receives.RemoveAt(idx);
            }
            CalcTotals();
        }
        /// <summary>
        /// Check is trace no is already exist in current receive list.
        /// </summary>
        /// <param name="tranceNo">The Trace No.</param>
        /// <param name="itemYarn">The Item Yarn.</param>
        /// <returns>Returns true if the trace no is in current receive list.</returns>
        public bool IsExist(string traceNo, string itemYarn)
        {
            if (null == _receives) Clear();
            int idx = -1;
            lock (this)
            {
                idx = _receives.FindIndex(oitem => 
                { 
                    return (oitem.TraceNo.Trim() == traceNo && oitem.ItemYarn.Trim() == itemYarn); 
                });
            }
            return (idx != -1);
        }
        /// <summary>
        /// Gets G4Yarn by Trace No.
        /// </summary>
        /// <param name="tranceNo">The Trace No.</param>
        /// <param name="itemYarn">The Item Yarn.</param>
        /// <returns>Returns match item if not found returns null.</returns>
        public G4Yarn SerachByTranceNo(string tranceNo, string itemYarn)
        {
            var ret = G4Yarn.Get(tranceNo, itemYarn).Value();
            return ret;
        }
        /// <summary>
        /// Save all receive current items to database.
        /// </summary>
        public bool SaveReceiveItems()
        {
            bool bSuccess = false;
            if (null != _receives)
            {
                lock (this)
                {
                    // update receive date + receive by
                    _receives.ForEach(yarn =>
                    {
                        yarn.ReceiveDate = DateTime.Now;
                        yarn.ReceiveBy = 1; // need userid here.
                        yarn.FinishFlag = false; // this value is true when issue only.
                    });

                    var ret = G4Yarn.Save(_receives);

                    bSuccess = (null != ret && ret.Ok);
                }
            }
            Clear(); // clear list.

            return bSuccess;
        }
        /// <summary>
        /// Load data from AS400.
        /// </summary>
        /// <param name="importMode"></param>
        /// <returns></returns>
        public int LoadFromAS400(bool importMode = false)
        {
            int ret = -1;

            List<BCSPRFTP> list;
            if (importMode)
                list = BCSPRFTP.AS400.LoadFromFile();
            else
                list = BCSPRFTP.AS400.Gets();

            if (null == list)
                return ret;

            int iErr = 0;
            list.ForEach(item =>
            {
                var dbRet = BCSPRFTP.M3Cord.Save(item);
                if (null != dbRet && !dbRet.HasError)
                {
                    BCSPRFTP.AS400.Delete(item);
                }
                else
                {
                    iErr++;
                }
            });

            ret = (iErr > 0) ? -1 : list.Count;

            return ret;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Current Receive Yarn (Raw Meterial) List.
        /// </summary>
        public List<G4Yarn> Receives
        {
            get { return _receives; }
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
