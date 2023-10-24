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
	public class DIPProductionCondition
	{
		#region Public Proeprties

		public string ProductCode { get; set; }
		public string S7CordStructure { get; set; }
		public string S7YarnType { get; set; }
		public string S71stTwist { get; set; }
		public string S72ndTwist { get; set; }
		public string S7ShapeOfYarn { get; set; }
		public decimal? S7Weight { get; set; }
		public string S7ProcessSettingTension { get; set; }
		public string S7UseTensor { get; set; }
		public string S7KnotCondition { get; set; }
		public string S7SuperNylonKnot { get; set; }
		public bool? S8DryeStretchSC { get; set; }
		public decimal? S8DryeStretch { get; set; }
		public decimal? S8DryeStretchErr { get; set; }
		public bool? S8DryeTempSC { get; set; }
		public decimal? S8DryeTemp { get; set; }
		public decimal? S8DryeTempErr { get; set; }
		public decimal? S8DryePass { get; set; }
		public string S8DryeTension { get; set; }
		public bool? S8HotStretchSC { get; set; }
		public decimal? S8HotStretch { get; set; }
		public decimal? S8HotStretchErr { get; set; }
		public bool? S8HotTempSC { get; set; }
		public decimal? S8HotTemp { get; set; }
		public decimal? S8HotTempErr { get; set; }
		public decimal? S8HotPass { get; set; }
		public string S8HotTension { get; set; }
		public bool? S8NormalStretchSC { get; set; }
		public decimal? S8NormalStretch { get; set; }
		public decimal? S8NormalStretchErr { get; set; }
		public bool? S8NormalTempSC { get; set; }
		public decimal? S8NormalTemp { get; set; }
		public decimal? S8NormalTempErr { get; set; }
		public decimal? S8NormalPass { get; set; }
		public string S8NormalTension { get; set; }
		public bool? S8TotalStretchSC { get; set; }
		public decimal? S8TotalStretch { get; set; }
		public decimal? S8TotalStretchErr { get; set; }
		public bool? S8TotalTempSC { get; set; }
		public decimal? S8TotalTemp { get; set; }
		public decimal? S8TotalTempErr { get; set; }
		public decimal? S8TotalPass { get; set; }
		public string S8TotalTension { get; set; }
		public bool? S8SpeedSC { get; set; }
		public decimal? S8Speed { get; set; }
		public decimal? S8SpeedErr { get; set; }
		public decimal? S8NoOfCordsOfProcessing { get; set; }
		public decimal? S8NoOfCordsOfProcessingErr { get; set; }
		public string S8Sofnor { get; set; }
		public string S8DrawNip { get; set; }
		public string S8Concentration { get; set; }
		public decimal? S8NipRollerPressureNo1Front { get; set; }
		public decimal? S8NipRollerPressureNo1FrontErr { get; set; }
		public decimal? S8NipRollerPressureNo2Front { get; set; }
		public decimal? S8NipRollerPressureNo2FrontErr { get; set; }
		public decimal? S8NipRollerPressureNo1Back { get; set; }
		public decimal? S8NipRollerPressureNo1BackErr { get; set; }
		public decimal? S8NipRollerPressureNo2Back { get; set; }
		public decimal? S8NipRollerPressureNo2BackErr { get; set; }
		public decimal? S8WPUStandardNo1 { get; set; }
		public decimal? S8WPUStandardNo1Err { get; set; }
		public decimal? S8WPUStandardNo2 { get; set; }
		public decimal? S8WPUStandardNo2Err { get; set; }
		public bool? S9ContactPressureUp { get; set; }
		public bool? S9ContactPressureDown { get; set; }
		public string S9ContactPressure { get; set; }
		public decimal? S9BaseWindGear { get; set; }
		public bool? S9TraverseGuideSizeLrage { get; set; }
		public bool? S9TraverseGuideSizeSmall { get; set; }
		public string S9TraverseGuideSize { get; set; }
		public decimal? S9WindVoltageSet { get; set; }
		public decimal? S9WindVoltageSetErr { get; set; }
		public decimal? S9AyameDaial { get; set; }
		public decimal? S9AyameDaialErr { get; set; }
		public bool? S9TensionSpringLrage { get; set; }
		public bool? S9TensionSpringSmall { get; set; }
		public string S9TensionSpring { get; set; }
		public string S9TensionWind { get; set; }
		public decimal? S9PackingStandard { get; set; }
		public decimal? S9PackingStandardErr { get; set; }
		public string S9TubeColor { get; set; }
		public string S9Tail { get; set; }
		public decimal? S9CounterSetting { get; set; }
		public decimal? S9CounterSettingErr { get; set; }
		public decimal? S9CheeseWeight { get; set; }
		public decimal? S9CheeseWeightErr { get; set; }
		public string S9HowToStartProductionEng { get; set; }
		public string S9HowToStartProductionTh { get; set; }
		public string S9Rewind { get; set; }
		public string S9Package { get; set; }
		public string S9Sampling { get; set; }


		#endregion

		#region Static Methods

		public static NDbResult<DIPProductionCondition> GetCurrent()
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<DIPProductionCondition> ret = new NDbResult<DIPProductionCondition>();

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
				var item = cnn.Query<DIPProductionCondition>("GetDIPProductionConditions", p,
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
		/// <param name="value">The DIPProductionCondition item to save.</param>
		/// <returns></returns>
		public static NDbResult<DIPProductionCondition> Save(DIPProductionCondition value)
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<DIPProductionCondition> ret = new NDbResult<DIPProductionCondition>();

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
			p.Add("@S7CordStructure", value.S7CordStructure);
			p.Add("@S7YarnType", value.S7YarnType);
			p.Add("@S71stTwist", value.S71stTwist);
			p.Add("@S72ndTwist", value.S72ndTwist);
			p.Add("@S7ShapeOfYarn", value.S7ShapeOfYarn);
			p.Add("@S7Weight", value.S7Weight);
			p.Add("@S7ProcessSettingTension", value.S7ProcessSettingTension);
			p.Add("@S7UseTensor", value.S7UseTensor);
			p.Add("@S7KnotCondition", value.S7KnotCondition);
			p.Add("@S7SuperNylonKnot", value.S7SuperNylonKnot);
			p.Add("@S8DryeStretchSC", value.S8DryeStretchSC);
			p.Add("@S8DryeStretch", value.S8DryeStretch);
			p.Add("@S8DryeStretchErr", value.S8DryeStretchErr);
			p.Add("@S8DryeTempSC", value.S8DryeTempSC);
			p.Add("@S8DryeTemp", value.S8DryeTemp);
			p.Add("@S8DryeTempErr", value.S8DryeTempErr);
			p.Add("@S8DryePass", value.S8DryePass);
			p.Add("@S8DryeTension", value.S8DryeTension);
			p.Add("@S8HotStretchSC", value.S8HotStretchSC);
			p.Add("@S8HotStretch", value.S8HotStretch);
			p.Add("@S8HotStretchErr", value.S8HotStretchErr);
			p.Add("@S8HotTempSC", value.S8HotTempSC);
			p.Add("@S8HotTemp", value.S8HotTemp);
			p.Add("@S8HotTempErr", value.S8HotTempErr);
			p.Add("@S8HotPass", value.S8HotPass);
			p.Add("@S8HotTension", value.S8HotTension);
			p.Add("@S8NormalStretchSC", value.S8NormalStretchSC);
			p.Add("@S8NormalStretch", value.S8NormalStretch);
			p.Add("@S8NormalStretchErr", value.S8NormalStretchErr);
			p.Add("@S8NormalTempSC", value.S8NormalTempSC);
			p.Add("@S8NormalTemp", value.S8NormalTemp);
			p.Add("@S8NormalTempErr", value.S8NormalTempErr);
			p.Add("@S8NormalPass", value.S8NormalPass);
			p.Add("@S8NormalTension", value.S8NormalTension);
			p.Add("@S8TotalStretchSC", value.S8TotalStretchSC);
			p.Add("@S8TotalStretch", value.S8TotalStretch);
			p.Add("@S8TotalStretchErr", value.S8TotalStretchErr);
			p.Add("@S8TotalTempSC", value.S8TotalTempSC);
			p.Add("@S8TotalTemp", value.S8TotalTemp);
			p.Add("@S8TotalTempErr", value.S8TotalTempErr);
			p.Add("@S8TotalPass", value.S8TotalPass);
			p.Add("@S8TotalTension", value.S8TotalTension);
			p.Add("@S8SpeedSC", value.S8SpeedSC);
			p.Add("@S8Speed", value.S8Speed);
			p.Add("@S8SpeedErr", value.S8SpeedErr);
			p.Add("@S8NoOfCordsOfProcessing", value.S8NoOfCordsOfProcessing);
			p.Add("@S8NoOfCordsOfProcessingErr", value.S8NoOfCordsOfProcessingErr);
			p.Add("@S8Sofnor", value.S8Sofnor);
			p.Add("@S8DrawNip", value.S8DrawNip);
			p.Add("@S8Concentration", value.S8Concentration);
			p.Add("@S8NipRollerPressureNo1Front", value.S8NipRollerPressureNo1Front);
			p.Add("@S8NipRollerPressureNo1FrontErr", value.S8NipRollerPressureNo1FrontErr);
			p.Add("@S8NipRollerPressureNo2Front", value.S8NipRollerPressureNo2Front);
			p.Add("@S8NipRollerPressureNo2FrontErr", value.S8NipRollerPressureNo2FrontErr);
			p.Add("@S8NipRollerPressureNo1Back", value.S8NipRollerPressureNo1Back);
			p.Add("@S8NipRollerPressureNo1BackErr", value.S8NipRollerPressureNo1BackErr);
			p.Add("@S8NipRollerPressureNo2Back", value.S8NipRollerPressureNo2Back);
			p.Add("@S8NipRollerPressureNo2BackErr", value.S8NipRollerPressureNo2BackErr);
			p.Add("@S8WPUStandardNo1", value.S8WPUStandardNo1);
			p.Add("@S8WPUStandardNo1Err", value.S8WPUStandardNo1Err);
			p.Add("@S8WPUStandardNo2", value.S8WPUStandardNo2);
			p.Add("@S8WPUStandardNo2Err", value.S8WPUStandardNo2Err);
			p.Add("@S9ContactPressureUp", value.S9ContactPressureUp);
			p.Add("@S9ContactPressureDown", value.S9ContactPressureDown);
			p.Add("@S9ContactPressure", value.S9ContactPressure);
			p.Add("@S9BaseWindGear", value.S9BaseWindGear);
			p.Add("@S9TraverseGuideSizeLrage", value.S9TraverseGuideSizeLrage);
			p.Add("@S9TraverseGuideSizeSmall", value.S9TraverseGuideSizeSmall);
			p.Add("@S9TraverseGuideSize", value.S9TraverseGuideSize);
			p.Add("@S9WindVoltageSet", value.S9WindVoltageSet);
			p.Add("@S9WindVoltageSetErr", value.S9WindVoltageSetErr);
			p.Add("@S9AyameDaial", value.S9AyameDaial);
			p.Add("@S9AyameDaialErr", value.S9AyameDaialErr);
			p.Add("@S9TensionSpringLrage", value.S9TensionSpringLrage);
			p.Add("@S9TensionSpringSmall", value.S9TensionSpringSmall);
			p.Add("@S9TensionSpring", value.S9TensionSpring);
			p.Add("@S9TensionWind", value.S9TensionWind);
			p.Add("@S9PackingStandard", value.S9PackingStandard);
			p.Add("@S9PackingStandardErr", value.S9PackingStandardErr);
			p.Add("@S9TubeColor", value.S9TubeColor);
			p.Add("@S9Tail", value.S9Tail);
			p.Add("@S9CounterSetting", value.S9CounterSetting);
			p.Add("@S9CounterSettingErr", value.S9CounterSettingErr);
			p.Add("@S9CheeseWeight", value.S9CheeseWeight);
			p.Add("@S9CheeseWeightErr", value.S9CheeseWeightErr);
			p.Add("@S9HowToStartProductionEng", value.S9HowToStartProductionEng);
			p.Add("@S9HowToStartProductionTh", value.S9HowToStartProductionTh);
			p.Add("@S9Rewind", value.S9Rewind);
			p.Add("@S9Package", value.S9Package);
			p.Add("@S9Sampling", value.S9Sampling);


			p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
			p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

			try
			{
				cnn.Execute("SaveDIPProductionCondition", p, commandType: CommandType.StoredProcedure);
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

		public static NDbResult Delete(DIPProductionCondition value)
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
				cnn.Execute("DELETE FROM DIPProductionCondition WHERE ProductCode = @ProductCode", p, commandType: CommandType.Text);
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
