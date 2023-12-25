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
    public class SolutionRecipe : NInpc
    {
        #region Public Properties
    
        public int SolutionId { get; set; }
        public int ProductId { get; set; }

        //ใน stored update ใหม่เหมือนเปลี่ยนชื่อเป็น DIPProductCode
        public string ProductCode { get; set; }

        public string SolutionName { get; set; }

        public string Recipe1 { get; set; }
        public string Recipe2 { get; set; }
        public string Recipe3 { get; set; }
        public string Recipe4 { get; set; }

        public string Remark { get; set; }
        public decimal? QtySpec { get; set; }

        //New 25/12/23
        public string DIPProductCode { get; set; }

        public string Compound { get; set; }

        #endregion

        #region Static Methods

        public static NDbResult<List<SolutionRecipe>> Gets(string productcode = null)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<SolutionRecipe>> rets = new NDbResult<List<SolutionRecipe>>();

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
            p.Add("@productcode", productcode);

            try
            {
                var items = cnn.Query<SolutionRecipe>("ChGetSolutionByItem", p,
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
                rets.data = new List<SolutionRecipe>();
            }

            return rets;
        }

        #endregion
    }
}
