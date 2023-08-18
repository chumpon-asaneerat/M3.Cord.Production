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
using System.Net.NetworkInformation;
using System.Xml.Linq;
using System.Windows.Controls;
using Newtonsoft.Json.Linq;

#endregion

namespace M3.Cord.Models
{
    public class PCTwist1
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        #endregion

        #region Public Proeprties

        public int? PCTwist1Id { get; set; }
        public int? PCId { get; set; }
        public string PINo { get; set; }
        public DateTime? PCDate { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ItemYarn { get; set; }
        public string CordStructure { get; set; }
        public string ProductLotNo { get; set; }
        public decimal? TargetQty { get; set; }

        public int LastTestNo { get; set; }
        public int LastDoffNo { get; set; }

        public bool? FinishFlag { get; set; }
        public bool? DeleteFlag { get; set; }

        public string MCCode { get; set; }
        public string PaperTubeColor { get; set; }
        public DateTime? IssueDate { get; set; }
        public string IssueBy { get; set; }
        public string CheckBy { get; set; }
        public string ApproveBy { get; set; }
        public string SectionHead { get; set; }
        public string SectionMgr { get; set; }
        public decimal? ActualQty { get; set; }
        public string Remark { get; set; }

        public SolidColorBrush TextColor { get { return BlackColor; } set { } }

        #endregion

        #region Static Methods

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public static NDbResult<PCTwist1> Get(string MCCode = null)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<PCTwist1> rets = new NDbResult<PCTwist1>();

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
            p.Add("@MCCode", string.IsNullOrWhiteSpace(MCCode) ? null : MCCode);
            try
            {
                var items = cnn.Query<PCTwist1>("GetPCTwist1s", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList().FirstOrDefault() : null;
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
                rets.data = null;
            }

            return rets;
        }

        /// <summary>
        /// AddNew
        /// </summary>
        /// <param name="value">The PCTwist1 item to save.</param>
        /// <returns></returns>
        public static NDbResult AddNew(FirstTwistMC mc, PCCard pccard, 
            string paperTubeColor = null, string remark = null)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult ret = new NDbResult();

            if (null == mc || null == pccard)
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
            p.Add("@PCId", pccard.PCId);
            p.Add("@MCCode", mc.MCCode);
            p.Add("@PaperTubeColor", paperTubeColor);
            p.Add("@Remark", remark);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("AddPC1Twist", p, commandType: CommandType.StoredProcedure);
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

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The PCCard item to save.</param>
        /// <returns></returns>
        public static NDbResult<PCTwist1> Save(PCTwist1 value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<PCTwist1> ret = new NDbResult<PCTwist1>();

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
            p.Add("@PCTwist1Id", value.PCTwist1Id);
            p.Add("@IssueDate", value.IssueDate);
            p.Add("@IssueBy", value.IssueBy);
            p.Add("@CheckBy", value.CheckBy);
            p.Add("@ApproveBy", value.ApproveBy);
            p.Add("@SectionHead", value.SectionHead);
            p.Add("@SectionMgr", value.SectionMgr);
            p.Add("@ActualQty", value.ActualQty);
            p.Add("@PaperTubeColor", value.PaperTubeColor);
            p.Add("@Remark", value.Remark);

            p.Add("@LastTestNo", value.LastTestNo);
            p.Add("@LastDoffNo", value.LastDoffNo);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SavePCTwist1", p, commandType: CommandType.StoredProcedure);
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

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="value">The PCCard item to delete.</param>
        /// <returns></returns>
        public static NDbResult Delete(PCTwist1 value)
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
            p.Add("@PCTwist1Id", value.PCId);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("DeletePCTwist1", p, commandType: CommandType.StoredProcedure);
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
