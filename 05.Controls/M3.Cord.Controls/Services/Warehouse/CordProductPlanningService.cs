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
    /// The Cord Product Planning Service.
    /// </summary>
    public class CordProductPlanningService : NSingelton<CordProductPlanningService>
    {
        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        protected CordProductPlanningService() : base()
        {

        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~CordProductPlanningService()
        {

        }

        #endregion

        #region Internal Variables

        private int _totalPallet = 0;
        private decimal _totalWeight = decimal.Zero;
        private decimal _totalCH = decimal.Zero;

        #endregion

        #region Privet Methods

        private void CalcTotals()
        {

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

            }
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

        #endregion
    }
}
