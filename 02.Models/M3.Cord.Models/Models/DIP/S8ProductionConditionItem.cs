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
    public class S8ProductionConditionItem
    {
        #region Public Proeprties

        public int? DIPPCId { get; set; }
        public int RowType { get; set; } = 1;

        public string ProductCode { get; set; }
        public string LotNo { get; set; }
        public DateTime? DoffingDate { get; set; }

        public string DoffingDateS
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
                    return (DoffingDate.HasValue) ?
                        DoffingDate.Value.ToString("yyyy-MM-dd") : "";
                }
            }
            set { }
        }

        public int? DoffingNo { get; set; }
        public string DoffingNoS
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
                    return (DoffingNo.HasValue) ?
                        DoffingNo.Value.ToString() : "";
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

        public bool? TreatSC { get; set; }
        public decimal? Treat { get; set; }
        public decimal? TreatValue { get; set; }

        public string TreatValueS
        {
            get
            {
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    fmt += (Treat.HasValue) ? Treat.Value.ToString("#,###.##") : "";
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!TreatSC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (TreatSC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return (TreatValue.HasValue) ? TreatValue.Value.ToString("#,###.##") : "";
                }
                else
                {
                    return (TreatValue.HasValue) ? TreatValue.Value.ToString("#,###.##") : "";
                }
            }
            set { }
        }

        public bool? DoffingLengthSC { get; set; }
        public decimal? DoffingLength { get; set; }
        public decimal? DoffingLengthValue { get; set; }

        public string DoffingLengthValueS
        {
            get
            {
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    fmt += (DoffingLength.HasValue) ? DoffingLength.Value.ToString("#,###.##") : "";
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!DoffingLengthSC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (DoffingLengthSC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return (DoffingLengthValue.HasValue) ? DoffingLengthValue.Value.ToString("#,###.##") : "";
                }
                else
                {
                    return (DoffingLengthValue.HasValue) ? DoffingLengthValue.Value.ToString("#,###.##") : "";
                }
            }
            set { }
        }

        public bool? WeightSC { get; set; }
        public decimal? Weight { get; set; }
        public decimal? WeightValue { get; set; }

        public string WeightValueS
        {
            get
            {
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    fmt += (Weight.HasValue) ? Weight.Value.ToString("#,###.##") : "";
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!WeightSC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (WeightSC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return (WeightValue.HasValue) ? WeightValue.Value.ToString("#,###.##") : "";
                }
                else
                {
                    return (WeightValue.HasValue) ? WeightValue.Value.ToString("#,###.##") : "";
                }
            }
            set { }
        }

        public bool? SpindleSC { get; set; }
        public decimal? Spindle { get; set; }
        public decimal? SpindleValue { get; set; }

        public string SpindleValueS
        {
            get
            {
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    fmt += (Spindle.HasValue) ? Spindle.Value.ToString("#,###.##") : "";
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!SpindleSC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (SpindleSC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return (SpindleValue.HasValue) ? SpindleValue.Value.ToString("#,###.##") : "";
                }
                else
                {
                    return (SpindleValue.HasValue) ? SpindleValue.Value.ToString("#,###.##") : "";
                }
            }
            set { }
        }

        public bool? ProductionGoodSC { get; set; }
        public decimal? ProductionGood { get; set; }
        public decimal? ProductionGoodValue { get; set; }

        public string ProductionGoodValueS
        {
            get
            {
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    fmt += (ProductionGood.HasValue) ? ProductionGood.Value.ToString("#,###.##") : "";
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!ProductionGoodSC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (ProductionGoodSC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return (ProductionGoodValue.HasValue) ? ProductionGoodValue.Value.ToString("#,###.##") : "";
                }
                else
                {
                    return (ProductionGoodValue.HasValue) ? ProductionGoodValue.Value.ToString("#,###.##") : "";
                }
            }
            set { }
        }

        public bool? ProductionTotalSC { get; set; }
        public decimal? ProductionTotal { get; set; }
        public decimal? ProductionTotalValue { get; set; }

        public string ProductionTotalValueS
        {
            get
            {
                if (RowType == -2)
                {
                    string fmt = string.Empty;
                    fmt += (ProductionTotal.HasValue) ? ProductionTotal.Value.ToString("#,###.##") : "";
                    return fmt;
                }
                else if (RowType == -1)
                {
                    string fmt = string.Empty;
                    if (!ProductionTotalSC.HasValue)
                    {
                        fmt = "";
                    }
                    else
                    {
                        fmt = (ProductionTotalSC.Value) ? "SC" : "";
                    }
                    return fmt;
                }
                else if (RowType == 0)
                {
                    return (ProductionTotalValue.HasValue) ? ProductionTotalValue.Value.ToString("#,###.##") : "";
                }
                else
                {
                    return (ProductionTotalValue.HasValue) ? ProductionTotalValue.Value.ToString("#,###.##") : "";
                }
            }
            set { }
        }

        public decimal? ProductionCut { get; set; }
        public string ProductionCutValueS
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
                    return (ProductionCut.HasValue) ? ProductionCut.Value.ToString("#,###.##") : "";
                }
                else
                {
                    return (ProductionCut.HasValue) ? ProductionCut.Value.ToString("#,###.##") : "";
                }
            }
            set { }
        }

        // CH
        public decimal? PositionCordCutCreel { get; set; }
        // KG
        public decimal? PositionCordCutCreelKG { get; set; }

        public string PositionCordCutCreelValueS
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
                    return string.Format("{0:#,###} CH, {1:#,###.##} KG", PositionCordCutCreel, PositionCordCutCreelKG);
                }
                else
                {
                    return string.Format("{0:#,###} CH, {1:#,###.##} KG", PositionCordCutCreel, PositionCordCutCreelKG);
                }
            }
            set { }
        }

        // CH
        public decimal? PositionCordCutCS { get; set; }
        // KG
        public decimal? PositionCordCutCSKG { get; set; }

        public string PositionCordCutCSValueS
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
                    return string.Format("{0:#,###} CH, {1:#,###.##} KG", PositionCordCutCS, PositionCordCutCSKG);
                }
                else
                {
                    return string.Format("{0:#,###} CH, {1:#,###.##} KG", PositionCordCutCS, PositionCordCutCSKG);
                }
            }
            set { }
        }

        // CH
        public decimal? PositionCordCutWinder { get; set; }
        // KG
        public decimal? PositionCordCutWinderKG { get; set; }

        public string PositionCordCutWinderValueS
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
                    return string.Format("{0:#,###} CH, {1:#,###.##} KG", PositionCordCutWinder, PositionCordCutWinderKG);
                }
                else
                {
                    return string.Format("{0:#,###} CH, {1:#,###.##} KG", PositionCordCutWinder, PositionCordCutWinderKG);
                }
            }
            set { }
        }

        // CH
        public decimal? PositionCordCutWasteYarn { get; set; }
        // KG
        public decimal? PositionCordCutWasteYarnKG { get; set; }

        public string PositionCordCutWasteYarnValueS
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
                    return string.Format("{0:#,###} CH, {1:#,###.##} KG", PositionCordCutWasteYarn, PositionCordCutWasteYarnKG);
                }
                else
                {
                    return string.Format("{0:#,###} CH, {1:#,###.##} KG", PositionCordCutWasteYarn, PositionCordCutWasteYarnKG);
                }
            }
            set { }
        }

        public DateTime? CheckTimeStart { get; set; }
        public string CheckTimeStartS
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
                    return (CheckTimeStart.HasValue) ? CheckTimeStart.Value.ToString("HH:mm") : "";
                }
                else
                {
                    return (CheckTimeStart.HasValue) ? CheckTimeStart.Value.ToString("HH:mm") : "";
                }
            }
            set { }
        }

        public DateTime? CheckTimeFinish { get; set; }
        public string CheckTimeFinishS
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
                    return (CheckTimeFinish.HasValue) ? CheckTimeFinish.Value.ToString("HH:mm") : "";
                }
                else
                {
                    return (CheckTimeFinish.HasValue) ? CheckTimeFinish.Value.ToString("HH:mm") : "";
                }
            }
            set { }
        }

        public DateTime? CheckTimeRecord { get; set; }
        public string CheckTimeRecordS
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
                    return (CheckTimeRecord.HasValue) ? CheckTimeRecord.Value.ToString("HH:mm") : "";
                }
                else
                {
                    return (CheckTimeRecord.HasValue) ? CheckTimeRecord.Value.ToString("HH:mm") : "";
                }
            }
            set { }
        }

        public string Opertor { get; set; }
        public string Leader { get; set; }

        #endregion

        #region Static Methods

        public static S8ProductionConditionItem Create(string productCode)
        {
            var inst = new S8ProductionConditionItem();
            var items = S8ProductionConditionItemStd.Gets(productCode).Value();
            var std = (null != items) ? items.FirstOrDefault() : null;
            Assign(std, inst);
            return inst;
        }

        public static void Assign(S8ProductionConditionItemStd src, S8ProductionConditionItem dst)
        {
            if (null != src && null != dst)
            {
                dst.ProductCode = src.ProductCode;

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

                dst.TreatSC = src.TreatSC;
                dst.Treat = src.Treat;

                dst.DoffingLengthSC = src.DoffingLengthSC;
                dst.DoffingLength = src.DoffingLength;

                dst.WeightSC = src.WeightSC;
                dst.Weight = src.Weight;

                dst.SpindleSC = src.SpindleSC;
                dst.Spindle = src.Spindle;

                dst.ProductionGoodSC = src.ProductionGoodSC;
                dst.ProductionGood = src.ProductionGood;

                dst.ProductionTotalSC = src.ProductionTotalSC;
                dst.ProductionTotal = src.ProductionTotal;
            }
        }

        public static NDbResult<List<S8ProductionConditionItem>> Gets(int? DIPPCId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<S8ProductionConditionItem>> rets = new NDbResult<List<S8ProductionConditionItem>>();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                rets.ErrNum = 8000;
                rets.ErrMsg = msg;

                return rets;
            }

            var p = new DynamicParameters();
            p.Add("@DIPPCId", DIPPCId);

            try
            {
                var items = cnn.Query<S8ProductionConditionItem>("GetS8ProductionConditionItems", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList() : null;

                rets.Success(data);
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                rets.ErrNum = 9999;
                rets.ErrMsg = ex.Message;
            }

            return rets;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The S8ProductionConditionItem item to save.</param>
        /// <returns></returns>
        public static NDbResult<S8ProductionConditionItem> Save(S8ProductionConditionItem value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S8ProductionConditionItem> ret = new NDbResult<S8ProductionConditionItem>();

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
            p.Add("@RowType", value.RowType);

            p.Add("@ProductCode", value.ProductCode);
            p.Add("@LotNo", value.LotNo);
            p.Add("@DoffingDate", value.DoffingDate);
            p.Add("@DoffingNo", value.DoffingNo);


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

            p.Add("@TreatSC", value.TreatSC);
            p.Add("@Treat", value.Treat);
            p.Add("@TreatValue", value.TreatValue);

            p.Add("@DoffingLengthSC", value.DoffingLengthSC);
            p.Add("@DoffingLength", value.DoffingLength);
            p.Add("@DoffingLengthValue", value.DoffingLengthValue);

            p.Add("@WeightSC", value.WeightSC);
            p.Add("@Weight", value.Weight);
            p.Add("@WeightValue", value.WeightValue);

            p.Add("@SpindleSC", value.SpindleSC);
            p.Add("@Spindle", value.Spindle);
            p.Add("@SpindleValue", value.SpindleValue);

            p.Add("@ProductionGoodSC", value.ProductionGoodSC);
            p.Add("@ProductionGood", value.ProductionGood);
            p.Add("@ProductionGoodValue", value.ProductionGoodValue);

            p.Add("@ProductionTotalSC", value.ProductionTotalSC);
            p.Add("@ProductionTotal", value.ProductionTotal);
            p.Add("@ProductionTotalValue", value.ProductionTotalValue);

            p.Add("@ProductionCut", value.ProductionCut);

            p.Add("@PositionCordCutCreel", value.PositionCordCutCreel);
            p.Add("@PositionCordCutCreelKG", value.PositionCordCutCreelKG);
            p.Add("@PositionCordCutCS", value.PositionCordCutCS);
            p.Add("@PositionCordCutCSKG", value.PositionCordCutCSKG);
            p.Add("@PositionCordCutWinder", value.PositionCordCutWinder);
            p.Add("@PositionCordCutWinderKG", value.PositionCordCutWinderKG);
            p.Add("@PositionCordCutWasteYarn", value.PositionCordCutWasteYarn);
            p.Add("@PositionCordCutWasteYarnKG", value.PositionCordCutWasteYarnKG);

            p.Add("@CheckTimeStart", value.CheckTimeStart);
            p.Add("@CheckTimeFinish", value.CheckTimeFinish);
            p.Add("@CheckTimeRecord", value.CheckTimeRecord);

            p.Add("@Opertor", value.Opertor);
            p.Add("@Leader", value.Leader);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveS8ProductionConditionItem", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);

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

        public static NDbResult<S8ProductionConditionItem> GetStdV(int? DIPPCId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S8ProductionConditionItem> ret = new NDbResult<S8ProductionConditionItem>();

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
                var items = cnn.Query<S8ProductionConditionItem>("GetS8ProductionConditionItemStdVRow", p,
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

        public static NDbResult<S8ProductionConditionItem> GetStdB(int? DIPPCId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S8ProductionConditionItem> ret = new NDbResult<S8ProductionConditionItem>();

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
                var items = cnn.Query<S8ProductionConditionItem>("GetS8ProductionConditionItemStdBRow", p,
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

        public static NDbResult SaveStdV(int? DIPPCId, string ProductCode, DateTime Date, 
            string LotNo, int? DoffingNo)
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
            p.Add("@ProductCode", ProductCode);
            p.Add("@DIPPCId", DIPPCId);
            p.Add("@Date", Date);
            p.Add("@LotNo", LotNo);
            p.Add("@DoffingNo", DoffingNo);

            try
            {
                var items = cnn.Query<DIPTimeTable>("SaveS8ProductionConditionItemStdVRow", p,
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

        public static NDbResult SaveStdB(int? DIPPCId, string ProductCode, DateTime Date, 
            string LotNo, int? DoffingNo)
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
            p.Add("@ProductCode", ProductCode);
            p.Add("@DIPPCId", DIPPCId);
            p.Add("@Date", Date);
            p.Add("@LotNo", LotNo);
            p.Add("@DoffingNo", DoffingNo);

            try
            {
                var items = cnn.Query<DIPTimeTable>("SaveS8ProductionConditionItemStdBRow", p,
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
                var items = cnn.Query<DIPTimeTable>("DeleteS8ProductionConditionItemStd", p,
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

        #endregion
    }
}
