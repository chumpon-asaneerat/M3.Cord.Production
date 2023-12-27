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
using BarcodeLib;

#endregion

namespace M3.Cord.Models
{
    public class SolutionLotLabel : NInpc
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        #endregion

        private static Barcode BarcodeGenerator = null;

        static SolutionLotLabel()
        {
            BarcodeGenerator = new Barcode();
            BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
            BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
            BarcodeGenerator.IncludeLabel = false;
        }

        #region Public Proeprties

        public int SolutionId { get; set; }
        public string SolutionLot { get; set; }
        // SolutionLot barcode
        public byte[] SolutionLotImage
        {
            get
            {
                byte[] results = null;
                if (!string.IsNullOrWhiteSpace(SolutionLot))
                {
                    System.Drawing.Image img = BarcodeGenerator.Encode(BarcodeGenerator.EncodedType,
                        SolutionLot, 400, 100);

                    results = NLib.Utils.ImageUtils.GetImage(img);
                }
                return results;
            }
            set { }
        }


        public decimal? DipSolutionQty { get; set; }

        public DateTime? MixDate { get; set; }
        public DateTime? QualifiedDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public int CreateBy { get; set; }

        // runtime
        private string _CreateByUserName = null;
        public string CreateByUserName 
        {
            get
            {
                if (_CreateByUserName == null)
                {
                    var user = UserInfo.Get(CreateBy).Value();
                    if (null != user)
                    {
                        _CreateByUserName = user.FullName;
                    }
                    else
                    {
                        _CreateByUserName = string.Empty;
                    }
                }
                return _CreateByUserName;
            }
        }

        public string ProductCode { get; set; }
        public string SolutionName { get; set; }
        public string Compound { get; set; }

        public SolidColorBrush TextColor { get { return BlackColor; } set { } }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<SolutionLotLabel>> Gets(string solutionlot = null)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<SolutionLotLabel>> rets = new NDbResult<List<SolutionLotLabel>>();

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
            p.Add("@solutionlot", string.IsNullOrWhiteSpace(solutionlot) ? null : solutionlot);
            try
            {
                var items = cnn.Query<SolutionLotLabel>("ChSearchSolutionLot", p,
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
                rets.data = new List<SolutionLotLabel>();
            }

            return rets;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The PCCard item to save.</param>
        /// <returns></returns>
        public static NDbResult<SolutionLotLabel> Save(SolutionLotLabel value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<SolutionLotLabel> ret = new NDbResult<SolutionLotLabel>();

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
            p.Add("@solutionlot", value.SolutionLot);
            p.Add("@solutionid", value.SolutionId);
            p.Add("@qty", value.DipSolutionQty);
            p.Add("@mixdate", value.MixDate);
            p.Add("@qualifieddate", value.QualifiedDate);
            p.Add("@expiredate", value.ExpireDate);
            p.Add("@userid", value.CreateBy);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("ChSaveSolutionLotLabel", p, commandType: CommandType.StoredProcedure);
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
