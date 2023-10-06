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
    public class CordSamplingDetails
    {
        #region Public Proeprties
        public bool? TwistYarn { get; set; }
        public bool? CordDipping { get; set; }
        public bool? Rawmaterial { get; set; }
        public DateTime? SamplingDate { get; set; }
        public string CustomerName { get; set; }
        public string ProductCode { get; set; }
        public string CordStructure { get; set; }
        public string LotNo { get; set; }
        public bool? ProductionSituationTwisting { get; set; }
        public bool? ProductionSituationHeatSet { get; set; }
        public bool? ProductionSituationDipCord { get; set; }
        public bool? ProductionSituationJointing { get; set; }
        public string StandardOfTwisting { get; set; }
        public bool? S1 { get; set; }
        public bool? S2 { get; set; }
        public bool? S4 { get; set; }
        public bool? S8 { get; set; }
        public bool? S19 { get; set; }
        public bool? Bobbin { get; set; }
        public bool? Cheese { get; set; }
        public bool? TaperBB { get; set; }
        public bool? ContainerOthers { get; set; }
        public string SpindleNo { get; set; }
        public string Quantity { get; set; }
        public bool? Polyester { get; set; }
        public bool? Nylon66 { get; set; }
        public bool? MaterialOthers { get; set; }
        public string YarnType { get; set; }
        public bool? Process { get; set; }
        public bool? Product { get; set; }
        public bool? Special { get; set; }
        public bool? Trial { get; set; }
        public string Shift { get; set; }
        public string ShiftName { get; set; }
        public string Remark { get; set; }
        public bool? Direct { get; set; }
        public bool? Relax { get; set; }
        public string Relax_k { get; set; }
        public string Relax_n { get; set; }
        public bool? Necessary { get; set; }
        public bool? NotNecessary { get; set; }
        public bool? SpecialMentionJointing { get; set; }
        public bool? SpecialMentionNoJointing { get; set; }
        public decimal? TensileR1 { get; set; }
        public decimal? TensileL1 { get; set; }
        public decimal? TensileR2 { get; set; }
        public decimal? TensileL2 { get; set; }
        public decimal? TensileR3 { get; set; }
        public decimal? TensileL3 { get; set; }
        public decimal? TensileR4 { get; set; }
        public decimal? TensileL4 { get; set; }
        public decimal? TensileR5 { get; set; }
        public decimal? TensileL5 { get; set; }
        public decimal? TensileRSum { get; set; }
        public decimal? TensileLSum { get; set; }
        public decimal? FirstTwistingR1 { get; set; }
        public decimal? FirstTwistingL1 { get; set; }
        public decimal? FirstTwistingR2 { get; set; }
        public decimal? FirstTwistingL2 { get; set; }
        public decimal? FirstTwistingR3 { get; set; }
        public decimal? FirstTwistingL3 { get; set; }
        public decimal? FirstTwistingR4 { get; set; }
        public decimal? FirstTwistingL4 { get; set; }
        public decimal? FirstTwistingR5 { get; set; }
        public decimal? FirstTwistingL5 { get; set; }
        public decimal? FirstTwistingRSum { get; set; }
        public decimal? FirstTwistingLSum { get; set; }
        public decimal? SecondTwistingR1 { get; set; }
        public decimal? SecondTwistingL1 { get; set; }
        public decimal? SecondTwistingR2 { get; set; }
        public decimal? SecondTwistingL2 { get; set; }
        public decimal? SecondTwistingR3 { get; set; }
        public decimal? SecondTwistingL3 { get; set; }
        public decimal? SecondTwistingR4 { get; set; }
        public decimal? SecondTwistingL4 { get; set; }
        public decimal? SecondTwistingR5 { get; set; }
        public decimal? SecondTwistingL5 { get; set; }
        public decimal? SecondTwistingRSum { get; set; }
        public decimal? SecondTwistingLSum { get; set; }
        public string Remark1 { get; set; }
        public string Remark2 { get; set; }
        public string Remark3 { get; set; }
        public string Remark4 { get; set; }
        public string Remark5 { get; set; }
        public string RemarkSum { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<CordSamplingDetails>> Gets()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordSamplingDetails>> rets = new NDbResult<List<CordSamplingDetails>>();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                rets.ErrNum = 8000;
                rets.ErrMsg = msg;

                return rets;
            }

            var p = new DynamicParameters();

            try
            {
                var items = cnn.Query<CordSamplingDetails>("GetCordSamplingDetails", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList() : null;
                rets.Success(data);
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                rets.ErrNum = 9999;
                rets.ErrMsg = ex.Message;
            }

            if (null == rets.data)
            {
                // create empty list.
                rets.data = new List<CordSamplingDetails>();
            }

            return rets;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The CordSamplingDetails item to save.</param>
        /// <returns></returns>
        public static NDbResult<CordSamplingDetails> Save(CordSamplingDetails value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordSamplingDetails> ret = new NDbResult<CordSamplingDetails>();

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
            p.Add("@TwistYarn", value.TwistYarn);
            p.Add("@CordDipping", value.CordDipping);
            p.Add("@Rawmaterial", value.Rawmaterial);
            p.Add("@SamplingDate", value.SamplingDate);
            p.Add("@CustomerName", value.CustomerName);
            p.Add("@ProductCode", value.ProductCode);
            p.Add("@CordStructure", value.CordStructure);
            p.Add("@LotNo", value.LotNo);
            p.Add("@ProductionSituationTwisting", value.ProductionSituationTwisting);
            p.Add("@ProductionSituationHeatSet", value.ProductionSituationHeatSet);
            p.Add("@ProductionSituationDipCord", value.ProductionSituationDipCord);
            p.Add("@ProductionSituationJointing", value.ProductionSituationJointing);
            p.Add("@StandardOfTwisting", value.StandardOfTwisting);
            p.Add("@S1", value.S1);
            p.Add("@S2", value.S2);
            p.Add("@S4", value.S4);
            p.Add("@S8", value.S8);
            p.Add("@S19", value.S19);
            p.Add("@Bobbin", value.Bobbin);
            p.Add("@Cheese", value.Cheese);
            p.Add("@TaperBB", value.TaperBB);
            p.Add("@ContainerOthers", value.ContainerOthers);
            p.Add("@SpindleNo", value.SpindleNo);
            p.Add("@Quantity", value.Quantity);
            p.Add("@Polyester", value.Polyester);
            p.Add("@Nylon66", value.Nylon66);
            p.Add("@MaterialOthers", value.MaterialOthers);
            p.Add("@YarnType", value.YarnType);
            p.Add("@Process", value.Process);
            p.Add("@Product", value.Product);
            p.Add("@Special", value.Special);
            p.Add("@Trial", value.Trial);
            p.Add("@Shift", value.Shift);
            p.Add("@ShiftName", value.ShiftName);
            p.Add("@Remark", value.Remark);
            p.Add("@Direct", value.Direct);
            p.Add("@Relax", value.Relax);
            p.Add("@Relax_k", value.Relax_k);
            p.Add("@Relax_n", value.Relax_n);
            p.Add("@Necessary", value.Necessary);
            p.Add("@NotNecessary", value.NotNecessary);
            p.Add("@SpecialMentionJointing", value.SpecialMentionJointing);
            p.Add("@SpecialMentionNoJointing", value.SpecialMentionNoJointing);
            p.Add("@TensileR1", value.TensileR1);
            p.Add("@TensileL1", value.TensileL1);
            p.Add("@TensileR2", value.TensileR2);
            p.Add("@TensileL2", value.TensileL2);
            p.Add("@TensileR3", value.TensileR3);
            p.Add("@TensileL3", value.TensileL3);
            p.Add("@TensileR4", value.TensileR4);
            p.Add("@TensileL4", value.TensileL4);
            p.Add("@TensileR5", value.TensileR5);
            p.Add("@TensileL5", value.TensileL5);
            p.Add("@TensileRSum", value.TensileRSum);
            p.Add("@TensileLSum", value.TensileLSum);
            p.Add("@FirstTwistingR1", value.FirstTwistingR1);
            p.Add("@FirstTwistingL1", value.FirstTwistingL1);
            p.Add("@FirstTwistingR2", value.FirstTwistingR2);
            p.Add("@FirstTwistingL2", value.FirstTwistingL2);
            p.Add("@FirstTwistingR3", value.FirstTwistingR3);
            p.Add("@FirstTwistingL3", value.FirstTwistingL3);
            p.Add("@FirstTwistingR4", value.FirstTwistingR4);
            p.Add("@FirstTwistingL4", value.FirstTwistingL4);
            p.Add("@FirstTwistingR5", value.FirstTwistingR5);
            p.Add("@FirstTwistingL5", value.FirstTwistingL5);
            p.Add("@FirstTwistingRSum", value.FirstTwistingRSum);
            p.Add("@FirstTwistingLSum", value.FirstTwistingLSum);
            p.Add("@SecondTwistingR1", value.SecondTwistingR1);
            p.Add("@SecondTwistingL1", value.SecondTwistingL1);
            p.Add("@SecondTwistingR2", value.SecondTwistingR2);
            p.Add("@SecondTwistingL2", value.SecondTwistingL2);
            p.Add("@SecondTwistingR3", value.SecondTwistingR3);
            p.Add("@SecondTwistingL3", value.SecondTwistingL3);
            p.Add("@SecondTwistingR4", value.SecondTwistingR4);
            p.Add("@SecondTwistingL4", value.SecondTwistingL4);
            p.Add("@SecondTwistingR5", value.SecondTwistingR5);
            p.Add("@SecondTwistingL5", value.SecondTwistingL5);
            p.Add("@SecondTwistingRSum", value.SecondTwistingRSum);
            p.Add("@SecondTwistingLSum", value.SecondTwistingLSum);
            p.Add("@Remark1", value.Remark1);
            p.Add("@Remark2", value.Remark2);
            p.Add("@Remark3", value.Remark3);
            p.Add("@Remark4", value.Remark4);
            p.Add("@Remark5", value.Remark5);
            p.Add("@RemarkSum", value.RemarkSum);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveCordSamplingDetails", p, commandType: CommandType.StoredProcedure);
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

        public static NDbResult Delete(CordSamplingDetails value)
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
                cnn.Execute("DELETE FROM CordSamplingDetails WHERE ProductCode = @ProductCode", p, commandType: CommandType.Text);
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
