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

#endregion

namespace M3.Cord.Models.V0
{
    /// <summary>
    /// The Operator class.
    /// </summary>
    public class Operator : NInpc
    {
        #region Internal Variables

        private int _OperatorId = 0;

        private string _OperatorCode = string.Empty;

        private string _Title = string.Empty;
        private string _FirstName = string.Empty;
        private string _LastName = string.Empty;

        private string _UserName = string.Empty;
        private string _Password = string.Empty;

        private int _Active = 1;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Operator() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~Operator() { }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets OperatorId.
        /// </summary>
        public int OperatorId
        {
            get { return _OperatorId; }
            set
            {
                if (_OperatorId != value)
                {
                    _OperatorId = value;
                    Raise(() => OperatorId);
                }
            }
        }
        /// <summary>
        /// Gets or sets Operator Code.
        /// </summary>
        public string OperatorCode
        {
            get { return _OperatorCode; }
            set
            {
                if (_OperatorCode != value)
                {
                    _OperatorCode = value;
                    Raise(() => OperatorCode);
                }
            }
        }
        /// <summary>
        /// Gets or sets Title.
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    Raise(() => Title);
                    Raise(() => FullName);
                }
            }
        }
        /// <summary>
        /// Gets or sets First Name.
        /// </summary>
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                if (_FirstName != value)
                {
                    _FirstName = value;
                    Raise(() => FirstName);
                    Raise(() => FullName);
                }
            }
        }
        /// <summary>
        /// Gets or sets Last Name.
        /// </summary>
        public string LastName
        {
            get { return _LastName; }
            set
            {
                if (_LastName != value)
                {
                    _LastName = value;
                    Raise(() => LastName);
                    Raise(() => FullName);
                }
            }
        }

        public string FullName
        {
            get 
            {
                return string.Format("{0} {1} {2}", Title, FirstName, LastName);
            }
            set { }
        }

        /// <summary>
        /// Gets or sets User Name.
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set
            {
                if (_UserName != value)
                {
                    _UserName = value;
                    Raise(() => UserName);
                }
            }
        }
        /// <summary>
        /// Gets or sets Password.
        /// </summary>
        public string Password
        {
            get { return _Password; }
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    Raise(() => Password);
                }
            }
        }
        /// <summary>
        /// Gets or sets Active.
        /// </summary>
        public int Active
        {
            get { return _Active; }
            set
            {
                if (_Active != value)
                {
                    _Active = value;
                    Raise(() => Active);
                }
            }
        }

        #endregion

        #region Static Methods

        #endregion
    }
}
