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
            BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE93;
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
                return ProductCode1 + " " + LotNo1 + DoffNo1.ToString() + " SP " + SPNo1.ToString();
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
        public int DoffNo1 { get; set; }
        public int SPNo1 { get; set; }

        public string LotNoDoffNo1 { get; set; }
        
        #endregion
        public string BarcodeText2
        {
            get
            {
                if (string.IsNullOrWhiteSpace(LotNo2) || string.IsNullOrWhiteSpace(ProductCode2) ||
                    SPNo2 <= 0)
                    return null;
                return ProductCode2 + " " + LotNo2 + DoffNo2.ToString() + " SP " + SPNo2.ToString();
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
        public int DoffNo2 { get; set; }
        public int SPNo2 { get; set; }

        public string LotNoDoffNo2 { get; set; }

        #endregion

        #region Static Methods

        public static List<LabelCHS9> Gets(string lotNo, int? doffNo, int? SPStart, int? SPEnd)
        {
            List<LabelCHS9> rets = new List<LabelCHS9>();

            var items = LabelCHS9Item.Gets(lotNo, doffNo, SPStart, SPEnd).Value();
            if (null != items)
            {
                LabelCHS9 item = null;
                for (int i = 0; i < items.Count; i++) 
                { 
                    if (i % 2 == 0)
                    {
                        item = new LabelCHS9();
                        item.LotNo1 = items[i].DIPLotNo;
                        item.DoffNo1 = items[i].DoffNo;
                        item.ProductCode1 = items[i].ProductCode;
                        item.SPNo1 = items[i].SPNo.Value;

                        item.LotNoDoffNo1 = items[i].DIPLotNo+ items[i].DoffNo.ToString();

                        rets.Add(item);
                    }
                    else
                    {
                        if (null != item)
                        {
                            item.LotNo2 = items[i].DIPLotNo;
                            item.DoffNo2 = items[i].DoffNo;
                            item.ProductCode2 = items[i].ProductCode;
                            item.SPNo2 = items[i].SPNo.Value;

                            item.LotNoDoffNo2 = items[i].DIPLotNo + items[i].DoffNo.ToString();
                        }
                    }
                }
            }

            return rets;
        }

        #endregion
    }
}
