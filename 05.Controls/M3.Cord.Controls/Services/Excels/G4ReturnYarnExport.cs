#region Using

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
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
    public class G4ReturnYarnExport
    {
        private static void Set(ExcelWorksheet ws, string addr, object value)
        {
            if (null != ws)
            {
                ws.Cells[addr].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[addr].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[addr].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[addr].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                if (null != value && value.GetType() == typeof(DateTime))
                {
                    ws.Cells[addr].Style.Numberformat.Format = "MM/dd/yyyy hh:mm:ss AM/PM";
                }

                ws.Cells[addr].Value = value;
            }
        }

        public static void Export(List<G4YarnReturn> items)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            string outputFile = ExcelModel.Dialogs.SaveDialog();
            if (string.IsNullOrEmpty(outputFile))
                return;

            try
            {
                using (var package = new ExcelPackage(outputFile))
                {
                    var ws = package.Workbook.Worksheets["RETURNYARN"];
                    if (ws != null) package.Workbook.Worksheets.Delete(ws);

                    ws = package.Workbook.Worksheets.Add("RETURNYARN");

                    if (null != ws)
                    {
                        #region Header

                        Set(ws, "A1", "NO");
                        Set(ws, "B1", "TruckNo");
                        Set(ws, "C1", "Desc");
                        Set(ws, "D1", "TraceNo");
                        Set(ws, "E1", "Cone");
                        Set(ws, "F1", "Qty");
                        Set(ws, "G1", "Lot");
                        Set(ws, "H1", "Item");
                        Set(ws, "I1", "RecDt");
                        Set(ws, "J1", "Um");
                        Set(ws, "K1", "Grade");
                        Set(ws, "L1", "Trans");

                        #endregion

                        #region Items

                        int row = 2;

                        if (null != items && items.Count > 0)
                        {
                            foreach (var item in items)
                            {
                                string sRow = row.ToString();

                                // NO
                                Set(ws, "A" + sRow, item.RowNo);
                                // TruckNo
                                Set(ws, "B" + sRow, string.Empty);
                                // Desc
                                Set(ws, "C" + sRow, string.Empty);
                                // Trace No
                                Set(ws, "D" + sRow, item.NewTraceNo);
                                // Cone
                                int cone = (item.ConeCH.HasValue) ? Convert.ToInt32(item.ConeCH.Value) : 0;
                                Set(ws, "E" + sRow, cone);
                                // Qty
                                int wg = (item.WeightQty.HasValue) ? Convert.ToInt32(item.WeightQty.Value) : 0;
                                Set(ws, "F" + sRow, wg);
                                // Lot
                                Set(ws, "G" + sRow, item.NewLotNo);
                                // Item
                                Set(ws, "H" + sRow, item.Item400);
                                // RecDt
                                if (item.ReceiveDate.HasValue)
                                {
                                    Set(ws, "I" + sRow, item.ReceiveDate.Value);
                                }
                                // Um
                                Set(ws, "J" + sRow, item.Unit);
                                // Grade
                                Set(ws, "K" + sRow, item.Grade);
                                // Trans
                                Set(ws, "L" + sRow, "GR");

                                row++;
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
