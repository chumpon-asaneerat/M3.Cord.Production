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
    /// The PC Card Service.
    /// </summary>
    public class PCCardService : NSingelton<PCCardService>
    {
        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        protected PCCardService() : base()
        {

        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~PCCardService()
        {

        }

        #endregion

        #region Internal Variables

        private List<PCCard> items = new List<PCCard>();

        #endregion

        #region Public Methods

        public void Search(string productLotNo, string customerName)
        {
            items = PCCard.Gets(productLotNo, customerName).Value();
        }

        #endregion

        #region Public Properties

        public List<PCCard> PCCards
        { 
            get { return items; } 
            set { } 
        }

        #endregion
    }
}
