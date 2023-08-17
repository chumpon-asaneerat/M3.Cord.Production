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

        private List<CordProduct> items = new List<CordProduct>();

        #endregion

        #region Public Methods

        public void Search(string productLotNo, string customerName, string itemYarn)
        {
            items = CordProduct.Gets(productLotNo, customerName, itemYarn).Value();
        }

        #endregion

        #region Public Properties

        public List<CordProduct> Products 
        { 
            get { return items; } 
            set { } 
        }

        #endregion
    }
}
