#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace NLib.Services
{
    using LiteDB;

    #region

    public class LiteDbSerivce : NSingelton<LiteDbSerivce>
    {
        #region Static class

        /// <summary>The Working paths</summary>
        public static class Paths
        {
            /// <summary>App Location Path.</summary>
            public readonly static string App = Assembly.GetExecutingAssembly().Location;
            /// <summary>Db File Location Path.</summary>
            public readonly static string Db = Path.Combine(App, "db");
        }

        #endregion

        #region Internal Variables

        private LiteDatabase _db = null;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        protected LiteDbSerivce() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~LiteDbSerivce()
        {
            Shutdown();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start Service.
        /// </summary>
        public void Start() 
        {
            if (null != _db) return;
            if (Directory.Exists(Paths.Db))
            {
                try { Directory.CreateDirectory(Paths.Db); }
                catch { }
            }
            if (!Directory.Exists(Paths.Db)) return;
            if (string.IsNullOrEmpty(FileName)) return;

            _db = new LiteDatabase(Path.Combine(Paths.Db, FileName));
        }
        /// <summary>
        /// Shutdown Service.
        /// </summary>
        public void Shutdown() 
        {
            if (null != _db)
            {
                _db.Dispose();
            }
            _db = null;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets LiteDb File Name.
        /// </summary>
        public string FileName { get; set; } = "cord.db";
        /// <summary>
        /// Gets is connected.
        /// </summary>
        public bool IsConnected { get { return (null != _db); } }

        #endregion
    }

    #endregion
}
