#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Reflection;

using NLib;
using NLib.Components;
using NLib.Data;
using NLib.IO;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace M3.Cord
{
    #region Connection Config

    /// <summary>
    /// The AS400 database config.
    /// </summary>
    public class AS400DbConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public AS400DbConfig() : base()
        {
            DataSource = "172.20.7.16";
            UserId = "PLM";
            Password = "LUCKY";
            DefaultColl = "TESLUCDAT";
        }

        #endregion

        #region Public Methods

        public string GetConnectionString()
        {
            return string.Format(
                "Provider=IBMDA400;Data Source={0};User Id={1};Password={2};Default Collection={3};",
                DataSource, UserId, Password, DefaultColl);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets ServerName or IP address.
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// Gets or sets User Id.
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Gets or sets Password.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets Default Collection.
        /// </summary>
        public string DefaultColl { get; set; }

        #endregion
    }

    #endregion

    #region AS400 DbServer

    /// <summary>
    /// AS400 Db Server (SqlServer).
    /// </summary>
    public class AS400DbServer : NSingelton<AS400DbServer>
    {
        #region Internal Variables

        private OleDbConnection _connection;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        protected AS400DbServer() : base()
        {

        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~AS400DbServer()
        {
            Shutdown();
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Gets local Configs folder path name.
        /// </summary>
        private static string ConfigFolder
        {
            get
            {
                string localFilder = Folders.Combine(
                    Folders.Assemblies.CurrentExecutingAssembly, "Configs");

                if (!Folders.Exists(localFilder))
                {
                    Folders.Create(localFilder);
                }
                return localFilder;
            }
        }

        #endregion

        #region Private Methods

        private bool ConfigExists(string filename)
        {
            string configPath = Path.Combine(NJson.AppPath, "Configs");
            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
            }

            string fullFileName = Path.Combine(configPath, filename);
            return File.Exists(fullFileName);
        }

        private AS400DbConfig GetConfig()
        {
            AS400DbConfig cfg;

            string fileName = Path.Combine(ConfigFolder, "AS400DbServer.json");
            if (!ConfigExists(fileName))
            {
                // create new one and save.
                cfg = new AS400DbConfig();
                NJson.SaveToFile(cfg, fileName, false);
            }

            cfg = NJson.LoadFromFile<AS400DbConfig>(fileName);

            if (null == cfg)
            {
                // create new one and save.
                cfg = new AS400DbConfig();
                //NJson.SaveToFile(cfg, fileName, false);
            }
            return cfg;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start.
        /// </summary>
        public void Start()
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            if (null == Db)
            {
                lock (typeof(DbServer))
                {
                    try
                    {
                        var jCfg = GetConfig();

                        _connection = new OleDbConnection();
                        _connection.ConnectionString = jCfg.GetConnectionString();
                        _connection.Open();
                    }
                    catch (Exception ex)
                    {
                        med.Err(ex);
                        _connection = null;

                        OnConectError.Call(this, EventArgs.Empty);
                    }
                    if (null != _connection && _connection.State == ConnectionState.Open)
                    {
                        OnConnected.Call(this, EventArgs.Empty);
                    }
                    else
                    {
                        Shutdown();

                        OnConectError.Call(this, EventArgs.Empty);
                    }
                }
            }
        }
        /// <summary>
        /// Shutdown.
        /// </summary>
        public void Shutdown()
        {
            if (null != _connection)
            {
                try 
                {
                    _connection.Close();
                    _connection.Dispose(); 
                }
                catch { }
            }
            _connection = null;

            OnDisconnected.Call(this, EventArgs.Empty);
        }

        public DataTable ExecuteQuery(string query, string sourceTable)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            DataSet dataSet = new DataSet();
            OleDbDataAdapter adapter;
            try
            {
                adapter = new OleDbDataAdapter(query, _connection);
                adapter.Fill(dataSet, sourceTable);
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }

            return (null != dataSet && null != dataSet.Tables && dataSet.Tables.Count > 0) ? 
                dataSet.Tables[0] : null;
        }

        public bool ExecuteNonQuery(string query)
        {
            bool success = true;
            MethodBase med = MethodBase.GetCurrentMethod();

            try
            {
                var cmd = new OleDbCommand(query, _connection);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                med.Err(ex);
                success = false;
            }
            return success;
        }

        public OleDbCommand GetCommand(string query)
        {
            OleDbCommand cmd;
            MethodBase med = MethodBase.GetCurrentMethod();

            try
            {
                cmd = new OleDbCommand(query, _connection);
            }
            catch (Exception ex)
            {
                med.Err(ex);
                cmd = null;
            }
            return cmd;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets IDbConnection.
        /// </summary>
        public IDbConnection Db
        {
            get { return _connection; }
        }
        /// <summary>
        /// Gets is database connected.
        /// </summary>
        public bool Connected
        {
            get { return null != _connection && _connection.State == ConnectionState.Open; }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// OnConnected event.
        /// </summary>
        public event EventHandler OnConnected;
        /// <summary>
        /// OnDisconnected event.
        /// </summary>
        public event EventHandler OnDisconnected;
        /// <summary>
        /// OnConectError event.
        /// </summary>
        public event EventHandler OnConectError;

        #endregion
    }

    #endregion
}
