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
    /// The G4Yarn class.
    /// </summary>
    public class G4Yarn : NInpc
    {
        #region Internal Variables

        private int _PKId = 0;
        private DateTime? _EntryDate = new DateTime?();
        private string _ItemYane = null;
        private string _PalletNo = null;
        private string _YarnType = null;
        private decimal _WeightQty = decimal.Zero;
        private decimal? _Conech = decimal.Zero;
        private string _Verify = null;
        private decimal _RemainQty = decimal.Zero;
        private DateTime? _ReceivedDate = new DateTime?();
        private string _ReceiveBy = null;

        private string _FinishFlag = null;
        private DateTime? _UpdateDate = null;
        private string _PalletType = null;

        private string _Item400Code = null;
        private string _UM = null;

        private bool _Packing = false;
        private bool _Clean = false;
        private bool _Tearing = false;
        private bool _FallDown = false;
        private bool _Certification = false;
        private bool _Invoice = false;
        private bool _IdentifyArea = false;
        private bool _AmountPallet = false;

        private string _Other = null;
        private string _Action = null;

        private DateTime? _MovementDate = null;

        private string _LotNo = null;
        private string _TraceNo = null;

        private decimal _KGPERCH = decimal.Zero;

        private bool _DeleteFlag = false;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public G4Yarn() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~G4Yarn() { }

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
