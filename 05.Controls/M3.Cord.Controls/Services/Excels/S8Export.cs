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
                        /*
                        // Doff No
                        ws.Cells["U2"].Value = " Production : " + pcCard.ProductCode;
                        // Shift
                        ws.Cells["AA2"].Value = " Lot No : " + pcCard.DIPLotNo;
                        // Date
                        string sDate = sheet.CheckDate.ToString("dd/MM/yyyy");
                        ws.Cells["AH2"].Value = " Date : " + sDate;


                        // Doff(s)
                        ws.Cells["B4"].Value = " Doff : " + pcCard.DoffNo.ToString();
                        ws.Cells["L4"].Value = " Doff : " + pcCard.DoffNo.ToString();
                        ws.Cells["V4"].Value = " Doff : " + pcCard.DoffNo.ToString();
                        ws.Cells["AF4"].Value = " Doff : " + pcCard.DoffNo.ToString();
                        */
                        #endregion

                        #region Items
                        /*
                        int iRow = -1;
                        int iCol = -1;
                        foreach (var item in items)
                        {
                            if (item.SPNo <= 38)
                            {
                                iRow = 5 + item.SPNo;
                                iCol = 1;
                            }
                            else if (item.SPNo > 38 && item.SPNo <= 76)
                            {
                                iRow = 5 + item.SPNo - 38;
                                iCol = 11;
                            }
                            else if (item.SPNo > 76 && item.SPNo <= 114)
                            {
                                iRow = 5 + item.SPNo - 76;
                                iCol = 21;
                            }
                            else if (item.SPNo > 114 && item.SPNo <= 150)
                            {
                                iRow = 5 + item.SPNo - 114;
                                iCol = 31;
                            }
                            else
                            {
                                iRow = -1;
                                iCol = -1;
                            }

                            if (iRow != -1 && iCol != -1)
                            {
                                ws.Cells[iRow, iCol + 1].Value = pcCard.ProductCode;
                                ws.Cells[iRow, iCol + 2].Value = item.LotNo;
                                ws.Cells[iRow, iCol + 3].Value = (item.CHNo == 0) ? "" : item.CHNo.ToString();
                                ws.Cells[iRow, iCol + 4].Value = (item.CheckYarnNo) ? "P" : "";
                                ws.Cells[iRow, iCol + 5].Value = (item.CheckYanScrap) ? "P" : "";
                                ws.Cells[iRow, iCol + 6].Value = (item.CheckYarnBall) ? "P" : "";
                                ws.Cells[iRow, iCol + 7].Value = (item.CheckCover) ? "P" : "";
                                ws.Cells[iRow, iCol + 8].Value = (item.CheckSensor) ? "P" : "";
                                ws.Cells[iRow, iCol + 9].Value = (item.CheckDustFilter) ? "P" : "";
                            }
                        }
                        */
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
