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
    public class PCTwist1Operation : NInpc
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        #endregion

        public SolidColorBrush TextColor { get { return BlackColor; } set { } }

        #region Public Properties

        public int? PCTwist1OpId { get; set; }
        public int? PCTwist1Id { get; set; }
        public DateTime ProductionDate { get; set; }
        
        public string MCCode { get; set; }

        public bool TestFlag { get; set; }

        public int DoffNo
        {
            get { return Get<int>(); }
            set { Set(value); }
        }

        public decimal? UnitWeight
        {
            get { return Get<decimal?>(); }
            set 
            { 
                Set(value, () => 
                { 
                    Raise(() => this.CalcProductWeight); 
                }); 
            }
        }
        public decimal? OutputCH
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.CalcProductWeight);
                });
            }
        }

        public decimal? CalcProductWeight 
        {
            get 
            {
                return (UnitWeight.HasValue && OutputCH.HasValue) ?
                    UnitWeight.Value * OutputCH.Value : new decimal?();
            }
            set { }
        }

        public decimal? ProductWeight { get; set; }
        public decimal? WasteWeight { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public string Remark { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<PCTwist1Operation>> Gets(int PCTwist1Id)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<PCTwist1Operation>> rets = new NDbResult<List<PCTwist1Operation>>();

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
            p.Add("@PCTwist1Id", PCTwist1Id);
            try
            {
                var items = cnn.Query<PCTwist1Operation>("GetTwist1Opts", p,
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
                rets.data = new List<PCTwist1Operation>();
            }

            return rets;
        }
        /// <summary>
        /// Start
        /// </summary>
        /// <param name="value">The PCTwist1Operation item to save.</param>
        /// <returns></returns>
        public static NDbResult<PCTwist1Operation> StartOperation(PCTwist1Operation value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<PCTwist1Operation> ret = new NDbResult<PCTwist1Operation>();

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
            p.Add("@ProductionDate", value.ProductionDate);
            p.Add("@TestFlag", value.TestFlag);
            p.Add("@DoffNo", value.DoffNo);

            p.Add("@StartTime", value.StartTime);

            p.Add("@PCTwist1OpId", value.PCTwist1OpId, DbType.Int32, direction: ParameterDirection.InputOutput);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("StartTwist1Ops", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);
                // Set PK
                value.PCTwist1OpId = p.Get<int?>("@PCTwist1OpId");
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
        /// End
        /// </summary>
        /// <param name="value">The PCTwist1Operation item to save.</param>
        /// <returns></returns>
        public static NDbResult<PCTwist1Operation> EndOperation(PCTwist1Operation value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<PCTwist1Operation> ret = new NDbResult<PCTwist1Operation>();

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

            value.ProductWeight = value.CalcProductWeight;

            var p = new DynamicParameters();
            p.Add("@PCTwist1Id", value.PCTwist1Id);
            p.Add("@ProductionDate", value.ProductionDate);
            p.Add("@TestFlag", value.TestFlag);
            p.Add("@DoffNo", value.DoffNo);

            p.Add("@UnitWeight", value.UnitWeight);
            p.Add("@OutputCH", value.OutputCH);
            p.Add("@ProductWeight", value.ProductWeight);
            p.Add("@WasteWeight", value.WasteWeight);

            p.Add("@EndTime", value.EndTime);

            p.Add("@Remark", value.Remark);
            p.Add("@PCTwist1OpId", value.PCTwist1OpId, DbType.Int32, direction: ParameterDirection.InputOutput);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("EndTwist1Ops", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);
                // Set PK
                value.PCTwist1OpId = p.Get<int?>("@PCTwist1OpId");
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
