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

#endregion

namespace M3.Cord.Models
{
    public enum PalletType : int
    {
        Cart = 0,
        Pallet = 1
    }

    public enum PalletStatus : int
    {
        Create = 0,
        Ageing = 1,
        Dip = 2
    }

    public class PalletSetting
    {
        #region Public Properties

        public int? PalletId { get; set; }
        public int? PCTwist1Id { get; set; }
        public string PalletCode { get; set; }
        public string CreateDate { get; set; }
        public int Counter { get; set; }
        // Create UserId
        public int UserId { get; set; }
        public PalletType PalletType { get; set; }
        public PalletStatus PalletStatus { get; set; }

        public SolidColorBrush TextColor
        {
            get
            {
                return ModelConsts.BlackColor;
            }
            set { }
        }

        #endregion

        #region Static Methods

        #endregion
    }

    public class PalletSettingItem
    {
        #region Public Properties

        public int PalletId { get; set; }
        public int DoffNo { get; set; }
        public int? SPNoStart { get; set; }
        public int? SPNoEnd { get; set; }

        public SolidColorBrush TextColor
        {
            get
            {
                return ModelConsts.BlackColor;
            }
            set { }
        }

        #endregion

        #region Static Methods

        #endregion
    }
}
