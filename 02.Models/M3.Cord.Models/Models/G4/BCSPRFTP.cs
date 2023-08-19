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
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Windows;
using System.Text;
using System.Drawing;
using System.Data.OleDb;

using M3.Cord.Utils;

#endregion

namespace M3.Cord.Models
{
    public class BCSPRFTP
    {
        #region Public Properties

        /// <summary>Flags</summary>
        public string ANNUL { get; set; }
        /// <summary>Flags</summary>
        public string FLAGS { get; set; }
        /// <summary>Type</summary>
        public string RECTY { get; set; }
        /// <summary>Warehouse</summary>
        public string CDSTO { get; set; }
        /// <summary>User Name</summary>
        public string USRNM { get; set; }
        /// <summary>Movement Date</summary>
        public string DTTRA { get; set; }
        /// <summary>Entry Date</summary>
        public string DTINP { get; set; }
        /// <summary>Roll No</summary>
        public string CDEL0 { get; set; }
        /// <summary>Pallet No</summary>
        public string CDCON { get; set; }
        /// <summary>Quantity</summary>
        public string BLELE { get; set; }
        /// <summary>Unit</summary>
        public string CDUM0 { get; set; }
        /// <summary>Item Code</summary>
        public string CDKE1 { get; set; }
        /// <summary>Production Type</summary>
        public string CDKE2 { get; set; }
        /// <summary>N/A</summary>
        public string CDKE3 { get; set; }
        /// <summary>N/A</summary>
        public string CDKE4 { get; set; }
        /// <summary>N/A</summary>
        public string CDKE5 { get; set; }
        /// <summary>Lot No</summary>
        public string CDLOT { get; set; }
        /// <summary>Transaction Code</summary>
        public string CDTRA { get; set; }
        /// <summary>Document No</summary>
        public string REFER { get; set; }
        /// <summary>Location</summary>
        public string LOCAT { get; set; }
        /// <summary>Grade</summary>
        public string CDQUA { get; set; }
        /// <summary>Defect Group</summary>
        public string QUACA { get; set; }
        /// <summary>Technical Data 1</summary>
        public string TECU1 { get; set; }
        /// <summary>Technical Data 2</summary>
        public string TECU2 { get; set; }
        /// <summary>Technical Data 3</summary>
        public string TECU3 { get; set; }
        /// <summary>Technical Data 4</summary>
        public string TECU4 { get; set; }
        /// <summary>Technical Data 5</summary>
        public string TECU5 { get; set; }
        /// <summary>Technical Data 6</summary>
        public string TECU6 { get; set; }
        /// <summary>Comment</summary>
        public string COMM0 { get; set; }
        /// <summary>Last Update Time</summary>
        public string DTORA { get; set; }

        #endregion

        #region Private Static Methods

        private static string DbPath
        {
            get
            {
                var assem = Assembly.GetExecutingAssembly();
                string rootPath = Path.GetDirectoryName(assem.Location);
                var dbPath = Path.Combine(rootPath, "db");
                if (!Directory.Exists(dbPath))
                    Directory.CreateDirectory(dbPath);
                return dbPath;
            }
        }

        private static string GetRow(DataRow row, string columnName)
        {
            return (null != row && null != row[columnName]) ? row[columnName].ToString() : null;
        }

        private static List<BCSPRFTP> CreateList(DataTable tbl)
        {
            List<BCSPRFTP> items = null;
            if (null != tbl && null != tbl.Rows)
            {
                int cnt = tbl.Rows.Count;
                items = new List<BCSPRFTP>();
                for (int i = 0; i < cnt; i++)
                {
                    var row = tbl.Rows[i];
                    var inst = new BCSPRFTP();

                    inst.ANNUL = GetRow(row, "ANNUL");
                    inst.FLAGS = GetRow(row, "FLAGS");
                    inst.RECTY = GetRow(row, "RECTY");
                    inst.CDSTO = GetRow(row, "CDSTO");
                    inst.USRNM = GetRow(row, "USRNM");
                    inst.DTTRA = GetRow(row, "DTTRA");
                    inst.DTINP = GetRow(row, "DTINP");
                    inst.CDEL0 = GetRow(row, "CDEL0");
                    inst.CDCON = GetRow(row, "CDCON");
                    inst.BLELE = GetRow(row, "BLELE");
                    inst.CDUM0 = GetRow(row, "CDUM0");
                    inst.CDKE1 = GetRow(row, "CDKE1");
                    inst.CDKE2 = GetRow(row, "CDKE2");
                    inst.CDKE3 = GetRow(row, "CDKE3");
                    inst.CDKE4 = GetRow(row, "CDKE4");
                    inst.CDKE5 = GetRow(row, "CDKE5");
                    inst.CDLOT = GetRow(row, "CDLOT");
                    inst.CDTRA = GetRow(row, "CDTRA");
                    inst.REFER = GetRow(row, "REFER");
                    inst.LOCAT = GetRow(row, "LOCAT");
                    inst.CDQUA = GetRow(row, "CDQUA");
                    inst.QUACA = GetRow(row, "QUACA");
                    inst.TECU1 = GetRow(row, "TECU1");
                    inst.TECU2 = GetRow(row, "TECU2");
                    inst.TECU3 = GetRow(row, "TECU3");
                    inst.TECU4 = GetRow(row, "TECU4");
                    inst.TECU5 = GetRow(row, "TECU5");
                    inst.TECU6 = GetRow(row, "TECU6");
                    inst.COMM0 = GetRow(row, "COMM0");
                    inst.DTORA = GetRow(row, "DTORA");

                    items.Add(inst);
                }
            }

            return items;
        }

        #endregion

        #region Public Static Class/Methods

        public class AS400
        {
            /// <summary>
            /// Gets Default Query
            /// </summary>
            /// <returns></returns>
            public static string GetQuery()
            {
                #region Query

                string query = string.Empty;

                query += "SELECT #ANNUL AS ANNUL " + Environment.NewLine;
                query += "     , #FLAGS AS FLAGS " + Environment.NewLine;
                query += "     , #RECTY AS RECTY " + Environment.NewLine;
                query += "     , #CDSTO AS CDSTO " + Environment.NewLine;
                query += "     , #USRNM AS USRNM " + Environment.NewLine;
                query += "     , #DTTRA AS DTTRA " + Environment.NewLine;
                query += "     , #DTINP AS DTINP " + Environment.NewLine;
                query += "     , #CDEL0 AS CDEL0 " + Environment.NewLine;
                query += "     , #CDCON AS CDCON " + Environment.NewLine;
                query += "     , #BLELE AS BLELE " + Environment.NewLine;
                query += "     , #CDUM0 AS CDUM0 " + Environment.NewLine;
                query += "     , #CDKE1 AS CDKE1 " + Environment.NewLine;
                query += "     , #CDKE2 AS CDKE2 " + Environment.NewLine;
                query += "     , #CDKE3 AS CDKE3 " + Environment.NewLine;
                query += "     , #CDKE4 AS CDKE4 " + Environment.NewLine;
                query += "     , #CDKE5 AS CDKE5 " + Environment.NewLine;
                query += "     , #CDLOT AS CDLOT " + Environment.NewLine;
                query += "     , #CDTRA AS CDTRA " + Environment.NewLine;
                query += "     , #REFER AS REFER " + Environment.NewLine;
                query += "     , #LOCAT AS LOCAT " + Environment.NewLine;
                query += "     , #CDQUA AS CDQUA " + Environment.NewLine;
                query += "     , #QUACA AS QUACA " + Environment.NewLine;
                query += "     , #TECU1 AS TECU1 " + Environment.NewLine;
                query += "     , #TECU2 AS TECU2 " + Environment.NewLine;
                query += "     , #TECU3 AS TECU3 " + Environment.NewLine;
                query += "     , #TECU4 AS TECU4 " + Environment.NewLine;
                query += "     , #TECU5 AS TECU5 " + Environment.NewLine;
                query += "     , #TECU6 AS TECU6 " + Environment.NewLine;
                query += "     , #COMM0 AS COMM0 " + Environment.NewLine;
                query += "     , #DTORA AS DTORA " + Environment.NewLine;
                query += "  FROM BCSPRFTP " + Environment.NewLine;
                query += " WHERE #CDSTO = '3G' " + Environment.NewLine;

                #endregion

                return query;
            }

            public static List<BCSPRFTP> Gets()
            {
                string query = GetQuery();
                return Gets(query);
            }

            public static bool Issue(BCSPRFTP item)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                bool ret = false;
                if (null == item)
                    return ret;

                string queryString;
                queryString = " INSERT INTO BCSPRFTP( #ANNUL, #FLAGS, #RECTY, #CDSTO, #USRNM "
                                + " , #DTTRA  , #DTINP"
                                + " , #CDEL0, #CDCON "
                                + " , #BLELE "
                                + " , #CDUM0 , #CDKE1 , #CDKE2, #CDKE3, #CDKE4, #CDKE5 "
                                + " , #CDLOT, #CDTRA, #REFER , #LOCAT, #CDQUA, #QUACA "
                                + " , #TECU1, #TECU2 , #TECU3, #TECU4 "
                                + " , #TECU5 , #TECU6, #COMM0 "
                                + " , #DTORA ) "
                                + " VALUES( ? , ? , ? , ? , ? "
                                + " , ? , ? "
                                + " , ? , ? "
                                + " , ? "
                                + " , ? , ? , ? , ? , ? , ? "
                                + " , ? , ? , ? , ? , ? , ? "
                                + " , ? , ? , ? , ? "
                                + " , ? , ?, ? "
                                + " , ? ) ";
                var cmd = AS400DbServer.Instance.GetCommand(queryString);
                if (null != cmd)
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@#ANNUL", OleDbType.Char).Value = item.ANNUL;
                        cmd.Parameters.AddWithValue("@#FLAGS", OleDbType.Char).Value = item.FLAGS;
                        cmd.Parameters.AddWithValue("@#RECTY", OleDbType.Char).Value = item.RECTY;
                        cmd.Parameters.AddWithValue("@#CDSTO", OleDbType.Char).Value = item.CDSTO;
                        cmd.Parameters.AddWithValue("@#USRNM", OleDbType.Char).Value = item.USRNM;
                        cmd.Parameters.AddWithValue("@#DTTRA", OleDbType.Numeric).Value = item.DTTRA.AsInt32();
                        cmd.Parameters.AddWithValue("@#DTINP", OleDbType.Numeric).Value = item.DTINP.AsInt32();
                        cmd.Parameters.AddWithValue("@#CDEL0", OleDbType.Char).Value = item.CDEL0;
                        cmd.Parameters.AddWithValue("@#CDCON", OleDbType.Char).Value = item.CDCON;
                        cmd.Parameters.AddWithValue("@#BLELE", OleDbType.Numeric).Value = item.BLELE;
                        cmd.Parameters.AddWithValue("@#CDUM0", OleDbType.Char).Value = item.CDUM0;
                        cmd.Parameters.AddWithValue("@#CDKE1", OleDbType.Char).Value = item.CDKE1;
                        cmd.Parameters.AddWithValue("@#CDKE2", OleDbType.Char).Value = item.CDKE2;
                        cmd.Parameters.AddWithValue("@#CDKE3", OleDbType.Char).Value = item.CDKE3;
                        cmd.Parameters.AddWithValue("@#CDKE4", OleDbType.Char).Value = item.CDKE4;
                        cmd.Parameters.AddWithValue("@#CDKE5", OleDbType.Char).Value = item.CDKE5;
                        cmd.Parameters.AddWithValue("@#CDLOT", OleDbType.Char).Value = item.CDLOT;
                        cmd.Parameters.AddWithValue("@#CDTRA", OleDbType.Char).Value = item.CDTRA;
                        cmd.Parameters.AddWithValue("@#REFER", OleDbType.Char).Value = item.REFER;
                        cmd.Parameters.AddWithValue("@#LOCAT", OleDbType.Char).Value = item.LOCAT;
                        cmd.Parameters.AddWithValue("@#CDQUA", OleDbType.Char).Value = item.CDQUA;
                        cmd.Parameters.AddWithValue("@#QUACA", OleDbType.Char).Value = item.QUACA;
                        cmd.Parameters.AddWithValue("@#TECU1", OleDbType.Numeric).Value = item.TECU1.AsDecimal();
                        cmd.Parameters.AddWithValue("@#TECU2", OleDbType.Numeric).Value = item.TECU2.AsDecimal();
                        cmd.Parameters.AddWithValue("@#TECU3", OleDbType.Numeric).Value = item.TECU3.AsDecimal();
                        cmd.Parameters.AddWithValue("@#TECU4", OleDbType.Numeric).Value = item.TECU4.AsDecimal();
                        cmd.Parameters.AddWithValue("@#TECU5", OleDbType.Char).Value = item.TECU5;
                        cmd.Parameters.AddWithValue("@#TECU6", OleDbType.Char).Value = item.TECU6;
                        cmd.Parameters.AddWithValue("@#COMM0", OleDbType.Char).Value = item.COMM0;
                        cmd.Parameters.AddWithValue("@#DTORA", OleDbType.Numeric).Value = item.DTORA;

                        // Dump Value
                        string dump = string.Empty;
                        dump += "--- BEGIN SEND BCSPRFTP TO AS400 DUMP ---" + Environment.NewLine;
                        for (int i = 0; i < cmd.Parameters.Count; ++i)
                        {
                            var param = cmd.Parameters[i];

                            dump += string.Format("Name: {0}, Value: {1}", param.ParameterName, param.Value);
                            dump += Environment.NewLine;
                        }
                        Console.WriteLine("--- END DUMP ---");
                        dump += "--- END SEND BCSPRFTP TO AS400 DUMP ---" + Environment.NewLine;

                        Console.WriteLine(dump);

                        //cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        med.Err(ex);
                    }
                    finally
                    {
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                    }
                }

                return ret;
            }

            public static bool Delete(BCSPRFTP item)
            {
                bool ret = false;
                if (null == item) 
                    return ret;

                StringBuilder q = new StringBuilder();

                q.Append("DELETE FROM BCSPRFTP ");
                q.Append(" WHERE #CDEL0 = '");
                q.Append(item.CDEL0);
                q.Append("'  AND #CDCON = '");
                q.Append(item.CDCON);
                q.Append("'  AND #CDKE1 = '");
                q.Append(item.CDKE1);
                q.Append("'  AND #FLAGS = 'S' And #RECTY = 'S' And #CDSTO = '3G' ");

                AS400DbServer.Instance.ExecuteNonQuery(q.ToString());

                return ret;
            }

            /// <summary>
            /// Gets from AS400
            /// </summary>
            /// <param name="query"></param>
            /// <returns></returns>
            public static List<BCSPRFTP> Gets(string query)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                List<BCSPRFTP> rets = null;

                if (!AS400DbServer.Instance.Connected)
                {
                    string msg = "Connection is null or cannot connect to database server.";
                    med.Err(msg);
                    return rets;
                }

                try
                {
                    var table = AS400DbServer.Instance.ExecuteQuery(query, "BCSPRFTP");
                    if (null != table)
                    {
                        rets = CreateList(table);
                    }
                }
                catch (Exception ex)
                {
                    med.Err(ex);
                    rets = null;
                }

                return rets;
            }

            public static List<BCSPRFTP> LoadFromFile()
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                List<BCSPRFTP> rets = null;
                try
                {
                    string fileName = Path.Combine(DbPath, "BCSPRFTP.json");
                    if (File.Exists(fileName))
                    {
                        rets = NJson.LoadFromFile<List<BCSPRFTP>>(fileName);
                    }
                }
                catch (Exception ex)
                {
                    med.Err(ex);
                    rets = null;
                }
                return rets;
            }
        }

        public class M3Cord
        {
            private static DateTime? ToDateTime(string value)
            {
                DateTime? ret;
                try
                {
                    int yr = int.Parse(value.Substring(0, 4));
                    int mn = int.Parse(value.Substring(4, 2));
                    int dd = int.Parse(value.Substring(6, 2));

                    ret = new DateTime(yr, mn, dd);
                }
                catch
                {
                    ret = new DateTime?();
                }
                return ret;
            }

            private static decimal? ToDecimal(string value)
            {
                decimal? ret;
                decimal val;
                if (!decimal.TryParse(value, out val))
                {
                    ret = new decimal?();
                }
                else ret = val;

                return ret;
            }

            /// <summary>
            /// Save
            /// </summary>
            /// <param name="value">The G4Yarn item to save.</param>
            /// <returns></returns>
            public static NDbResult Save(BCSPRFTP value)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult ret = new NDbResult();

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
                p.Add("@DTTRA", ToDateTime(value.DTTRA)); // MovementDate
                p.Add("@DTINP", ToDateTime(value.DTINP)); // EntryDate
                p.Add("@CDCON", value.CDCON); // Pallet No
                p.Add("@BLELE", ToDecimal(value.BLELE)); // WeightQty

                p.Add("@CDUM0", value.CDUM0); // Unit
                p.Add("@CDKE1", value.CDKE1); // Item400
                p.Add("@CDLOT", value.CDLOT); // Lot No
                p.Add("@CDQUA", value.CDQUA); // N/A

                p.Add("@TECU1", ToDecimal(value.TECU1));
                p.Add("@TECU2", ToDecimal(value.TECU2));
                p.Add("@TECU3", ToDecimal(value.TECU3)); // Cone CH
                p.Add("@TECU4", ToDecimal(value.TECU4));
                p.Add("@TECU5", ToDecimal(value.TECU5));

                p.Add("@TECU6", value.TECU6); // Trace No

                p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

                try
                {
                    cnn.Execute("SaveBCSPRFTP", p, commandType: CommandType.StoredProcedure);
                    ret.Success();
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

            public static NDbResult Save(List<BCSPRFTP> values)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult ret = new NDbResult();

                if (null == values)
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

                try
                {
                    foreach (var item in values) 
                    { 
                        Save(item); 
                    }
                    ret.Success(); // mark as success
                }
                catch (Exception ex)
                {
                    med.Err(ex);
                }

                return ret;
            }
        }

        #endregion
    }
}
