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
	public class S8BeforeCondition
	{
		#region Public Proeprties

		public int? S8BeforeId { get; set; }
        public int? DIPPCId { get; set; }

        public string ProductCode { get; set; }

        public int RowType { get; set; }
        public string LotNo { get; set; }

        public string LotNoS
        {
            get
            {
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return "";
                }
                else
                {
                    return LotNo;
                }
            }
            set { }
        }

        public bool? SolutionNameBath1SC { get; set; }
        public string SolutionNameBath1 { get; set; }
        public string SolutionNameBath1Value { get; set; }

        public string SolutionNameBath1ValueS
        {
            get
            {
                if (RowType == -2)
                {
                    return SolutionNameBath1;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!SolutionNameBath1SC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (SolutionNameBath1SC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return SolutionNameBath1Value;
                }
                else
                {
                    return SolutionNameBath1Value;
                }
            }
            set { }
        }

        public bool? SolutionNameBath2SC { get; set; }
        public string SolutionNameBath2 { get; set; }
        public string SolutionNameBath2Value { get; set; }

        public string SolutionNameBath2ValueS
        {
            get
            {
                if (RowType == -2)
                {
                    return SolutionNameBath1;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!SolutionNameBath2SC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (SolutionNameBath2SC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return SolutionNameBath2Value;
                }
                else
                {
                    return SolutionNameBath2Value;
                }
            }
            set { }
        }

        public bool? TempJacketDrumBath1SC { get; set; }
        public decimal? TempJacketDrumBath1Min { get; set; }
        public decimal? TempJacketDrumBath1Max { get; set; }
        public decimal? TempJacketDrumBath1 { get; set; }

        public string TempJacketDrumBath1S
        {
            get
            {
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    fmt += (TempJacketDrumBath1Min.HasValue) ?
                        TempJacketDrumBath1Min.Value.ToString("#,###.##") : "";
                    fmt += " - ";
                    fmt += (TempJacketDrumBath1Max.HasValue) ?
                        TempJacketDrumBath1Max.Value.ToString("#,###.##") : "";
                    fmt += " ℃ ";
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!TempJacketDrumBath1SC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (TempJacketDrumBath1SC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return (TempJacketDrumBath1.HasValue) ?
                        TempJacketDrumBath1.Value.ToString("#,###.##") : "";
                }
                else
                {
                    return (TempJacketDrumBath1.HasValue) ?
                        TempJacketDrumBath1.Value.ToString("#,###.##") : "";
                }
            }
            set { }
        }

        public bool? TempJacketDrumBath2SC { get; set; }
        public decimal? TempJacketDrumBath2Min { get; set; }
        public decimal? TempJacketDrumBath2Max { get; set; }
        public decimal? TempJacketDrumBath2 { get; set; }

        public string TempJacketDrumBath2S
        {
            get
            {
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    fmt += (TempJacketDrumBath2Min.HasValue) ?
                        TempJacketDrumBath2Min.Value.ToString("#,###.##") : "";
                    fmt += " - ";
                    fmt += (TempJacketDrumBath2Max.HasValue) ?
                        TempJacketDrumBath2Max.Value.ToString("#,###.##") : "";
                    fmt += " ℃ ";
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!TempJacketDrumBath2SC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (TempJacketDrumBath2SC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return (TempJacketDrumBath2.HasValue) ?
                        TempJacketDrumBath2.Value.ToString("#,###.##") : "";
                }
                else
                {
                    return (TempJacketDrumBath2.HasValue) ?
                        TempJacketDrumBath2.Value.ToString("#,###.##") : "";
                }
            }
            set { }
        }

        public bool? TempChemicalBath1SC { get; set; }
        public decimal? TempChemicalBath1Min { get; set; }
        public decimal? TempChemicalBath1Max { get; set; }
        public decimal? TempChemicalBath1 { get; set; }

        public string TempChemicalBath1S
        {
            get
            {
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    fmt += (TempChemicalBath1Min.HasValue) ?
                        TempChemicalBath1Min.Value.ToString("#,###.##") : "";
                    fmt += " - ";
                    fmt += (TempChemicalBath1Max.HasValue) ?
                        TempChemicalBath1Max.Value.ToString("#,###.##") : "";
                    fmt += " ℃ ";
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!TempChemicalBath1SC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (TempChemicalBath1SC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return (TempChemicalBath1.HasValue) ?
                        TempChemicalBath1.Value.ToString("#,###.##") : "";
                }
                else
                {
                    return (TempChemicalBath1.HasValue) ?
                        TempChemicalBath1.Value.ToString("#,###.##") : "";
                }
            }
            set { }
        }

        public bool? TempChemicalBath2SC { get; set; }
        public decimal? TempChemicalBath2Min { get; set; }
        public decimal? TempChemicalBath2Max { get; set; }
        public decimal? TempChemicalBath2 { get; set; }

        public string TempChemicalBath2S
        {
            get
            {
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    fmt += (TempChemicalBath2Min.HasValue) ?
                        TempChemicalBath2Min.Value.ToString("#,###.##") : "";
                    fmt += " - ";
                    fmt += (TempChemicalBath2Max.HasValue) ?
                        TempChemicalBath2Max.Value.ToString("#,###.##") : "";
                    fmt += " ℃ ";
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!TempChemicalBath2SC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (TempChemicalBath2SC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return (TempChemicalBath2.HasValue) ?
                        TempChemicalBath2.Value.ToString("#,###.##") : "";
                }
                else
                {
                    return (TempChemicalBath2.HasValue) ?
                        TempChemicalBath2.Value.ToString("#,###.##") : "";
                }
            }
            set { }
        }

        public bool? StretchDSC { get; set; }
        public decimal? StretchD { get; set; }
        public decimal? StretchDErr { get; set; }
        public decimal? StretchDValue { get; set; }

        public string StretchDValueS
        {
            get
            {
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    fmt += (StretchD.HasValue) ? StretchD.Value.ToString("#,###.##") : "";
                    fmt += " ± ";
                    fmt += (StretchDErr.HasValue) ? StretchDErr.Value.ToString("#,###.##") : "";
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!StretchDSC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (StretchDSC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return (StretchDValue.HasValue) ? StretchDValue.Value.ToString("#,###.##") : "";
                }
                else
                {
                    return (StretchDValue.HasValue) ? StretchDValue.Value.ToString("#,###.##") : "";
                }
            }
            set { }
        }

        public bool? StretchHSC { get; set; }
        public decimal? StretchH { get; set; }
        public decimal? StretchHErr { get; set; }
        public decimal? StretchHValue { get; set; }

        public string StretchHValueS
        {
            get
            {
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    fmt += (StretchH.HasValue) ? StretchH.Value.ToString("#,###.##") : "";
                    fmt += " ± ";
                    fmt += (StretchHErr.HasValue) ? StretchHErr.Value.ToString("#,###.##") : "";
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!StretchHSC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (StretchHSC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return (StretchHValue.HasValue) ? StretchHValue.Value.ToString("#,###.##") : "";
                }
                else
                {
                    return (StretchHValue.HasValue) ? StretchHValue.Value.ToString("#,###.##") : "";
                }
            }
            set { }
        }

        public bool? StretchNSC { get; set; }
        public decimal? StretchN { get; set; }
        public decimal? StretchNErr { get; set; }
        public decimal? StretchNValue { get; set; }

        public string StretchNValueS
        {
            get
            {
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    fmt += (StretchN.HasValue) ? StretchN.Value.ToString("#,###.##") : "";
                    fmt += " ± ";
                    fmt += (StretchNErr.HasValue) ? StretchNErr.Value.ToString("#,###.##") : "";
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!StretchNSC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (StretchNSC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return (StretchNValue.HasValue) ? StretchNValue.Value.ToString("#,###.##") : "";
                }
                else
                {
                    return (StretchNValue.HasValue) ? StretchNValue.Value.ToString("#,###.##") : "";
                }
            }
            set { }
        }

        public bool? TempDSC { get; set; }
        public decimal? TempD { get; set; }
        public decimal? TempDErr { get; set; }
        public decimal? TempDValue { get; set; }

        public string TempDValueS
        {
            get
            {
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    fmt += (TempD.HasValue) ? TempD.Value.ToString("#,###.##") : "";
                    fmt += " ± ";
                    fmt += (TempDErr.HasValue) ? TempDErr.Value.ToString("#,###.##") : "";
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!TempDSC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (TempDSC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return (TempDValue.HasValue) ? TempDValue.Value.ToString("#,###.##") : "";
                }
                else
                {
                    return (TempDValue.HasValue) ? TempDValue.Value.ToString("#,###.##") : "";
                }
            }
            set { }
        }

        public bool? TempHNSC { get; set; }
        public decimal? TempHN { get; set; }
        public decimal? TempHNErr { get; set; }
        public decimal? TempHNValue { get; set; }

        public string TempHNValueS
        {
            get
            {
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    fmt += (TempHN.HasValue) ? TempHN.Value.ToString("#,###.##") : "";
                    fmt += " ± ";
                    fmt += (TempHNErr.HasValue) ? TempHNErr.Value.ToString("#,###.##") : "";
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!TempHNSC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (TempHNSC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return (TempHNValue.HasValue) ? TempHNValue.Value.ToString("#,###.##") : "";
                }
                else
                {
                    return (TempHNValue.HasValue) ? TempHNValue.Value.ToString("#,###.##") : "";
                }
            }
            set { }
        }

        public bool? SpeedSC { get; set; }
        public decimal? Speed { get; set; }
        public decimal? SpeedErr { get; set; }
        public decimal? SpeedValue { get; set; }

        public string SpeedValueS
        {
            get
            {
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    fmt += (Speed.HasValue) ? Speed.Value.ToString("#,###.##") : "";
                    fmt += " ± ";
                    fmt += (SpeedErr.HasValue) ? SpeedErr.Value.ToString("#,###.##") : "";
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!SpeedSC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (SpeedSC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return (SpeedValue.HasValue) ? SpeedValue.Value.ToString("#,###.##") : "";
                }
                else
                {
                    return (SpeedValue.HasValue) ? SpeedValue.Value.ToString("#,###.##") : "";
                }
            }
            set { }
        }

        public bool? ExhaustFanDryerSC { get; set; }
        public decimal? ExhaustFanDryer { get; set; }
        public decimal? ExhaustFanDryerValue { get; set; }

        public string ExhaustFanDryerValueS
        {
            get
            {
                if (RowType == -2)
                {
                    return (ExhaustFanDryer.HasValue) ? 
                        ExhaustFanDryer.Value.ToString("#.###.##") : "";
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!ExhaustFanDryerSC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (ExhaustFanDryerSC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return (ExhaustFanDryerValue.HasValue) ?
                        ExhaustFanDryerValue.Value.ToString("#.###.##") : "";
                }
                else
                {
                    return (ExhaustFanDryerValue.HasValue) ?
                        ExhaustFanDryerValue.Value.ToString("#.###.##") : "";
                }
            }
            set { }
        }

        public bool? ExhaustFanHNSC { get; set; }
        public decimal? ExhaustFanHN { get; set; }
        public decimal? ExhaustFanHNValue { get; set; }

        public string ExhaustFanHNValueS
        {
            get
            {
                if (RowType == -2)
                {
                    return (ExhaustFanHN.HasValue) ?
                        ExhaustFanHN.Value.ToString("#.###.##") : "";
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!ExhaustFanHNSC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (ExhaustFanHNSC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return (ExhaustFanHNValue.HasValue) ?
                        ExhaustFanHNValue.Value.ToString("#.###.##") : "";
                }
                else
                {
                    return (ExhaustFanHNValue.HasValue) ?
                        ExhaustFanHNValue.Value.ToString("#.###.##") : "";
                }
            }
            set { }
        }

        public bool? CleanBath1SC { get; set; }
        public bool? CleanBath1 { get; set; }

        public string CleanBath1S
        {
            get
            {
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!CleanBath1SC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (CleanBath1SC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    // Check Symbol ✔
                    // Cross Symbol ✗
                    if (!CleanBath1.HasValue)
                        return "";
                    else return (CleanBath1.Value) ? "✔" : "✗";
                }
                else
                {
                    // Check Symbol ✔
                    // Cross Symbol ✗
                    if (!CleanBath1.HasValue)
                        return "";
                    else return (CleanBath1.Value) ? "✔" : "✗";
                }
            }
            set { }
        }

        public bool? CleanBath2SC { get; set; }
        public bool? CleanBath2 { get; set; }

        public string CleanBath2S
        {
            get
            {
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!CleanBath2SC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (CleanBath2SC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    // Check Symbol ✔
                    // Cross Symbol ✗
                    if (!CleanBath2.HasValue)
                        return "";
                    else return (CleanBath2.Value) ? "✔" : "✗";
                }
                else
                {
                    // Check Symbol ✔
                    // Cross Symbol ✗
                    if (!CleanBath2.HasValue)
                        return "";
                    else return (CleanBath2.Value) ? "✔" : "✗";
                }
            }
            set { }
        }

        public bool? CleanFrontSC { get; set; }
        public bool? CleanFront { get; set; }

        public string CleanFrontS
        {
            get
            {
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!CleanFrontSC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (CleanFrontSC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    // Check Symbol ✔
                    // Cross Symbol ✗
                    if (!CleanFront.HasValue)
                        return "";
                    else return (CleanFront.Value) ? "✔" : "✗";
                }
                else
                {
                    // Check Symbol ✔
                    // Cross Symbol ✗
                    if (!CleanFront.HasValue)
                        return "";
                    else return (CleanFront.Value) ? "✔" : "✗";
                }
            }
            set { }
        }

        public bool? CamboxSC { get; set; }
        public bool? Cambox { get; set; }

        public string CamboxS
        {
            get
            {
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!CamboxSC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (CamboxSC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    // Check Symbol ✔
                    // Cross Symbol ✗
                    if (!Cambox.HasValue)
                        return "";
                    else return (Cambox.Value) ? "✔" : "✗";
                }
                else
                {
                    // Check Symbol ✔
                    // Cross Symbol ✗
                    if (!Cambox.HasValue)
                        return "";
                    else return (Cambox.Value) ? "✔" : "✗";
                }
            }
            set { }
        }

        public string CheckBy { get; set; }
        public DateTime? CheckDate { get; set; }
        public string VerifyBy { get; set; }
        public DateTime? VerifyDate { get; set; }

        public bool ShowVerify
        {
            get 
            { 
                return (RowType == 1 && S8BeforeId.HasValue && S8BeforeId.Value > 0); 
            }
            set { }
        }

        public bool IsVerify
        {
            get { return (RowType == 1 && !string.IsNullOrEmpty(VerifyBy)); }
            set { }
        }

        #endregion

        #region Static Methods

        public static S8BeforeCondition Create(string productCode)
        {
            var inst = new S8BeforeCondition();
            var std = S8BeforeConditionStd.Gets(productCode).Value().FirstOrDefault();
            Assign(std, inst);
            return inst;
        }

        public static void Assign(S8BeforeConditionStd src, S8BeforeCondition dst)
        {
            if (null != src && null != dst)
            {
                dst.ProductCode = src.ProductCode;
                dst.SolutionNameBath1SC = src.SolutionNameBath1SC;
                dst.SolutionNameBath1 = src.SolutionNameBath1;
                dst.SolutionNameBath2SC = src.SolutionNameBath2SC;
                dst.SolutionNameBath2 = src.SolutionNameBath2;
                dst.TempJacketDrumBath1SC = src.TempJacketDrumBath1SC;
                dst.TempJacketDrumBath1Min = src.TempJacketDrumBath1Min;
                dst.TempJacketDrumBath1Max = src.TempJacketDrumBath1Max;
                dst.TempJacketDrumBath2SC = src.TempJacketDrumBath2SC;
                dst.TempJacketDrumBath2Min = src.TempJacketDrumBath2Min;
                dst.TempJacketDrumBath2Max = src.TempJacketDrumBath2Max;
                dst.TempChemicalBath1SC = src.TempChemicalBath1SC;
                dst.TempChemicalBath1Min = src.TempChemicalBath1Min;
                dst.TempChemicalBath1Max = src.TempChemicalBath1Max;
                dst.TempChemicalBath2SC = src.TempChemicalBath2SC;
                dst.TempChemicalBath2Min = src.TempChemicalBath2Min;
                dst.TempChemicalBath2Max = src.TempChemicalBath2Max;
                dst.StretchDSC = src.StretchDSC;
                dst.StretchD = src.StretchD;
                dst.StretchDErr = src.StretchDErr;
                dst.StretchHSC = src.StretchHSC;
                dst.StretchH = src.StretchH;
                dst.StretchHErr = src.StretchHErr;
                dst.StretchNSC = src.StretchNSC;
                dst.StretchN = src.StretchN;
                dst.StretchNErr = src.StretchNErr;
                dst.TempDSC = src.TempDSC;
                dst.TempD = src.TempD;
                dst.TempDErr = src.TempDErr;
                dst.TempHNSC = src.TempHNSC;
                dst.TempHN = src.TempHN;
                dst.TempHNErr = src.TempHNErr;
                dst.SpeedSC = src.SpeedSC;
                dst.Speed = src.Speed;
                dst.SpeedErr = src.SpeedErr;
                dst.ExhaustFanDryerSC = src.ExhaustFanDryerSC;
                dst.ExhaustFanDryer = src.ExhaustFanDryer;
                dst.ExhaustFanHNSC = src.ExhaustFanHNSC;
                dst.ExhaustFanHN = src.ExhaustFanHN;
                dst.CleanBath1SC = src.CleanBath1SC;
                dst.CleanBath2SC = src.CleanBath2SC;
                dst.CleanFrontSC = src.CleanFrontSC;
                dst.CamboxSC = src.CamboxSC;
            }
        }

        public static NDbResult<List<S8BeforeCondition>> Gets(int? DIPPCId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<S8BeforeCondition>> ret = new NDbResult<List<S8BeforeCondition>>();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                ret.ErrNum = 8000;
                ret.ErrMsg = msg;

                return ret;
            }

            var p = new DynamicParameters();
            p.Add("@DIPPCId", DIPPCId);

            try
            {
                var items = cnn.Query<S8BeforeCondition>("GetS8BeforeConditions", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList() : null;
                ret.Success(data);
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
                ret.data = new List<S8BeforeCondition>();
            }

            return ret;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The S8BeforeConditionItem item to save.</param>
        /// <returns></returns>
        public static NDbResult<S8BeforeCondition> Save(S8BeforeCondition value)
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<S8BeforeCondition> ret = new NDbResult<S8BeforeCondition>();

			if (null == value)
			{
				ret.ParameterIsNull();
				return ret;
			}

			IDbConnection cnn = DbServer.Instance.Db;
			if (null == cnn || !DbServer.Instance.Connected)
			{
				string msg = "Connection is null or cannot connect to database server.";
				med.Err(msg);
				// Set error number/message
				ret.ErrNum = 8000;
				ret.ErrMsg = msg;

				return ret;
			}

			var p = new DynamicParameters();

			p.Add("@DIPPCId", value.DIPPCId);
			
            p.Add("@ProductCode", value.ProductCode);
            p.Add("@RowType", value.RowType);
            p.Add("@LotNo", value.LotNo);

			p.Add("@SolutionNameBath1SC", value.SolutionNameBath1SC);
            p.Add("@SolutionNameBath1", value.SolutionNameBath1);            
			p.Add("@SolutionNameBath1Value", value.SolutionNameBath1Value);

            p.Add("@SolutionNameBath2SC", value.SolutionNameBath2SC);
            p.Add("@SolutionNameBath2", value.SolutionNameBath2);
            p.Add("@SolutionNameBath2Value", value.SolutionNameBath2Value);

            p.Add("@TempJacketDrumBath1SC", value.TempJacketDrumBath1SC);
            p.Add("@TempJacketDrumBath1Min", value.TempJacketDrumBath1Min);
            p.Add("@TempJacketDrumBath1Max", value.TempJacketDrumBath1Max);
            p.Add("@TempJacketDrumBath1", value.TempJacketDrumBath1);

            p.Add("@TempJacketDrumBath2SC", value.TempJacketDrumBath2SC);
            p.Add("@TempJacketDrumBath2Min", value.TempJacketDrumBath2Min);
            p.Add("@TempJacketDrumBath2Max", value.TempJacketDrumBath2Max);
            p.Add("@TempJacketDrumBath2", value.TempJacketDrumBath2);

            p.Add("@TempChemicalBath1SC", value.TempChemicalBath1SC);
            p.Add("@TempChemicalBath1Min", value.TempChemicalBath1Min);
            p.Add("@TempChemicalBath1Max", value.TempChemicalBath1Max);
            p.Add("@TempChemicalBath1", value.TempChemicalBath1);
            
			p.Add("@TempChemicalBath2SC", value.TempChemicalBath2SC);
            p.Add("@TempChemicalBath2Min", value.TempChemicalBath2Min);
            p.Add("@TempChemicalBath2Max", value.TempChemicalBath2Max);
            p.Add("@TempChemicalBath2", value.TempChemicalBath2);

			p.Add("@StretchDSC", value.StretchDSC);
            p.Add("@StretchD", value.StretchD);
            p.Add("@StretchDErr", value.StretchDErr);
            p.Add("@StretchDValue", value.StretchDValue);

            p.Add("@StretchHSC", value.StretchHSC);
            p.Add("@StretchH", value.StretchH);
            p.Add("@StretchHErr", value.StretchHErr);
            p.Add("@StretchHValue", value.StretchHValue);

            p.Add("@StretchNSC", value.StretchNSC);
            p.Add("@StretchN", value.StretchN);
            p.Add("@StretchNErr", value.StretchNErr);
            p.Add("@StretchNValue", value.StretchNValue);

            p.Add("@TempDSC", value.TempDSC);
            p.Add("@TempD", value.TempD);
            p.Add("@TempDErr", value.TempDErr);
            p.Add("@TempDValue", value.TempDValue);

            p.Add("@TempHNSC", value.TempHNSC);
            p.Add("@TempHN", value.TempHN);
            p.Add("@TempHNErr", value.TempHNErr);
            p.Add("@TempHNValue", value.TempHNValue);

            p.Add("@SpeedSC", value.SpeedSC);
            p.Add("@Speed", value.Speed);
            p.Add("@SpeedErr", value.SpeedErr);
            p.Add("@SpeedValue", value.SpeedValue);

            p.Add("@ExhaustFanDryerSC", value.ExhaustFanDryerSC);
            p.Add("@ExhaustFanDryer", value.ExhaustFanDryer);
            p.Add("@ExhaustFanDryerValue", value.ExhaustFanDryerValue);

            p.Add("@ExhaustFanHNSC", value.ExhaustFanHNSC);
            p.Add("@ExhaustFanHN", value.ExhaustFanHN);
            p.Add("@ExhaustFanHNValue", value.ExhaustFanHNValue);

            p.Add("@CleanBath1SC", value.CleanBath1SC);
            p.Add("@CleanBath1", value.CleanBath1);

            p.Add("@CleanBath2SC", value.CleanBath2SC);
            p.Add("@CleanBath2", value.CleanBath2);

            p.Add("@CleanFrontSC", value.CleanFrontSC);
            p.Add("@CleanFront", value.CleanFront);

            p.Add("@CamboxSC", value.CamboxSC);
            p.Add("@Cambox", value.Cambox);

            p.Add("@CheckBy", value.CheckBy);
            p.Add("@CheckDate", value.CheckDate);
            p.Add("@VerifyBy", value.VerifyBy);
            p.Add("@VerifyDate", value.VerifyDate);

            p.Add("@S8BeforeId", value.S8BeforeId, DbType.Int32, direction: ParameterDirection.InputOutput);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
			p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

			try
			{
				cnn.Execute("SaveS8BeforeCondition", p, commandType: CommandType.StoredProcedure);
				ret.Success(value);

                // Set PK
                value.S8BeforeId = p.Get<dynamic>("@S8BeforeId");
                // Set error number/message
                ret.ErrNum = p.Get<int>("@errNum");
				ret.ErrMsg = p.Get<string>("@errMsg");
			}
			catch (Exception ex)
			{
				med.Err(ex);
				// Set error number/message
				ret.ErrNum = 9999;
				ret.ErrMsg = ex.Message;
			}

			return ret;
		}

        public static NDbResult<S8BeforeCondition> GetStdV(int? DIPPCId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S8BeforeCondition> ret = new NDbResult<S8BeforeCondition>();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                ret.ErrNum = 8000;
                ret.ErrMsg = msg;

                return ret;
            }

            var p = new DynamicParameters();
            p.Add("@DIPPCId", DIPPCId);

            try
            {
                var items = cnn.Query<S8BeforeCondition>("GetS8BeforeStdVRow", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.FirstOrDefault() : null;
                ret.Success(data);
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
                ret.data = null;
            }

            return ret;
        }

        public static NDbResult<S8BeforeCondition> GetStdB(int? DIPPCId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S8BeforeCondition> ret = new NDbResult<S8BeforeCondition>();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                ret.ErrNum = 8000;
                ret.ErrMsg = msg;

                return ret;
            }

            var p = new DynamicParameters();
            p.Add("@DIPPCId", DIPPCId);

            try
            {
                var items = cnn.Query<S8BeforeCondition>("GetS8BeforeStdBRow", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.FirstOrDefault() : null;
                ret.Success(data);
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
                ret.data = null;
            }

            return ret;
        }

        public static NDbResult<S8BeforeCondition> GetStdCF(int? DIPPCId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S8BeforeCondition> ret = new NDbResult<S8BeforeCondition>();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                ret.ErrNum = 8000;
                ret.ErrMsg = msg;

                return ret;
            }

            var p = new DynamicParameters();
            p.Add("@DIPPCId", DIPPCId);

            try
            {
                var items = cnn.Query<S8BeforeCondition>("GetS8BeforeStdCFRow", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.FirstOrDefault() : null;
                ret.Success(data);
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
                ret.data = null;
            }

            return ret;
        }

        public static NDbResult SaveStdV(int? DIPPCId, string ProductCode, string LotNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult ret = new NDbResult();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                ret.ErrNum = 8000;
                ret.ErrMsg = msg;

                return ret;
            }

            var p = new DynamicParameters();
            p.Add("@DIPPCId", DIPPCId);
            p.Add("@ProductCode", ProductCode);
            p.Add("@LotNo", LotNo);

            try
            {
                var items = cnn.Query<DIPTimeTable>("SaveS8BeforeStdVRow", p,
                    commandType: CommandType.StoredProcedure);
                ret.Success();
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
            }

            return ret;
        }

        public static NDbResult SaveStdB(int? DIPPCId, string ProductCode, string LotNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult ret = new NDbResult();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                ret.ErrNum = 8000;
                ret.ErrMsg = msg;

                return ret;
            }

            var p = new DynamicParameters();
            p.Add("@DIPPCId", DIPPCId);
            p.Add("@ProductCode", ProductCode);
            p.Add("@LotNo", LotNo);

            try
            {
                var items = cnn.Query<DIPTimeTable>("SaveS8BeforeStdBRow", p,
                    commandType: CommandType.StoredProcedure);
                ret.Success();
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
            }

            return ret;
        }

        public static NDbResult SaveStdCF(int? DIPPCId, string ProductCode, string LotNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult ret = new NDbResult();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                ret.ErrNum = 8000;
                ret.ErrMsg = msg;

                return ret;
            }

            var p = new DynamicParameters();
            p.Add("@DIPPCId", DIPPCId);
            p.Add("@ProductCode", ProductCode);
            p.Add("@LotNo", LotNo);

            try
            {
                var items = cnn.Query<DIPTimeTable>("SaveS8BeforeStdCFRow", p,
                    commandType: CommandType.StoredProcedure);
                ret.Success();
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
            }

            return ret;
        }

        public static NDbResult DeleteStd(int? DIPPCId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult ret = new NDbResult();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                ret.ErrNum = 8000;
                ret.ErrMsg = msg;

                return ret;
            }

            var p = new DynamicParameters();
            p.Add("@DIPPCId", DIPPCId);

            try
            {
                var items = cnn.Query<DIPTimeTable>("DeleteS8BeforeStd", p,
                    commandType: CommandType.StoredProcedure);
                ret.Success();
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
            }

            return ret;
        }

        public static NDbResult Delete(S8BeforeCondition value)
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult ret = new NDbResult();

			if (null == value)
			{
				ret.ParameterIsNull();
				return ret;
			}

			IDbConnection cnn = DbServer.Instance.Db;
			if (null == cnn || !DbServer.Instance.Connected)
			{
				string msg = "Connection is null or cannot connect to database server.";
				med.Err(msg);
				// Set error number/message
				ret.ErrNum = 8000;
				ret.ErrMsg = msg;

				return ret;
			}

			var p = new DynamicParameters();

			p.Add("@ProductCode", value.ProductCode);
			
			try
			{
				cnn.Execute("DELETE FROM S8BeforeConditionItem WHERE ProductCode = @ProductCode", p, commandType: CommandType.Text);
				ret.Success();
				// Set error number/message
				ret.ErrNum = p.Get<int>("@errNum");
				ret.ErrMsg = p.Get<string>("@errMsg");
			}
			catch (Exception ex)
			{
				med.Err(ex);
				// Set error number/message
				ret.ErrNum = 9999;
				ret.ErrMsg = ex.Message;
			}

			return ret;
		}

		#endregion
	}
}
