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
            List<S8ProductionConditionItem> items)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            string outputFile = ExcelModel.Dialogs.SaveDialog();
            if (string.IsNullOrEmpty(outputFile))
                return;
            if (null == pcCard)
                return;
            if (null == sheet || null == items)
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
                        ws.Cells["R6"].Value = (sheet.Sofner.HasValue && sheet.Sofner.Value) ? "P" : string.Empty;
                        // DarwNip
                        ws.Cells["S6"].Value = (sheet.DarwNip.HasValue && sheet.DarwNip.Value) ? "P" : string.Empty;
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
                        ws.Cells["C34"].Value = sheet.CoolingWarterAfter;
                        ws.Cells["C34"].Value = sheet.CoolingWarterTotal;
                        // Air Pressure - Before/After/Total
                        ws.Cells["C34"].Value = sheet.AirPressureBefore;
                        ws.Cells["C34"].Value = sheet.AirPressureAfter;
                        ws.Cells["C34"].Value = sheet.AirPressureTotal;

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

                                ws.Cells[18 + iRow, 3].Value = item.StretchNValueS;
                                ws.Cells[18 + iRow, 4].Value = item.TempDValueS;
                                ws.Cells[18 + iRow, 5].Value = item.TempHNValueS;
                                ws.Cells[18 + iRow, 6].Value = item.SpeedValueS;
                                ws.Cells[18 + iRow, 7].Value = item.TreatValueS;
                                         
                                ws.Cells[18 + iRow, 8].Value = item.DoffingLengthValueS;
                                ws.Cells[18 + iRow, 9].Value = item.WeightValueS;
                                ws.Cells[18 + iRow, 10].Value = item.SpindleValueS;
                                ws.Cells[18 + iRow, 11].Value = item.ProductionGoodValueS;
                                ws.Cells[18 + iRow, 12].Value = item.ProductionTotalValueS;
                                ws.Cells[18 + iRow, 13].Value = item.ProductionCutValueS;
                                         
                                ws.Cells[18 + iRow, 14].Value = item.PositionCordCutCreelValueS;
                                ws.Cells[18 + iRow, 15].Value = item.PositionCordCutCS;
                                ws.Cells[18 + iRow, 16].Value = item.PositionCordCutWinderValueS;
                                ws.Cells[18 + iRow, 17].Value = item.PositionCordCutWasteYarnValueS;
                                ws.Cells[18 + iRow, 18].Value = item.CheckTimeStartS;
                                ws.Cells[18 + iRow, 19].Value = item.CheckTimeFinishS;
                                ws.Cells[18 + iRow, 20].Value = item.CheckTimeRecordS;
                                ws.Cells[18 + iRow, 21].Value = item.Opertor;

                                iRow++;
                            }
                        }

                        #endregion
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
