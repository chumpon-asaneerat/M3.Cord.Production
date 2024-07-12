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
using System.Windows.Controls.Primitives;
using System.Windows.Data;

#endregion

namespace M3.Cord.Models
{
    public class S9AppearanceCheckSheetItem : NInpc
    {
        #region Const

        public static readonly SolidColorBrush RedColor = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush BlackColor = new SolidColorBrush(Colors.Black);

        public static readonly SolidColorBrush SilverColor = new SolidColorBrush(Colors.Silver);
        public static readonly SolidColorBrush TransparentColor = new SolidColorBrush(Colors.Transparent);

        #endregion

        public SolidColorBrush TextColor 
        { 
            get { return (!_SPUnusable) ? BlackColor : RedColor; } 
            set { } 
        }

        #region Public Properties

        public int AppearId { get; set; }
        public int SPNo { get; set; }

        private bool _SPUnusable = false;
        public bool SPUnusable
        {
            get { return _SPUnusable; }
            set
            {
                if (_SPUnusable != value)
                {
                    _SPUnusable = value;

                    if (value)
                    {
                        MarkUnuseableSP();
                    }
                    Raise(() => this.SPUnusable);
                    Raise(() => this.TextColor);
                }
            }
        }

        private bool _CheckGood = false;
        public bool CheckGood 
        {
            get
            {
                return _CheckGood;
            }
            set
            {
                if (_SPUnusable) return;

                if (value)
                {
                    ClearDefects();
                }

                _CheckGood = value;
                Raise(() => this.CheckGood);
            }
        }
        private bool _CheckBad = false;
        public bool CheckBad
        {
            get
            {
                return _CheckBad;
            }
            set
            {
                if (_SPUnusable) return;
                if (value)
                {
                    SetBadForm();
                }

                _CheckBad = value;
                Raise(() => this.CheckBad);
            }
        }

        private bool _Check2Color = false;
        public bool Check2Color 
        {
            get
            {
                return _Check2Color;
            }
            set
            {
                if (_SPUnusable) return;
                if (value)
                {
                    SetBadForm();
                }

                _Check2Color = value;
                Raise(() => this.Check2Color);
            }
        }

        private bool _CheckKeiba = false;
        public bool CheckKeiba
        {
            get 
            {
                return _CheckKeiba; 
            }
            set
            {
                if (_SPUnusable) return;
                if (value)
                {
                    SetBadForm();
                }

                _CheckKeiba = value;
                Raise(() => this.CheckKeiba);
            }
        }

        private string _CheckWeight = null;
        public string CheckWeight
        {
            get
            {
                return _CheckWeight;
            }
            set
            {
                if (_SPUnusable) return;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    SetBadForm();
                }

                _CheckWeight = string.IsNullOrWhiteSpace(value) ? null : value;
                Raise(() => this.CheckWeight);
            }
        }

        private bool _CheckFrontTwist = false;
        public bool CheckFrontTwist
        {
            get
            {
                return _CheckFrontTwist;
            }
            set
            {
                if (_SPUnusable) return;
                if (value)
                {
                    SetBadForm();
                }

                _CheckFrontTwist = value;
                Raise(() => this.CheckFrontTwist);
            }
        }

        private bool _CheckBackTwist = false;
        public bool CheckBackTwist
        {
            get
            {
                return _CheckBackTwist;
            }
            set
            {
                if (_SPUnusable) return;
                if (value)
                {
                    SetBadForm();
                }

                _CheckBackTwist = value;
                Raise(() => this.CheckBackTwist);
            }
        }

        private bool _CheckSnarl = false;
        public bool CheckSnarl
        {
            get
            {
                return _CheckSnarl;
            }
            set
            {
                if (_SPUnusable) return;
                if (value)
                {
                    SetBadForm();
                }

                _CheckSnarl = value;
                Raise(() => this.CheckSnarl);
            }
        }

        private bool _CheckTube = false;
        public bool CheckTube
        {
            get
            {
                return _CheckTube;
            }
            set
            {
                if (_SPUnusable) return;
                if (value)
                {
                    SetBadForm();
                }

                _CheckTube = value;
                Raise(() => this.CheckTube);
            }
        }

        public string Remark { get; set; }

        #endregion

        #region Private Methods

        private void MarkUnuseableSP()
        {
            _CheckGood = false;
            _CheckBad = false;
            _Check2Color = false;
            _CheckKeiba = false;
            _CheckWeight = null;
            _CheckFrontTwist = false;
            _CheckBackTwist = false;
            _CheckSnarl = false;
            _CheckTube = false;

            Raise(() => this.CheckBad);
            Raise(() => this.CheckGood);
            Raise(() => this.Check2Color);
            Raise(() => this.CheckKeiba);
            Raise(() => this.CheckWeight);
            Raise(() => this.CheckFrontTwist);
            Raise(() => this.CheckBackTwist);
            Raise(() => this.CheckSnarl);
            Raise(() => this.CheckTube);
        }

        private void ClearDefects()
        {
            _CheckBad = false;
            _Check2Color = false;
            _CheckKeiba = false;
            _CheckWeight = null;
            _CheckFrontTwist = false;
            _CheckBackTwist = false;
            _CheckSnarl = false;
            _CheckTube = false;

            Raise(() => this.CheckBad);
            Raise(() => this.Check2Color);
            Raise(() => this.CheckKeiba);
            Raise(() => this.CheckWeight);
            Raise(() => this.CheckFrontTwist);
            Raise(() => this.CheckBackTwist);
            Raise(() => this.CheckSnarl);
            Raise(() => this.CheckTube);
        }

        private void SetBadForm()
        {
            _CheckGood = false;
            Raise(() => this.CheckGood);
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<S9AppearanceCheckSheetItem>> Gets(int? AppearId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<S9AppearanceCheckSheetItem>> rets = new NDbResult<List<S9AppearanceCheckSheetItem>>();

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
            p.Add("@AppearId", AppearId);
            try
            {
                var items = cnn.Query<S9AppearanceCheckSheetItem>("GetS9AppearanceCheckSheetItems", p,
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
                rets.data = new List<S9AppearanceCheckSheetItem>();
            }

            return rets;
        }
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The S9AppearanceCheckSheetItem to save.</param>
        /// <returns></returns>
        public static NDbResult<S9AppearanceCheckSheetItem> Save(S9AppearanceCheckSheetItem value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<S9AppearanceCheckSheetItem> ret = new NDbResult<S9AppearanceCheckSheetItem>();

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

            p.Add("@AppearId", value.AppearId);
            p.Add("@SPNo", value.SPNo);
            p.Add("@CheckGood", value.CheckGood);
            p.Add("@CheckBad", value.CheckBad);
            p.Add("@Check2Color", value.Check2Color);
            p.Add("@CheckKeiba", value.CheckKeiba);
            p.Add("@CheckWeight", value.CheckWeight);
            p.Add("@CheckFrontTwist", value.CheckFrontTwist);
            p.Add("@CheckBackTwist", value.CheckBackTwist);
            p.Add("@CheckSnarl", value.CheckSnarl);
            p.Add("@CheckTube", value.CheckTube);
            p.Add("@SPUnusable", value.SPUnusable);
            p.Add("@Remark", value.Remark);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveS9AppearanceCheckSheetItem", p, commandType: CommandType.StoredProcedure);
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
