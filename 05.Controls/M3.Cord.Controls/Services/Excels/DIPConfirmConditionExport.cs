#region Using

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using M3.Cord.Models;
using M3.Cord.Pages;
using NLib;
using OfficeOpenXml;

#endregion

namespace M3.Cord.Services.Excels
{
    public class DIPConfirmConditionExport
    {
        public static void Export(DIPPCCard pcCard, DIPCondition cond)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            string outputFile = ExcelModel.Dialogs.SaveDialog();
            if (string.IsNullOrEmpty(outputFile))
                return;
            if (null == pcCard)
                return;
            if (null == cond)
                return;

            if (!ExcelExportUtils.CreateDIPConfirmConditionFile(outputFile, true))
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
                        #region Document

                        // Item Code
                        ws.Cells["B3"].Value = pcCard.ProductCode;
                        // Date
                        string sDate = (cond.UpdateDate.HasValue) ?
                            cond.UpdateDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                        ws.Cells["H3"].Value = sDate;
                        // LOT
                        ws.Cells["B5"].Value = pcCard.DIPLotNo;
                        // Remark
                        //ws.Cells["H5"].Value = cond.Remark;

                        // S7 - Cord Structure
                        ws.Cells["F10"].Value = (cond.S7YarnCordStructureSC.HasValue && cond.S7Yarn1stTwistSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G10"].Value = cond.S7YarnCordStructureSet;
                        ws.Cells["H10"].Value = cond.S7YarnCordStructureActual;
                        ws.Cells["I10"].Value = cond.S7YarnCordStructureSet2;
                        ws.Cells["J10"].Value = cond.S7YarnCordStructureActual2;

                        // S7 - Yarn Type
                        ws.Cells["F11"].Value = (cond.S7YarnYarnTypeSC.HasValue && cond.S7YarnYarnTypeSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G11"].Value = cond.S7YarnYarnTypeSet;
                        ws.Cells["H11"].Value = cond.S7YarnYarnTypeActual;
                        ws.Cells["I11"].Value = cond.S7YarnYarnTypeSet2;
                        ws.Cells["J11"].Value = cond.S7YarnYarnTypeActual2;

                        // S7 - 1st Twisting
                        ws.Cells["F12"].Value = (cond.S7Yarn1stTwistSC.HasValue && cond.S7Yarn1stTwistSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G12"].Value = cond.S7Yarn1stTwistSet;
                        ws.Cells["H12"].Value = cond.S7Yarn1stTwistActual;
                        ws.Cells["I12"].Value = cond.S7Yarn1stTwistSet2;
                        ws.Cells["J12"].Value = cond.S7Yarn1stTwistActual2;

                        // S7 - 2nd Twisting
                        ws.Cells["F13"].Value = (cond.S7Yarn2ndTwistSC.HasValue && cond.S7Yarn2ndTwistSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G13"].Value = cond.S7Yarn2ndTwistSet;
                        ws.Cells["H13"].Value = cond.S7Yarn2ndTwistActual;
                        ws.Cells["I13"].Value = cond.S7Yarn2ndTwistSet2;
                        ws.Cells["J13"].Value = cond.S7Yarn2ndTwistActual2;

                        // S7 - Shape of Yarn
                        ws.Cells["F14"].Value = (cond.S7YarnLabelOfYarnSC.HasValue && cond.S7YarnLabelOfYarnSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G14"].Value = cond.S7YarnLabelOfYarnSet;
                        ws.Cells["H14"].Value = cond.S7YarnLabelOfYarnActual;
                        ws.Cells["I14"].Value = cond.S7YarnLabelOfYarnSet2;
                        ws.Cells["J14"].Value = cond.S7YarnLabelOfYarnActual2;

                        // S7 - Yarn Weight
                        ws.Cells["F15"].Value = (cond.S7YarnWeightSC.HasValue && cond.S7YarnWeightSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G15"].Value = string.Format("{0} ± {1}", cond.S7YarnWeightSet, cond.S7YarnWeightSetErr);
                        ws.Cells["H15"].Value = cond.S7YarnWeightActual;
                        ws.Cells["I15"].Value = string.Format("{0} ± {1}", cond.S7YarnWeightSet2, cond.S7YarnWeightSetErr2);
                        ws.Cells["J15"].Value = cond.S7YarnWeightActual2;

                        // S7 - Creel Setting User Tensor
                        ws.Cells["F16"].Value = (cond.S7CreelSettingUseTensorSC.HasValue && cond.S7CreelSettingUseTensorSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G16"].Value = cond.S7CreelSettingUseTensorSet;
                        ws.Cells["H16"].Value = cond.S7CreelSettingUseTensorActual;
                        ws.Cells["I16"].Value = cond.S7CreelSettingUseTensorSet2;
                        ws.Cells["J16"].Value = cond.S7CreelSettingUseTensorActual2;

                        // S7 - Creel Setting Knot Condition
                        ws.Cells["F17"].Value = (cond.S7CreelSettingKnotConditionSC.HasValue && cond.S7CreelSettingKnotConditionSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G17"].Value = cond.S7CreelSettingKnotConditionSet;
                        ws.Cells["H17"].Value = cond.S7CreelSettingKnotConditionActual;
                        ws.Cells["I17"].Value = cond.S7CreelSettingKnotConditionSet2;
                        ws.Cells["J17"].Value = cond.S7CreelSettingKnotConditionActual2;

                        // S7 - Creel Setting Slub Catswe
                        ws.Cells["F18"].Value = (cond.S7CreelSettingSlubCatsweSC.HasValue && cond.S7CreelSettingSlubCatsweSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G18"].Value = cond.S7CreelSettingSlubCatsweSet;
                        ws.Cells["H18"].Value = cond.S7CreelSettingSlubCatsweActual;
                        ws.Cells["I18"].Value = cond.S7CreelSettingSlubCatsweSet2;
                        ws.Cells["J18"].Value = cond.S7CreelSettingSlubCatsweActual2;

                        // S8 - Strech Dryer
                        ws.Cells["F19"].Value = (cond.S8StretchDryerSC.HasValue && cond.S8StretchDryerSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G19"].Value = string.Format("{0} ± {1}", cond.S8StretchDryerSet, cond.S8StretchDryerSetErr);
                        ws.Cells["H19"].Value = cond.S8StretchDryerActual;
                        ws.Cells["I19"].Value = string.Format("{0} ± {1}", cond.S8StretchDryerSet2, cond.S8StretchDryerSetErr2);
                        ws.Cells["J19"].Value = cond.S8StretchDryerActual2;

                        // S8 - Strech Hot
                        ws.Cells["F20"].Value = (cond.S8StretchStretchHotSC.HasValue && cond.S8StretchStretchHotSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G20"].Value = string.Format("{0} ± {1}", cond.S8StretchStretchHotSet, cond.S8StretchStretchHotSetErr);
                        ws.Cells["H20"].Value = cond.S8StretchHotActual;
                        ws.Cells["I20"].Value = string.Format("{0} ± {1}", cond.S8StretchHotSet2, cond.S8StretchHotSetErr2);
                        ws.Cells["J20"].Value = cond.S8StretchHotActual2;

                        // S8 - Strech Normal
                        ws.Cells["F21"].Value = (cond.S8StretchNormalSC.HasValue && cond.S8StretchNormalSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G21"].Value = string.Format("{0} ± {1}", cond.S8StretchNormalSet, cond.S8StretchNormalSetErr);
                        ws.Cells["H21"].Value = cond.S8StretchNormalActual;
                        ws.Cells["I21"].Value = string.Format("{0} ± {1}", cond.S8StretchNormalSet2, cond.S8StretchNormalSetErr2);
                        ws.Cells["J21"].Value = cond.S8StretchNormalActual2;

                        // S8 - Strech Total
                        ws.Cells["F22"].Value = (cond.S8StretchTotalSC.HasValue && cond.S8StretchTotalSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G22"].Value = string.Format("{0} ± {1}", cond.S8StretchTotalSet, cond.S8StretchTotalSetErr);
                        ws.Cells["H22"].Value = cond.S8StretchTotalActual;
                        ws.Cells["I22"].Value = string.Format("{0} ± {1}", cond.S8StretchTotalSet2, cond.S8StretchTotalSetErr2);
                        ws.Cells["J22"].Value = cond.S8StretchTotalActual2;

                        // S8 - Temp Dryer
                        ws.Cells["F23"].Value = (cond.S8TempDryerSC.HasValue && cond.S8TempDryerSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G23"].Value = string.Format("{0} ± {1}", cond.S8TempDryerSet, cond.S8TempDryerSetErr);
                        ws.Cells["H23"].Value = cond.S8TempDryerActual;
                        ws.Cells["I23"].Value = string.Format("{0} ± {1}", cond.S8TempDryerSet2, cond.S8TempDryerSetErr2);
                        ws.Cells["J23"].Value = cond.S8TempDryerActual2;

                        // S8 - Temp Hot
                        ws.Cells["F24"].Value = (cond.S8TempHotSC.HasValue && cond.S8TempHotSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G24"].Value = string.Format("{0} ± {1}", cond.S8TempHotSet, cond.S8TempHotSetErr);
                        ws.Cells["H24"].Value = cond.S8TempHotActual;
                        ws.Cells["I24"].Value = string.Format("{0} ± {1}", cond.S8TempHotSet2, cond.S8TempHotSetErr2);
                        ws.Cells["J24"].Value = cond.S8TempHotActual2;

                        // S8 - Temp Normal
                        ws.Cells["F25"].Value = (cond.S8TempNormalSC.HasValue && cond.S8TempNormalSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G25"].Value = string.Format("{0} ± {1}", cond.S8TempNormalSet, cond.S8TempNormalSetErr);
                        ws.Cells["H25"].Value = cond.S8TempNormalActual;
                        ws.Cells["I25"].Value = string.Format("{0} ± {1}", cond.S8TempNormalSet2, cond.S8TempNormalSetErr2);
                        ws.Cells["J25"].Value = cond.S8TempNormalActual2;

                        // S8 - Counter Setting
                        ws.Cells["F26"].Value = (cond.S8CounterSettingSC.HasValue && cond.S8CounterSettingSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G26"].Value = cond.S8CounterSettingSet;
                        ws.Cells["H26"].Value = cond.S8CounterSettingActual;
                        ws.Cells["I26"].Value = cond.S8CounterSettingSet2;
                        ws.Cells["J26"].Value = cond.S8CounterSettingActual2;

                        // S8 - Speed
                        ws.Cells["F27"].Value = (cond.S8SpeedSC.HasValue && cond.S8SpeedSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G27"].Value = string.Format("{0} ± {1}", cond.S8SpeedSet, cond.S8SpeedErr);
                        ws.Cells["H27"].Value = cond.S8SpeedActual;
                        ws.Cells["I27"].Value = string.Format("{0} ± {1}", cond.S8SpeedSet2, cond.S8SpeedSetErr2);
                        ws.Cells["J27"].Value = cond.S8SpeedActual2;

                        // S8 - No Of Cord
                        ws.Cells["F28"].Value = (cond.S8NoOfCordsSC.HasValue && cond.S8NoOfCordsSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G28"].Value = cond.S8NoOfCordsSet;
                        ws.Cells["H28"].Value = cond.S8NoOfCordsActual;
                        ws.Cells["I28"].Value = cond.S8NoOfCordsSet2;
                        ws.Cells["J28"].Value = cond.S8NoOfCordsActual2;

                        // S8 - Sofnor
                        ws.Cells["F29"].Value = (cond.S8SofnorSC.HasValue && cond.S8SofnorSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G29"].Value = cond.S8SofnorSet;
                        ws.Cells["H29"].Value = cond.S8SofnorActual;
                        ws.Cells["I29"].Value = cond.S8SofnorSet2;
                        ws.Cells["J29"].Value = cond.S8SofnorActual2;

                        // S8 - Draw nip
                        ws.Cells["F30"].Value = (cond.S8DrawNipSC.HasValue && cond.S8DrawNipSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G30"].Value = cond.S8DrawNipSet;
                        ws.Cells["H30"].Value = cond.S8DrawNipActual;
                        ws.Cells["I30"].Value = cond.S8DrawNipSet2;
                        ws.Cells["J30"].Value = cond.S8DrawNipActual2;

                        // S8 - Dip 1 Concentration
                        ws.Cells["F31"].Value = (cond.S8DippingNo1ConcentrationSC.HasValue && cond.S8DippingNo1ConcentrationSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G31"].Value = cond.S8DippingNo1ConcentrationSet;
                        ws.Cells["H31"].Value = cond.S8DippingNo1ConcentrationActual;
                        ws.Cells["I31"].Value = cond.S8DippingNo1ConcentrationSet2;
                        ws.Cells["J31"].Value = cond.S8DippingNo1ConcentrationActual2;

                        // S8 - Dip 1 NIP Front
                        ws.Cells["F32"].Value = (cond.S8DippingNo1NipFrontSC.HasValue && cond.S8DippingNo1NipFrontSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G32"].Value = cond.S8DippingNo1NipFrontSet;
                        ws.Cells["H32"].Value = cond.S8DippingNo1NipFrontActual;
                        ws.Cells["I32"].Value = cond.S8DippingNo1NipFrontSet2;
                        ws.Cells["J32"].Value = cond.S8DippingNo1NipFrontActual2;

                        // S8 - Dip 1 NIP Back
                        ws.Cells["F33"].Value = (cond.S8DippingNo1NipBackSC.HasValue && cond.S8DippingNo1NipBackSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G33"].Value = cond.S8DippingNo1NipBackSet;
                        ws.Cells["H33"].Value = cond.S8DippingNo1NipBackActual;
                        ws.Cells["I33"].Value = cond.S8DippingNo1NipBackSet2;
                        ws.Cells["J33"].Value = cond.S8DippingNo1NipBackActual2;

                        // S8 - Dip 1 WPU
                        ws.Cells["F34"].Value = (cond.S8DippingNo1WPUstdSC.HasValue && cond.S8DippingNo1WPUstdSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G34"].Value = cond.S8DippingNo1WPUstdSet;
                        ws.Cells["H34"].Value = cond.S8DippingNo1WPUstdActual;
                        ws.Cells["I34"].Value = cond.S8DippingNo1WPUstdSet2;
                        ws.Cells["J34"].Value = cond.S8DippingNo1WPUstdActual2;

                        // S8 - Dip 2 Concentration
                        ws.Cells["F35"].Value = (cond.S8DippingNo2ConcentrationSC.HasValue && cond.S8DippingNo2ConcentrationSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G35"].Value = cond.S8DippingNo2ConcentrationSet;
                        ws.Cells["H35"].Value = cond.S8DippingNo2ConcentrationActual;
                        ws.Cells["I35"].Value = cond.S8DippingNo2ConcentrationSet2;
                        ws.Cells["J35"].Value = cond.S8DippingNo2ConcentrationActual2;

                        // S8 - Dip 2 NIP Front
                        ws.Cells["F36"].Value = (cond.S8DippingNo2NipFrontSC.HasValue && cond.S8DippingNo2NipFrontSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G36"].Value = cond.S8DippingNo2NipFrontSet;
                        ws.Cells["H36"].Value = cond.S8DippingNo2NipFrontActual;
                        ws.Cells["I36"].Value = cond.S8DippingNo2NipFrontSet2;
                        ws.Cells["J36"].Value = cond.S8DippingNo2NipFrontActual2;

                        // S8 - Dip 2 NIP Back
                        ws.Cells["F37"].Value = (cond.S8DippingNo2NipBackSC.HasValue && cond.S8DippingNo2NipBackSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G37"].Value = cond.S8DippingNo2NipBackSet;
                        ws.Cells["H37"].Value = cond.S8DippingNo2NipBackActual;
                        ws.Cells["I37"].Value = cond.S8DippingNo2NipBackSet2;
                        ws.Cells["J37"].Value = cond.S8DippingNo2NipBackActual2;

                        // S8 - Dip 2 WPU
                        ws.Cells["F38"].Value = (cond.S8DippingNo2WPUstdSC.HasValue && cond.S8DippingNo2WPUstdSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G38"].Value = cond.S8DippingNo2WPUstdSet;
                        ws.Cells["H38"].Value = cond.S8DippingNo2WPUstdActual;
                        ws.Cells["I38"].Value = cond.S8DippingNo2WPUstdSet2;
                        ws.Cells["J38"].Value = cond.S8DippingNo2WPUstdActual2;

                        // S9 - Winder Ayame Daial
                        ws.Cells["F39"].Value = (cond.S9WinderAyameDaialSC.HasValue && cond.S9WinderAyameDaialSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G39"].Value = cond.S9WinderAyameDaialSet;
                        ws.Cells["H39"].Value = cond.S9WinderAyameDaialActual;
                        ws.Cells["I39"].Value = cond.S9WinderAyameDaialSet2;
                        ws.Cells["J39"].Value = cond.S9WinderAyameDaialActual2;

                        // S9 - Winder Contrn presure MPA
                        ws.Cells["F40"].Value = (cond.S9WinderMpaSC.HasValue && cond.S9WinderMpaSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G40"].Value = cond.S9WinderMpaSet;
                        ws.Cells["H40"].Value = cond.S9WinderMpaActual;
                        ws.Cells["I40"].Value = cond.S9WinderMpaSet2;
                        ws.Cells["J40"].Value = cond.S9WinderMpaActual2;

                        // S9 - Winder Spring Tension
                        ws.Cells["F41"].Value = (cond.S9WinderSpringSC.HasValue && cond.S9WinderSpringSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G41"].Value = cond.S9WinderSpringSet;
                        ws.Cells["H41"].Value = cond.S9WinderSpringActual;
                        ws.Cells["I41"].Value = cond.S9WinderSpringSet2;
                        ws.Cells["J41"].Value = cond.S9WinderSpringActual2;

                        // S9 - Winder Paper Tube
                        ws.Cells["F42"].Value = (cond.S9WinderPeperTubeColorSC.HasValue && cond.S9WinderPeperTubeColorSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G42"].Value = cond.S9WinderPeperTubeColorSet;
                        ws.Cells["H42"].Value = cond.S9WinderPeperTubeColorActual;
                        ws.Cells["I42"].Value = cond.S9WinderPeperTubeColorSet2;
                        ws.Cells["J42"].Value = cond.S9WinderPeperTubeColorActual2;

                        // S9 - Winder Cheese Weight
                        ws.Cells["F43"].Value = (cond.S9WinderCheeseWeightSC.HasValue && cond.S9WinderCheeseWeightSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G43"].Value = cond.S9WinderCheeseWeightSet;
                        ws.Cells["H43"].Value = cond.S9WinderCheeseWeightActual;
                        ws.Cells["I43"].Value = cond.S9WinderCheeseWeightSet2;
                        ws.Cells["J43"].Value = cond.S9WinderCheeseWeightActual2;

                        // S9 - Exhaust Fan Oven 1 Circulating Fan
                        ws.Cells["F44"].Value = (cond.S9ExhaustFanOven1CirculatingFanSC.HasValue && cond.S9ExhaustFanOven1CirculatingFanSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G44"].Value = cond.S9ExhaustFanOven1CirculatingFanSet;
                        ws.Cells["H44"].Value = cond.S9ExhaustFanOven1CirculatingFanActual;
                        ws.Cells["I44"].Value = cond.S9ExhaustFanOven1CirculatingFanSet2;
                        ws.Cells["J44"].Value = cond.S9ExhaustFanOven1CirculatingFanActual2;

                        // S9 - Exhaust Fan Oven 1 Exhaust Fan
                        ws.Cells["F45"].Value = (cond.S9ExhaustFanOven1ExhaustFanSC.HasValue && cond.S9ExhaustFanOven1ExhaustFanSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G45"].Value = cond.S9ExhaustFanOven1ExhaustFanSet;
                        ws.Cells["H45"].Value = cond.S9ExhaustFanOven1ExhaustFanActual;
                        ws.Cells["I45"].Value = cond.S9ExhaustFanOven1ExhaustFanSet2;
                        ws.Cells["J45"].Value = cond.S9ExhaustFanOven1ExhaustFanActual2;

                        // S9 - Exhaust Fan Oven Exhaust Fan Front
                        ws.Cells["F46"].Value = (cond.S9ExhaustFanOvenFrontExhaustFanSC.HasValue && cond.S9ExhaustFanOven1ExhaustFanSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G46"].Value = cond.S9ExhaustFanOvenFrontExhaustFanSet;
                        ws.Cells["H46"].Value = cond.S9ExhaustFanOvenFrontExhaustFanActual;
                        ws.Cells["I46"].Value = cond.S9ExhaustFanOvenFrontExhaustFanSet2;
                        ws.Cells["J46"].Value = cond.S9ExhaustFanOvenFrontExhaustFanActual2;

                        // S9 - Exhaust Fan Oven Spong
                        ws.Cells["F47"].Value = (cond.S9SpongSC.HasValue && cond.S9SpongSC.Value) ?
                            "[SC]" : string.Empty;
                        ws.Cells["G47"].Value = cond.S9SpongSet;
                        ws.Cells["H47"].Value = cond.S9SpongActual;
                        ws.Cells["I47"].Value = cond.S9SpongSet2;
                        ws.Cells["J47"].Value = cond.S9SpongActual2;

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
