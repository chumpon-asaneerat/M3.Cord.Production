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

        #endregion

        #region Static Methods

        public static NDbResult<List<SolutionLotDetail>> Gets(string solutionlot = null)
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

            try
            {
                var items = cnn.Query<SolutionLotDetail>("ChGetSolutionLotDetail", p,
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
                rets.data = new List<SolutionLotDetail>();
            }

            return rets;
        }

        #endregion
    }
}
