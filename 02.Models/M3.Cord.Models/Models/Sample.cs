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
    /// <summary>
    /// The Sample Class
    /// </summary>
    public class Sample : NInpc
    {
        #region Internal Variables

        private int _PKId = 0;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Sample() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~Sample() { }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets PKId.
        /// </summary>
        public int PKId
        {
            get { return _PKId; }
            set
            {
                if (_PKId != value)
                {
                    _PKId = value;
                    Raise(() => PKId);
                }
            }
        }

        #endregion
    }
}
