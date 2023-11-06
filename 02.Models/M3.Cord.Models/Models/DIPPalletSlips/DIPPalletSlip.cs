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
    public enum DIPPalletStatus : int
    {
        All = -1,
        Create = 0
    }

    public class DIPPalletSlip : NInpc
    {
        private static Barcode BarcodeGenerator = null;

        static DIPPalletSlip()
        {
            BarcodeGenerator = new Barcode();
            BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
            BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
            BarcodeGenerator.IncludeLabel = false;
        }

        #region Constructor

        public DIPPalletSlip() : base()
        {
            //Items = new List<PalletSettingItem>();
        }

        #endregion

        #region Public Properties

        public int? PalletId { get; set; }
        public int? DIPPCId { get; set; }
        public string PalletCode { get; set; }
        // Pallet barcode
        public byte[] PalletCodeImage
        {
            get
            {
                byte[] results = null;
                if (!string.IsNullOrWhiteSpace(PalletCode))
                {
                    System.Drawing.Image img = BarcodeGenerator.Encode(BarcodeGenerator.EncodedType,
                        PalletCode, 400, 100);

                    results = NLib.Utils.ImageUtils.GetImage(img);
                }
                return results;
            }
            set { }
        }

        public DateTime? CreateDate { get; set; }
        public string TwistNo { get; set; }
        public int Counter { get; set; }
        public string UserName { get; set; }
        public DateTime? PCDate { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ItemYarn { get; set; }
        public string CordStructure { get; set; }
        public string TreatRoute { get; set; }
        public string TwistSpec { get; set; }

        public string ProductLotNo { get; set; }
        public string MCCode { get; set; }

        public string DoffNos { get; set; }

        public decimal? ActualQty { get; set; }
        public decimal? ActualWeight { get; set; }
        public decimal? TargetQty { get; set; }

        public DIPPalletStatus PalletStatus { get; set; }

        public string PalletStatusText
        {
            get
            {
                if (PalletStatus == DIPPalletStatus.Create)
                {
                    return "DIP";
                }
                else
                {
                    return "Create";
                }
            }
            set { }
        }

        public SolidColorBrush TextColor
        {
            get
            {
                return ModelConsts.BlackColor;
            }
            set { }
        }

        //public List<PalletSettingItem> Items { get; set; }

        #endregion

        #region Static Methods

        #endregion

        public static List<DIPPalletSlip> GetSamples()
        {
            var rets = new List<DIPPalletSlip>();
            rets.Add(new DIPPalletSlip
            {
                ActualQty = 126,
                ActualWeight = decimal.Parse("5.12"),
                CordStructure = @"P123//22/22",
                Counter = 29000,
                CreateDate = DateTime.Now,
                CustomerName = "BANDO",
                MCCode = "S-8-1",
                DoffNos = "D2",
                ItemYarn = "123-456-789-0",
                PalletCode = "P-2023-XXX-1",
                ProductLotNo = "293019A",
                ProductName = "9H3099AA",
                UserName = "Administrator User",
                TwistNo = "222",
                TwistSpec = "4444"
            });
            return rets;
        }
    }
}
