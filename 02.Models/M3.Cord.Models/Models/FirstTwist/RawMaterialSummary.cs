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
    public class RawMaterialSummary : NInpc
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        #endregion

        #region Public Properties

        public int Twist1LoadId { get; set; }
        public int PCTwist1Id { get; set; }
        public DateTime? ProductionDate { get; set; }

        public string ItemYarn { get; set; }

        public string PalletNo { get; set; }
        public string TraceNo { get; set; }

        public int DoffNo { get; set; }
        public int InputCH { get; set; }
        public int SPNoMin { get; set; }
        public int SPNoMax { get; set; }

        public string SPNos 
        {
            get 
            {
                if (SPNoMin == SPNoMax)
                {
                    return SPNoMax.ToString();
                }
                else
                {
                    return string.Format("{0}-{1}", SPNoMin, SPNoMax);
                }
            }
            set { }

        }

        public string ShiftName { get; set; }
        public string UserName { get; set; }
        public string Chief { get; set; }
        public string Remark { get; set; }

        public bool Keiba { get; set; }
        public bool Traverse { get; set; }
        public bool Crossing { get; set; }
        public bool ScrapeTube { get; set; }
        public bool Form { get; set; }
        public bool Stain { get; set; }

        public string SKeiba
        {
            get { return (Keiba) ? "X" : "-"; }
            set { }
        }
        public string STraverse
        {
            get { return (Traverse) ? "X" : "-"; }
            set { }
        }

        public string SCrossing
        {
            get { return (Crossing) ? "X" : "-"; }
            set { }
        }

        public string SScrapeTube
        {
            get { return (ScrapeTube) ? "X" : "-"; }
            set { }
        }

        public string SForm
        {
            get { return (Form) ? "X" : "-"; }
            set { }
        }

        public string SStain
        {
            get { return (Stain) ? "X" : "-"; }
            set { }
        }


        public SolidColorBrush TextColor { get { return BlackColor; } set { } }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<RawMaterialSummary>> Gets(int PCTwist1Id)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<RawMaterialSummary>> rets = new NDbResult<List<RawMaterialSummary>>();

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
                var items = cnn.Query<RawMaterialSummary>("GetRawMaterialSummaries", p,
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
                rets.data = new List<RawMaterialSummary>();
            }

            return rets;
        }

        #endregion
    }
}
