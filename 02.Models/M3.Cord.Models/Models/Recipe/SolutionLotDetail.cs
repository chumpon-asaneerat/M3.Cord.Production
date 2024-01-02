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
using System.Windows;

#endregion

namespace M3.Cord.Models
{
    public class SolutionLotDetail : NInpc
    {
        #region Public Properties
        public SolidColorBrush TextColor
        {
            get { return ModelConsts.BlackColor; }
            set { }
        }

        public string SolutionLot { get; set; }
        public int? SolutionID { get; set; }
        public int? RecipeOrder { get; set; }
        public string Recipe { get; set; }
        public int? MixOrder { get; set; }
        public string ChemicalNo { get; set; }
        public decimal? WeightCal { get; set; }
        public decimal? WeightActual { get; set; }
        public string WeightMc { get; set; }
        public DateTime? WeightDate { get; set; }
        public int? WeightBy { get; set; }
        public string ChemicalName { get; set; }
        public string ChemicalType { get; set; }

        // runtime properties
        public Visibility ShowWeightButton
        {
            get 
            {
                bool isTypeC = (!string.IsNullOrWhiteSpace(ChemicalType) && ChemicalType == "C");
                return (isTypeC) ? Visibility.Visible : Visibility.Hidden; 
            }
            set { }
        }

        public string RecipeView { get; set; }

        #endregion

        #region Static Methods

        public static NDbResult<List<SolutionLotDetail>> Gets(string solutionlot = null,int? solutionid = null,string recipe = null)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<SolutionLotDetail>> rets = new NDbResult<List<SolutionLotDetail>>();

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
            p.Add("@solutionlot", solutionlot);
            p.Add("@solutionid", solutionid);
            p.Add("@recipe", recipe);

            try
            {
                var items = cnn.Query<SolutionLotDetail>("ChGetSolutionLotDetail", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList() : null;
                //rets.Success(data);

                List<SolutionLotDetail> results = new List<SolutionLotDetail>();
                SolutionLotDetail result = new SolutionLotDetail();

                if (null != data && data.Count > 0)
                {
                    int? i1 = 0;
                    int? count = data.Count;
                    string rec = string.Empty;
                    decimal? weightCal = 0;
                    decimal? weightActual = 0;

                    foreach (var item in data)
                    {
                        if (i1 > 0)
                        {
                            if (!string.IsNullOrEmpty(item.Recipe))
                            {
                                if (item.Recipe != rec)
                                {
                                    result = new SolutionLotDetail();

                                    result.ChemicalName = "Total";
                                    result.WeightCal = weightCal;
                                    result.WeightActual = weightActual;

                                    results.Add(result);

                                    weightCal = 0;
                                    weightActual = 0;
                                }
                            }
                        }

                        result = new SolutionLotDetail();

                        result.SolutionLot = item.SolutionLot;
                        result.SolutionID = item.SolutionID;
                        result.RecipeOrder = item.RecipeOrder;
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
                        result.ChemicalName = item.ChemicalName;

                        result.WeightCal = item.WeightCal;
                        if (result.WeightCal != null && result.WeightCal != 0)
                        {
                            weightCal += result.WeightCal;
                        }

                        result.WeightActual = item.WeightActual;
                        if (result.WeightActual != null && result.WeightActual != 0)
                        {
                            weightActual += result.WeightActual;
                        }

                        result.WeightMc = item.WeightMc;
                        result.WeightDate = item.WeightDate;
                        result.WeightBy = item.WeightBy;

                        results.Add(result);

                        i1++;
                        if (i1 == count)
                        {
                            result = new SolutionLotDetail();

                            result.ChemicalName = "Total";
                            result.WeightCal = weightCal;
                            result.WeightActual = weightActual;

                            results.Add(result);

                            weightCal = 0;
                            weightActual = 0;
                        }
                    }
                }

                if (results != null && results.Count > 0)
                    rets.Success(results);
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
                rets.data = new List<SolutionLotDetail>();
            }

            return rets;
        }

        public static NDbResult<List<SolutionLotDetail>> GetsChemical(string solutionlot = null, int? solutionid = null, string recipe = null)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<SolutionLotDetail>> rets = new NDbResult<List<SolutionLotDetail>>();

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
            p.Add("@solutionlot", solutionlot);
            p.Add("@solutionid", solutionid);
            p.Add("@recipe", recipe);

            try
            {
                var items = cnn.Query<SolutionLotDetail>("ChGetSolutionLotDetail", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList() : null;
                //rets.Success(data);

                List<SolutionLotDetail> results = new List<SolutionLotDetail>();
                SolutionLotDetail result = new SolutionLotDetail();

                if (null != data && data.Count > 0)
                {
                    foreach (var item in data)
                    {
                        result = new SolutionLotDetail();

                        result.SolutionLot = item.SolutionLot;
                        result.SolutionID = item.SolutionID;
                        result.RecipeOrder = item.RecipeOrder;
                        result.Recipe = item.Recipe;
                        result.MixOrder = item.MixOrder;
                        result.ChemicalType = item.ChemicalType;
                        result.ChemicalNo = item.ChemicalNo;
                        result.ChemicalName = item.ChemicalName;
                        result.WeightCal = item.WeightCal;
                        result.WeightActual = item.WeightActual;
                        result.WeightMc = item.WeightMc;
                        result.WeightDate = item.WeightDate;
                        result.WeightBy = item.WeightBy;

                        if (result.ChemicalType != null && result.ChemicalType.Contains("C"))
                            results.Add(result);
                    }
                }

                if (results != null && results.Count > 0)
                    rets.Success(results);
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
                rets.data = new List<SolutionLotDetail>();
            }

            return rets;
        }


        #endregion
    }
}
