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
    public class CordYarn : NInpc
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        #endregion

        #region Public Properties

        public int PKId { get; set; }

        public DateTime? ReceiveDate { get; set; } = new DateTime?();

        public string PalletNo { get; set; }
        public string TraceNo { get; set; }
        public string LotNo { get; set; }

        public string ItemYarn { get; set; }

        public decimal? WeightQty { get; set; } = 520;
        public decimal? CH { get; set; } = 48;

        public string PalletType { get; set; } = "F";

        public string DeleteFlag { get; set; }

        public SolidColorBrush TextColor
        {
            get 
            {
                return (!ReceiveDate.HasValue) ? BlackColor : RedColor;
            }
            set { }
        }

        public bool IsMark
        {
            get { return ReceiveDate.HasValue; }
            set { }
        }

        #endregion

        #region Public Methods

        public void MarkReceive(DateTime? receiveDate)
        {
            ReceiveDate = receiveDate;
        }

        public void UnmarkReceive()
        {
            ReceiveDate = new DateTime?();
        }

        #endregion

        #region Static Methods

        public static CordYarn Create(int pkId,
            string itemYarm, string palletNo, 
            string lotNo, string traceNo)
        {
            return new CordYarn()
            {
                PKId = pkId,
                ItemYarn = itemYarm,
                PalletNo = palletNo,
                LotNo = lotNo,
                TraceNo = traceNo
            };
        }

        public static List<CordYarn> GetCordYarns()
        {
            var rets = new List<CordYarn>();

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
                var item = Create(i, itemYarns[i], palletNos[i], lotNos[i], traceNos[i]);
                rets.Add(item);
            }

            return rets;
        }

        #endregion
    }
}
