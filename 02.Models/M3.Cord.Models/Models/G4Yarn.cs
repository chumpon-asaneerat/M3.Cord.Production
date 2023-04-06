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
    public class G4Yarn2 : NInpc
    {
        #region Internal Variables

        private int _PKId = 0;
        private DateTime? _EntryDate = new DateTime?();
        private string _ItemYarn = null;
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

        private string _DeleteFlag = null;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public G4Yarn2() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~G4Yarn2() { }

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

    public class G4Yarn : NInpc
    {
        #region Public Properties

        public int PKId { get; set; }
        public DateTime? EntryDate { get; set; } = new DateTime(2023, 4, 5);

        public string ItemYarn { get; set; }
        public string PalletNo { get; set; }
        public string YarnType { get; set; }
        public string Item400Code { get; set; }

        public string LotNo { get; set; }
        public string TraceNo { get; set; }

        public string PalletType { get; set; } = "F";
        public string Unit { get; set; } = "KG";

        public decimal? WeightQty { get; set; } = 520;
        public decimal? RemainQty { get; set; } = 520;
        public int Conech { get; set; } = 52;

        public DateTime? ReceivedDate { get; set; } = new DateTime?();
        public string ReceiveBy { get; set; }

        public string FinishFlag { get; set; }

        public string Verify { get; set; }

        public bool OK
        {
            get { return (Verify == "OK"); }
            set
            {
                if (value)
                {
                    Verify = "OK";
                }
                Raise(() => OK);
                Raise(() => NG);
            }
        }
        public bool NG
        {
            get { return (Verify == "NG"); }
            set
            {
                if (value)
                {
                    Verify = "NG";
                }
                Raise(() => OK);
                Raise(() => NG);
            }
        }

        public bool IsOK
        {
            get { return (Verify == "OK"); }
            set { }
        }
        public bool IsNG
        {
            get { return (Verify == "NG"); }
            set { }
        }


        public bool? Packing { get; set; } = new bool?();
        public bool? Clean { get; set; } = new bool?();
        public bool? Tearing { get; set; } = new bool?();
        public bool? FallDown { get; set; } = new bool?();
        public bool? Certification { get; set; } = new bool?();
        public bool? Invoice { get; set; } = new bool?();
        public bool? IdentifyArea { get; set; } = new bool?();
        public bool? AmountPallet { get; set; } = new bool?();

        public string Other { get; set; }
        public string Action { get; set; }

        public DateTime? MovementDate { get; set; } = new DateTime?();
        public decimal? KGPERCH { get; set; } = 10;

        public string DeleteFlag { get; set; }

        #endregion

        #region Static Methods

        public static G4Yarn Create(int pkId, 
            string itemYarm, string palletNo, string yarnType, 
            string item400Code,
            string lotNo, string traceNo)
        {
            return new G4Yarn()
            {
                PKId = pkId,
                ItemYarn = itemYarm,
                PalletNo = palletNo,
                YarnType = yarnType,
                Item400Code = item400Code,
                LotNo = lotNo,
                TraceNo = traceNo
            };
        }

        public static List<G4Yarn> GetG4Yarns(DateTime? receiveDate = new DateTime?())
        {
            var rets = new List<G4Yarn>();

            var itemYarns = new string[]
            {
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ",
                "470-72-1781-JJ"
            };

            var palletNos = new string[]
            {
                "S7G280004",
                "S7G280005",
                "S7G280006",
                "S7G280007",
                "S7G280008",
                "S7G280009",
                "S7G280010",
                "S7G280012",
                "S7G280013",
                "S7G280014",
                "S7G280015",
                "S7G280016",
                "S7G280017",
                "S7G280018",
                "S7G280019",
                "S7G280020",
                "S7G280023",
                "S7G280024",
                "S7G280025",
                "S7G280026",
                "S7G280027",
                "S7G280028",
                "S7G280029",
                "S7G280030",
                "S7G280031"
            };

            var yarnTypes = new string[]
            {
                "Warp",
                "Warp",
                "Warp",
                "Warp",
                "Warp",
                "Warp",
                "Warp",
                "Warp",
                "Warp",
                "Warp",
                "Warp",
                "Warp",
                "Warp",
                "Warp",
                "Warp",
                "Warp",
                "Warp",
                "Warp",
                "Warp",
                "Warp",
                "Warp",
                "Warp",
                "Warp",
                "Warp",
                "Warp"
            };

            var item400Codes = new string[]
            {
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP",
                "47072OP"
            };

            var lotNos = new string[]
            {
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227",
                "#M3/227"
            };

            var traceNos = new string[]
            {
                "76000156",
                "76000157",
                "76000158",
                "76000159",
                "76000160",
                "76000161",
                "76000162",
                "76000164",
                "76000167",
                "76000168",
                "76000169",
                "76000170",
                "76000171",
                "76000172",
                "76000173",
                "76000174",
                "76000177",
                "76000178",
                "76000179",
                "76000180",
                "76000181",
                "76000182",
                "76000183",
                "76000184",
                "76000185"
            };

            for (int i = 0; i < itemYarns.Length; ++i)
            {
                var item = Create(1, itemYarns[i], palletNos[i], yarnTypes[i], item400Codes[i], lotNos[i], traceNos[i]);
                if (receiveDate.HasValue)
                {
                    item.ReceivedDate = receiveDate.Value;
                }
                rets.Add(item);
            }

            return rets;
        }

        #endregion
    }
}
