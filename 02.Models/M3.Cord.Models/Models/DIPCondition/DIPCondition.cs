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
	public class DIPCondition
	{
        #region Public Proeprties

        public int DIPConditionId { get; set; }
        public int? DIPPCId { get; set; }

		public string ProductCode { get; set; }
		public bool? S7YarnCordStructureSC { get; set; }
		public string S7YarnCordStructureSet { get; set; }
		public string S7YarnCordStructureActual { get; set; }
		public string S7YarnCordStructureSet2 { get; set; }
		public string S7YarnCordStructureActual2 { get; set; }
		public bool? S7YarnYarnTypeSC { get; set; }
		public string S7YarnYarnTypeSet { get; set; }
		public string S7YarnYarnTypeActual { get; set; }
		public string S7YarnYarnTypeSet2 { get; set; }
		public string S7YarnYarnTypeActual2 { get; set; }
		public bool? S7Yarn1stTwistSC { get; set; }
		public string S7Yarn1stTwistSet { get; set; }
		public decimal? S7Yarn1stTwistActual { get; set; }
		public string S7Yarn1stTwistSet2 { get; set; }
		public decimal? S7Yarn1stTwistActual2 { get; set; }
		public bool? S7Yarn2ndTwistSC { get; set; }
		public string S7Yarn2ndTwistSet { get; set; }
		public decimal? S7Yarn2ndTwistActual { get; set; }
		public string S7Yarn2ndTwistSet2 { get; set; }
		public decimal? S7Yarn2ndTwistActual2 { get; set; }
		public bool? S7YarnLabelOfYarnSC { get; set; }
		public string S7YarnLabelOfYarnSet { get; set; }
		public string S7YarnLabelOfYarnActual { get; set; }
		public string S7YarnLabelOfYarnSet2 { get; set; }
		public string S7YarnLabelOfYarnActual2 { get; set; }
		public bool? S7YarnWeightSC { get; set; }
		public decimal? S7YarnWeightSet { get; set; }
		public decimal? S7YarnWeightSetErr { get; set; }
		public decimal? S7YarnWeightActual { get; set; }
		public decimal? S7YarnWeightSet2 { get; set; }
		public decimal? S7YarnWeightSetErr2 { get; set; }
		public decimal? S7YarnWeightActual2 { get; set; }
		public bool? S7CreelSettingUseTensorSC { get; set; }
		public string S7CreelSettingUseTensorSet { get; set; }
		public string S7CreelSettingUseTensorActual { get; set; }
		public string S7CreelSettingUseTensorSet2 { get; set; }
		public string S7CreelSettingUseTensorActual2 { get; set; }
		public bool? S7CreelSettingKnotConditionSC { get; set; }
		public decimal? S7CreelSettingKnotConditionSet { get; set; }
		public decimal? S7CreelSettingKnotConditionActual { get; set; }
		public decimal? S7CreelSettingKnotConditionSet2 { get; set; }
		public decimal? S7CreelSettingKnotConditionActual2 { get; set; }
		public bool? S7CreelSettingSlubCatsweSC { get; set; }
		public decimal? S7CreelSettingSlubCatsweSet { get; set; }
		public decimal? S7CreelSettingSlubCatsweActual { get; set; }
		public decimal? S7CreelSettingSlubCatsweSet2 { get; set; }
		public decimal? S7CreelSettingSlubCatsweActual2 { get; set; }
		public bool? S8StretchDryerSC { get; set; }
		public decimal? S8StretchDryerSet { get; set; }
		public decimal? S8StretchDryerSetErr { get; set; }
		public decimal? S8StretchDryerActual { get; set; }
		public decimal? S8StretchDryerSet2 { get; set; }
		public decimal? S8StretchDryerSetErr2 { get; set; }
		public decimal? S8StretchDryerActual2 { get; set; }
		public bool? S8StretchStretchHotSC { get; set; }
		public decimal? S8StretchStretchHotSet { get; set; }
		public decimal? S8StretchStretchHotSetErr { get; set; }
		public decimal? S8StretchHotActual { get; set; }
		public decimal? S8StretchHotSet2 { get; set; }
		public decimal? S8StretchHotSetErr2 { get; set; }
		public decimal? S8StretchHotActual2 { get; set; }
		public bool? S8StretchNormalSC { get; set; }
		public decimal? S8StretchNormalSet { get; set; }
		public decimal? S8StretchNormalSetErr { get; set; }
		public decimal? S8StretchNormalActual { get; set; }
		public decimal? S8StretchNormalSet2 { get; set; }
		public decimal? S8StretchNormalSetErr2 { get; set; }
		public decimal? S8StretchNormalActual2 { get; set; }
		public bool? S8StretchTotalSC { get; set; }
		public decimal? S8StretchTotalSet { get; set; }
		public decimal? S8StretchTotalSetErr { get; set; }
		public decimal? S8StretchTotalActual { get; set; }
		public decimal? S8StretchTotalSet2 { get; set; }
		public decimal? S8StretchTotalSetErr2 { get; set; }
		public decimal? S8StretchTotalActual2 { get; set; }

		public bool? S8TempDryerSC { get; set; }
		public decimal? S8TempDryerSet { get; set; }
		public decimal? S8TempDryerSetErr { get; set; }
		public decimal? S8TempDryerActual { get; set; }

		public decimal? S8TempDryerSet2 { get; set; }
		public decimal? S8TempDryerSetErr2 { get; set; }
		public decimal? S8TempDryerActual2 { get; set; }
		
		public bool? S8TempHotSC { get; set; }
		public decimal? S8TempHotSet { get; set; }
		public decimal? S8TempHotSetErr { get; set; }
		public decimal? S8TempHotActual { get; set; }

		public decimal? S8TempHotSet2 { get; set; }
		public decimal? S8TempHotSetErr2 { get; set; }
		public decimal? S8TempHotActual2 { get; set; }

		public bool? S8TempNormalSC { get; set; }
		public decimal? S8TempNormalSet { get; set; }
		public decimal? S8TempNormalSetErr { get; set; }
		public decimal? S8TempNormalActual { get; set; }
		public decimal? S8TempNormalSet2 { get; set; }
		public decimal? S8TempNormalSetErr2 { get; set; }
		public decimal? S8TempNormalActual2 { get; set; }

		public bool? S8CounterSettingSC { get; set; }
		public string S8CounterSettingSet { get; set; }
		public decimal? S8CounterSettingActual { get; set; }
		public string S8CounterSettingSet2 { get; set; }
		public decimal? S8CounterSettingActual2 { get; set; }
		public bool? S8SpeedSC { get; set; }
		public decimal? S8SpeedSet { get; set; }
		public decimal? S8SpeedErr { get; set; }
		public decimal? S8SpeedActual { get; set; }
		public decimal? S8SpeedSet2 { get; set; }
		public decimal? S8SpeedSetErr2 { get; set; }
		public decimal? S8SpeedActual2 { get; set; }
		public bool? S8NoOfCordsSC { get; set; }
		public decimal? S8NoOfCordsSet { get; set; }
		public decimal? S8NoOfCordsActual { get; set; }
		public decimal? S8NoOfCordsSet2 { get; set; }
		public decimal? S8NoOfCordsActual2 { get; set; }
		public bool? S8SofnorSC { get; set; }
		public string S8SofnorSet { get; set; }
		public string S8SofnorActual { get; set; }
		public string S8SofnorSet2 { get; set; }
		public string S8SofnorActual2 { get; set; }
		public bool? S8DrawNipSC { get; set; }
		public decimal? S8DrawNipSet { get; set; }
		public decimal? S8DrawNipActual { get; set; }
		public decimal? S8DrawNipSet2 { get; set; }
		public decimal? S8DrawNipActual2 { get; set; }
		public bool? S8DippingNo1ConcentrationSC { get; set; }
		public string S8DippingNo1ConcentrationSet { get; set; }
		public string S8DippingNo1ConcentrationActual { get; set; }
		public string S8DippingNo1ConcentrationSet2 { get; set; }
		public string S8DippingNo1ConcentrationActual2 { get; set; }
		public bool? S8DippingNo1NipFrontSC { get; set; }
		public string S8DippingNo1NipFrontSet { get; set; }
		public decimal? S8DippingNo1NipFrontActual { get; set; }
		public string S8DippingNo1NipFrontSet2 { get; set; }
		public decimal? S8DippingNo1NipFrontActual2 { get; set; }
		public bool? S8DippingNo1NipBackSC { get; set; }
		public string S8DippingNo1NipBackSet { get; set; }
		public decimal? S8DippingNo1NipBackActual { get; set; }
		public string S8DippingNo1NipBackSet2 { get; set; }
		public decimal? S8DippingNo1NipBackActual2 { get; set; }
		public bool? S8DippingNo1WPUstdSC { get; set; }
		public string S8DippingNo1WPUstdSet { get; set; }
		public decimal? S8DippingNo1WPUstdActual { get; set; }
		public string S8DippingNo1WPUstdSet2 { get; set; }
		public decimal? S8DippingNo1WPUstdActual2 { get; set; }
		public bool? S8DippingNo2ConcentrationSC { get; set; }
		public string S8DippingNo2ConcentrationSet { get; set; }
		public string S8DippingNo2ConcentrationActual { get; set; }
		public string S8DippingNo2ConcentrationSet2 { get; set; }
		public string S8DippingNo2ConcentrationActual2 { get; set; }
		public bool? S8DippingNo2NipFrontSC { get; set; }
		public string S8DippingNo2NipFrontSet { get; set; }
		public decimal? S8DippingNo2NipFrontActual { get; set; }
		public string S8DippingNo2NipFrontSet2 { get; set; }
		public decimal? S8DippingNo2NipFrontActual2 { get; set; }
		public bool? S8DippingNo2NipBackSC { get; set; }
		public string S8DippingNo2NipBackSet { get; set; }
		public decimal? S8DippingNo2NipBackActual { get; set; }
		public string S8DippingNo2NipBackSet2 { get; set; }
		public decimal? S8DippingNo2NipBackActual2 { get; set; }
		public bool? S8DippingNo2WPUstdSC { get; set; }
		public string S8DippingNo2WPUstdSet { get; set; }
		public decimal? S8DippingNo2WPUstdActual { get; set; }
		public string S8DippingNo2WPUstdSet2 { get; set; }
		public decimal? S8DippingNo2WPUstdActual2 { get; set; }
		public bool? S9WinderAyameDaialSC { get; set; }
		public decimal? S9WinderAyameDaialSet { get; set; }
		public decimal? S9WinderAyameDaialActual { get; set; }
		public decimal? S9WinderAyameDaialSet2 { get; set; }
		public decimal? S9WinderAyameDaialActual2 { get; set; }
		public bool? S9WinderMpaSC { get; set; }
		public decimal? S9WinderMpaSet { get; set; }
		public decimal? S9WinderMpaActual { get; set; }
		public decimal? S9WinderMpaSet2 { get; set; }
		public decimal? S9WinderMpaActual2 { get; set; }
		public bool? S9WinderSpringSC { get; set; }
		public decimal? S9WinderSpringSet { get; set; }
		public decimal? S9WinderSpringActual { get; set; }
		public decimal? S9WinderSpringSet2 { get; set; }
		public decimal? S9WinderSpringActual2 { get; set; }
		public bool? S9WinderPeperTubeColorSC { get; set; }
		public string S9WinderPeperTubeColorSet { get; set; }
		public string S9WinderPeperTubeColorActual { get; set; }
		public string S9WinderPeperTubeColorSet2 { get; set; }
		public string S9WinderPeperTubeColorActual2 { get; set; }
		public bool? S9WinderCheeseWeightSC { get; set; }
		public string S9WinderCheeseWeightSet { get; set; }
		public decimal? S9WinderCheeseWeightActual { get; set; }
		public string S9WinderCheeseWeightSet2 { get; set; }
		public decimal? S9WinderCheeseWeightActual2 { get; set; }
		public bool? S9ExhaustFanOven1CirculatingFanSC { get; set; }
		public decimal? S9ExhaustFanOven1CirculatingFanSet { get; set; }
		public decimal? S9ExhaustFanOven1CirculatingFanActual { get; set; }
		public decimal? S9ExhaustFanOven1CirculatingFanSet2 { get; set; }
		public decimal? S9ExhaustFanOven1CirculatingFanActual2 { get; set; }
		public bool? S9ExhaustFanOven2CirculatingFanSC { get; set; }
		public decimal? S9ExhaustFanOven2CirculatingFanSet { get; set; }
		public decimal? S9ExhaustFanOven2CirculatingFanActual { get; set; }
		public decimal? S9ExhaustFanOven2CirculatingFanSet2 { get; set; }
		public decimal? S9ExhaustFanOven2CirculatingFanActual2 { get; set; }
		public bool? S9ExhaustFanOven1ExhaustFanSC { get; set; }
		public decimal? S9ExhaustFanOven1ExhaustFanSet { get; set; }
		public decimal? S9ExhaustFanOven1ExhaustFanActual { get; set; }
		public decimal? S9ExhaustFanOven1ExhaustFanSet2 { get; set; }
		public decimal? S9ExhaustFanOven1ExhaustFanActual2 { get; set; }
		public bool? S9ExhaustFanOven2ExhaustFanSC { get; set; }
		public decimal? S9ExhaustFanOven2ExhaustFanSet { get; set; }
		public decimal? S9ExhaustFanOven2ExhaustFanActual { get; set; }
		public decimal? S9ExhaustFanOven2ExhaustFanSet2 { get; set; }
		public decimal? S9ExhaustFanOven2ExhaustFanActual2 { get; set; }
		public bool? S9ExhaustFanOvenFrontExhaustFanSC { get; set; }
		public decimal? S9ExhaustFanOvenFrontExhaustFanSet { get; set; }
		public decimal? S9ExhaustFanOvenFrontExhaustFanActual { get; set; }
		public decimal? S9ExhaustFanOvenFrontExhaustFanSet2 { get; set; }
		public decimal? S9ExhaustFanOvenFrontExhaustFanActual2 { get; set; }
		public bool? S9ExhaustFanOvenBackExhaustFanSC { get; set; }
		public decimal? S9ExhaustFanOvenBackExhaustFanSet { get; set; }
		public decimal? S9ExhaustFanOvenBackExhaustFanActual { get; set; }
		public decimal? S9ExhaustFanOvenBackExhaustFanSet2 { get; set; }
		public decimal? S9ExhaustFanOvenBackExhaustFanActual2 { get; set; }
		public bool? S9SpongSC { get; set; }
		public string S9SpongSet { get; set; }
		public string S9SpongActual { get; set; }
		public string S9SpongSet2 { get; set; }
		public string S9SpongActual2 { get; set; }
		public System.String UpdateBy { get; set; }
		public System.DateTime? UpdateDate { get; set; }
		public System.String UpdateBy2 { get; set; }
		public System.DateTime? UpdateDate2 { get; set; }
		public System.String CheckedBy { get; set; }
		public System.DateTime? CheckedDate { get; set; }
		public System.String ApproveBy { get; set; }
		public System.DateTime? ApproveDate { get; set; }
		public System.String ShiftLeader { get; set; }
		public System.String ProductionManager { get; set; }
		public System.Int32? errNum { get; set; }
		public System.String errMsg { get; set; }

        #endregion

        #region Static Methods

        public static DIPCondition Create(string productCode)
        {
            var inst = new DIPCondition();
            var std = DIPConditionStd.Gets(productCode).Value().FirstOrDefault();
            Assign(std, inst);
            return inst;
        }

        public static void Assign(DIPConditionStd src, DIPCondition dst)
        {
            if (null != src && null != dst)
            {
                dst.ProductCode = src.ProductCode;
                dst.S7YarnCordStructureSC = src.S7YarnCordStructureSC;
                dst.S7YarnCordStructureSet = src.S7YarnCordStructureSet;
                dst.S7YarnCordStructureSet2 = src.S7YarnCordStructureSet2;
                dst.S7YarnYarnTypeSC = src.S7YarnYarnTypeSC;
                dst.S7YarnYarnTypeSet = src.S7YarnYarnTypeSet;
                dst.S7YarnYarnTypeSet2 = src.S7YarnYarnTypeSet2;
                dst.S7Yarn1stTwistSC = src.S7Yarn1stTwistSC;
                dst.S7Yarn1stTwistSet = src.S7Yarn1stTwistSet;
                dst.S7Yarn1stTwistSet2 = src.S7Yarn1stTwistSet2;
                dst.S7Yarn2ndTwistSC = src.S7Yarn2ndTwistSC;
                dst.S7Yarn2ndTwistSet = src.S7Yarn2ndTwistSet;
                dst.S7Yarn2ndTwistSet2 = src.S7Yarn2ndTwistSet2;
                dst.S7YarnLabelOfYarnSC = src.S7YarnLabelOfYarnSC;
                dst.S7YarnLabelOfYarnSet = src.S7YarnLabelOfYarnSet;
                dst.S7YarnLabelOfYarnSet2 = src.S7YarnLabelOfYarnSet2;
                dst.S7YarnWeightSC = src.S7YarnWeightSC;
                dst.S7YarnWeightSet = src.S7YarnWeightSet;
                dst.S7YarnWeightSetErr = src.S7YarnWeightSetErr;
                dst.S7YarnWeightSet2 = src.S7YarnWeightSet2;
                dst.S7YarnWeightSetErr2 = src.S7YarnWeightSetErr2;
                dst.S7CreelSettingUseTensorSC = src.S7CreelSettingUseTensorSC;
                dst.S7CreelSettingUseTensorSet = src.S7CreelSettingUseTensorSet;
                dst.S7CreelSettingUseTensorSet2 = src.S7CreelSettingUseTensorSet2;
                dst.S7CreelSettingKnotConditionSC = src.S7CreelSettingKnotConditionSC;
                dst.S7CreelSettingKnotConditionSet = src.S7CreelSettingKnotConditionSet;
                dst.S7CreelSettingKnotConditionSet2 = src.S7CreelSettingKnotConditionSet2;
                dst.S7CreelSettingSlubCatsweSC = src.S7CreelSettingSlubCatsweSC;
                dst.S7CreelSettingSlubCatsweSet = src.S7CreelSettingSlubCatsweSet;
                dst.S7CreelSettingSlubCatsweSet2 = src.S7CreelSettingSlubCatsweSet2;

                dst.S8StretchDryerSC = src.S8StretchDryerSC;
                dst.S8StretchDryerSet = src.S8StretchDryerSet;
                dst.S8StretchDryerSetErr = src.S8StretchDryerSetErr;
                dst.S8StretchDryerSet2 = src.S8StretchDryerSet2;
                dst.S8StretchDryerSetErr2 = src.S8StretchDryerSetErr2;
                dst.S8StretchStretchHotSC = src.S8StretchStretchHotSC;
                dst.S8StretchStretchHotSet = src.S8StretchStretchHotSet;
                dst.S8StretchStretchHotSetErr = src.S8StretchStretchHotSetErr;
                dst.S8StretchHotSet2 = src.S8StretchHotSet2;
                dst.S8StretchHotSetErr2 = src.S8StretchHotSetErr2;
                dst.S8StretchNormalSC = src.S8StretchNormalSC;
                dst.S8StretchNormalSet = src.S8StretchNormalSet;
                dst.S8StretchNormalSetErr = src.S8StretchNormalSetErr;
                dst.S8StretchNormalSet2 = src.S8StretchNormalSet2;
                dst.S8StretchNormalSetErr2 = src.S8StretchNormalSetErr2;
                dst.S8StretchTotalSC = src.S8StretchTotalSC;
                dst.S8StretchTotalSet = src.S8StretchTotalSet;
                dst.S8StretchTotalSetErr = src.S8StretchTotalSetErr;
                dst.S8StretchTotalSet2 = src.S8StretchTotalSet2;
                dst.S8StretchTotalSetErr2 = src.S8StretchTotalSetErr2;

                dst.S8TempDryerSC = src.S8TempDryerSC;
                dst.S8TempDryerSet = src.S8TempDryerSet;
                dst.S8TempDryerSetErr = src.S8TempDryerSetErr;

                dst.S8TempDryerSet2 = src.S8TempDryerSet2;
                dst.S8TempDryerSetErr2 = src.S8TempDryerSetErr2;

                dst.S8TempHotSC = src.S8TempHotSC;
                dst.S8TempHotSet = src.S8TempHotSet;
                dst.S8TempHotSetErr = src.S8TempHotSetErr;

                dst.S8TempHotSet2 = src.S8TempHotSet2;
                dst.S8TempHotSetErr2 = src.S8TempHotSetErr2;

                dst.S8TempNormalSC = src.S8TempNormalSC;
                dst.S8TempNormalSet = src.S8TempNormalSet;
                dst.S8TempNormalSetErr = src.S8TempNormalSetErr;
                dst.S8TempNormalSet2 = src.S8TempNormalSet2;
                dst.S8TempNormalSetErr2 = src.S8TempNormalSetErr2;
                dst.S8CounterSettingSC = src.S8CounterSettingSC;
                dst.S8CounterSettingSet = src.S8CounterSettingSet;
                dst.S8CounterSettingSet2 = src.S8CounterSettingSet2;
                dst.S8SpeedSC = src.S8SpeedSC;
                dst.S8SpeedSet = src.S8SpeedSet;
                dst.S8SpeedErr = src.S8SpeedErr;
                dst.S8SpeedSet2 = src.S8SpeedSet2;
                dst.S8SpeedSetErr2 = src.S8SpeedSetErr2;
                dst.S8NoOfCordsSC = src.S8NoOfCordsSC;
                dst.S8NoOfCordsSet = src.S8NoOfCordsSet;
                dst.S8NoOfCordsSet2 = src.S8NoOfCordsSet2;
                dst.S8SofnorSC = src.S8SofnorSC;
                dst.S8SofnorSet = src.S8SofnorSet;
                dst.S8SofnorSet2 = src.S8SofnorSet2;
                dst.S8DrawNipSC = src.S8DrawNipSC;
                dst.S8DrawNipSet = src.S8DrawNipSet;
                dst.S8DrawNipSet2 = src.S8DrawNipSet2;
                dst.S8DippingNo1ConcentrationSC = src.S8DippingNo1ConcentrationSC;
                dst.S8DippingNo1ConcentrationSet = src.S8DippingNo1ConcentrationSet;
                dst.S8DippingNo1ConcentrationSet2 = src.S8DippingNo1ConcentrationSet2;
                dst.S8DippingNo1NipFrontSC = src.S8DippingNo1NipFrontSC;
                dst.S8DippingNo1NipFrontSet = src.S8DippingNo1NipFrontSet;
                dst.S8DippingNo1NipFrontSet2 = src.S8DippingNo1NipFrontSet2;
                dst.S8DippingNo1NipBackSC = src.S8DippingNo1NipBackSC;
                dst.S8DippingNo1NipBackSet = src.S8DippingNo1NipBackSet;
                dst.S8DippingNo1NipBackSet2 = src.S8DippingNo1NipBackSet2;
                dst.S8DippingNo1WPUstdSC = src.S8DippingNo1WPUstdSC;
                dst.S8DippingNo1WPUstdSet = src.S8DippingNo1WPUstdSet;
                dst.S8DippingNo1WPUstdSet2 = src.S8DippingNo1WPUstdSet2;
                dst.S8DippingNo2ConcentrationSC = src.S8DippingNo2ConcentrationSC;
                dst.S8DippingNo2ConcentrationSet = src.S8DippingNo2ConcentrationSet;
                dst.S8DippingNo2ConcentrationSet2 = src.S8DippingNo2ConcentrationSet2;
                dst.S8DippingNo2NipFrontSC = src.S8DippingNo2NipFrontSC;
                dst.S8DippingNo2NipFrontSet = src.S8DippingNo2NipFrontSet;
                dst.S8DippingNo2NipFrontSet2 = src.S8DippingNo2NipFrontSet2;
                dst.S8DippingNo2NipBackSC = src.S8DippingNo2NipBackSC;
                dst.S8DippingNo2NipBackSet = src.S8DippingNo2NipBackSet;
                dst.S8DippingNo2NipBackSet2 = src.S8DippingNo2NipBackSet2;
                dst.S8DippingNo2WPUstdSC = src.S8DippingNo2WPUstdSC;
                dst.S8DippingNo2WPUstdSet = src.S8DippingNo2WPUstdSet;
                dst.S8DippingNo2WPUstdSet2 = src.S8DippingNo2WPUstdSet2;

                dst.S9WinderAyameDaialSC = src.S9WinderAyameDaialSC;
                dst.S9WinderAyameDaialSet = src.S9WinderAyameDaialSet;
                dst.S9WinderAyameDaialSet2 = src.S9WinderAyameDaialSet2;
                dst.S9WinderMpaSC = src.S9WinderMpaSC;
                dst.S9WinderMpaSet = src.S9WinderMpaSet;
                dst.S9WinderMpaSet2 = src.S9WinderMpaSet2;
                dst.S9WinderSpringSC = src.S9WinderSpringSC;
                dst.S9WinderSpringSet = src.S9WinderSpringSet;
                dst.S9WinderSpringSet2 = src.S9WinderSpringSet2;
                dst.S9WinderPeperTubeColorSC = src.S9WinderPeperTubeColorSC;
                dst.S9WinderPeperTubeColorSet = src.S9WinderPeperTubeColorSet;
                dst.S9WinderPeperTubeColorSet2 = src.S9WinderPeperTubeColorSet2;
                dst.S9WinderCheeseWeightSC = src.S9WinderCheeseWeightSC;
                dst.S9WinderCheeseWeightSet = src.S9WinderCheeseWeightSet;
                dst.S9WinderCheeseWeightSet2 = src.S9WinderCheeseWeightSet2;
                dst.S9ExhaustFanOven1CirculatingFanSC = src.S9ExhaustFanOven1CirculatingFanSC;
                dst.S9ExhaustFanOven1CirculatingFanSet = src.S9ExhaustFanOven1CirculatingFanSet;
                dst.S9ExhaustFanOven1CirculatingFanSet2 = src.S9ExhaustFanOven1CirculatingFanSet2;
                dst.S9ExhaustFanOven2CirculatingFanSC = src.S9ExhaustFanOven2CirculatingFanSC;
                dst.S9ExhaustFanOven2CirculatingFanSet = src.S9ExhaustFanOven2CirculatingFanSet;
                dst.S9ExhaustFanOven2CirculatingFanSet2 = src.S9ExhaustFanOven2CirculatingFanSet2;
                dst.S9ExhaustFanOven1ExhaustFanSC = src.S9ExhaustFanOven1ExhaustFanSC;
                dst.S9ExhaustFanOven1ExhaustFanSet = src.S9ExhaustFanOven1ExhaustFanSet;
                dst.S9ExhaustFanOven1ExhaustFanSet2 = src.S9ExhaustFanOven1ExhaustFanSet2;
                dst.S9ExhaustFanOven2ExhaustFanSC = src.S9ExhaustFanOven2ExhaustFanSC;
                dst.S9ExhaustFanOven2ExhaustFanSet = src.S9ExhaustFanOven2ExhaustFanSet;
                dst.S9ExhaustFanOven2ExhaustFanSet2 = src.S9ExhaustFanOven2ExhaustFanSet2;
                dst.S9ExhaustFanOvenFrontExhaustFanSC = src.S9ExhaustFanOvenFrontExhaustFanSC;
                dst.S9ExhaustFanOvenFrontExhaustFanSet = src.S9ExhaustFanOvenFrontExhaustFanSet;
                dst.S9ExhaustFanOvenFrontExhaustFanSet2 = src.S9ExhaustFanOvenFrontExhaustFanSet2;
                dst.S9ExhaustFanOvenBackExhaustFanSC = src.S9ExhaustFanOvenBackExhaustFanSC;
                dst.S9ExhaustFanOvenBackExhaustFanSet = src.S9ExhaustFanOvenBackExhaustFanSet;
                dst.S9ExhaustFanOvenBackExhaustFanSet2 = src.S9ExhaustFanOvenBackExhaustFanSet2;
                dst.S9SpongSC = src.S9SpongSC;
                dst.S9SpongSet = src.S9SpongSet;
                dst.S9SpongSet2 = src.S9SpongSet2;
                // Replace with 2 with 1
                dst.S7YarnCordStructureSet2 = src.S7YarnCordStructureSet;
                dst.S7YarnYarnTypeSet2 = src.S7YarnYarnTypeSet;
                dst.S7Yarn1stTwistSet2 = src.S7Yarn1stTwistSet;
                dst.S7Yarn2ndTwistSet2 = src.S7Yarn2ndTwistSet;
                dst.S7YarnLabelOfYarnSet2 = src.S7YarnLabelOfYarnSet;
                dst.S7YarnWeightSet2 = src.S7YarnWeightSet;
                dst.S7YarnWeightSetErr2 = src.S7YarnWeightSetErr;
                dst.S7CreelSettingUseTensorSet2 = src.S7CreelSettingUseTensorSet;
                dst.S7CreelSettingKnotConditionSet2 = src.S7CreelSettingKnotConditionSet;
                dst.S7CreelSettingSlubCatsweSet2 = src.S7CreelSettingSlubCatsweSet;
                dst.S8StretchDryerSet2 = src.S8StretchDryerSet;
                dst.S8StretchDryerSetErr2 = src.S8StretchDryerSetErr;
                dst.S8StretchHotSet2 = src.S8StretchStretchHotSet;
                dst.S8StretchHotSetErr2 = src.S8StretchStretchHotSetErr;
                dst.S8StretchNormalSet2 = src.S8StretchNormalSet;
                dst.S8StretchNormalSetErr2 = src.S8StretchNormalSetErr;
                dst.S8TempDryerSet2 = src.S8TempDryerSet;
                dst.S8TempDryerSetErr2 = src.S8TempDryerSetErr;
                dst.S8TempNormalSet2 = src.S8TempNormalSet;
                dst.S8TempNormalSetErr2 = src.S8TempNormalSetErr;
                dst.S8CounterSettingSet2 = src.S8CounterSettingSet;
                dst.S8SpeedSet2 = src.S8SpeedSet;
                dst.S8SpeedSetErr2 = src.S8SpeedErr;
                dst.S8NoOfCordsSet2 = src.S8NoOfCordsSet;
                dst.S8SofnorSet2 = src.S8SofnorSet;
                dst.S8DrawNipSet2 = src.S8DrawNipSet;
                dst.S8DippingNo1ConcentrationSet2 = src.S8DippingNo1ConcentrationSet;
                dst.S8DippingNo1NipFrontSet2 = src.S8DippingNo1NipFrontSet;
                dst.S8DippingNo1NipBackSet2 = src.S8DippingNo1NipBackSet;
                dst.S8DippingNo1WPUstdSet2 = src.S8DippingNo1WPUstdSet;
                dst.S8DippingNo2ConcentrationSet2 = src.S8DippingNo2ConcentrationSet;
                dst.S8DippingNo2NipFrontSet2 = src.S8DippingNo2NipFrontSet;
                dst.S8DippingNo2NipBackSet2 = src.S8DippingNo2NipBackSet;
                dst.S8DippingNo2WPUstdSet2 = src.S8DippingNo2WPUstdSet;
                dst.S9WinderAyameDaialSet2 = src.S9WinderAyameDaialSet;
                dst.S9WinderMpaSet2 = src.S9WinderMpaSet;
                dst.S9WinderSpringSet2 = src.S9WinderSpringSet;
                dst.S9WinderPeperTubeColorSet2 = src.S9WinderPeperTubeColorSet;
                dst.S9WinderCheeseWeightSet2 = src.S9WinderCheeseWeightSet;
                dst.S9ExhaustFanOven1CirculatingFanSet2 = src.S9ExhaustFanOven1CirculatingFanSet;
                dst.S9ExhaustFanOven2CirculatingFanSet2 = src.S9ExhaustFanOven2CirculatingFanSet;
                dst.S9ExhaustFanOven1ExhaustFanSet2 = src.S9ExhaustFanOven1ExhaustFanSet;
                dst.S9ExhaustFanOven2ExhaustFanSet2 = src.S9ExhaustFanOven2ExhaustFanSet;
                dst.S9ExhaustFanOvenFrontExhaustFanSet2 = src.S9ExhaustFanOvenFrontExhaustFanSet;
                dst.S9ExhaustFanOvenBackExhaustFanSet2 = src.S9ExhaustFanOvenBackExhaustFanSet;
                dst.S9SpongSet2 = src.S9SpongSet;
            }
        }


        public static NDbResult<DIPCondition> Gets(int? DIPPCId)
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<DIPCondition> ret = new NDbResult<DIPCondition>();

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
				var item = cnn.Query<DIPCondition>("GetDIPConditions", p,
					commandType: CommandType.StoredProcedure).FirstOrDefault();
				var data = item;
				ret.Success(data);
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

		/// <summary>
		/// Save
		/// </summary>
		/// <param name="value">The DIPCondition item to save.</param>
		/// <returns></returns>
		public static NDbResult<DIPCondition> Save(DIPCondition value)
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<DIPCondition> ret = new NDbResult<DIPCondition>();

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
			p.Add("@S7YarnCordStructureSC", value.S7YarnCordStructureSC);
			p.Add("@S7YarnCordStructureSet", value.S7YarnCordStructureSet);
			p.Add("@S7YarnCordStructureActual", value.S7YarnCordStructureActual);
			p.Add("@S7YarnCordStructureSet2", value.S7YarnCordStructureSet2);
			p.Add("@S7YarnCordStructureActual2", value.S7YarnCordStructureActual2);
			p.Add("@S7YarnYarnTypeSC", value.S7YarnYarnTypeSC);
			p.Add("@S7YarnYarnTypeSet", value.S7YarnYarnTypeSet);
			p.Add("@S7YarnYarnTypeActual", value.S7YarnYarnTypeActual);
			p.Add("@S7YarnYarnTypeSet2", value.S7YarnYarnTypeSet2);
			p.Add("@S7YarnYarnTypeActual2", value.S7YarnYarnTypeActual2);
			p.Add("@S7Yarn1stTwistSC", value.S7Yarn1stTwistSC);
			p.Add("@S7Yarn1stTwistSet", value.S7Yarn1stTwistSet);
			p.Add("@S7Yarn1stTwistActual", value.S7Yarn1stTwistActual);
			p.Add("@S7Yarn1stTwistSet2", value.S7Yarn1stTwistSet2);
			p.Add("@S7Yarn1stTwistActual2", value.S7Yarn1stTwistActual2);
			p.Add("@S7Yarn2ndTwistSC", value.S7Yarn2ndTwistSC);
			p.Add("@S7Yarn2ndTwistSet", value.S7Yarn2ndTwistSet);
			p.Add("@S7Yarn2ndTwistActual", value.S7Yarn2ndTwistActual);
			p.Add("@S7Yarn2ndTwistSet2", value.S7Yarn2ndTwistSet2);
			p.Add("@S7Yarn2ndTwistActual2", value.S7Yarn2ndTwistActual2);
			p.Add("@S7YarnLabelOfYarnSC", value.S7YarnLabelOfYarnSC);
			p.Add("@S7YarnLabelOfYarnSet", value.S7YarnLabelOfYarnSet);
			p.Add("@S7YarnLabelOfYarnActual", value.S7YarnLabelOfYarnActual);
			p.Add("@S7YarnLabelOfYarnSet2", value.S7YarnLabelOfYarnSet2);
			p.Add("@S7YarnLabelOfYarnActual2", value.S7YarnLabelOfYarnActual2);
			p.Add("@S7YarnWeightSC", value.S7YarnWeightSC);
			p.Add("@S7YarnWeightSet", value.S7YarnWeightSet);
			p.Add("@S7YarnWeightSetErr", value.S7YarnWeightSetErr);
			p.Add("@S7YarnWeightActual", value.S7YarnWeightActual);
			p.Add("@S7YarnWeightSet2", value.S7YarnWeightSet2);
			p.Add("@S7YarnWeightSetErr2", value.S7YarnWeightSetErr2);
			p.Add("@S7YarnWeightActual2", value.S7YarnWeightActual2);
			p.Add("@S7CreelSettingUseTensorSC", value.S7CreelSettingUseTensorSC);
			p.Add("@S7CreelSettingUseTensorSet", value.S7CreelSettingUseTensorSet);
			p.Add("@S7CreelSettingUseTensorActual", value.S7CreelSettingUseTensorActual);
			p.Add("@S7CreelSettingUseTensorSet2", value.S7CreelSettingUseTensorSet2);
			p.Add("@S7CreelSettingUseTensorActual2", value.S7CreelSettingUseTensorActual2);
			p.Add("@S7CreelSettingKnotConditionSC", value.S7CreelSettingKnotConditionSC);
			p.Add("@S7CreelSettingKnotConditionSet", value.S7CreelSettingKnotConditionSet);
			p.Add("@S7CreelSettingKnotConditionActual", value.S7CreelSettingKnotConditionActual);
			p.Add("@S7CreelSettingKnotConditionSet2", value.S7CreelSettingKnotConditionSet2);
			p.Add("@S7CreelSettingKnotConditionActual2", value.S7CreelSettingKnotConditionActual2);

			p.Add("@S7CreelSettingSlubCatsweSC", value.S7CreelSettingSlubCatsweSC);
			p.Add("@S7CreelSettingSlubCatsweSet", value.S7CreelSettingSlubCatsweSet);
			p.Add("@S7CreelSettingSlubCatsweActual", value.S7CreelSettingSlubCatsweActual);
			p.Add("@S7CreelSettingSlubCatsweSet2", value.S7CreelSettingSlubCatsweSet2);
			p.Add("@S7CreelSettingSlubCatsweActual2", value.S7CreelSettingSlubCatsweActual2);

			p.Add("@S8StretchDryerSC", value.S8StretchDryerSC);
			p.Add("@S8StretchDryerSet", value.S8StretchDryerSet);
			p.Add("@S8StretchDryerSetErr", value.S8StretchDryerSetErr);
			p.Add("@S8StretchDryerActual", value.S8StretchDryerActual);
			p.Add("@S8StretchDryerSet2", value.S8StretchDryerSet2);
			p.Add("@S8StretchDryerSetErr2", value.S8StretchDryerSetErr2);
			p.Add("@S8StretchDryerActual2", value.S8StretchDryerActual2);
			p.Add("@S8StretchStretchHotSC", value.S8StretchStretchHotSC);
			p.Add("@S8StretchStretchHotSet", value.S8StretchStretchHotSet);
			p.Add("@S8StretchStretchHotSetErr", value.S8StretchStretchHotSetErr);
			p.Add("@S8StretchHotActual", value.S8StretchHotActual);
			p.Add("@S8StretchHotSet2", value.S8StretchHotSet2);
			p.Add("@S8StretchHotSetErr2", value.S8StretchHotSetErr2);
			p.Add("@S8StretchHotActual2", value.S8StretchHotActual2);
			p.Add("@S8StretchNormalSC", value.S8StretchNormalSC);
			p.Add("@S8StretchNormalSet", value.S8StretchNormalSet);
			p.Add("@S8StretchNormalSetErr", value.S8StretchNormalSetErr);
			p.Add("@S8StretchNormalActual", value.S8StretchNormalActual);
			p.Add("@S8StretchNormalSet2", value.S8StretchNormalSet2);
			p.Add("@S8StretchNormalSetErr2", value.S8StretchNormalSetErr2);
			p.Add("@S8StretchNormalActual2", value.S8StretchNormalActual2);
			p.Add("@S8StretchTotalSC", value.S8StretchTotalSC);
			p.Add("@S8StretchTotalSet", value.S8StretchTotalSet);
			p.Add("@S8StretchTotalSetErr", value.S8StretchTotalSetErr);
			p.Add("@S8StretchTotalActual", value.S8StretchTotalActual);
			p.Add("@S8StretchTotalSet2", value.S8StretchTotalSet2);
			p.Add("@S8StretchTotalSetErr2", value.S8StretchTotalSetErr2);
			p.Add("@S8StretchTotalActual2", value.S8StretchTotalActual2);
			p.Add("@S8TempDryerSC", value.S8TempDryerSC);
			p.Add("@S8TempDryerSet", value.S8TempDryerSet);
			p.Add("@S8TempDryerSetErr", value.S8TempDryerSetErr);
			p.Add("@S8TempDryerActual", value.S8TempDryerActual);
			p.Add("@S8TempDryerSet2", value.S8TempDryerSet2);
			p.Add("@S8TempDryerSetErr2", value.S8TempDryerSetErr2);
			p.Add("@S8TempDryerActual2", value.S8TempDryerActual2);
			p.Add("@S8TempHotSC", value.S8TempHotSC);
			p.Add("@S8TempHotSet", value.S8TempHotSet);
			p.Add("@S8TempHotSetErr", value.S8TempHotSetErr);
			p.Add("@S8TempHotActual", value.S8TempHotActual);
			p.Add("@S8TempHotSet2", value.S8TempHotSet2);
			p.Add("@S8TempHotSetErr2", value.S8TempHotSetErr2);
			p.Add("@S8TempHotActual2", value.S8TempHotActual2);
			p.Add("@S8TempNormalSC", value.S8TempNormalSC);
			p.Add("@S8TempNormalSet", value.S8TempNormalSet);
			p.Add("@S8TempNormalSetErr", value.S8TempNormalSetErr);
			p.Add("@S8TempNormalActual", value.S8TempNormalActual);
			p.Add("@S8TempNormalSet2", value.S8TempNormalSet2);
			p.Add("@S8TempNormalSetErr2", value.S8TempNormalSetErr2);
			p.Add("@S8TempNormalActual2", value.S8TempNormalActual2);
			p.Add("@S8CounterSettingSC", value.S8CounterSettingSC);
			p.Add("@S8CounterSettingSet", value.S8CounterSettingSet);
			p.Add("@S8CounterSettingActual", value.S8CounterSettingActual);
			p.Add("@S8CounterSettingSet2", value.S8CounterSettingSet2);
			p.Add("@S8CounterSettingActual2", value.S8CounterSettingActual2);
			p.Add("@S8SpeedSC", value.S8SpeedSC);
			p.Add("@S8SpeedSet", value.S8SpeedSet);
			p.Add("@S8SpeedErr", value.S8SpeedErr);
			p.Add("@S8SpeedActual", value.S8SpeedActual);
			p.Add("@S8SpeedSet2", value.S8SpeedSet2);
			p.Add("@S8SpeedSetErr2", value.S8SpeedSetErr2);
			p.Add("@S8SpeedActual2", value.S8SpeedActual2);
			p.Add("@S8NoOfCordsSC", value.S8NoOfCordsSC);
			p.Add("@S8NoOfCordsSet", value.S8NoOfCordsSet);
			p.Add("@S8NoOfCordsActual", value.S8NoOfCordsActual);
			p.Add("@S8NoOfCordsSet2", value.S8NoOfCordsSet2);
			p.Add("@S8NoOfCordsActual2", value.S8NoOfCordsActual2);
			p.Add("@S8SofnorSC", value.S8SofnorSC);
			p.Add("@S8SofnorSet", value.S8SofnorSet);
			p.Add("@S8SofnorActual", value.S8SofnorActual);
			p.Add("@S8SofnorSet2", value.S8SofnorSet2);
			p.Add("@S8SofnorActual2", value.S8SofnorActual2);
			p.Add("@S8DrawNipSC", value.S8DrawNipSC);
			p.Add("@S8DrawNipSet", value.S8DrawNipSet);
			p.Add("@S8DrawNipActual", value.S8DrawNipActual);
			p.Add("@S8DrawNipSet2", value.S8DrawNipSet2);
			p.Add("@S8DrawNipActual2", value.S8DrawNipActual2);
			p.Add("@S8DippingNo1ConcentrationSC", value.S8DippingNo1ConcentrationSC);
			p.Add("@S8DippingNo1ConcentrationSet", value.S8DippingNo1ConcentrationSet);
			p.Add("@S8DippingNo1ConcentrationActual", value.S8DippingNo1ConcentrationActual);
			p.Add("@S8DippingNo1ConcentrationSet2", value.S8DippingNo1ConcentrationSet2);
			p.Add("@S8DippingNo1ConcentrationActual2", value.S8DippingNo1ConcentrationActual2);
			p.Add("@S8DippingNo1NipFrontSC", value.S8DippingNo1NipFrontSC);
			p.Add("@S8DippingNo1NipFrontSet", value.S8DippingNo1NipFrontSet);
			p.Add("@S8DippingNo1NipFrontActual", value.S8DippingNo1NipFrontActual);
			p.Add("@S8DippingNo1NipFrontSet2", value.S8DippingNo1NipFrontSet2);
			p.Add("@S8DippingNo1NipFrontActual2", value.S8DippingNo1NipFrontActual2);
			p.Add("@S8DippingNo1NipBackSC", value.S8DippingNo1NipBackSC);
			p.Add("@S8DippingNo1NipBackSet", value.S8DippingNo1NipBackSet);
			p.Add("@S8DippingNo1NipBackActual", value.S8DippingNo1NipBackActual);
			p.Add("@S8DippingNo1NipBackSet2", value.S8DippingNo1NipBackSet2);
			p.Add("@S8DippingNo1NipBackActual2", value.S8DippingNo1NipBackActual2);

			p.Add("@S8DippingNo1WPUstdSC", value.S8DippingNo1WPUstdSC);
			p.Add("@S8DippingNo1WPUstdSet", value.S8DippingNo1WPUstdSet);
			p.Add("@S8DippingNo1WPUstdActual", value.S8DippingNo1WPUstdActual);
			p.Add("@S8DippingNo1WPUstdSet2", value.S8DippingNo1WPUstdSet2);
			p.Add("@S8DippingNo1WPUstdActual2", value.S8DippingNo1WPUstdActual2);

			p.Add("@S8DippingNo2ConcentrationSC", value.S8DippingNo2ConcentrationSC);
			p.Add("@S8DippingNo2ConcentrationSet", value.S8DippingNo2ConcentrationSet);
			p.Add("@S8DippingNo2ConcentrationActual", value.S8DippingNo2ConcentrationActual);
			p.Add("@S8DippingNo2ConcentrationSet2", value.S8DippingNo2ConcentrationSet2);
			p.Add("@S8DippingNo2ConcentrationActual2", value.S8DippingNo2ConcentrationActual2);
			p.Add("@S8DippingNo2NipFrontSC", value.S8DippingNo2NipFrontSC);
			p.Add("@S8DippingNo2NipFrontSet", value.S8DippingNo2NipFrontSet);
			p.Add("@S8DippingNo2NipFrontActual", value.S8DippingNo2NipFrontActual);
			p.Add("@S8DippingNo2NipFrontSet2", value.S8DippingNo2NipFrontSet2);
			p.Add("@S8DippingNo2NipFrontActual2", value.S8DippingNo2NipFrontActual2);
			p.Add("@S8DippingNo2NipBackSC", value.S8DippingNo2NipBackSC);
			p.Add("@S8DippingNo2NipBackSet", value.S8DippingNo2NipBackSet);
			p.Add("@S8DippingNo2NipBackActual", value.S8DippingNo2NipBackActual);
			p.Add("@S8DippingNo2NipBackSet2", value.S8DippingNo2NipBackSet2);
			p.Add("@S8DippingNo2NipBackActual2", value.S8DippingNo2NipBackActual2);

			p.Add("@S8DippingNo2WPUstdSC", value.S8DippingNo2WPUstdSC);
			p.Add("@S8DippingNo2WPUstdSet", value.S8DippingNo2WPUstdSet);
			p.Add("@S8DippingNo2WPUstdActual", value.S8DippingNo2WPUstdActual);
			p.Add("@S8DippingNo2WPUstdSet2", value.S8DippingNo2WPUstdSet2);
			p.Add("@S8DippingNo2WPUstdActual2", value.S8DippingNo2WPUstdActual2);

			p.Add("@S9WinderAyameDaialSC", value.S9WinderAyameDaialSC);
			p.Add("@S9WinderAyameDaialSet", value.S9WinderAyameDaialSet);
			p.Add("@S9WinderAyameDaialActual", value.S9WinderAyameDaialActual);
			p.Add("@S9WinderAyameDaialSet2", value.S9WinderAyameDaialSet2);
			p.Add("@S9WinderAyameDaialActual2", value.S9WinderAyameDaialActual2);
			p.Add("@S9WinderMpaSC", value.S9WinderMpaSC);
			p.Add("@S9WinderMpaSet", value.S9WinderMpaSet);
			p.Add("@S9WinderMpaActual", value.S9WinderMpaActual);
			p.Add("@S9WinderMpaSet2", value.S9WinderMpaSet2);
			p.Add("@S9WinderMpaActual2", value.S9WinderMpaActual2);
			p.Add("@S9WinderSpringSC", value.S9WinderSpringSC);
			p.Add("@S9WinderSpringSet", value.S9WinderSpringSet);
			p.Add("@S9WinderSpringActual", value.S9WinderSpringActual);
			p.Add("@S9WinderSpringSet2", value.S9WinderSpringSet2);
			p.Add("@S9WinderSpringActual2", value.S9WinderSpringActual2);
			p.Add("@S9WinderPeperTubeColorSC", value.S9WinderPeperTubeColorSC);
			p.Add("@S9WinderPeperTubeColorSet", value.S9WinderPeperTubeColorSet);
			p.Add("@S9WinderPeperTubeColorActual", value.S9WinderPeperTubeColorActual);
			p.Add("@S9WinderPeperTubeColorSet2", value.S9WinderPeperTubeColorSet2);
			p.Add("@S9WinderPeperTubeColorActual2", value.S9WinderPeperTubeColorActual2);
			p.Add("@S9WinderCheeseWeightSC", value.S9WinderCheeseWeightSC);
			p.Add("@S9WinderCheeseWeightSet", value.S9WinderCheeseWeightSet);
			p.Add("@S9WinderCheeseWeightActual", value.S9WinderCheeseWeightActual);
			p.Add("@S9WinderCheeseWeightSet2", value.S9WinderCheeseWeightSet2);
			p.Add("@S9WinderCheeseWeightActual2", value.S9WinderCheeseWeightActual2);
			p.Add("@S9ExhaustFanOven1CirculatingFanSC", value.S9ExhaustFanOven1CirculatingFanSC);
			p.Add("@S9ExhaustFanOven1CirculatingFanSet", value.S9ExhaustFanOven1CirculatingFanSet);
			p.Add("@S9ExhaustFanOven1CirculatingFanActual", value.S9ExhaustFanOven1CirculatingFanActual);
			p.Add("@S9ExhaustFanOven1CirculatingFanSet2", value.S9ExhaustFanOven1CirculatingFanSet2);
			p.Add("@S9ExhaustFanOven1CirculatingFanActual2", value.S9ExhaustFanOven1CirculatingFanActual2);
			p.Add("@S9ExhaustFanOven2CirculatingFanSC", value.S9ExhaustFanOven2CirculatingFanSC);
			p.Add("@S9ExhaustFanOven2CirculatingFanSet", value.S9ExhaustFanOven2CirculatingFanSet);
			p.Add("@S9ExhaustFanOven2CirculatingFanActual", value.S9ExhaustFanOven2CirculatingFanActual);
			p.Add("@S9ExhaustFanOven2CirculatingFanSet2", value.S9ExhaustFanOven2CirculatingFanSet2);
			p.Add("@S9ExhaustFanOven2CirculatingFanActual2", value.S9ExhaustFanOven2CirculatingFanActual2);
			p.Add("@S9ExhaustFanOven1ExhaustFanSC", value.S9ExhaustFanOven1ExhaustFanSC);
			p.Add("@S9ExhaustFanOven1ExhaustFanSet", value.S9ExhaustFanOven1ExhaustFanSet);
			p.Add("@S9ExhaustFanOven1ExhaustFanActual", value.S9ExhaustFanOven1ExhaustFanActual);
			p.Add("@S9ExhaustFanOven1ExhaustFanSet2", value.S9ExhaustFanOven1ExhaustFanSet2);
			p.Add("@S9ExhaustFanOven1ExhaustFanActual2", value.S9ExhaustFanOven1ExhaustFanActual2);
			p.Add("@S9ExhaustFanOven2ExhaustFanSC", value.S9ExhaustFanOven2ExhaustFanSC);
			p.Add("@S9ExhaustFanOven2ExhaustFanSet", value.S9ExhaustFanOven2ExhaustFanSet);
			p.Add("@S9ExhaustFanOven2ExhaustFanActual", value.S9ExhaustFanOven2ExhaustFanActual);
			p.Add("@S9ExhaustFanOven2ExhaustFanSet2", value.S9ExhaustFanOven2ExhaustFanSet2);
			p.Add("@S9ExhaustFanOven2ExhaustFanActual2", value.S9ExhaustFanOven2ExhaustFanActual2);
			p.Add("@S9ExhaustFanOvenFrontExhaustFanSC", value.S9ExhaustFanOvenFrontExhaustFanSC);
			p.Add("@S9ExhaustFanOvenFrontExhaustFanSet", value.S9ExhaustFanOvenFrontExhaustFanSet);
			p.Add("@S9ExhaustFanOvenFrontExhaustFanActual", value.S9ExhaustFanOvenFrontExhaustFanActual);
			p.Add("@S9ExhaustFanOvenFrontExhaustFanSet2", value.S9ExhaustFanOvenFrontExhaustFanSet2);
			p.Add("@S9ExhaustFanOvenFrontExhaustFanActual2", value.S9ExhaustFanOvenFrontExhaustFanActual2);
			p.Add("@S9ExhaustFanOvenBackExhaustFanSC", value.S9ExhaustFanOvenBackExhaustFanSC);
			p.Add("@S9ExhaustFanOvenBackExhaustFanSet", value.S9ExhaustFanOvenBackExhaustFanSet);
			p.Add("@S9ExhaustFanOvenBackExhaustFanActual", value.S9ExhaustFanOvenBackExhaustFanActual);
			p.Add("@S9ExhaustFanOvenBackExhaustFanSet2", value.S9ExhaustFanOvenBackExhaustFanSet2);
			p.Add("@S9ExhaustFanOvenBackExhaustFanActual2", value.S9ExhaustFanOvenBackExhaustFanActual2);
			p.Add("@S9SpongSC", value.S9SpongSC);
			p.Add("@S9SpongSet", value.S9SpongSet);
			p.Add("@S9SpongActual", value.S9SpongActual);
			p.Add("@S9SpongSet2", value.S9SpongSet2);
			p.Add("@S9SpongActual2", value.S9SpongActual2);

			p.Add("@UpdateBy", value.UpdateBy);
			p.Add("@UpdateDate", value.UpdateDate);
			p.Add("@UpdateBy2", value.UpdateBy2);
			p.Add("@UpdateDate2", value.UpdateDate2);
			p.Add("@CheckedBy", value.CheckedBy);
			p.Add("@CheckedDate", value.CheckedDate);
			p.Add("@ApproveBy", value.ApproveBy);
			p.Add("@ApproveDate", value.ApproveDate);
			p.Add("@ShiftLeader", value.ShiftLeader);
			p.Add("@ProductionManager", value.ProductionManager);

            p.Add("@DIPConditionId", value.DIPConditionId, DbType.Int32, direction: ParameterDirection.InputOutput);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
			p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

			try
			{
				cnn.Execute("SaveDIPCondition", p, commandType: CommandType.StoredProcedure);
				ret.Success(value);

                // Set PK
                value.DIPConditionId = p.Get<dynamic>("@DIPConditionId");
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
