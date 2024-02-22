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
    public class S9AppearanceExport
    {
        public static void Export(DIPPCCard pcCard, S9AppearanceCheckSheet sheet,
            List<S9AppearanceCheckSheetItem> items)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            string outputFile = ExcelModel.Dialogs.SaveDialog();
            if (string.IsNullOrEmpty(outputFile))
                return;
            if (null == pcCard)
                return;
            if (null == sheet || null == items)
                return;

            if (!ExcelExportUtils.CreateS9AppearanceFile(outputFile, true))
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

                        string hdr = "Cord  production  appearance  check  sheet ( ใบตรวจเช็คเส้นด้ายของ S-9 ) ";
                        hdr += " Item Code : " + pcCard.ProductCode;
                        hdr += " Lot :  " + pcCard.DIPLotNo;
                        ws.Cells["A2"].Value = hdr;
                        // Date
                        string sDate = sheet.CheckDate.ToString("dd/MM/yyyy");
                        ws.Cells["AS2"].Value = " Date : " + sDate;

                        #endregion

                        #region Items

                        int iRow = -1;
                        int iCol = -1;
                        foreach (var item in items)
                        {
                            if (item.SPNo <= 38)
                            {
                                iRow = 6 + item.SPNo;
                                iCol = 1;
                            }
                            else if (item.SPNo > 38 && item.SPNo <= 76)
                            {
                                iRow = 6 + item.SPNo - 38;
                                iCol = 15;
                            }
                            else if (item.SPNo > 76 && item.SPNo <= 114)
                            {
                                iRow = 6 + item.SPNo - 76;
                                iCol = 29;
                            }
                            else if (item.SPNo > 114 && item.SPNo <= 150)
                            {
                                iRow = 6 + item.SPNo - 114;
                                iCol = 43;
                            }
                            else
                            {
                                iRow = -1;
                                iCol = -1;
                            }

                            if (iRow != -1 && iCol != -1)
                            {
                                ws.Cells[iRow, iCol + 1].Value = (item.CheckGood.HasValue) ?
                                    ((item.CheckGood.Value) ? "P" : "O") : "";

                                ws.Cells[iRow, iCol + 3].Value = (item.CheckBad.HasValue) ?
                                    ((item.CheckBad.Value) ? "P" : "O") : "";

                                ws.Cells[iRow, iCol + 5].Value = (item.Check2Color.HasValue) ?
                                    ((item.Check2Color.Value) ? "P" : "O") : "";
                                ws.Cells[iRow, iCol + 6].Value = (item.CheckKeiba.HasValue) ?
                                    ((item.CheckKeiba.Value) ? "P" : "O") : "";
                                ws.Cells[iRow, iCol + 7].Value = (item.CheckWeight).HasValue ?
                                    ((item.CheckWeight.Value) ? "P" : "O") : "";

                                ws.Cells[iRow, iCol + 8].Value = (item.CheckFrontTwist.HasValue) ?
                                    ((item.CheckFrontTwist.Value) ? "P" : "O") : "";

                                ws.Cells[iRow, iCol + 10].Value = (item.CheckBackTwist.HasValue) ?
                                    ((item.CheckBackTwist.Value) ? "P" : "O") : "";

                                ws.Cells[iRow, iCol + 12].Value = (item.CheckSnarl.HasValue) ?
                                    ((item.CheckSnarl.Value) ? "P" : "O") : "";
                                ws.Cells[iRow, iCol + 13].Value = (item.CheckTube.HasValue) ?
                                    ((item.CheckTube.Value) ? "P" : "O") : "";
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

    public class S9CleaningExport
    {

    }
}
