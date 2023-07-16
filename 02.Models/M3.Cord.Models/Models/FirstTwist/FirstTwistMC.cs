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

#endregion

namespace M3.Cord.Models
{
    public class FirstTwistMC : NInpc
    {
        #region Public Properties

        public string MCCode { get; set; }
        public string ProcessName { get; set; }
        public int DeckPerCore { get; set; }
        public int StartCore { get; set; }
        public int EndCore { get; set; }

        #endregion

        #region Static Methods

        public static NDbResult<List<FirstTwistMC>> Gets()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<FirstTwistMC>> rets = new NDbResult<List<FirstTwistMC>>();

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
                var items = cnn.Query<FirstTwistMC>("GetFirstTwistMCs", p,
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
                rets.data = new List<FirstTwistMC>();
            }

            return rets;
        }

        #endregion
    }

    public class MCBBCondition
    {
        #region Public Properties

        public DateTime RecordDate { get; set; }

        public string ProductLotNo { get; set; }
        public string ItemCode { get; set; }

        public string MCCode { get; set; }
        public int DoffNo { get; set; }

        #endregion
    }

    public class MCBBConditionItem
    {
        #region Public Properties

        public int BBNo { get; set; }

        public bool RawmaterialB { get; set; }
        public bool RawmaterialA { get; set; }

        public bool CrossYarnB { get; set; }
        public bool CrossYarnA { get; set; }

        public bool FormB { get; set; }
        public bool FormA { get; set; }

        public bool KebaB { get; set; }
        public bool KebaA { get; set; }

        public bool StainB { get; set; }
        public bool StainA { get; set; }

        public bool PaperTubeB { get; set; }
        public bool PaperTubeA { get; set; }

        public bool YarnNoB { get; set; }
        public bool YarnNoA { get; set; }

        public bool BBMarkB { get; set; }
        public bool BBMarkA { get; set; }

        #endregion
    }

    /*
    public class YarnLoadingItem : NInpc
    {
        public string DoffNo { get; set; }
        public string ItemYarn { get; set; }
        public string LotNo { get; set; }
        public int SPNo { get; set; }
        public int DeckNo { get; set; }

        public static List<YarnLoadingItem> Gets()
        {
            List<YarnLoadingItem> rets = new List<YarnLoadingItem>();

            int DoffNo = 1;
            string itemYarn = "470-72-1781-JJ";
            string lotNo = "21101";
            for (int i = 0; i < 10; ++i)
            {
                int SPNo = i + 1;

                for (int j = 0; j < 2; ++j)
                {
                    var inst = new YarnLoadingItem();
                    inst.DoffNo = DoffNo.ToString();
                    inst.ItemYarn = itemYarn;
                    inst.LotNo = lotNo;
                    inst.SPNo = SPNo;
                    inst.DeckNo = j + 1;
                    rets.Add(inst);
                }
            }

            return rets;
        }
    }
    */
}
