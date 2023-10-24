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
	public class DIPConditionStd
	{
		#region Public Proeprties

		public string ProductCode { get; set; }
		public bool? S7YarnCordStructureSC { get; set; }
		public string S7YarnCordStructureSet { get; set; }
		public string S7YarnCordStructureSet2 { get; set; }
		public bool? S7YarnYarnTypeSC { get; set; }
		public string S7YarnYarnTypeSet { get; set; }
		public string S7YarnYarnTypeSet2 { get; set; }
		public bool? S7Yarn1stTwistSC { get; set; }
		public string S7Yarn1stTwistSet { get; set; }
		public string S7Yarn1stTwistSet2 { get; set; }
		public bool? S7Yarn2ndTwistSC { get; set; }
		public string S7Yarn2ndTwistSet { get; set; }
		public string S7Yarn2ndTwistSet2 { get; set; }
		public bool? S7YarnLabelOfYarnSC { get; set; }
		public string S7YarnLabelOfYarnSet { get; set; }
		public string S7YarnLabelOfYarnSet2 { get; set; }
		public bool? S7YarnWeightSC { get; set; }
		public decimal? S7YarnWeightSet { get; set; }
		public decimal? S7YarnWeightSetErr { get; set; }
		public decimal? S7YarnWeightSet2 { get; set; }
		public decimal? S7YarnWeightSetErr2 { get; set; }
		public bool? S7CreelSettingUseTensorSC { get; set; }
		public string S7CreelSettingUseTensorSet { get; set; }
		public string S7CreelSettingUseTensorSet2 { get; set; }
		public bool? S7CreelSettingKnotConditionSC { get; set; }
		public decimal? S7CreelSettingKnotConditionSet { get; set; }
		public decimal? S7CreelSettingKnotConditionSet2 { get; set; }
		public bool? S7CreelSettingSlubCatsweSC { get; set; }
		public decimal? S7CreelSettingSlubCatsweSet { get; set; }
		public decimal? S7CreelSettingSlubCatsweSet2 { get; set; }
		public bool? S8StretchDryerSC { get; set; }
		public decimal? S8StretchDryerSet { get; set; }
		public decimal? S8StretchDryerSetErr { get; set; }
		public decimal? S8StretchDryerSet2 { get; set; }
		public decimal? S8StretchDryerSetErr2 { get; set; }
		public bool? S8StretchStretchHotSC { get; set; }
		public decimal? S8StretchStretchHotSet { get; set; }
		public decimal? S8StretchStretchHotSetErr { get; set; }
		public decimal? S8StretchHotSet2 { get; set; }
		public decimal? S8StretchHotSetErr2 { get; set; }
		public bool? S8StretchNormalSC { get; set; }
		public decimal? S8StretchNormalSet { get; set; }
		public decimal? S8StretchNormalSetErr { get; set; }
		public decimal? S8StretchNormalSet2 { get; set; }
		public decimal? S8StretchNormalSetErr2 { get; set; }
		public bool? S8StretchTotalSC { get; set; }
		public decimal? S8StretchTotalSet { get; set; }
		public decimal? S8StretchTotalSetErr { get; set; }
		public decimal? S8StretchTotalSet2 { get; set; }
		public decimal? S8StretchTotalSetErr2 { get; set; }
		public bool? S8TempDryerSC { get; set; }
		public decimal? S8TempDryerSet { get; set; }
		public decimal? S8TempDryerSetErr { get; set; }
		public decimal? S8TempDryerSet2 { get; set; }
		public decimal? S8TempDryerSetErr2 { get; set; }
		public bool? S8TempHotSC { get; set; }
		public decimal? S8TempHotSet { get; set; }
		public decimal? S8TempHotSetErr { get; set; }
		public decimal? S8TempHotSet2 { get; set; }
		public decimal? S8TempHotSetErr2 { get; set; }
		public bool? S8TempNormalSC { get; set; }
		public decimal? S8TempNormalSet { get; set; }
		public decimal? S8TempNormalSetErr { get; set; }
		public decimal? S8TempNormalSet2 { get; set; }
		public decimal? S8TempNormalSetErr2 { get; set; }
		public bool? S8CounterSettingSC { get; set; }
		public string S8CounterSettingSet { get; set; }
		public string S8CounterSettingSet2 { get; set; }
		public bool? S8SpeedSC { get; set; }
		public decimal? S8SpeedSet { get; set; }
		public decimal? S8SpeedErr { get; set; }
		public decimal? S8SpeedSet2 { get; set; }
		public decimal? S8SpeedSetErr2 { get; set; }
		public bool? S8NoOfCordsSC { get; set; }
		public decimal? S8NoOfCordsSet { get; set; }
		public decimal? S8NoOfCordsSet2 { get; set; }
		public bool? S8SofnorSC { get; set; }
		public string S8SofnorSet { get; set; }
		public string S8SofnorSet2 { get; set; }
		public bool? S8DrawNipSC { get; set; }
		public decimal? S8DrawNipSet { get; set; }
		public decimal? S8DrawNipSet2 { get; set; }
		public bool? S8DippingNo1ConcentrationSC { get; set; }
		public string S8DippingNo1ConcentrationSet { get; set; }
		public string S8DippingNo1ConcentrationSet2 { get; set; }
		public bool? S8DippingNo1NipFrontSC { get; set; }
		public string S8DippingNo1NipFrontSet { get; set; }
		public string S8DippingNo1NipFrontSet2 { get; set; }
		public bool? S8DippingNo1NipBackSC { get; set; }
		public string S8DippingNo1NipBackSet { get; set; }
		public string S8DippingNo1NipBackSet2 { get; set; }
		public bool? S8DippingNo1WPUstdSC { get; set; }
		public string S8DippingNo1WPUstdSet { get; set; }
		public string S8DippingNo1WPUstdSet2 { get; set; }
		public bool? S8DippingNo2ConcentrationSC { get; set; }
		public string S8DippingNo2ConcentrationSet { get; set; }
		public string S8DippingNo2ConcentrationSet2 { get; set; }
		public bool? S8DippingNo2NipFrontSC { get; set; }
		public string S8DippingNo2NipFrontSet { get; set; }
		public string S8DippingNo2NipFrontSet2 { get; set; }
		public bool? S8DippingNo2NipBackSC { get; set; }
		public string S8DippingNo2NipBackSet { get; set; }
		public string S8DippingNo2NipBackSet2 { get; set; }
		public bool? S8DippingNo2WPUstdSC { get; set; }
		public string S8DippingNo2WPUstdSet { get; set; }
		public string S8DippingNo2WPUstdSet2 { get; set; }
		public bool? S9WinderAyameDaialSC { get; set; }
		public decimal? S9WinderAyameDaialSet { get; set; }
		public decimal? S9WinderAyameDaialSet2 { get; set; }
		public bool? S9WinderMpaSC { get; set; }
		public decimal? S9WinderMpaSet { get; set; }
		public decimal? S9WinderMpaSet2 { get; set; }
		public bool? S9WinderSpringSC { get; set; }
		public decimal? S9WinderSpringSet { get; set; }
		public decimal? S9WinderSpringSet2 { get; set; }
		public bool? S9WinderPeperTubeColorSC { get; set; }
		public string S9WinderPeperTubeColorSet { get; set; }
		public string S9WinderPeperTubeColorSet2 { get; set; }
		public bool? S9WinderCheeseWeightSC { get; set; }
		public string S9WinderCheeseWeightSet { get; set; }
		public string S9WinderCheeseWeightSet2 { get; set; }
		public bool? S9ExhaustFanOven1CirculatingFanSC { get; set; }
		public decimal? S9ExhaustFanOven1CirculatingFanSet { get; set; }
		public decimal? S9ExhaustFanOven1CirculatingFanSet2 { get; set; }
		public bool? S9ExhaustFanOven2CirculatingFanSC { get; set; }
		public decimal? S9ExhaustFanOven2CirculatingFanSet { get; set; }
		public decimal? S9ExhaustFanOven2CirculatingFanSet2 { get; set; }
		public bool? S9ExhaustFanOven1ExhaustFanSC { get; set; }
		public decimal? S9ExhaustFanOven1ExhaustFanSet { get; set; }
		public decimal? S9ExhaustFanOven1ExhaustFanSet2 { get; set; }
		public bool? S9ExhaustFanOven2ExhaustFanSC { get; set; }
		public decimal? S9ExhaustFanOven2ExhaustFanSet { get; set; }
		public decimal? S9ExhaustFanOven2ExhaustFanSet2 { get; set; }
		public bool? S9ExhaustFanOvenFrontExhaustFanSC { get; set; }
		public decimal? S9ExhaustFanOvenFrontExhaustFanSet { get; set; }
		public decimal? S9ExhaustFanOvenFrontExhaustFanSet2 { get; set; }
		public bool? S9ExhaustFanOvenBackExhaustFanSC { get; set; }
		public decimal? S9ExhaustFanOvenBackExhaustFanSet { get; set; }
		public decimal? S9ExhaustFanOvenBackExhaustFanSet2 { get; set; }
		public bool? S9SpongSC { get; set; }
		public string S9SpongSet { get; set; }
		public string S9SpongSet2 { get; set; }


		#endregion

		#region Static Methods

		public static NDbResult<DIPConditionStd> GetCurrent()
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<DIPConditionStd> ret = new NDbResult<DIPConditionStd>();

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

			try
			{
				var item = cnn.Query<DIPConditionStd>("GetDIPConditionStd", p,
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
		/// <param name="value">The DIPConditionStd item to save.</param>
		/// <returns></returns>
		public static NDbResult<DIPConditionStd> Save(DIPConditionStd value)
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<DIPConditionStd> ret = new NDbResult<DIPConditionStd>();

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

			p.Add("@ProductCode", value.ProductCode);
			p.Add("@S7YarnCordStructureSC", value.S7YarnCordStructureSC);
			p.Add("@S7YarnCordStructureSet", value.S7YarnCordStructureSet);
			p.Add("@S7YarnCordStructureSet2", value.S7YarnCordStructureSet2);
			p.Add("@S7YarnYarnTypeSC", value.S7YarnYarnTypeSC);
			p.Add("@S7YarnYarnTypeSet", value.S7YarnYarnTypeSet);
			p.Add("@S7YarnYarnTypeSet2", value.S7YarnYarnTypeSet2);
			p.Add("@S7Yarn1stTwistSC", value.S7Yarn1stTwistSC);
			p.Add("@S7Yarn1stTwistSet", value.S7Yarn1stTwistSet);
			p.Add("@S7Yarn1stTwistSet2", value.S7Yarn1stTwistSet2);
			p.Add("@S7Yarn2ndTwistSC", value.S7Yarn2ndTwistSC);
			p.Add("@S7Yarn2ndTwistSet", value.S7Yarn2ndTwistSet);
			p.Add("@S7Yarn2ndTwistSet2", value.S7Yarn2ndTwistSet2);
			p.Add("@S7YarnLabelOfYarnSC", value.S7YarnLabelOfYarnSC);
			p.Add("@S7YarnLabelOfYarnSet", value.S7YarnLabelOfYarnSet);
			p.Add("@S7YarnLabelOfYarnSet2", value.S7YarnLabelOfYarnSet2);
			p.Add("@S7YarnWeightSC", value.S7YarnWeightSC);
			p.Add("@S7YarnWeightSet", value.S7YarnWeightSet);
			p.Add("@S7YarnWeightSetErr", value.S7YarnWeightSetErr);
			p.Add("@S7YarnWeightSet2", value.S7YarnWeightSet2);
			p.Add("@S7YarnWeightSetErr2", value.S7YarnWeightSetErr2);
			p.Add("@S7CreelSettingUseTensorSC", value.S7CreelSettingUseTensorSC);
			p.Add("@S7CreelSettingUseTensorSet", value.S7CreelSettingUseTensorSet);
			p.Add("@S7CreelSettingUseTensorSet2", value.S7CreelSettingUseTensorSet2);
			p.Add("@S7CreelSettingKnotConditionSC", value.S7CreelSettingKnotConditionSC);
			p.Add("@S7CreelSettingKnotConditionSet", value.S7CreelSettingKnotConditionSet);
			p.Add("@S7CreelSettingKnotConditionSet2", value.S7CreelSettingKnotConditionSet2);

			p.Add("@S7CreelSettingSlubCatsweSC", value.S7CreelSettingSlubCatsweSC);
			p.Add("@S7CreelSettingSlubCatsweSet", value.S7CreelSettingSlubCatsweSet);
			p.Add("@S7CreelSettingSlubCatsweSet2", value.S7CreelSettingSlubCatsweSet2);

			p.Add("@S8StretchDryerSC", value.S8StretchDryerSC);
			p.Add("@S8StretchDryerSet", value.S8StretchDryerSet);
			p.Add("@S8StretchDryerSetErr", value.S8StretchDryerSetErr);
			p.Add("@S8StretchDryerSet2", value.S8StretchDryerSet2);
			p.Add("@S8StretchDryerSetErr2", value.S8StretchDryerSetErr2);
			p.Add("@S8StretchStretchHotSC", value.S8StretchStretchHotSC);
			p.Add("@S8StretchStretchHotSet", value.S8StretchStretchHotSet);
			p.Add("@S8StretchStretchHotSetErr", value.S8StretchStretchHotSetErr);
			p.Add("@S8StretchHotSet2", value.S8StretchHotSet2);
			p.Add("@S8StretchHotSetErr2", value.S8StretchHotSetErr2);
			p.Add("@S8StretchNormalSC", value.S8StretchNormalSC);
			p.Add("@S8StretchNormalSet", value.S8StretchNormalSet);
			p.Add("@S8StretchNormalSetErr", value.S8StretchNormalSetErr);
			p.Add("@S8StretchNormalSet2", value.S8StretchNormalSet2);
			p.Add("@S8StretchNormalSetErr2", value.S8StretchNormalSetErr2);
			p.Add("@S8StretchTotalSC", value.S8StretchTotalSC);
			p.Add("@S8StretchTotalSet", value.S8StretchTotalSet);
			p.Add("@S8StretchTotalSetErr", value.S8StretchTotalSetErr);
			p.Add("@S8StretchTotalSet2", value.S8StretchTotalSet2);
			p.Add("@S8StretchTotalSetErr2", value.S8StretchTotalSetErr2);
			p.Add("@S8TempDryerSC", value.S8TempDryerSC);
			p.Add("@S8TempDryerSet", value.S8TempDryerSet);
			p.Add("@S8TempDryerSetErr", value.S8TempDryerSetErr);
			p.Add("@S8TempDryerSet2", value.S8TempDryerSet2);
			p.Add("@S8TempDryerSetErr2", value.S8TempDryerSetErr2);
			p.Add("@S8TempHotSC", value.S8TempHotSC);
			p.Add("@S8TempHotSet", value.S8TempHotSet);
			p.Add("@S8TempHotSetErr", value.S8TempHotSetErr);
			p.Add("@S8TempHotSet2", value.S8TempHotSet2);
			p.Add("@S8TempHotSetErr2", value.S8TempHotSetErr2);
			p.Add("@S8TempNormalSC", value.S8TempNormalSC);
			p.Add("@S8TempNormalSet", value.S8TempNormalSet);
			p.Add("@S8TempNormalSetErr", value.S8TempNormalSetErr);
			p.Add("@S8TempNormalSet2", value.S8TempNormalSet2);
			p.Add("@S8TempNormalSetErr2", value.S8TempNormalSetErr2);
			p.Add("@S8CounterSettingSC", value.S8CounterSettingSC);
			p.Add("@S8CounterSettingSet", value.S8CounterSettingSet);
			p.Add("@S8CounterSettingSet2", value.S8CounterSettingSet2);
			p.Add("@S8SpeedSC", value.S8SpeedSC);
			p.Add("@S8SpeedSet", value.S8SpeedSet);
			p.Add("@S8SpeedErr", value.S8SpeedErr);
			p.Add("@S8SpeedSet2", value.S8SpeedSet2);
			p.Add("@S8SpeedSetErr2", value.S8SpeedSetErr2);
			p.Add("@S8NoOfCordsSC", value.S8NoOfCordsSC);
			p.Add("@S8NoOfCordsSet", value.S8NoOfCordsSet);
			p.Add("@S8NoOfCordsSet2", value.S8NoOfCordsSet2);
			p.Add("@S8SofnorSC", value.S8SofnorSC);
			p.Add("@S8SofnorSet", value.S8SofnorSet);
			p.Add("@S8SofnorSet2", value.S8SofnorSet2);
			p.Add("@S8DrawNipSC", value.S8DrawNipSC);
			p.Add("@S8DrawNipSet", value.S8DrawNipSet);
			p.Add("@S8DrawNipSet2", value.S8DrawNipSet2);
			p.Add("@S8DippingNo1ConcentrationSC", value.S8DippingNo1ConcentrationSC);
			p.Add("@S8DippingNo1ConcentrationSet", value.S8DippingNo1ConcentrationSet);
			p.Add("@S8DippingNo1ConcentrationSet2", value.S8DippingNo1ConcentrationSet2);
			p.Add("@S8DippingNo1NipFrontSC", value.S8DippingNo1NipFrontSC);
			p.Add("@S8DippingNo1NipFrontSet", value.S8DippingNo1NipFrontSet);
			p.Add("@S8DippingNo1NipFrontSet2", value.S8DippingNo1NipFrontSet2);
			p.Add("@S8DippingNo1NipBackSC", value.S8DippingNo1NipBackSC);
			p.Add("@S8DippingNo1NipBackSet", value.S8DippingNo1NipBackSet);
			p.Add("@S8DippingNo1NipBackSet2", value.S8DippingNo1NipBackSet2);

			p.Add("@S8DippingNo1WPUstdSC", value.S8DippingNo1WPUstdSC);
			p.Add("@S8DippingNo1WPUstdSet", value.S8DippingNo1WPUstdSet);
			p.Add("@S8DippingNo1WPUstdSet2", value.S8DippingNo1WPUstdSet2);

			p.Add("@S8DippingNo2ConcentrationSC", value.S8DippingNo2ConcentrationSC);
			p.Add("@S8DippingNo2ConcentrationSet", value.S8DippingNo2ConcentrationSet);
			p.Add("@S8DippingNo2ConcentrationSet2", value.S8DippingNo2ConcentrationSet2);
			p.Add("@S8DippingNo2NipFrontSC", value.S8DippingNo2NipFrontSC);
			p.Add("@S8DippingNo2NipFrontSet", value.S8DippingNo2NipFrontSet);
			p.Add("@S8DippingNo2NipFrontSet2", value.S8DippingNo2NipFrontSet2);
			p.Add("@S8DippingNo2NipBackSC", value.S8DippingNo2NipBackSC);
			p.Add("@S8DippingNo2NipBackSet", value.S8DippingNo2NipBackSet);
			p.Add("@S8DippingNo2NipBackSet2", value.S8DippingNo2NipBackSet2);

			p.Add("@S8DippingNo2WPUstdSC", value.S8DippingNo2WPUstdSC);
			p.Add("@S8DippingNo2WPUstdSet", value.S8DippingNo2WPUstdSet);
			p.Add("@S8DippingNo2WPUstdSet2", value.S8DippingNo2WPUstdSet2);

			p.Add("@S9WinderAyameDaialSC", value.S9WinderAyameDaialSC);
			p.Add("@S9WinderAyameDaialSet", value.S9WinderAyameDaialSet);
			p.Add("@S9WinderAyameDaialSet2", value.S9WinderAyameDaialSet2);
			p.Add("@S9WinderMpaSC", value.S9WinderMpaSC);
			p.Add("@S9WinderMpaSet", value.S9WinderMpaSet);
			p.Add("@S9WinderMpaSet2", value.S9WinderMpaSet2);
			p.Add("@S9WinderSpringSC", value.S9WinderSpringSC);
			p.Add("@S9WinderSpringSet", value.S9WinderSpringSet);
			p.Add("@S9WinderSpringSet2", value.S9WinderSpringSet2);
			p.Add("@S9WinderPeperTubeColorSC", value.S9WinderPeperTubeColorSC);
			p.Add("@S9WinderPeperTubeColorSet", value.S9WinderPeperTubeColorSet);
			p.Add("@S9WinderPeperTubeColorSet2", value.S9WinderPeperTubeColorSet2);
			p.Add("@S9WinderCheeseWeightSC", value.S9WinderCheeseWeightSC);
			p.Add("@S9WinderCheeseWeightSet", value.S9WinderCheeseWeightSet);
			p.Add("@S9WinderCheeseWeightSet2", value.S9WinderCheeseWeightSet2);
			p.Add("@S9ExhaustFanOven1CirculatingFanSC", value.S9ExhaustFanOven1CirculatingFanSC);
			p.Add("@S9ExhaustFanOven1CirculatingFanSet", value.S9ExhaustFanOven1CirculatingFanSet);
			p.Add("@S9ExhaustFanOven1CirculatingFanSet2", value.S9ExhaustFanOven1CirculatingFanSet2);
			p.Add("@S9ExhaustFanOven2CirculatingFanSC", value.S9ExhaustFanOven2CirculatingFanSC);
			p.Add("@S9ExhaustFanOven2CirculatingFanSet", value.S9ExhaustFanOven2CirculatingFanSet);
			p.Add("@S9ExhaustFanOven2CirculatingFanSet2", value.S9ExhaustFanOven2CirculatingFanSet2);
			p.Add("@S9ExhaustFanOven1ExhaustFanSC", value.S9ExhaustFanOven1ExhaustFanSC);
			p.Add("@S9ExhaustFanOven1ExhaustFanSet", value.S9ExhaustFanOven1ExhaustFanSet);
			p.Add("@S9ExhaustFanOven1ExhaustFanSet2", value.S9ExhaustFanOven1ExhaustFanSet2);
			p.Add("@S9ExhaustFanOven2ExhaustFanSC", value.S9ExhaustFanOven2ExhaustFanSC);
			p.Add("@S9ExhaustFanOven2ExhaustFanSet", value.S9ExhaustFanOven2ExhaustFanSet);
			p.Add("@S9ExhaustFanOven2ExhaustFanSet2", value.S9ExhaustFanOven2ExhaustFanSet2);
			p.Add("@S9ExhaustFanOvenFrontExhaustFanSC", value.S9ExhaustFanOvenFrontExhaustFanSC);
			p.Add("@S9ExhaustFanOvenFrontExhaustFanSet", value.S9ExhaustFanOvenFrontExhaustFanSet);
			p.Add("@S9ExhaustFanOvenFrontExhaustFanSet2", value.S9ExhaustFanOvenFrontExhaustFanSet2);
			p.Add("@S9ExhaustFanOvenBackExhaustFanSC", value.S9ExhaustFanOvenBackExhaustFanSC);
			p.Add("@S9ExhaustFanOvenBackExhaustFanSet", value.S9ExhaustFanOvenBackExhaustFanSet);
			p.Add("@S9ExhaustFanOvenBackExhaustFanSet2", value.S9ExhaustFanOvenBackExhaustFanSet2);
			p.Add("@S9SpongSC", value.S9SpongSC);
			p.Add("@S9SpongSet", value.S9SpongSet);
			p.Add("@S9SpongSet2", value.S9SpongSet2);

			p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
			p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

			try
			{
				cnn.Execute("SaveDIPConditionStd", p, commandType: CommandType.StoredProcedure);
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

		public static NDbResult Delete(DIPConditionStd value)
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult ret = new NDbResult();

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
			p.Add("@ProductCode", value.ProductCode);

			try
			{
				cnn.Execute("DELETE FROM DIPConditionStd WHERE ProductCode = @ProductCode", p, commandType: CommandType.Text);
				ret.Success();
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
