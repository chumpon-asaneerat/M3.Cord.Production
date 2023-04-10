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
    public class CordProduct : NInpc
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        #endregion

        #region Public Properties

        public int PKId { get; set; }

        public string CustomerName { get; set; }
        public string ItemYarn { get; set; }
        public string ItemCode { get; set; }
        public string Color { get; set; }
        public string LotNo { get; set; }
        public decimal TargetQty { get; set; } = decimal.Zero;
        public decimal CurrentQty { get; set; } = decimal.Zero;
        public string Flow { get; set; }
        public SolidColorBrush TextColor
        {
            get
            {
                return BlackColor;
            }
            set { }
        }

        #endregion

        #region Static Methods

        public static CordProduct Create(int pkId,
            string customerName,
            string itemYarm, string itemCode,
            string color, string lotNo,
            decimal qty, string flow)
        {
            return new CordProduct()
            {
                PKId = pkId,
                CustomerName = customerName,
                ItemYarn = itemYarm,
                ItemCode = itemCode,
                Color = color,
                LotNo = lotNo,
                TargetQty = qty,
                Flow = flow
            };
        }

        public static List<CordProduct> GetCordProducts()
        {
            var rets = new List<CordProduct>();

            rets.Add(Create(1, "BANDO", "100-364-704M", "9H 0663L", "ลายดาว", "21091", 5400, "S-1-1"));

            return rets;
        }

        #endregion
    }
}
