#region Using

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using M3.Cord.Models;
using NLib;
using OfficeOpenXml;
using static M3.Cord.AppConsts.Application;

#endregion

namespace M3.Cord.Services.Excels
{
    public class S8Export
    {
        public static void Export(DIPPCCard pcCard, S8ProductionCondition sheet,
            List<S8ProductionConditionItem> items,
            S8WetPickUp pickup, List<S8WetPickUpItem> p1items, List<S8WetPickUpItem> p2items)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            string outputFile = ExcelModel.Dialogs.SaveDialog();
            if (string.IsNullOrEmpty(outputFile))
                return;
            if (null == pcCard)
                return;
            if (null == sheet || null == items || null == pickup || null == p1items || null == p2items)
                return;

            if (!ExcelExportUtils.CreateS8File(outputFile, true))
            {
                M3CordApp.Windows.ExportFailed();
                return;
            }

            try
            {
                using (var package = new ExcelPackage(outputFile))
                {
                    var ws = package.Workbook.Worksheets[0]; // check exists
                    if (null != ws)
                    {
                        #region Header

                        // Date
                        string sDate = (sheet.RecordDate.HasValue) ? 
                            sheet.RecordDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                        ws.Cells["A5"].Value = sDate;

                        // Customer
                        ws.Cells["C5"].Value = pcCard.CustomerName;
                        // Cord Structure
                        ws.Cells["F5"].Value = pcCard.CordStructure;
                        // Process HS
                        ws.Cells["A10"].Value = (sheet.ProcessHS.HasValue && sheet.ProcessHS.Value) ? "P" : string.Empty;
                        // Process DIP
                        ws.Cells["B10"].Value = (sheet.ProcessDIP.HasValue && sheet.ProcessDIP.Value) ? "P" : string.Empty;
                        // Counter
                        ws.Cells["C9"].Value = string.Format("{0} ± {1}", sheet.Counter, sheet.CounterErr);
                        // Item Code
                        ws.Cells["F9"].Value = pcCard.ProductCode;
                        // Lot No
                        ws.Cells["H9"].Value = pcCard.DIPLotNo;

                        // Bath 1 - Solution Name
                        ws.Cells["J6"].Value = sheet.Bath1SolutionName;
                        // Bath 1 - Nip Front
                        ws.Cells["J10"].Value = sheet.Bath1NipFront;
                        // Bath 1 - Nip Back
                        ws.Cells["L10"].Value = string.Format("{0} ± {1}", sheet.Bath1NipBack, sheet.Bath1NipBackErr);

                        // Bath 2 - Solution Name
                        ws.Cells["N6"].Value = sheet.Bath2SolutionName;
                        // Bath 1 - Nip Front
                        ws.Cells["N10"].Value = sheet.Bath2NipFront;
                        // Bath 1 - Nip Back
                        ws.Cells["P10"].Value = string.Format("{0} ± {1}", sheet.Bath2NipBack, sheet.Bath2NipBackErr);

                        // Sofner
                        ws.Cells["R6"].Value = sheet.SofnerValue;
                        // DarwNip
                        ws.Cells["S6"].Value = sheet.DarwNipValue;
                        // Paper Tube Color
                        ws.Cells["R10"].Value = sheet.PaperTubeColorUse;


                        // Tension Zone D
                        ws.Cells["V7"].Value = sheet.TensionD;
                        // Tension Zone H
                        ws.Cells["V8"].Value = sheet.TensionH;
                        // Tension Zone N
                        ws.Cells["V9"].Value = sheet.TensionN;
                        // Tension Winder
                        ws.Cells["V10"].Value = sheet.TensionWinder;

                        #endregion

                        #region Footer

                        // Gas - Before/After/Total
                        ws.Cells["C32"].Value = sheet.GasBefore;
                        ws.Cells["E32"].Value = sheet.GasAfter;
                        ws.Cells["G32"].Value = sheet.GasTotal;
                        // Cooling Warter - Before/After/Total
                        ws.Cells["C34"].Value = sheet.CoolingWarterBefore;
                        ws.Cells["E34"].Value = sheet.CoolingWarterAfter;
                        ws.Cells["G34"].Value = sheet.CoolingWarterTotal;
                        // Air Pressure - Before/After/Total
                        ws.Cells["C35"].Value = sheet.AirPressureBefore;
                        ws.Cells["E35"].Value = sheet.AirPressureAfter;
                        ws.Cells["G35"].Value = sheet.AirPressureTotal;

                        // Bath 1 - Before/After/Total
                        ws.Cells["J31"].Value = sheet.Bath1Before;
                        ws.Cells["J32"].Value = sheet.Bath1After;
                        // Bach 1 - WPU
                        ws.Cells["M31"].Value = sheet.Bath1WPU;

                        // Bath 2 - Before/After/Total
                        ws.Cells["J33"].Value = sheet.Bath1Before;
                        ws.Cells["J34"].Value = sheet.Bath1After;
                        // Bach 2 - WPU
                        ws.Cells["M33"].Value = sheet.Bath2WPU;

                        // Temp D Zone
                        ws.Cells["P33"].Value = sheet.TempDZone1;
                        ws.Cells["P34"].Value = sheet.TempDZone2;
                        ws.Cells["P35"].Value = sheet.TempDZone3;
                        ws.Cells["R33"].Value = sheet.TempDZone4;
                        ws.Cells["R34"].Value = sheet.TempDZone5;
                        ws.Cells["R35"].Value = sheet.TempDZone6;

                        // Temp HN Zone
                        ws.Cells["T33"].Value = sheet.TempHNZone1;
                        ws.Cells["T34"].Value = sheet.TempHNZone2;
                        ws.Cells["T35"].Value = sheet.TempHNZone3;
                        ws.Cells["X33"].Value = sheet.TempHNZone4;
                        ws.Cells["X34"].Value = sheet.TempHNZone5;
                        ws.Cells["X35"].Value = sheet.TempHNZone6;

                        #endregion

                        #region Items

                        int iRow = 0;
                        foreach (var item in items)
                        {
                            if (item.RowType == -2)
                            {
                                // Std
                                ws.Cells["C16"].Value = item.StretchDValueS;
                                ws.Cells["D16"].Value = item.StretchHValueS;
                                ws.Cells["E16"].Value = item.StretchNValueS;
                                ws.Cells["F16"].Value = item.TempDValueS;
                                ws.Cells["G16"].Value = item.TempHNValueS;
                                ws.Cells["H16"].Value = item.SpeedValueS;
                                ws.Cells["I16"].Value = item.TreatValueS;

                                ws.Cells["J16"].Value = item.DoffingLengthValueS;
                                ws.Cells["K16"].Value = item.WeightValueS;
                                ws.Cells["L16"].Value = item.SpindleValueS;
                                ws.Cells["M16"].Value = item.ProductionGoodValueS;
                                ws.Cells["N16"].Value = item.ProductionTotalValueS;
                                ws.Cells["O16"].Value = item.ProductionCutValueS;

                                ws.Cells["P16"].Value = item.PositionCordCutCreelValueS;
                                //ws.Cells["Q16"].Value = item.PositionCordCutCS;
                                ws.Cells["R16"].Value = item.PositionCordCutWinderValueS;
                                ws.Cells["S16"].Value = item.PositionCordCutWasteYarnValueS;
                                ws.Cells["T16"].Value = item.CheckTimeStartS;
                                ws.Cells["U16"].Value = item.CheckTimeFinishS;
                                ws.Cells["V16"].Value = item.CheckTimeRecordS;
                                ws.Cells["W16"].Value = item.Opertor;
                            }
                            else if (item.RowType == -1)
                            {
                                // SC
                                ws.Cells["E15"].Value = item.StretchNValueS;
                                ws.Cells["F15"].Value = item.TempDValueS;
                                ws.Cells["G15"].Value = item.TempHNValueS;
                                ws.Cells["H15"].Value = item.SpeedValueS;
                                ws.Cells["I15"].Value = item.TreatValueS;

                                ws.Cells["J15"].Value = item.DoffingLengthValueS;
                                ws.Cells["K15"].Value = item.WeightValueS;
                                ws.Cells["L15"].Value = item.SpindleValueS;
                                ws.Cells["M15"].Value = item.ProductionGoodValueS;
                                ws.Cells["N15"].Value = item.ProductionTotalValueS;
                                ws.Cells["O15"].Value = item.ProductionCutValueS;

                                ws.Cells["P15"].Value = item.PositionCordCutCreelValueS;
                                //ws.Cells["Q15"].Value = item.PositionCordCutCS;
                                ws.Cells["R15"].Value = item.PositionCordCutWinderValueS;
                                ws.Cells["S15"].Value = item.PositionCordCutWasteYarnValueS;
                                ws.Cells["T15"].Value = item.CheckTimeStartS;
                                ws.Cells["U15"].Value = item.CheckTimeFinishS;
                                ws.Cells["V15"].Value = item.CheckTimeRecordS;
                                ws.Cells["W15"].Value = item.Opertor;
                            }
                            else if (item.RowType == 0)
                            {
                                // Cond
                                ws.Cells["E17"].Value = item.StretchNValueS;
                                ws.Cells["F17"].Value = item.TempDValueS;
                                ws.Cells["G17"].Value = item.TempHNValueS;
                                ws.Cells["H17"].Value = item.SpeedValueS;
                                ws.Cells["I17"].Value = item.TreatValueS;

                                ws.Cells["J17"].Value = item.DoffingLengthValueS;
                                ws.Cells["K17"].Value = item.WeightValueS;
                                ws.Cells["L17"].Value = item.SpindleValueS;
                                ws.Cells["M17"].Value = item.ProductionGoodValueS;
                                ws.Cells["N17"].Value = item.ProductionTotalValueS;
                                ws.Cells["O17"].Value = item.ProductionCutValueS;

                                ws.Cells["P17"].Value = item.PositionCordCutCreelValueS;
                                ws.Cells["Q17"].Value = item.PositionCordCutCS;
                                ws.Cells["R17"].Value = item.PositionCordCutWinderValueS;
                                ws.Cells["S17"].Value = item.PositionCordCutWasteYarnValueS;
                                ws.Cells["T17"].Value = item.CheckTimeStartS;
                                ws.Cells["U17"].Value = item.CheckTimeFinishS;
                                ws.Cells["V17"].Value = item.CheckTimeRecordS;
                                ws.Cells["W17"].Value = item.Opertor;
                            }
                            else
                            {
                                if (iRow > 11) continue;

                                ws.Cells[18 + iRow, 1].Value = item.DoffingDateS;
                                ws.Cells[18 + iRow, 2].Value = item.DoffingNoS;

                                ws.Cells[18 + iRow, 3].Value = item.StretchDValueS;
                                ws.Cells[18 + iRow, 4].Value = item.StretchHValueS;
                                ws.Cells[18 + iRow, 5].Value = item.StretchNValueS;

                                ws.Cells[18 + iRow, 6].Value = item.TempDValueS;
                                ws.Cells[18 + iRow, 7].Value = item.TempHNValueS;
                                ws.Cells[18 + iRow, 8].Value = item.SpeedValueS;
                                ws.Cells[18 + iRow, 9].Value = item.TreatValueS;

                                ws.Cells[18 + iRow, 10].Value = item.DoffingLengthValueS;
                                ws.Cells[18 + iRow, 11].Value = item.WeightValueS;
                                ws.Cells[18 + iRow, 12].Value = item.SpindleValueS;
                                ws.Cells[18 + iRow, 13].Value = item.ProductionGoodValueS;
                                ws.Cells[18 + iRow, 14].Value = item.ProductionTotalValueS;
                                ws.Cells[18 + iRow, 15].Value = item.ProductionCutValueS;

                                ws.Cells[18 + iRow, 16].Value = item.PositionCordCutCreelValueS;
                                ws.Cells[18 + iRow, 17].Value = item.PositionCordCutCSValueS;
                                ws.Cells[18 + iRow, 18].Value = item.PositionCordCutWinderValueS;
                                ws.Cells[18 + iRow, 19].Value = item.PositionCordCutWasteYarnValueS;
                                ws.Cells[18 + iRow, 20].Value = item.CheckTimeStartS;
                                ws.Cells[18 + iRow, 21].Value = item.CheckTimeFinishS;
                                ws.Cells[18 + iRow, 22].Value = item.CheckTimeRecordS;
                                ws.Cells[18 + iRow, 23].Value = item.Opertor;

                                iRow++;
                            }
                        }

                        #endregion
                    }

                    if (package.Workbook.Worksheets.Count >= 2)
                    {
                        var ws2 = package.Workbook.Worksheets[1]; // check exists
                        if (null != ws2)
                        {
                            // Product
                            ws2.Cells["A5"].Value = pcCard.ProductCode;
                            // Customer
                            ws2.Cells["C5"].Value = pcCard.CustomerName;
                            // First DIP Twist 1
                            ws2.Cells["E5"].Value = pickup.FirstDip1;
                            // Solution
                            ws2.Cells["G5"].Value = pickup.SolutionName;

                            // Product
                            ws2.Cells["A22"].Value = pcCard.ProductCode;
                            // Customer
                            ws2.Cells["C22"].Value = pcCard.CustomerName;
                            // First DIP Twist 2
                            ws2.Cells["E22"].Value = pickup.FirstDip2;
                            // Solution
                            ws2.Cells["G22"].Value = pickup.SolutionLotNo;

                            ws2.Cells["N5"].Value = pickup.SpeedSet;
                            ws2.Cells["Q5"].Value = pickup.SpeedActual;

                            ws2.Cells["N6"].Value = pickup.StretchD;
                            ws2.Cells["P6"].Value = pickup.StretchH;
                            ws2.Cells["R6"].Value = pickup.StretchN;

                            ws2.Cells["N7"].Value = pickup.TempD;
                            ws2.Cells["Q7"].Value = pickup.TempHN;


                            ws2.Cells["N9"].Value = (pickup.AMV1.HasValue) ? 
                                string.Format("{0:n2} V (โวลด์)", pickup.AMV1.Value) : null;
                            ws2.Cells["N10"].Value = (pickup.AMV2.HasValue) ?
                                string.Format("{0:n2} V (โวลด์)", pickup.AMV2.Value) : null;
                            ws2.Cells["N11"].Value = (pickup.AMV3.HasValue) ?
                                string.Format("{0:n2} V (โวลด์)", pickup.AMV3.Value) : null;
                            ws2.Cells["N12"].Value = (pickup.AMV4.HasValue) ?
                                string.Format("{0:n2} V (โวลด์)", pickup.AMV4.Value) : null;

                            ws2.Cells["P9"].Value = (pickup.AMAmp1.HasValue) ?
                                string.Format("{0:n2} Amp (แอมแปร์)", pickup.AMAmp1.Value) : null;
                            ws2.Cells["P10"].Value = (pickup.AMAmp2.HasValue) ?
                                string.Format("{0:n2} Amp (แอมแปร์)", pickup.AMAmp2.Value) : null;
                            ws2.Cells["P11"].Value = (pickup.AMAmp3.HasValue) ?
                                string.Format("{0:n2} Amp (แอมแปร์)", pickup.AMAmp3.Value) : null;
                            ws2.Cells["P12"].Value = (pickup.AMAmp4.HasValue) ?
                                string.Format("{0:n2} Amp (แอมแปร์)", pickup.AMAmp4.Value) : null;

                            if (pickup.AMBearing1.HasValue)
                            {
                                ws2.Cells["O15"].Value = (pickup.AMBearing1.Value) ? "P" : "O";
                            }
                            if (pickup.AMBearing2.HasValue)
                            {
                                ws2.Cells["P15"].Value = (pickup.AMBearing2.Value) ? "P" : "O";
                            }
                            if (pickup.AMBearing3.HasValue)
                            {
                                ws2.Cells["Q15"].Value = (pickup.AMBearing3.Value) ? "P" : "O";
                            }
                            if (pickup.AMBearing4.HasValue)
                            {
                                ws2.Cells["R15"].Value = (pickup.AMBearing4.Value) ? "P" : "O";
                            }

                            if (pickup.AMMotorNoise1.HasValue)
                            {
                                ws2.Cells["O16"].Value = (pickup.AMMotorNoise1.Value) ? "P" : "O";
                            }
                            if (pickup.AMMotorNoise2.HasValue)
                            {
                                ws2.Cells["P16"].Value = (pickup.AMMotorNoise2.Value) ? "P" : "O";
                            }
                            if (pickup.AMMotorNoise3.HasValue)
                            {
                                ws2.Cells["Q16"].Value = (pickup.AMMotorNoise3.Value) ? "P" : "O";
                            }
                            if (pickup.AMMotorNoise4.HasValue)
                            {
                                ws2.Cells["R16"].Value = (pickup.AMMotorNoise4.Value) ? "P" : "O";
                            }

                            if (pickup.AMNoVibration1.HasValue)
                            {
                                ws2.Cells["O17"].Value = (pickup.AMNoVibration1.Value) ? "P" : "O";
                            }
                            if (pickup.AMNoVibration2.HasValue)
                            {
                                ws2.Cells["P17"].Value = (pickup.AMNoVibration2.Value) ? "P" : "O";
                            }
                            if (pickup.AMNoVibration3.HasValue)
                            {
                                ws2.Cells["Q17"].Value = (pickup.AMNoVibration3.Value) ? "P" : "O";
                            }
                            if (pickup.AMNoVibration4.HasValue)
                            {
                                ws2.Cells["R17"].Value = (pickup.AMNoVibration4.Value) ? "P" : "O";
                            }

                            if (pickup.BurnerD.HasValue)
                            {
                                ws2.Cells["O20"].Value = (pickup.BurnerD.Value) ? "P" : "O";
                            }
                            if (pickup.BurnerHN.HasValue)
                            {
                                ws2.Cells["Q20"].Value = (pickup.BurnerHN.Value) ? "P" : "O";
                            }

                            if (pickup.GasLeakD.HasValue)
                            {
                                ws2.Cells["O21"].Value = (pickup.GasLeakD.Value) ? "P" : "O";
                            }
                            if (pickup.GasLeakHN.HasValue)
                            {
                                ws2.Cells["Q21"].Value = (pickup.GasLeakHN.Value) ? "P" : "O";
                            }

                            ws2.Cells["O22"].Value = pickup.GasPresureD;
                            ws2.Cells["Q22"].Value = pickup.GasPresureHN;

                            ws2.Cells["O23"].Value = pickup.AirPresureD;
                            ws2.Cells["Q23"].Value = pickup.AirPresureHN;

                            if (pickup.ACMotorExhaustFanD.HasValue)
                            {
                                ws2.Cells["N27"].Value = (pickup.ACMotorExhaustFanD.Value) ? "P" : "O";
                            }
                            if (pickup.ACMotorExhaustFanHN.HasValue)
                            {
                                ws2.Cells["N28"].Value = (pickup.ACMotorExhaustFanHN.Value) ? "P" : "O";
                            }
                            if (pickup.ExhaustFan1.HasValue)
                            {
                                ws2.Cells["N29"].Value = (pickup.ExhaustFan1.Value) ? "P" : "O";
                            }
                            if (pickup.ExhaustFan2.HasValue)
                            {
                                ws2.Cells["N30"].Value = (pickup.ExhaustFan2.Value) ? "P" : "O";
                            }

                            if (pickup.ACMotorSupplyD.HasValue)
                            {
                                ws2.Cells["Q27"].Value = (pickup.ACMotorSupplyD.Value) ? "P" : "O";
                            }
                            if (pickup.ACMotorSupplyHN.HasValue)
                            {
                                ws2.Cells["Q28"].Value = (pickup.ACMotorSupplyHN.Value) ? "P" : "O";
                            }
                            if (pickup.ExhaustFanSupply1.HasValue)
                            {
                                ws2.Cells["Q29"].Value = (pickup.ExhaustFanSupply1.Value) ? "P" : "O";
                            }
                            if (pickup.ExhaustFanSupply2.HasValue)
                            {
                                ws2.Cells["Q30"].Value = (pickup.ExhaustFanSupply2.Value) ? "P" : "O";
                            }

                            // P1 Items
                            int iRow = 0;
                            foreach (var p1 in p1items)
                            {
                                if (iRow > 8) continue;
                                ws2.Cells[11 + iRow, 1].Value = (p1.DoffingDate.HasValue) ?
                                    p1.DoffingDate.Value.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo) : null;
                                ws2.Cells[11 + iRow, 2].Value = p1.DoffNo;
                                ws2.Cells[11 + iRow, 3].Value = p1.NipPressure;
                                ws2.Cells[11 + iRow, 4].Value = (p1.WPU.HasValue) ? p1.WPU.Value.ToString("n2") : null;
                                ws2.Cells[11 + iRow, 5].Value = (p1.FirstAmt.HasValue) ? p1.FirstAmt.Value.ToString("n2") : null;
                                ws2.Cells[11 + iRow, 6].Value = (p1.UseAmt.HasValue) ? p1.UseAmt.Value.ToString("n2") : null;
                                ws2.Cells[11 + iRow, 7].Value = (p1.RestAmt.HasValue) ? p1.RestAmt.Value.ToString("n2") : null;
                                ws2.Cells[11 + iRow, 8].Value = (p1.ThrowAmt.HasValue) ? p1.ThrowAmt.Value.ToString("n2") : null;
                                ws2.Cells[11 + iRow, 9].Value = p1.Operator;

                                iRow++;
                            }

                            // P2 Items
                            iRow = 0;
                            foreach (var p2 in p2items)
                            {
                                if (iRow > 8) continue;
                                ws2.Cells[27 + iRow, 1].Value = (p2.DoffingDate.HasValue) ?
                                    p2.DoffingDate.Value.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo) : null;
                                ws2.Cells[27 + iRow, 2].Value = p2.DoffNo;
                                ws2.Cells[27 + iRow, 3].Value = p2.NipPressure;
                                ws2.Cells[27 + iRow, 4].Value = (p2.WPU.HasValue) ? p2.WPU.Value.ToString("n2") : null;
                                ws2.Cells[27 + iRow, 5].Value = (p2.FirstAmt.HasValue) ? p2.FirstAmt.Value.ToString("n2") : null;
                                ws2.Cells[27 + iRow, 6].Value = (p2.UseAmt.HasValue) ? p2.UseAmt.Value.ToString("n2") : null;
                                ws2.Cells[27 + iRow, 7].Value = (p2.RestAmt.HasValue) ? p2.RestAmt.Value.ToString("n2") : null;
                                ws2.Cells[27 + iRow, 8].Value = (p2.ThrowAmt.HasValue) ? p2.ThrowAmt.Value.ToString("n2") : null;
                                ws2.Cells[27 + iRow, 9].Value = p2.Operator;

                                iRow++;
                            }
                        }
                    }

                    package.Save();
                }
                M3CordApp.Windows.ExportSuccess();
            }
            catch (Exception ex)
            {
                med.Err(ex);
                M3CordApp.Windows.ExportFailed(ex.Message);
            }
        }
    }

    public class S8x2Export
    {
        public static void Export(DIPPCCard pcCard, S8x2ProductionCondition sheet,
            List<S8x2ProductionConditionItem> items, 
            S8x2WetPickUp pickup, List<S8x2WetPickUpItem> p1items, List<S8x2WetPickUpItem> p2items)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            string outputFile = ExcelModel.Dialogs.SaveDialog();
            if (string.IsNullOrEmpty(outputFile))
                return;
            if (null == pcCard)
                return;
            if (null == sheet || null == items || null == pickup || null == p1items || null == p2items)
                return;

            if (!ExcelExportUtils.CreateS8x2File(outputFile, true))
            {
                M3CordApp.Windows.ExportFailed();
                return;
            }

            try
            {
                using (var package = new ExcelPackage(outputFile))
                {
                    var ws = package.Workbook.Worksheets[0]; // check exists
                    if (null != ws)
                    {
                        #region Header

                        // Date
                        string sDate = (sheet.RecordDate.HasValue) ?
                            sheet.RecordDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                        ws.Cells["A5"].Value = sDate;

                        // Customer
                        ws.Cells["C5"].Value = pcCard.CustomerName;
                        // Cord Structure
                        ws.Cells["F5"].Value = pcCard.CordStructure;
                        // Process HS
                        ws.Cells["A10"].Value = (sheet.ProcessHS.HasValue && sheet.ProcessHS.Value) ? "P" : string.Empty;
                        // Process DIP
                        ws.Cells["B10"].Value = (sheet.ProcessDIP.HasValue && sheet.ProcessDIP.Value) ? "P" : string.Empty;
                        // Counter
                        ws.Cells["C9"].Value = string.Format("{0} ± {1}", sheet.Counter, sheet.CounterErr);
                        // Item Code
                        ws.Cells["F9"].Value = pcCard.ProductCode;
                        // Lot No
                        ws.Cells["H9"].Value = pcCard.DIPLotNo;

                        // Bath 1 - Solution Name
                        ws.Cells["J6"].Value = sheet.Bath1SolutionName;
                        // Bath 1 - Nip Front
                        ws.Cells["J10"].Value = sheet.Bath1NipFront;
                        // Bath 1 - Nip Back
                        ws.Cells["L10"].Value = string.Format("{0} ± {1}", sheet.Bath1NipBack, sheet.Bath1NipBackErr);

                        // Bath 2 - Solution Name
                        ws.Cells["N6"].Value = sheet.Bath2SolutionName;
                        // Bath 1 - Nip Front
                        ws.Cells["N10"].Value = sheet.Bath2NipFront;
                        // Bath 1 - Nip Back
                        ws.Cells["P10"].Value = string.Format("{0} ± {1}", sheet.Bath2NipBack, sheet.Bath2NipBackErr);

                        // Sofner
                        ws.Cells["R6"].Value = sheet.SofnerValue;
                        // DarwNip
                        ws.Cells["S6"].Value = sheet.DarwNipValue;
                        // Paper Tube Color
                        ws.Cells["R10"].Value = sheet.PaperTubeColorUse;


                        // Tension Zone D
                        ws.Cells["V7"].Value = sheet.TensionD;
                        // Tension Zone H
                        ws.Cells["V8"].Value = sheet.TensionH;
                        // Tension Zone N
                        ws.Cells["V9"].Value = sheet.TensionN;
                        // Tension Winder
                        ws.Cells["V10"].Value = sheet.TensionWinder;

                        #endregion

                        #region Footer

                        // Gas - Before/After/Total
                        ws.Cells["C32"].Value = sheet.GasBefore;
                        ws.Cells["E32"].Value = sheet.GasAfter;
                        ws.Cells["G32"].Value = sheet.GasTotal;
                        // Cooling Warter - Before/After/Total
                        ws.Cells["C33"].Value = sheet.CoolingWarterBefore;
                        ws.Cells["E33"].Value = sheet.CoolingWarterAfter;
                        ws.Cells["G33"].Value = sheet.CoolingWarterTotal;
                        // Air Pressure - Before/After/Total
                        ws.Cells["C34"].Value = sheet.AirPressureBefore;
                        ws.Cells["E34"].Value = sheet.AirPressureAfter;
                        ws.Cells["G34"].Value = sheet.AirPressureTotal;

                        // Bath 1 - Before/After/Total
                        ws.Cells["J31"].Value = sheet.Bath1Before;
                        ws.Cells["J32"].Value = sheet.Bath1After;
                        // Bach 1 - WPU
                        ws.Cells["L31"].Value = sheet.Bath1WPU;

                        // Bath 2 - Before/After/Total
                        ws.Cells["J33"].Value = sheet.Bath1Before;
                        ws.Cells["J34"].Value = sheet.Bath1After;
                        // Bach 2 - WPU
                        ws.Cells["L33"].Value = sheet.Bath2WPU;

                        // Temp D Zone
                        ws.Cells["N27"].Value = sheet.TempDZone1;
                        ws.Cells["N28"].Value = sheet.TempDZone2;
                        ws.Cells["N29"].Value = sheet.TempDZone3;
                        ws.Cells["N30"].Value = sheet.TempDZone4;
                        ws.Cells["N31"].Value = sheet.TempDZone5;
                        ws.Cells["N32"].Value = sheet.TempDZone6;
                        ws.Cells["N33"].Value = sheet.TempDZone7;
                        ws.Cells["N34"].Value = sheet.TempDZone8;

                        ws.Cells["Q27"].Value = sheet.TempDZone9;
                        ws.Cells["Q28"].Value = sheet.TempDZone10;
                        ws.Cells["Q29"].Value = sheet.TempDZone11;
                        ws.Cells["Q30"].Value = sheet.TempDZone12;
                        ws.Cells["Q31"].Value = sheet.TempDZone13;
                        ws.Cells["Q32"].Value = sheet.TempDZone14;
                        ws.Cells["Q33"].Value = sheet.TempDZone15;

                        // Temp HN Zone
                        ws.Cells["T27"].Value = sheet.TempHNZone1;
                        ws.Cells["T28"].Value = sheet.TempHNZone2;
                        ws.Cells["T29"].Value = sheet.TempHNZone3;
                        ws.Cells["T30"].Value = sheet.TempHNZone4;
                        ws.Cells["T31"].Value = sheet.TempHNZone5;
                        ws.Cells["T32"].Value = sheet.TempHNZone6;
                        ws.Cells["T33"].Value = sheet.TempHNZone7;
                        ws.Cells["T34"].Value = sheet.TempHNZone8;

                        ws.Cells["W27"].Value = sheet.TempHNZone9;
                        ws.Cells["W28"].Value = sheet.TempHNZone10;
                        ws.Cells["W29"].Value = sheet.TempHNZone11;
                        ws.Cells["W30"].Value = sheet.TempHNZone12;
                        ws.Cells["W31"].Value = sheet.TempHNZone13;
                        ws.Cells["W32"].Value = sheet.TempHNZone14;
                        ws.Cells["W33"].Value = sheet.TempHNZone15;

                        #endregion

                        #region Items

                        int iRow = 0;
                        foreach (var item in items)
                        {
                            if (item.RowType == -2)
                            {
                                // Std
                                ws.Cells["C16"].Value = item.StretchDValueS;
                                ws.Cells["D16"].Value = item.StretchHValueS;
                                ws.Cells["E16"].Value = item.StretchNValueS;
                                ws.Cells["F16"].Value = item.TempDValueS;
                                ws.Cells["G16"].Value = item.TempHNValueS;
                                ws.Cells["H16"].Value = item.SpeedValueS;
                                ws.Cells["I16"].Value = item.TreatValueS;

                                ws.Cells["J16"].Value = item.DoffingLengthValueS;
                                ws.Cells["K16"].Value = item.WeightValueS;
                                ws.Cells["L16"].Value = item.SpindleValueS;
                                ws.Cells["M16"].Value = item.ProductionGoodValueS;
                                ws.Cells["N16"].Value = item.ProductionTotalValueS;
                                ws.Cells["O16"].Value = item.ProductionCutValueS;

                                ws.Cells["P16"].Value = item.PositionCordCutCreelValueS;
                                //ws.Cells["Q16"].Value = item.PositionCordCutCS;
                                ws.Cells["R16"].Value = item.PositionCordCutWinderValueS;
                                ws.Cells["S16"].Value = item.PositionCordCutWasteYarnValueS;
                                ws.Cells["T16"].Value = item.CheckTimeStartS;
                                ws.Cells["U16"].Value = item.CheckTimeFinishS;
                                ws.Cells["V16"].Value = item.CheckTimeRecordS;
                                ws.Cells["W16"].Value = item.Opertor;
                            }
                            else if (item.RowType == -1)
                            {
                                // SC
                                ws.Cells["E15"].Value = item.StretchNValueS;
                                ws.Cells["F15"].Value = item.TempDValueS;
                                ws.Cells["G15"].Value = item.TempHNValueS;
                                ws.Cells["H15"].Value = item.SpeedValueS;
                                ws.Cells["I15"].Value = item.TreatValueS;

                                ws.Cells["J15"].Value = item.DoffingLengthValueS;
                                ws.Cells["K15"].Value = item.WeightValueS;
                                ws.Cells["L15"].Value = item.SpindleValueS;
                                ws.Cells["M15"].Value = item.ProductionGoodValueS;
                                ws.Cells["N15"].Value = item.ProductionTotalValueS;
                                ws.Cells["O15"].Value = item.ProductionCutValueS;

                                ws.Cells["P15"].Value = item.PositionCordCutCreelValueS;
                                //ws.Cells["Q15"].Value = item.PositionCordCutCS;
                                ws.Cells["R15"].Value = item.PositionCordCutWinderValueS;
                                ws.Cells["S15"].Value = item.PositionCordCutWasteYarnValueS;
                                ws.Cells["T15"].Value = item.CheckTimeStartS;
                                ws.Cells["U15"].Value = item.CheckTimeFinishS;
                                ws.Cells["V15"].Value = item.CheckTimeRecordS;
                                ws.Cells["W15"].Value = item.Opertor;
                            }
                            else if (item.RowType == 0)
                            {
                                // Cond
                                ws.Cells["E17"].Value = item.StretchNValueS;
                                ws.Cells["F17"].Value = item.TempDValueS;
                                ws.Cells["G17"].Value = item.TempHNValueS;
                                ws.Cells["H17"].Value = item.SpeedValueS;
                                ws.Cells["I17"].Value = item.TreatValueS;

                                ws.Cells["J17"].Value = item.DoffingLengthValueS;
                                ws.Cells["K17"].Value = item.WeightValueS;
                                ws.Cells["L17"].Value = item.SpindleValueS;
                                ws.Cells["M17"].Value = item.ProductionGoodValueS;
                                ws.Cells["N17"].Value = item.ProductionTotalValueS;
                                ws.Cells["O17"].Value = item.ProductionCutValueS;

                                ws.Cells["P17"].Value = item.PositionCordCutCreelValueS;
                                ws.Cells["Q17"].Value = item.PositionCordCutCS;
                                ws.Cells["R17"].Value = item.PositionCordCutWinderValueS;
                                ws.Cells["S17"].Value = item.PositionCordCutWasteYarnValueS;
                                ws.Cells["T17"].Value = item.CheckTimeStartS;
                                ws.Cells["U17"].Value = item.CheckTimeFinishS;
                                ws.Cells["V17"].Value = item.CheckTimeRecordS;
                                ws.Cells["W17"].Value = item.Opertor;
                            }
                            else
                            {
                                if (iRow > 11) continue;
                                ws.Cells[18 + iRow, 1].Value = item.DoffingDateS;
                                ws.Cells[18 + iRow, 2].Value = item.DoffingNoS;

                                ws.Cells[18 + iRow, 3].Value = item.StretchDValueS;
                                ws.Cells[18 + iRow, 4].Value = item.StretchHValueS;
                                ws.Cells[18 + iRow, 5].Value = item.StretchNValueS;

                                ws.Cells[18 + iRow, 6].Value = item.TempDValueS;
                                ws.Cells[18 + iRow, 7].Value = item.TempHNValueS;
                                ws.Cells[18 + iRow, 8].Value = item.SpeedValueS;
                                ws.Cells[18 + iRow, 9].Value = item.TreatValueS;

                                ws.Cells[18 + iRow, 10].Value = item.DoffingLengthValueS;
                                ws.Cells[18 + iRow, 11].Value = item.WeightValueS;
                                ws.Cells[18 + iRow, 12].Value = item.SpindleValueS;
                                ws.Cells[18 + iRow, 13].Value = item.ProductionGoodValueS;
                                ws.Cells[18 + iRow, 14].Value = item.ProductionTotalValueS;
                                ws.Cells[18 + iRow, 15].Value = item.ProductionCutValueS;

                                ws.Cells[18 + iRow, 16].Value = item.PositionCordCutCreelValueS;
                                ws.Cells[18 + iRow, 17].Value = item.PositionCordCutCSValueS;
                                ws.Cells[18 + iRow, 18].Value = item.PositionCordCutWinderValueS;
                                ws.Cells[18 + iRow, 19].Value = item.PositionCordCutWasteYarnValueS;
                                ws.Cells[18 + iRow, 20].Value = item.CheckTimeStartS;
                                ws.Cells[18 + iRow, 21].Value = item.CheckTimeFinishS;
                                ws.Cells[18 + iRow, 22].Value = item.CheckTimeRecordS;
                                ws.Cells[18 + iRow, 23].Value = item.Opertor;

                                iRow++;
                            }
                        }

                        #endregion
                    }

                    if (package.Workbook.Worksheets.Count >= 2)
                    {
                        var ws2 = package.Workbook.Worksheets[1]; // check exists
                        if (null != ws2)
                        {
                            // Product
                            ws2.Cells["A5"].Value = pcCard.ProductCode;
                            // Customer
                            ws2.Cells["C5"].Value = pcCard.CustomerName;
                            // First DIP Twist 1
                            ws2.Cells["E5"].Value = pickup.FirstDip1;
                            // Solution
                            ws2.Cells["G5"].Value = pickup.SolutionName;

                            // Product
                            ws2.Cells["A22"].Value = pcCard.ProductCode;
                            // Customer
                            ws2.Cells["C22"].Value = pcCard.CustomerName;
                            // First DIP Twist 2
                            ws2.Cells["E22"].Value = pickup.FirstDip2;
                            // Solution
                            ws2.Cells["G22"].Value = pickup.SolutionLotNo;

                            // Pull Motor Act Speed
                            ws2.Cells["L6"].Value = (pickup.PullMotorActSpeed1.HasValue) ?
                                string.Format("{0:n2} RPM", pickup.PullMotorActSpeed1.Value) : null;
                            ws2.Cells["M6"].Value = (pickup.PullMotorActSpeed2.HasValue) ?
                                string.Format("{0:n2} RPM", pickup.PullMotorActSpeed2.Value) : null;
                            ws2.Cells["N6"].Value = (pickup.PullMotorActSpeed3.HasValue) ?
                                string.Format("{0:n2} RPM", pickup.PullMotorActSpeed3.Value) : null;
                            ws2.Cells["O6"].Value = (pickup.PullMotorActSpeed4.HasValue) ?
                                string.Format("{0:n2} RPM", pickup.PullMotorActSpeed4.Value) : null;
                            ws2.Cells["P6"].Value = (pickup.PullMotorActSpeed5.HasValue) ?
                                string.Format("{0:n2} RPM", pickup.PullMotorActSpeed5.Value) : null;
                            ws2.Cells["Q6"].Value = (pickup.PullMotorActSpeed6.HasValue) ? 
                                string.Format("{0:n2} RPM", pickup.PullMotorActSpeed6.Value) : null;

                            // Pull Motor Speed SetPoint
                            ws2.Cells["L7"].Value = (pickup.PullMotorSpeedSetPoint1.HasValue) ?
                                string.Format("{0:n2} RPM", pickup.PullMotorSpeedSetPoint1.Value) : null;
                            ws2.Cells["M7"].Value = (pickup.PullMotorSpeedSetPoint2.HasValue) ?
                                string.Format("{0:n2} RPM", pickup.PullMotorSpeedSetPoint2.Value) : null;
                            ws2.Cells["N7"].Value = (pickup.PullMotorSpeedSetPoint3.HasValue) ?
                                string.Format("{0:n2} RPM", pickup.PullMotorSpeedSetPoint3.Value) : null;
                            ws2.Cells["O7"].Value = (pickup.PullMotorSpeedSetPoint4.HasValue) ?
                                string.Format("{0:n2} RPM", pickup.PullMotorSpeedSetPoint4.Value) : null;
                            ws2.Cells["P7"].Value = (pickup.PullMotorSpeedSetPoint5.HasValue) ?
                                string.Format("{0:n2} RPM", pickup.PullMotorSpeedSetPoint5.Value) : null;
                            ws2.Cells["Q7"].Value = (pickup.PullMotorSpeedSetPoint6.HasValue) ?
                                string.Format("{0:n2} RPM", pickup.PullMotorSpeedSetPoint6.Value) : null;

                            // Pull Motor Act Current
                            ws2.Cells["L8"].Value = (pickup.PullMotorActCurrent1.HasValue) ?
                                string.Format("{0:n2} A", pickup.PullMotorActCurrent1.Value) : null;
                            ws2.Cells["M8"].Value = (pickup.PullMotorActCurrent2.HasValue) ?
                                string.Format("{0:n2} A", pickup.PullMotorActCurrent2.Value) : null;
                            ws2.Cells["N8"].Value = (pickup.PullMotorActCurrent3.HasValue) ?
                                string.Format("{0:n2} A", pickup.PullMotorActCurrent3.Value) : null;
                            ws2.Cells["O8"].Value = (pickup.PullMotorActCurrent4.HasValue) ?
                                string.Format("{0:n2} A", pickup.PullMotorActCurrent4.Value) : null;
                            ws2.Cells["P8"].Value = (pickup.PullMotorActCurrent5.HasValue) ?
                                string.Format("{0:n2} A", pickup.PullMotorActCurrent5.Value) : null;
                            ws2.Cells["Q8"].Value = (pickup.PullMotorActCurrent6.HasValue) ?
                                string.Format("{0:n2} A", pickup.PullMotorActCurrent6.Value) : null;

                            // Pull Motor Pct Current
                            ws2.Cells["L9"].Value = (pickup.PullMotorPctCurrent1.HasValue) ?
                                string.Format("{0:n2} A", pickup.PullMotorPctCurrent1.Value) : null;
                            ws2.Cells["M9"].Value = (pickup.PullMotorPctCurrent2.HasValue) ?
                                string.Format("{0:n2} A", pickup.PullMotorPctCurrent2.Value) : null;
                            ws2.Cells["N9"].Value = (pickup.PullMotorPctCurrent3.HasValue) ?
                                string.Format("{0:n2} A", pickup.PullMotorPctCurrent3.Value) : null;
                            ws2.Cells["O9"].Value = (pickup.PullMotorPctCurrent4.HasValue) ?
                                string.Format("{0:n2} A", pickup.PullMotorPctCurrent4.Value) : null;
                            ws2.Cells["P9"].Value = (pickup.PullMotorPctCurrent5.HasValue) ?
                                string.Format("{0:n2} A", pickup.PullMotorPctCurrent5.Value) : null;
                            ws2.Cells["Q9"].Value = (pickup.PullMotorPctCurrent6.HasValue) ?
                                string.Format("{0:n2} A", pickup.PullMotorPctCurrent6.Value) : null;

                            // Oven Fan Act Speed
                            ws2.Cells["L13"].Value = (pickup.OvenCirculatingFanActSpeed1.HasValue) ?
                                string.Format("{0:n2} Hz", pickup.OvenCirculatingFanActSpeed1.Value) : null;
                            ws2.Cells["M13"].Value = (pickup.OvenCirculatingFanActSpeed2.HasValue) ?
                                string.Format("{0:n2} Hz", pickup.OvenCirculatingFanActSpeed2.Value) : null;
                            ws2.Cells["N13"].Value = (pickup.OvenExhaustFanActSpeed1.HasValue) ?
                                string.Format("{0:n2} Hz", pickup.OvenExhaustFanActSpeed1.Value) : null;
                            ws2.Cells["O13"].Value = (pickup.OvenExhaustFanActSpeed2.HasValue) ?
                                string.Format("{0:n2} Hz", pickup.OvenExhaustFanActSpeed2.Value) : null;
                            ws2.Cells["P13"].Value = (pickup.OvenExhaustFanFrontActSpeed.HasValue) ?
                                string.Format("{0:n2} Hz", pickup.OvenExhaustFanFrontActSpeed.Value) : null;
                            ws2.Cells["Q13"].Value = (pickup.OvenExhaustFanBackActSpeed.HasValue) ?
                                string.Format("{0:n2} Hz", pickup.OvenExhaustFanBackActSpeed.Value) : null;

                            // Oven Fan Set Point
                            ws2.Cells["L14"].Value = (pickup.OvenCirculatingFanSpeedSetpoint1.HasValue) ?
                                string.Format("{0:n2} Hz", pickup.OvenCirculatingFanSpeedSetpoint1.Value) : null;
                            ws2.Cells["M14"].Value = (pickup.OvenCirculatingFanSpeedSetpoint2.HasValue) ?
                                string.Format("{0:n2} Hz", pickup.OvenCirculatingFanSpeedSetpoint2.Value) : null;
                            ws2.Cells["N14"].Value = (pickup.OvenExhaustFanSpeedSetpoint1.HasValue) ?
                                string.Format("{0:n2} Hz", pickup.OvenExhaustFanSpeedSetpoint1.Value) : null;
                            ws2.Cells["O14"].Value = (pickup.OvenExhaustFanSpeedSetpoint2.HasValue) ?
                                string.Format("{0:n2} Hz", pickup.OvenExhaustFanSpeedSetpoint2.Value) : null;
                            ws2.Cells["P14"].Value = (pickup.OvenExhaustFanFrontSpeedSetpoint.HasValue) ?
                                string.Format("{0:n2} Hz", pickup.OvenExhaustFanFrontSpeedSetpoint.Value) : null;
                            ws2.Cells["Q14"].Value = (pickup.OvenExhaustFanBackSpeedSetpoint.HasValue) ?
                                string.Format("{0:n2} Hz", pickup.OvenExhaustFanBackSpeedSetpoint.Value) : null;

                            // Oven Fan Act Current
                            ws2.Cells["L15"].Value = (pickup.OvenCirculatingFanActCurrent1.HasValue) ?
                                string.Format("{0:n2} A", pickup.OvenCirculatingFanActCurrent1.Value) : null;
                            ws2.Cells["M15"].Value = (pickup.OvenCirculatingFanActCurrent2.HasValue) ?
                                string.Format("{0:n2} A", pickup.OvenCirculatingFanActCurrent2.Value) : null;
                            ws2.Cells["N15"].Value = (pickup.OvenExhaustFanActCurrent1.HasValue) ?
                                string.Format("{0:n2} A", pickup.OvenExhaustFanActCurrent1.Value) : null;
                            ws2.Cells["O15"].Value = (pickup.OvenExhaustFanActCurrent2.HasValue) ?
                                string.Format("{0:n2} A", pickup.OvenExhaustFanActCurrent2.Value) : null;
                            ws2.Cells["P15"].Value = (pickup.OvenExhaustFanFrontActCurrent.HasValue) ?
                                string.Format("{0:n2} A", pickup.OvenExhaustFanFrontActCurrent.Value) : null;
                            ws2.Cells["Q15"].Value = (pickup.OvenExhaustFanBackActCurrent.HasValue) ?
                                string.Format("{0:n2} A", pickup.OvenExhaustFanBackActCurrent.Value) : null;

                            // Oven Fan Pct Current
                            ws2.Cells["L16"].Value = (pickup.OvenCirculatingFanPctCurrent1.HasValue) ?
                                string.Format("{0:n2} %", pickup.OvenCirculatingFanPctCurrent1.Value) : null;
                            ws2.Cells["M16"].Value = (pickup.OvenCirculatingFanPctCurrent2.HasValue) ?
                                string.Format("{0:n2} %", pickup.OvenCirculatingFanPctCurrent2.Value) : null;
                            ws2.Cells["N16"].Value = (pickup.OvenExhaustFanPctCurrent1.HasValue) ?
                                string.Format("{0:n2} %", pickup.OvenExhaustFanPctCurrent1.Value) : null;
                            ws2.Cells["O16"].Value = (pickup.OvenExhaustFanPctCurrent2.HasValue) ?
                                string.Format("{0:n2} %", pickup.OvenExhaustFanPctCurrent2.Value) : null;
                            ws2.Cells["P16"].Value = (pickup.OvenExhaustFanFrontPctCurrent.HasValue) ?
                                string.Format("{0:n2} %", pickup.OvenExhaustFanFrontPctCurrent.Value) : null;
                            ws2.Cells["Q16"].Value = (pickup.OvenExhaustFanBackPctCurrent.HasValue) ?
                                string.Format("{0:n2} %", pickup.OvenExhaustFanBackPctCurrent.Value) : null;

                            // Motor Strech and Tension - Act Stretch
                            ws2.Cells["M20"].Value = (pickup.MotorActStretch1x2.HasValue) ?
                                string.Format("{0:n2} %", pickup.MotorActStretch1x2.Value) : null;
                            ws2.Cells["N20"].Value = (pickup.MotorActStretch3x4.HasValue) ?
                                string.Format("{0:n2} %", pickup.MotorActStretch3x4.Value) : null;
                            ws2.Cells["O20"].Value = (pickup.MotorActStretch4x5.HasValue) ?
                                string.Format("{0:n2} %", pickup.MotorActStretch4x5.Value) : null;
                            ws2.Cells["P20"].Value = (pickup.MotorActStretch1x6.HasValue) ?
                                string.Format("{0:n2} %", pickup.MotorActStretch1x6.Value) : null;

                            // Motor Strech and Tension - Set point
                            ws2.Cells["M21"].Value = (pickup.MotorSetPoint1x2.HasValue) ?
                                string.Format("{0:n2} %", pickup.MotorSetPoint1x2.Value) : null;
                            ws2.Cells["N21"].Value = (pickup.MotorSetPoint3x4.HasValue) ?
                                string.Format("{0:n2} %", pickup.MotorSetPoint3x4.Value) : null;
                            ws2.Cells["O21"].Value = (pickup.MotorSetPoint4x5.HasValue) ?
                                string.Format("{0:n2} %", pickup.MotorSetPoint4x5.Value) : null;
                            ws2.Cells["P21"].Value = (pickup.MotorSetPoint1x6.HasValue) ?
                                string.Format("{0:n2} %", pickup.MotorSetPoint1x6.Value) : null;

                            // Motor Strech and Tension - Act total tension
                            ws2.Cells["M22"].Value = (pickup.MotorActTensionTotal1x2.HasValue) ?
                                string.Format("{0:n2} Kg", pickup.MotorActTensionTotal1x2.Value) : null;
                            ws2.Cells["N22"].Value = (pickup.MotorActTensionTotal3x4.HasValue) ?
                                string.Format("{0:n2} Kg", pickup.MotorActTensionTotal3x4.Value) : null;
                            ws2.Cells["O22"].Value = (pickup.MotorActTensionTotal4x5.HasValue) ?
                                string.Format("{0:n2} Kg", pickup.MotorActTensionTotal4x5.Value) : null;

                            // Motor Strech and Tension - Act single cord tension
                            ws2.Cells["M23"].Value = (pickup.MotorSingleTension1x2.HasValue) ?
                                string.Format("{0:n2} Kg", pickup.MotorSingleTension1x2.Value) : null;
                            ws2.Cells["N23"].Value = (pickup.MotorSingleTension3x4.HasValue) ?
                                string.Format("{0:n2} Kg", pickup.MotorSingleTension3x4.Value) : null;
                            ws2.Cells["O23"].Value = (pickup.MotorSingleTension4x5.HasValue) ?
                                string.Format("{0:n2} Kg", pickup.MotorSingleTension4x5.Value) : null;
                            
                            // Remark
                            ws2.Cells["K26"].Value = pickup.Remark;

                            // P1 Items
                            int iRow = 0;
                            foreach (var p1 in p1items)
                            {
                                if (iRow > 8) continue;
                                ws2.Cells[11 + iRow, 1].Value = (p1.DoffingDate.HasValue) ?
                                    p1.DoffingDate.Value.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo) : null;
                                ws2.Cells[11 + iRow, 2].Value = p1.DoffNo;
                                ws2.Cells[11 + iRow, 3].Value = p1.NipPressure;
                                ws2.Cells[11 + iRow, 4].Value = (p1.WPU.HasValue) ? p1.WPU.Value.ToString("n2") : null;
                                ws2.Cells[11 + iRow, 5].Value = (p1.FirstAmt.HasValue) ? p1.FirstAmt.Value.ToString("n2") : null;
                                ws2.Cells[11 + iRow, 6].Value = (p1.UseAmt.HasValue) ? p1.UseAmt.Value.ToString("n2") : null;
                                ws2.Cells[11 + iRow, 7].Value = (p1.RestAmt.HasValue) ? p1.RestAmt.Value.ToString("n2") : null;
                                ws2.Cells[11 + iRow, 8].Value = (p1.ThrowAmt.HasValue) ? p1.ThrowAmt.Value.ToString("n2") : null;
                                ws2.Cells[11 + iRow, 9].Value = p1.Operator;

                                iRow++;
                            }

                            // P2 Items
                            iRow = 0;
                            foreach (var p2 in p2items)
                            {
                                if (iRow > 8) continue;
                                ws2.Cells[27 + iRow, 1].Value = (p2.DoffingDate.HasValue) ?
                                    p2.DoffingDate.Value.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo) : null;
                                ws2.Cells[27 + iRow, 2].Value = p2.DoffNo;
                                ws2.Cells[27 + iRow, 3].Value = p2.NipPressure;
                                ws2.Cells[27 + iRow, 4].Value = (p2.WPU.HasValue) ? p2.WPU.Value.ToString("n2") : null;
                                ws2.Cells[27 + iRow, 5].Value = (p2.FirstAmt.HasValue) ? p2.FirstAmt.Value.ToString("n2") : null;
                                ws2.Cells[27 + iRow, 6].Value = (p2.UseAmt.HasValue) ? p2.UseAmt.Value.ToString("n2") : null;
                                ws2.Cells[27 + iRow, 7].Value = (p2.RestAmt.HasValue) ? p2.RestAmt.Value.ToString("n2") : null;
                                ws2.Cells[27 + iRow, 8].Value = (p2.ThrowAmt.HasValue) ? p2.ThrowAmt.Value.ToString("n2") : null;
                                ws2.Cells[27 + iRow, 9].Value = p2.Operator;

                                iRow++;
                            }
                        }
                    }

                    package.Save();
                }
                M3CordApp.Windows.ExportSuccess();
            }
            catch (Exception ex)
            {
                med.Err(ex);
                M3CordApp.Windows.ExportFailed(ex.Message);
            }
        }
    }
}
