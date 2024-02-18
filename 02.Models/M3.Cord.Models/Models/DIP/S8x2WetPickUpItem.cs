#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Reflection;

using System.Windows.Media;

using NLib;
using NLib.Models;

using Dapper;
using Newtonsoft.Json;
using System.Windows.Controls.Primitives;

#endregion

namespace M3.Cord.Models
{
    public class S8x2WetPickUpItem
    {
        #region Public Properties

        public string ProductCode { get; set; }
        public string LotNo { get; set; }
        public DateTime? DoffingDate { get; set; }
        public int DoffingNo { get; set; }
        public int TwistNo { get; set; }
        public int RowType { get; set; }

        public string NipPressure { get; set; }
        public decimal? WPU { get; set; }
        public decimal? WPUErr { get; set; }
        public decimal? WPUValue { get; set; }

        public decimal? FirstAmt { get; set; }
        public decimal? UseAmt { get; set; }
        public decimal? RestAmt { get; set; }
        public decimal? ThrowAmt { get; set; }

        public string Operator { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Remark { get; set; }

        #endregion

        #region Static Methods

        #endregion
    }
}
