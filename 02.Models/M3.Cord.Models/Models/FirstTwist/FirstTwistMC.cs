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
using System.Net;

#endregion

namespace M3.Cord.Models
{
    public class FirstTwistMC : NInpc
    {
        #region Public Properties

        public string MCName { get; set; }
        public int BBCount { get; set; } = 102;
        public int SheetPerBB { get; set; } = 1;

        public CordProduct Product { get; set; } = null;
        public List<RawMaterialSheetItem> RawMaterialSheets { get; set; } = new List<RawMaterialSheetItem>();

        #endregion

        #region Static Methods

        public static List<FirstTwistMC> Gets()
        {
            List<FirstTwistMC> rets = new List<FirstTwistMC>();

            rets.Add(new FirstTwistMC()
            {
                MCName = "S-1",
                SheetPerBB = 2,
                Product = CordProduct.Create(1, "BANDO", "100-364-704M", "9H 0663L", "ลายดาว", "21091", 5400, "S-1-1"),
                RawMaterialSheets = RawMaterialSheetItem.GetRawMaterialSheets("S-1")
            });
            rets.Add(new FirstTwistMC() { MCName = "S-4" });

            return rets;
        }

        #endregion
    }

    public class RawMaterialSheetItem : NInpc
    {
        #region Public Properties

        public DateTime? ProductionDate { get; set; } = new DateTime?();
        public string YarnType { get; set; } = string.Empty;
        public string TraceNo { get; set; } = string.Empty;
        public string InputCh { get; set; } = string.Empty;
        public string DoffName { get; set; } = string.Empty;
        public string SPNo { get; set; } = string.Empty;

        #endregion

        #region Static Methods

        public static RawMaterialSheetItem Create(DateTime productionDate, 
            string yarnType, string traceNo,
            string inputCh, string doffName, string spNo)
        {
            RawMaterialSheetItem ret = new RawMaterialSheetItem()
            {
                ProductionDate = productionDate,
                YarnType = yarnType,
                TraceNo = traceNo,
                InputCh = inputCh, 
                DoffName = doffName,
                SPNo = spNo
            };
            return ret;
        }

        public static List<RawMaterialSheetItem> GetRawMaterialSheets(string mcName)
        {
            List<RawMaterialSheetItem> rets = new List<RawMaterialSheetItem>();
            if (mcName == "S-1") 
            {
                rets.Add(Create(DateTime.Today,"100-360-704", "17003010", "100", "1-4", "1-50"));
                rets.Add(Create(DateTime.Today, "100-360-704", "17003002", "99", "1-4", "51-100"));
            }
            return rets;
        }

        #endregion
    }
}
