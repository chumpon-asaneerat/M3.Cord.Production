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
    public class SaveSolutionLotDetail : NInpc
    {
        #region Public Proeprties

        public string solutionlot { get; set; }
        public int? solutionid { get; set; }
        public int? recipeorder { get; set; }
        public string recipe { get; set; }
        public int? mixorder { get; set; }
        public string chemicalno { get; set; }
        public decimal? weightcal { get; set; }
        public decimal? weightactual { get; set; }
        public string weightmc { get; set; }
        public DateTime? weightdate { get; set; }
        public int? weightby { get; set; }

        #endregion

        #region Static Methods

        public static NDbResult<SaveSolutionLotDetail> Save(SaveSolutionLotDetail value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<SaveSolutionLotDetail> ret = new NDbResult<SaveSolutionLotDetail>();

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

            p.Add("@solutionlot", value.solutionlot);
            p.Add("@solutionid", value.solutionid);
            p.Add("@recipeorder", value.recipeorder);
            p.Add("@recipe", value.recipe);
            p.Add("@mixorder", value.mixorder);
            p.Add("@chemicalno", value.chemicalno);
            p.Add("@weightcal", value.weightcal);
            p.Add("@weightactual", value.weightactual);
            p.Add("@weightmc", value.weightmc);
            p.Add("@weightdate", value.weightdate);
            p.Add("@weightby", value.weightby);


            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("ChSaveSolutionLotDetail", p, commandType: CommandType.StoredProcedure);
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

        #endregion
    }
}
