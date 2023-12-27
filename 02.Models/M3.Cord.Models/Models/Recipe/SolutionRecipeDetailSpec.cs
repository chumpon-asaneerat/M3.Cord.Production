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
    public class SolutionRecipeDetailSpec : NInpc
    {
        #region Public Properties

        public SolidColorBrush TextColor
        {
            get { return ModelConsts.BlackColor; }
            set { }
        }

        public int? SolutionID { get; set; }
        public string Recipe { get; set; }
        public int? MixOrder { get; set; }
        public string ChemicalType { get; set; }
        public string ChemicalNo { get; set; }
        public decimal? ChemWet { get; set; }
        public decimal? ChemDry { get; set; }

        public string SolutionLot { get; set; }
        public string ChemicalName { get; set; }
        public decimal? CalculationWet { get; set; }

        public int? RecipeOrder { get; set; }
        public string RecipeView { get; set; }

        #endregion

        #region Static Methods

        public static NDbResult<List<SolutionRecipeDetailSpec>> Gets(int? solutionid = null
            , string recipe = null
            , string recipe2 = null
            , string recipe3 = null
            , string recipe4 = null
            , decimal? qty = null)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<SolutionRecipeDetailSpec>> rets = new NDbResult<List<SolutionRecipeDetailSpec>>();

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

            List<SolutionRecipeDetailSpec> results = new List<SolutionRecipeDetailSpec>();
            SolutionRecipeDetailSpec result = new SolutionRecipeDetailSpec();

            #region recipe

            if (!string.IsNullOrEmpty(recipe))
            {
                var p = new DynamicParameters();
                p.Add("@solutionid", solutionid);
                p.Add("@recipe", recipe);

                try
                {
                    var items = cnn.Query<SolutionRecipeDetailSpec>("ChGetSolutionRecipeDetailSpec", p,
                        commandType: CommandType.StoredProcedure);
                    var data = (null != items) ? items.ToList() : null;
                    //rets.Success(data);

                    if (null != data && data.Count > 0)
                    {
                        int? i1 = 0;
                        int? count = data.Count;
                        string rec = string.Empty;
                        decimal? wet = 0;
                        decimal? dry = 0;
                        decimal? calTotal = 0;

                        foreach (var item in data)
                        {
                            result = new SolutionRecipeDetailSpec();

                            //result.SolutionLot = item.SolutionLot;
                            result.SolutionID = item.SolutionID;
                            result.Recipe = item.Recipe;

                            if (!string.IsNullOrEmpty(result.Recipe))
                            {
                                if (result.Recipe != rec)
                                {
                                    result.RecipeView = result.Recipe;
                                    rec = result.Recipe;
                                }
                            }

                            result.MixOrder = item.MixOrder;
                            result.ChemicalType = item.ChemicalType;
                            result.ChemicalNo = item.ChemicalNo;
                            result.ChemWet = item.ChemWet;

                            if (result.ChemWet != null && result.ChemWet != 0)
                            {
                                wet += result.ChemWet;
                            }

                            result.ChemDry = item.ChemDry;

                            if (result.ChemDry != null && result.ChemDry != 0)
                            {
                                dry += result.ChemDry;
                            }

                            result.ChemicalName = item.ChemicalName;
                            result.RecipeOrder = 1;

                            if ((item.ChemWet != null && item.ChemWet != 0)
                                && (item.ChemDry != null && item.ChemDry != 0)
                                && (qty != null && qty != 0))
                            {
                                result.CalculationWet = ((item.ChemWet * qty) / item.ChemDry);

                                calTotal += result.CalculationWet;
                            }

                            results.Add(result);

                            i1++;
                            if (i1 == count)
                            {
                                result = new SolutionRecipeDetailSpec();

                                result.ChemicalName = "Total";
                                result.ChemWet = wet;
                                result.ChemDry = dry;
                                result.CalculationWet = calTotal;

                                results.Add(result);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    med.Err(ex);
                    // Set error number/message
                    rets.ErrNum = 9999;
                    rets.ErrMsg = ex.Message;
                }

            }
            #endregion

            #region recipe2

            if (!string.IsNullOrEmpty(recipe2))
            {
                var p2 = new DynamicParameters();
                p2.Add("@solutionid", solutionid);
                p2.Add("@recipe", recipe2);

                try
                {
                    var items = cnn.Query<SolutionRecipeDetailSpec>("ChGetSolutionRecipeDetailSpec", p2,
                        commandType: CommandType.StoredProcedure);
                    var data = (null != items) ? items.ToList() : null;
                    //rets.Success(data);

                    if (null != data && data.Count > 0)
                    {
                        int? i1 = 0;
                        int? count = data.Count;
                        string rec = string.Empty;
                        decimal? wet = 0;
                        decimal? dry = 0;
                        decimal? calTotal = 0;

                        foreach (var item in data)
                        {
                            result = new SolutionRecipeDetailSpec();

                            //result.SolutionLot = item.SolutionLot;
                            result.SolutionID = item.SolutionID;
                            result.Recipe = item.Recipe;

                            if (!string.IsNullOrEmpty(result.Recipe))
                            {
                                if (result.Recipe != rec)
                                {
                                    result.RecipeView = result.Recipe;
                                    rec = result.Recipe;
                                }
                            }

                            result.MixOrder = item.MixOrder;
                            result.ChemicalType = item.ChemicalType;
                            result.ChemicalNo = item.ChemicalNo;
                            result.ChemWet = item.ChemWet;

                            if (result.ChemWet != null && result.ChemWet != 0)
                            {
                                wet += result.ChemWet;
                            }

                            result.ChemDry = item.ChemDry;

                            if (result.ChemDry != null && result.ChemDry != 0)
                            {
                                dry += result.ChemDry;
                            }

                            result.ChemicalName = item.ChemicalName;
                            result.RecipeOrder = 2;

                            if ((item.ChemWet != null && item.ChemWet != 0)
                                && (item.ChemDry != null && item.ChemDry != 0)
                                && (qty != null && qty != 0))
                            {
                                result.CalculationWet = ((item.ChemWet * qty) / item.ChemDry);

                                calTotal += result.CalculationWet;
                            }

                            results.Add(result);

                            i1++;
                            if (i1 == count)
                            {
                                result = new SolutionRecipeDetailSpec();

                                result.ChemicalName = "Total";
                                result.ChemWet = wet;
                                result.ChemDry = dry;
                                result.CalculationWet = calTotal;

                                results.Add(result);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    med.Err(ex);
                    // Set error number/message
                    rets.ErrNum = 9999;
                    rets.ErrMsg = ex.Message;
                }
            }
            #endregion

            #region recipe3

            if (!string.IsNullOrEmpty(recipe3))
            {
                var p3 = new DynamicParameters();
                p3.Add("@solutionid", solutionid);
                p3.Add("@recipe", recipe3);

                try
                {
                    var items = cnn.Query<SolutionRecipeDetailSpec>("ChGetSolutionRecipeDetailSpec", p3,
                        commandType: CommandType.StoredProcedure);
                    var data = (null != items) ? items.ToList() : null;
                    //rets.Success(data);

                    if (null != data && data.Count > 0)
                    {
                        int? i1 = 0;
                        int? count = data.Count;
                        string rec = string.Empty;
                        decimal? wet = 0;
                        decimal? dry = 0;
                        decimal? calTotal = 0;

                        foreach (var item in data)
                        {
                            result = new SolutionRecipeDetailSpec();

                            //result.SolutionLot = item.SolutionLot;
                            result.SolutionID = item.SolutionID;
                            result.Recipe = item.Recipe;

                            if (!string.IsNullOrEmpty(result.Recipe))
                            {
                                if (result.Recipe != rec)
                                {
                                    result.RecipeView = result.Recipe;
                                    rec = result.Recipe;
                                }
                            }

                            result.MixOrder = item.MixOrder;
                            result.ChemicalType = item.ChemicalType;
                            result.ChemicalNo = item.ChemicalNo;
                            result.ChemWet = item.ChemWet;

                            if (result.ChemWet != null && result.ChemWet != 0)
                            {
                                wet += result.ChemWet;
                            }

                            result.ChemDry = item.ChemDry;

                            if (result.ChemDry != null && result.ChemDry != 0)
                            {
                                dry += result.ChemDry;
                            }

                            result.ChemicalName = item.ChemicalName;
                            result.RecipeOrder = 3;

                            if ((item.ChemWet != null && item.ChemWet != 0)
                                && (item.ChemDry != null && item.ChemDry != 0)
                                && (qty != null && qty != 0))
                            {
                                result.CalculationWet = ((item.ChemWet * qty) / item.ChemDry);

                                calTotal += result.CalculationWet;
                            }

                            results.Add(result);

                            i1++;
                            if (i1 == count)
                            {
                                result = new SolutionRecipeDetailSpec();

                                result.ChemicalName = "Total";
                                result.ChemWet = wet;
                                result.ChemDry = dry;
                                result.CalculationWet = calTotal;

                                results.Add(result);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    med.Err(ex);
                    // Set error number/message
                    rets.ErrNum = 9999;
                    rets.ErrMsg = ex.Message;
                }
            }
            #endregion

            #region recipe4

            if (!string.IsNullOrEmpty(recipe4))
            {
                var p4 = new DynamicParameters();
                p4.Add("@solutionid", solutionid);
                p4.Add("@recipe", recipe4);

                try
                {
                    var items = cnn.Query<SolutionRecipeDetailSpec>("ChGetSolutionRecipeDetailSpec", p4,
                        commandType: CommandType.StoredProcedure);
                    var data = (null != items) ? items.ToList() : null;
                    //rets.Success(data);

                    if (null != data && data.Count > 0)
                    {
                        int? i1 = 0;
                        int? count = data.Count;
                        string rec = string.Empty;
                        decimal? wet = 0;
                        decimal? dry = 0;
                        decimal? calTotal = 0;

                        foreach (var item in data)
                        {
                            result = new SolutionRecipeDetailSpec();

                            //result.SolutionLot = item.SolutionLot;
                            result.SolutionID = item.SolutionID;
                            result.Recipe = item.Recipe;

                            if (!string.IsNullOrEmpty(result.Recipe))
                            {
                                if (result.Recipe != rec)
                                {
                                    result.RecipeView = result.Recipe;
                                    rec = result.Recipe;
                                }
                            }

                            result.MixOrder = item.MixOrder;
                            result.ChemicalType = item.ChemicalType;
                            result.ChemicalNo = item.ChemicalNo;
                            result.ChemWet = item.ChemWet;

                            if (result.ChemWet != null && result.ChemWet != 0)
                            {
                                wet += result.ChemWet;
                            }

                            result.ChemDry = item.ChemDry;

                            if (result.ChemDry != null && result.ChemDry != 0)
                            {
                                dry += result.ChemDry;
                            }

                            result.ChemicalName = item.ChemicalName;
                            result.RecipeOrder = 4;

                            if ((item.ChemWet != null && item.ChemWet != 0)
                                && (item.ChemDry != null && item.ChemDry != 0)
                                && (qty != null && qty != 0))
                            {
                                result.CalculationWet = ((item.ChemWet * qty) / item.ChemDry);

                                calTotal += result.CalculationWet;
                            }

                            results.Add(result);

                            i1++;
                            if (i1 == count)
                            {
                                result = new SolutionRecipeDetailSpec();

                                result.ChemicalName = "Total";
                                result.ChemWet = wet;
                                result.ChemDry = dry;
                                result.CalculationWet = calTotal;

                                results.Add(result);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    med.Err(ex);
                    // Set error number/message
                    rets.ErrNum = 9999;
                    rets.ErrMsg = ex.Message;
                }
            }
            #endregion

            if (results != null && results.Count > 0)
                rets.Success(results);

            if (null == rets.data)
            {
                // create empty list.
                rets.data = new List<SolutionRecipeDetailSpec>();
            }

            return rets;
        }

        #endregion
    }
}
