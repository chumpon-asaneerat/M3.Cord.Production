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
            BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
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
                return LotNo1 + " " + ProductCode1 + " " + SPNo1.ToString();
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
                return LotNo2 + " " + ProductCode2 + " " + SPNo2.ToString();
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

        #region Barcode 3
        public string BarcodeText3
        {
            get
            {
                if (string.IsNullOrWhiteSpace(LotNo3) || string.IsNullOrWhiteSpace(ProductCode3) ||
                    SPNo3 <= 0)
                    return null;
                return LotNo3 + " " + ProductCode3 + " " + SPNo3.ToString();
            }
            set { }
        }
        // Label barcode
        public byte[] BarcodeImage3
        {
            get
            {
                byte[] results = null;
                if (!string.IsNullOrWhiteSpace(BarcodeText3))
                {
                    System.Drawing.Image img = BarcodeGenerator.Encode(BarcodeGenerator.EncodedType,
                        BarcodeText3, 800, 100);

                    results = NLib.Utils.ImageUtils.GetImage(img);
                }
                return results;
            }
            set { }
        }

        public string ProductCode3 { get; set; }
        public string LotNo3 { get; set; }
        public int SPNo3 { get; set; }

        #endregion

        #region Barcode 4
        public string BarcodeText4
        {
            get
            {
                if (string.IsNullOrWhiteSpace(LotNo4) || string.IsNullOrWhiteSpace(ProductCode4) ||
                    SPNo4 <= 0)
                    return null;
                return LotNo4 + " " + ProductCode4 + " " + SPNo4.ToString();
            }
            set { }
        }
        // Label barcode
        public byte[] BarcodeImage4
        {
            get
            {
                byte[] results = null;
                if (!string.IsNullOrWhiteSpace(BarcodeText4))
                {
                    System.Drawing.Image img = BarcodeGenerator.Encode(BarcodeGenerator.EncodedType,
                        BarcodeText4, 800, 100);

                    results = NLib.Utils.ImageUtils.GetImage(img);
                }
                return results;
            }
            set { }
        }

        public string ProductCode4 { get; set; }
        public string LotNo4 { get; set; }
        public int SPNo4 { get; set; }

        #endregion

        #region Barcode 5
        public string BarcodeText5
        {
            get
            {
                if (string.IsNullOrWhiteSpace(LotNo5) || string.IsNullOrWhiteSpace(ProductCode5) ||
                    SPNo5 <= 0)
                    return null;
                return LotNo5 + " " + ProductCode5 + " " + SPNo5.ToString();
            }
            set { }
        }
        // Label barcode
        public byte[] BarcodeImage5
        {
            get
            {
                byte[] results = null;
                if (!string.IsNullOrWhiteSpace(BarcodeText5))
                {
                    System.Drawing.Image img = BarcodeGenerator.Encode(BarcodeGenerator.EncodedType,
                        BarcodeText5, 800, 100);

                    results = NLib.Utils.ImageUtils.GetImage(img);
                }
                return results;
            }
            set { }
        }

        public string ProductCode5 { get; set; }
        public string LotNo5 { get; set; }
        public int SPNo5 { get; set; }

        #endregion

        #region Barcode 6
        public string BarcodeText6
        {
            get
            {
                if (string.IsNullOrWhiteSpace(LotNo6) || string.IsNullOrWhiteSpace(ProductCode6) ||
                    SPNo6 <= 0)
                    return null;
                return LotNo6 + " " + ProductCode6 + " " + SPNo6.ToString();
            }
            set { }
        }
        // Label barcode
        public byte[] BarcodeImage6
        {
            get
            {
                byte[] results = null;
                if (!string.IsNullOrWhiteSpace(BarcodeText6))
                {
                    System.Drawing.Image img = BarcodeGenerator.Encode(BarcodeGenerator.EncodedType,
                        BarcodeText6, 800, 100);

                    results = NLib.Utils.ImageUtils.GetImage(img);
                }
                return results;
            }
            set { }
        }

        public string ProductCode6 { get; set; }
        public string LotNo6 { get; set; }
        public int SPNo6 { get; set; }

        #endregion

        #region Barcode 7
        public string BarcodeText7
        {
            get
            {
                if (string.IsNullOrWhiteSpace(LotNo7) || string.IsNullOrWhiteSpace(ProductCode7) ||
                    SPNo7 <= 0)
                    return null;
                return LotNo7 + " " + ProductCode7 + " " + SPNo7.ToString();
            }
            set { }
        }
        // Label barcode
        public byte[] BarcodeImage7
        {
            get
            {
                byte[] results = null;
                if (!string.IsNullOrWhiteSpace(BarcodeText7))
                {
                    System.Drawing.Image img = BarcodeGenerator.Encode(BarcodeGenerator.EncodedType,
                        BarcodeText7, 800, 100);

                    results = NLib.Utils.ImageUtils.GetImage(img);
                }
                return results;
            }
            set { }
        }

        public string ProductCode7 { get; set; }
        public string LotNo7 { get; set; }
        public int SPNo7 { get; set; }

        #endregion

        #region Barcode 8
        public string BarcodeText8
        {
            get
            {
                if (string.IsNullOrWhiteSpace(LotNo8) || string.IsNullOrWhiteSpace(ProductCode8) ||
                    SPNo8 <= 0)
                    return null;
                return LotNo8 + " " + ProductCode8 + " " + SPNo8.ToString();
            }
            set { }
        }
        // Label barcode
        public byte[] BarcodeImage8
        {
            get
            {
                byte[] results = null;
                if (!string.IsNullOrWhiteSpace(BarcodeText8))
                {
                    System.Drawing.Image img = BarcodeGenerator.Encode(BarcodeGenerator.EncodedType,
                        BarcodeText8, 800, 100);

                    results = NLib.Utils.ImageUtils.GetImage(img);
                }
                return results;
            }
            set { }
        }

        public string ProductCode8 { get; set; }
        public string LotNo8 { get; set; }
        public int SPNo8 { get; set; }

        #endregion

        #region Barcode 9
        public string BarcodeText9
        {
            get
            {
                if (string.IsNullOrWhiteSpace(LotNo9) || string.IsNullOrWhiteSpace(ProductCode9) ||
                    SPNo9 <= 0)
                    return null;
                return LotNo9 + " " + ProductCode9 + " " + SPNo9.ToString();
            }
            set { }
        }
        // Label barcode
        public byte[] BarcodeImage9
        {
            get
            {
                byte[] results = null;
                if (!string.IsNullOrWhiteSpace(BarcodeText9))
                {
                    System.Drawing.Image img = BarcodeGenerator.Encode(BarcodeGenerator.EncodedType,
                        BarcodeText9, 800, 100);

                    results = NLib.Utils.ImageUtils.GetImage(img);
                }
                return results;
            }
            set { }
        }

        public string ProductCode9 { get; set; }
        public string LotNo9 { get; set; }
        public int SPNo9 { get; set; }

        #endregion

        #region Barcode 10
        public string BarcodeText10
        {
            get
            {
                if (string.IsNullOrWhiteSpace(LotNo10) || string.IsNullOrWhiteSpace(ProductCode10) ||
                    SPNo10 <= 0)
                    return null;
                return LotNo10 + " " + ProductCode10 + " " + SPNo10.ToString();
            }
            set { }
        }
        // Label barcode
        public byte[] BarcodeImage10
        {
            get
            {
                byte[] results = null;
                if (!string.IsNullOrWhiteSpace(BarcodeText10))
                {
                    System.Drawing.Image img = BarcodeGenerator.Encode(BarcodeGenerator.EncodedType,
                        BarcodeText10, 800, 100);

                    results = NLib.Utils.ImageUtils.GetImage(img);
                }
                return results;
            }
            set { }
        }

        public string ProductCode10 { get; set; }
        public string LotNo10 { get; set; }
        public int SPNo10 { get; set; }

        #endregion

        #endregion

        #region Static Methods

        public static List<LabelCHS9> Gets()
        {
            List<LabelCHS9> rets = new List<LabelCHS9>();

            string lotNo = "23J06A1";
            string productCode = "P1671T";

            LabelCHS9 ret = new LabelCHS9();

            for (int i = 0; i < 10; i++)
            {
                
                if (i == 0)
                {
                    ret.LotNo1 = lotNo;
                    ret.ProductCode1 = productCode;
                    ret.SPNo1 = i + 1;
                }
                else if (i == 1)
                {
                    ret.LotNo2 = lotNo;
                    ret.ProductCode2 = productCode;
                    ret.SPNo2 = i + 1;
                }
                else if (i == 2)
                {
                    ret.LotNo3 = lotNo;
                    ret.ProductCode3 = productCode;
                    ret.SPNo3 = i + 1;
                }
                else if (i == 3)
                {
                    ret.LotNo4 = lotNo;
                    ret.ProductCode4 = productCode;
                    ret.SPNo4 = i + 1;
                }
                else if (i == 4)
                {
                    ret.LotNo5 = lotNo;
                    ret.ProductCode5 = productCode;
                    ret.SPNo5 = i + 1;
                }
                else if (i == 5)
                {
                    ret.LotNo6 = lotNo;
                    ret.ProductCode6 = productCode;
                    ret.SPNo6 = i + 1;
                }
                else if (i == 6)
                {
                    ret.LotNo7 = lotNo;
                    ret.ProductCode7 = productCode;
                    ret.SPNo7 = i + 1;
                }
                else if (i == 7)
                {
                    ret.LotNo8 = lotNo;
                    ret.ProductCode8 = productCode;
                    ret.SPNo8 = i + 1;
                }
                else if (i == 8)
                {
                    ret.LotNo9 = lotNo;
                    ret.ProductCode9 = productCode;
                    ret.SPNo9 = i + 1;
                }
                else if (i == 9)
                {
                    ret.LotNo10 = lotNo;
                    ret.ProductCode10 = productCode;
                    ret.SPNo10 = i + 1;
                }
            }

            rets.Add(ret);

            return rets;
        }

        #endregion
    }
}
