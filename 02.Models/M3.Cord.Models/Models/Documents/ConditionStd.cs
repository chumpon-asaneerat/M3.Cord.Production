#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Reflection;

using System.Windows.Media;

using NLib;

using Dapper;
using Newtonsoft.Json;
using System.Net;
using NLib.Models;
using System.Runtime.CompilerServices;

#endregion

namespace M3.Cord.Models
{
    #region ConditionParamTypes

    /// <summary>
    /// ConditionParamTypes enum
    /// </summary>
    public enum ConditionParamTypes : int
    {
        String = 1, 
        Number = 2, 
        NumberRange = 3,
        Bool = 4
    }

    #endregion

    #region ConditionStd

    /// <summary>
    /// The ConditionStd class.
    /// </summary>
    public class ConditionStd : NInpc
    {
        #region Override Methods

        public override int GetHashCode()
        {
            string code = string.Format("{0}_{1}_{2}", ProcessName, ProductCode, ParamName);
            return code.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            if ((obj is ConditionStd)) 
                return obj.GetHashCode().Equals(this.GetHashCode());
            else return false;
        }

        #endregion

        #region Public Properties

        [ExcelColumn("ProcessName", 1)]
        public string ProcessName { get; set; }
        [ExcelColumn("ProductCode", 2)]
        public string ProductCode { get; set; }
        [ExcelColumn("ParamName", 3)]
        public string ParamName { get; set; }
        [ExcelColumn("Type", 4)]
        public ConditionParamTypes ParamType { get; set; }
        [ExcelColumn("SC", 5)]
        public bool? SC { get; set; }
        [ExcelColumn("ValueS", 6)]
        public string StdValueS { get; set; }
        [ExcelColumn("ValueD", 7)]
        public decimal? StdValueD { get; set; }
        [ExcelColumn("ErrorRange", 8)]
        public decimal? StdValueE { get; set; }
        [ExcelColumn("ValueB", 9)]
        public bool? StdValueB { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create String Parameter.
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="pName"></param>
        /// <param name="pType"></param>
        /// <param name="sc"></param>
        /// <param name="sVal"></param>
        /// <param name="dVal"></param>
        /// <param name="eVal"></param>
        /// <returns></returns>
        public static ConditionStd Create(string productCode,
            string pName, ConditionParamTypes pType, bool sc,
            string sVal = null,
            decimal? dVal = new decimal?(),
            decimal? eVal = new decimal?())
        {
            return new ConditionStd()
            {
                ProcessName = "S-1",
                ProductCode = productCode,
                ParamName = pName,
                ParamType = pType,
                SC = sc,
                StdValueS = sVal,
                StdValueD = dVal,
                StdValueE = eVal
            };
        }
        /// <summary>
        /// Create Number Parameter.
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="productCode"></param>
        /// <param name="pName"></param>
        /// <param name="sVal"></param>
        /// <param name="sc"></param>
        /// <returns></returns>
        public static ConditionStd Create(string processName, string productCode, string pName,
            string sVal, bool sc = false)
        {
            return new ConditionStd()
            {
                ProcessName = processName,
                ProductCode = productCode,
                ParamName = pName,
                ParamType = ConditionParamTypes.String,
                SC = sc,
                StdValueS = sVal,
                StdValueD = new decimal?(),
                StdValueE = new decimal?(),
                StdValueB = new bool?(),
            };
        }
        /// <summary>
        /// Create Number with error range Parameter.
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="productCode"></param>
        /// <param name="pName"></param>
        /// <param name="dVal"></param>
        /// <param name="sc"></param>
        /// <returns></returns>
        public static ConditionStd Create(string processName, string productCode, string pName,
            decimal? dVal, bool sc = false)
        {
            return new ConditionStd()
            {
                ProcessName = processName,
                ProductCode = productCode,
                ParamName = pName,
                ParamType = ConditionParamTypes.Number,
                SC = sc,
                StdValueS = null,
                StdValueD = dVal,
                StdValueE = new decimal?(),
                StdValueB = new bool?(),
            };
        }
        public static ConditionStd Create(string processName, string productCode, string pName,
            decimal? dVal, decimal? eVal, bool sc = false)
        {
            return new ConditionStd()
            {
                ProcessName = processName,
                ProductCode = productCode,
                ParamName = pName,
                ParamType = ConditionParamTypes.NumberRange,
                SC = sc,
                StdValueS = null,
                StdValueD = dVal,
                StdValueE = eVal,
                StdValueB = new bool?(),
            };
        }
        /// <summary>
        /// Create Bool Parameter.
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="productCode"></param>
        /// <param name="pName"></param>
        /// <param name="bVal"></param>
        /// <param name="sc"></param>
        /// <returns></returns>
        public static ConditionStd Create(string processName, string productCode, string pName,
            bool? bVal, bool sc = false)
        {
            return new ConditionStd()
            {
                ProcessName = processName,
                ProductCode = productCode,
                ParamName = pName,
                ParamType = ConditionParamTypes.NumberRange,
                SC = sc,
                StdValueS = null,
                StdValueD = new decimal?(),
                StdValueE = new decimal?(),
                StdValueB = bVal,
            };
        }


        private static List<ConditionStd> Creates(string productCode)
        {
            var results = new List<ConditionStd>();

            // Ring Diameter
            results.Add(Create(productCode, "RingDiameter", ConditionParamTypes.String, false, "8 1/2\" R x 14\" L"));
            // TwistChangeGear (I x J x K)
            results.Add(Create(productCode, "TwistChangeGearI", ConditionParamTypes.Number, true, null, 41));
            results.Add(Create(productCode, "TwistChangeGearJ", ConditionParamTypes.Number, true, null, 45));
            results.Add(Create(productCode, "TwistChangeGearK", ConditionParamTypes.Number, true, null, 83));
            // LifterChangeGear (A x B)
            results.Add(Create(productCode, "LifterChangeGearA", ConditionParamTypes.Number, false, null, 48));
            results.Add(Create(productCode, "LifterChangeGearB", ConditionParamTypes.Number, false, null, 56));
            // Outer Diameter
            results.Add(Create(productCode, "OuterDiameter", ConditionParamTypes.Number, false, null, 217));
            // TravellerNo
            results.Add(Create(productCode, "TravellerNo", ConditionParamTypes.String, false, "RK-500 J x 1 Pcs."));

            return results;
        }



        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The PCCard item to save.</param>
        /// <returns></returns>
        public static NDbResult<ConditionStd> Save(ConditionStd value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<ConditionStd> ret = new NDbResult<ConditionStd>();

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
            p.Add("@ProcessName", value.ProcessName);
            p.Add("@ProductCode", value.ProductCode);
            p.Add("@ParamName", value.ParamName);
            p.Add("@ParamType", value.ParamType);
            p.Add("@SC", value.SC);
            p.Add("@StdValueS", value.StdValueS);
            p.Add("@StdValueD", value.StdValueD);
            p.Add("@StdValueE", value.StdValueE);
            p.Add("@StdValueB", value.StdValueB);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveConditionStd", p, commandType: CommandType.StoredProcedure);
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
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<ConditionStd>> Gets(string ProcessName, string ProductCode)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<ConditionStd>> rets = new NDbResult<List<ConditionStd>>();

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
            p.Add("@ProcessName", ProcessName);
            p.Add("@ProductCode", ProductCode);
            try
            {
                var items = cnn.Query<ConditionStd>("GetConditionStds", p,
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
                rets.data = new List<ConditionStd>();
            }

            return rets;
        }

        #endregion
    }

    #endregion

    public class ProcessConditionStd : NInpc
    {
        private Dictionary<string, ConditionStd> _productStds = new Dictionary<string, ConditionStd>();

        public ProcessConditionStd() 
        {
        }

        public string ProcessName { get; set; }

        public static ProcessConditionStd Get()
        {
            var products = Product.Gets().Value();
            if (null != products && products.Count > 0)
            {
                foreach (var item in products)
                {
                    //results.Add(new ProductConditionStd() { ProductCode = item.ProductCode });
                }
            }
            return null;
        }
    }

    public class ProductConditionStd
    {
        public ProductConditionStd(ProcessConditionStd process)
        {
            this.Process = process;
        }

        public ProcessConditionStd Process { get; private set; }
        public string ProductCode { get; set; }
    }
}
