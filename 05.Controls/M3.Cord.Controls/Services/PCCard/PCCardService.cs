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
            var rets = PCCard.Gets(productLotNo, customerName).Value();
            items = new List<PCCard>();
            if (null != rets)
            {
                rets.ForEach(pc =>
                {
                    bool isFinished = pc.FinishFlag.HasValue && pc.FinishFlag == true;
                    bool isDeleted = pc.DeleteFlag.HasValue && pc.DeleteFlag == true;
                    bool isT1Finished = pc.Twist1FinishFlag.HasValue && pc.Twist1FinishFlag == true;
                    bool isT1Deleted = pc.Twist1FinishFlag.HasValue && pc.Twist1FinishFlag == true;

                    if (!isFinished && !isDeleted && !isT1Finished && !isT1Deleted)
                    {
                        items.Add(pc);
                    }
                });
            }
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
