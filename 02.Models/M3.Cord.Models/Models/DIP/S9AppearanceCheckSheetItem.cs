﻿#region Using

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
                _SPUnusable = value;
                if (value)
                {
                    MarkUnuseableSP();
                }
                Raise(() => this.SPUnusable);
                Raise(() => this.TextColor);
            }
        }

        private bool? _CheckGood = new bool?();
        public bool? CheckGood 
        {
            get
            {
                return _CheckGood.HasValue ? _CheckGood.Value : false;
            }
            set
            {
                if (_SPUnusable) return;
                if (value.HasValue && value.Value == true)
                {
                    ClearDefects();
                }

                _CheckGood = value;
                Raise(() => this.CheckGood);
            }
        }
        public bool? CheckBad { get; set; } // not used anymore

        private bool? _Check2Color = new bool?();
        public bool? Check2Color 
        {
            get
            {
                return _Check2Color.HasValue ? _Check2Color.Value : false;
            }
            set
            {
                if (_SPUnusable) return;
                if (value.HasValue && value.Value == true)
                {
                    SetBadForm();
                }

                _Check2Color = value;
                Raise(() => this.Check2Color);
            }
        }

        private bool? _CheckKeiba = new bool?();
        public bool? CheckKeiba
        {
            get 
            { 
                return _CheckKeiba.HasValue ? _CheckKeiba.Value : false; 
            }
            set
            {
                if (_SPUnusable) return;
                if (value.HasValue && value.Value == true)
                {
                    SetBadForm();
                }

                _CheckKeiba = value;
                Raise(() => this.CheckKeiba);
            }
        }

        private bool? _CheckWeight = new bool?();
        public bool? CheckWeight
        {
            get
            {
                return _CheckWeight.HasValue ? _CheckWeight.Value : false;
            }
            set
            {
                if (_SPUnusable) return;
                if (value.HasValue && value.Value == true)
                {
                    SetBadForm();
                }

                _CheckWeight = value;
                Raise(() => this.CheckWeight);
            }
        }

        private bool? _CheckFrontTwist = new bool?();
        public bool? CheckFrontTwist
        {
            get
            {
                return _CheckFrontTwist.HasValue ? _CheckFrontTwist.Value : false;
            }
            set
            {
                if (_SPUnusable) return;
                if (value.HasValue && value.Value == true)
                {
                    SetBadForm();
                }

                _CheckFrontTwist = value;
                Raise(() => this.CheckFrontTwist);
            }
        }

        private bool? _CheckBackTwist = new bool?();
        public bool? CheckBackTwist
        {
            get
            {
                return _CheckBackTwist.HasValue ? _CheckBackTwist.Value : false;
            }
            set
            {
                if (_SPUnusable) return;
                if (value.HasValue && value.Value == true)
                {
                    SetBadForm();
                }

                _CheckBackTwist = value;
                Raise(() => this.CheckBackTwist);
            }
        }

        private bool? _CheckSnarl = new bool?();
        public bool? CheckSnarl
        {
            get
            {
                return _CheckSnarl.HasValue ? _CheckSnarl.Value : false;
            }
            set
            {
                if (_SPUnusable) return;
                if (value.HasValue && value.Value == true)
                {
                    SetBadForm();
                }

                _CheckSnarl = value;
                Raise(() => this.CheckSnarl);
            }
        }

        private bool? _CheckTube = new bool?();
        public bool? CheckTube
        {
            get
            {
                return _CheckTube.HasValue ? _CheckTube.Value : false;
            }
            set
            {
                if (_SPUnusable) return;
                if (value.HasValue && value.Value == true)
                {
                    SetBadForm();
                }

                _CheckTube = value;
                Raise(() => this.CheckTube);
            }
        }

        #endregion

        #region Private Methods

        private void MarkUnuseableSP()
        {
            _CheckGood = false;
            //_CheckBad = false;
            _Check2Color = false;
            _CheckKeiba = false;
            _CheckWeight = false;
            _CheckFrontTwist = false;
            _CheckBackTwist = false;
            _CheckSnarl = false;
            _CheckTube = false;

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
            //_CheckBad = false;
            _Check2Color = false;
            _CheckKeiba = false;
            _CheckWeight = false;
            _CheckFrontTwist = false;
            _CheckBackTwist = false;
            _CheckSnarl = false;
            _CheckTube = false;

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
