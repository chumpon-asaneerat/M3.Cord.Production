#region Using

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using M3.Cord.Models;
using NLib;
using NLib.Models;
using OfficeOpenXml;

#endregion

namespace M3.Cord.Services.Excels
{
    public class SolutionExports
    {
        public static void Export(string solutionlot, int solutionId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            if (string.IsNullOrWhiteSpace(solutionlot))
                return;

            string outputFile = ExcelModel.Dialogs.SaveDialog();
            if (string.IsNullOrEmpty(outputFile))
            {
                return;
            }

            var lots = SolutionLotLabel.Gets(solutionlot).Value();
            SolutionLotLabel lot = null;

            if (lots == null && lots.Count <= 0)
            {
                M3CordApp.Windows.ExportFailed();
                return;
            }
            else
            {
                foreach (var item in lots)
                {
                    if (item.SolutionId == solutionId)
                    {
                        lot = item;
                        break;
                    }
                }
            }

            if (null == lot)
            {
                M3CordApp.Windows.ExportFailed();
                return;
            }

            if (solutionId == 14 || solutionId == 15)
            {
                #region SolutionId 14,15 AX-09PV3

                if (!ExcelExportUtils.CreateSolutionAX09PV3File(outputFile, true))
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

                            // Mixed Date
                            ws.Cells["I1"].Value = (lot.MixDate.HasValue) ?
                                lot.MixDate.Value.ToString("dd/MM/yyyy") : null;
                            // Customer
                            //ws.Cells["A2"].Value = string.Format("ลูกค้า / Customer : {0}", lot.CustomerName);

                            // Product code
                            ws.Cells["A3"].Value = string.Format("ผลิตภัณฑ์ / Product item : {0}", lot.ProductCode);
                            ws.Cells["A44"].Value = string.Format("ผลิตภัณฑ์ / Product item : {0}", lot.ProductCode);

                            // Lot No
                            ws.Cells["B4"].Value = lot.SolutionLot;
                            // Weight Qty
                            ws.Cells["G4"].Value = (lot.DipSolutionQty.HasValue) ?
                                lot.DipSolutionQty.Value : new decimal?();
                            // Date
                            ws.Cells["I3"].Value = (lot.MixDate.HasValue) ?
                                lot.MixDate.Value.ToString("dd/MM/yyyy") : null;

                            #endregion

                            string receipe = null;
                            var lotDetails = SolutionLotDetail.Gets(solutionlot, lot.SolutionId, receipe).Value();

                            int iRow = -1;

                            #region Items - RF

                            iRow = 9;
                            foreach (var item in lotDetails)
                            {
                                if (item.Recipe != "RF") continue;
                                if (iRow > 12) continue;

                                // Chem No
                                ws.Cells[iRow, 2].Value = (string.IsNullOrWhiteSpace(item.ChemicalNo) || item.ChemicalNo == "0") ?
                                    "-" : item.ChemicalNo;
                                // Chem Lot No
                                ws.Cells[iRow, 3].Value = item.ChemicalLot;
                                // Chem Name
                                ws.Cells[iRow, 4].Value = item.ChemicalName;

                                // Chem Wet Kg
                                if (item.ChemWet.HasValue)
                                {
                                    ws.Cells[iRow, 5].Value = item.ChemWet.Value;
                                }
                                else
                                {
                                    ws.Cells[iRow, 5].Value = new decimal?();
                                }
                                // Chem Dry Kg
                                if (item.ChemDry.HasValue)
                                {
                                    ws.Cells[iRow, 6].Value = item.ChemDry.Value;
                                }
                                else
                                {
                                    ws.Cells[iRow, 6].Value = new decimal?();
                                }
                                /*
                                // Chem Wet (calc) Kg
                                if (item.WeightCal.HasValue)
                                {
                                    ws.Cells[iRow, 7].Value = item.WeightCal.Value;
                                }
                                else ws.Cells[iRow, 7].Value = new decimal?();
                                */
                                iRow++;
                            }

                            #endregion

                            #region Items - RFL

                            iRow = 21;
                            //totalCalc = decimal.Zero;
                            foreach (var item in lotDetails)
                            {
                                if (item.Recipe != "RFL") continue;
                                if (iRow > 24) continue;

                                // Chem No
                                ws.Cells[iRow, 2].Value = (string.IsNullOrWhiteSpace(item.ChemicalNo) || item.ChemicalNo == "0") ?
                                    "-" : item.ChemicalNo;
                                // Chem Lot No
                                ws.Cells[iRow, 3].Value = item.ChemicalLot;
                                // Chem Name
                                ws.Cells[iRow, 4].Value = item.ChemicalName;

                                // Chem Wet Kg
                                if (item.ChemWet.HasValue)
                                {
                                    ws.Cells[iRow, 5].Value = item.ChemWet.Value;
                                }
                                else
                                {
                                    ws.Cells[iRow, 5].Value = new decimal?();
                                }
                                // Chem Dry Kg
                                if (item.ChemDry.HasValue)
                                {
                                    ws.Cells[iRow, 6].Value = item.ChemDry.Value;
                                }
                                else
                                {
                                    ws.Cells[iRow, 6].Value = new decimal?();
                                }
                                /*
                                // Chem Wet (calc) Kg
                                if (item.WeightCal.HasValue)
                                {
                                    ws.Cells[iRow, 7].Value = item.WeightCal.Value;
                                }
                                else ws.Cells[iRow, 7].Value = new decimal?();
                                */
                                iRow++;
                            }

                            #endregion

                            #region Items - FINAL

                            iRow = 50;
                            //totalCalc = decimal.Zero;
                            foreach (var item in lotDetails)
                            {
                                if (item.Recipe != "FINAL") continue;
                                if (iRow > 52) continue;

                                // Chem No
                                ws.Cells[iRow, 2].Value = (string.IsNullOrWhiteSpace(item.ChemicalNo) || item.ChemicalNo == "0") ?
                                    "-" : item.ChemicalNo;
                                // Chem Lot No
                                ws.Cells[iRow, 3].Value = item.ChemicalLot;
                                // Chem Name
                                ws.Cells[iRow, 4].Value = item.ChemicalName;

                                // Chem Wet Kg
                                if (item.ChemWet.HasValue)
                                {
                                    ws.Cells[iRow, 5].Value = item.ChemWet.Value;
                                }
                                else
                                {
                                    ws.Cells[iRow, 5].Value = new decimal?();
                                }
                                // Chem Dry Kg
                                if (item.ChemDry.HasValue)
                                {
                                    ws.Cells[iRow, 6].Value = item.ChemDry.Value;
                                }
                                else
                                {
                                    ws.Cells[iRow, 6].Value = new decimal?();
                                }
                                /*
                                // Chem Wet (calc) Kg
                                if (item.WeightCal.HasValue)
                                {
                                    ws.Cells[iRow, 7].Value = item.WeightCal.Value;
                                }
                                else ws.Cells[iRow, 7].Value = new decimal?();
                                */
                                iRow++;
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

                #endregion
            }
            else if (solutionId == 5 || solutionId == 7 || solutionId == 13)
            {
                #region SolutionId 5,7,13 - KT-02

                if (!ExcelExportUtils.CreateSolutionKT02File(outputFile, true))
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

                            // Customer
                            //ws.Cells["A3"].Value = string.Format("ลูกค้า / Customer : {0}", lot.CustomerName);

                            // Product code
                            ws.Cells["A4"].Value = string.Format("ผลิตภัณฑ์ / Product item : {0}", lot.ProductCode);

                            // Lot No
                            ws.Cells["C5"].Value = lot.SolutionLot;
                            // Weight Qty
                            ws.Cells["H5"].Value = (lot.DipSolutionQty.HasValue) ?
                                lot.DipSolutionQty.Value : new decimal?();
                            // Mixed Date
                            ws.Cells["J2"].Value = (lot.MixDate.HasValue) ?
                                lot.MixDate.Value.ToString("dd/MM/yyyy") : null;

                            #endregion

                            string receipe = null;
                            var lotDetails = SolutionLotDetail.Gets(solutionlot, lot.SolutionId, receipe).Value();

                            int iRow = -1;

                            #region Items - E-SOLUTION

                            iRow = 10;
                            foreach (var item in lotDetails)
                            {
                                if (item.Recipe.ToUpper() != "E-SOLUTION") continue;
                                if (iRow > 13) continue;

                                // Chem No
                                ws.Cells[iRow, 2].Value = (string.IsNullOrWhiteSpace(item.ChemicalNo) || item.ChemicalNo == "0") ?
                                    "-" : item.ChemicalNo;
                                // Chem Lot No
                                ws.Cells[iRow, 3].Value = item.ChemicalLot;
                                // Chem Name
                                ws.Cells[iRow, 4].Value = item.ChemicalName;

                                // Chem Wet Kg
                                if (item.ChemWet.HasValue)
                                {
                                    ws.Cells[iRow, 5].Value = item.ChemWet.Value;
                                }
                                else
                                {
                                    ws.Cells[iRow, 5].Value = new decimal?();
                                }
                                // Chem Dry Kg
                                if (item.ChemDry.HasValue)
                                {
                                    ws.Cells[iRow, 6].Value = item.ChemDry.Value;
                                }
                                else
                                {
                                    ws.Cells[iRow, 6].Value = new decimal?();
                                }
                                /*
                                // Chem Wet (calc) Kg
                                if (item.WeightCal.HasValue)
                                {
                                    ws.Cells[iRow, 7].Value = item.WeightCal.Value;
                                }
                                else ws.Cells[iRow, 7].Value = new decimal?();
                                */
                                iRow++;
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

                #endregion
            }
            else if (solutionId == 9 || solutionId == 10)
            {
                #region SolutionId 9,10 - MX520D

                if (!ExcelExportUtils.CreateSolutionMX520DFile(outputFile, true))
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

                            // Customer
                            //ws.Cells["A2"].Value = string.Format("ลูกค้า / Customer : {0}", lot.CustomerName);

                            // Product code
                            ws.Cells["A3"].Value = string.Format("ผลิตภัณฑ์ / Product item : {0}", lot.ProductCode);
                            ws.Cells["A40"].Value = string.Format("ผลิตภัณฑ์ / Product item : {0}", lot.ProductCode);

                            // Lot No
                            ws.Cells["C4"].Value = lot.SolutionLot;
                            // Weight Qty
                            ws.Cells["H4"].Value = (lot.DipSolutionQty.HasValue) ?
                                lot.DipSolutionQty.Value : new decimal?();
                            // Mixed Date
                            ws.Cells["J3"].Value = (lot.MixDate.HasValue) ?
                                lot.MixDate.Value.ToString("dd/MM/yyyy") : null;

                            #endregion

                            string receipe = null;
                            var lotDetails = SolutionLotDetail.Gets(solutionlot, lot.SolutionId, receipe).Value();

                            int iRow = -1;

                            #region Items - RFL

                            iRow = 9;
                            foreach (var item in lotDetails)
                            {
                                if (item.Recipe != "RFL") continue;
                                if (iRow > 13) continue;

                                // Chem No
                                ws.Cells[iRow, 2].Value = (string.IsNullOrWhiteSpace(item.ChemicalNo) || item.ChemicalNo == "0") ?
                                    "-" : item.ChemicalNo;
                                // Chem Lot No
                                ws.Cells[iRow, 3].Value = item.ChemicalLot;
                                // Chem Name
                                ws.Cells[iRow, 4].Value = item.ChemicalName;

                                // Chem Wet Kg
                                if (item.ChemWet.HasValue)
                                {
                                    ws.Cells[iRow, 5].Value = item.ChemWet.Value;
                                }
                                else
                                {
                                    ws.Cells[iRow, 5].Value = new decimal?();
                                }
                                // Chem Dry Kg
                                if (item.ChemDry.HasValue)
                                {
                                    ws.Cells[iRow, 6].Value = item.ChemDry.Value;
                                }
                                else
                                {
                                    ws.Cells[iRow, 6].Value = new decimal?();
                                }
                                /*
                                // Chem Wet (calc) Kg
                                if (item.WeightCal.HasValue)
                                {
                                    ws.Cells[iRow, 7].Value = item.WeightCal.Value;
                                }
                                else ws.Cells[iRow, 7].Value = new decimal?();
                                */
                                iRow++;
                            }

                            #endregion

                            #region Items - P-RFL

                            iRow = 22; // Only RFL item
                            foreach (var item in lotDetails)
                            {
                                if (item.Recipe != "P-RFL" && item.ChemicalName != "RFL") continue;
                                // Chem No
                                ws.Cells[iRow, 2].Value = "-";
                                // Chem Lot No
                                ws.Cells[iRow, 3].Value = "-";
                                // Chem Name
                                ws.Cells[iRow, 4].Value = item.ChemicalName;

                                // Chem Wet Kg
                                if (item.ChemWet.HasValue)
                                {
                                    ws.Cells[iRow, 5].Value = item.ChemWet.Value;
                                }
                                else
                                {
                                    ws.Cells[iRow, 5].Value = new decimal?();
                                }
                                // Chem Dry Kg
                                if (item.ChemDry.HasValue)
                                {
                                    ws.Cells[iRow, 6].Value = item.ChemDry.Value;
                                }
                                else
                                {
                                    ws.Cells[iRow, 6].Value = new decimal?();
                                }
                            }

                            iRow = 23; // item other than RFL
                            //totalCalc = decimal.Zero;
                            foreach (var item in lotDetails)
                            {
                                if (item.Recipe != "P-RFL") continue;
                                if (item.ChemicalName == "RFL") continue;
                                if (iRow > 25) continue;

                                // Chem No
                                ws.Cells[iRow, 2].Value = (string.IsNullOrWhiteSpace(item.ChemicalNo) || item.ChemicalNo == "0") ?
                                    "-" : item.ChemicalNo;
                                // Chem Lot No
                                ws.Cells[iRow, 3].Value = item.ChemicalLot;
                                // Chem Name
                                ws.Cells[iRow, 4].Value = item.ChemicalName;

                                // Chem Wet Kg
                                if (item.ChemWet.HasValue)
                                {
                                    ws.Cells[iRow, 5].Value = item.ChemWet.Value;
                                }
                                else
                                {
                                    ws.Cells[iRow, 5].Value = new decimal?();
                                }
                                // Chem Dry Kg
                                if (item.ChemDry.HasValue)
                                {
                                    ws.Cells[iRow, 6].Value = item.ChemDry.Value;
                                }
                                else
                                {
                                    ws.Cells[iRow, 6].Value = new decimal?();
                                }
                                /*
                                // Chem Wet (calc) Kg
                                if (item.WeightCal.HasValue)
                                {
                                    ws.Cells[iRow, 7].Value = item.WeightCal.Value;
                                }
                                else ws.Cells[iRow, 7].Value = new decimal?();
                                */
                                iRow++;
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

                #endregion
            }
        }
    }
}
