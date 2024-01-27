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
using System.ComponentModel;

#endregion

using BarcodeLib;

namespace M3.Cord.Models
{
    public class LabelCHS9
    {
        private static Barcode BarcodeGenerator = null;

        static LabelCHS9()
        {
            BarcodeGenerator = new Barcode();
            BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE128;
            BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
            BarcodeGenerator.IncludeLabel = false;
        }

        #region Constructor

        public LabelCHS9() : base()
        {

        }

        #endregion

        #region Public Properties

        #region Barcode 1
        public string BarcodeText1 
        { 
            get
            {
                if (string.IsNullOrWhiteSpace(LotNo1) || string.IsNullOrWhiteSpace(ProductCode1) ||
                    SPNo1 <= 0)
                    return null;
                return ProductCode1 + " " + LotNo1 + " " + SPNo1.ToString();
            }
            set { }
        }
        // Label barcode
        public byte[] BarcodeImage1
        {
            get
            {
                byte[] results = null;
                if (!string.IsNullOrWhiteSpace(BarcodeText1))
                {
                    System.Drawing.Image img = BarcodeGenerator.Encode(BarcodeGenerator.EncodedType,
                        BarcodeText1, 800, 100);

                    results = NLib.Utils.ImageUtils.GetImage(img);
                }
                return results;
            }
            set { }
        }

        public string ProductCode1 { get; set; }
        public string LotNo1 { get; set; }
        public int SPNo1 { get; set; }

        #endregion
        public string BarcodeText2
        {
            get
            {
                if (string.IsNullOrWhiteSpace(LotNo2) || string.IsNullOrWhiteSpace(ProductCode2) ||
                    SPNo2 <= 0)
                    return null;
                return ProductCode2 + " " + LotNo2 + " " + SPNo2.ToString();
            }
            set { }
        }
        // Label barcode
        public byte[] BarcodeImage2
        {
            get
            {
                byte[] results = null;
                if (!string.IsNullOrWhiteSpace(BarcodeText2))
                {
                    System.Drawing.Image img = BarcodeGenerator.Encode(BarcodeGenerator.EncodedType,
                        BarcodeText2, 800, 100);

                    results = NLib.Utils.ImageUtils.GetImage(img);
                }
                return results;
            }
            set { }
        }

        public string ProductCode2 { get; set; }
        public string LotNo2 { get; set; }
        public int SPNo2 { get; set; }

        #endregion

        #region Static Methods

        public static List<LabelCHS9> Gets(string lotNo, int? SPStart, int? SPEnd)
        {
            List<LabelCHS9> rets = new List<LabelCHS9>();

            var items = LabelCHS9Item.Gets(lotNo, SPStart, SPEnd).Value();
            if (null != items)
            {
                LabelCHS9 item = null;
                for (int i = 0; i < items.Count; i++) 
                { 
                    if (i % 2 == 0)
                    {
                        item = new LabelCHS9();
                        item.LotNo1 = items[i].DIPLotNo;
                        item.ProductCode1 = items[i].ProductCode;
                        item.SPNo1 = items[i].SPNo.Value;
                        rets.Add(item);
                    }
                    else
                    {
                        if (null != item)
                        {
                            item.LotNo2 = items[i].DIPLotNo;
                            item.ProductCode2 = items[i].ProductCode;
                            item.SPNo2 = items[i].SPNo.Value;
                        }
                    }
                }
            }

            return rets;
        }

        #endregion
    }
}
