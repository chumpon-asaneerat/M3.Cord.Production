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

#endregion

namespace M3.Cord.Services.Excels
{
    public class Twist1CheckSheetExport
    {
        public static void Export(PCTwist1 pcCard, Twist1CheckSheet sheet)
        {
            string outputFile = ExcelModel.Dialogs.SaveDialog();
            if (string.IsNullOrEmpty(outputFile))
                return;
            if (null == pcCard) 
                return;
            if (null == sheet || null == sheet.Items) 
                return;
            if (pcCard.MCCode.StartsWith("S-1"))
            {
                ExportS1(outputFile, pcCard, sheet);
            }
            else if (pcCard.MCCode.StartsWith("S-4-1"))
            {
                ExportS4x1(outputFile, pcCard, sheet);
            }
            else if (pcCard.MCCode.StartsWith("S-4-2"))
            {
                ExportS4x2(outputFile, pcCard, sheet);
            }
            else
            {
                M3CordApp.Windows.ExportFailed();
            }
        }

        private static void ExportS1(string outputFile, PCTwist1 pcCard, Twist1CheckSheet sheet)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            if (!ExcelExportUtils.CreateS1File(outputFile, true))
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

                        string hdr = "ใบบันทึกสภาวะการตีเกลียว (Actual Yarn Twist Record Sheet ) ";
                        hdr += " Machine :  " + pcCard.MCCode;
                        hdr += " Item : " + pcCard.ProductCode;
                        hdr += " Lot :  " + pcCard.ProductLotNo;
                        ws.Cells["A1"].Value = hdr;
                        // Doff No
                        ws.Cells["AV1"].Value = " Doff No : " + sheet.DoffNo.ToString();
                        // Shift
                        ws.Cells["BC1"].Value = " Shift : " + sheet.ShiftName;
                        // Date
                        string sDate = sheet.ConditionDate.HasValue ? 
                            sheet.ConditionDate.Value.ToString("dd/MM/yyyy") : "";
                        ws.Cells["BG1"].Value = " Date : " + sDate;

                        #endregion

                        #region Items

                        int iRow = -1;
                        int iCol = -1;
                        foreach (var item in sheet.Items) 
                        {
                            if (item.SPNo <= 25)
                            {
                                iRow = 5 + item.SPNo;
                                iCol = 1;
                            }
                            else if (item.SPNo > 25 && item.SPNo <= 50)
                            {
                                iRow = 5 + item.SPNo - 25;
                                iCol = 18;
                            }
                            else if (item.SPNo > 50 && item.SPNo <= 75)
                            {
                                iRow = 5 + item.SPNo - 50;
                                iCol = 35;
                            }
                            else if (item.SPNo > 75 && item.SPNo <= 100)
                            {
                                iRow = 5 + item.SPNo - 75;
                                iCol = 52;
                            }
                            else
                            {
                                iRow = -1;
                                iCol = -1;
                            }

                            if (iRow != -1 && iCol != -1)
                            {
                                ws.Cells[iRow, iCol + 1].Value = (item.RawB) ? "P" : "";
                                ws.Cells[iRow, iCol + 2].Value = (item.RawE) ? "P" : "";

                                ws.Cells[iRow, iCol + 3].Value = (item.CrossB) ? "P" : "";
                                ws.Cells[iRow, iCol + 4].Value = (item.CrossE) ? "P" : "";

                                ws.Cells[iRow, iCol + 5].Value = (item.FormB) ? "P" : "";
                                ws.Cells[iRow, iCol + 6].Value = (item.FormE) ? "P" : "";

                                ws.Cells[iRow, iCol + 7].Value = (item.KebaB) ? "P" : "";
                                ws.Cells[iRow, iCol + 8].Value = (item.KebaE) ? "P" : "";

                                ws.Cells[iRow, iCol + 9].Value = (item.StainB) ? "P" : "";
                                ws.Cells[iRow, iCol + 10].Value = (item.StainE) ? "P" : "";

                                ws.Cells[iRow, iCol + 11].Value = (item.PaperTubeB) ? "P" : "";
                                ws.Cells[iRow, iCol + 12].Value = (item.PaperTubeE) ? "P" : "";

                                ws.Cells[iRow, iCol + 13].Value = (item.YarnNoB) ? "P" : "";
                                ws.Cells[iRow, iCol + 14].Value = (item.YarnNoE) ? "P" : "";

                                ws.Cells[iRow, iCol + 15].Value = (item.BBMarkB) ? "P" : "";
                                ws.Cells[iRow, iCol + 16].Value = (item.BBMarkE) ? "P" : "";
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

        private static void ExportS4x1(string outputFile, PCTwist1 pcCard, Twist1CheckSheet sheet)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            if (!ExcelExportUtils.CreateS4x1File(outputFile, true))
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

                        string hdr = "ใบบันทึกสภาวะการตีเกลียว (Actual Yarn Twist Record Sheet ) ";
                        hdr += " Machine :  " + pcCard.MCCode;
                        hdr += " Item : " + pcCard.ProductCode;
                        hdr += " Lot :  " + pcCard.ProductLotNo;
                        ws.Cells["B1"].Value = hdr;
                        // Doff No
                        ws.Cells["AK1"].Value = " Doff No : " + sheet.DoffNo.ToString();
                        // Shift
                        ws.Cells["AO1"].Value = " Shift : " + sheet.ShiftName;
                        // Date
                        string sDate = sheet.ConditionDate.HasValue ?
                            sheet.ConditionDate.Value.ToString("dd/MM/yyyy") : "";
                        ws.Cells["AR1"].Value = " Date : " + sDate;

                        #endregion

                        #region Items

                        int iRow = -1;
                        int iCol = -1;
                        foreach (var item in sheet.Items)
                        {
                            if (item.SPNo <= 25)
                            {
                                iRow = 5 + item.SPNo;
                                iCol = 2;
                            }
                            else if (item.SPNo > 25 && item.SPNo <= 50)
                            {
                                iRow = 5 + item.SPNo - 25;
                                iCol = 19;
                            }
                            else if (item.SPNo > 50 && item.SPNo <= 72)
                            {
                                iRow = 5 + item.SPNo - 50;
                                iCol = 36;
                            }
                            else
                            {
                                iRow = -1;
                                iCol = -1;
                            }

                            if (iRow != -1 && iCol != -1)
                            {
                                ws.Cells[iRow, iCol + 1].Value = (item.RawB) ? "P" : "";
                                ws.Cells[iRow, iCol + 2].Value = (item.RawE) ? "P" : "";

                                ws.Cells[iRow, iCol + 3].Value = (item.CrossB) ? "P" : "";
                                ws.Cells[iRow, iCol + 4].Value = (item.CrossE) ? "P" : "";

                                ws.Cells[iRow, iCol + 5].Value = (item.FormB) ? "P" : "";
                                ws.Cells[iRow, iCol + 6].Value = (item.FormE) ? "P" : "";

                                ws.Cells[iRow, iCol + 7].Value = (item.KebaB) ? "P" : "";
                                ws.Cells[iRow, iCol + 8].Value = (item.KebaE) ? "P" : "";

                                ws.Cells[iRow, iCol + 9].Value = (item.StainB) ? "P" : "";
                                ws.Cells[iRow, iCol + 10].Value = (item.StainE) ? "P" : "";

                                ws.Cells[iRow, iCol + 11].Value = (item.PaperTubeB) ? "P" : "";
                                ws.Cells[iRow, iCol + 12].Value = (item.PaperTubeE) ? "P" : "";

                                ws.Cells[iRow, iCol + 13].Value = (item.YarnNoB) ? "P" : "";
                                ws.Cells[iRow, iCol + 14].Value = (item.YarnNoE) ? "P" : "";

                                ws.Cells[iRow, iCol + 15].Value = (item.BBMarkB) ? "P" : "";
                                ws.Cells[iRow, iCol + 16].Value = (item.BBMarkE) ? "P" : "";
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

        private static void ExportS4x2(string outputFile, PCTwist1 pcCard, Twist1CheckSheet sheet)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            if (!ExcelExportUtils.CreateS4x2File(outputFile, true))
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

                        string hdr = "ใบบันทึกสภาวะการตีเกลียว (Actual Yarn Twist Record Sheet ) ";
                        hdr += " Machine :  " + pcCard.MCCode;
                        hdr += " Item : " + pcCard.ProductCode;
                        hdr += " Lot :  " + pcCard.ProductLotNo;
                        ws.Cells["A1"].Value = hdr;
                        // Doff No
                        ws.Cells["X1"].Value = " Doff No : " + sheet.DoffNo.ToString();
                        // Shift
                        ws.Cells["AB1"].Value = " Shift : " + sheet.ShiftName;
                        // Date
                        string sDate = sheet.ConditionDate.HasValue ?
                            sheet.ConditionDate.Value.ToString("dd/MM/yyyy") : "";
                        ws.Cells["AE1"].Value = " Date : " + sDate;

                        #endregion

                        #region Items

                        int iRow = -1;
                        int iCol = -1;
                        foreach (var item in sheet.Items)
                        {
                            if (item.SPNo >= 73 && item.SPNo <= 92)
                            {
                                iRow = 5 + item.SPNo - 72;
                                iCol = 1;
                            }
                            else if (item.SPNo > 92 && item.SPNo <= 112)
                            {
                                iRow = 5 + item.SPNo - 72 - 25;
                                iCol = 18;
                            }
                            else
                            {
                                iRow = -1;
                                iCol = -1;
                            }

                            if (iRow != -1 && iCol != -1)
                            {
                                ws.Cells[iRow, iCol + 1].Value = (item.RawB) ? "P" : "";
                                ws.Cells[iRow, iCol + 2].Value = (item.RawE) ? "P" : "";

                                ws.Cells[iRow, iCol + 3].Value = (item.CrossB) ? "P" : "";
                                ws.Cells[iRow, iCol + 4].Value = (item.CrossE) ? "P" : "";

                                ws.Cells[iRow, iCol + 5].Value = (item.FormB) ? "P" : "";
                                ws.Cells[iRow, iCol + 6].Value = (item.FormE) ? "P" : "";

                                ws.Cells[iRow, iCol + 7].Value = (item.KebaB) ? "P" : "";
                                ws.Cells[iRow, iCol + 8].Value = (item.KebaE) ? "P" : "";

                                ws.Cells[iRow, iCol + 9].Value = (item.StainB) ? "P" : "";
                                ws.Cells[iRow, iCol + 10].Value = (item.StainE) ? "P" : "";

                                ws.Cells[iRow, iCol + 11].Value = (item.PaperTubeB) ? "P" : "";
                                ws.Cells[iRow, iCol + 12].Value = (item.PaperTubeE) ? "P" : "";

                                ws.Cells[iRow, iCol + 13].Value = (item.YarnNoB) ? "P" : "";
                                ws.Cells[iRow, iCol + 14].Value = (item.YarnNoE) ? "P" : "";

                                ws.Cells[iRow, iCol + 15].Value = (item.BBMarkB) ? "P" : "";
                                ws.Cells[iRow, iCol + 16].Value = (item.BBMarkE) ? "P" : "";
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
